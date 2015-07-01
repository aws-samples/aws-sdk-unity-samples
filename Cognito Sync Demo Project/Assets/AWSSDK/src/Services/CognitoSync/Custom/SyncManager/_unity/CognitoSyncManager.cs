//
// Copyright 2014-2015 Amazon.com, 
// Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the Amazon Software License (the "License"). 
// You may not use this file except in compliance with the 
// License. A copy of the License is located at
// 
//     http://aws.amazon.com/asl/
// 
// or in the "license" file accompanying this file. This file is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, express or implied. See the License 
// for the specific language governing permissions and 
// limitations under the License.
//

using System;
using System.Collections.Generic;

using Amazon.Runtime;
using Amazon.CognitoSync.SyncManager.Internal;
using Amazon.CognitoIdentity;
using Amazon.Runtime.Internal.Util;
using UnityEngine;
using Amazon.Util.Internal;

namespace Amazon.CognitoSync.SyncManager
{
    public class CognitoSyncManager:IDisposable
    {
        private Logger _logger;
        private bool _disposed;

        protected static readonly string DATABASE_NAME = "aws_cognito_cache.db";

        protected ILocalStorage Local
        {
            get;
            set;
        }

        protected readonly CognitoSyncStorage remote;
        protected readonly CognitoAWSCredentials cognitoCredentials;

        public CognitoSyncManager(CognitoAWSCredentials cognitoCredentials) : this(cognitoCredentials, new AmazonCognitoSyncConfig()) { }

        public CognitoSyncManager(CognitoAWSCredentials cognitoCredentials, RegionEndpoint endpoint)
            : this(cognitoCredentials, new AmazonCognitoSyncConfig {
                RegionEndpoint = endpoint
            })
        { }

        public CognitoSyncManager(CognitoAWSCredentials cognitoCredentials, AmazonCognitoSyncConfig config)
        {
            if (cognitoCredentials == null)
            {
                throw new ArgumentNullException("cognitoCredentials");
            }

            if (string.IsNullOrEmpty(cognitoCredentials.IdentityPoolId))
            {
                throw new ArgumentNullException("cognitoCredentials.IdentityPoolId");
            }
            this.cognitoCredentials = cognitoCredentials;
            Local = new SQLiteLocalStorage(System.IO.Path.Combine(Application.persistentDataPath, DATABASE_NAME));
            remote = new CognitoSyncStorage(cognitoCredentials, config);
            cognitoCredentials.IdentityChangedEvent += this.IdentityChanged;

            _logger = Logger.GetLogger(this.GetType());
        }

        #region dispose methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                cognitoCredentials.IdentityChangedEvent -= this.IdentityChanged;
                _disposed = true;
            }
        }

        #endregion

        /// <summary>
        /// Opens or creates a dataset. If the dataset doesn't exist, an empty one
        /// with the given name will be created. Otherwise, the dataset is loaded from
        /// local storage. If a dataset is marked as deleted but hasn't been deleted
        /// on remote via <see cref="Amazon.CognitoSync.SyncManager.CognitoSyncManager.RefreshDatasetMetadataAsync"/>, it will throw
        /// <see cref="System.InvalidOperationException"/>.
        /// </summary>
        /// <returns>Dataset loaded from local storage</returns>
        /// <param name="datasetName">DatasetName dataset name, must be [a-zA-Z0=9_.:-]+</param>
        public virtual Dataset OpenOrCreateDataset(string datasetName)
        {
            DatasetUtils.ValidateDatasetName(datasetName);
            Local.CreateDataset(GetIdentityId(), datasetName);
            return new Dataset(datasetName, cognitoCredentials, Local, remote);
        }

        /// <summary>
        /// Retrieves a list of datasets from local storage. It may not reflects
        /// latest dataset on the remote storage until <see cref="CognitoSyncManager#RefreshDatasetMetadataAsync"/> is
        /// called.
        /// </summary>
        /// <returns>List of datasets</returns>
        public List<DatasetMetadata> ListDatasets()
        {
            return Local.GetDatasetMetadata(GetIdentityId());
        }

        /// <summary>
        /// Refreshes dataset metadata. Dataset metadata is pulled from remote
        /// storage and stored in local storage. Their record data isn't pulled down
        /// until you sync each dataset.
        /// </summary>
        /// <param name="callback">Callback once the refresh is complete</param>
        /// <param name="options">Options for asynchronous execution</param>
        /// <exception cref="Amazon.CognitoSync.SyncManager.DataStorageException">Thrown when fail to fresh dataset metadata</exception>
        public void RefreshDatasetMetadataAsync(AmazonCognitoSyncCallback<List<DatasetMetadata>> callback, AsyncOptions options = null)
        {
            options = options ?? new AsyncOptions();

            remote.GetDatasetMetadataAsync((cognitoResult) =>
            {
                Exception ex = cognitoResult.Exception;
                List<DatasetMetadata> res = cognitoResult.Response;
                if (ex != null)
                {
                    InternalSDKUtils.AsyncExecutor(() => callback(cognitoResult), options);
                }
                else
                {
                    Local.UpdateDatasetMetadata(GetIdentityId(), res);
                    InternalSDKUtils.AsyncExecutor(() => callback(cognitoResult), options);
                }
            }, options);
        }

        /// <summary>
        /// Wipes all user data cached locally, including identity id, session
        /// credentials, dataset metadata, and all records. Any data that hasn't been
        /// synced will be lost. This method is usually used when customer logs out.
        /// </summary>
        public void WipeData()
        {
            Local.WipeData();
            _logger.InfoFormat("All data has been wiped");
        }

        protected void IdentityChanged(object sender, EventArgs e)
        {
            Amazon.CognitoIdentity.CognitoAWSCredentials.IdentityChangedArgs identityChangedEvent = e as Amazon.CognitoIdentity.CognitoAWSCredentials.IdentityChangedArgs;
            if (identityChangedEvent.NewIdentityId != null)
            {
                String oldIdentity = identityChangedEvent.OldIdentityId == null ? DatasetUtils.UNKNOWN_IDENTITY_ID : identityChangedEvent.OldIdentityId;
                String newIdentity = identityChangedEvent.NewIdentityId;
                _logger.InfoFormat("Identity changed from {0} to {1}", oldIdentity, newIdentity);
                Local.ChangeIdentityId(oldIdentity, newIdentity);
            }
        }

        protected string GetIdentityId()
        {
            return DatasetUtils.GetIdentityId(cognitoCredentials);
        }
    }
}
