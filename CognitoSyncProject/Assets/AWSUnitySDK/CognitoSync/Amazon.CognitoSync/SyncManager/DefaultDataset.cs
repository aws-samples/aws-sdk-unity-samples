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
using System.Threading;

using UnityEngine;

using Amazon.CognitoIdentity;
using Amazon.CognitoSync.SyncManager.Storage;
using Amazon.CognitoSync.SyncManager.Storage.Model;
using Amazon.CognitoSync.SyncManager.Util;
using Amazon.CognitoSync.SyncManager.Exceptions;
using Amazon.Runtime;
using Amazon.Common;


namespace Amazon.CognitoSync.SyncManager
{
    /// <summary>
    /// Default implementation of {@link Dataset}. It uses {@link CognitoSyncStorage}
    /// as remote storage and {@link SQLiteLocalStorage} as local storage.
    /// </summary>
    public class DefaultDataset : Dataset
    {
        /// <summary>
        /// Max number of retries during synchronize before it gives up.
        /// </summary>
        private static readonly int MAX_RETRY = 3;
        private readonly string _datasetName;
        private readonly LocalStorage _local;
        private readonly RemoteDataStorage _remote;
        private readonly CognitoAWSCredentials _cognitoCredentials;

        public override DatasetMetadata Metadata
        {
            get
            {
                return _local.GetDatasetMetadata(GetIdentityId(), _datasetName);
            }
        }

        public DefaultDataset(string datasetName, CognitoAWSCredentials cognitoCredentials, LocalStorage local, RemoteDataStorage remote)
        {
            this._datasetName = datasetName;
            this._cognitoCredentials = cognitoCredentials;
            this._local = local;
            this._remote = remote;
        }

        public override void Delete()
        {
            _local.DeleteDataset(GetIdentityId(), _datasetName);
        }

        public override string Get(string key)
        {
            return _local.GetValue(GetIdentityId(), _datasetName,
                                  DatasetUtils.ValidateRecordKey(key));
        }

        public override Dictionary<string, string> GetAll()
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            foreach (Record record in _local.GetRecords(GetIdentityId(), _datasetName))
            {
                if (!record.IsDeleted())
                {
                    map.Add(record.Key, record.Value);
                }
            }
            return map;
        }

        public override List<Record> GetAllRecords()
        {
            return _local.GetRecords(GetIdentityId(), _datasetName);
        }

        public override long GetSizeInBytes(string key)
        {
            return DatasetUtils.ComputeRecordSize(_local.GetRecord(GetIdentityId(),
                _datasetName, DatasetUtils.ValidateRecordKey(key)));
        }

        public override long GetTotalSizeInBytes()
        {
            long size = 0;
            foreach (Record record in _local.GetRecords(GetIdentityId(), _datasetName))
            {
                size += DatasetUtils.ComputeRecordSize(record);
            }
            return size;
        }

        public override bool IsChanged(string key)
        {
            Record record = _local.GetRecord(GetIdentityId(), _datasetName,
                                             DatasetUtils.ValidateRecordKey(key));
            return (record != null && record.Modified);
        }

        public override void Put(string key, string value)
        {
            _local.PutValue(GetIdentityId(), _datasetName,
                           DatasetUtils.ValidateRecordKey(key), value);
        }

        public override void PutAll(System.Collections.Generic.Dictionary<string, string> values)
        {
            foreach (string key in values.Keys)
            {
                DatasetUtils.ValidateRecordKey(key);
            }
            _local.PutAllValues(GetIdentityId(), _datasetName, values);
        }

        public override void Remove(string key)
        {
            _local.PutValue(GetIdentityId(), _datasetName,
                            DatasetUtils.ValidateRecordKey(key), null);
        }

        public override void Resolve(List<Record> remoteRecords)
        {
            _local.PutRecords(GetIdentityId(), _datasetName, remoteRecords);
        }

        public override void Synchronize()
        {
            ThreadPool.QueueUserWorkItem(delegate(object notUsed)
            {
                if (!IsNetworkAvailable())
                {
                    FireSyncFailureEvent(new NetworkException("Network connectivity unavailable."));

                    return;
                }
                List<string> mergedDatasets = GetLocalMergedDatasets();
                if (mergedDatasets.Count > 0)
                {
                    AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "DefaultDataset", "detected merge datasets " + _datasetName);

                    if (this.OnDatasetMerged != null)
                    {
                        this.OnDatasetMerged(this, mergedDatasets);
                    }
                }

                if (_cognitoCredentials.IdentityProvider.GetCurrentIdentityId() != null)
                    SynchronizeInternalAsync();
                else
                {
                    _cognitoCredentials.IdentityProvider.RefreshAsync(delegate(AmazonServiceResult voidResult)
                    {
                        if (voidResult.Exception != null)
                        {
                            FireSyncFailureEvent(voidResult.Exception);
                            return;
                        }
                        SynchronizeInternalAsync();
                    }, null);
                }
            });
        }

        public override void SynchronizeOnConnectivity()
        {
            throw new NotImplementedException();
        }

        public void SynchronizeInternalAsync()
        {
            try
            {
                List<string> mergedDatasets = GetLocalMergedDatasets();
                if (mergedDatasets.Count != 0)
                {
                    this.OnDatasetMerged(this, mergedDatasets);
                }

                this.RunSyncOperationAsync(MAX_RETRY, delegate(RunSyncOperationResponse response)
                {
                    if (response == null || response.Exception != null)
                        FireSyncFailureEvent(new DataStorageException("Unknown exception", response.Exception));
                });

            }
            catch (Exception e)
            {
                FireSyncFailureEvent(new DataStorageException("Unknown exception", e));
                AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Errors, "CognitoSyncManager", "failed to synchronize " + e.Message);
            }


        }

        private List<string> GetLocalMergedDatasets()
        {
            List<string> mergedDatasets = new List<string>();
            string prefix = _datasetName + ".";
            foreach (DatasetMetadata dataset in _local.GetDatasets(GetIdentityId()))
            {
                if (dataset.DatasetName.StartsWith(prefix))
                {
                    mergedDatasets.Add(dataset.DatasetName);
                }
            }
            return mergedDatasets;
        }

        class RunSyncOperationResponse
        {
            private bool _status;

            public Exception Exception { get; set; }

            public bool Status
            {
                get { return this._status; }
            }

            public RunSyncOperationResponse(bool status, Exception exception)
            {
                this._status = status;
                this.Exception = exception;
            }
        }

        private void RunSyncOperationAsync(int retry, Action<RunSyncOperationResponse> callback)
        {
            if (retry < 0)
            {
                callback(new RunSyncOperationResponse(false, null));
                return;
            }

            long lastSyncCount = _local.GetLastSyncCount(GetIdentityId(), _datasetName);

            // if dataset is deleted locally, push it to remote
            if (lastSyncCount == -1)
            {
#if DELETE_METHOD_SUPPORT
                _remote.DeleteDatasetAsync(_datasetName, delegate(AmazonCognitoResult result)
                {
                    if (result.Exception != null)
                    {
                        var e = result.Exception as DataStorageException;
                        AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Errors, "CognitoSyncManager", "OnSyncFailure" + e.Message);
                        this.FireSyncFailureEvent(e);
                        callback(new RunSyncOperationResponse(false, null));
                        return;
                    }

                    _local.PurgeDataset(GetIdentityId(), _datasetName);
                    AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", "OnSyncSuccess: dataset delete is pushed to remote");
                    this.FireSyncSuccessEvent(new List<Record>());
                    callback(new RunSyncOperationResponse(true, null));
                    return;
                }, null);
#endif
                // invalid scenario 
                AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Critical, "CognitoSyncManager", "OnSyncFailure: DeleteDataset is an invalid operation");
                FireSyncFailureEvent(new DataStorageException("DeleteDataset is an invalid operation"));
                callback(new RunSyncOperationResponse(false, null));
                return;
            }

            // get latest modified records from remote
            AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", "get latest modified records since " + lastSyncCount);

            _remote.ListUpdatesAsync(_datasetName, lastSyncCount, delegate(AmazonCognitoResult listUpdatesResult)
            {
                RemoteDataStorage.DatasetUpdates datasetUpdates = null;
                if (listUpdatesResult == null || listUpdatesResult.Exception != null)
                {
                    var e = listUpdatesResult.Exception as DataStorageException;
                    AmazonLogging.LogException(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", e);
                    FireSyncFailureEvent(e);
                    callback(new RunSyncOperationResponse(false, listUpdatesResult.Exception));
                    return;
                }

                ListUpdatesResponse listUpdatesResponse = listUpdatesResult.Response as ListUpdatesResponse;
                datasetUpdates = listUpdatesResponse.DatasetUpdates;


                if (datasetUpdates.MergedDatasetNameList.Count != 0 && this.OnDatasetMerged != null)
                {
                    bool resume = this.OnDatasetMerged(this, datasetUpdates.MergedDatasetNameList);
                    if (resume)
                    {
                        this.RunSyncOperationAsync(--retry, callback);
                        return;
                    }
                    else
                    {
                        AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", "OnSyncFailure: Manual cancel");
                        FireSyncFailureEvent(new DataStorageException("Manual cancel"));
                        callback(new RunSyncOperationResponse(false, null));
                        return;
                    }
                }


                // if the dataset doesn't exist or is deleted, trigger onDelete
                if (lastSyncCount != 0 && !datasetUpdates.Exists
                    || datasetUpdates.Deleted && this.OnDatasetDeleted != null)
                {
                    bool resume = this.OnDatasetDeleted(this);
                    if (resume)
                    {
                        // remove both records and metadata
                        _local.DeleteDataset(GetIdentityId(), _datasetName);
                        _local.PurgeDataset(GetIdentityId(), _datasetName);
                        AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", "OnSyncSuccess");
                        FireSyncSuccessEvent(new List<Record>());
                        callback(new RunSyncOperationResponse(true, null));
                        return;
                    }
                    else
                    {
                        AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", "OnSyncFailure");
                        FireSyncFailureEvent(new DataStorageException("Manual cancel"));
                        callback(new RunSyncOperationResponse(false, null));
                        return;
                    }
                }

               
                List<Record> remoteRecords = datasetUpdates.Records;
                if (remoteRecords.Count != 0)
                {
                    // if conflict, prompt developer/user with callback
                    List<SyncConflict> conflicts = new List<SyncConflict>();
                    List<Record> conflictRecords = new List<Record>();
                    foreach (Record remoteRecord in remoteRecords)
                    {
                        Record localRecord = _local.GetRecord(GetIdentityId(),
                                                              _datasetName,
                                                              remoteRecord.Key);
                        // only when local is changed and its value is different
                        if (localRecord != null && localRecord.Modified
                            && !StringUtils.Equals(localRecord.Value, remoteRecord.Value))
                        {
                            conflicts.Add(new SyncConflict(remoteRecord, localRecord));
                            conflictRecords.Add(remoteRecord);
                        }
                    }
                    // retaining only non-conflict records
                    remoteRecords.RemoveAll(t => conflictRecords.Contains(t));

                    if (conflicts.Count > 0)
                    {
                        AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", String.Format("{0} records in conflict!", conflicts.Count));

                        bool syncConflictResult = false;
                        if (this.OnSyncConflict == null)
                        {
                            // delegate is not implemented so the conflict resolution is applied
                            syncConflictResult = this.DefaultConflictResolution(conflicts);
                        }
                        else
                        {
                            syncConflictResult = this.OnSyncConflict(this, conflicts);
                        }
                        if (!syncConflictResult)
                        {
                            AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", "User cancelled conflict resolution");
                            callback(new RunSyncOperationResponse(false, null));
                            return;
                        }
                    }

                    // save to local
                    if (remoteRecords.Count > 0)
                    {
                        AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", String.Format("save {0} records to local", remoteRecords.Count));
                        _local.PutRecords(GetIdentityId(), _datasetName, remoteRecords);
                    }


                    // new last sync count
                    AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", String.Format("updated sync count {0}", datasetUpdates.SyncCount));
                    _local.UpdateLastSyncCount(GetIdentityId(), _datasetName,
                                              datasetUpdates.SyncCount);
                }


                // push changes to remote
                List<Record> localChanges = this.GetModifiedRecords();
                if (localChanges.Count != 0)
                {
                    AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", String.Format("push {0} records to remote", localChanges.Count));

                    _remote.PutRecordsAsync(_datasetName, localChanges,
                                                   datasetUpdates.SyncSessionToken, delegate(AmazonCognitoResult putRecordsResult)
                    {
                        if (putRecordsResult.Exception != null)
                        {
                            if (putRecordsResult.Exception.GetType() == typeof(DataConflictException))
                            {
                                AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Warnings, "CognitoSyncManager", "Conflicts detected when pushing changes to remote: " + putRecordsResult.Exception.Message);
                                this.RunSyncOperationAsync(--retry, callback);
                                return;
                            }
                            else if (putRecordsResult.Exception.GetType() == typeof(DataStorageException))
                            {
                                AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", "OnSyncFailure" + putRecordsResult.Exception.Message);
                                FireSyncFailureEvent(putRecordsResult.Exception);
                                callback(new RunSyncOperationResponse(false, null));
                                return;
                            }
                        }
                        PutRecordsResponse putRecordsResponse = putRecordsResult.Response as PutRecordsResponse;
                        List<Record> result = putRecordsResponse.UpdatedRecords;

                        // update local meta data
                        _local.PutRecords(GetIdentityId(), _datasetName, result);

                        // verify the server sync count is increased exactly by one, aka no
                        // other updates were made during this update.
                        long newSyncCount = 0;
                        foreach (Record record in result)
                        {
                            newSyncCount = newSyncCount < record.SyncCount
                                ? record.SyncCount
                                    : newSyncCount;
                        }
                        if (newSyncCount == lastSyncCount + 1)
                        {
                            AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Info, "DefaultDataset",
                                              String.Format("updated sync count %d", newSyncCount));
                            _local.UpdateLastSyncCount(GetIdentityId(), _datasetName,
                                                      newSyncCount);
                        }

                        AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", "OnSyncSuccess");
                        // call back
                        FireSyncSuccessEvent(remoteRecords);
                        callback(new RunSyncOperationResponse(true, null));
                        return;
                    }, null);
                    return;
                }


                AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CognitoSyncManager", "OnSyncSuccess");
                // call back
                FireSyncSuccessEvent(remoteRecords);
                callback(new RunSyncOperationResponse(true, null));
                return;
            }, null);
        }

        private String GetIdentityId()
        {
            return DatasetUtils.GetIdentityId(_cognitoCredentials);
        }

        private List<Record> GetModifiedRecords()
        {
            return _local.GetModifiedRecords(GetIdentityId(), _datasetName);
        }

        // TODO check internet connectivity
        private bool IsNetworkAvailable()
        {
            // Start/Poll the connection test, report the results in a label and 
            // react to the results accordingly
            //var connectionTestResult = Network.TestConnection();

            //Debug.Log(connectionTestResult);
            return true;

            /*bool connectionAvailable = false;
            ConnectionTesterStatus connectionTestResult = Network.TestConnection ();
            switch (connectionTestResult) {
            case ConnectionTesterStatus.Error: 
                connectionAvailable = false;
                break;
				
            case ConnectionTesterStatus.Undetermined: 
                connectionAvailable = false;
                break;
				
            case ConnectionTesterStatus.PublicIPIsConnectable:
                connectionAvailable = true;
                break;
            case ConnectionTesterStatus.PublicIPPortBlocked:
                connectionAvailable = true;
                break;
            case ConnectionTesterStatus.PublicIPNoServerStarted:
                connectionAvailable = true;
                break;
            case ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted:
                connectionAvailable = true;
                break;
            case ConnectionTesterStatus.LimitedNATPunchthroughSymmetric:
                connectionAvailable = true;
                break;
            case ConnectionTesterStatus.NATpunchthroughAddressRestrictedCone:
            case ConnectionTesterStatus.NATpunchthroughFullCone:
                connectionAvailable = true;
                break;
            default: 
                connectionAvailable = false;
                break;
            }

            return connectionAvailable;*/
        }
    }
}

