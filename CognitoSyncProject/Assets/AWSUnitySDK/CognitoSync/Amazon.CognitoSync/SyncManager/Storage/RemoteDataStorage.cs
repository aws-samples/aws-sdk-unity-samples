/**
 * Copyright 2013-2014 Amazon.com, 
 * Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Amazon Software License (the "License"). 
 * You may not use this file except in compliance with the 
 * License. A copy of the License is located at
 * 
 *     http://aws.amazon.com/asl/
 * 
 * or in the "license" file accompanying this file. This file is 
 * distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 * CONDITIONS OF ANY KIND, express or implied. See the License 
 * for the specific language governing permissions and 
 * limitations under the License.
 */
#define DELETE_METHOD_SUPPORT
using System;
using System.Collections.Generic;

using Amazon.CognitoSync.Model;
using Amazon.CognitoSync.SyncManager;
using Amazon.Runtime;
using Amazon.CognitoSync.SyncManager.Storage.Model;

namespace Amazon.CognitoSync.SyncManager.Storage
{
    public abstract class RemoteDataStorage
    {

        /// <summary>
        /// Gets a list of {@link DatasetMetadata}s.
        /// </summary>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. 
        /// 	Retrieve this object from within the callback
        /// 	procedure using the AsyncState property.</param>
        /// <exception cref="DataStorageException"></exception>
        public abstract void GetDatasetsAsync(AmazonCognitoCallback callback, object state);

        /// <summary>
        /// Retrieves the metadata of a dataset.
        /// </summary>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when 
        /// 	the operation completes with AmazonServiceResult result</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. 
        /// 	Retrieve this object from within the callback
        /// 	procedure using the AsyncState property.</param>
        /// <exception cref="DataStorageException"></exception>
        public abstract void GetDatasetMetadataAsync(string datasetName, AmazonCognitoCallback callback, object state);

        /// <summary>
        /// Gets a list of records which have been updated since lastSyncCount
        /// (inclusive). If the value of a record equals null, then the record is
        /// deleted. If you pass 0 as lastSyncCount, the full list of records will be
        /// returned.
        /// </summary>
        /// <returns>A list of records which have been updated since lastSyncCount.</returns>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when 
        /// 	the operation completes with AmazonServiceResult result</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. 
        /// 	Retrieve this object from within the callback
        /// 	procedure using the AsyncState property.</param>
        /// <param name="lastSyncCount">Last sync count.</param>
        /// <exception cref="DataStorageException"></exception>
        public abstract void ListUpdatesAsync(string datasetName, long lastSyncCount, AmazonCognitoCallback callback, object state);

        /// <summary>
        /// Post updates to remote storage. Each record has a sync count. If the sync
        /// count doesn't match what's on the remote storage, i.e. the record is
        /// modified by a different device, this operation throws ConflictException.
        /// Otherwise it returns a list of records that are updated successfully.
        /// </summary>
        /// <returns>The records.</returns>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="records">Records.</param>
        /// <param name="syncSessionToken">Sync session token.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when 
        /// 	the operation completes with AmazonServiceResult result</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. 
        /// 	Retrieve this object from within the callback
        /// 	procedure using the AsyncState property.</param>
        /// <exception cref="DatasetNotFoundException"></exception>
        /// <exception cref="DataConflictException"></exception>
        public abstract void PutRecordsAsync(string datasetName, List<Record> records, string syncSessionToken, AmazonCognitoCallback callback, object state);

#if DELETE_METHOD_SUPPORT
        /// <summary>
        /// Deletes a dataset.
        /// </summary>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when 
        /// 	the operation completes with AmazonServiceResult result</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. 
        /// 	Retrieve this object from within the callback
        /// 	procedure using the AsyncState property.</param>
        /// <exception cref="DatasetNotFoundException"></exception>
        public abstract void DeleteDatasetAsync(string datasetName, AmazonCognitoCallback callback, object state);
#endif
        public abstract class DatasetUpdates
        {
            public abstract bool Exists
            {
                get;
            }

            public abstract bool Deleted
            {
                get;
            }

            public abstract string DatasetName
            {
                get;
            }

            public abstract List<Record> Records
            {
                get;
            }

            public abstract string SyncSessionToken
            {
                get;
            }

            public abstract long SyncCount
            {
                get;
            }

            public abstract List<string> MergedDatasetNameList
            {
                get;
            }

            public abstract AmazonServiceException Exception
            {
                get;
            }
        }
    }
}

