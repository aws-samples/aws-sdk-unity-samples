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
using System.Linq;

using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace Amazon.DynamoDBv2.DataModel
{
    /// <summary>
    /// Context object for using the DataModel mode of DynamoDB.
    /// Used to interact with the service, save/load objects, etc.
    /// </summary>
    public partial class DynamoDBContext : IDynamoDBContext
    {
        #region Save async

        /// <summary>
        /// Initiates the asynchronous execution of the Save operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Save"/>
        /// </summary>
        /// <typeparam name="T">Type to save as.</typeparam>
        /// <param name="value">Object to save.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>        
        public void SaveAsync<T>(T value, AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () => { SaveHelper<T>(value, null, true);},
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Save operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Save"/>
        /// </summary>
        /// <typeparam name="T">Type to save as.</typeparam>
        /// <param name="value">Object to save.</param>
        /// <param name="operationConfig">Overriding configuration.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void SaveAsync<T>(T value, DynamoDBOperationConfig operationConfig, AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () => { SaveHelper<T>(value, operationConfig, true); },
                asyncOptions,
                callback);
        }

        #endregion

        #region Load async

        /// <summary>
        /// Initiates the asynchronous execution of the Load operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Load"/>
        /// </summary>
        /// <typeparam name="T">Type to populate.</typeparam>
        /// <param name="hashKey">Hash key element of the target item.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void LoadAsync<T>(object hashKey, AmazonDynamoDBCallback<T> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<T>(
                    () =>
                    {
                        return LoadHelper<T>(hashKey, null, null, true);
                    },
                    asyncOptions,
                    callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Load operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Load"/>
        /// </summary>
        /// <typeparam name="T">Type to populate.</typeparam>
        /// <param name="hashKey">Hash key element of the target item.</param>
        /// <param name="rangeKey">Range key element of the target item.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void LoadAsync<T>(object hashKey, object rangeKey, AmazonDynamoDBCallback<T> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<T>(
                    () =>
                    {
                        return LoadHelper<T>(hashKey, rangeKey, null, true);
                    },
                    asyncOptions,
                    callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Load operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Load"/>
        /// </summary>
        /// <typeparam name="T">Type to populate.</typeparam>
        /// <param name="hashKey">Hash key element of the target item.</param>
        /// <param name="operationConfig">Overriding configuration.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void LoadAsync<T>(object hashKey, DynamoDBOperationConfig operationConfig,
            AmazonDynamoDBCallback<T> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<T>(
                    () =>
                    {
                        return LoadHelper<T>(hashKey, null, operationConfig, true);
                    },
                    asyncOptions,
                    callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Load operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Load"/>
        /// </summary>
        /// <typeparam name="T">Type to populate.</typeparam>
        /// <param name="hashKey">Hash key element of the target item.</param>
        /// <param name="rangeKey">Range key element of the target item.</param>
        /// <param name="operationConfig">Overriding configuration.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void LoadAsync<T>(object hashKey, object rangeKey, DynamoDBOperationConfig operationConfig,
            AmazonDynamoDBCallback<T> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<T>(
                    () =>
                    {
                        return LoadHelper<T>(hashKey, rangeKey, operationConfig, true);
                    },
                    asyncOptions,
                    callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Load operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Load"/>
        /// </summary>
        /// <typeparam name="T">Type to populate.</typeparam>
        /// <param name="keyObject">Key of the target item.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void LoadAsync<T>(T keyObject, AmazonDynamoDBCallback<T> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<T>(
                        () =>
                        {
                            return LoadHelper<T>(keyObject, null, true);
                        },
                        asyncOptions,
                        callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Load operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Load"/>
        /// </summary>
        /// <typeparam name="T">Type to populate.</typeparam>
        /// <param name="keyObject">Key of the target item.</param>
        /// <param name="operationConfig">Overriding configuration.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void LoadAsync<T>(T keyObject, DynamoDBOperationConfig operationConfig,
            AmazonDynamoDBCallback<T> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<T>(
                       () =>
                       {
                           return LoadHelper<T>(keyObject, operationConfig, true);
                       },
                       asyncOptions,
                       callback);
        }
        
        #endregion

        #region Delete async

        /// <summary>
        /// Initiates the asynchronous execution of the Delete operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Delete"/>
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="value">Object to delete.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteAsync<T>(T value, AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                       () => { DeleteHelper<T>(value, null, true); },
                       asyncOptions,
                       callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Delete operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Delete"/>
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="value">Object to delete.</param>
        /// <param name="operationConfig">Overriding configuration.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteAsync<T>(T value, DynamoDBOperationConfig operationConfig,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                       () => { DeleteHelper<T>(value, operationConfig, true);},
                       asyncOptions,
                       callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Delete operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Delete"/>
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="hashKey">Hash key element of the object to delete.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteAsync<T>(object hashKey, AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                           () => { DeleteHelper<T>(hashKey, null, null, true); },
                           asyncOptions,
                           callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Delete operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Delete"/>
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="hashKey">Hash key element of the object to delete.</param>
        /// <param name="operationConfig">Config object which can be used to override that table used.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteAsync<T>(object hashKey, DynamoDBOperationConfig operationConfig,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                           () => { DeleteHelper<T>(hashKey, null, operationConfig, true); },
                           asyncOptions,
                           callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Delete operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Delete"/>
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="hashKey">Hash key element of the object to delete.</param>
        /// <param name="rangeKey">Range key element of the object to delete.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteAsync<T>(object hashKey, object rangeKey,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                           () => { DeleteHelper<T>(hashKey, rangeKey, null, true); },
                           asyncOptions,
                           callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Delete operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.Delete"/>
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="hashKey">Hash key element of the object to delete.</param>
        /// <param name="rangeKey">Range key element of the object to delete.</param>
        /// <param name="operationConfig">Config object which can be used to override that table used.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteAsync<T>(object hashKey, object rangeKey, DynamoDBOperationConfig operationConfig,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                           () => { DeleteHelper<T>(hashKey, rangeKey, operationConfig, true); },
                           asyncOptions,
                           callback);
        }

        #endregion

        #region BatchGet async

        /// <summary>
        /// Initiates the asynchronous execution of the ExecuteBatchGet operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.ExecuteBatchGet"/>
        /// </summary>
        /// <param name="batches">Configured BatchGet objects</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void ExecuteBatchGetAsync<T>(BatchGet[] batches, AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            MultiTableBatchGet superBatch = new MultiTableBatchGet(batches);
            DynamoDBAsyncExecutor.ExecuteAsync(
                          () => { superBatch.ExecuteHelper(true); },
                          asyncOptions,
                          callback);
        }

        #endregion

        #region BatchWrite async

        /// <summary>
        /// Initiates the asynchronous execution of the ExecuteBatchWrite operation.
        /// <seealso cref="Amazon.DynamoDBv2.DataModel.DynamoDBContext.ExecuteBatchWrite"/>
        /// </summary>
        /// <param name="batches">Configured BatchWrite objects</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void ExecuteBatchWriteAsync<T>(BatchWrite[] batches, AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            MultiTableBatchWrite superBatch = new MultiTableBatchWrite(batches);
            DynamoDBAsyncExecutor.ExecuteAsync(
                          () => { superBatch.ExecuteHelper(true); },
                          asyncOptions,
                          callback);
        }

        #endregion

        #region Scan async

        /// <summary>
        /// Configures an async Scan operation against DynamoDB, finding items
        /// that match the specified conditions.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="conditions">
        /// Conditions that the results should meet.
        /// </param>
        /// <returns>AsyncSearch which can be used to retrieve DynamoDB data.</returns>
        public AsyncSearch<T> ScanAsync<T>(params ScanCondition[] conditions)
        {
            if (conditions == null)
                throw new ArgumentNullException("conditions");

            return ScanAsync<T>(conditions, null);
        }

        /// <summary>
        ///  Configures an async Scan operation against DynamoDB, finding items
        /// that match the specified conditions.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="conditions">
        /// Conditions that the results should meet.
        /// </param>
        /// <param name="operationConfig">Config object which can be used to override that table used.</param>
        /// <returns>AsyncSearch which can be used to retrieve DynamoDB data.</returns>
        public AsyncSearch<T> ScanAsync<T>(IEnumerable<ScanCondition> conditions, DynamoDBOperationConfig operationConfig)
        {
            if (conditions == null)
                throw new ArgumentNullException("conditions");

            var scan = ConvertScan<T>(conditions, operationConfig);
            return FromSearchAsync<T>(scan);
        }

        /// <summary>
        ///  Configures an async Scan operation against DynamoDB, finding items
        /// that match the specified conditions.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="scanConfig">Scan request object.</param>
        /// <returns>AsyncSearch which can be used to retrieve DynamoDB data.</returns>
        public AsyncSearch<T> FromScanAsync<T>(ScanOperationConfig scanConfig)
        {
            return FromScanAsync<T>(scanConfig, null);
        }

        /// <summary>
        ///  Configures an async Scan operation against DynamoDB, finding items
        /// that match the specified conditions.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="scanConfig">Scan request object.</param>
        /// <param name="operationConfig">Config object which can be used to override the table used.</param>
        /// <returns>AsyncSearch which can be used to retrieve DynamoDB data.</returns>
        public AsyncSearch<T> FromScanAsync<T>(ScanOperationConfig scanConfig, DynamoDBOperationConfig operationConfig)
        {
            if (scanConfig == null) throw new ArgumentNullException("scanConfig");

            var scan = ConvertFromScan<T>(scanConfig, operationConfig);
            return FromSearchAsync<T>(scan);
        }

        #endregion

        #region Query async

        /// <summary>
        /// Configures an async Query operation against DynamoDB, finding items
        /// that match the specified hash primary key.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="hashKeyValue">Hash key of the items to query.</param>
        /// <returns>AsyncSearch which can be used to retrieve DynamoDB data.</returns>
        public AsyncSearch<T> QueryAsync<T>(object hashKeyValue)
        {
            var query = ConvertQueryByValue<T>(hashKeyValue, null, null);
            return FromSearchAsync<T>(query);
        }

        /// <summary>
        /// Configures an async Query operation against DynamoDB, finding items
        /// that match the specified hash primary key.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="hashKeyValue">Hash key of the items to query.</param>
        /// <param name="operationConfig">Config object which can be used to override the table used.</param>
        /// <returns>AsyncSearch which can be used to retrieve DynamoDB data.</returns>
        public AsyncSearch<T> QueryAsync<T>(object hashKeyValue, DynamoDBOperationConfig operationConfig)
        {
            ContextSearch query = ConvertQueryByValue<T>(hashKeyValue, null, operationConfig);
            return FromSearchAsync<T>(query);
        }

        /// <summary>
        /// Configures an async Query operation against DynamoDB, finding items
        /// that match the specified range element condition for a hash-and-range primary key.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="hashKeyValue">Hash key of the items to query.</param>
        /// <param name="op">Operation of the condition.</param>
        /// <param name="values">
        /// Value(s) of the condition.
        /// For all operations except QueryOperator.Between, values should be one value.
        /// For QueryOperator.Between, values should be two values.
        /// </param>
        /// <returns>AsyncSearch which can be used to retrieve DynamoDB data.</returns>
        public AsyncSearch<T> QueryAsync<T>(object hashKeyValue, QueryOperator op, params object[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentOutOfRangeException("values");

            return QueryAsync<T>(hashKeyValue, op, values, null);
        }

        /// <summary>
        /// Configures an async Query operation against DynamoDB, finding items
        /// that match the specified range element condition for a hash-and-range primary key.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="hashKeyValue">Hash key of the items to query.</param>
        /// <param name="op">Operation of the condition.</param>
        /// <param name="values">
        /// Value(s) of the condition.
        /// For all operations except QueryOperator.Between, values should be one value.
        /// For QueryOperator.Between, values should be two values.
        /// </param>
        /// <param name="operationConfig">Config object which can be used to override the table used.</param>
        /// <returns>AsyncSearch which can be used to retrieve DynamoDB data.</returns>
        public AsyncSearch<T> QueryAsync<T>(object hashKeyValue, QueryOperator op, IEnumerable<object> values, DynamoDBOperationConfig operationConfig)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            ContextSearch query = ConvertQueryByValue<T>(hashKeyValue, op, values, operationConfig);
            return FromSearchAsync<T>(query);
        }

        /// <summary>
        /// Configures an async Query operation against DynamoDB, finding items
        /// that match the specified conditions.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="queryConfig">Query request object.</param>
        /// <returns>AsyncSearch which can be used to retrieve DynamoDB data.</returns>
        public AsyncSearch<T> FromQueryAsync<T>(QueryOperationConfig queryConfig)
        {
            return FromQueryAsync<T>(queryConfig, null);
        }

        /// <summary>
        /// Configures an async Query operation against DynamoDB, finding items
        /// that match the specified conditions.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="queryConfig">Query request object.</param>
        /// <param name="operationConfig">Config object which can be used to override the table used.</param>
        /// <returns>AsyncSearch which can be used to retrieve DynamoDB data.</returns>
        public AsyncSearch<T> FromQueryAsync<T>(QueryOperationConfig queryConfig, DynamoDBOperationConfig operationConfig)
        {
            if (queryConfig == null) throw new ArgumentNullException("queryConfig");

            var query = ConvertFromQuery<T>(queryConfig, operationConfig);
            return FromSearchAsync<T>(query);
        }

        #endregion
    }
}
