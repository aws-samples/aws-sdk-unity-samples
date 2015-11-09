#define AWSSDK_UNITY
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

using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace Amazon.DynamoDBv2.DocumentModel
{
    /// <summary>
    /// Class for retrieving a batch of Documents from a single DynamoDB table.
    /// </summary>
    public partial class DocumentBatchGet
    {
        #region Internal properties

        internal Table TargetTable { get; private set; }
        internal List<Key> Keys { get; private set; }

        #endregion


        #region Public properties

        /// <summary>
        /// List of results retrieved from DynamoDB.
        /// Populated after Execute is called.
        /// </summary>
        public List<Document> Results { get; internal set; }

        /// <summary>
        /// List of attributes to retrieve.
        /// </summary>
        public List<string> AttributesToGet { get; set; }

        /// <summary>
        /// If set to true, a consistent read is issued. Otherwise eventually-consistent is used.
        /// </summary>
        public bool ConsistentRead { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructs a DocumentBatchGet instance for a specific table.
        /// </summary>
        /// <param name="targetTable">Table to get items from.</param>
        public DocumentBatchGet(Table targetTable)
        {
            TargetTable = targetTable;
            Keys = new List<Key>();
        }

        #endregion


        #region Public methods

        /// <summary>
        /// Add a single item to get, identified by its hash primary key.
        /// </summary>
        /// <param name="hashKey">Hash key element of the item to get.</param>
        public void AddKey(Primitive hashKey)
        {
            AddKey(hashKey, null);
        }

        /// <summary>
        /// Add a single item to get, identified by its hash-and-range primary key.
        /// </summary>
        /// <param name="hashKey">Hash key element of the item to get.</param>
        /// <param name="rangeKey">Range key element of the item to get.</param>
        public void AddKey(Primitive hashKey, Primitive rangeKey)
        {
            Keys.Add(TargetTable.MakeKey(hashKey, rangeKey));
        }

        /// <summary>
        /// Add a single item to get, identified by its key.
        /// </summary>
        /// <param name="key">Key of the item to get.</param>
        public void AddKey(IDictionary<string, DynamoDBEntry> key)
        {
            Keys.Add(TargetTable.MakeKey(key));
        }

        /// <summary>
        /// Creates a MultiTableDocumentBatchGet object that is a combination
        /// of the current DocumentBatchGet and the specified DocumentBatchGet.
        /// </summary>
        /// <param name="otherBatch">Other DocumentBatchGet object.</param>
        /// <returns>
        /// MultiTableDocumentBatchGet consisting of the two DocumentBatchGet
        /// objects.
        /// </returns>
        public MultiTableDocumentBatchGet Combine(DocumentBatchGet otherBatch)
        {
            return new MultiTableDocumentBatchGet(this, otherBatch);
        }

        #endregion


        #region Internal methods

        internal void ExecuteHelper(bool isAsync)
        {
            MultiBatchGet resultsObject = new MultiBatchGet
            {
                Batches = new List<DocumentBatchGet> { this }
            };

            var results = resultsObject.GetItemsHelper(isAsync);

            List<Document> batchResults;
            if (results.TryGetValue(TargetTable.TableName, out batchResults))
            {
                Results = batchResults;
            }
            else
            {
                Results = new List<Document>();
            }
        }
        internal void AddKey(Document document)
        {
            Keys.Add(TargetTable.MakeKey(document));
        }
        internal void AddKey(Key key)
        {
            Keys.Add(key);
        }

        #endregion
    }

    /// <summary>
    /// Class for retrieving a batch of Documents from multiple DynamoDB tables.
    /// </summary>
    public partial class MultiTableDocumentBatchGet
    {
        #region Properties

        /// <summary>
        /// List of DocumentBatchGet objects to include in the multi-table
        /// batch request.
        /// </summary>
        public List<DocumentBatchGet> Batches { get; private set; }

        /// <summary>
        /// Total number of primary keys in the multi-table batch request.
        /// </summary>
        public int TotalKeys
        {
            get
            {
                int count = 0;
                foreach (var batch in Batches)
                {
                    count += batch.Keys.Count;
                }
                return count;
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructs a MultiTableDocumentBatchGet object from a number of
        /// DocumentBatchGet objects.
        /// </summary>
        /// <param name="batches">Collection of DocumentBatchGet objects.</param>
        public MultiTableDocumentBatchGet(params DocumentBatchGet[] batches)
        {
            if (batches == null)
                throw new ArgumentNullException("batches");

            Batches = new List<DocumentBatchGet>(batches);
        }

        #endregion


        #region Public methods
        
        /// <summary>
        /// Add a DocumentBatchGet object to the multi-table batch request.
        /// </summary>
        /// <param name="batch">DocumentBatchGet to add.</param>
        public void AddBatch(DocumentBatchGet batch)
        {
            Batches.Add(batch);
        }

        #endregion

        #region Internal methods

        internal void ExecuteHelper(bool isAsync)
        {
            MultiBatchGet resultsObject = new MultiBatchGet
            {
                Batches = Batches
            };

            var results = resultsObject.GetItemsHelper(isAsync);

            foreach (var batch in Batches)
            {
                List<Document> batchResults;
                if (results.TryGetValue(batch.TargetTable.TableName, out batchResults))
                {
                    batch.Results = batchResults;
                }
                else
                {
                    batch.Results = new List<Document>();
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Internal class for handling multi-table batch gets.
    /// </summary>
    internal class MultiBatchGet
    {
        /// <summary>
        /// Batches that comprise the current BatchGet operation
        /// </summary>
        public List<DocumentBatchGet> Batches { get; set; }

        /// <summary>
        /// Maximum number of items that can be sent in a single BatchGet request
        /// </summary>
        public const int MaxItemsPerCall = 100;

        /// <summary>
        /// Gets items ocnfigured in Batches from the server
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<Document>> GetItems()
        {
            return GetItemsHelper(false);
        }

        // Dictionary to be populated by service calls
        private Dictionary<string, List<Dictionary<string, AttributeValue>>> AllRetrievedItems;

        internal Dictionary<string, List<Document>> GetItemsHelper(bool isAsync)
        {
            AllRetrievedItems = new Dictionary<string, List<Dictionary<string, AttributeValue>>>();
            GetAttributeItems(isAsync);
            var itemsAsDocuments = new Dictionary<string, List<Document>>();

            foreach (var kvp in AllRetrievedItems)
            {
                List<Document> documents = new List<Document>();
                foreach (var dictionary in kvp.Value)
                {
                    documents.Add(Document.FromAttributeMap(dictionary));
                }
                itemsAsDocuments[kvp.Key] = documents;
            }

            return itemsAsDocuments;
        }

        private void GetAttributeItems(bool isAsync)
        {
            if (Batches == null || Batches.Count == 0)
                return;

            var firstBatch = this.Batches[0];
            var targetTable = firstBatch.TargetTable;
            var client = targetTable.DDBClient;

            var convertedBatches = ConvertBatches();
            while (true)
            {
                var nextSet = GetNextRequestItems(ref convertedBatches, MaxItemsPerCall);
                if (nextSet.Count == 0)
                    break;

                BatchGetItemRequest request = CreateRequest(nextSet, targetTable, isAsync);
                CallUntilCompletion(client, request);
            }
        }

#if PCL|| AWSSDK_UNITY
        private void CallUntilCompletion(AmazonDynamoDBClient client, BatchGetItemRequest request)
#else
        private void CallUntilCompletion(IAmazonDynamoDB client, BatchGetItemRequest request)
#endif
        {
            do
            {
                var result = client.BatchGetItem(request);

                var responses = result.Responses;
                foreach (var response in responses)
                {
                    var tableName = response.Key;
                    var items = response.Value;

                    List<Dictionary<string, AttributeValue>> fetchedItems;
                    if (!AllRetrievedItems.TryGetValue(tableName, out fetchedItems))
                    {
                        fetchedItems = new List<Dictionary<string, AttributeValue>>();
                        AllRetrievedItems[tableName] = fetchedItems;
                    }
                    fetchedItems.AddRange(items);
                }
                request.RequestItems = result.UnprocessedKeys;
            } while (request.RequestItems.Count > 0);
        }

        private static BatchGetItemRequest CreateRequest(Dictionary<string, RequestSet> set, Table targetTable, bool isAsync)
        {
            BatchGetItemRequest request = new BatchGetItemRequest();
            ((Amazon.Runtime.Internal.IAmazonWebServiceRequest)request).AddBeforeRequestHandler(isAsync ?
                new RequestEventHandler(targetTable.UserAgentRequestEventHandlerAsync) :
                new RequestEventHandler(targetTable.UserAgentRequestEventHandlerSync)
                );
            var requestItems = new Dictionary<string, KeysAndAttributes>();
            foreach (var kvp in set)
            {
                var tableName = kvp.Key;
                var requestSet = kvp.Value;

                var keys = new KeysAndAttributes
                {
                    Keys = requestSet.GetItems(),
                    ConsistentRead = requestSet.Batch.ConsistentRead,
                    AttributesToGet = requestSet.Batch.AttributesToGet
                };

                requestItems.Add(tableName, keys);
            }
            request.RequestItems = requestItems;

            return request;
        }

        private Dictionary<string, RequestSet> ConvertBatches()
        {
            var allItems = new Dictionary<string, RequestSet>();
            if (Batches == null || Batches.Count == 0)
                return allItems;

            foreach (var batch in Batches)
            {
                string tableName = batch.TargetTable.TableName;
                if (allItems.ContainsKey(tableName))
                    throw new AmazonDynamoDBException("More than one batch request against a single table is not supported.");

                if (batch.Keys != null && batch.Keys.Count > 0)
                {
                    var keysList = batch.Keys.Select((Key k) => k as Dictionary<string, AttributeValue>);
                    var keys = new RequestSet(keysList, batch);

                    allItems.Add(tableName, keys);
                }
            }

            return allItems;
        }

        private static Dictionary<string, RequestSet> GetNextRequestItems(ref Dictionary<string, RequestSet> getRequestsMap, int maxNumberOfItems)
        {
            int numberOfItems = 0;
            var nextItems = new Dictionary<string, RequestSet>();
            List<string> keys = new List<string>(getRequestsMap.Keys);
            foreach (string tableName in keys)
            {
                if (numberOfItems >= maxNumberOfItems)
                    break;

                var getRequests = getRequestsMap[tableName];
                if (getRequests.Count == 0)
                    continue;

                var partialRequests = getRequests.RemoveFromHead(maxNumberOfItems - numberOfItems);
                var partialRequestsList = new RequestSet(partialRequests, getRequests.Batch);

                nextItems[tableName] = partialRequestsList;
                numberOfItems += partialRequestsList.Count;
            }

            return nextItems;
        }

        private class RequestSet : QuickList<Dictionary<string, AttributeValue>>
        {
            public DocumentBatchGet Batch { get; private set; }

            public RequestSet(IEnumerable<Dictionary<string, AttributeValue>> items, DocumentBatchGet batch)
                : base(items)
            {
                Batch = batch;
            }
        }
    }
}
