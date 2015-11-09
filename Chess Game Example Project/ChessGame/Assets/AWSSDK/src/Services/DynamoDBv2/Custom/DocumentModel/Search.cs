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
using System.Globalization;

namespace Amazon.DynamoDBv2.DocumentModel
{
    /// <summary>
    /// Search response object
    /// </summary>
    public partial class Search
    {
        #region Internal constructors

        internal Search()
            : this((SearchType)0)
        {
        }

        internal Search(SearchType searchMethod)
        {
            SearchMethod = searchMethod;
            Reset();
        }

        #endregion


        #region Public properties

        /// <summary>
        /// Name of the table being searched
        /// </summary>
        public String TableName { get; internal set; }

        /// <summary>
        /// Whether to collect GetNextSet and GetRemaining results in Matches property.
        /// Default is true. If set to false, Matches will always be empty.
        /// </summary>
        public bool CollectResults { get; internal set; }

        /// <summary>
        /// Upper limit on the number of items returned.
        /// For Queries: upper limit on the number of items returned.
        /// For Scan: limit on the number of items scanned.
        /// 
        /// Maps directly to Limit property on Query and Scan operations.
        /// </summary>
        public int Limit { get; internal set; }

        /// <summary>
        /// The key expression that is evaluated for each item of a query.
        /// This applies only to Query operations.
        /// </summary>
        public Expression KeyExpression { get; set; }

        /// <summary>
        /// The filter expression that is evaluated for each item.
        /// This applies to Query and Scan operations.
        /// </summary>
        public Expression FilterExpression { get; set; }

        /// <summary>
        /// Filter for the search operation
        /// This applies to Query and Scan operations.
        /// </summary>
        public Filter Filter { get; internal set; }

        /// <summary>
        /// Conditional operator for the search operation
        /// </summary>
        public ConditionalOperatorValues ConditionalOperator { get; internal set; }

        /// <summary>
        /// List of attribute names to retrieve
        /// </summary>
        public List<String> AttributesToGet { get; internal set; }

        /// <summary>
        /// Flag that, if true, indicates that the search is operating in consistent-read mode
        /// </summary>
        public bool IsConsistentRead { get; internal set; }

        /// <summary>
        /// Flag that, if true, indicates that the search is traversing backwards
        /// </summary>
        public bool IsBackwardSearch { get; internal set; }

        /// <summary>
        /// Flag that, if true, indicates that the search is done
        /// </summary>
        public bool IsDone { get; private set; }

        /// <summary>
        /// Key denoting the starting point of the next set of results
        /// </summary>
        public Dictionary<string, AttributeValue> NextKey { get; private set; }

        /// <summary>
        /// List of currently found items
        /// </summary>
        public List<Document> Matches { get; private set; }

        /// <summary>
        /// For parallel <i>Scan</i> requests, <i>TotalSegments</i>represents the total number of segments for a table that is being scanned. Segments
        /// are a way to logically divide a table into equally sized portions, for the duration of the <i>Scan</i> request. The value of
        /// <i>TotalSegments</i> corresponds to the number of application "workers" (such as threads or processes) that will perform the parallel
        /// <i>Scan</i>. For example, if you want to scan a table using four application threads, you would specify a <i>TotalSegments</i> value of 4.
        /// The value for <i>TotalSegments</i> must be greater than or equal to 1, and less than or equal to 4096. If you specify a <i>TotalSegments</i>
        /// value of 1, the <i>Scan</i> will be sequential rather than parallel. If you specify <i>TotalSegments</i>, you must also specify
        /// <i>Segment</i>.
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Range</term>
        ///         <description>1 - 4096</description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public int TotalSegments { get; set; }

        /// <summary>
        /// For parallel <i>Scan</i> requests, <i>Segment</i> identifies an individual segment to be scanned by an application "worker" (such as a
        /// thread or a process). Each worker issues a <i>Scan</i> request with a distinct value for the segment it will scan. Segment IDs are
        /// zero-based, so the first segment is always 0. For example, if you want to scan a table using four application threads, the first thread
        /// would specify a <i>Segment</i> value of 0, the second thread would specify 1, and so on. LastEvaluatedKey returned from a parallel scan
        /// request must be used with same Segment id in a subsequent operation. The value for <i>Segment</i> must be less than or equal to 0, and less
        /// than the value provided for <i>TotalSegments</i>. If you specify <i>Segment</i>, you must also specify <i>TotalSegments</i>.
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Range</term>
        ///         <description>0 - 4095</description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public int Segment { get; set; }

        /// <summary>
        /// Gets the total number of items that match the search parameters.
        /// 
        /// If IsDone is true and CollectResults is true, returns Matches.Count.
        /// Otherwise, makes a call to DynamoDB to find out the number of
        /// matching items, without retrieving the items. Count is then cached.
        /// </summary>
        public int Count { get { return GetCount(); } }

        /// <summary>
        /// Name of the index to query or scan against.
        /// </summary>
        public string IndexName { get; internal set; }

        /// <summary>
        /// Enum specifying what data to return from query.
        /// </summary>
        public SelectValues Select { get; internal set; }

        #endregion


        #region Public methods

        internal List<Document> GetNextSetHelper(bool isAsync)
        {
            List<Document> ret = new List<Document>();

            if (!IsDone)
            {
                switch (SearchMethod)
                {
                    case SearchType.Scan:
                        ScanRequest scanReq = new ScanRequest
                        {
                            ExclusiveStartKey = NextKey,
                            Limit = Limit,
                            TableName = TableName,
                            AttributesToGet = AttributesToGet,
                            ScanFilter = Filter.ToConditions(SourceTable.Conversion),
                            Select = EnumMapper.Convert(Select),
                            ConsistentRead = IsConsistentRead
                        };
                        if (!string.IsNullOrEmpty(this.IndexName))
                            scanReq.IndexName = this.IndexName;
                        if (this.FilterExpression != null && this.FilterExpression.IsSet)
                            this.FilterExpression.ApplyExpression(scanReq, SourceTable.Conversion);
                        if (scanReq.ScanFilter != null && scanReq.ScanFilter.Count > 1)
                            scanReq.ConditionalOperator = EnumMapper.Convert(ConditionalOperator);
                        Common.ConvertAttributesToGetToProjectionExpression(scanReq);

                        if (this.TotalSegments != 0)
                        {
                            scanReq.TotalSegments = this.TotalSegments;
                            scanReq.Segment = this.Segment;
                        }

                        ((Amazon.Runtime.Internal.IAmazonWebServiceRequest)scanReq).AddBeforeRequestHandler(isAsync ?
                            new RequestEventHandler(SourceTable.UserAgentRequestEventHandlerAsync) :
                            new RequestEventHandler(SourceTable.UserAgentRequestEventHandlerSync)
                            );

                        var scanResult = SourceTable.DDBClient.Scan(scanReq);
                        foreach (var item in scanResult.Items)
                        {
                            Document doc = Document.FromAttributeMap(item);
                            ret.Add(doc);
                            if (CollectResults)
                            {
                                Matches.Add(doc);
                            }
                        }
                        NextKey = scanResult.LastEvaluatedKey;
                        if (NextKey == null || NextKey.Count == 0)
                        {
                            IsDone = true;
                        }
                        return ret;
                    case SearchType.Query:
                        QueryRequest queryReq = new QueryRequest
                        {
                            TableName = TableName,
                            ConsistentRead = IsConsistentRead,
                            Select = EnumMapper.Convert(Select),
                            ExclusiveStartKey = NextKey,
                            Limit = Limit,
                            ScanIndexForward = !IsBackwardSearch,
                            AttributesToGet = AttributesToGet,
                            IndexName = IndexName,
                        };
                        
                        
                        Expression.ApplyExpression(queryReq, SourceTable.Conversion, KeyExpression, FilterExpression);

                        Dictionary<string, Condition> keyConditions, filterConditions;
                        SplitQueryFilter(Filter, SourceTable, queryReq.IndexName, out keyConditions, out filterConditions);
                        queryReq.KeyConditions = keyConditions;
                        queryReq.QueryFilter = filterConditions;
                        Common.ConvertAttributesToGetToProjectionExpression(queryReq);

                        if (queryReq.QueryFilter != null && queryReq.QueryFilter.Count > 1)
                            queryReq.ConditionalOperator = EnumMapper.Convert(ConditionalOperator);

                        ((Amazon.Runtime.Internal.IAmazonWebServiceRequest)queryReq).AddBeforeRequestHandler(isAsync ?
                            new RequestEventHandler(SourceTable.UserAgentRequestEventHandlerAsync) :
                            new RequestEventHandler(SourceTable.UserAgentRequestEventHandlerSync)
                            );
                        var queryResult = SourceTable.DDBClient.Query(queryReq);
                        foreach (var item in queryResult.Items)
                        {
                            Document doc = Document.FromAttributeMap(item);
                            ret.Add(doc);
                            if (CollectResults)
                            {
                                Matches.Add(doc);
                            }
                        }
                        NextKey = queryResult.LastEvaluatedKey;
                        if (NextKey == null || NextKey.Count == 0)
                        {
                            IsDone = true;
                        }
                        return ret;
                    default:
                        throw new InvalidOperationException("Unknown Search Method");
                }
            }

            return ret;
        }

        internal List<Document> GetRemainingHelper(bool isAsync)
        {
            List<Document> ret = new List<Document>();

            while (!IsDone)
            {
                foreach (Document doc in GetNextSetHelper(isAsync))
                {
                    ret.Add(doc);
                }
            }

            return ret;
        }

        #endregion


        #region Private/internal properties

        private int count;

        private SearchType SearchMethod { get; set; }

        internal Table SourceTable { get; set; }

        private static void SplitQueryFilter(Filter filter, Table targetTable, string indexName, out Dictionary<string, Condition> keyConditions, out Dictionary<string, Condition> filterConditions)
        {
            QueryFilter queryFilter = filter as QueryFilter;
            if (queryFilter == null) throw new InvalidOperationException("Filter is not of type QueryFilter");

            keyConditions = new Dictionary<string, Condition>();
            filterConditions = new Dictionary<string, Condition>();

            var conditions = filter.ToConditions(targetTable.Conversion);
            foreach (var kvp in conditions)
            {
                string attributeName = kvp.Key;
                Condition condition = kvp.Value;

                // depending on whether the attribute is key, place either in keyConditions or filterConditions
                if (IsKeyAttribute(targetTable, indexName, attributeName))
                    keyConditions[attributeName] = condition;
                else
                    filterConditions[attributeName] = condition;
            }
        }

        // Test if the given attribute is a key on the table or a key on the given index
        private static bool IsKeyAttribute(Table table, string indexName, string attributeName)
        {
            GlobalSecondaryIndexDescription gsi;
            LocalSecondaryIndexDescription lsi;

            // if no index, check only table keys
            if (string.IsNullOrEmpty(indexName))
                return table.Keys.ContainsKey(attributeName);
            // for an index, check if attribute is part of KeySchema for GSI or LSI
            else if (table.GlobalSecondaryIndexes.TryGetValue(indexName, out gsi) && gsi != null)
                return gsi.KeySchema.Any(AttributeIsKey(attributeName));
            else if (table.LocalSecondaryIndexes.TryGetValue(indexName, out lsi) && lsi != null)
                return lsi.KeySchema.Any(AttributeIsKey(attributeName));
            else
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                    "Unable to locate index [{0}] on table [{1}]", indexName, table.TableName));
        }

        private static Func<KeySchemaElement, bool> AttributeIsKey(string attributeName)
        {
            return (kse => string.Equals(kse.AttributeName, attributeName, StringComparison.Ordinal));
        }

        private int GetCount()
        {
            if (IsDone && CollectResults)
            {
                return Matches.Count;
            }
            else
            {
                if (count != -1)
                {
                    return count;
                }
                else
                {
                    switch (SearchMethod)
                    {
                        case SearchType.Scan:
                            ScanRequest scanReq = new ScanRequest
                            {
                                TableName = TableName,
                                Select = EnumMapper.Convert(SelectValues.Count),
                                ExclusiveStartKey = NextKey,
                                ScanFilter = Filter.ToConditions(SourceTable.Conversion),
                                ConsistentRead = IsConsistentRead
                            };
                            if (!string.IsNullOrEmpty(this.IndexName))
                                scanReq.IndexName = this.IndexName;
                            if (this.FilterExpression != null && this.FilterExpression.IsSet)
                                this.FilterExpression.ApplyExpression(scanReq, SourceTable.Conversion);
                            if (scanReq.ScanFilter != null && scanReq.ScanFilter.Count > 1)
                                scanReq.ConditionalOperator = EnumMapper.Convert(ConditionalOperator);

                            if (this.TotalSegments != 0)
                            {
                                scanReq.TotalSegments = this.TotalSegments;
                                scanReq.Segment = this.Segment;
                            }
                            ((Amazon.Runtime.Internal.IAmazonWebServiceRequest)scanReq).AddBeforeRequestHandler(SourceTable.UserAgentRequestEventHandlerSync);
                            var scanResult = SourceTable.DDBClient.Scan(scanReq);
                            count = Matches.Count + scanResult.Count;
                            return count;
                        case SearchType.Query:
                            QueryRequest queryReq = new QueryRequest
                            {
                                TableName = TableName,
                                ConsistentRead = IsConsistentRead,
                                Select = EnumMapper.Convert(SelectValues.Count),
                                ExclusiveStartKey = NextKey,
                                ScanIndexForward = !IsBackwardSearch,
                                IndexName = IndexName
                            };

                            Expression.ApplyExpression(queryReq, SourceTable.Conversion, KeyExpression, FilterExpression);
                            Dictionary<string, Condition> keyConditions, filterConditions;
                            SplitQueryFilter(Filter, SourceTable, queryReq.IndexName, out keyConditions, out filterConditions);
                            queryReq.KeyConditions = keyConditions;
                            queryReq.QueryFilter = filterConditions;

                            if (queryReq.QueryFilter != null && queryReq.QueryFilter.Count > 1)
                                queryReq.ConditionalOperator = EnumMapper.Convert(ConditionalOperator);

                            ((Amazon.Runtime.Internal.IAmazonWebServiceRequest)queryReq).AddBeforeRequestHandler(SourceTable.UserAgentRequestEventHandlerSync);
                            var queryResult = SourceTable.DDBClient.Query(queryReq);
                            count = Matches.Count + queryResult.Count;
                            return count;
                        default:
                            throw new InvalidOperationException("Unknown Search Method");
                    }
                }
            }
        }

        /// <summary>
        /// Resets the Search object to default state.
        /// </summary>
        internal void Reset()
        {
            count = -1;
            IsDone = false;
            NextKey = null;
            Matches = new List<Document>();
            CollectResults = true;
        }

        #endregion
    }
}
