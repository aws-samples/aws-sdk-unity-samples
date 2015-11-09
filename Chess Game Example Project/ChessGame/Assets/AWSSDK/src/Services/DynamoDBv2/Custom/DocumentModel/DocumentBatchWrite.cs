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
using System.IO;
using System.Net;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Util;

namespace Amazon.DynamoDBv2.DocumentModel
{
    /// <summary>
    /// Class for putting and/or deleting a batch of items in a single DynamoDB table.
    /// </summary>
    public partial class DocumentBatchWrite
    {
        #region Internal properties

        internal Table TargetTable { get; private set; }
        internal List<Key> ToDelete { get; private set; }
        internal List<Document> ToPut { get; private set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructs a DocumentBatchWrite instance for a specific table.
        /// </summary>
        /// <param name="targetTable">Table to get items from.</param>
        public DocumentBatchWrite(Table targetTable)
        {
            TargetTable = targetTable;
            ToDelete = new List<Key>();
            ToPut = new List<Document>();
        }

        #endregion


        #region Public Delete methods

        /// <summary>
        /// Add a single item to delete, identified by its hash primary key.
        /// </summary>
        /// <param name="hashKey">Hash key element of the item to delete.</param>
        public void AddKeyToDelete(Primitive hashKey)
        {
            AddKeyToDelete(hashKey, null);
        }

        /// <summary>
        /// Add a single item to delete, identified by its hash-and-range primary key.
        /// </summary>
        /// <param name="hashKey">Hash key element of the item to delete.</param>
        /// <param name="rangeKey">Range key element of the item to delete.</param>
        public void AddKeyToDelete(Primitive hashKey, Primitive rangeKey)
        {
            AddKeyToDelete(TargetTable.MakeKey(hashKey, rangeKey));
        }

        /// <summary>
        /// Add a single item to delete, identified by its key.
        /// </summary>
        /// <param name="key">Key of the item to delete.</param>
        public void AddKeyToDelete(IDictionary<string, DynamoDBEntry> key)
        {
            AddKeyToDelete(TargetTable.MakeKey(key));
        }

        /// <summary>
        /// Add a single item to delete, identified by a Document object.
        /// </summary>
        /// <param name="document">Document representing the item to be deleted.</param>
        public void AddItemToDelete(Document document)
        {
            AddKeyToDelete(TargetTable.MakeKey(document));
        }

        #endregion


        #region Public Put methods

        /// <summary>
        /// Add a single Document to put.
        /// </summary>
        /// <param name="document">Document to put.</param>
        public void AddDocumentToPut(Document document)
        {
            ToPut.Add(document);
        }

        #endregion


        #region Public methods

        /// <summary>
        /// Creates a MultiTableDocumentBatchWrite object that is a combination
        /// of the current DocumentBatchWrite and the specified DocumentBatchWrite.
        /// </summary>
        /// <param name="otherBatch">Other DocumentBatchWrite object.</param>
        /// <returns>
        /// MultiTableDocumentBatchWrite consisting of the two DocumentBatchWrite
        /// objects.
        /// </returns>
        public MultiTableDocumentBatchWrite Combine(DocumentBatchWrite otherBatch)
        {
            return new MultiTableDocumentBatchWrite(this, otherBatch);
        }

        #endregion


        #region Internal methods

        internal void ExecuteHelper(bool isAsync)
        {
            MultiBatchWrite multiBatchWrite = new MultiBatchWrite
            {
                Batches = new List<DocumentBatchWrite> { this }
            };
            multiBatchWrite.WriteItems(isAsync);
        }

        internal void AddKeyToDelete(Key key)
        {
            ToDelete.Add(key);
        }

        #endregion
    }

    /// <summary>
    /// Class for putting and/or deleting a batch of items in multiple DynamoDB tables.
    /// </summary>
    public partial class MultiTableDocumentBatchWrite
    {
        #region Properties

        /// <summary>
        /// List of DocumentBatchWrite objects to include in the multi-table
        /// batch request.
        /// </summary>
        public List<DocumentBatchWrite> Batches { get; private set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructs a MultiTableDocumentBatchWrite object from a number of
        /// DocumentBatchWrite objects.
        /// </summary>
        /// <param name="batches">Collection of DocumentBatchWrite objects.</param>
        public MultiTableDocumentBatchWrite(params DocumentBatchWrite[] batches)
        {
            if (batches == null)
                throw new ArgumentNullException("batches");

            Batches = new List<DocumentBatchWrite>(batches);
        }

        #endregion


        #region Public methods

        /// <summary>
        /// Add a DocumentBatchWrite object to the multi-table batch request.
        /// </summary>
        /// <param name="batch">DocumentBatchWrite to add.</param>
        public void AddBatch(DocumentBatchWrite batch)
        {
            Batches.Add(batch);
        }

        #endregion


        #region Internal methods

        internal void ExecuteHelper(bool isAsync)
        {
            MultiBatchWrite multiBatchWrite = new MultiBatchWrite
            {
                Batches = Batches
            };
            multiBatchWrite.WriteItems(isAsync);
        }

        #endregion
    }


    /// <summary>
    /// Internal class for handling multi-table batch writes.
    /// </summary>
    internal class MultiBatchWrite
    {
        private static KeyComparer keyComparer = new KeyComparer();

        /// <summary>
        /// Map which stores tables by its name.
        /// </summary>
        private Dictionary<string, Table> tableMap = new Dictionary<string, Table>(StringComparer.Ordinal);

        /// <summary>
        /// Maximum number of items that can be sent in a single BatchWrite request
        /// </summary>
        public const int MaxItemsPerCall = 25;

        /// <summary>
        /// Batches that comprise the current BatchWrite operation
        /// </summary>
        public List<DocumentBatchWrite> Batches { get; set; }

        /// <summary>
        /// Pushes items configured in Batches to the server
        /// </summary>
        /// <param name="isAsync"></param>
        public void WriteItems(bool isAsync)
        {
            WriteItemsHelper(Batches, isAsync);
        }

        #region Private helper methods

        private void WriteItemsHelper(List<DocumentBatchWrite> batches, bool isAsync)
        {
            if (Batches == null || Batches.Count == 0)
                return;

            Dictionary<string, QuickList<WriteRequestDocument>> writeRequestsMap = ConvertBatches(batches);
            Table targetTable = this.Batches[0].TargetTable;
            while (true)
            {
                Dictionary<string, QuickList<WriteRequestDocument>> nextSet = GetNextWriteItems(ref writeRequestsMap, MaxItemsPerCall);
                if (nextSet.Count == 0)
                    break;

                SendSet(nextSet, targetTable, isAsync);
            }
        }

        private void SendSet(Dictionary<string, QuickList<WriteRequestDocument>> set, Table targetTable, bool isAsync)
        {
            Dictionary<string, Dictionary<Key, Document>> documentMap = null;
            BatchWriteItemRequest request = ConstructRequest(set, targetTable, out documentMap, isAsync);
            if (request.RequestItems.Count == 0)
                return;

            bool shouldTrySmallerRequest = false;
            try
            {
                CallUntilCompletion(request, documentMap, targetTable.DDBClient);
            }
            catch (AmazonDynamoDBException addbex)
            {
                if (addbex.StatusCode == HttpStatusCode.RequestEntityTooLarge)
                    shouldTrySmallerRequest = true;
                else
                    throw;
            }

            if (shouldTrySmallerRequest)
            {
                int totalWrites = GetNumberOfWrites(request);
                if (totalWrites >= 2)
                {
                    // 2 or more items in request, retry with the request split up
                    var partialSet = GetNextWriteItems(ref set, totalWrites / 2);
                    SendSet(partialSet, targetTable, isAsync);
                    SendSet(set, targetTable, isAsync);
                }
                else
                {
                    // only 1 item in request, retry as-is
                    SendSet(set, targetTable, isAsync);
                }
            }
        }

        private static int GetNumberOfWrites(BatchWriteItemRequest request)
        {
            int totalWrites = 0;
            foreach (var writeItem in request.RequestItems)
            {
                var writes = writeItem.Value;
                totalWrites += (writes == null ? 0 : writes.Count);
            }
            return totalWrites;
        }

        private BatchWriteItemRequest ConstructRequest(
            Dictionary<string, QuickList<WriteRequestDocument>> writeItems,
            Table targetTable,
            out Dictionary<string, Dictionary<Key, Document>> documentMap,
            bool isAsync)
        {
            documentMap = new Dictionary<string, Dictionary<Key, Document>>(StringComparer.Ordinal);
            BatchWriteItemRequest request = new BatchWriteItemRequest();

            foreach (var writeItem in writeItems)
            {
                string tableName = writeItem.Key;
                List<WriteRequestDocument> requestItems = writeItem.Value.GetItems();
                if (requestItems != null && requestItems.Count > 0)
                {
                    var table = tableMap[tableName];
                    var requestList = new List<WriteRequest>();
                    var tableDocumentMap = new Dictionary<Key, Document>(keyComparer);
                    documentMap.Add(tableName, tableDocumentMap);

                    foreach (var item in requestItems)
                    {
                        requestList.Add(item.WriteRequest);
                        // Add document corresponding to the Put request to document map
                        if (item.WriteRequest.PutRequest != null)
                        {
                            var key = table.MakeKey(item.Document);
                            tableDocumentMap.Add(key, item.Document);
                        }
                    }
                    request.RequestItems[tableName] = requestList;
                }
            }

            ((Amazon.Runtime.Internal.IAmazonWebServiceRequest)request).AddBeforeRequestHandler(isAsync ?
                new RequestEventHandler(targetTable.UserAgentRequestEventHandlerAsync) :
                new RequestEventHandler(targetTable.UserAgentRequestEventHandlerSync)
                );

            return request;
        }

        private static Dictionary<string, QuickList<WriteRequestDocument>> GetNextWriteItems(ref Dictionary<string, QuickList<WriteRequestDocument>> writeRequestsMap, int maxNumberOfItems)
        {
            int numberOfItems = 0;
            Dictionary<string, QuickList<WriteRequestDocument>> nextItems = new Dictionary<string, QuickList<WriteRequestDocument>>();
            List<string> keys = new List<string>(writeRequestsMap.Keys);
            foreach (string tableName in keys)
            {
                if (numberOfItems >= maxNumberOfItems)
                    break;

                QuickList<WriteRequestDocument> writeRequests = writeRequestsMap[tableName];
                if (writeRequests.Count == 0)
                    continue;

                IEnumerable<WriteRequestDocument> partialRequests = writeRequests.RemoveFromHead(maxNumberOfItems - numberOfItems);
                QuickList<WriteRequestDocument> partialRequestsList = new QuickList<WriteRequestDocument>(partialRequests);

                nextItems[tableName] = partialRequestsList;
                numberOfItems += partialRequestsList.Count;
            }

            return nextItems;
        }

#if (PCL|| AWSSDK_UNITY)
        private void CallUntilCompletion(BatchWriteItemRequest request, Dictionary<string, Dictionary<Key, Document>> documentMap, AmazonDynamoDBClient client)
#else
        private void CallUntilCompletion(BatchWriteItemRequest request, Dictionary<string, Dictionary<Key, Document>> documentMap, IAmazonDynamoDB client)
#endif
        {
            do
            {
                var result = client.BatchWriteItem(request);
                request.RequestItems = result.UnprocessedItems;

                Dictionary<Key, Document> unprocessedDocuments = new Dictionary<Key, Document>(keyComparer);
                foreach (var unprocessedItems in result.UnprocessedItems)
                {
                    string tableName = unprocessedItems.Key;
                    Table table = tableMap[tableName];
                    Dictionary<Key, Document> tableDocumentMap = documentMap[tableName];

                    foreach (var writeRequest in unprocessedItems.Value)
                    {
                        if (writeRequest.PutRequest != null)
                        {
                            var key = table.MakeKey(Document.FromAttributeMap(writeRequest.PutRequest.Item));

                            Document document = null;
                            if (tableDocumentMap.TryGetValue(key, out document))
                            {
                                // Remove unprocessed requests from the document map 
                                // and copy them to unprocessed documents.
                                unprocessedDocuments.Add(key, document);
                                tableDocumentMap.Remove(key);
                            }
                        }
                    }

                    // Commit the remaining documents in the document map
                    foreach (var document in tableDocumentMap.Values)
                    {
                        document.CommitChanges();
                    }
                    // Replace existing documents with just the unprocessed documents
                    documentMap[tableName] = unprocessedDocuments;
                }

            } while (request.RequestItems.Count > 0);

            // Commit any remaining documents in document map.
            // This would only happen if we are not able to match the items sent in the request
            // with the items returned back as unprocessed items.
            foreach (var tableDocumentMap in documentMap.Values)
            {
                foreach (var document in tableDocumentMap.Values)
                {
                    document.CommitChanges();
                }
            }
        }

        private Dictionary<string, QuickList<WriteRequestDocument>> ConvertBatches(List<DocumentBatchWrite> batches)
        {
            Dictionary<string, QuickList<WriteRequestDocument>> result = new Dictionary<string, QuickList<WriteRequestDocument>>();

            foreach (var batch in batches)
            {
                var table = batch.TargetTable;
                var tableName = table.TableName;
                var conversion = table.Conversion;

                if (result.ContainsKey(tableName))
                    throw new AmazonDynamoDBException("More than one batch request against a single table is not supported.");

                List<WriteRequestDocument> writeRequests = new List<WriteRequestDocument>();
                if (batch.ToDelete != null)
                {
                    foreach (var toDelete in batch.ToDelete)
                        writeRequests.Add(new WriteRequestDocument
                        {
                            WriteRequest = new WriteRequest
                            {
                                DeleteRequest = new DeleteRequest { Key = toDelete }
                            }
                        });
                }
                if (batch.ToPut != null)
                {
                    foreach (var toPut in batch.ToPut)
                        writeRequests.Add(new WriteRequestDocument
                        {
                            WriteRequest = new WriteRequest
                            {
                                PutRequest = new PutRequest { Item = toPut.ToAttributeMap(conversion) }
                            },
                            Document = toPut
                        });
                }

                if (writeRequests.Count > 0)
                {
                    QuickList<WriteRequestDocument> qlWriteRequests = new QuickList<WriteRequestDocument>(writeRequests);
                    result.Add(tableName, qlWriteRequests);
                    tableMap.Add(tableName, batch.TargetTable);
                }
            }

            return result;
        }

        #endregion
    }

    internal class QuickList<T>
    {
        private List<T> List;
        private int StartIndex = 0;

        public QuickList(IEnumerable<T> items)
        {
            List = new List<T>(items);
        }

        public int Count
        {
            get
            {
                return List.Count - StartIndex;
            }
        }

        public IEnumerable<T> RemoveFromHead(int numberOfItems)
        {
            int boundedNumberOfItems = Math.Min(numberOfItems, Count);

            for (int i = 0; i < boundedNumberOfItems; i++)
            {
                yield return List[StartIndex];
                StartIndex++;
            }
        }

        public List<T> GetItems()
        {
            return List.GetRange(StartIndex, Count);
        }
    }

    internal class WriteRequestDocument
    {
        public WriteRequest WriteRequest { get; set; }

        public Document Document { get; set; }
    }

    internal class KeyComparer : IEqualityComparer<Key>
    {
        public bool Equals(Key x, Key y)
        {
            if (AreBothNull(x, y))
            {
                return true;
            }

            if (IsEitherNull(x, y))
            {
                return false;
            }

            // Objects are equal if both reference the same object.
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (x.Count != y.Count)
            {
                return false;
            }

            foreach (var item in x)
            {
                AttributeValue valueY = null;
                if (y.TryGetValue(item.Key, out valueY))
                {
                    if (!CompareAttributeValue(item.Value, valueY))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(Key key)
        {
            int hashCode = 0;
            foreach (var item in key)
            {
                hashCode ^= GetHashCode(item.Value);
            }
            return hashCode;
        }

        private static int GetHashCode(AttributeValue attributeValue)
        {
            if (attributeValue == null)
            {
                return 0;
            }

            if (attributeValue.S != null)
            {
                return attributeValue.S.GetHashCode();
            }

            if (attributeValue.N != null)
            {
                return attributeValue.N.GetHashCode();
            }

            if (attributeValue.B != null)
            {
                long xPos = attributeValue.B.Position;
                attributeValue.B.Position = 0;

                int hash = 0;
                int valueX = 0;
                do
                {
                    hash ^= valueX;
                    valueX = attributeValue.B.ReadByte();
                } while (valueX != -1);

                attributeValue.B.Position = xPos;
                return hash;
            }

            return 0;
        }

        private static bool CompareAttributeValue(AttributeValue x, AttributeValue y)
        {
            if (AreBothNull(x, y))
            {
                return true;
            }

            if (IsEitherNull(x, y))
            {
                return false;
            }

            // Compare scalar properties of primary attributes. Primary attributes can only be scalar types.
            // http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataModel.html 

            if (IsEitherNull(x.S, y.S) || IsEitherNull(x.N, y.N) || IsEitherNull(x.B, y.B))
            {
                return false;
            }

            if (!AreBothNull(x.S, y.S))
            {
                if (!x.S.Equals(y.S, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            if (!AreBothNull(x.N, y.N))
            {
                if (!x.N.Equals(y.N, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            if (!AreBothNull(x.B, y.B))
            {
                if (x.B.Length != y.B.Length)
                {
                    return false;
                }

                long xPos = x.B.Position, yPos = y.B.Position;
                x.B.Position = 0;
                y.B.Position = 0;

                int valueX = 0, valueY = 0;
                do
                {
                    if (valueX != valueY)
                    {
                        return false;
                    }
                    valueX = x.B.ReadByte();
                    valueY = y.B.ReadByte();

                } while (valueX != -1 && valueY != -1);

                x.B.Position = xPos;
                y.B.Position = yPos;
            }

            return true;
        }

        private static bool IsEitherNull<T>(T x, T y) where T : class
        {
            return ((x == null && y != null) || (x != null && y == null));
        }

        private static bool AreBothNull<T>(T x, T y) where T : class
        {
            return (x == null && y == null);
        }
    }
}
