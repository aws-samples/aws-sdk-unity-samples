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
    /// Container for the parameters to the UpdateItem operation.
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
    public partial class UpdateItemRequest : AmazonDynamoDBRequest
    {
        private Dictionary<string, AttributeValueUpdate> _attributeUpdates = new Dictionary<string, AttributeValueUpdate>();
        private ConditionalOperator _conditionalOperator;
        private string _conditionExpression;
        private Dictionary<string, ExpectedAttributeValue> _expected = new Dictionary<string, ExpectedAttributeValue>();
        private Dictionary<string, string> _expressionAttributeNames = new Dictionary<string, string>();
        private Dictionary<string, AttributeValue> _expressionAttributeValues = new Dictionary<string, AttributeValue>();
        private Dictionary<string, AttributeValue> _key = new Dictionary<string, AttributeValue>();
        private ReturnConsumedCapacity _returnConsumedCapacity;
        private ReturnItemCollectionMetrics _returnItemCollectionMetrics;
        private ReturnValue _returnValues;
        private string _tableName;
        private string _updateExpression;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public UpdateItemRequest() { }

        /// <summary>
        /// Instantiates UpdateItemRequest with the parameterized properties
        /// </summary>
        /// <param name="tableName">The name of the table containing the item to update. </param>
        /// <param name="key">The primary key of the item to be updated. Each element consists of an attribute name and a value for that attribute. For the primary key, you must provide all of the attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute.</param>
        /// <param name="attributeUpdates"><important> This is a legacy parameter, for backward compatibility. New applications should use <i>UpdateExpression</i> instead. Do not combine legacy parameters and expression parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i> exception. This parameter can be used for modifying top-level attributes; however, it does not support individual list or map elements. </important> The names of attributes to be modified, the action to perform on each, and the new value for each. If you are updating an attribute that is an index key attribute for any indexes on that table, the attribute type must match the index key type defined in the <i>AttributesDefinition</i> of the table description. You can use <i>UpdateItem</i> to update any nonkey attributes. Attribute values cannot be null. String and Binary type attributes must have lengths greater than zero. Set type attributes must not be empty. Requests with empty values will be rejected with a <i>ValidationException</i> exception. Each <i>AttributeUpdates</i> element consists of an attribute name to modify, along with the following: <ul> <li> <i>Value</i> - The new value, if applicable, for this attribute. </li> <li> <i>Action</i> - A value that specifies how to perform the update. This action is only valid for an existing attribute whose data type is Number or is a set; do not use <code>ADD</code> for other data types.  If an item with the specified primary key is found in the table, the following values perform the following actions: <ul> <li> <code>PUT</code> - Adds the specified attribute to the item. If the attribute already exists, it is replaced by the new value.  </li> <li> <code>DELETE</code> - Removes the attribute and its value, if no value is specified for <code>DELETE</code>. The data type of the specified value must match the existing value's data type. If a set of values is specified, then those values are subtracted from the old set. For example, if the attribute value was the set <code>[a,b,c]</code> and the <code>DELETE</code> action specifies <code>[a,c]</code>, then the final attribute value is <code>[b]</code>. Specifying an empty set is an error. </li> <li> <code>ADD</code> - Adds the specified value to the item, if the attribute does not already exist. If the attribute does exist, then the behavior of <code>ADD</code> depends on the data type of the attribute: <ul> <li> If the existing attribute is a number, and if <i>Value</i> is also a number, then <i>Value</i> is mathematically added to the existing attribute. If <i>Value</i> is a negative number, then it is subtracted from the existing attribute. <note> If you use <code>ADD</code> to increment or decrement a number value for an item that doesn't exist before the update, DynamoDB uses 0 as the initial value. Similarly, if you use <code>ADD</code> for an existing item to increment or decrement an attribute value that doesn't exist before the update, DynamoDB uses <code>0</code> as the initial value. For example, suppose that the item you want to update doesn't have an attribute named <i>itemcount</i>, but you decide to <code>ADD</code> the number <code>3</code> to this attribute anyway. DynamoDB will create the <i>itemcount</i> attribute, set its initial value to <code>0</code>, and finally add <code>3</code> to it. The result will be a new <i>itemcount</i> attribute, with a value of <code>3</code>. </note> </li> <li> If the existing data type is a set, and if <i>Value</i> is also a set, then <i>Value</i> is appended to the existing set. For example, if the attribute value is the set <code>[1,2]</code>, and the <code>ADD</code> action specified <code>[3]</code>, then the final attribute value is <code>[1,2,3]</code>. An error occurs if an <code>ADD</code> action is specified for a set attribute and the attribute type specified does not match the existing set type.  Both sets must have the same primitive data type. For example, if the existing data type is a set of strings, <i>Value</i> must also be a set of strings. </li> </ul> </li> </ul> If no item with the specified key is found in the table, the following values perform the following actions:  <ul> <li> <code>PUT</code> - Causes DynamoDB to create a new item with the specified primary key, and then adds the attribute.  </li> <li> <code>DELETE</code> - Nothing happens, because attributes cannot be deleted from a nonexistent item. The operation succeeds, but DynamoDB does not create a new item. </li> <li> <code>ADD</code> - Causes DynamoDB to create an item with the supplied primary key and number (or set of numbers) for the attribute value. The only data types allowed are Number and Number Set. </li> </ul> </li> </ul> If you provide any attributes that are part of an index key, then the data types for those attributes must match those of the schema in the table's attribute definition.</param>
        public UpdateItemRequest(string tableName, Dictionary<string, AttributeValue> key, Dictionary<string, AttributeValueUpdate> attributeUpdates)
        {
            _tableName = tableName;
            _key = key;
            _attributeUpdates = attributeUpdates;
        }

        /// <summary>
        /// Instantiates UpdateItemRequest with the parameterized properties
        /// </summary>
        /// <param name="tableName">The name of the table containing the item to update. </param>
        /// <param name="key">The primary key of the item to be updated. Each element consists of an attribute name and a value for that attribute. For the primary key, you must provide all of the attributes. For example, with a hash type primary key, you only need to provide the hash attribute. For a hash-and-range type primary key, you must provide both the hash attribute and the range attribute.</param>
        /// <param name="attributeUpdates"><important> This is a legacy parameter, for backward compatibility. New applications should use <i>UpdateExpression</i> instead. Do not combine legacy parameters and expression parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i> exception. This parameter can be used for modifying top-level attributes; however, it does not support individual list or map elements. </important> The names of attributes to be modified, the action to perform on each, and the new value for each. If you are updating an attribute that is an index key attribute for any indexes on that table, the attribute type must match the index key type defined in the <i>AttributesDefinition</i> of the table description. You can use <i>UpdateItem</i> to update any nonkey attributes. Attribute values cannot be null. String and Binary type attributes must have lengths greater than zero. Set type attributes must not be empty. Requests with empty values will be rejected with a <i>ValidationException</i> exception. Each <i>AttributeUpdates</i> element consists of an attribute name to modify, along with the following: <ul> <li> <i>Value</i> - The new value, if applicable, for this attribute. </li> <li> <i>Action</i> - A value that specifies how to perform the update. This action is only valid for an existing attribute whose data type is Number or is a set; do not use <code>ADD</code> for other data types.  If an item with the specified primary key is found in the table, the following values perform the following actions: <ul> <li> <code>PUT</code> - Adds the specified attribute to the item. If the attribute already exists, it is replaced by the new value.  </li> <li> <code>DELETE</code> - Removes the attribute and its value, if no value is specified for <code>DELETE</code>. The data type of the specified value must match the existing value's data type. If a set of values is specified, then those values are subtracted from the old set. For example, if the attribute value was the set <code>[a,b,c]</code> and the <code>DELETE</code> action specifies <code>[a,c]</code>, then the final attribute value is <code>[b]</code>. Specifying an empty set is an error. </li> <li> <code>ADD</code> - Adds the specified value to the item, if the attribute does not already exist. If the attribute does exist, then the behavior of <code>ADD</code> depends on the data type of the attribute: <ul> <li> If the existing attribute is a number, and if <i>Value</i> is also a number, then <i>Value</i> is mathematically added to the existing attribute. If <i>Value</i> is a negative number, then it is subtracted from the existing attribute. <note> If you use <code>ADD</code> to increment or decrement a number value for an item that doesn't exist before the update, DynamoDB uses 0 as the initial value. Similarly, if you use <code>ADD</code> for an existing item to increment or decrement an attribute value that doesn't exist before the update, DynamoDB uses <code>0</code> as the initial value. For example, suppose that the item you want to update doesn't have an attribute named <i>itemcount</i>, but you decide to <code>ADD</code> the number <code>3</code> to this attribute anyway. DynamoDB will create the <i>itemcount</i> attribute, set its initial value to <code>0</code>, and finally add <code>3</code> to it. The result will be a new <i>itemcount</i> attribute, with a value of <code>3</code>. </note> </li> <li> If the existing data type is a set, and if <i>Value</i> is also a set, then <i>Value</i> is appended to the existing set. For example, if the attribute value is the set <code>[1,2]</code>, and the <code>ADD</code> action specified <code>[3]</code>, then the final attribute value is <code>[1,2,3]</code>. An error occurs if an <code>ADD</code> action is specified for a set attribute and the attribute type specified does not match the existing set type.  Both sets must have the same primitive data type. For example, if the existing data type is a set of strings, <i>Value</i> must also be a set of strings. </li> </ul> </li> </ul> If no item with the specified key is found in the table, the following values perform the following actions:  <ul> <li> <code>PUT</code> - Causes DynamoDB to create a new item with the specified primary key, and then adds the attribute.  </li> <li> <code>DELETE</code> - Nothing happens, because attributes cannot be deleted from a nonexistent item. The operation succeeds, but DynamoDB does not create a new item. </li> <li> <code>ADD</code> - Causes DynamoDB to create an item with the supplied primary key and number (or set of numbers) for the attribute value. The only data types allowed are Number and Number Set. </li> </ul> </li> </ul> If you provide any attributes that are part of an index key, then the data types for those attributes must match those of the schema in the table's attribute definition.</param>
        /// <param name="returnValues">Use <i>ReturnValues</i> if you want to get the item attributes as they appeared either before or after they were updated. For <i>UpdateItem</i>, the valid values are: <ul> <li> <code>NONE</code> - If <i>ReturnValues</i> is not specified, or if its value is <code>NONE</code>, then nothing is returned. (This setting is the default for <i>ReturnValues</i>.) </li> <li> <code>ALL_OLD</code> - If <i>UpdateItem</i> overwrote an attribute name-value pair, then the content of the old item is returned. </li> <li> <code>UPDATED_OLD</code> - The old versions of only the updated attributes are returned. </li> <li> <code>ALL_NEW</code> - All of the attributes of the new version of the item are returned. </li> <li> <code>UPDATED_NEW</code> - The new versions of only the updated attributes are returned. </li> </ul></param>
        public UpdateItemRequest(string tableName, Dictionary<string, AttributeValue> key, Dictionary<string, AttributeValueUpdate> attributeUpdates, ReturnValue returnValues)
        {
            _tableName = tableName;
            _key = key;
            _attributeUpdates = attributeUpdates;
            _returnValues = returnValues;
        }

        /// <summary>
        /// Gets and sets the property AttributeUpdates. <important> 
        /// <para>
        /// This is a legacy parameter, for backward compatibility. New applications should use
        /// <i>UpdateExpression</i> instead. Do not combine legacy parameters and expression parameters
        /// in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i>
        /// exception.
        /// </para>
        ///  
        /// <para>
        /// This parameter can be used for modifying top-level attributes; however, it does not
        /// support individual list or map elements.
        /// </para>
        ///  </important> 
        /// <para>
        /// The names of attributes to be modified, the action to perform on each, and the new
        /// value for each. If you are updating an attribute that is an index key attribute for
        /// any indexes on that table, the attribute type must match the index key type defined
        /// in the <i>AttributesDefinition</i> of the table description. You can use <i>UpdateItem</i>
        /// to update any nonkey attributes.
        /// </para>
        ///  
        /// <para>
        /// Attribute values cannot be null. String and Binary type attributes must have lengths
        /// greater than zero. Set type attributes must not be empty. Requests with empty values
        /// will be rejected with a <i>ValidationException</i> exception.
        /// </para>
        ///  
        /// <para>
        /// Each <i>AttributeUpdates</i> element consists of an attribute name to modify, along
        /// with the following:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>Value</i> - The new value, if applicable, for this attribute.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>Action</i> - A value that specifies how to perform the update. This action is only
        /// valid for an existing attribute whose data type is Number or is a set; do not use
        /// <code>ADD</code> for other data types. 
        /// </para>
        ///  
        /// <para>
        /// If an item with the specified primary key is found in the table, the following values
        /// perform the following actions:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <code>PUT</code> - Adds the specified attribute to the item. If the attribute already
        /// exists, it is replaced by the new value. 
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>DELETE</code> - Removes the attribute and its value, if no value is specified
        /// for <code>DELETE</code>. The data type of the specified value must match the existing
        /// value's data type.
        /// </para>
        ///  
        /// <para>
        /// If a set of values is specified, then those values are subtracted from the old set.
        /// For example, if the attribute value was the set <code>[a,b,c]</code> and the <code>DELETE</code>
        /// action specifies <code>[a,c]</code>, then the final attribute value is <code>[b]</code>.
        /// Specifying an empty set is an error.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>ADD</code> - Adds the specified value to the item, if the attribute does not
        /// already exist. If the attribute does exist, then the behavior of <code>ADD</code>
        /// depends on the data type of the attribute:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// If the existing attribute is a number, and if <i>Value</i> is also a number, then
        /// <i>Value</i> is mathematically added to the existing attribute. If <i>Value</i> is
        /// a negative number, then it is subtracted from the existing attribute.
        /// </para>
        ///  <note> 
        /// <para>
        /// If you use <code>ADD</code> to increment or decrement a number value for an item that
        /// doesn't exist before the update, DynamoDB uses 0 as the initial value.
        /// </para>
        ///  
        /// <para>
        /// Similarly, if you use <code>ADD</code> for an existing item to increment or decrement
        /// an attribute value that doesn't exist before the update, DynamoDB uses <code>0</code>
        /// as the initial value. For example, suppose that the item you want to update doesn't
        /// have an attribute named <i>itemcount</i>, but you decide to <code>ADD</code> the number
        /// <code>3</code> to this attribute anyway. DynamoDB will create the <i>itemcount</i>
        /// attribute, set its initial value to <code>0</code>, and finally add <code>3</code>
        /// to it. The result will be a new <i>itemcount</i> attribute, with a value of <code>3</code>.
        /// </para>
        ///  </note> </li> <li> 
        /// <para>
        /// If the existing data type is a set, and if <i>Value</i> is also a set, then <i>Value</i>
        /// is appended to the existing set. For example, if the attribute value is the set <code>[1,2]</code>,
        /// and the <code>ADD</code> action specified <code>[3]</code>, then the final attribute
        /// value is <code>[1,2,3]</code>. An error occurs if an <code>ADD</code> action is specified
        /// for a set attribute and the attribute type specified does not match the existing set
        /// type. 
        /// </para>
        ///  
        /// <para>
        /// Both sets must have the same primitive data type. For example, if the existing data
        /// type is a set of strings, <i>Value</i> must also be a set of strings.
        /// </para>
        ///  </li> </ul> </li> </ul> 
        /// <para>
        /// If no item with the specified key is found in the table, the following values perform
        /// the following actions: 
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <code>PUT</code> - Causes DynamoDB to create a new item with the specified primary
        /// key, and then adds the attribute. 
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>DELETE</code> - Nothing happens, because attributes cannot be deleted from a
        /// nonexistent item. The operation succeeds, but DynamoDB does not create a new item.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>ADD</code> - Causes DynamoDB to create an item with the supplied primary key
        /// and number (or set of numbers) for the attribute value. The only data types allowed
        /// are Number and Number Set.
        /// </para>
        ///  </li> </ul> </li> </ul> 
        /// <para>
        /// If you provide any attributes that are part of an index key, then the data types for
        /// those attributes must match those of the schema in the table's attribute definition.
        /// </para>
        /// </summary>
        public Dictionary<string, AttributeValueUpdate> AttributeUpdates
        {
            get { return this._attributeUpdates; }
            set { this._attributeUpdates = value; }
        }

        // Check to see if AttributeUpdates property is set
        internal bool IsSetAttributeUpdates()
        {
            return this._attributeUpdates != null && this._attributeUpdates.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property ConditionalOperator. <important> 
        /// <para>
        /// This is a legacy parameter, for backward compatibility. New applications should use
        /// <i>ConditionExpression</i> instead. Do not combine legacy parameters and expression
        /// parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i>
        /// exception.
        /// </para>
        ///  </important> 
        /// <para>
        /// A logical operator to apply to the conditions in the <i>Expected</i> map:
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
        /// Gets and sets the property ConditionExpression. 
        /// <para>
        /// A condition that must be satisfied in order for a conditional update to succeed.
        /// </para>
        ///  
        /// <para>
        /// An expression can contain any of the following:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// Functions: <code>attribute_exists | attribute_not_exists | attribute_type | contains
        /// | begins_with | size</code>
        /// </para>
        ///  
        /// <para>
        /// These function names are case-sensitive.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// Comparison operators: <code> = | &#x3C;&#x3E; | &#x3C; | &#x3E; | &#x3C;= | &#x3E;=
        /// | BETWEEN | IN</code>
        /// </para>
        ///  </li> <li> 
        /// <para>
        ///  Logical operators: <code>AND | OR | NOT</code>
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// For more information on condition expressions, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.SpecifyingConditions.html">Specifying
        /// Conditions</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  <note>
        /// <para>
        /// <i>ConditionExpression</i> replaces the legacy <i>ConditionalOperator</i> and <i>Expected</i>
        /// parameters.
        /// </para>
        /// </note>
        /// </summary>
        public string ConditionExpression
        {
            get { return this._conditionExpression; }
            set { this._conditionExpression = value; }
        }

        // Check to see if ConditionExpression property is set
        internal bool IsSetConditionExpression()
        {
            return this._conditionExpression != null;
        }

        /// <summary>
        /// Gets and sets the property Expected. <important> 
        /// <para>
        /// This is a legacy parameter, for backward compatibility. New applications should use
        /// <i> ConditionExpression </i> instead. Do not combine legacy parameters and expression
        /// parameters in a single API call; otherwise, DynamoDB will return a <i>ValidationException</i>
        /// exception.
        /// </para>
        ///  </important> 
        /// <para>
        /// A map of attribute/condition pairs. <i>Expected</i> provides a conditional block for
        /// the <i>UpdateItem</i> operation.
        /// </para>
        ///  
        /// <para>
        /// Each element of <i>Expected</i> consists of an attribute name, a comparison operator,
        /// and one or more values. DynamoDB compares the attribute with the value(s) you supplied,
        /// using the comparison operator. For each <i>Expected</i> element, the result of the
        /// evaluation is either true or false.
        /// </para>
        ///  
        /// <para>
        /// If you specify more than one element in the <i>Expected</i> map, then by default all
        /// of the conditions must evaluate to true. In other words, the conditions are ANDed
        /// together. (You can use the <i>ConditionalOperator</i> parameter to OR the conditions
        /// instead. If you do this, then at least one of the conditions must evaluate to true,
        /// rather than all of them.)
        /// </para>
        ///  
        /// <para>
        /// If the <i>Expected</i> map evaluates to true, then the conditional operation succeeds;
        /// otherwise, it fails.
        /// </para>
        ///  
        /// <para>
        /// <i>Expected</i> contains the following:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>AttributeValueList</i> - One or more values to evaluate against the supplied attribute.
        /// The number of values in the list depends on the <i>ComparisonOperator</i> being used.
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
        /// <a href="http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters" >http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters</a>.
        /// </para>
        ///  
        /// <para>
        /// For type Binary, DynamoDB treats each byte of the binary data as unsigned when it
        /// compares binary values.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>ComparisonOperator</i> - A comparator for evaluating attributes in the <i>AttributeValueList</i>.
        /// When performing the comparison, DynamoDB uses strongly consistent reads.
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
        /// The following are descriptions of each comparison operator.
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <code>EQ</code> : Equal. <code>EQ</code> is supported for all datatypes, including
        /// lists and maps.
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> element of type
        /// String, Number, Binary, String Set, Number Set, or Binary Set. If an item contains
        /// an <i>AttributeValue</i> element of a different type than the one provided in the
        /// request, the value does not match. For example, <code>{"S":"6"}</code> does not equal
        /// <code>{"N":"6"}</code>. Also, <code>{"N":"6"}</code> does not equal <code>{"NS":["6",
        /// "2", "1"]}</code>.
        /// </para>
        ///  <p/> </li> <li> 
        /// <para>
        /// <code>NE</code> : Not equal. <code>NE</code> is supported for all datatypes, including
        /// lists and maps.
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String,
        /// Number, Binary, String Set, Number Set, or Binary Set. If an item contains an <i>AttributeValue</i>
        /// of a different type than the one provided in the request, the value does not match.
        /// For example, <code>{"S":"6"}</code> does not equal <code>{"N":"6"}</code>. Also, <code>{"N":"6"}</code>
        /// does not equal <code>{"NS":["6", "2", "1"]}</code>.
        /// </para>
        ///  <p/> </li> <li> 
        /// <para>
        /// <code>LE</code> : Less than or equal. 
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> element of type
        /// String, Number, or Binary (not a set type). If an item contains an <i>AttributeValue</i>
        /// element of a different type than the one provided in the request, the value does not
        /// match. For example, <code>{"S":"6"}</code> does not equal <code>{"N":"6"}</code>.
        /// Also, <code>{"N":"6"}</code> does not compare to <code>{"NS":["6", "2", "1"]}</code>.
        /// </para>
        ///  <p/> </li> <li> 
        /// <para>
        /// <code>LT</code> : Less than. 
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String,
        /// Number, or Binary (not a set type). If an item contains an <i>AttributeValue</i> element
        /// of a different type than the one provided in the request, the value does not match.
        /// For example, <code>{"S":"6"}</code> does not equal <code>{"N":"6"}</code>. Also, <code>{"N":"6"}</code>
        /// does not compare to <code>{"NS":["6", "2", "1"]}</code>.
        /// </para>
        ///  <p/> </li> <li> 
        /// <para>
        /// <code>GE</code> : Greater than or equal. 
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> element of type
        /// String, Number, or Binary (not a set type). If an item contains an <i>AttributeValue</i>
        /// element of a different type than the one provided in the request, the value does not
        /// match. For example, <code>{"S":"6"}</code> does not equal <code>{"N":"6"}</code>.
        /// Also, <code>{"N":"6"}</code> does not compare to <code>{"NS":["6", "2", "1"]}</code>.
        /// </para>
        ///  <p/> </li> <li> 
        /// <para>
        /// <code>GT</code> : Greater than. 
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> element of type
        /// String, Number, or Binary (not a set type). If an item contains an <i>AttributeValue</i>
        /// element of a different type than the one provided in the request, the value does not
        /// match. For example, <code>{"S":"6"}</code> does not equal <code>{"N":"6"}</code>.
        /// Also, <code>{"N":"6"}</code> does not compare to <code>{"NS":["6", "2", "1"]}</code>.
        /// </para>
        ///  <p/> </li> <li> 
        /// <para>
        /// <code>NOT_NULL</code> : The attribute exists. <code>NOT_NULL</code> is supported for
        /// all datatypes, including lists and maps.
        /// </para>
        ///  <note>
        /// <para>
        /// This operator tests for the existence of an attribute, not its data type. If the data
        /// type of attribute "<code>a</code>" is null, and you evaluate it using <code>NOT_NULL</code>,
        /// the result is a Boolean <i>true</i>. This result is because the attribute "<code>a</code>"
        /// exists; its data type is not relevant to the <code>NOT_NULL</code> comparison operator.
        /// </para>
        ///  </note> </li> <li> 
        /// <para>
        /// <code>NULL</code> : The attribute does not exist. <code>NULL</code> is supported for
        /// all datatypes, including lists and maps.
        /// </para>
        ///  <note>
        /// <para>
        /// This operator tests for the nonexistence of an attribute, not its data type. If the
        /// data type of attribute "<code>a</code>" is null, and you evaluate it using <code>NULL</code>,
        /// the result is a Boolean <i>false</i>. This is because the attribute "<code>a</code>"
        /// exists; its data type is not relevant to the <code>NULL</code> comparison operator.
        /// </para>
        ///  </note> </li> <li> 
        /// <para>
        /// <code>CONTAINS</code> : Checks for a subsequence, or value in a set.
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> element of type
        /// String, Number, or Binary (not a set type). If the target attribute of the comparison
        /// is of type String, then the operator checks for a substring match. If the target attribute
        /// of the comparison is of type Binary, then the operator looks for a subsequence of
        /// the target that matches the input. If the target attribute of the comparison is a
        /// set ("<code>SS</code>", "<code>NS</code>", or "<code>BS</code>"), then the operator
        /// evaluates to true if it finds an exact match with any member of the set.
        /// </para>
        ///  
        /// <para>
        /// CONTAINS is supported for lists: When evaluating "<code>a CONTAINS b</code>", "<code>a</code>"
        /// can be a list; however, "<code>b</code>" cannot be a set, a map, or a list.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>NOT_CONTAINS</code> : Checks for absence of a subsequence, or absence of a value
        /// in a set.
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> element of type
        /// String, Number, or Binary (not a set type). If the target attribute of the comparison
        /// is a String, then the operator checks for the absence of a substring match. If the
        /// target attribute of the comparison is Binary, then the operator checks for the absence
        /// of a subsequence of the target that matches the input. If the target attribute of
        /// the comparison is a set ("<code>SS</code>", "<code>NS</code>", or "<code>BS</code>"),
        /// then the operator evaluates to true if it <i>does not</i> find an exact match with
        /// any member of the set.
        /// </para>
        ///  
        /// <para>
        /// NOT_CONTAINS is supported for lists: When evaluating "<code>a NOT CONTAINS b</code>",
        /// "<code>a</code>" can be a list; however, "<code>b</code>" cannot be a set, a map,
        /// or a list.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>BEGINS_WITH</code> : Checks for a prefix. 
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String
        /// or Binary (not a Number or a set type). The target attribute of the comparison must
        /// be of type String or Binary (not a Number or a set type).
        /// </para>
        ///  <p/> </li> <li> 
        /// <para>
        /// <code>IN</code> : Checks for matching elements within two sets.
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> can contain one or more <i>AttributeValue</i> elements of
        /// type String, Number, or Binary (not a set type). These attributes are compared against
        /// an existing set type attribute of an item. If any elements of the input set are present
        /// in the item attribute, the expression evaluates to true.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>BETWEEN</code> : Greater than or equal to the first value, and less than or
        /// equal to the second value. 
        /// </para>
        ///  
        /// <para>
        /// <i>AttributeValueList</i> must contain two <i>AttributeValue</i> elements of the same
        /// type, either String, Number, or Binary (not a set type). A target attribute matches
        /// if the target value is greater than, or equal to, the first element and less than,
        /// or equal to, the second element. If an item contains an <i>AttributeValue</i> element
        /// of a different type than the one provided in the request, the value does not match.
        /// For example, <code>{"S":"6"}</code> does not compare to <code>{"N":"6"}</code>. Also,
        /// <code>{"N":"6"}</code> does not compare to <code>{"NS":["6", "2", "1"]}</code>
        /// </para>
        ///  </li> </ul> </li> </ul> 
        /// <para>
        /// For usage examples of <i>AttributeValueList</i> and <i>ComparisonOperator</i>, see
        /// <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LegacyConditionalParameters.html">Legacy
        /// Conditional Parameters</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  
        /// <para>
        /// For backward compatibility with previous DynamoDB releases, the following parameters
        /// can be used instead of <i>AttributeValueList</i> and <i>ComparisonOperator</i>:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>Value</i> - A value for DynamoDB to compare with an attribute.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>Exists</i> - A Boolean value that causes DynamoDB to evaluate the value before
        /// attempting the conditional operation:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// If <i>Exists</i> is <code>true</code>, DynamoDB will check to see if that attribute
        /// value already exists in the table. If it is found, then the condition evaluates to
        /// true; otherwise the condition evaluate to false.
        /// </para>
        ///  </li> <li>
        /// <para>
        /// If <i>Exists</i> is <code>false</code>, DynamoDB assumes that the attribute value
        /// does <i>not</i> exist in the table. If in fact the value does not exist, then the
        /// assumption is valid and the condition evaluates to true. If the value is found, despite
        /// the assumption that it does not exist, the condition evaluates to false.
        /// </para>
        /// </li> </ul> 
        /// <para>
        /// Note that the default value for <i>Exists</i> is <code>true</code>.
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// The <i>Value</i> and <i>Exists</i> parameters are incompatible with <i>AttributeValueList</i>
        /// and <i>ComparisonOperator</i>. Note that if you use both sets of parameters at once,
        /// DynamoDB will return a <i>ValidationException</i> exception.
        /// </para>
        ///  <note>
        /// <para>
        /// This parameter does not support attributes of type List or Map.
        /// </para>
        /// </note>
        /// </summary>
        public Dictionary<string, ExpectedAttributeValue> Expected
        {
            get { return this._expected; }
            set { this._expected = value; }
        }

        // Check to see if Expected property is set
        internal bool IsSetExpected()
        {
            return this._expected != null && this._expected.Count > 0; 
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
        /// Gets and sets the property Key. 
        /// <para>
        /// The primary key of the item to be updated. Each element consists of an attribute name
        /// and a value for that attribute.
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

        /// <summary>
        /// Gets and sets the property ReturnValues. 
        /// <para>
        /// Use <i>ReturnValues</i> if you want to get the item attributes as they appeared either
        /// before or after they were updated. For <i>UpdateItem</i>, the valid values are:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <code>NONE</code> - If <i>ReturnValues</i> is not specified, or if its value is <code>NONE</code>,
        /// then nothing is returned. (This setting is the default for <i>ReturnValues</i>.)
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>ALL_OLD</code> - If <i>UpdateItem</i> overwrote an attribute name-value pair,
        /// then the content of the old item is returned.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>UPDATED_OLD</code> - The old versions of only the updated attributes are returned.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>ALL_NEW</code> - All of the attributes of the new version of the item are returned.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>UPDATED_NEW</code> - The new versions of only the updated attributes are returned.
        /// </para>
        ///  </li> </ul>
        /// </summary>
        public ReturnValue ReturnValues
        {
            get { return this._returnValues; }
            set { this._returnValues = value; }
        }

        // Check to see if ReturnValues property is set
        internal bool IsSetReturnValues()
        {
            return this._returnValues != null;
        }

        /// <summary>
        /// Gets and sets the property TableName. 
        /// <para>
        /// The name of the table containing the item to update. 
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
        /// Gets and sets the property UpdateExpression. 
        /// <para>
        /// An expression that defines one or more attributes to be updated, the action to be
        /// performed on them, and new value(s) for them.
        /// </para>
        ///  
        /// <para>
        /// The following action values are available for <i>UpdateExpression</i>.
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <code>SET</code> - Adds one or more attributes and values to an item. If any of these
        /// attribute already exist, they are replaced by the new values. You can also use <code>SET</code>
        /// to add or subtract from an attribute that is of type Number. For example: <code>SET
        /// myNum = myNum + :val</code>
        /// </para>
        ///  
        /// <para>
        /// <code>SET</code> supports the following functions:
        /// </para>
        ///  <ul> <li>
        /// <para>
        /// <code>if_not_exists (path, operand)</code> - if the item does not contain an attribute
        /// at the specified path, then <code>if_not_exists</code> evaluates to operand; otherwise,
        /// it evaluates to path. You can use this function to avoid overwriting an attribute
        /// that may already be present in the item.
        /// </para>
        /// </li> <li>
        /// <para>
        /// <code>list_append (operand, operand)</code> - evaluates to a list with a new element
        /// added to it. You can append the new element to the start or the end of the list by
        /// reversing the order of the operands.
        /// </para>
        /// </li> </ul> 
        /// <para>
        /// These function names are case-sensitive.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>REMOVE</code> - Removes one or more attributes from an item.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>ADD</code> - Adds the specified value to the item, if the attribute does not
        /// already exist. If the attribute does exist, then the behavior of <code>ADD</code>
        /// depends on the data type of the attribute:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// If the existing attribute is a number, and if <i>Value</i> is also a number, then
        /// <i>Value</i> is mathematically added to the existing attribute. If <i>Value</i> is
        /// a negative number, then it is subtracted from the existing attribute.
        /// </para>
        ///  <note> 
        /// <para>
        /// If you use <code>ADD</code> to increment or decrement a number value for an item that
        /// doesn't exist before the update, DynamoDB uses <code>0</code> as the initial value.
        /// </para>
        ///  
        /// <para>
        /// Similarly, if you use <code>ADD</code> for an existing item to increment or decrement
        /// an attribute value that doesn't exist before the update, DynamoDB uses <code>0</code>
        /// as the initial value. For example, suppose that the item you want to update doesn't
        /// have an attribute named <i>itemcount</i>, but you decide to <code>ADD</code> the number
        /// <code>3</code> to this attribute anyway. DynamoDB will create the <i>itemcount</i>
        /// attribute, set its initial value to <code>0</code>, and finally add <code>3</code>
        /// to it. The result will be a new <i>itemcount</i> attribute in the item, with a value
        /// of <code>3</code>.
        /// </para>
        ///  </note> </li> <li> 
        /// <para>
        /// If the existing data type is a set and if <i>Value</i> is also a set, then <i>Value</i>
        /// is added to the existing set. For example, if the attribute value is the set <code>[1,2]</code>,
        /// and the <code>ADD</code> action specified <code>[3]</code>, then the final attribute
        /// value is <code>[1,2,3]</code>. An error occurs if an <code>ADD</code> action is specified
        /// for a set attribute and the attribute type specified does not match the existing set
        /// type. 
        /// </para>
        ///  
        /// <para>
        /// Both sets must have the same primitive data type. For example, if the existing data
        /// type is a set of strings, the <i>Value</i> must also be a set of strings.
        /// </para>
        ///  </li> </ul> <important>
        /// <para>
        /// The <code>ADD</code> action only supports Number and set data types. In addition,
        /// <code>ADD</code> can only be used on top-level attributes, not nested attributes.
        /// </para>
        ///  </important> </li> <li> 
        /// <para>
        /// <code>DELETE</code> - Deletes an element from a set.
        /// </para>
        ///  
        /// <para>
        /// If a set of values is specified, then those values are subtracted from the old set.
        /// For example, if the attribute value was the set <code>[a,b,c]</code> and the <code>DELETE</code>
        /// action specifies <code>[a,c]</code>, then the final attribute value is <code>[b]</code>.
        /// Specifying an empty set is an error.
        /// </para>
        ///  <important>
        /// <para>
        /// The <code>DELETE</code> action only supports set data types. In addition, <code>DELETE</code>
        /// can only be used on top-level attributes, not nested attributes.
        /// </para>
        ///  </important> </li> </ul> 
        /// <para>
        /// You can have many actions in a single expression, such as the following: <code>SET
        /// a=:value1, b=:value2 DELETE :value3, :value4, :value5</code>
        /// </para>
        ///  
        /// <para>
        /// For more information on update expressions, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Expressions.Modifying.html">Modifying
        /// Items and Attributes</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  <note>
        /// <para>
        /// <i>UpdateExpression</i> replaces the legacy <i>AttributeUpdates</i> parameter.
        /// </para>
        /// </note>
        /// </summary>
        public string UpdateExpression
        {
            get { return this._updateExpression; }
            set { this._updateExpression = value; }
        }

        // Check to see if UpdateExpression property is set
        internal bool IsSetUpdateExpression()
        {
            return this._updateExpression != null;
        }

    }
}