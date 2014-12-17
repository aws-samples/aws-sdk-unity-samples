using Amazon.Runtime;
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
using System;
using System.Collections.Generic;

namespace Amazon.CognitoSync.SyncManager
{
    public abstract class CognitoSyncManager
    {
        /// <summary>
        /// Opens or creates a dataset. If the dataset doesn't exist, an empty one
        /// with the given name will be created. Otherwise, dataset is loaded from
        /// local storage. If a dataset is marked as deleted but hasn't been deleted
        /// on remote via {@link #refreshDatasetMetadata()}, it will throw
        /// {@link IllegalStateException}.
        /// </summary>
        /// <returns>dataset loaded from local storage</returns>
        /// <param name="datasetName">datasetName dataset name, must be [a-zA-Z0=9_.:-]+</param>
        public abstract Dataset OpenOrCreateDataset(string datasetName);

        /// <summary>
        /// Refreshes dataset metadata. Dataset metadata is pulled from remote
        /// storage and stored in local storage. Their record data isn't pulled down
        /// until you sync each dataset. Note: this is a network request, so calling
        /// this method in the main thread will result in
        /// NetworkOnMainThreadException.
        /// </summary>
        /// <exception cref="DataStorageException">thrown when fail to fresh dataset metadata</exception>
        public abstract void RefreshDatasetMetadataAsync(AmazonCognitoCallback callback, object state);

        /// <summary>
        /// Retrieves a list of datasets from local storage. It may not reflects
        /// latest dataset on the remote storage until refreshDatasetMetadata is
        /// called.
        /// </summary>
        /// <returns>list of datasets</returns>
        public abstract List<DatasetMetadata> ListDatasets();

        /// <summary>
        /// Wipes all user data cached locally, including identity id, session
        /// credentials, dataset metadata, and all records. Any data that hasn't been
        /// synced will be lost. This method is usually used when customer logs out.
        /// </summary>
        public abstract void WipeData();
    }
}

