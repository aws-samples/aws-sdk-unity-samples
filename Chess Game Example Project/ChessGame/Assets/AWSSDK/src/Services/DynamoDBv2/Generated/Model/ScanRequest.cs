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
    /// Container for the parameters to the Scan operation.
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
    public partial class ScanRequest : AmazonDynamoDBRequest
    {
        private List<string> _attributesToGet = new List<string>();
        private ConditionalOperator _conditionalOperator;
        private bool? _consistentRead;
        private Dictionary<string, AttributeValue> _exclusiveStartKey = new Dictionary<string, AttributeValue>();
        private Dictionary<string, string> _expressionAttributeNames = new Dictionary<string, string>();
        private Dictionary<string, AttributeValue> _expressionAttributeValues = new Dictionary<string, AttributeValue>();
        private string _filterExpression;
        private string _indexName;
        private int? _limit;
        private string _projectionExpression;
        private ReturnConsumedCapacity _returnConsumedCapacity;
        private Dictionary<string, Condition> _scanFilter = new Dictionary<string, Condition>();
        private int? _segment;
        private Select _select;
        private string _tableName;
        private int? _totalSegments;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public ScanRequest() { }

        /// <summary>
        /// Instantiates ScanRequest with the parameterized properties
        /// </summary>
        /// <param name="tableName">The name of the table containing the requested items; or, if you provide <code>IndexName</code>, the name of the table to which that index belongs.</param>
        public ScanRequest(string tableName)
        {
            _tableName = tableName;
        }

        /// <summary>
        /// Gets and sets the property AttributesToGet. <important>
        /// <para>
        /// This is a legacy parameter, for backward compatibility. New applications should use
        /// <i>ProjectionExpression</i> instead. Do not combine legacy parameters and expression
        /// parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i>
        /// exception.
        /// </para>
        ///  
        /// <para>
        /// This parameter allows you to retrieve attributes of type List or Map; however, it
        /// cannot retrieve individual elements within a List or a Map.
        /// </para>
        /// </important> 
        /// <para>
        /// The names of one or more attributes to retrieve. If no attribute names are provided,
        /// then all attributes will be returned. If any of the requested attributes are not found,
        /// they will not appear in the result.
        /// </para>
        ///  
        /// <para>
        /// Note that <i>AttributesToGet</i> has no effect on provisioned throughput consumption.
        /// DynamoDB determines capacity units consumed based on item size, not on the amount
        /// of data that is returned to an application.
        /// </para>
        /// </summary>
        public List<string> AttributesToGet
        {
            get { return this._attributesToGet; }
            set { this._attributesToGet = value; }
        }

        // Check to see if AttributesToGet property is set
        internal bool IsSetAttributesToGet()
        {
            return this._attributesToGet != null && this._attributesToGet.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property ConditionalOperator. <important> 
        /// <para>
        /// This is a legacy parameter, for backward compatibility. New applications should use
        /// <i>FilterExpression</i> instead. Do not combine legacy parameters and expression parameters
        /// in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i>
        /// exception.
        /// </para>
        ///  </important> 
        /// <para>
        /// A logical operator to apply to the conditions in a <i>ScanFilter</i> map:
        /// </para>
        ///  <ul> <li>
        /// <para>
        /// <code>AND</code> - If all of the conditions evaluate to true, then the entire map
        /// evaluates to true.
        /// </para>
        /// </li> <li>
        /// <para>
        /// <code>OR</code> - If at least one of the conditions evaluate to true, then the entire
        /// map evaluates to true.
        /// </para>
        /// </li> </ul> 
        /// <para>
        /// If you omit <i>ConditionalOperator</i>, then <code>AND</code> is the default.
        /// </para>
        ///  
        /// <para>
        /// The operation will succeed only if the entire map evaluates to true.
        /// </para>
        ///  <note>
        /// <para>
        /// This parameter does not support attributes of type List or Map.
        /// </para>
        /// </note>
        /// </summary>
        public ConditionalOperator ConditionalOperator
        {
            get { return this._conditionalOperator; }
            set { this._conditionalOperator = value; }
        }

        // Check to see if ConditionalOperator property is set
        internal bool IsSetConditionalOperator()
        {
            return this._conditionalOperator != null;
        }

        /// <summary>
        /// Gets and sets the property ConsistentRead. 
        /// <para>
        /// A Boolean value that determines the read consistency model during the scan:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// If <i>ConsistentRead</i> is <code>false</code>, then <i>Scan</i> will use eventually
        /// consistent reads. The data returned from <i>Scan</i> might not contain the results
        /// of other recently completed write operations (PutItem, UpdateItem or DeleteItem).
        /// The <i>Scan</i> response might include some stale data.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// If <i>ConsistentRead</i> is <code>true</code>, then <i>Scan</i> will use strongly
        /// consistent reads. All of the write operations that completed before the <i>Scan</i>
        /// began are guaranteed to be contained in the <i>Scan</i> response.
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// The default setting for <i>ConsistentRead</i> is <code>false</code>, meaning that
        /// eventually consistent reads will be used.
        /// </para>
        ///  
        /// <para>
        /// Strongly consistent reads are not supported on global secondary indexes. If you scan
        /// a global secondary index with <i>ConsistentRead</i> set to true, you will receive
        /// a <i>ValidationException</i>.
        /// </para>
        /// </summary>
        public bool ConsistentRead
        {
            get { return this._consistentRead.GetValueOrDefault(); }
            set { this._consistentRead = value; }
        }

        // Check to see if ConsistentRead property is set
        internal bool IsSetConsistentRead()
        {
            return this._consistentRead.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property ExclusiveStartKey. 
        /// <para>
        /// The primary key of the first item that this operation will evaluate. Use the value
        /// that was returned for <i>LastEvaluatedKey</i> in the previous operation.
        /// </para>
        ///  
        /// <para>
        /// The data type for <i>ExclusiveStartKey</i> must be String, Number or Binary. No set
        /// data types are allowed.
        /// </para>
        ///  
        /// <para>
        /// In a parallel scan, a <i>Scan</i> request that includes <i>ExclusiveStartKey</i> must
        /// specify the same segment whose previous <i>Scan</i> returned the corresponding value
        /// of <i>LastEvaluatedKey</i>.
        /// </para>
        /// </summary>
        public Dictionary<string, AttributeValue> ExclusiveStartKey
        {
            get { return this._exclusiveStartKey; }
            set { this._exclusiveStartKey = value; }
        }

        // Check to see if ExclusiveStartKey property is set
        internal bool IsSetExclusiveStartKey()
        {
            return this._exclusiveStartKey != null && this._exclusiveStartKey.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property ExpressionAttributeNames. 
        /// <para>
        /// One or more substitution tokens for attribute names in an expression. The following
        /// are some use cases for using <i>ExpressionAttributeNames</i>:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// To access an attribute whose name conflicts with a DynamoDB reserved word.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// To create a placeholder for repeating occurrences of an attribute name in an expression.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// To prevent special characters in an attribute name from being misinterpreted in an
        /// expression.
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// Use the <b>#</b> character in an expression to dereference an attribute name. For
        /// example, consider the following attribute name:
        /// </para>
        ///  <ul><li>
        /// <para>
        /// <code>Percentile</code>
        /// </para>
        /// </li></ul> 
        /// <para>
        /// The name of this attribute conflicts with a reserved word, so it cannot be used directly
        /// in an expression. (For the complete list of reserved words, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ReservedWords.html">Reserved
        /// Words</a> in the <i>Amazon DynamoDB Developer Guide</i>). To work around this, you
        /// could specify the following for <i>ExpressionAttributeNames</i>:
        /// </para>
        ///  <ul><li>
        /// <para>
        /// <code>{"#P":"Percentile"}</code>
        /// </para>
        /// </li></ul> 
        /// <para>
        /// You could then use this substitution in an expression, as in this example:
        /// </para>
        ///  <ul><li>
        /// <para>
        /// <code>#P = :val</code>
        /// </para>
        /// </li></ul> <note>
        /// <para>
        /// Tokens that begin with the <b>:</b> character are <i>expression attribute values</i>,
        /// which are placeholders for the actual value at runtime.
        /// </para>
        /// </note> 
        /// <para>
        /// For more information on expression attribute names, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.AccessingItemAttributes.html">Accessing
        /// Item Attributes</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        public Dictionary<string, string> ExpressionAttributeNames
        {
            get { return this._expressionAttributeNames; }
            set { this._expressionAttributeNames = value; }
        }

        // Check to see if ExpressionAttributeNames property is set
        internal bool IsSetExpressionAttributeNames()
        {
            return this._expressionAttributeNames != null && this._expressionAttributeNames.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property ExpressionAttributeValues. 
        /// <para>
        /// One or more values that can be substituted in an expression.
        /// </para>
        ///  
        /// <para>
        /// Use the <b>:</b> (colon) character in an expression to dereference an attribute value.
        /// For example, suppose that you wanted to check whether the value of the <i>ProductStatus</i>
        /// attribute was one of the following: 
        /// </para>
        ///  
        /// <para>
        /// <code>Available | Backordered | Discontinued</code>
        /// </para>
        ///  
        /// <para>
        /// You would first need to specify <i>ExpressionAttributeValues</i> as follows:
        /// </para>
        ///  
        /// <para>
        /// <code>{ ":avail":{"S":"Available"}, ":back":{"S":"Backordered"}, ":disc":{"S":"Discontinued"}
        /// }</code>
        /// </para>
        ///  
        /// <para>
        /// You could then use these values in an expression, such as this:
        /// </para>
        ///  
        /// <para>
        /// <code>ProductStatus IN (:avail, :back, :disc)</code>
        /// </para>
        ///  
        /// <para>
        /// For more information on expression attribute values, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.SpecifyingConditions.html">Specifying
        /// Conditions</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        public Dictionary<string, AttributeValue> ExpressionAttributeValues
        {
            get { return this._expressionAttributeValues; }
            set { this._expressionAttributeValues = value; }
        }

        // Check to see if ExpressionAttributeValues property is set
        internal bool IsSetExpressionAttributeValues()
        {
            return this._expressionAttributeValues != null && this._expressionAttributeValues.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property FilterExpression. 
        /// <para>
        /// A string that contains conditions that DynamoDB applies after the <i>Scan</i> operation,
        /// but before the data is returned to you. Items that do not satisfy the <i>FilterExpression</i>
        /// criteria are not returned.
        /// </para>
        ///  <note> 
        /// <para>
        /// A <i>FilterExpression</i> is applied after the items have already been read; the process
        /// of filtering does not consume any additional read capacity units.
        /// </para>
        /// </note> 
        /// <para>
        /// For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/QueryAndScan.html#FilteringResults">Filter
        /// Expressions</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  <note>
        /// <para>
        /// <i>FilterExpression</i> replaces the legacy <i>ScanFilter</i> and <i>ConditionalOperator</i>
        /// parameters.
        /// </para>
        /// </note>
        /// </summary>
        public string FilterExpression
        {
            get { return this._filterExpression; }
            set { this._filterExpression = value; }
        }

        // Check to see if FilterExpression property is set
        internal bool IsSetFilterExpression()
        {
            return this._filterExpression != null;
        }

        /// <summary>
        /// Gets and sets the property IndexName. 
        /// <para>
        /// The name of a secondary index to scan. This index can be any local secondary index
        /// or global secondary index. Note that if you use the <code>IndexName</code> parameter,
        /// you must also provide <code>TableName</code>.
        /// </para>
        /// </summary>
        public string IndexName
        {
            get { return this._indexName; }
            set { this._indexName = value; }
        }

        // Check to see if IndexName property is set
        internal bool IsSetIndexName()
        {
            return this._indexName != null;
        }

        /// <summary>
        /// Gets and sets the property Limit. 
        /// <para>
        /// The maximum number of items to evaluate (not necessarily the number of matching items).
        /// If DynamoDB processes the number of items up to the limit while processing the results,
        /// it stops the operation and returns the matching values up to that point, and a key
        /// in <i>LastEvaluatedKey</i> to apply in a subsequent operation, so that you can pick
        /// up where you left off. Also, if the processed data set size exceeds 1 MB before DynamoDB
        /// reaches this limit, it stops the operation and returns the matching values up to the
        /// limit, and a key in <i>LastEvaluatedKey</i> to apply in a subsequent operation to
        /// continue the operation. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/QueryAndScan.html"
        /// >Query and Scan</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        public int Limit
        {
            get { return this._limit.GetValueOrDefault(); }
            set { this._limit = value; }
        }

        // Check to see if Limit property is set
        internal bool IsSetLimit()
        {
            return this._limit.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property ProjectionExpression. 
        /// <para>
        /// A string that identifies one or more attributes to retrieve from the specified table
        /// or index. These attributes can include scalars, sets, or elements of a JSON document.
        /// The attributes in the expression must be separated by commas.
        /// </para>
        ///  
        /// <para>
        /// If no attribute names are specified, then all attributes will be returned. If any
        /// of the requested attributes are not found, they will not appear in the result.
        /// </para>
        ///  
        /// <para>
        /// For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.AccessingItemAttributes.html">Accessing
        /// Item Attributes</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  <note>
        /// <para>
        /// <i>ProjectionExpression</i> replaces the legacy <i>AttributesToGet</i> parameter.
        /// </para>
        /// </note>
        /// </summary>
        public string ProjectionExpression
        {
            get { return this._projectionExpression; }
            set { this._projectionExpression = value; }
        }

        // Check to see if ProjectionExpression property is set
        internal bool IsSetProjectionExpression()
        {
            return this._projectionExpression != null;
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
        /// Gets and sets the property ScanFilter. <important> 
        /// <para>
        /// This is a legacy parameter, for backward compatibility. New applications should use
        /// <i>FilterExpression</i> instead. Do not combine legacy parameters and expression parameters
        /// in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i>
        /// exception.
        /// </para>
        ///  </important> 
        /// <para>
        /// A condition that evaluates the scan results and returns only the desired values.
        /// </para>
        ///  <note>
        /// <para>
        /// This parameter does not support attributes of type List or Map.
        /// </para>
        /// </note> 
        /// <para>
        /// If you specify more than one condition in the <i>ScanFilter</i> map, then by default
        /// all of the conditions must evaluate to true. In other words, the conditions are ANDed
        /// together. (You can use the <i>ConditionalOperator</i> parameter to OR the conditions
        /// instead. If you do this, then at least one of the conditions must evaluate to true,
        /// rather than all of them.)
        /// </para>
        ///  
        /// <para>
        /// Each <i>ScanFilter</i> element consists of an attribute name to compare, along with
        /// the following:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>AttributeValueList</i> - One or more values to evaluate against the supplied attribute.
        /// The number of values in the list depends on the operator specified in <i>ComparisonOperator</i>
        /// .
        /// </para>
        ///  
        /// <para>
        /// For type Number, value comparisons are numeric.
        /// </para>
        ///  
        /// <para>
        /// String value comparisons for greater than, equals, or less than are based on ASCII
        /// character code values. For example, <code>a</code> is greater than <code>A</code>,
        /// and <code>a</code> is greater than <code>B</code>. For a list of code values, see
        /// <a href="http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters">http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters</a>.
        /// </para>
        ///  
        /// <para>
        /// For Binary, DynamoDB treats each byte of the binary data as unsigned when it compares
        /// binary values.
        /// </para>
        ///  
        /// <para>
        /// For information on specifying data types in JSON, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataFormat.html">JSON
        /// Data Format</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>ComparisonOperator</i> - A comparator for evaluating attributes. For example, equals,
        /// greater than, less than, etc.
        /// </para>
        ///  
        /// <para>
        /// The following comparison operators are available:
        /// </para>
        ///  
        /// <para>
        /// <code>EQ | NE | LE | LT | GE | GT | NOT_NULL | NULL | CONTAINS | NOT_CONTAINS | BEGINS_WITH
        /// | IN | BETWEEN</code>
        /// </para>
        ///  
        /// <para>
        /// For complete descriptions of all comparison operators, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/APIReference/API_Condition.html">Condition</a>.
        /// </para>
        ///  </li> </ul>
        /// </summary>
        public Dictionary<string, Condition> ScanFilter
        {
            get { return this._scanFilter; }
            set { this._scanFilter = value; }
        }

        // Check to see if ScanFilter property is set
        internal bool IsSetScanFilter()
        {
            return this._scanFilter != null && this._scanFilter.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property Segment. 
        /// <para>
        /// For a parallel <i>Scan</i> request, <i>Segment</i> identifies an individual segment
        /// to be scanned by an application worker.
        /// </para>
        ///  
        /// <para>
        /// Segment IDs are zero-based, so the first segment is always 0. For example, if you
        /// want to use four application threads to scan a table or an index, then the first thread
        /// specifies a <i>Segment</i> value of 0, the second thread specifies 1, and so on.
        /// </para>
        ///  
        /// <para>
        /// The value of <i>LastEvaluatedKey</i> returned from a parallel <i>Scan</i> request
        /// must be used as <i>ExclusiveStartKey</i> with the same segment ID in a subsequent
        /// <i>Scan</i> operation.
        /// </para>
        ///  
        /// <para>
        /// The value for <i>Segment</i> must be greater than or equal to 0, and less than the
        /// value provided for <i>TotalSegments</i>.
        /// </para>
        ///  
        /// <para>
        /// If you provide <i>Segment</i>, you must also provide <i>TotalSegments</i>.
        /// </para>
        /// </summary>
        public int Segment
        {
            get { return this._segment.GetValueOrDefault(); }
            set { this._segment = value; }
        }

        // Check to see if Segment property is set
        internal bool IsSetSegment()
        {
            return this._segment.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property Select. 
        /// <para>
        /// The attributes to be returned in the result. You can retrieve all item attributes,
        /// specific item attributes, or the count of matching items.
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <code>ALL_ATTRIBUTES</code> - Returns all of the item attributes.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>COUNT</code> - Returns the number of matching items, rather than the matching
        /// items themselves.
        /// </para>
        ///  </li> <li> 
        /// <para>
        ///  <code>SPECIFIC_ATTRIBUTES</code> - Returns only the attributes listed in <i>AttributesToGet</i>.
        /// This return value is equivalent to specifying <i>AttributesToGet</i> without specifying
        /// any value for <i>Select</i>.
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// If neither <i>Select</i> nor <i>AttributesToGet</i> are specified, DynamoDB defaults
        /// to <code>ALL_ATTRIBUTES</code>. You cannot use both <i>AttributesToGet</i> and <i>Select</i>
        /// together in a single request, unless the value for <i>Select</i> is <code>SPECIFIC_ATTRIBUTES</code>.
        /// (This usage is equivalent to specifying <i>AttributesToGet</i> without any value for
        /// <i>Select</i>.)
        /// </para>
        /// </summary>
        public Select Select
        {
            get { return this._select; }
            set { this._select = value; }
        }

        // Check to see if Select property is set
        internal bool IsSetSelect()
        {
            return this._select != null;
        }

        /// <summary>
        /// Gets and sets the property TableName. 
        /// <para>
        /// The name of the table containing the requested items; or, if you provide <code>IndexName</code>,
        /// the name of the table to which that index belongs.
        /// </para>
        /// </summary>
        public string TableName
        {
            get { return this._tableName; }
            set { this._tableName = value; }
        }

        // Check to see if TableName property is set
        internal bool IsSetTableName()
        {
            return this._tableName != null;
        }

        /// <summary>
        /// Gets and sets the property TotalSegments. 
        /// <para>
        /// For a parallel <i>Scan</i> request, <i>TotalSegments</i> represents the total number
        /// of segments into which the <i>Scan</i> operation will be divided. The value of <i>TotalSegments</i>
        /// corresponds to the number of application workers that will perform the parallel scan.
        /// For example, if you want to use four application threads to scan a table or an index,
        /// specify a <i>TotalSegments</i> value of 4.
        /// </para>
        ///  
        /// <para>
        /// The value for <i>TotalSegments</i> must be greater than or equal to 1, and less than
        /// or equal to 1000000. If you specify a <i>TotalSegments</i> value of 1, the <i>Scan</i>
        /// operation will be sequential rather than parallel.
        /// </para>
        ///  
        /// <para>
        /// If you specify <i>TotalSegments</i>, you must also specify <i>Segment</i>.
        /// </para>
        /// </summary>
        public int TotalSegments
        {
            get { return this._totalSegments.GetValueOrDefault(); }
            set { this._totalSegments = value; }
        }

        // Check to see if TotalSegments property is set
        internal bool IsSetTotalSegments()
        {
            return this._totalSegments.HasValue; 
        }

    }
}