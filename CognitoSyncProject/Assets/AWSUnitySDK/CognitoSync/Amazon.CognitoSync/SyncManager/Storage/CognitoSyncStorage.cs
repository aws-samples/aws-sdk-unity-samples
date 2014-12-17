#pragma warning disable 0649
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
using System.Collections;
using System.IO;

using UnityEngine;
using Amazon.Runtime;
using Amazon.Unity;
using Amazon.CognitoSync.SyncManager;
using Amazon.CognitoIdentity;
using Amazon.CognitoSync.SyncManager.Exceptions;
using Amazon.CognitoSync;
using Amazon.CognitoSync.Model;
using Amazon.CognitoSync.SyncManager.Storage.Model;

namespace Amazon.CognitoSync.SyncManager.Storage
{
    public class CognitoSyncStorage : RemoteDataStorage
    {
        private readonly string identityPoolId;
        private readonly AmazonCognitoSyncClient client;
        private readonly CognitoAWSCredentials cognitoCredentials;

        public CognitoSyncStorage(CognitoAWSCredentials cognitoCredentials, AmazonCognitoSyncConfig config)
        {
            if (cognitoCredentials == null)
            {
                throw new ArgumentNullException("cognitoCredentials is null");
            }
            this.identityPoolId = cognitoCredentials.IdentityProvider.IdentityPoolId;
            this.cognitoCredentials = cognitoCredentials;
            this.client = new AmazonCognitoSyncClient(cognitoCredentials, config);
        }

        /// <summary>
        /// Gets a list of {@link DatasetMetadata}s.
        /// </summary>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. 
        ///  Retrieve this object from within the callback
        ///  procedure using the AsyncState property.</param>
        /// <exception cref="DataStorageException"></exception>
        public override void GetDatasetsAsync(AmazonCognitoCallback callback, object state)
        {
            PopulateGetDatasets(null, new List<DatasetMetadata>(), callback, state);
        }


        private void PopulateGetDatasets(string nextToken, List<DatasetMetadata> datasets, AmazonCognitoCallback callback, object state)
        {
            ListDatasetsRequest request = new ListDatasetsRequest();
            //appendUserAgent(request, userAgent);
            request.IdentityPoolId = identityPoolId;
            request.IdentityId = this.GetCurrentIdentityId();
            // a large enough number to reduce # of requests
            request.MaxResults = 64;
            request.NextToken = nextToken;

            client.ListDatasetsAsync(request, delegate(AmazonServiceResult result)
            {

                if (result.Exception != null)
                {
                    AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonCognitoResult(null,
                                   HandleException(result.Exception, "Failed to list dataset metadata"), state));
                    return;
                }

                ListDatasetsResponse response = result.Response as ListDatasetsResponse;
                foreach (Amazon.CognitoSync.Model.Dataset dataset in response.Datasets)
                {
                    datasets.Add(ModelToDatasetMetadata(dataset));
                }

                nextToken = response.NextToken;

                if (nextToken == null)
                {
                    GetDatasetsResponse getDatasetsResponse = new GetDatasetsResponse
                    {
                        Datasets = datasets
                    };
					AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonCognitoResult(getDatasetsResponse, null, state));
                    return;
                }
                PopulateGetDatasets(nextToken, datasets, callback, state);

            }, null);
        }

        public override void ListUpdatesAsync(string datasetName, long lastSyncCount, AmazonCognitoCallback callback, object state)
        {

            PopulateListUpdates(datasetName, lastSyncCount, new List<Record>(), null, callback, state);
        }


        private void PopulateListUpdates(string datasetName, long lastSyncCount, List<Record> records, string nextToken, AmazonCognitoCallback callback, object state)
        {
            {
                ListRecordsRequest request = new ListRecordsRequest();
                //appendUserAgent(request, userAgent);
                request.IdentityPoolId = identityPoolId;
                request.IdentityId = this.GetCurrentIdentityId();
                request.DatasetName = datasetName;
                request.LastSyncCount = lastSyncCount;
                // mark it large enough to reduce # of requests
                request.MaxResults = 1024;
                request.NextToken = nextToken;

                client.ListRecordsAsync(request, delegate(AmazonServiceResult result)
                {

                    if (result.Exception != null)
                    {
						AmazonMainThreadDispatcher.ExecCallback(callback,
                                     new AmazonCognitoResult(null, HandleException(result.Exception, "Failed to list records in dataset: " + datasetName), state));

                        return;
                    }

                    ListRecordsResponse listRecordsResponse = result.Response as ListRecordsResponse;
                    foreach (Amazon.CognitoSync.Model.Record remoteRecord in listRecordsResponse.Records)
                    {
                        //builder.addRecord(modelToRecord(remoteRecord));
                        records.Add(this.ModelToRecord(remoteRecord));
                    }
                    if (listRecordsResponse.NextToken == null)
                    {
                        DatasetUpdatesImpl updates = new DatasetUpdatesImpl(
                            datasetName,
                            records,
                            listRecordsResponse.DatasetSyncCount,
                            listRecordsResponse.SyncSessionToken,
                            listRecordsResponse.DatasetExists,
                            listRecordsResponse.DatasetDeletedAfterRequestedSyncCount,
                            listRecordsResponse.MergedDatasetNames
                            );
                        ListUpdatesResponse listUpdatesResponse = new ListUpdatesResponse
                        {
                            DatasetUpdates = updates
                        };
						AmazonMainThreadDispatcher.ExecCallback(callback,
                                                                             new AmazonCognitoResult(listUpdatesResponse, null, state));
                        return;
                    }
                    // update last evaluated key
                    nextToken = listRecordsResponse.NextToken;

                    // emulating the while loop
                    PopulateListUpdates(datasetName, lastSyncCount, records, nextToken, callback, state);
                }, state);

            }

        }

        public override void PutRecordsAsync(string datasetName, List<Record> records, string syncSessionToken, AmazonCognitoCallback callback, object state)
        {
            UpdateRecordsRequest request = new UpdateRecordsRequest();
            //appendUserAgent(request, userAgent);
            request.DatasetName = datasetName;
            request.IdentityPoolId = identityPoolId;
            request.IdentityId = this.GetCurrentIdentityId();
            request.SyncSessionToken = syncSessionToken;

            // create patches
            List<RecordPatch> patches = new List<RecordPatch>();
            foreach (Record record in records)
            {
                patches.Add(this.RecordToPatch(record));
            }
            request.RecordPatches = patches;

            List<Record> updatedRecords = new List<Record>();
            client.UpdateRecordsAsync(request, delegate(AmazonServiceResult result)
            {
                AmazonCognitoResult callbackResult = new AmazonCognitoResult(state);

                if (result.Exception != null)
                {
                    callbackResult.Exception = HandleException(result.Exception, "Failed to update records in dataset: " + datasetName);
                }
                else
                {
                    UpdateRecordsResponse updateRecordsResponse = result.Response as UpdateRecordsResponse;
                    foreach (Amazon.CognitoSync.Model.Record remoteRecord in updateRecordsResponse.Records)
                    {
                        updatedRecords.Add(ModelToRecord(remoteRecord));
                    }
                    callbackResult.Response = new PutRecordsResponse { UpdatedRecords = updatedRecords };
                }

				AmazonMainThreadDispatcher.ExecCallback(callback, callbackResult);
            }, null);


        }
#if DELETE_METHOD_SUPPORT
        public override void DeleteDatasetAsync(string datasetName, AmazonCognitoCallback callback, object state)
        {
            DeleteDatasetRequest request = new DeleteDatasetRequest();
            //appendUserAgent(request, userAgent);
            request.IdentityPoolId = identityPoolId;
            request.IdentityId = this.GetCurrentIdentityId();
            request.DatasetName = datasetName;
            client.DeleteDatasetAsync(request, delegate(AmazonServiceResult deleteDatasetResult)
            {
                AmazonCognitoResult result = new AmazonCognitoResult(state);
                if (deleteDatasetResult.Exception == null)
                {
                    result.Exception = HandleException(deleteDatasetResult.Exception, "Failed to delete dataset: " + datasetName);
                }
                else
                {
                    result.Exception = new Exception("Unsupported DeleteDatasetAsync");
                    //result.Response = deleteDatasetResult.Response;
                }
                AmazonMainThreadDispatcher.ExecCallback(callback, result);
            }, null);
        }
#endif
        public override void GetDatasetMetadataAsync(string datasetName, AmazonCognitoCallback callback, object state)
        {
            DescribeDatasetRequest request = new DescribeDatasetRequest();
            //appendUserAgent(request, userAgent);
            request.IdentityPoolId = identityPoolId;
            request.IdentityId = this.GetCurrentIdentityId();
            request.DatasetName = datasetName;
            client.DescribeDatasetAsync(request, delegate(AmazonServiceResult describeDatasetResult)
            {

                AmazonCognitoResult callbackResult = new AmazonCognitoResult(state);
                if (describeDatasetResult.Exception != null)
                {
                    callbackResult.Exception = new DataStorageException("Failed to get metadata of dataset: " 
                                                                        + datasetName, describeDatasetResult.Exception);
                }
                else
                {
                    callbackResult.Response = new DatasetMetadataResponse
                    {
                        Metadata = ModelToDatasetMetadata((describeDatasetResult.Response as DescribeDatasetResponse).Dataset)
                    };

                }
				AmazonMainThreadDispatcher.ExecCallback(callback, callbackResult);
            }, null);

        }

        private string GetCurrentIdentityId()
        {
            // identity id may change after provider.refresh()
            //provider.Refresh();
            //return cognitoCredentials.GetIdentityId ();
            return cognitoCredentials.IdentityProvider.GetCurrentIdentityId();
        }

        private RecordPatch RecordToPatch(Record record)
        {
            RecordPatch patch = new RecordPatch();
            patch.Key = record.Key;
            patch.Value = record.Value;
            patch.SyncCount = record.SyncCount;
            patch.Op = (record.Value == null ? Operation.Remove : Operation.Replace);
            return patch;
        }

        private DatasetMetadata ModelToDatasetMetadata(Amazon.CognitoSync.Model.Dataset model)
        {
            return new DatasetMetadata(
                model.DatasetName,
                model.CreationDate,
                model.LastModifiedDate,
                model.LastModifiedBy,
                model.DataStorage,
                model.NumRecords
                );
        }

        private Record ModelToRecord(Amazon.CognitoSync.Model.Record model)
        {
            return new Record(
                model.Key,
                model.Value,
                model.SyncCount,
                model.LastModifiedDate,
                model.LastModifiedBy,
                model.DeviceLastModifiedDate,
                false);
        }

        private DataStorageException HandleException(Exception e, string message)
        {
            var ase = e as AmazonServiceException;

            if (ase == null)    ase = new AmazonServiceException(e);

            if (ase.GetType() == typeof(ResourceNotFoundException))
            {
                return new DatasetNotFoundException(message);
            }
            else if (ase.GetType() == typeof(ResourceConflictException) 
                     || ase.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return new DataConflictException(message);
            }
            else if (ase.GetType() == typeof(LimitExceededException))
            {
                return new DataLimitExceededException(message);
            }
            else if (this.IsNetworkException(ase))
            {
                return new NetworkException(message);
            }
            else
            {
                return new DataStorageException(message, ase);
            }
        }

        private bool IsNetworkException(AmazonServiceException ase)
        {
            return ase.InnerException != null && ase.InnerException.GetType() == typeof(IOException);
        }

        private class DatasetUpdatesImpl : DatasetUpdates
        {
            private readonly string _datasetName;
            private readonly List<Record> _records;
            private readonly long _syncCount;
            private readonly string _syncSessionToken;
            private readonly bool _exists;
            private readonly bool _deleted;
            private readonly List<string> _mergedDatasetNameList;
            private readonly AmazonServiceException _exception;

            public override string DatasetName
            {
                get
                {
                    return this._datasetName;
                }
            }

            public override bool Deleted
            {
                get
                {
                    return this._deleted;
                }
            }

            public override bool Exists
            {
                get
                {
                    return this._exists;
                }
            }

            public override List<string> MergedDatasetNameList
            {
                get
                {
                    return _mergedDatasetNameList;
                }
            }

            public override List<Record> Records
            {
                get
                {
                    return this._records;
                }
            }

            public override long SyncCount
            {
                get
                {
                    return this._syncCount;
                }
            }

            public override string SyncSessionToken
            {
                get
                {
                    return this._syncSessionToken;
                }
            }

            public override AmazonServiceException Exception
            {
                get { return this._exception; }
            }

            public DatasetUpdatesImpl(string datasetName, List<Record> records, long syncCount, string syncSessionToken, 
                                      bool exists, bool deleted, List<string> mergedDatasetNameList)
            {
                this._datasetName = datasetName;
                this._records = records;
                this._syncCount = syncCount;
                this._syncSessionToken = syncSessionToken;
                this._exists = exists;
                this._deleted = deleted;
                this._mergedDatasetNameList = mergedDatasetNameList;
            }
        }
    }
}

