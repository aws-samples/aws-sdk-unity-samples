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
    /// Container for the parameters to the CreateTable operation.
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
    public partial class CreateTableRequest : AmazonDynamoDBRequest
    {
        private List<AttributeDefinition> _attributeDefinitions = new List<AttributeDefinition>();
        private List<GlobalSecondaryIndex> _globalSecondaryIndexes = new List<GlobalSecondaryIndex>();
        private List<KeySchemaElement> _keySchema = new List<KeySchemaElement>();
        private List<LocalSecondaryIndex> _localSecondaryIndexes = new List<LocalSecondaryIndex>();
        private ProvisionedThroughput _provisionedThroughput;
        private StreamSpecification _streamSpecification;
        private string _tableName;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public CreateTableRequest() { }

        /// <summary>
        /// Instantiates CreateTableRequest with the parameterized properties
        /// </summary>
        /// <param name="tableName">The name of the table to create.</param>
        /// <param name="keySchema">Specifies the attributes that make up the primary key for a table or an index. The attributes in <i>KeySchema</i> must also be defined in the <i>AttributeDefinitions</i> array. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataModel.html">Data Model</a> in the <i>Amazon DynamoDB Developer Guide</i>. Each <i>KeySchemaElement</i> in the array is composed of: <ul> <li> <i>AttributeName</i> - The name of this key attribute. </li> <li> <i>KeyType</i> - Determines whether the key attribute is <code>HASH</code> or <code>RANGE</code>. </li> </ul> For a primary key that consists of a hash attribute, you must provide exactly one element with a <i>KeyType</i> of <code>HASH</code>. For a primary key that consists of hash and range attributes, you must provide exactly two elements, in this order: The first element must have a <i>KeyType</i> of <code>HASH</code>, and the second element must have a <i>KeyType</i> of <code>RANGE</code>. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#WorkingWithTables.primary.key">Specifying the Primary Key</a> in the <i>Amazon DynamoDB Developer Guide</i>.</param>
        public CreateTableRequest(string tableName, List<KeySchemaElement> keySchema)
        {
            _tableName = tableName;
            _keySchema = keySchema;
        }

        /// <summary>
        /// Instantiates CreateTableRequest with the parameterized properties
        /// </summary>
        /// <param name="tableName">The name of the table to create.</param>
        /// <param name="keySchema">Specifies the attributes that make up the primary key for a table or an index. The attributes in <i>KeySchema</i> must also be defined in the <i>AttributeDefinitions</i> array. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataModel.html">Data Model</a> in the <i>Amazon DynamoDB Developer Guide</i>. Each <i>KeySchemaElement</i> in the array is composed of: <ul> <li> <i>AttributeName</i> - The name of this key attribute. </li> <li> <i>KeyType</i> - Determines whether the key attribute is <code>HASH</code> or <code>RANGE</code>. </li> </ul> For a primary key that consists of a hash attribute, you must provide exactly one element with a <i>KeyType</i> of <code>HASH</code>. For a primary key that consists of hash and range attributes, you must provide exactly two elements, in this order: The first element must have a <i>KeyType</i> of <code>HASH</code>, and the second element must have a <i>KeyType</i> of <code>RANGE</code>. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#WorkingWithTables.primary.key">Specifying the Primary Key</a> in the <i>Amazon DynamoDB Developer Guide</i>.</param>
        /// <param name="attributeDefinitions">An array of attributes that describe the key schema for the table and indexes.</param>
        /// <param name="provisionedThroughput">Sets the CreateTableRequest ProvisionedThroughput property</param>
        public CreateTableRequest(string tableName, List<KeySchemaElement> keySchema, List<AttributeDefinition> attributeDefinitions, ProvisionedThroughput provisionedThroughput)
        {
            _tableName = tableName;
            _keySchema = keySchema;
            _attributeDefinitions = attributeDefinitions;
            _provisionedThroughput = provisionedThroughput;
        }

        /// <summary>
        /// Gets and sets the property AttributeDefinitions. 
        /// <para>
        /// An array of attributes that describe the key schema for the table and indexes.
        /// </para>
        /// </summary>
        public List<AttributeDefinition> AttributeDefinitions
        {
            get { return this._attributeDefinitions; }
            set { this._attributeDefinitions = value; }
        }

        // Check to see if AttributeDefinitions property is set
        internal bool IsSetAttributeDefinitions()
        {
            return this._attributeDefinitions != null && this._attributeDefinitions.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property GlobalSecondaryIndexes. 
        /// <para>
        /// One or more global secondary indexes (the maximum is five) to be created on the table.
        /// Each global secondary index in the array includes the following:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>IndexName</i> - The name of the global secondary index. Must be unique only for
        /// this table.
        /// </para>
        ///  
        /// <para>
        /// 
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>KeySchema</i> - Specifies the key schema for the global secondary index.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>Projection</i> - Specifies attributes that are copied (projected) from the table
        /// into the index. These are in addition to the primary key attributes and index key
        /// attributes, which are automatically projected. Each attribute specification is composed
        /// of:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>ProjectionType</i> - One of the following:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <code>KEYS_ONLY</code> - Only the index and primary keys are projected into the index.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>INCLUDE</code> - Only the specified table attributes are projected into the
        /// index. The list of projected attributes are in <i>NonKeyAttributes</i>.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>ALL</code> - All of the table attributes are projected into the index.
        /// </para>
        ///  </li> </ul> </li> <li> 
        /// <para>
        /// <i>NonKeyAttributes</i> - A list of one or more non-key attribute names that are projected
        /// into the secondary index. The total count of attributes provided in <i>NonKeyAttributes</i>,
        /// summed across all of the secondary indexes, must not exceed 20. If you project the
        /// same attribute into two different indexes, this counts as two distinct attributes
        /// when determining the total.
        /// </para>
        ///  </li> </ul> </li> <li> 
        /// <para>
        /// <i>ProvisionedThroughput</i> - The provisioned throughput settings for the global
        /// secondary index, consisting of read and write capacity units.
        /// </para>
        ///  </li> </ul>
        /// </summary>
        public List<GlobalSecondaryIndex> GlobalSecondaryIndexes
        {
            get { return this._globalSecondaryIndexes; }
            set { this._globalSecondaryIndexes = value; }
        }

        // Check to see if GlobalSecondaryIndexes property is set
        internal bool IsSetGlobalSecondaryIndexes()
        {
            return this._globalSecondaryIndexes != null && this._globalSecondaryIndexes.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property KeySchema. 
        /// <para>
        /// Specifies the attributes that make up the primary key for a table or an index. The
        /// attributes in <i>KeySchema</i> must also be defined in the <i>AttributeDefinitions</i>
        /// array. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataModel.html">Data
        /// Model</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        ///  
        /// <para>
        /// Each <i>KeySchemaElement</i> in the array is composed of:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>AttributeName</i> - The name of this key attribute.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>KeyType</i> - Determines whether the key attribute is <code>HASH</code> or <code>RANGE</code>.
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// For a primary key that consists of a hash attribute, you must provide exactly one
        /// element with a <i>KeyType</i> of <code>HASH</code>.
        /// </para>
        ///  
        /// <para>
        /// For a primary key that consists of hash and range attributes, you must provide exactly
        /// two elements, in this order: The first element must have a <i>KeyType</i> of <code>HASH</code>,
        /// and the second element must have a <i>KeyType</i> of <code>RANGE</code>.
        /// </para>
        ///  
        /// <para>
        /// For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#WorkingWithTables.primary.key">Specifying
        /// the Primary Key</a> in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        public List<KeySchemaElement> KeySchema
        {
            get { return this._keySchema; }
            set { this._keySchema = value; }
        }

        // Check to see if KeySchema property is set
        internal bool IsSetKeySchema()
        {
            return this._keySchema != null && this._keySchema.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property LocalSecondaryIndexes. 
        /// <para>
        /// One or more local secondary indexes (the maximum is five) to be created on the table.
        /// Each index is scoped to a given hash key value. There is a 10 GB size limit per hash
        /// key; otherwise, the size of a local secondary index is unconstrained.
        /// </para>
        ///  
        /// <para>
        /// Each local secondary index in the array includes the following:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>IndexName</i> - The name of the local secondary index. Must be unique only for
        /// this table.
        /// </para>
        ///  
        /// <para>
        /// 
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>KeySchema</i> - Specifies the key schema for the local secondary index. The key
        /// schema must begin with the same hash key attribute as the table.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>Projection</i> - Specifies attributes that are copied (projected) from the table
        /// into the index. These are in addition to the primary key attributes and index key
        /// attributes, which are automatically projected. Each attribute specification is composed
        /// of:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>ProjectionType</i> - One of the following:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <code>KEYS_ONLY</code> - Only the index and primary keys are projected into the index.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>INCLUDE</code> - Only the specified table attributes are projected into the
        /// index. The list of projected attributes are in <i>NonKeyAttributes</i>.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <code>ALL</code> - All of the table attributes are projected into the index.
        /// </para>
        ///  </li> </ul> </li> <li> 
        /// <para>
        /// <i>NonKeyAttributes</i> - A list of one or more non-key attribute names that are projected
        /// into the secondary index. The total count of attributes provided in <i>NonKeyAttributes</i>,
        /// summed across all of the secondary indexes, must not exceed 20. If you project the
        /// same attribute into two different indexes, this counts as two distinct attributes
        /// when determining the total.
        /// </para>
        ///  </li> </ul> </li> </ul>
        /// </summary>
        public List<LocalSecondaryIndex> LocalSecondaryIndexes
        {
            get { return this._localSecondaryIndexes; }
            set { this._localSecondaryIndexes = value; }
        }

        // Check to see if LocalSecondaryIndexes property is set
        internal bool IsSetLocalSecondaryIndexes()
        {
            return this._localSecondaryIndexes != null && this._localSecondaryIndexes.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property ProvisionedThroughput.
        /// </summary>
        public ProvisionedThroughput ProvisionedThroughput
        {
            get { return this._provisionedThroughput; }
            set { this._provisionedThroughput = value; }
        }

        // Check to see if ProvisionedThroughput property is set
        internal bool IsSetProvisionedThroughput()
        {
            return this._provisionedThroughput != null;
        }

        /// <summary>
        /// Gets and sets the property StreamSpecification. 
        /// <para>
        /// The settings for DynamoDB Streams on the table. These settings consist of:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>StreamEnabled</i> - Indicates whether Streams is to be enabled (true) or disabled
        /// (false).
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>StreamViewType</i> - When an item in the table is modified, <i>StreamViewType</i>
        /// determines what information is written to the table's stream. Valid values for <i>StreamViewType</i>
        /// are:
        /// </para>
        ///  <ul> <li>
        /// <para>
        /// <i>KEYS_ONLY</i> - Only the key attributes of the modified item are written to the
        /// stream.
        /// </para>
        /// </li> <li>
        /// <para>
        /// <i>NEW_IMAGE</i> - The entire item, as it appears after it was modified, is written
        /// to the stream.
        /// </para>
        /// </li> <li>
        /// <para>
        /// <i>OLD_IMAGE</i> - The entire item, as it appeared before it was modified, is written
        /// to the stream.
        /// </para>
        /// </li> <li>
        /// <para>
        /// <i>NEW_AND_OLD_IMAGES</i> - Both the new and the old item images of the item are written
        /// to the stream.
        /// </para>
        /// </li> </ul> </li> </ul>
        /// </summary>
        public StreamSpecification StreamSpecification
        {
            get { return this._streamSpecification; }
            set { this._streamSpecification = value; }
        }

        // Check to see if StreamSpecification property is set
        internal bool IsSetStreamSpecification()
        {
            return this._streamSpecification != null;
        }

        /// <summary>
        /// Gets and sets the property TableName. 
        /// <para>
        /// The name of the table to create.
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