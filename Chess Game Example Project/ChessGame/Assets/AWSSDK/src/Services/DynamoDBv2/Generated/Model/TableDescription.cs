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
    /// Represents the properties of a table.
    /// </summary>
    public partial class TableDescription
    {
        private List<AttributeDefinition> _attributeDefinitions = new List<AttributeDefinition>();
        private DateTime? _creationDateTime;
        private List<GlobalSecondaryIndexDescription> _globalSecondaryIndexes = new List<GlobalSecondaryIndexDescription>();
        private long? _itemCount;
        private List<KeySchemaElement> _keySchema = new List<KeySchemaElement>();
        private string _latestStreamArn;
        private string _latestStreamLabel;
        private List<LocalSecondaryIndexDescription> _localSecondaryIndexes = new List<LocalSecondaryIndexDescription>();
        private ProvisionedThroughputDescription _provisionedThroughput;
        private StreamSpecification _streamSpecification;
        private string _tableArn;
        private string _tableName;
        private long? _tableSizeBytes;
        private TableStatus _tableStatus;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public TableDescription() { }

        /// <summary>
        /// Gets and sets the property AttributeDefinitions. 
        /// <para>
        /// An array of <i>AttributeDefinition</i> objects. Each of these objects describes one
        /// attribute in the table and index key schema.
        /// </para>
        ///  
        /// <para>
        /// Each <i>AttributeDefinition</i> object in this array is composed of:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>AttributeName</i> - The name of the attribute.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>AttributeType</i> - The data type for the attribute.
        /// </para>
        ///  </li> </ul>
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
        /// Gets and sets the property CreationDateTime. 
        /// <para>
        /// The date and time when the table was created, in <a href="http://www.epochconverter.com/">UNIX
        /// epoch time</a> format.
        /// </para>
        /// </summary>
        public DateTime CreationDateTime
        {
            get { return this._creationDateTime.GetValueOrDefault(); }
            set { this._creationDateTime = value; }
        }

        // Check to see if CreationDateTime property is set
        internal bool IsSetCreationDateTime()
        {
            return this._creationDateTime.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property GlobalSecondaryIndexes. 
        /// <para>
        /// The global secondary indexes, if any, on the table. Each index is scoped to a given
        /// hash key value. Each element is composed of:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>Backfilling</i> - If true, then the index is currently in the backfilling phase.
        /// Backfilling occurs only when a new global secondary index is added to the table; it
        /// is the process by which DynamoDB populates the new index with data from the table.
        /// (This attribute does not appear for indexes that were created during a <i>CreateTable</i>
        /// operation.)
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>IndexName</i> - The name of the global secondary index.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>IndexSizeBytes</i> - The total size of the global secondary index, in bytes. DynamoDB
        /// updates this value approximately every six hours. Recent changes might not be reflected
        /// in this value. 
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>IndexStatus</i> - The current status of the global secondary index:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>CREATING</i> - The index is being created.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>UPDATING</i> - The index is being updated.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>DELETING</i> - The index is being deleted.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>ACTIVE</i> - The index is ready for use.
        /// </para>
        ///  </li> </ul> </li> <li> 
        /// <para>
        /// <i>ItemCount</i> - The number of items in the global secondary index. DynamoDB updates
        /// this value approximately every six hours. Recent changes might not be reflected in
        /// this value. 
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>KeySchema</i> - Specifies the complete index key schema. The attribute names in
        /// the key schema must be between 1 and 255 characters (inclusive). The key schema must
        /// begin with the same hash key attribute as the table.
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
        /// secondary index, consisting of read and write capacity units, along with data about
        /// increases and decreases. 
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// If the table is in the <code>DELETING</code> state, no information about indexes will
        /// be returned.
        /// </para>
        /// </summary>
        public List<GlobalSecondaryIndexDescription> GlobalSecondaryIndexes
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
        /// Gets and sets the property ItemCount. 
        /// <para>
        /// The number of items in the specified table. DynamoDB updates this value approximately
        /// every six hours. Recent changes might not be reflected in this value. 
        /// </para>
        /// </summary>
        public long ItemCount
        {
            get { return this._itemCount.GetValueOrDefault(); }
            set { this._itemCount = value; }
        }

        // Check to see if ItemCount property is set
        internal bool IsSetItemCount()
        {
            return this._itemCount.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property KeySchema. 
        /// <para>
        /// The primary key structure for the table. Each <i>KeySchemaElement</i> consists of:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>AttributeName</i> - The name of the attribute.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>KeyType</i> - The key type for the attribute. Can be either <code>HASH</code> or
        /// <code>RANGE</code>.
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// For more information about primary keys, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataModel.html#DataModelPrimaryKey">Primary
        /// Key</a> in the <i>Amazon DynamoDB Developer Guide</i>.
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
        /// Gets and sets the property LatestStreamArn. 
        /// <para>
        /// The Amazon Resource Name (ARN) that uniquely identifies the latest stream for this
        /// table.
        /// </para>
        /// </summary>
        public string LatestStreamArn
        {
            get { return this._latestStreamArn; }
            set { this._latestStreamArn = value; }
        }

        // Check to see if LatestStreamArn property is set
        internal bool IsSetLatestStreamArn()
        {
            return this._latestStreamArn != null;
        }

        /// <summary>
        /// Gets and sets the property LatestStreamLabel. 
        /// <para>
        /// A timestamp, in ISO 8601 format, for this stream.
        /// </para>
        ///  
        /// <para>
        /// Note that <i>LatestStreamLabel</i> is not a unique identifier for the stream, because
        /// it is possible that a stream from another table might have the same timestamp. However,
        /// the combination of the following three elements is guaranteed to be unique:
        /// </para>
        ///  <ul> <li>
        /// <para>
        /// the AWS customer ID.
        /// </para>
        /// </li> <li>
        /// <para>
        /// the table name.
        /// </para>
        /// </li> <li>
        /// <para>
        /// the <i>StreamLabel</i>.
        /// </para>
        /// </li> </ul>
        /// </summary>
        public string LatestStreamLabel
        {
            get { return this._latestStreamLabel; }
            set { this._latestStreamLabel = value; }
        }

        // Check to see if LatestStreamLabel property is set
        internal bool IsSetLatestStreamLabel()
        {
            return this._latestStreamLabel != null;
        }

        /// <summary>
        /// Gets and sets the property LocalSecondaryIndexes. 
        /// <para>
        /// Represents one or more local secondary indexes on the table. Each index is scoped
        /// to a given hash key value. Tables with one or more local secondary indexes are subject
        /// to an item collection size limit, where the amount of data within a given item collection
        /// cannot exceed 10 GB. Each element is composed of:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>IndexName</i> - The name of the local secondary index.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>KeySchema</i> - Specifies the complete index key schema. The attribute names in
        /// the key schema must be between 1 and 255 characters (inclusive). The key schema must
        /// begin with the same hash key attribute as the table.
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
        /// <i>IndexSizeBytes</i> - Represents the total size of the index, in bytes. DynamoDB
        /// updates this value approximately every six hours. Recent changes might not be reflected
        /// in this value.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>ItemCount</i> - Represents the number of items in the index. DynamoDB updates this
        /// value approximately every six hours. Recent changes might not be reflected in this
        /// value.
        /// </para>
        ///  </li> </ul> 
        /// <para>
        /// If the table is in the <code>DELETING</code> state, no information about indexes will
        /// be returned.
        /// </para>
        /// </summary>
        public List<LocalSecondaryIndexDescription> LocalSecondaryIndexes
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
        /// <para>
        /// The provisioned throughput settings for the table, consisting of read and write capacity
        /// units, along with data about increases and decreases.
        /// </para>
        /// </summary>
        public ProvisionedThroughputDescription ProvisionedThroughput
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
        /// The current DynamoDB Streams configuration for the table.
        /// </para>
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
        /// Gets and sets the property TableArn. 
        /// <para>
        /// The Amazon Resource Name (ARN) that uniquely identifies the table.
        /// </para>
        /// </summary>
        public string TableArn
        {
            get { return this._tableArn; }
            set { this._tableArn = value; }
        }

        // Check to see if TableArn property is set
        internal bool IsSetTableArn()
        {
            return this._tableArn != null;
        }

        /// <summary>
        /// Gets and sets the property TableName. 
        /// <para>
        /// The name of the table.
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
        /// Gets and sets the property TableSizeBytes. 
        /// <para>
        /// The total size of the specified table, in bytes. DynamoDB updates this value approximately
        /// every six hours. Recent changes might not be reflected in this value. 
        /// </para>
        /// </summary>
        public long TableSizeBytes
        {
            get { return this._tableSizeBytes.GetValueOrDefault(); }
            set { this._tableSizeBytes = value; }
        }

        // Check to see if TableSizeBytes property is set
        internal bool IsSetTableSizeBytes()
        {
            return this._tableSizeBytes.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property TableStatus. 
        /// <para>
        /// The current state of the table:
        /// </para>
        ///  <ul> <li> 
        /// <para>
        /// <i>CREATING</i> - The table is being created.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>UPDATING</i> - The table is being updated.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>DELETING</i> - The table is being deleted.
        /// </para>
        ///  </li> <li> 
        /// <para>
        /// <i>ACTIVE</i> - The table is ready for use.
        /// </para>
        ///  </li> </ul>
        /// </summary>
        public TableStatus TableStatus
        {
            get { return this._tableStatus; }
            set { this._tableStatus = value; }
        }

        // Check to see if TableStatus property is set
        internal bool IsSetTableStatus()
        {
            return this._tableStatus != null;
        }

    }
}