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
    /// Container for the parameters to the GetItem operation.
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
    public partial class GetItemRequest : AmazonDynamoDBRequest
    {
        private List<string> _attributesToGet = new List<string>();
        private bool? _consistentRead;
        private Dictionary<string, string> _expressionAttributeNames = new Dictionary<string, string>();
        private Dictionary<string, AttributeValue> _key = new Dictionary<string, AttributeValue>();
        private string _projectionExpression;
        private ReturnConsumedCapacity _returnConsumedCapacity;
        private string _tableName;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public GetItemRequest() { }

        /// <summary>
        /// Instantiates GetItemRequest with the parameterized properties
        /// </summary>
        /// <param name="tableName">The name of the table containing the requested item.</param>
        /// <param name="key">A map of attribute names to <i>AttributeValue</i> objects, representing the primary key of the item to retrieve. For the primary key, you must provide all of the attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute.</param>
        public GetItemRequest(string tableName, Dictionary<string, AttributeValue> key)
        {
            _tableName = tableName;
            _key = key;
        }

        /// <summary>
        /// Instantiates GetItemRequest with the parameterized properties
        /// </summary>
        /// <param name="tableName">The name of the table containing the requested item.</param>
        /// <param name="key">A map of attribute names to <i>AttributeValue</i> objects, representing the primary key of the item to retrieve. For the primary key, you must provide all of the attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute.</param>
        /// <param name="consistentRead">Determines the read consistency model: If set to <code>true</code>, then the operation uses strongly consistent reads; otherwise, the operation uses eventually consistent reads.</param>
        public GetItemRequest(string tableName, Dictionary<string, AttributeValue> key, bool consistentRead)
        {
            _tableName = tableName;
            _key = key;
            _consistentRead = consistentRead;
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
        /// Gets and sets the property ConsistentRead. 
        /// <para>
        /// Determines the read consistency model: If set to <code>true</code>, then the operation
        /// uses strongly consistent reads; otherwise, the operation uses eventually consistent
        /// reads.
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
        /// Gets and sets the property Key. 
        /// <para>
        /// A map of attribute names to <i>AttributeValue</i> objects, representing the primary
        /// key of the item to retrieve.
        /// </para>
        ///  
        /// <para>
        /// For the primary key, you must provide all of the attributes. For example, with a hash
        /// type primary key, you only need to provide the hash attribute. For a hash-and-range
        /// type primary key, you must provide both the hash attribute and the range attribute.
        /// </para>
        /// </summary>
        public Dictionary<string, AttributeValue> Key
        {
            get { return this._key; }
            set { this._key = value; }
        }

        // Check to see if Key property is set
        internal bool IsSetKey()
        {
            return this._key != null && this._key.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property ProjectionExpression. 
        /// <para>
        /// A string that identifies one or more attributes to retrieve from the table. These
        /// attributes can include scalars, sets, or elements of a JSON document. The attributes
        /// in the expression must be separated by commas.
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
        /// Gets and sets the property TableName. 
        /// <para>
        /// The name of the table containing the requested item.
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

    }
}