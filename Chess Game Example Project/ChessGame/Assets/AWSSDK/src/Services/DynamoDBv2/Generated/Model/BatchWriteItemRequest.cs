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

/*
 * Do not modify this file. This file is generated from the dynamodb-2012-08-10.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.DynamoDBv2.Model
{
    /// <summary>
    /// Container for the parameters to the BatchWriteItem operation.
    /// The <i>BatchWriteItem</i> operation puts or deletes multiple items in one or more
    /// tables. A single call to <i>BatchWriteItem</i> can write up to 16 MB of data, which
    /// can comprise as many as 25 put or delete requests. Individual items to be written
    /// can be as large as 400 KB.
    /// 
    ///  <note> 
    /// <para>
    /// <i>BatchWriteItem</i> cannot update items. To update items, use the <i>UpdateItem</i>
    /// API.
    /// </para>
    ///  </note> 
    /// <para>
    /// The individual <i>PutItem</i> and <i>DeleteItem</i> operations specified in <i>BatchWriteItem</i>
    /// are atomic; however <i>BatchWriteItem</i> as a whole is not. If any requested operations
    /// fail because the table's provisioned throughput is exceeded or an internal processing
    /// failure occurs, the failed operations are returned in the <i>UnprocessedItems</i>
    /// response parameter. You can investigate and optionally resend the requests. Typically,
    /// you would call <i>BatchWriteItem</i> in a loop. Each iteration would check for unprocessed
    /// items and submit a new <i>BatchWriteItem</i> request with those unprocessed items
    /// until all items have been processed.
    /// </para>
    ///  
    /// <para>
    /// Note that if <i>none</i> of the items can be processed due to insufficient provisioned
    /// throughput on all of the tables in the request, then <i>BatchWriteItem</i> will return
    /// a <i>ProvisionedThroughputExceededException</i>.
    /// </para>
    ///  <important> 
    /// <para>
    /// If DynamoDB returns any unprocessed items, you should retry the batch operation on
    /// those items. However, <i>we strongly recommend that you use an exponential backoff
    /// algorithm</i>. If you retry the batch operation immediately, the underlying read or
    /// write requests can still fail due to throttling on the individual tables. If you delay
    /// the batch operation using exponential backoff, the individual requests in the batch
    /// are much more likely to succeed.
    /// </para>
    ///  
    /// <para>
    /// For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#BatchOperations">Batch
    /// Operations and Error Handling</a> in the <i>Amazon DynamoDB Developer Guide</i>.
    /// </para>
    ///  </important> 
    /// <para>
    /// With <i>BatchWriteItem</i>, you can efficiently write or delete large amounts of data,
    /// such as from Amazon Elastic MapReduce (EMR), or copy data from another database into
    /// DynamoDB. In order to improve performance with these large-scale operations, <i>BatchWriteItem</i>
    /// does not behave in the same way as individual <i>PutItem</i> and <i>DeleteItem</i>
    /// calls would. For example, you cannot specify conditions on individual put and delete
    /// requests, and <i>BatchWriteItem</i> does not return deleted items in the response.
    /// </para>
    ///  
    /// <para>
    /// If you use a programming language that supports concurrency, you can use threads to
    /// write items in parallel. Your application must include the necessary logic to manage
    /// the threads. With languages that don't support threading, you must update or delete
    /// the specified items one at a time. In both situations, <i>BatchWriteItem</i> provides
    /// an alternative where the API performs the specified put and delete operations in parallel,
    /// giving you the power of the thread pool approach without having to introduce complexity
    /// into your application.
    /// </para>
    ///  
    /// <para>
    /// Parallel processing reduces latency, but each specified put and delete request consumes
    /// the same number of write capacity units whether it is processed in parallel or not.
    /// Delete operations on nonexistent items consume one write capacity unit.
    /// </para>
    ///  
    /// <para>
    /// If one or more of the following is true, DynamoDB rejects the entire batch write operation:
    /// </para>
    ///  <ul> <li> 
    /// <para>
    /// One or more tables specified in the <i>BatchWriteItem</i> request does not exist.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// Primary key attributes specified on an item in the request do not match those in the
    /// corresponding table's primary key schema.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// You try to perform multiple operations on the same item in the same <i>BatchWriteItem</i>
    /// request. For example, you cannot put and delete the same item in the same <i>BatchWriteItem</i>
    /// request. 
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// There are more than 25 requests in the batch.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// Any individual item in a batch exceeds 400 KB.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// The total request size exceeds 16 MB.
    /// </para>
    ///  </li> </ul>
    /// </summary>
    public partial class BatchWriteItemRequest : AmazonDynamoDBRequest
    {
        private Dictionary<string, List<WriteRequest>> _requestItems = new Dictionary<string, List<WriteRequest>>();
        private ReturnConsumedCapacity _returnConsumedCapacity;
        private ReturnItemCollectionMetrics _returnItemCollectionMetrics;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public BatchWriteItemRequest() { }

        /// <summary>
        /// Instantiates BatchWriteItemRequest with the parameterized properties
        /// </summary>
        /// <param name="requestItems">A map of one or more table names and, for each table, a list of operations to be performed (<i>DeleteRequest</i> or <i>PutRequest</i>). Each element in the map consists of the following: <ul> <li> <i>DeleteRequest</i> - Perform a <i>DeleteItem</i> operation on the specified item. The item to be deleted is identified by a <i>Key</i> subelement: <ul> <li> <i>Key</i> - A map of primary key attribute values that uniquely identify the ! item. Each entry in this map consists of an attribute name and an attribute value. For each primary key, you must provide <i>all</i> of the key attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide <i>both</i> the hash attribute and the range attribute. </li> </ul> </li> <li> <i>PutRequest</i> - Perform a <i>PutItem</i> operation on the specified item. The item to be put is identified by an <i>Item</i> subelement: <ul> <li> <i>Item</i> - A map of attributes and their values. Each entry in this map consists of an attribute name and an attribute value. Attribute values must not be null; string and binary type attributes must have lengths greater than zero; and set type attributes must not be empty. Requests that contain empty values will be rejected with a <i>ValidationException</i> exception. If you specify any attributes that are part of an index key, then the data types for those attributes must match those of the schema in the table's attribute definition. </li> </ul> </li> </ul></param>
        public BatchWriteItemRequest(Dictionary<string, List<WriteRequest>> requestItems)
        {
            _requestItems = requestItems;
        }

        /// <summary>
        /// Gets and sets the property RequestItems. 
        /// <para>
        /// A map of one or more table names and, for each table, a list of operations to be performed
        /// (<i>DeleteRequest</i> or <i>PutRequest</i>). Each element in the map consists of the
        /// following:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>DeleteRequest</i> - Perform a <i>DeleteItem</i> operation on the specified item.
        /// The item to be deleted is identified by a <i>Key</i> subelement:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>Key</i> - A map of primary key attribute values that uniquely identify the ! item.
        /// Each entry in this map consists of an attribute name and an attribute value. For each
        /// primary key, you must provide <i>all</i> of the key attributes. For example, with
        /// a hash type primary key, you only need to provide the hash attribute. For a hash-and-range
        /// type primary key, you must provide <i>both</i> the hash attribute and the range attribute.
        /// </para>
        ///  </li> </ul> </li> <li> 
        /// <para>
        /// <i>PutRequest</i> - Perform a <i>PutItem</i> operation on the specified item. The
        /// item to be put is identified by an <i>Item</i> subelement:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>Item</i> - A map of attributes and their values. Each entry in this map consists
        /// of an attribute name and an attribute value. Attribute values must not be null; string
        /// and binary type attributes must have lengths greater than zero; and set type attributes
        /// must not be empty. Requests that contain empty values will be rejected with a <i>ValidationException</i>
        /// exception.
        /// </para>
        ///  
        /// <para>
        /// If you specify any attributes that are part of an index key, then the data types for
        /// those attributes must match those of the schema in the table's attribute definition.
        /// </para>
        ///  </li> </ul> </li> </ul>
        /// </summary>
        public Dictionary<string, List<WriteRequest>> RequestItems
        {
            get { return this._requestItems; }
            set { this._requestItems = value; }
        }

        // Check to see if RequestItems property is set
        internal bool IsSetRequestItems()
        {
            return this._requestItems != null && this._requestItems.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property ReturnConsumedCapacity.
        /// </summary>
        public ReturnConsumedCapacity ReturnConsumedCapacity
        {
            get { return this._returnConsumedCapacity; }
            set { this._returnConsumedCapacity = value; }
        }

        // Check to see if ReturnConsumedCapacity property is set
        internal bool IsSetReturnConsumedCapacity()
        {
            return this._returnConsumedCapacity != null;
        }

        /// <summary>
        /// Gets and sets the property ReturnItemCollectionMetrics. 
        /// <para>
        /// Determines whether item collection metrics are returned. If set to <code>SIZE</code>,
        /// the response includes statistics about item collections, if any, that were modified
        /// during the operation are returned in the response. If set to <code>NONE</code> (the
        /// default), no statistics are returned.
        /// </para>
        /// </summary>
        public ReturnItemCollectionMetrics ReturnItemCollectionMetrics
        {
            get { return this._returnItemCollectionMetrics; }
            set { this._returnItemCollectionMetrics = value; }
        }

        // Check to see if ReturnItemCollectionMetrics property is set
        internal bool IsSetReturnItemCollectionMetrics()
        {
            return this._returnItemCollectionMetrics != null;
        }

    }
}