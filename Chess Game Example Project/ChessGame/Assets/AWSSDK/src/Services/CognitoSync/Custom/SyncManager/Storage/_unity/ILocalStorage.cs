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
using System.Collections;
using System.Collections.Generic;

namespace Amazon.CognitoSync.SyncManager
{
    public interface ILocalStorage
    {
        /// <summary>
        /// Create a dataset 
        /// </summary>
        /// <param name="identityId">Identity Id</param>
        /// <param name="datasetName">Dataset name.</param>
        void CreateDataset(string identityId, string datasetName);

        /// <summary>
        /// Retrieves the string value of a key in dataset. The value can be null
        /// when the record doesn't exist or is marked as deleted.
        /// </summary>
        /// <returns>string value of the record, or null if not present or deleted.</returns>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="key">record key.</param>
        string GetValue(string identityId, string datasetName, string key);

        /// <summary>
        /// Puts the value of a key in dataset. If a new value is assigned to the
        /// key, the record is marked as dirty. If the value is null, then the record
        /// is marked as deleted. The changed record will be synced with remote
        /// storage.
        /// </summary>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="key">record key.</param>
        /// <param name="value">string value. If null, the record is marked as deleted.</param>
        void PutValue(string identityId, string datasetName, string key, string value);

        /// <summary>
        /// Retrieves a key-value map from dataset, excluding marked as deleted
        /// values.
        /// </summary>
        /// <returns>a key-value map of all but deleted values.</returns>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        Dictionary<string, string> GetValueMap(string identityId, string datasetName);

        /// <summary>
        /// Puts a key-value map into a dataset. This is optimized for batch
        /// operation. It's the preferred way to put a list of records into dataset.
        /// </summary>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="values">a key-value map.</param>
        void PutAllValues(string identityId, string datasetName, IDictionary<string, string> values);

        /// <summary>
        /// Gets a raw record from local store. If the dataset/key combo doesn't
        /// // exist, null will be returned.
        /// </summary>
        /// <returns>a Record object if found, null otherwise.</returns>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="key">Key for the record.</param>
        Record GetRecord(string identityId, string datasetName, string key);

        /// <summary>
        /// Gets a list of all records.
        /// </summary>
        /// <returns>A list of records which have been updated since lastSyncCount.</returns>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        List<Record> GetRecords(string identityId, string datasetName);

        /// <summary>
        /// GRetrieves a list of locally modified records since last successful sync
        /// operation.
        /// </summary>
        /// <returns>a list of locally modified records</returns>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        List<Record> GetModifiedRecords(string identityId, string datasetName);

        /// <summary>
        /// Puts a list of raw records into dataset.
        /// </summary>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="records">A list of Records.</param>
        void PutRecords(string identityId, string datasetName, List<Record> records);

        /// <summary>
        /// Puts a list of raw records into thet dataset if 
        /// the local version hasn't changed (to be used in 
        /// synchronizations). 
        /// </summary> 
        /// <param name="identityId">Identity id.</param>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="localRecords">A list of records to check for changes.</param>
        void ConditionallyPutRecords(String identityId, String datasetName,
            List<Record> records, List<Record> localRecords);

        /// <summary>
        /// Gets a list of dataset's metadata information.
        /// </summary>
        /// <returns>a list of dataset metadata</returns>
        /// <param name="identityId">Identity identifier.</param>
        /// <exception cref="DataStorageException"></exception>
        List<DatasetMetadata> GetDatasetMetadata(string identityId);

        /// <summary>
        /// Deletes a dataset. It clears all records in this dataset and marked it as
        /// deleted for future sync. It's still visible in {@link #getDatasets()}
        /// </summary>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        /// <exception cref="DatasetNotFoundException"></exception>
        void DeleteDataset(string identityId, string datasetName);

        /// <summary>
        /// This is different from <see cref="Amazon.CognitoSync.SyncManager.DeleteDataset(String)"/>. Not only does it
        /// clears all records in the dataset, it also remove it from metadata table.
        /// It won't be visible in <see cref="Amazon.CognitoSync.SyncManager.GetDatasets()"/>.
        /// </summary>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        void PurgeDataset(string identityId, string datasetName);

        /// <summary>
        /// Retrieves the metadata of a dataset.
        /// </summary>
        /// <returns>The dataset metadata.</returns>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        /// <exception cref="DataStorageException"></exception>
        DatasetMetadata GetDatasetMetadata(string identityId, string datasetName);

        /// <summary>
        /// Retrieves the last sync count. This sync count is a counter that
        /// represents when the last sync happened. The counter should be updated on
        /// a successful sync.
        /// </summary>
        /// <returns>The last sync count.</returns>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        long GetLastSyncCount(string identityId, string datasetName);

        /// <summary>
        /// Updates the last sync count after successful sync with the remote data
        /// store.
        /// </summary>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="lastSyncCount">Last sync count.</param>
        void UpdateLastSyncCount(string identityId, string datasetName, long lastSyncCount);

        /// <summary>
        /// Wipes all locally cached data including dataset metadata and records. All
        /// opened dataset handler should not perform further operations to avoid
        /// inconsistent state.
        /// </summary>
        void WipeData();

        /// <summary>
        /// Reparents all datasets from old identity id to a new one.
        /// </summary>
        /// <param name="oldIdentityId">Old identity identifier.</param>
        /// <param name="newIdentityId">New identity identifier.</param>
        void ChangeIdentityId(string oldIdentityId, string newIdentityId);

        /// <summary>
        /// Updates local dataset metadata
        /// </summary>
        /// <param name="identityId">Identity identifier.</param>
        /// <param name="datasetMetadata">Dataset metadata.</param>
        void UpdateDatasetMetadata(string identityId, List<DatasetMetadata> datasetMetadata);
    }
}

