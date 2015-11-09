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

using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.DynamoDBv2
{
    /// <summary>
    /// Implementation for accessing DynamoDB
    ///
    /// Amazon DynamoDB 
    /// <para>
    ///  <b>Overview</b>
    /// </para>
    ///  
    /// <para>
    /// This is the Amazon DynamoDB API Reference. This guide provides descriptions and samples
    /// of the low-level DynamoDB API. For information about DynamoDB application development,
    /// see the <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/">Amazon
    /// DynamoDB Developer Guide</a>.
    /// </para>
    ///  
    /// <para>
    /// Instead of making the requests to the low-level DynamoDB API directly from your application,
    /// we recommend that you use the AWS Software Development Kits (SDKs). The easy-to-use
    /// libraries in the AWS SDKs make it unnecessary to call the low-level DynamoDB API directly
    /// from your application. The libraries take care of request authentication, serialization,
    /// and connection management. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/UsingAWSSDK.html">Using
    /// the AWS SDKs with DynamoDB</a> in the <i>Amazon DynamoDB Developer Guide</i>.
    /// </para>
    ///  
    /// <para>
    /// If you decide to code against the low-level DynamoDB API directly, you will need to
    /// write the necessary code to authenticate your requests. For more information on signing
    /// your requests, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/API.html">Using
    /// the DynamoDB API</a> in the <i>Amazon DynamoDB Developer Guide</i>.
    /// </para>
    ///  
    /// <para>
    /// The following are short descriptions of each low-level API action, organized by function.
    /// </para>
    ///  
    /// <para>
    ///  <b>Managing Tables</b> 
    /// </para>
    ///  <ul> <li> 
    /// <para>
    /// <i>CreateTable</i> - Creates a table with user-specified provisioned throughput settings.
    /// You must designate one attribute as the hash primary key for the table; you can optionally
    /// designate a second attribute as the range primary key. DynamoDB creates indexes on
    /// these key attributes for fast data access. Optionally, you can create one or more
    /// secondary indexes, which provide fast data access using non-key attributes.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// <i>DescribeTable</i> - Returns metadata for a table, such as table size, status, and
    /// index information.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// <i>UpdateTable</i> - Modifies the provisioned throughput settings for a table. Optionally,
    /// you can modify the provisioned throughput settings for global secondary indexes on
    /// the table.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// <i>ListTables</i> - Returns a list of all tables associated with the current AWS account
    /// and endpoint.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// <i>DeleteTable</i> - Deletes a table and all of its indexes.
    /// </para>
    ///  </li> </ul> 
    /// <para>
    /// For conceptual information about managing tables, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html">Working
    /// with Tables</a> in the <i>Amazon DynamoDB Developer Guide</i>.
    /// </para>
    ///  
    /// <para>
    ///  <b>Reading Data</b> 
    /// </para>
    ///  <ul> <li> 
    /// <para>
    /// <i>GetItem</i> - Returns a set of attributes for the item that has a given primary
    /// key. By default, <i>GetItem</i> performs an eventually consistent read; however, applications
    /// can request a strongly consistent read instead.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// <i>BatchGetItem</i> - Performs multiple <i>GetItem</i> requests for data items using
    /// their primary keys, from one table or multiple tables. The response from <i>BatchGetItem</i>
    /// has a size limit of 16 MB and returns a maximum of 100 items. Both eventually consistent
    /// and strongly consistent reads can be used.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// <i>Query</i> - Returns one or more items from a table or a secondary index. You must
    /// provide a specific hash key value. You can narrow the scope of the query using comparison
    /// operators against a range key value, or on the index key. <i>Query</i> supports either
    /// eventual or strong consistency. A single response has a size limit of 1 MB.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// <i>Scan</i> - Reads every item in a table; the result set is eventually consistent.
    /// You can limit the number of items returned by filtering the data attributes, using
    /// conditional expressions. <i>Scan</i> can be used to enable ad-hoc querying of a table
    /// against non-key attributes; however, since this is a full table scan without using
    /// an index, <i>Scan</i> should not be used for any application query use case that requires
    /// predictable performance.
    /// </para>
    ///  </li> </ul> 
    /// <para>
    /// For conceptual information about reading data, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithItems.html">Working
    /// with Items</a> and <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/QueryAndScan.html">Query
    /// and Scan Operations</a> in the <i>Amazon DynamoDB Developer Guide</i>.
    /// </para>
    ///  
    /// <para>
    ///  <b>Modifying Data</b> 
    /// </para>
    ///  <ul> <li> 
    /// <para>
    /// <i>PutItem</i> - Creates a new item, or replaces an existing item with a new item
    /// (including all the attributes). By default, if an item in the table already exists
    /// with the same primary key, the new item completely replaces the existing item. You
    /// can use conditional operators to replace an item only if its attribute values match
    /// certain conditions, or to insert a new item only if that item doesn't already exist.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// <i>UpdateItem</i> - Modifies the attributes of an existing item. You can also use
    /// conditional operators to perform an update only if the item's attribute values match
    /// certain conditions.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// <i>DeleteItem</i> - Deletes an item in a table by primary key. You can use conditional
    /// operators to perform a delete an item only if the item's attribute values match certain
    /// conditions.
    /// </para>
    ///  </li> <li> 
    /// <para>
    /// <i>BatchWriteItem</i> - Performs multiple <i>PutItem</i> and <i>DeleteItem</i> requests
    /// across multiple tables in a single request. A failure of any request(s) in the batch
    /// will not cause the entire <i>BatchWriteItem</i> operation to fail. Supports batches
    /// of up to 25 items to put or delete, with a maximum total request size of 16 MB. 
    /// </para>
    ///  </li> </ul> 
    /// <para>
    /// For conceptual information about modifying data, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithItems.html">Working
    /// with Items</a> and <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/QueryAndScan.html">Query
    /// and Scan Operations</a> in the <i>Amazon DynamoDB Developer Guide</i>.
    /// </para>
    /// </summary>
    public partial class AmazonDynamoDBClient : AmazonUnityServiceClient, IAmazonDynamoDB
    {
        #region Constructors

        /// <summary>
        /// Constructs AmazonDynamoDBClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        public AmazonDynamoDBClient(AWSCredentials credentials)
            : this(credentials, new AmazonDynamoDBConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonDynamoDBClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="region">The region to connect.</param>
        public AmazonDynamoDBClient(AWSCredentials credentials, RegionEndpoint region)
            : this(credentials, new AmazonDynamoDBConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonDynamoDBClient with AWS Credentials and an
        /// AmazonDynamoDBClient Configuration object.
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="clientConfig">The AmazonDynamoDBClient Configuration Object</param>
        public AmazonDynamoDBClient(AWSCredentials credentials, AmazonDynamoDBConfig clientConfig)
            : base(credentials, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonDynamoDBClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        public AmazonDynamoDBClient(string awsAccessKeyId, string awsSecretAccessKey)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonDynamoDBConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonDynamoDBClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="region">The region to connect.</param>
        public AmazonDynamoDBClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonDynamoDBConfig() {RegionEndpoint=region})
        {
        }

        /// <summary>
        /// Constructs AmazonDynamoDBClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonDynamoDBClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="clientConfig">The AmazonDynamoDBClient Configuration Object</param>
        public AmazonDynamoDBClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonDynamoDBConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonDynamoDBClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        public AmazonDynamoDBClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonDynamoDBConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonDynamoDBClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="region">The region to connect.</param>
        public AmazonDynamoDBClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonDynamoDBConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonDynamoDBClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonDynamoDBClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="clientConfig">The AmazonDynamoDBClient Configuration Object</param>
        public AmazonDynamoDBClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, AmazonDynamoDBConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, clientConfig)
        {
        }

        #endregion

        #region Overrides

        protected override AbstractAWSSigner CreateSigner()
        {
            return new AWS4Signer();
        }

        protected override void CustomizeRuntimePipeline(RuntimePipeline pipeline)
        {
            pipeline.ReplaceHandler<Amazon.Runtime.Internal.ErrorHandler>(new Amazon.Runtime.Internal.ErrorHandler<AmazonDynamoDBException>(this.Logger));
            pipeline.ReplaceHandler<Amazon.Runtime.Internal.RetryHandler>(new Amazon.Runtime.Internal.RetryHandler(new Amazon.DynamoDBv2.Internal.DynamoDBRetryPolicy(this.Config.MaxErrorRetry)));
        }    

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

        
        #region  BatchGetItem

        /// <summary>
        /// The <i>BatchGetItem</i> operation returns the attributes of one or more items from
        /// one or more tables. You identify requested items by primary key.
        /// 
        ///  
        /// <para>
        /// A single operation can retrieve up to 16 MB of data, which can contain as many as
        /// 100 items. <i>BatchGetItem</i> will return a partial result if the response size limit
        /// is exceeded, the table's provisioned throughput is exceeded, or an internal processing
        /// failure occurs. If a partial result is returned, the operation returns a value for
        /// <i>UnprocessedKeys</i>. You can use this value to retry the operation starting with
        /// the next item to get.
        /// </para>
        ///  <important>
        /// <para>
        /// If you request more than 100 items <i>BatchGetItem</i> will return a <i>ValidationException</i>
        /// with the message "Too many items requested for the BatchGetItem call".
        /// </para>
        /// </important> 
        /// <para>
        /// For example, if you ask to retrieve 100 items, but each individual item is 300 KB
        /// in size, the system returns 52 items (so as not to exceed the 16 MB limit). It also
        /// returns an appropriate <i>UnprocessedKeys</i> value so you can get the next page of
        /// results. If desired, your application can include its own logic to assemble the pages
        /// of results into one data set.
        /// </para>
        ///  
        /// <para>
        /// If <i>none</i> of the items can be processed due to insufficient provisioned throughput
        /// on all of the tables in the request, then <i>BatchGetItem</i> will return a <i>ProvisionedThroughputExceededException</i>.
        /// If <i>at least one</i> of the items is successfully processed, then <i>BatchGetItem</i>
        /// completes successfully, while returning the keys of the unread items in <i>UnprocessedKeys</i>.
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
        /// By default, <i>BatchGetItem</i> performs eventually consistent reads on every table
        /// in the request. If you want strongly consistent reads instead, you can set <i>ConsistentRead</i>
        /// to <code>true</code> for any or all tables.
        /// </para>
        ///  
        /// <para>
        /// In order to minimize response latency, <i>BatchGetItem</i> retrieves items in parallel.
        /// </para>
        ///  
        /// <para>
        /// When designing your application, keep in mind that DynamoDB does not return attributes
        /// in any particular order. To help parse the response by item, include the primary key
        /// values for the items in your request in the <i>AttributesToGet</i> parameter.
        /// </para>
        ///  
        /// <para>
        /// If a requested item does not exist, it is not returned in the result. Requests for
        /// nonexistent items consume the minimum read capacity units according to the type of
        /// read. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#CapacityUnitCalculations">Capacity
        /// Units Calculations</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        /// 
        /// <returns>The response from the BatchGetItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal BatchGetItemResponse BatchGetItem(BatchGetItemRequest request)
        {
            var marshaller = new BatchGetItemRequestMarshaller();
            var unmarshaller = BatchGetItemResponseUnmarshaller.Instance;

            return Invoke<BatchGetItemRequest,BatchGetItemResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// The <i>BatchGetItem</i> operation returns the attributes of one or more items from
        /// one or more tables. You identify requested items by primary key.
        /// 
        ///  
        /// <para>
        /// A single operation can retrieve up to 16 MB of data, which can contain as many as
        /// 100 items. <i>BatchGetItem</i> will return a partial result if the response size limit
        /// is exceeded, the table's provisioned throughput is exceeded, or an internal processing
        /// failure occurs. If a partial result is returned, the operation returns a value for
        /// <i>UnprocessedKeys</i>. You can use this value to retry the operation starting with
        /// the next item to get.
        /// </para>
        ///  <important>
        /// <para>
        /// If you request more than 100 items <i>BatchGetItem</i> will return a <i>ValidationException</i>
        /// with the message "Too many items requested for the BatchGetItem call".
        /// </para>
        /// </important> 
        /// <para>
        /// For example, if you ask to retrieve 100 items, but each individual item is 300 KB
        /// in size, the system returns 52 items (so as not to exceed the 16 MB limit). It also
        /// returns an appropriate <i>UnprocessedKeys</i> value so you can get the next page of
        /// results. If desired, your application can include its own logic to assemble the pages
        /// of results into one data set.
        /// </para>
        ///  
        /// <para>
        /// If <i>none</i> of the items can be processed due to insufficient provisioned throughput
        /// on all of the tables in the request, then <i>BatchGetItem</i> will return a <i>ProvisionedThroughputExceededException</i>.
        /// If <i>at least one</i> of the items is successfully processed, then <i>BatchGetItem</i>
        /// completes successfully, while returning the keys of the unread items in <i>UnprocessedKeys</i>.
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
        /// By default, <i>BatchGetItem</i> performs eventually consistent reads on every table
        /// in the request. If you want strongly consistent reads instead, you can set <i>ConsistentRead</i>
        /// to <code>true</code> for any or all tables.
        /// </para>
        ///  
        /// <para>
        /// In order to minimize response latency, <i>BatchGetItem</i> retrieves items in parallel.
        /// </para>
        ///  
        /// <para>
        /// When designing your application, keep in mind that DynamoDB does not return attributes
        /// in any particular order. To help parse the response by item, include the primary key
        /// values for the items in your request in the <i>AttributesToGet</i> parameter.
        /// </para>
        ///  
        /// <para>
        /// If a requested item does not exist, it is not returned in the result. Requests for
        /// nonexistent items consume the minimum read capacity units according to the type of
        /// read. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#CapacityUnitCalculations">Capacity
        /// Units Calculations</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        /// <param name="requestItems">A map of one or more table names and, for each table, a map that describes one or more items to retrieve from that table. Each table name can be used only once per <i>BatchGetItem</i> request. Each element in the map of items to retrieve consists of the following: <ul> <li> <i>ConsistentRead</i> - If <code>true</code>, a strongly consistent read is used; if <code>false</code> (the default), an eventually consistent read is used. </li> <li>  <i>ExpressionAttributeNames</i> - One or more substitution tokens for attribute names in the <i>ProjectionExpression</i> parameter. The following are some use cases for using <i>ExpressionAttributeNames</i>: <ul> <li> To access an attribute whose name conflicts with a DynamoDB reserved word. </li> <li> To create a placeholder for repeating occurrences of an attribute name in an expression. </li> <li> To prevent special characters in an attribute name from being misinterpreted in an expression. </li> </ul> Use the <b>#</b> character in an expression to dereference an attribute name. For example, consider the following attribute name: <ul><li><code>Percentile</code></li></ul> The name of this attribute conflicts with a reserved word, so it cannot be used directly in an expression. (For the complete list of reserved words, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ReservedWords.html">Reserved Words</a> in the <i>Amazon DynamoDB Developer Guide</i>). To work around this, you could specify the following for <i>ExpressionAttributeNames</i>: <ul><li><code>{"#P":"Percentile"}</code></li></ul> You could then use this substitution in an expression, as in this example: <ul><li><code>#P = :val</code></li></ul> <note>Tokens that begin with the <b>:</b> character are <i>expression attribute values</i>, which are placeholders for the actual value at runtime.</note> For more information on expression attribute names, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.AccessingItemAttributes.html">Accessing Item Attributes</a> in the <i>Amazon DynamoDB Developer Guide</i>. </li> <li> <i>Keys</i> - An array of primary key attribute values that define specific items in the table. For each primary key, you must provide <i>all</i> of the key attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide <i>both</i> the hash attribute and the range attribute. </li> <li> <i>ProjectionExpression</i> - A string that identifies one or more attributes to retrieve from the table. These attributes can include scalars, sets, or elements of a JSON document. The attributes in the expression must be separated by commas. If no attribute names are specified, then all attributes will be returned. If any of the requested attributes are not found, they will not appear in the result. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.AccessingItemAttributes.html">Accessing Item Attributes</a> in the <i>Amazon DynamoDB Developer Guide</i>. </li> <li>  <i>AttributesToGet</i> -  <important> This is a legacy parameter, for backward compatibility. New applications should use <i>ProjectionExpression</i> instead. Do not combine legacy parameters and expression parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i> exception. This parameter allows you to retrieve attributes of type List or Map; however, it cannot retrieve individual elements within a List or a Map.</important> The names of one or more attributes to retrieve. If no attribute names are provided, then all attributes will be returned. If any of the requested attributes are not found, they will not appear in the result. Note that <i>AttributesToGet</i> has no effect on provisioned throughput consumption. DynamoDB determines capacity units consumed based on item size, not on the amount of data that is returned to an application. </li> </ul></param>
        /// <param name="returnConsumedCapacity">A property of BatchGetItemRequest used to execute the BatchGetItem service method.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the BatchGetItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void BatchGetItemAsync(Dictionary<string, KeysAndAttributes> requestItems, ReturnConsumedCapacity returnConsumedCapacity,  AmazonServiceCallback<BatchGetItemRequest, BatchGetItemResponse> callback, AsyncOptions options = null)
        {
            var request = new BatchGetItemRequest();
            request.RequestItems = requestItems;
            request.ReturnConsumedCapacity = returnConsumedCapacity;
            BatchGetItemAsync(request, callback, options);
        }


        /// <summary>
        /// The <i>BatchGetItem</i> operation returns the attributes of one or more items from
        /// one or more tables. You identify requested items by primary key.
        /// 
        ///  
        /// <para>
        /// A single operation can retrieve up to 16 MB of data, which can contain as many as
        /// 100 items. <i>BatchGetItem</i> will return a partial result if the response size limit
        /// is exceeded, the table's provisioned throughput is exceeded, or an internal processing
        /// failure occurs. If a partial result is returned, the operation returns a value for
        /// <i>UnprocessedKeys</i>. You can use this value to retry the operation starting with
        /// the next item to get.
        /// </para>
        ///  <important>
        /// <para>
        /// If you request more than 100 items <i>BatchGetItem</i> will return a <i>ValidationException</i>
        /// with the message "Too many items requested for the BatchGetItem call".
        /// </para>
        /// </important> 
        /// <para>
        /// For example, if you ask to retrieve 100 items, but each individual item is 300 KB
        /// in size, the system returns 52 items (so as not to exceed the 16 MB limit). It also
        /// returns an appropriate <i>UnprocessedKeys</i> value so you can get the next page of
        /// results. If desired, your application can include its own logic to assemble the pages
        /// of results into one data set.
        /// </para>
        ///  
        /// <para>
        /// If <i>none</i> of the items can be processed due to insufficient provisioned throughput
        /// on all of the tables in the request, then <i>BatchGetItem</i> will return a <i>ProvisionedThroughputExceededException</i>.
        /// If <i>at least one</i> of the items is successfully processed, then <i>BatchGetItem</i>
        /// completes successfully, while returning the keys of the unread items in <i>UnprocessedKeys</i>.
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
        /// By default, <i>BatchGetItem</i> performs eventually consistent reads on every table
        /// in the request. If you want strongly consistent reads instead, you can set <i>ConsistentRead</i>
        /// to <code>true</code> for any or all tables.
        /// </para>
        ///  
        /// <para>
        /// In order to minimize response latency, <i>BatchGetItem</i> retrieves items in parallel.
        /// </para>
        ///  
        /// <para>
        /// When designing your application, keep in mind that DynamoDB does not return attributes
        /// in any particular order. To help parse the response by item, include the primary key
        /// values for the items in your request in the <i>AttributesToGet</i> parameter.
        /// </para>
        ///  
        /// <para>
        /// If a requested item does not exist, it is not returned in the result. Requests for
        /// nonexistent items consume the minimum read capacity units according to the type of
        /// read. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#CapacityUnitCalculations">Capacity
        /// Units Calculations</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        /// <param name="requestItems">A map of one or more table names and, for each table, a map that describes one or more items to retrieve from that table. Each table name can be used only once per <i>BatchGetItem</i> request. Each element in the map of items to retrieve consists of the following: <ul> <li> <i>ConsistentRead</i> - If <code>true</code>, a strongly consistent read is used; if <code>false</code> (the default), an eventually consistent read is used. </li> <li>  <i>ExpressionAttributeNames</i> - One or more substitution tokens for attribute names in the <i>ProjectionExpression</i> parameter. The following are some use cases for using <i>ExpressionAttributeNames</i>: <ul> <li> To access an attribute whose name conflicts with a DynamoDB reserved word. </li> <li> To create a placeholder for repeating occurrences of an attribute name in an expression. </li> <li> To prevent special characters in an attribute name from being misinterpreted in an expression. </li> </ul> Use the <b>#</b> character in an expression to dereference an attribute name. For example, consider the following attribute name: <ul><li><code>Percentile</code></li></ul> The name of this attribute conflicts with a reserved word, so it cannot be used directly in an expression. (For the complete list of reserved words, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ReservedWords.html">Reserved Words</a> in the <i>Amazon DynamoDB Developer Guide</i>). To work around this, you could specify the following for <i>ExpressionAttributeNames</i>: <ul><li><code>{"#P":"Percentile"}</code></li></ul> You could then use this substitution in an expression, as in this example: <ul><li><code>#P = :val</code></li></ul> <note>Tokens that begin with the <b>:</b> character are <i>expression attribute values</i>, which are placeholders for the actual value at runtime.</note> For more information on expression attribute names, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.AccessingItemAttributes.html">Accessing Item Attributes</a> in the <i>Amazon DynamoDB Developer Guide</i>. </li> <li> <i>Keys</i> - An array of primary key attribute values that define specific items in the table. For each primary key, you must provide <i>all</i> of the key attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide <i>both</i> the hash attribute and the range attribute. </li> <li> <i>ProjectionExpression</i> - A string that identifies one or more attributes to retrieve from the table. These attributes can include scalars, sets, or elements of a JSON document. The attributes in the expression must be separated by commas. If no attribute names are specified, then all attributes will be returned. If any of the requested attributes are not found, they will not appear in the result. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.AccessingItemAttributes.html">Accessing Item Attributes</a> in the <i>Amazon DynamoDB Developer Guide</i>. </li> <li>  <i>AttributesToGet</i> -  <important> This is a legacy parameter, for backward compatibility. New applications should use <i>ProjectionExpression</i> instead. Do not combine legacy parameters and expression parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i> exception. This parameter allows you to retrieve attributes of type List or Map; however, it cannot retrieve individual elements within a List or a Map.</important> The names of one or more attributes to retrieve. If no attribute names are provided, then all attributes will be returned. If any of the requested attributes are not found, they will not appear in the result. Note that <i>AttributesToGet</i> has no effect on provisioned throughput consumption. DynamoDB determines capacity units consumed based on item size, not on the amount of data that is returned to an application. </li> </ul></param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the BatchGetItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void BatchGetItemAsync(Dictionary<string, KeysAndAttributes> requestItems,  AmazonServiceCallback<BatchGetItemRequest, BatchGetItemResponse> callback, AsyncOptions options = null)
        {
            var request = new BatchGetItemRequest();
            request.RequestItems = requestItems;
            BatchGetItemAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the BatchGetItem operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the BatchGetItem operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void BatchGetItemAsync(BatchGetItemRequest request, AmazonServiceCallback<BatchGetItemRequest, BatchGetItemResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new BatchGetItemRequestMarshaller();
            var unmarshaller = BatchGetItemResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<BatchGetItemRequest,BatchGetItemResponse> responseObject 
                            = new AmazonServiceResult<BatchGetItemRequest,BatchGetItemResponse>((BatchGetItemRequest)req, (BatchGetItemResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<BatchGetItemRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  BatchWriteItem

        /// <summary>
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
        /// 
        /// <returns>The response from the BatchWriteItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal BatchWriteItemResponse BatchWriteItem(BatchWriteItemRequest request)
        {
            var marshaller = new BatchWriteItemRequestMarshaller();
            var unmarshaller = BatchWriteItemResponseUnmarshaller.Instance;

            return Invoke<BatchWriteItemRequest,BatchWriteItemResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
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
        /// <param name="requestItems">A map of one or more table names and, for each table, a list of operations to be performed (<i>DeleteRequest</i> or <i>PutRequest</i>). Each element in the map consists of the following: <ul> <li> <i>DeleteRequest</i> - Perform a <i>DeleteItem</i> operation on the specified item. The item to be deleted is identified by a <i>Key</i> subelement: <ul> <li> <i>Key</i> - A map of primary key attribute values that uniquely identify the ! item. Each entry in this map consists of an attribute name and an attribute value. For each primary key, you must provide <i>all</i> of the key attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide <i>both</i> the hash attribute and the range attribute. </li> </ul> </li> <li> <i>PutRequest</i> - Perform a <i>PutItem</i> operation on the specified item. The item to be put is identified by an <i>Item</i> subelement: <ul> <li> <i>Item</i> - A map of attributes and their values. Each entry in this map consists of an attribute name and an attribute value. Attribute values must not be null; string and binary type attributes must have lengths greater than zero; and set type attributes must not be empty. Requests that contain empty values will be rejected with a <i>ValidationException</i> exception. If you specify any attributes that are part of an index key, then the data types for those attributes must match those of the schema in the table's attribute definition. </li> </ul> </li> </ul></param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the BatchWriteItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void BatchWriteItemAsync(Dictionary<string, List<WriteRequest>> requestItems,  AmazonServiceCallback<BatchWriteItemRequest, BatchWriteItemResponse> callback, AsyncOptions options = null)
        {
            var request = new BatchWriteItemRequest();
            request.RequestItems = requestItems;
            BatchWriteItemAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the BatchWriteItem operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the BatchWriteItem operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void BatchWriteItemAsync(BatchWriteItemRequest request, AmazonServiceCallback<BatchWriteItemRequest, BatchWriteItemResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new BatchWriteItemRequestMarshaller();
            var unmarshaller = BatchWriteItemResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<BatchWriteItemRequest,BatchWriteItemResponse> responseObject 
                            = new AmazonServiceResult<BatchWriteItemRequest,BatchWriteItemResponse>((BatchWriteItemRequest)req, (BatchWriteItemResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<BatchWriteItemRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  CreateTable

        /// <summary>
        /// The <i>CreateTable</i> operation adds a new table to your account. In an AWS account,
        /// table names must be unique within each region. That is, you can have two tables with
        /// same name if you create the tables in different regions.
        /// 
        ///  
        /// <para>
        /// <i>CreateTable</i> is an asynchronous operation. Upon receiving a <i>CreateTable</i>
        /// request, DynamoDB immediately returns a response with a <i>TableStatus</i> of <code>CREATING</code>.
        /// After the table is created, DynamoDB sets the <i>TableStatus</i> to <code>ACTIVE</code>.
        /// You can perform read and write operations only on an <code>ACTIVE</code> table. 
        /// </para>
        ///  
        /// <para>
        /// You can optionally define secondary indexes on the new table, as part of the <i>CreateTable</i>
        /// operation. If you want to create multiple tables with secondary indexes on them, you
        /// must create the tables sequentially. Only one table with secondary indexes can be
        /// in the <code>CREATING</code> state at any given time.
        /// </para>
        ///  
        /// <para>
        /// You can use the <i>DescribeTable</i> API to check the table status.
        /// </para>
        /// </summary>
        /// 
        /// <returns>The response from the CreateTable service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.LimitExceededException">
        /// The number of concurrent table requests (cumulative number of tables in the <code>CREATING</code>,
        /// <code>DELETING</code> or <code>UPDATING</code> state) exceeds the maximum allowed
        /// of 10.
        /// 
        ///  
        /// <para>
        /// Also, for tables with secondary indexes, only one of those tables can be in the <code>CREATING</code>
        /// state at any point in time. Do not attempt to create more than one such table simultaneously.
        /// </para>
        ///  
        /// <para>
        /// The total limit of tables in the <code>ACTIVE</code> state is 250.
        /// </para>
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceInUseException">
        /// The operation conflicts with the resource's availability. For example, you attempted
        /// to recreate an existing table, or tried to delete a table currently in the <code>CREATING</code>
        /// state.
        /// </exception>
        internal CreateTableResponse CreateTable(CreateTableRequest request)
        {
            var marshaller = new CreateTableRequestMarshaller();
            var unmarshaller = CreateTableResponseUnmarshaller.Instance;

            return Invoke<CreateTableRequest,CreateTableResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// The <i>CreateTable</i> operation adds a new table to your account. In an AWS account,
        /// table names must be unique within each region. That is, you can have two tables with
        /// same name if you create the tables in different regions.
        /// 
        ///  
        /// <para>
        /// <i>CreateTable</i> is an asynchronous operation. Upon receiving a <i>CreateTable</i>
        /// request, DynamoDB immediately returns a response with a <i>TableStatus</i> of <code>CREATING</code>.
        /// After the table is created, DynamoDB sets the <i>TableStatus</i> to <code>ACTIVE</code>.
        /// You can perform read and write operations only on an <code>ACTIVE</code> table. 
        /// </para>
        ///  
        /// <para>
        /// You can optionally define secondary indexes on the new table, as part of the <i>CreateTable</i>
        /// operation. If you want to create multiple tables with secondary indexes on them, you
        /// must create the tables sequentially. Only one table with secondary indexes can be
        /// in the <code>CREATING</code> state at any given time.
        /// </para>
        ///  
        /// <para>
        /// You can use the <i>DescribeTable</i> API to check the table status.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table to create.</param>
        /// <param name="keySchema">Specifies the attributes that make up the primary key for a table or an index. The attributes in <i>KeySchema</i> must also be defined in the <i>AttributeDefinitions</i> array. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataModel.html">Data Model</a> in the <i>Amazon DynamoDB Developer Guide</i>. Each <i>KeySchemaElement</i> in the array is composed of: <ul> <li> <i>AttributeName</i> - The name of this key attribute. </li> <li> <i>KeyType</i> - Determines whether the key attribute is <code>HASH</code> or <code>RANGE</code>. </li> </ul> For a primary key that consists of a hash attribute, you must provide exactly one element with a <i>KeyType</i> of <code>HASH</code>. For a primary key that consists of hash and range attributes, you must provide exactly two elements, in this order: The first element must have a <i>KeyType</i> of <code>HASH</code>, and the second element must have a <i>KeyType</i> of <code>RANGE</code>. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#WorkingWithTables.primary.key">Specifying the Primary Key</a> in the <i>Amazon DynamoDB Developer Guide</i>.</param>
        /// <param name="attributeDefinitions">An array of attributes that describe the key schema for the table and indexes.</param>
        /// <param name="provisionedThroughput">A property of CreateTableRequest used to execute the CreateTable service method.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the CreateTable service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.LimitExceededException">
        /// The number of concurrent table requests (cumulative number of tables in the <code>CREATING</code>,
        /// <code>DELETING</code> or <code>UPDATING</code> state) exceeds the maximum allowed
        /// of 10.
        /// 
        ///  
        /// <para>
        /// Also, for tables with secondary indexes, only one of those tables can be in the <code>CREATING</code>
        /// state at any point in time. Do not attempt to create more than one such table simultaneously.
        /// </para>
        ///  
        /// <para>
        /// The total limit of tables in the <code>ACTIVE</code> state is 250.
        /// </para>
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceInUseException">
        /// The operation conflicts with the resource's availability. For example, you attempted
        /// to recreate an existing table, or tried to delete a table currently in the <code>CREATING</code>
        /// state.
        /// </exception>
        public void CreateTableAsync(string tableName, List<KeySchemaElement> keySchema, List<AttributeDefinition> attributeDefinitions, ProvisionedThroughput provisionedThroughput,  AmazonServiceCallback<CreateTableRequest, CreateTableResponse> callback, AsyncOptions options = null)
        {
            var request = new CreateTableRequest();
            request.TableName = tableName;
            request.KeySchema = keySchema;
            request.AttributeDefinitions = attributeDefinitions;
            request.ProvisionedThroughput = provisionedThroughput;
            CreateTableAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the CreateTable operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CreateTable operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void CreateTableAsync(CreateTableRequest request, AmazonServiceCallback<CreateTableRequest, CreateTableResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new CreateTableRequestMarshaller();
            var unmarshaller = CreateTableResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<CreateTableRequest,CreateTableResponse> responseObject 
                            = new AmazonServiceResult<CreateTableRequest,CreateTableResponse>((CreateTableRequest)req, (CreateTableResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<CreateTableRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  DeleteItem

        /// <summary>
        /// Deletes a single item in a table by primary key. You can perform a conditional delete
        /// operation that deletes the item if it exists, or if it has an expected attribute value.
        /// 
        ///  
        /// <para>
        /// In addition to deleting an item, you can also return the item's attribute values in
        /// the same operation, using the <i>ReturnValues</i> parameter.
        /// </para>
        ///  
        /// <para>
        /// Unless you specify conditions, the <i>DeleteItem</i> is an idempotent operation; running
        /// it multiple times on the same item or attribute does <i>not</i> result in an error
        /// response.
        /// </para>
        ///  
        /// <para>
        /// Conditional deletes are useful for deleting items only if specific conditions are
        /// met. If those conditions are met, DynamoDB performs the delete. Otherwise, the item
        /// is not deleted. 
        /// </para>
        /// </summary>
        /// 
        /// <returns>The response from the DeleteItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.ConditionalCheckFailedException">
        /// A condition specified in the operation could not be evaluated.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal DeleteItemResponse DeleteItem(DeleteItemRequest request)
        {
            var marshaller = new DeleteItemRequestMarshaller();
            var unmarshaller = DeleteItemResponseUnmarshaller.Instance;

            return Invoke<DeleteItemRequest,DeleteItemResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// Deletes a single item in a table by primary key. You can perform a conditional delete
        /// operation that deletes the item if it exists, or if it has an expected attribute value.
        /// 
        ///  
        /// <para>
        /// In addition to deleting an item, you can also return the item's attribute values in
        /// the same operation, using the <i>ReturnValues</i> parameter.
        /// </para>
        ///  
        /// <para>
        /// Unless you specify conditions, the <i>DeleteItem</i> is an idempotent operation; running
        /// it multiple times on the same item or attribute does <i>not</i> result in an error
        /// response.
        /// </para>
        ///  
        /// <para>
        /// Conditional deletes are useful for deleting items only if specific conditions are
        /// met. If those conditions are met, DynamoDB performs the delete. Otherwise, the item
        /// is not deleted. 
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table from which to delete the item.</param>
        /// <param name="key">A map of attribute names to <i>AttributeValue</i> objects, representing the primary key of the item to delete. For the primary key, you must provide all of the attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the DeleteItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.ConditionalCheckFailedException">
        /// A condition specified in the operation could not be evaluated.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void DeleteItemAsync(string tableName, Dictionary<string, AttributeValue> key,  AmazonServiceCallback<DeleteItemRequest, DeleteItemResponse> callback, AsyncOptions options = null)
        {
            var request = new DeleteItemRequest();
            request.TableName = tableName;
            request.Key = key;
            DeleteItemAsync(request, callback, options);
        }


        /// <summary>
        /// Deletes a single item in a table by primary key. You can perform a conditional delete
        /// operation that deletes the item if it exists, or if it has an expected attribute value.
        /// 
        ///  
        /// <para>
        /// In addition to deleting an item, you can also return the item's attribute values in
        /// the same operation, using the <i>ReturnValues</i> parameter.
        /// </para>
        ///  
        /// <para>
        /// Unless you specify conditions, the <i>DeleteItem</i> is an idempotent operation; running
        /// it multiple times on the same item or attribute does <i>not</i> result in an error
        /// response.
        /// </para>
        ///  
        /// <para>
        /// Conditional deletes are useful for deleting items only if specific conditions are
        /// met. If those conditions are met, DynamoDB performs the delete. Otherwise, the item
        /// is not deleted. 
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table from which to delete the item.</param>
        /// <param name="key">A map of attribute names to <i>AttributeValue</i> objects, representing the primary key of the item to delete. For the primary key, you must provide all of the attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute.</param>
        /// <param name="returnValues">Use <i>ReturnValues</i> if you want to get the item attributes as they appeared before they were deleted. For <i>DeleteItem</i>, the valid values are: <ul> <li> <code>NONE</code> - If <i>ReturnValues</i> is not specified, or if its value is <code>NONE</code>, then nothing is returned. (This setting is the default for <i>ReturnValues</i>.) </li> <li> <code>ALL_OLD</code> - The content of the old item is returned. </li> </ul></param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the DeleteItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.ConditionalCheckFailedException">
        /// A condition specified in the operation could not be evaluated.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void DeleteItemAsync(string tableName, Dictionary<string, AttributeValue> key, ReturnValue returnValues,  AmazonServiceCallback<DeleteItemRequest, DeleteItemResponse> callback, AsyncOptions options = null)
        {
            var request = new DeleteItemRequest();
            request.TableName = tableName;
            request.Key = key;
            request.ReturnValues = returnValues;
            DeleteItemAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the DeleteItem operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteItem operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DeleteItemAsync(DeleteItemRequest request, AmazonServiceCallback<DeleteItemRequest, DeleteItemResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DeleteItemRequestMarshaller();
            var unmarshaller = DeleteItemResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DeleteItemRequest,DeleteItemResponse> responseObject 
                            = new AmazonServiceResult<DeleteItemRequest,DeleteItemResponse>((DeleteItemRequest)req, (DeleteItemResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DeleteItemRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  DeleteTable

        /// <summary>
        /// The <i>DeleteTable</i> operation deletes a table and all of its items. After a <i>DeleteTable</i>
        /// request, the specified table is in the <code>DELETING</code> state until DynamoDB
        /// completes the deletion. If the table is in the <code>ACTIVE</code> state, you can
        /// delete it. If a table is in <code>CREATING</code> or <code>UPDATING</code> states,
        /// then DynamoDB returns a <i>ResourceInUseException</i>. If the specified table does
        /// not exist, DynamoDB returns a <i>ResourceNotFoundException</i>. If table is already
        /// in the <code>DELETING</code> state, no error is returned. 
        /// 
        ///  <note> 
        /// <para>
        /// DynamoDB might continue to accept data read and write operations, such as <i>GetItem</i>
        /// and <i>PutItem</i>, on a table in the <code>DELETING</code> state until the table
        /// deletion is complete.
        /// </para>
        ///  </note> 
        /// <para>
        /// When you delete a table, any indexes on that table are also deleted.
        /// </para>
        ///  
        /// <para>
        /// If you have DynamoDB Streams enabled on the table, then the corresponding stream on
        /// that table goes into the <code>DISABLED</code> state, and the stream is automatically
        /// deleted after 24 hours.
        /// </para>
        ///  
        /// <para>
        /// Use the <i>DescribeTable</i> API to check the status of the table. 
        /// </para>
        /// </summary>
        /// 
        /// <returns>The response from the DeleteTable service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.LimitExceededException">
        /// The number of concurrent table requests (cumulative number of tables in the <code>CREATING</code>,
        /// <code>DELETING</code> or <code>UPDATING</code> state) exceeds the maximum allowed
        /// of 10.
        /// 
        ///  
        /// <para>
        /// Also, for tables with secondary indexes, only one of those tables can be in the <code>CREATING</code>
        /// state at any point in time. Do not attempt to create more than one such table simultaneously.
        /// </para>
        ///  
        /// <para>
        /// The total limit of tables in the <code>ACTIVE</code> state is 250.
        /// </para>
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceInUseException">
        /// The operation conflicts with the resource's availability. For example, you attempted
        /// to recreate an existing table, or tried to delete a table currently in the <code>CREATING</code>
        /// state.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal DeleteTableResponse DeleteTable(DeleteTableRequest request)
        {
            var marshaller = new DeleteTableRequestMarshaller();
            var unmarshaller = DeleteTableResponseUnmarshaller.Instance;

            return Invoke<DeleteTableRequest,DeleteTableResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// The <i>DeleteTable</i> operation deletes a table and all of its items. After a <i>DeleteTable</i>
        /// request, the specified table is in the <code>DELETING</code> state until DynamoDB
        /// completes the deletion. If the table is in the <code>ACTIVE</code> state, you can
        /// delete it. If a table is in <code>CREATING</code> or <code>UPDATING</code> states,
        /// then DynamoDB returns a <i>ResourceInUseException</i>. If the specified table does
        /// not exist, DynamoDB returns a <i>ResourceNotFoundException</i>. If table is already
        /// in the <code>DELETING</code> state, no error is returned. 
        /// 
        ///  <note> 
        /// <para>
        /// DynamoDB might continue to accept data read and write operations, such as <i>GetItem</i>
        /// and <i>PutItem</i>, on a table in the <code>DELETING</code> state until the table
        /// deletion is complete.
        /// </para>
        ///  </note> 
        /// <para>
        /// When you delete a table, any indexes on that table are also deleted.
        /// </para>
        ///  
        /// <para>
        /// If you have DynamoDB Streams enabled on the table, then the corresponding stream on
        /// that table goes into the <code>DISABLED</code> state, and the stream is automatically
        /// deleted after 24 hours.
        /// </para>
        ///  
        /// <para>
        /// Use the <i>DescribeTable</i> API to check the status of the table. 
        /// </para>
        /// </summary>
        /// <param name="tableName"> The name of the table to delete.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the DeleteTable service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.LimitExceededException">
        /// The number of concurrent table requests (cumulative number of tables in the <code>CREATING</code>,
        /// <code>DELETING</code> or <code>UPDATING</code> state) exceeds the maximum allowed
        /// of 10.
        /// 
        ///  
        /// <para>
        /// Also, for tables with secondary indexes, only one of those tables can be in the <code>CREATING</code>
        /// state at any point in time. Do not attempt to create more than one such table simultaneously.
        /// </para>
        ///  
        /// <para>
        /// The total limit of tables in the <code>ACTIVE</code> state is 250.
        /// </para>
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceInUseException">
        /// The operation conflicts with the resource's availability. For example, you attempted
        /// to recreate an existing table, or tried to delete a table currently in the <code>CREATING</code>
        /// state.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void DeleteTableAsync(string tableName,  AmazonServiceCallback<DeleteTableRequest, DeleteTableResponse> callback, AsyncOptions options = null)
        {
            var request = new DeleteTableRequest();
            request.TableName = tableName;
            DeleteTableAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the DeleteTable operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteTable operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DeleteTableAsync(DeleteTableRequest request, AmazonServiceCallback<DeleteTableRequest, DeleteTableResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DeleteTableRequestMarshaller();
            var unmarshaller = DeleteTableResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DeleteTableRequest,DeleteTableResponse> responseObject 
                            = new AmazonServiceResult<DeleteTableRequest,DeleteTableResponse>((DeleteTableRequest)req, (DeleteTableResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DeleteTableRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  DescribeTable

        /// <summary>
        /// Returns information about the table, including the current status of the table, when
        /// it was created, the primary key schema, and any indexes on the table.
        /// 
        ///  <note> 
        /// <para>
        /// If you issue a DescribeTable request immediately after a CreateTable request, DynamoDB
        /// might return a ResourceNotFoundException. This is because DescribeTable uses an eventually
        /// consistent query, and the metadata for your table might not be available at that moment.
        /// Wait for a few seconds, and then try the DescribeTable request again.
        /// </para>
        ///  </note>
        /// </summary>
        /// 
        /// <returns>The response from the DescribeTable service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal DescribeTableResponse DescribeTable(DescribeTableRequest request)
        {
            var marshaller = new DescribeTableRequestMarshaller();
            var unmarshaller = DescribeTableResponseUnmarshaller.Instance;

            return Invoke<DescribeTableRequest,DescribeTableResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// Returns information about the table, including the current status of the table, when
        /// it was created, the primary key schema, and any indexes on the table.
        /// 
        ///  <note> 
        /// <para>
        /// If you issue a DescribeTable request immediately after a CreateTable request, DynamoDB
        /// might return a ResourceNotFoundException. This is because DescribeTable uses an eventually
        /// consistent query, and the metadata for your table might not be available at that moment.
        /// Wait for a few seconds, and then try the DescribeTable request again.
        /// </para>
        ///  </note>
        /// </summary>
        /// <param name="tableName"> The name of the table to describe.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the DescribeTable service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void DescribeTableAsync(string tableName,  AmazonServiceCallback<DescribeTableRequest, DescribeTableResponse> callback, AsyncOptions options = null)
        {
            var request = new DescribeTableRequest();
            request.TableName = tableName;
            DescribeTableAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the DescribeTable operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeTable operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DescribeTableAsync(DescribeTableRequest request, AmazonServiceCallback<DescribeTableRequest, DescribeTableResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DescribeTableRequestMarshaller();
            var unmarshaller = DescribeTableResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DescribeTableRequest,DescribeTableResponse> responseObject 
                            = new AmazonServiceResult<DescribeTableRequest,DescribeTableResponse>((DescribeTableRequest)req, (DescribeTableResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DescribeTableRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetItem

        /// <summary>
        /// The <i>GetItem</i> operation returns a set of attributes for the item with the given
        /// primary key. If there is no matching item, <i>GetItem</i> does not return any data.
        /// 
        ///  
        /// <para>
        /// <i>GetItem</i> provides an eventually consistent read by default. If your application
        /// requires a strongly consistent read, set <i>ConsistentRead</i> to <code>true</code>.
        /// Although a strongly consistent read might take more time than an eventually consistent
        /// read, it always returns the last updated value.
        /// </para>
        /// </summary>
        /// 
        /// <returns>The response from the GetItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal GetItemResponse GetItem(GetItemRequest request)
        {
            var marshaller = new GetItemRequestMarshaller();
            var unmarshaller = GetItemResponseUnmarshaller.Instance;

            return Invoke<GetItemRequest,GetItemResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// The <i>GetItem</i> operation returns a set of attributes for the item with the given
        /// primary key. If there is no matching item, <i>GetItem</i> does not return any data.
        /// 
        ///  
        /// <para>
        /// <i>GetItem</i> provides an eventually consistent read by default. If your application
        /// requires a strongly consistent read, set <i>ConsistentRead</i> to <code>true</code>.
        /// Although a strongly consistent read might take more time than an eventually consistent
        /// read, it always returns the last updated value.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table containing the requested item.</param>
        /// <param name="key">A map of attribute names to <i>AttributeValue</i> objects, representing the primary key of the item to retrieve. For the primary key, you must provide all of the attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the GetItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void GetItemAsync(string tableName, Dictionary<string, AttributeValue> key,  AmazonServiceCallback<GetItemRequest, GetItemResponse> callback, AsyncOptions options = null)
        {
            var request = new GetItemRequest();
            request.TableName = tableName;
            request.Key = key;
            GetItemAsync(request, callback, options);
        }


        /// <summary>
        /// The <i>GetItem</i> operation returns a set of attributes for the item with the given
        /// primary key. If there is no matching item, <i>GetItem</i> does not return any data.
        /// 
        ///  
        /// <para>
        /// <i>GetItem</i> provides an eventually consistent read by default. If your application
        /// requires a strongly consistent read, set <i>ConsistentRead</i> to <code>true</code>.
        /// Although a strongly consistent read might take more time than an eventually consistent
        /// read, it always returns the last updated value.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table containing the requested item.</param>
        /// <param name="key">A map of attribute names to <i>AttributeValue</i> objects, representing the primary key of the item to retrieve. For the primary key, you must provide all of the attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute.</param>
        /// <param name="consistentRead">Determines the read consistency model: If set to <code>true</code>, then the operation uses strongly consistent reads; otherwise, the operation uses eventually consistent reads.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the GetItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void GetItemAsync(string tableName, Dictionary<string, AttributeValue> key, bool consistentRead,  AmazonServiceCallback<GetItemRequest, GetItemResponse> callback, AsyncOptions options = null)
        {
            var request = new GetItemRequest();
            request.TableName = tableName;
            request.Key = key;
            request.ConsistentRead = consistentRead;
            GetItemAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the GetItem operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetItem operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetItemAsync(GetItemRequest request, AmazonServiceCallback<GetItemRequest, GetItemResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetItemRequestMarshaller();
            var unmarshaller = GetItemResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetItemRequest,GetItemResponse> responseObject 
                            = new AmazonServiceResult<GetItemRequest,GetItemResponse>((GetItemRequest)req, (GetItemResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetItemRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListTables

        /// <summary>
        /// Returns an array of table names associated with the current account and endpoint.
        /// The output from <i>ListTables</i> is paginated, with each page returning a maximum
        /// of 100 table names.
        /// </summary>
        /// 
        /// <returns>The response from the ListTables service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        internal ListTablesResponse ListTables()
        {
            return ListTables(new ListTablesRequest());
        }

        /// <summary>
        /// Returns an array of table names associated with the current account and endpoint.
        /// The output from <i>ListTables</i> is paginated, with each page returning a maximum
        /// of 100 table names.
        /// </summary>
        /// 
        /// <returns>The response from the ListTables service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        internal ListTablesResponse ListTables(ListTablesRequest request)
        {
            var marshaller = new ListTablesRequestMarshaller();
            var unmarshaller = ListTablesResponseUnmarshaller.Instance;

            return Invoke<ListTablesRequest,ListTablesResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// Returns an array of table names associated with the current account and endpoint.
        /// The output from <i>ListTables</i> is paginated, with each page returning a maximum
        /// of 100 table names.
        /// </summary>
         /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListTables service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        public void ListTablesAsync(AmazonServiceCallback<ListTablesRequest, ListTablesResponse> callback, AsyncOptions options = null)
        {
            ListTablesAsync(new ListTablesRequest(), callback, options);
        }


        /// <summary>
        /// Returns an array of table names associated with the current account and endpoint.
        /// The output from <i>ListTables</i> is paginated, with each page returning a maximum
        /// of 100 table names.
        /// </summary>
        /// <param name="exclusiveStartTableName">The first table name that this operation will evaluate. Use the value that was returned for <i>LastEvaluatedTableName</i> in a previous operation, so that you can obtain the next page of results.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListTables service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        public void ListTablesAsync(string exclusiveStartTableName,  AmazonServiceCallback<ListTablesRequest, ListTablesResponse> callback, AsyncOptions options = null)
        {
            var request = new ListTablesRequest();
            request.ExclusiveStartTableName = exclusiveStartTableName;
            ListTablesAsync(request, callback, options);
        }


        /// <summary>
        /// Returns an array of table names associated with the current account and endpoint.
        /// The output from <i>ListTables</i> is paginated, with each page returning a maximum
        /// of 100 table names.
        /// </summary>
        /// <param name="exclusiveStartTableName">The first table name that this operation will evaluate. Use the value that was returned for <i>LastEvaluatedTableName</i> in a previous operation, so that you can obtain the next page of results.</param>
        /// <param name="limit"> A maximum number of table names to return. If this parameter is not specified, the limit is 100.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListTables service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        public void ListTablesAsync(string exclusiveStartTableName, int limit,  AmazonServiceCallback<ListTablesRequest, ListTablesResponse> callback, AsyncOptions options = null)
        {
            var request = new ListTablesRequest();
            request.ExclusiveStartTableName = exclusiveStartTableName;
            request.Limit = limit;
            ListTablesAsync(request, callback, options);
        }


        /// <summary>
        /// Returns an array of table names associated with the current account and endpoint.
        /// The output from <i>ListTables</i> is paginated, with each page returning a maximum
        /// of 100 table names.
        /// </summary>
        /// <param name="limit"> A maximum number of table names to return. If this parameter is not specified, the limit is 100.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListTables service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        public void ListTablesAsync(int limit,  AmazonServiceCallback<ListTablesRequest, ListTablesResponse> callback, AsyncOptions options = null)
        {
            var request = new ListTablesRequest();
            request.Limit = limit;
            ListTablesAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the ListTables operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListTables operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListTablesAsync(ListTablesRequest request, AmazonServiceCallback<ListTablesRequest, ListTablesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListTablesRequestMarshaller();
            var unmarshaller = ListTablesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListTablesRequest,ListTablesResponse> responseObject 
                            = new AmazonServiceResult<ListTablesRequest,ListTablesResponse>((ListTablesRequest)req, (ListTablesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListTablesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  PutItem

        /// <summary>
        /// Creates a new item, or replaces an old item with a new item. If an item that has the
        /// same primary key as the new item already exists in the specified table, the new item
        /// completely replaces the existing item. You can perform a conditional put operation
        /// (add a new item if one with the specified primary key doesn't exist), or replace an
        /// existing item if it has certain attribute values. 
        /// 
        ///  
        /// <para>
        /// In addition to putting an item, you can also return the item's attribute values in
        /// the same operation, using the <i>ReturnValues</i> parameter.
        /// </para>
        ///  
        /// <para>
        /// When you add an item, the primary key attribute(s) are the only required attributes.
        /// Attribute values cannot be null. String and Binary type attributes must have lengths
        /// greater than zero. Set type attributes cannot be empty. Requests with empty values
        /// will be rejected with a <i>ValidationException</i> exception.
        /// </para>
        ///  
        /// <para>
        /// You can request that <i>PutItem</i> return either a copy of the original item (before
        /// the update) or a copy of the updated item (after the update). For more information,
        /// see the <i>ReturnValues</i> description below.
        /// </para>
        ///  <note> 
        /// <para>
        /// To prevent a new item from replacing an existing item, use a conditional put operation
        /// with <i>ComparisonOperator</i> set to <code>NULL</code> for the primary key attribute,
        /// or attributes.
        /// </para>
        ///  </note> 
        /// <para>
        /// For more information about using this API, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithItems.html">Working
        /// with Items</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        /// 
        /// <returns>The response from the PutItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.ConditionalCheckFailedException">
        /// A condition specified in the operation could not be evaluated.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal PutItemResponse PutItem(PutItemRequest request)
        {
            var marshaller = new PutItemRequestMarshaller();
            var unmarshaller = PutItemResponseUnmarshaller.Instance;

            return Invoke<PutItemRequest,PutItemResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// Creates a new item, or replaces an old item with a new item. If an item that has the
        /// same primary key as the new item already exists in the specified table, the new item
        /// completely replaces the existing item. You can perform a conditional put operation
        /// (add a new item if one with the specified primary key doesn't exist), or replace an
        /// existing item if it has certain attribute values. 
        /// 
        ///  
        /// <para>
        /// In addition to putting an item, you can also return the item's attribute values in
        /// the same operation, using the <i>ReturnValues</i> parameter.
        /// </para>
        ///  
        /// <para>
        /// When you add an item, the primary key attribute(s) are the only required attributes.
        /// Attribute values cannot be null. String and Binary type attributes must have lengths
        /// greater than zero. Set type attributes cannot be empty. Requests with empty values
        /// will be rejected with a <i>ValidationException</i> exception.
        /// </para>
        ///  
        /// <para>
        /// You can request that <i>PutItem</i> return either a copy of the original item (before
        /// the update) or a copy of the updated item (after the update). For more information,
        /// see the <i>ReturnValues</i> description below.
        /// </para>
        ///  <note> 
        /// <para>
        /// To prevent a new item from replacing an existing item, use a conditional put operation
        /// with <i>ComparisonOperator</i> set to <code>NULL</code> for the primary key attribute,
        /// or attributes.
        /// </para>
        ///  </note> 
        /// <para>
        /// For more information about using this API, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithItems.html">Working
        /// with Items</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table to contain the item.</param>
        /// <param name="item">A map of attribute name/value pairs, one for each attribute. Only the primary key attributes are required; you can optionally provide other attribute name-value pairs for the item. You must provide all of the attributes for the primary key. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute. If you specify any attributes that are part of an index key, then the data types for those attributes must match those of the schema in the table's attribute definition. For more information about primary keys, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataModel.html#DataModelPrimaryKey">Primary Key</a> in the <i>Amazon DynamoDB Developer Guide</i>. Each element in the <i>Item</i> map is an <i>AttributeValue</i> object.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the PutItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.ConditionalCheckFailedException">
        /// A condition specified in the operation could not be evaluated.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void PutItemAsync(string tableName, Dictionary<string, AttributeValue> item,  AmazonServiceCallback<PutItemRequest, PutItemResponse> callback, AsyncOptions options = null)
        {
            var request = new PutItemRequest();
            request.TableName = tableName;
            request.Item = item;
            PutItemAsync(request, callback, options);
        }


        /// <summary>
        /// Creates a new item, or replaces an old item with a new item. If an item that has the
        /// same primary key as the new item already exists in the specified table, the new item
        /// completely replaces the existing item. You can perform a conditional put operation
        /// (add a new item if one with the specified primary key doesn't exist), or replace an
        /// existing item if it has certain attribute values. 
        /// 
        ///  
        /// <para>
        /// In addition to putting an item, you can also return the item's attribute values in
        /// the same operation, using the <i>ReturnValues</i> parameter.
        /// </para>
        ///  
        /// <para>
        /// When you add an item, the primary key attribute(s) are the only required attributes.
        /// Attribute values cannot be null. String and Binary type attributes must have lengths
        /// greater than zero. Set type attributes cannot be empty. Requests with empty values
        /// will be rejected with a <i>ValidationException</i> exception.
        /// </para>
        ///  
        /// <para>
        /// You can request that <i>PutItem</i> return either a copy of the original item (before
        /// the update) or a copy of the updated item (after the update). For more information,
        /// see the <i>ReturnValues</i> description below.
        /// </para>
        ///  <note> 
        /// <para>
        /// To prevent a new item from replacing an existing item, use a conditional put operation
        /// with <i>ComparisonOperator</i> set to <code>NULL</code> for the primary key attribute,
        /// or attributes.
        /// </para>
        ///  </note> 
        /// <para>
        /// For more information about using this API, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithItems.html">Working
        /// with Items</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table to contain the item.</param>
        /// <param name="item">A map of attribute name/value pairs, one for each attribute. Only the primary key attributes are required; you can optionally provide other attribute name-value pairs for the item. You must provide all of the attributes for the primary key. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute. If you specify any attributes that are part of an index key, then the data types for those attributes must match those of the schema in the table's attribute definition. For more information about primary keys, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataModel.html#DataModelPrimaryKey">Primary Key</a> in the <i>Amazon DynamoDB Developer Guide</i>. Each element in the <i>Item</i> map is an <i>AttributeValue</i> object.</param>
        /// <param name="returnValues">Use <i>ReturnValues</i> if you want to get the item attributes as they appeared before they were updated with the <i>PutItem</i> request. For <i>PutItem</i>, the valid values are: <ul> <li> <code>NONE</code> - If <i>ReturnValues</i> is not specified, or if its value is <code>NONE</code>, then nothing is returned. (This setting is the default for <i>ReturnValues</i>.) </li> <li> <code>ALL_OLD</code> - If <i>PutItem</i> overwrote an attribute name-value pair, then the content of the old item is returned. </li> </ul> <note>Other "Valid Values" are not relevant to PutItem.</note></param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the PutItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.ConditionalCheckFailedException">
        /// A condition specified in the operation could not be evaluated.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void PutItemAsync(string tableName, Dictionary<string, AttributeValue> item, ReturnValue returnValues,  AmazonServiceCallback<PutItemRequest, PutItemResponse> callback, AsyncOptions options = null)
        {
            var request = new PutItemRequest();
            request.TableName = tableName;
            request.Item = item;
            request.ReturnValues = returnValues;
            PutItemAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the PutItem operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutItem operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void PutItemAsync(PutItemRequest request, AmazonServiceCallback<PutItemRequest, PutItemResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new PutItemRequestMarshaller();
            var unmarshaller = PutItemResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<PutItemRequest,PutItemResponse> responseObject 
                            = new AmazonServiceResult<PutItemRequest,PutItemResponse>((PutItemRequest)req, (PutItemResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<PutItemRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  Query

        /// <summary>
        /// A <i>Query</i> operation uses the primary key of a table or a secondary index to directly
        /// access items from that table or index.
        /// 
        ///  
        /// <para>
        /// Use the <i>KeyConditionExpression</i> parameter to provide a specific hash key value.
        /// The <i>Query</i> operation will return all of the items from the table or index with
        /// that hash key value. You can optionally narrow the scope of the <i>Query</i> operation
        /// by specifying a range key value and a comparison operator in <i>KeyConditionExpression</i>.
        /// You can use the <i>ScanIndexForward</i> parameter to get results in forward or reverse
        /// order, by range key or by index key. 
        /// </para>
        ///  
        /// <para>
        /// Queries that do not return results consume the minimum number of read capacity units
        /// for that type of read operation.
        /// </para>
        ///  
        /// <para>
        /// If the total number of items meeting the query criteria exceeds the result set size
        /// limit of 1 MB, the query stops and results are returned to the user with the <i>LastEvaluatedKey</i>
        /// element to continue the query in a subsequent operation. Unlike a <i>Scan</i> operation,
        /// a <i>Query</i> operation never returns both an empty result set and a <i>LastEvaluatedKey</i>
        /// value. <i>LastEvaluatedKey</i> is only provided if the results exceed 1 MB, or if
        /// you have used the <i>Limit</i> parameter. 
        /// </para>
        ///  
        /// <para>
        /// You can query a table, a local secondary index, or a global secondary index. For a
        /// query on a table or on a local secondary index, you can set the <i>ConsistentRead</i>
        /// parameter to <code>true</code> and obtain a strongly consistent result. Global secondary
        /// indexes support eventually consistent reads only, so do not specify <i>ConsistentRead</i>
        /// when querying a global secondary index.
        /// </para>
        /// </summary>
        /// 
        /// <returns>The response from the Query service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal QueryResponse Query(QueryRequest request)
        {
            var marshaller = new QueryRequestMarshaller();
            var unmarshaller = QueryResponseUnmarshaller.Instance;

            return Invoke<QueryRequest,QueryResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// Initiates the asynchronous execution of the Query operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the Query operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void QueryAsync(QueryRequest request, AmazonServiceCallback<QueryRequest, QueryResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new QueryRequestMarshaller();
            var unmarshaller = QueryResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<QueryRequest,QueryResponse> responseObject 
                            = new AmazonServiceResult<QueryRequest,QueryResponse>((QueryRequest)req, (QueryResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<QueryRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  Scan

        /// <summary>
        /// The <i>Scan</i> operation returns one or more items and item attributes by accessing
        /// every item in a table or a secondary index. To have DynamoDB return fewer items, you
        /// can provide a <i>ScanFilter</i> operation.
        /// 
        ///  
        /// <para>
        /// If the total number of scanned items exceeds the maximum data set size limit of 1
        /// MB, the scan stops and results are returned to the user as a <i>LastEvaluatedKey</i>
        /// value to continue the scan in a subsequent operation. The results also include the
        /// number of items exceeding the limit. A scan can result in no table data meeting the
        /// filter criteria. 
        /// </para>
        ///  
        /// <para>
        /// By default, <i>Scan</i> operations proceed sequentially; however, for faster performance
        /// on a large table or secondary index, applications can request a parallel <i>Scan</i>
        /// operation by providing the <i>Segment</i> and <i>TotalSegments</i> parameters. For
        /// more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/QueryAndScan.html#QueryAndScanParallelScan">Parallel
        /// Scan</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  
        /// <para>
        /// By default, <i>Scan</i> uses eventually consistent reads when acessing the data in
        /// the table or local secondary index. However, you can use strongly consistent reads
        /// instead by setting the <i>ConsistentRead</i> parameter to <i>true</i>.
        /// </para>
        /// </summary>
        /// 
        /// <returns>The response from the Scan service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal ScanResponse Scan(ScanRequest request)
        {
            var marshaller = new ScanRequestMarshaller();
            var unmarshaller = ScanResponseUnmarshaller.Instance;

            return Invoke<ScanRequest,ScanResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// The <i>Scan</i> operation returns one or more items and item attributes by accessing
        /// every item in a table or a secondary index. To have DynamoDB return fewer items, you
        /// can provide a <i>ScanFilter</i> operation.
        /// 
        ///  
        /// <para>
        /// If the total number of scanned items exceeds the maximum data set size limit of 1
        /// MB, the scan stops and results are returned to the user as a <i>LastEvaluatedKey</i>
        /// value to continue the scan in a subsequent operation. The results also include the
        /// number of items exceeding the limit. A scan can result in no table data meeting the
        /// filter criteria. 
        /// </para>
        ///  
        /// <para>
        /// By default, <i>Scan</i> operations proceed sequentially; however, for faster performance
        /// on a large table or secondary index, applications can request a parallel <i>Scan</i>
        /// operation by providing the <i>Segment</i> and <i>TotalSegments</i> parameters. For
        /// more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/QueryAndScan.html#QueryAndScanParallelScan">Parallel
        /// Scan</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  
        /// <para>
        /// By default, <i>Scan</i> uses eventually consistent reads when acessing the data in
        /// the table or local secondary index. However, you can use strongly consistent reads
        /// instead by setting the <i>ConsistentRead</i> parameter to <i>true</i>.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table containing the requested items; or, if you provide <code>IndexName</code>, the name of the table to which that index belongs.</param>
        /// <param name="attributesToGet"><important>This is a legacy parameter, for backward compatibility. New applications should use <i>ProjectionExpression</i> instead. Do not combine legacy parameters and expression parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i> exception. This parameter allows you to retrieve attributes of type List or Map; however, it cannot retrieve individual elements within a List or a Map.</important> The names of one or more attributes to retrieve. If no attribute names are provided, then all attributes will be returned. If any of the requested attributes are not found, they will not appear in the result. Note that <i>AttributesToGet</i> has no effect on provisioned throughput consumption. DynamoDB determines capacity units consumed based on item size, not on the amount of data that is returned to an application.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the Scan service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void ScanAsync(string tableName, List<string> attributesToGet,  AmazonServiceCallback<ScanRequest, ScanResponse> callback, AsyncOptions options = null)
        {
            var request = new ScanRequest();
            request.TableName = tableName;
            request.AttributesToGet = attributesToGet;
            ScanAsync(request, callback, options);
        }


        /// <summary>
        /// The <i>Scan</i> operation returns one or more items and item attributes by accessing
        /// every item in a table or a secondary index. To have DynamoDB return fewer items, you
        /// can provide a <i>ScanFilter</i> operation.
        /// 
        ///  
        /// <para>
        /// If the total number of scanned items exceeds the maximum data set size limit of 1
        /// MB, the scan stops and results are returned to the user as a <i>LastEvaluatedKey</i>
        /// value to continue the scan in a subsequent operation. The results also include the
        /// number of items exceeding the limit. A scan can result in no table data meeting the
        /// filter criteria. 
        /// </para>
        ///  
        /// <para>
        /// By default, <i>Scan</i> operations proceed sequentially; however, for faster performance
        /// on a large table or secondary index, applications can request a parallel <i>Scan</i>
        /// operation by providing the <i>Segment</i> and <i>TotalSegments</i> parameters. For
        /// more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/QueryAndScan.html#QueryAndScanParallelScan">Parallel
        /// Scan</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  
        /// <para>
        /// By default, <i>Scan</i> uses eventually consistent reads when acessing the data in
        /// the table or local secondary index. However, you can use strongly consistent reads
        /// instead by setting the <i>ConsistentRead</i> parameter to <i>true</i>.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table containing the requested items; or, if you provide <code>IndexName</code>, the name of the table to which that index belongs.</param>
        /// <param name="scanFilter"><important> This is a legacy parameter, for backward compatibility. New applications should use <i>FilterExpression</i> instead. Do not combine legacy parameters and expression parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i> exception. </important> A condition that evaluates the scan results and returns only the desired values. <note>This parameter does not support attributes of type List or Map.</note> If you specify more than one condition in the <i>ScanFilter</i> map, then by default all of the conditions must evaluate to true. In other words, the conditions are ANDed together. (You can use the <i>ConditionalOperator</i> parameter to OR the conditions instead. If you do this, then at least one of the conditions must evaluate to true, rather than all of them.) Each <i>ScanFilter</i> element consists of an attribute name to compare, along with the following: <ul> <li> <i>AttributeValueList</i> - One or more values to evaluate against the supplied attribute. The number of values in the list depends on the operator specified in <i>ComparisonOperator</i> . For type Number, value comparisons are numeric. String value comparisons for greater than, equals, or less than are based on ASCII character code values. For example, <code>a</code> is greater than <code>A</code>, and <code>a</code> is greater than <code>B</code>. For a list of code values, see <a href="http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters">http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters</a>. For Binary, DynamoDB treats each byte of the binary data as unsigned when it compares binary values. For information on specifying data types in JSON, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataFormat.html">JSON Data Format</a> in the <i>Amazon DynamoDB Developer Guide</i>. </li> <li> <i>ComparisonOperator</i> - A comparator for evaluating attributes. For example, equals, greater than, less than, etc. The following comparison operators are available: <code>EQ | NE | LE | LT | GE | GT | NOT_NULL | NULL | CONTAINS | NOT_CONTAINS | BEGINS_WITH | IN | BETWEEN</code> For complete descriptions of all comparison operators, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/APIReference/API_Condition.html">Condition</a>. </li> </ul></param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the Scan service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void ScanAsync(string tableName, Dictionary<string, Condition> scanFilter,  AmazonServiceCallback<ScanRequest, ScanResponse> callback, AsyncOptions options = null)
        {
            var request = new ScanRequest();
            request.TableName = tableName;
            request.ScanFilter = scanFilter;
            ScanAsync(request, callback, options);
        }


        /// <summary>
        /// The <i>Scan</i> operation returns one or more items and item attributes by accessing
        /// every item in a table or a secondary index. To have DynamoDB return fewer items, you
        /// can provide a <i>ScanFilter</i> operation.
        /// 
        ///  
        /// <para>
        /// If the total number of scanned items exceeds the maximum data set size limit of 1
        /// MB, the scan stops and results are returned to the user as a <i>LastEvaluatedKey</i>
        /// value to continue the scan in a subsequent operation. The results also include the
        /// number of items exceeding the limit. A scan can result in no table data meeting the
        /// filter criteria. 
        /// </para>
        ///  
        /// <para>
        /// By default, <i>Scan</i> operations proceed sequentially; however, for faster performance
        /// on a large table or secondary index, applications can request a parallel <i>Scan</i>
        /// operation by providing the <i>Segment</i> and <i>TotalSegments</i> parameters. For
        /// more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/QueryAndScan.html#QueryAndScanParallelScan">Parallel
        /// Scan</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  
        /// <para>
        /// By default, <i>Scan</i> uses eventually consistent reads when acessing the data in
        /// the table or local secondary index. However, you can use strongly consistent reads
        /// instead by setting the <i>ConsistentRead</i> parameter to <i>true</i>.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table containing the requested items; or, if you provide <code>IndexName</code>, the name of the table to which that index belongs.</param>
        /// <param name="attributesToGet"><important>This is a legacy parameter, for backward compatibility. New applications should use <i>ProjectionExpression</i> instead. Do not combine legacy parameters and expression parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i> exception. This parameter allows you to retrieve attributes of type List or Map; however, it cannot retrieve individual elements within a List or a Map.</important> The names of one or more attributes to retrieve. If no attribute names are provided, then all attributes will be returned. If any of the requested attributes are not found, they will not appear in the result. Note that <i>AttributesToGet</i> has no effect on provisioned throughput consumption. DynamoDB determines capacity units consumed based on item size, not on the amount of data that is returned to an application.</param>
        /// <param name="scanFilter"><important> This is a legacy parameter, for backward compatibility. New applications should use <i>FilterExpression</i> instead. Do not combine legacy parameters and expression parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i> exception. </important> A condition that evaluates the scan results and returns only the desired values. <note>This parameter does not support attributes of type List or Map.</note> If you specify more than one condition in the <i>ScanFilter</i> map, then by default all of the conditions must evaluate to true. In other words, the conditions are ANDed together. (You can use the <i>ConditionalOperator</i> parameter to OR the conditions instead. If you do this, then at least one of the conditions must evaluate to true, rather than all of them.) Each <i>ScanFilter</i> element consists of an attribute name to compare, along with the following: <ul> <li> <i>AttributeValueList</i> - One or more values to evaluate against the supplied attribute. The number of values in the list depends on the operator specified in <i>ComparisonOperator</i> . For type Number, value comparisons are numeric. String value comparisons for greater than, equals, or less than are based on ASCII character code values. For example, <code>a</code> is greater than <code>A</code>, and <code>a</code> is greater than <code>B</code>. For a list of code values, see <a href="http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters">http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters</a>. For Binary, DynamoDB treats each byte of the binary data as unsigned when it compares binary values. For information on specifying data types in JSON, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataFormat.html">JSON Data Format</a> in the <i>Amazon DynamoDB Developer Guide</i>. </li> <li> <i>ComparisonOperator</i> - A comparator for evaluating attributes. For example, equals, greater than, less than, etc. The following comparison operators are available: <code>EQ | NE | LE | LT | GE | GT | NOT_NULL | NULL | CONTAINS | NOT_CONTAINS | BEGINS_WITH | IN | BETWEEN</code> For complete descriptions of all comparison operators, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/APIReference/API_Condition.html">Condition</a>. </li> </ul></param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the Scan service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void ScanAsync(string tableName, List<string> attributesToGet, Dictionary<string, Condition> scanFilter,  AmazonServiceCallback<ScanRequest, ScanResponse> callback, AsyncOptions options = null)
        {
            var request = new ScanRequest();
            request.TableName = tableName;
            request.AttributesToGet = attributesToGet;
            request.ScanFilter = scanFilter;
            ScanAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the Scan operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the Scan operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ScanAsync(ScanRequest request, AmazonServiceCallback<ScanRequest, ScanResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ScanRequestMarshaller();
            var unmarshaller = ScanResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ScanRequest,ScanResponse> responseObject 
                            = new AmazonServiceResult<ScanRequest,ScanResponse>((ScanRequest)req, (ScanResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ScanRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  UpdateItem

        /// <summary>
        /// Edits an existing item's attributes, or adds a new item to the table if it does not
        /// already exist. You can put, delete, or add attribute values. You can also perform
        /// a conditional update on an existing item (insert a new attribute name-value pair if
        /// it doesn't exist, or replace an existing name-value pair if it has certain expected
        /// attribute values). If conditions are specified and the item does not exist, then the
        /// operation fails and a new item is not created. 
        /// 
        ///  
        /// <para>
        /// You can also return the item's attribute values in the same <i>UpdateItem</i> operation
        /// using the <i>ReturnValues</i> parameter.
        /// </para>
        /// </summary>
        /// 
        /// <returns>The response from the UpdateItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.ConditionalCheckFailedException">
        /// A condition specified in the operation could not be evaluated.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal UpdateItemResponse UpdateItem(UpdateItemRequest request)
        {
            var marshaller = new UpdateItemRequestMarshaller();
            var unmarshaller = UpdateItemResponseUnmarshaller.Instance;

            return Invoke<UpdateItemRequest,UpdateItemResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// Edits an existing item's attributes, or adds a new item to the table if it does not
        /// already exist. You can put, delete, or add attribute values. You can also perform
        /// a conditional update on an existing item (insert a new attribute name-value pair if
        /// it doesn't exist, or replace an existing name-value pair if it has certain expected
        /// attribute values). If conditions are specified and the item does not exist, then the
        /// operation fails and a new item is not created. 
        /// 
        ///  
        /// <para>
        /// You can also return the item's attribute values in the same <i>UpdateItem</i> operation
        /// using the <i>ReturnValues</i> parameter.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table containing the item to update. </param>
        /// <param name="key">The primary key of the item to be updated. Each element consists of an attribute name and a value for that attribute. For the primary key, you must provide all of the attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute.</param>
        /// <param name="attributeUpdates"><important> This is a legacy parameter, for backward compatibility. New applications should use <i>UpdateExpression</i> instead. Do not combine legacy parameters and expression parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i> exception. This parameter can be used for modifying top-level attributes; however, it does not support individual list or map elements. </important> The names of attributes to be modified, the action to perform on each, and the new value for each. If you are updating an attribute that is an index key attribute for any indexes on that table, the attribute type must match the index key type defined in the <i>AttributesDefinition</i> of the table description. You can use <i>UpdateItem</i> to update any nonkey attributes. Attribute values cannot be null. String and Binary type attributes must have lengths greater than zero. Set type attributes must not be empty. Requests with empty values will be rejected with a <i>ValidationException</i> exception. Each <i>AttributeUpdates</i> element consists of an attribute name to modify, along with the following: <ul> <li> <i>Value</i> - The new value, if applicable, for this attribute. </li> <li> <i>Action</i> - A value that specifies how to perform the update. This action is only valid for an existing attribute whose data type is Number or is a set; do not use <code>ADD</code> for other data types.  If an item with the specified primary key is found in the table, the following values perform the following actions: <ul> <li> <code>PUT</code> - Adds the specified attribute to the item. If the attribute already exists, it is replaced by the new value.  </li> <li> <code>DELETE</code> - Removes the attribute and its value, if no value is specified for <code>DELETE</code>. The data type of the specified value must match the existing value's data type. If a set of values is specified, then those values are subtracted from the old set. For example, if the attribute value was the set <code>[a,b,c]</code> and the <code>DELETE</code> action specifies <code>[a,c]</code>, then the final attribute value is <code>[b]</code>. Specifying an empty set is an error. </li> <li> <code>ADD</code> - Adds the specified value to the item, if the attribute does not already exist. If the attribute does exist, then the behavior of <code>ADD</code> depends on the data type of the attribute: <ul> <li> If the existing attribute is a number, and if <i>Value</i> is also a number, then <i>Value</i> is mathematically added to the existing attribute. If <i>Value</i> is a negative number, then it is subtracted from the existing attribute. <note> If you use <code>ADD</code> to increment or decrement a number value for an item that doesn't exist before the update, DynamoDB uses 0 as the initial value. Similarly, if you use <code>ADD</code> for an existing item to increment or decrement an attribute value that doesn't exist before the update, DynamoDB uses <code>0</code> as the initial value. For example, suppose that the item you want to update doesn't have an attribute named <i>itemcount</i>, but you decide to <code>ADD</code> the number <code>3</code> to this attribute anyway. DynamoDB will create the <i>itemcount</i> attribute, set its initial value to <code>0</code>, and finally add <code>3</code> to it. The result will be a new <i>itemcount</i> attribute, with a value of <code>3</code>. </note> </li> <li> If the existing data type is a set, and if <i>Value</i> is also a set, then <i>Value</i> is appended to the existing set. For example, if the attribute value is the set <code>[1,2]</code>, and the <code>ADD</code> action specified <code>[3]</code>, then the final attribute value is <code>[1,2,3]</code>. An error occurs if an <code>ADD</code> action is specified for a set attribute and the attribute type specified does not match the existing set type.  Both sets must have the same primitive data type. For example, if the existing data type is a set of strings, <i>Value</i> must also be a set of strings. </li> </ul> </li> </ul> If no item with the specified key is found in the table, the following values perform the following actions:  <ul> <li> <code>PUT</code> - Causes DynamoDB to create a new item with the specified primary key, and then adds the attribute.  </li> <li> <code>DELETE</code> - Nothing happens, because attributes cannot be deleted from a nonexistent item. The operation succeeds, but DynamoDB does not create a new item. </li> <li> <code>ADD</code> - Causes DynamoDB to create an item with the supplied primary key and number (or set of numbers) for the attribute value. The only data types allowed are Number and Number Set. </li> </ul> </li> </ul> If you provide any attributes that are part of an index key, then the data types for those attributes must match those of the schema in the table's attribute definition.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the UpdateItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.ConditionalCheckFailedException">
        /// A condition specified in the operation could not be evaluated.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void UpdateItemAsync(string tableName, Dictionary<string, AttributeValue> key, Dictionary<string, AttributeValueUpdate> attributeUpdates,  AmazonServiceCallback<UpdateItemRequest, UpdateItemResponse> callback, AsyncOptions options = null)
        {
            var request = new UpdateItemRequest();
            request.TableName = tableName;
            request.Key = key;
            request.AttributeUpdates = attributeUpdates;
            UpdateItemAsync(request, callback, options);
        }


        /// <summary>
        /// Edits an existing item's attributes, or adds a new item to the table if it does not
        /// already exist. You can put, delete, or add attribute values. You can also perform
        /// a conditional update on an existing item (insert a new attribute name-value pair if
        /// it doesn't exist, or replace an existing name-value pair if it has certain expected
        /// attribute values). If conditions are specified and the item does not exist, then the
        /// operation fails and a new item is not created. 
        /// 
        ///  
        /// <para>
        /// You can also return the item's attribute values in the same <i>UpdateItem</i> operation
        /// using the <i>ReturnValues</i> parameter.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table containing the item to update. </param>
        /// <param name="key">The primary key of the item to be updated. Each element consists of an attribute name and a value for that attribute. For the primary key, you must provide all of the attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute.</param>
        /// <param name="attributeUpdates"><important> This is a legacy parameter, for backward compatibility. New applications should use <i>UpdateExpression</i> instead. Do not combine legacy parameters and expression parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i> exception. This parameter can be used for modifying top-level attributes; however, it does not support individual list or map elements. </important> The names of attributes to be modified, the action to perform on each, and the new value for each. If you are updating an attribute that is an index key attribute for any indexes on that table, the attribute type must match the index key type defined in the <i>AttributesDefinition</i> of the table description. You can use <i>UpdateItem</i> to update any nonkey attributes. Attribute values cannot be null. String and Binary type attributes must have lengths greater than zero. Set type attributes must not be empty. Requests with empty values will be rejected with a <i>ValidationException</i> exception. Each <i>AttributeUpdates</i> element consists of an attribute name to modify, along with the following: <ul> <li> <i>Value</i> - The new value, if applicable, for this attribute. </li> <li> <i>Action</i> - A value that specifies how to perform the update. This action is only valid for an existing attribute whose data type is Number or is a set; do not use <code>ADD</code> for other data types.  If an item with the specified primary key is found in the table, the following values perform the following actions: <ul> <li> <code>PUT</code> - Adds the specified attribute to the item. If the attribute already exists, it is replaced by the new value.  </li> <li> <code>DELETE</code> - Removes the attribute and its value, if no value is specified for <code>DELETE</code>. The data type of the specified value must match the existing value's data type. If a set of values is specified, then those values are subtracted from the old set. For example, if the attribute value was the set <code>[a,b,c]</code> and the <code>DELETE</code> action specifies <code>[a,c]</code>, then the final attribute value is <code>[b]</code>. Specifying an empty set is an error. </li> <li> <code>ADD</code> - Adds the specified value to the item, if the attribute does not already exist. If the attribute does exist, then the behavior of <code>ADD</code> depends on the data type of the attribute: <ul> <li> If the existing attribute is a number, and if <i>Value</i> is also a number, then <i>Value</i> is mathematically added to the existing attribute. If <i>Value</i> is a negative number, then it is subtracted from the existing attribute. <note> If you use <code>ADD</code> to increment or decrement a number value for an item that doesn't exist before the update, DynamoDB uses 0 as the initial value. Similarly, if you use <code>ADD</code> for an existing item to increment or decrement an attribute value that doesn't exist before the update, DynamoDB uses <code>0</code> as the initial value. For example, suppose that the item you want to update doesn't have an attribute named <i>itemcount</i>, but you decide to <code>ADD</code> the number <code>3</code> to this attribute anyway. DynamoDB will create the <i>itemcount</i> attribute, set its initial value to <code>0</code>, and finally add <code>3</code> to it. The result will be a new <i>itemcount</i> attribute, with a value of <code>3</code>. </note> </li> <li> If the existing data type is a set, and if <i>Value</i> is also a set, then <i>Value</i> is appended to the existing set. For example, if the attribute value is the set <code>[1,2]</code>, and the <code>ADD</code> action specified <code>[3]</code>, then the final attribute value is <code>[1,2,3]</code>. An error occurs if an <code>ADD</code> action is specified for a set attribute and the attribute type specified does not match the existing set type.  Both sets must have the same primitive data type. For example, if the existing data type is a set of strings, <i>Value</i> must also be a set of strings. </li> </ul> </li> </ul> If no item with the specified key is found in the table, the following values perform the following actions:  <ul> <li> <code>PUT</code> - Causes DynamoDB to create a new item with the specified primary key, and then adds the attribute.  </li> <li> <code>DELETE</code> - Nothing happens, because attributes cannot be deleted from a nonexistent item. The operation succeeds, but DynamoDB does not create a new item. </li> <li> <code>ADD</code> - Causes DynamoDB to create an item with the supplied primary key and number (or set of numbers) for the attribute value. The only data types allowed are Number and Number Set. </li> </ul> </li> </ul> If you provide any attributes that are part of an index key, then the data types for those attributes must match those of the schema in the table's attribute definition.</param>
        /// <param name="returnValues">Use <i>ReturnValues</i> if you want to get the item attributes as they appeared either before or after they were updated. For <i>UpdateItem</i>, the valid values are: <ul> <li> <code>NONE</code> - If <i>ReturnValues</i> is not specified, or if its value is <code>NONE</code>, then nothing is returned. (This setting is the default for <i>ReturnValues</i>.) </li> <li> <code>ALL_OLD</code> - If <i>UpdateItem</i> overwrote an attribute name-value pair, then the content of the old item is returned. </li> <li> <code>UPDATED_OLD</code> - The old versions of only the updated attributes are returned. </li> <li> <code>ALL_NEW</code> - All of the attributes of the new version of the item are returned. </li> <li> <code>UPDATED_NEW</code> - The new versions of only the updated attributes are returned. </li> </ul></param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the UpdateItem service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.ConditionalCheckFailedException">
        /// A condition specified in the operation could not be evaluated.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ItemCollectionSizeLimitExceededException">
        /// An item collection is too large. This exception is only returned for tables that have
        /// one or more local secondary indexes.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ProvisionedThroughputExceededException">
        /// Your request rate is too high. The AWS SDKs for DynamoDB automatically retry requests
        /// that receive this exception. Your request is eventually successful, unless your retry
        /// queue is too large to finish. Reduce the frequency of requests and use exponential
        /// backoff. For more information, go to <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ErrorHandling.html#APIRetries">Error
        /// Retries and Exponential Backoff</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void UpdateItemAsync(string tableName, Dictionary<string, AttributeValue> key, Dictionary<string, AttributeValueUpdate> attributeUpdates, ReturnValue returnValues,  AmazonServiceCallback<UpdateItemRequest, UpdateItemResponse> callback, AsyncOptions options = null)
        {
            var request = new UpdateItemRequest();
            request.TableName = tableName;
            request.Key = key;
            request.AttributeUpdates = attributeUpdates;
            request.ReturnValues = returnValues;
            UpdateItemAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the UpdateItem operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the UpdateItem operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void UpdateItemAsync(UpdateItemRequest request, AmazonServiceCallback<UpdateItemRequest, UpdateItemResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new UpdateItemRequestMarshaller();
            var unmarshaller = UpdateItemResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<UpdateItemRequest,UpdateItemResponse> responseObject 
                            = new AmazonServiceResult<UpdateItemRequest,UpdateItemResponse>((UpdateItemRequest)req, (UpdateItemResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<UpdateItemRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  UpdateTable

        /// <summary>
        /// Modifies the provisioned throughput settings, global secondary indexes, or DynamoDB
        /// Streams settings for a given table.
        /// 
        ///  
        /// <para>
        /// You can only perform one of the following operations at once:
        /// </para>
        ///  <ul> <li>
        /// <para>
        /// Modify the provisioned throughput settings of the table.
        /// </para>
        /// </li> <li>
        /// <para>
        /// Enable or disable Streams on the table.
        /// </para>
        /// </li> <li>
        /// <para>
        /// Remove a global secondary index from the table.
        /// </para>
        /// </li> <li> 
        /// <para>
        /// Create a new global secondary index on the table. Once the index begins backfilling,
        /// you can use <i>UpdateTable</i> to perform other operations.
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// <i>UpdateTable</i> is an asynchronous operation; while it is executing, the table
        /// status changes from <code>ACTIVE</code> to <code>UPDATING</code>. While it is <code>UPDATING</code>,
        /// you cannot issue another <i>UpdateTable</i> request. When the table returns to the
        /// <code>ACTIVE</code> state, the <i>UpdateTable</i> operation is complete.
        /// </para>
        /// </summary>
        /// 
        /// <returns>The response from the UpdateTable service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.LimitExceededException">
        /// The number of concurrent table requests (cumulative number of tables in the <code>CREATING</code>,
        /// <code>DELETING</code> or <code>UPDATING</code> state) exceeds the maximum allowed
        /// of 10.
        /// 
        ///  
        /// <para>
        /// Also, for tables with secondary indexes, only one of those tables can be in the <code>CREATING</code>
        /// state at any point in time. Do not attempt to create more than one such table simultaneously.
        /// </para>
        ///  
        /// <para>
        /// The total limit of tables in the <code>ACTIVE</code> state is 250.
        /// </para>
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceInUseException">
        /// The operation conflicts with the resource's availability. For example, you attempted
        /// to recreate an existing table, or tried to delete a table currently in the <code>CREATING</code>
        /// state.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        internal UpdateTableResponse UpdateTable(UpdateTableRequest request)
        {
            var marshaller = new UpdateTableRequestMarshaller();
            var unmarshaller = UpdateTableResponseUnmarshaller.Instance;

            return Invoke<UpdateTableRequest,UpdateTableResponse>(request, marshaller, unmarshaller);
        }

        /// <summary>
        /// Modifies the provisioned throughput settings, global secondary indexes, or DynamoDB
        /// Streams settings for a given table.
        /// 
        ///  
        /// <para>
        /// You can only perform one of the following operations at once:
        /// </para>
        ///  <ul> <li>
        /// <para>
        /// Modify the provisioned throughput settings of the table.
        /// </para>
        /// </li> <li>
        /// <para>
        /// Enable or disable Streams on the table.
        /// </para>
        /// </li> <li>
        /// <para>
        /// Remove a global secondary index from the table.
        /// </para>
        /// </li> <li> 
        /// <para>
        /// Create a new global secondary index on the table. Once the index begins backfilling,
        /// you can use <i>UpdateTable</i> to perform other operations.
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// <i>UpdateTable</i> is an asynchronous operation; while it is executing, the table
        /// status changes from <code>ACTIVE</code> to <code>UPDATING</code>. While it is <code>UPDATING</code>,
        /// you cannot issue another <i>UpdateTable</i> request. When the table returns to the
        /// <code>ACTIVE</code> state, the <i>UpdateTable</i> operation is complete.
        /// </para>
        /// </summary>
        /// <param name="tableName">The name of the table to be updated.</param>
        /// <param name="provisionedThroughput">A property of UpdateTableRequest used to execute the UpdateTable service method.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the UpdateTable service method, as returned by DynamoDB.</returns>
        /// <exception cref="Amazon.DynamoDBv2.Model.InternalServerErrorException">
        /// An error occurred on the server side.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.LimitExceededException">
        /// The number of concurrent table requests (cumulative number of tables in the <code>CREATING</code>,
        /// <code>DELETING</code> or <code>UPDATING</code> state) exceeds the maximum allowed
        /// of 10.
        /// 
        ///  
        /// <para>
        /// Also, for tables with secondary indexes, only one of those tables can be in the <code>CREATING</code>
        /// state at any point in time. Do not attempt to create more than one such table simultaneously.
        /// </para>
        ///  
        /// <para>
        /// The total limit of tables in the <code>ACTIVE</code> state is 250.
        /// </para>
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceInUseException">
        /// The operation conflicts with the resource's availability. For example, you attempted
        /// to recreate an existing table, or tried to delete a table currently in the <code>CREATING</code>
        /// state.
        /// </exception>
        /// <exception cref="Amazon.DynamoDBv2.Model.ResourceNotFoundException">
        /// The operation tried to access a nonexistent table or index. The resource might not
        /// be specified correctly, or its status might not be <code>ACTIVE</code>.
        /// </exception>
        public void UpdateTableAsync(string tableName, ProvisionedThroughput provisionedThroughput,  AmazonServiceCallback<UpdateTableRequest, UpdateTableResponse> callback, AsyncOptions options = null)
        {
            var request = new UpdateTableRequest();
            request.TableName = tableName;
            request.ProvisionedThroughput = provisionedThroughput;
            UpdateTableAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the UpdateTable operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the UpdateTable operation on AmazonDynamoDBClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void UpdateTableAsync(UpdateTableRequest request, AmazonServiceCallback<UpdateTableRequest, UpdateTableResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new UpdateTableRequestMarshaller();
            var unmarshaller = UpdateTableResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<UpdateTableRequest,UpdateTableResponse> responseObject 
                            = new AmazonServiceResult<UpdateTableRequest,UpdateTableResponse>((UpdateTableRequest)req, (UpdateTableResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<UpdateTableRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
    }
}