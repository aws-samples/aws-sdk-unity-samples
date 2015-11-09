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

using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Util;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Amazon.DynamoDBv2.DocumentModel
{
    public partial class Table
    {
        #region Constructor/factory
        
        /// <summary>
        /// Creates a Table object with the specified name, using the
        /// passed-in client to load the table definition.
        /// The returned table will use the conversion specified by
        /// AWSConfigs.DynamoDBConfig.ConversionSchema
        /// 
        /// This method return an exception if the table does not exist within the callback.
        /// </summary>
        /// <param name="ddbClient">Client to use to access DynamoDB.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</para>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</para>
        public static void LoadTableAsync(IAmazonDynamoDB ddbClient, string tableName, AmazonDynamoDBCallback<Table> callback, AsyncOptions asyncOptions = null)
        {
            LoadTableAsync(ddbClient,tableName, DynamoDBEntryConversion.CurrentConversion, callback,asyncOptions);
        }
        
        /// <summary>
        /// Creates a Table object with the specified name, using the
        /// passed-in client to load the table definition.
        /// 
        /// This method return an exception if the table does not exist within the callback.
        /// </summary>
        /// <param name="ddbClient">Client to use to access DynamoDB.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="conversion">Conversion to use for converting .NET values to DynamoDB values.</param>
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</para>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</para>
        public static void LoadTableAsync(IAmazonDynamoDB ddbClient, string tableName, DynamoDBEntryConversion conversion, AmazonDynamoDBCallback<Table> callback, AsyncOptions asyncOptions = null)
        {
            LoadTableAsync(ddbClient,tableName, DynamoDBConsumer.DocumentModel, conversion,callback,asyncOptions);
        }
        
        internal static void LoadTableAsync(IAmazonDynamoDB ddbClient, string tableName, Table.DynamoDBConsumer consumer, DynamoDBEntryConversion conversion, AmazonDynamoDBCallback<Table> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions??new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Table>(
            ()=>{
                return LoadTable(ddbClient,tableName,consumer,conversion);
            },asyncOptions,callback);
        }
        
        #endregion
    
        #region PutItemAsync

        /// <summary>
        /// Initiates the asynchronous execution of the PutItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.PutItem"/>
        /// </summary>
        /// <param name="doc">Document to save.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void PutItemAsync(Document doc, AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return PutItemHelper(doc, null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the PutItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.PutItem"/>
        /// </summary>
        /// <param name="doc">Document to save.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void PutItemAsync(Document doc, PutItemOperationConfig config,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return PutItemHelper(doc, config, true); },
                asyncOptions,
                callback);
        }

        #endregion

        #region GetItemAsync

        /// <summary>
        /// Initiates the asynchronous execution of the GetItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.GetItem"/>
        /// </summary>
        /// <param name="hashKey">Hash key element of the document.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void GetItemAsync(Primitive hashKey, AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return GetItemHelper(MakeKey(hashKey, null), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the GetItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.GetItem"/>
        /// </summary>
        /// <param name="hashKey">Hash key element of the document.</param>
        /// <param name="rangeKey">Range key element of the document.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void GetItemAsync(Primitive hashKey, Primitive rangeKey,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return GetItemHelper(MakeKey(hashKey, rangeKey), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the GetItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.GetItem"/>
        /// </summary>
        /// <param name="hashKey">Hash key element of the document.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void GetItemAsync(Primitive hashKey, GetItemOperationConfig config,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return GetItemHelper(MakeKey(hashKey, null), config, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the GetItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.GetItem"/>
        /// </summary>
        /// <param name="hashKey">Hash key element of the document.</param>
        /// <param name="rangeKey">Range key element of the document.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void GetItemAsync(Primitive hashKey, Primitive rangeKey, GetItemOperationConfig config,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return GetItemHelper(MakeKey(hashKey, rangeKey), config, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the GetItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.GetItem"/>
        /// </summary>
        /// <param name="key">Key of the document.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void GetItemAsync(IDictionary<string, DynamoDBEntry> key,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return GetItemHelper(MakeKey(key), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the GetItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.GetItem"/>
        /// </summary>
        /// <param name="key">Ley of the document.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void GetItemAsync(IDictionary<string, DynamoDBEntry> key, GetItemOperationConfig config,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return GetItemHelper(MakeKey(key), config, true); },
                asyncOptions,
                callback);
        }

        #endregion

        #region UpdateItemAsync

        /// <summary>
        /// Initiates the asynchronous execution of the UpdateItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.UpdateItem"/>
        /// </summary>
        /// <param name="doc">Document to update.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void UpdateItemAsync(Document doc,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return UpdateHelper(doc, MakeKey(doc), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the UpdateItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.UpdateItem"/>
        /// </summary>
        /// <param name="doc">Attributes to update.</param>
        /// <param name="hashKey">Hash key element of the document.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void UpdateItemAsync(Document doc, Primitive hashKey,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return UpdateHelper(doc, MakeKey(hashKey, null), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the UpdateItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.UpdateItem"/>
        /// </summary>
        /// <param name="doc">Attributes to update.</param>
        /// <param name="hashKey">Hash key element of the document.</param>
        /// <param name="rangeKey">Range key element of the document.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void UpdateItemAsync(Document doc, Primitive hashKey, Primitive rangeKey,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return UpdateHelper(doc, MakeKey(hashKey, rangeKey), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the UpdateItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.UpdateItem"/>
        /// </summary>
        /// <param name="doc">Attributes to update.</param>
        /// <param name="key">Key of the document.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void UpdateItemAsync(Document doc, IDictionary<string, DynamoDBEntry> key,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return UpdateHelper(doc, MakeKey(key), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the UpdateItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.UpdateItem"/>
        /// </summary>
        /// <param name="doc">Document to update.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void UpdateItemAsync(Document doc, UpdateItemOperationConfig config,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return UpdateHelper(doc, MakeKey(doc), config, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the UpdateItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.UpdateItem"/>
        /// </summary>
        /// <param name="doc">Attributes to update.</param>
        /// <param name="hashKey">Hash key element of the document.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void UpdateItemAsync(Document doc, Primitive hashKey, UpdateItemOperationConfig config,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return UpdateHelper(doc, MakeKey(hashKey, null), config, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the UpdateItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.UpdateItem"/>
        /// </summary>
        /// <param name="doc">Attributes to update.</param>
        /// <param name="hashKey">Hash key element of the document.</param>
        /// <param name="rangeKey">Range key element of the document.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void UpdateItemAsync(Document doc, Primitive hashKey, Primitive rangeKey, UpdateItemOperationConfig config,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return UpdateHelper(doc, MakeKey(hashKey, rangeKey), config, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the UpdateItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.UpdateItem"/>
        /// </summary>
        /// <param name="doc">Attributes to update.</param>
        /// <param name="key">Key of the document.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void UpdateItemAsync(Document doc, IDictionary<string, DynamoDBEntry> key, UpdateItemOperationConfig config,
            AmazonDynamoDBCallback<Document> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<Document>(
                () => { return UpdateHelper(doc, MakeKey(key), config, true); },
                asyncOptions,
                callback);
        }

        #endregion

        #region DeleteItemAsync

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.DeleteItem"/>
        /// </summary>
        /// <param name="document">Document to delete.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteItemAsync(Document document,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () => { DeleteHelper(MakeKey(document), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.DeleteItem"/>
        /// </summary>
        /// <param name="hashKey">Hash key element of the document.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteItemAsync(Primitive hashKey,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () => { DeleteHelper(MakeKey(hashKey, null), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.DeleteItem"/>
        /// </summary>
        /// <param name="hashKey">Hash key element of the document.</param>
        /// <param name="rangeKey">Range key element of the document.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteItemAsync(Primitive hashKey, Primitive rangeKey,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () => { DeleteHelper(MakeKey(hashKey, rangeKey), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.DeleteItem"/>
        /// </summary>
        /// <param name="key">Key of the document.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteItemAsync(IDictionary<string, DynamoDBEntry> key,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () => { DeleteHelper(MakeKey(key), null, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.DeleteItem"/>
        /// </summary>
        /// <param name="document">Document to delete.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteItemAsync(Document document, DeleteItemOperationConfig config,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () => { DeleteHelper(MakeKey(document), config, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.DeleteItem"/>
        /// </summary>
        /// <param name="hashKey">Hash key element of the document.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteItemAsync(Primitive hashKey, DeleteItemOperationConfig config,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () => { DeleteHelper(MakeKey(hashKey, null), config, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.DeleteItem"/>
        /// </summary>
        /// <param name="hashKey">Hash key element of the document.</param>
        /// <param name="rangeKey">Range key element of the document.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteItemAsync(Primitive hashKey, Primitive rangeKey, DeleteItemOperationConfig config,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () => { DeleteHelper(MakeKey(hashKey, rangeKey), config, true); },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteItem operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Table.DeleteItem"/>
        /// </summary>
        /// <param name="key">Key of the document.</param>
        /// <param name="config">Configuration to use.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DeleteItemAsync(IDictionary<string, DynamoDBEntry> key, DeleteItemOperationConfig config,
            AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () => { DeleteHelper(MakeKey(key), config, true); },
                asyncOptions,
                callback);
        }
        
        #endregion

    }
}
