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

using System;
using System.Collections.Generic;
using System.IO;

using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon.CognitoSync.Model;
using Amazon.Util.Internal;

namespace Amazon.CognitoSync.SyncManager.Internal
{
    public class CognitoSyncStorage : IRemoteDataStorage
    {
        private readonly string identityPoolId;
        private readonly AmazonCognitoSyncClient client;
        private readonly CognitoAWSCredentials cognitoCredentials;

        #region constructor
        public CognitoSyncStorage(CognitoAWSCredentials cognitoCredentials, AmazonCognitoSyncConfig config)
        {
            if (cognitoCredentials == null)
            {
                throw new ArgumentNullException("cognitoCredentials");
            }
            this.identityPoolId = cognitoCredentials.IdentityPoolId;
            this.cognitoCredentials = cognitoCredentials;
            this.client = new AmazonCognitoSyncClient(cognitoCredentials, config);
        }

        #endregion


        #region GetDataset


        /// <summary>
        /// Gets a list of <see cref="DatasetMetadata"/>
        /// </summary>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. </param>
        /// <exception cref="DataStorageException"></exception>
        public void GetDatasetMetadataAsync(AmazonCognitoSyncCallback<List<DatasetMetadata>> callback, AsyncOptions options = null)
        {
            options = options ?? new AsyncOptions();
            PopulateGetDatasetMetadataAsync(null, new List<DatasetMetadata>(), callback, options);
        }

        private void PopulateGetDatasetMetadataAsync(string nextToken, List<DatasetMetadata> datasets, AmazonCognitoSyncCallback<List<DatasetMetadata>> callback, AsyncOptions options)
        {

            ListDatasetsRequest request = new ListDatasetsRequest();
            // a large enough number to reduce # of requests
            request.MaxResults = 64;
            request.NextToken = nextToken;

            client.ListDatasetsAsync(request, (responseObj) =>
            {
                Exception ex = responseObj.Exception;
                ListDatasetsResponse response = responseObj.Response;
                object obj = responseObj.state;
                if (ex != null)
                {
                    InternalSDKUtils.AsyncExecutor(() => callback(new AmazonCognitoSyncResult<List<DatasetMetadata>>(null, ex, obj)), options);
                }
                else
                {
                    foreach (Amazon.CognitoSync.Model.Dataset dataset in response.Datasets)
                    {
                        datasets.Add(ModelToDatasetMetadata(dataset));
                    }

                    nextToken = response.NextToken;

                    if (nextToken == null)
                    {
                        InternalSDKUtils.AsyncExecutor(() => callback(new AmazonCognitoSyncResult<List<DatasetMetadata>>(datasets, null, obj)), options);
                        return;
                    }
                    PopulateGetDatasetMetadataAsync(nextToken, datasets, callback, options);
                }
            },
            options);
        }
        #endregion

        #region ListUpdates


        public void ListUpdatesAsync(string datasetName, long lastSyncCount, AmazonCognitoSyncCallback<DatasetUpdates> callback, AsyncOptions options = null)
        {
            options = options ?? new AsyncOptions();
            PopulateListUpdatesAsync(datasetName, lastSyncCount, new List<Record>(), null, callback, options);
        }

        private void PopulateListUpdatesAsync(string datasetName, long lastSyncCount, List<Record> records, string nextToken, AmazonCognitoSyncCallback<DatasetUpdates> callback, AsyncOptions options)
        {
            ListRecordsRequest request = new ListRecordsRequest();
            request.IdentityPoolId = identityPoolId;
            request.IdentityId = this.GetCurrentIdentityId();
            request.DatasetName = datasetName;
            request.LastSyncCount = lastSyncCount;
            // mark it large enough to reduce # of requests
            request.MaxResults = 1024;
            request.NextToken = nextToken;

            client.ListRecordsAsync(request, (responseObj) =>
            {
                ListRecordsResponse res = responseObj.Response;
                Exception ex = responseObj.Exception;
                object obj = responseObj.state;

                if (ex != null)
                {
                    InternalSDKUtils.AsyncExecutor(() => callback(new AmazonCognitoSyncResult<DatasetUpdates>(null, ex, obj)), options); ;
                }
                else
                {
                    ListRecordsResponse listRecordsResponse = res;
                    foreach (Amazon.CognitoSync.Model.Record remoteRecord in listRecordsResponse.Records)
                    {
                        //builder.addRecord(modelToRecord(remoteRecord));
                        records.Add(this.ModelToRecord(remoteRecord));
                    }
                    if (listRecordsResponse.NextToken == null)
                    {
                        DatasetUpdates updates = new DatasetUpdates(
                            datasetName,
                            records,
                            listRecordsResponse.DatasetSyncCount,
                            listRecordsResponse.SyncSessionToken,
                            listRecordsResponse.DatasetExists,
                            listRecordsResponse.DatasetDeletedAfterRequestedSyncCount,
                            listRecordsResponse.MergedDatasetNames
                        );

                        InternalSDKUtils.AsyncExecutor(() => callback(new AmazonCognitoSyncResult<DatasetUpdates>(updates, ex, obj)), options);

                        return;
                    }
                    // update last evaluated key
                    nextToken = listRecordsResponse.NextToken;

                    // emulating the while loop
                    PopulateListUpdatesAsync(datasetName, lastSyncCount, records, nextToken, callback, new AsyncOptions() { ExecuteCallbackOnMainThread = false, State = obj });
                }
            },
            options);

        }

        #endregion


        #region PutRecords


        public void PutRecordsAsync(string datasetName, List<Record> records, string syncSessionToken, AmazonCognitoSyncCallback<List<Record>> callback, AsyncOptions options = null)
        {
            options = options ?? new AsyncOptions();
            UpdateRecordsRequest request = new UpdateRecordsRequest();
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

            client.UpdateRecordsAsync(request, (responseObj) =>
            {
                Exception ex = responseObj.Exception;
                UpdateRecordsResponse updateRecordsResponse = responseObj.Response;
                Exception putRecordsException = null;
                object obj = responseObj.state;
                if (ex != null)
                {
                    putRecordsException = HandleException(ex, "Failed to update records in dataset: " + datasetName);
                }
                else
                {
                    foreach (Amazon.CognitoSync.Model.Record remoteRecord in updateRecordsResponse.Records)
                    {
                        updatedRecords.Add(ModelToRecord(remoteRecord));
                    }
                }

                InternalSDKUtils.AsyncExecutor(() => callback(new AmazonCognitoSyncResult<List<Record>>(updatedRecords, putRecordsException, obj)), options);

            },
            options);
        }

        #endregion


        #region DeleteDataset


        public void DeleteDatasetAsync(string datasetName, AmazonCognitoSyncCallback callback, AsyncOptions options = null)
        {
            options = options ?? new AsyncOptions();

            DeleteDatasetRequest request = new DeleteDatasetRequest();
            request.IdentityPoolId = identityPoolId;
            request.IdentityId = this.GetCurrentIdentityId();
            request.DatasetName = datasetName;

            client.DeleteDatasetAsync(request, (responseObj) =>
            {
                if (responseObj.Exception != null)
                {
                    InternalSDKUtils.AsyncExecutor(() => callback(new AmazonCognitoSyncResult(HandleException(responseObj.Exception, "Failed to delete dataset: " + datasetName), responseObj.state)), options);
                }
                else
                {
                    InternalSDKUtils.AsyncExecutor(() => callback(new AmazonCognitoSyncResult(null, responseObj.state)), options);
                }
            },
            options);
        }

        #endregion


        #region GetDatasetMetadata

        public void GetDatasetMetadataAsync(string datasetName, AmazonCognitoSyncCallback<DatasetMetadata> callback, AsyncOptions options = null)
        {
            options = options ?? new AsyncOptions();

            DescribeDatasetRequest request = new DescribeDatasetRequest();
            request.IdentityPoolId = identityPoolId;
            request.IdentityId = this.GetCurrentIdentityId();
            request.DatasetName = datasetName;

            client.DescribeDatasetAsync(request, (responseObj) =>
            {
                Exception dataStorageException = null;
                DatasetMetadata metadata = null;
                if (responseObj.Exception != null)
                {
                    dataStorageException = new DataStorageException("Failed to get metadata of dataset: "
                                                                        + datasetName, responseObj.Exception);
                }
                else
                {
                    metadata = ModelToDatasetMetadata(responseObj.Response.Dataset);
                }

                InternalSDKUtils.AsyncExecutor(() => callback(new AmazonCognitoSyncResult<DatasetMetadata>(metadata, dataStorageException, responseObj.state)), options);
            },
            options);
        }

        #endregion


        #region Private Methods

        private string GetCurrentIdentityId()
        {
            return cognitoCredentials.GetIdentityId();
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

        private SyncManagerException HandleException(Exception e, string message)
        {
            var ase = e as AmazonServiceException;

            if (ase == null) ase = new AmazonServiceException(e);

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
            else if (IsNetworkException(ase))
            {
                return new NetworkException(message);
            }
            else
            {
                return new DataStorageException(message, ase);
            }
        }

        private static bool IsNetworkException(AmazonServiceException ase)
        {
            return ase.InnerException != null && ase.InnerException.GetType() == typeof(IOException);
        }

        #endregion
    }
}

