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
    /// Represents the properties of a global secondary index.
    /// </summary>
    public partial class GlobalSecondaryIndexDescription
    {
        private bool? _backfilling;
        private string _indexArn;
        private string _indexName;
        private long? _indexSizeBytes;
        private IndexStatus _indexStatus;
        private long? _itemCount;
        private List<KeySchemaElement> _keySchema = new List<KeySchemaElement>();
        private Projection _projection;
        private ProvisionedThroughputDescription _provisionedThroughput;

        /// <summary>
        /// Gets and sets the property Backfilling. 
        /// <para>
        /// Indicates whether the index is currently backfilling. <i>Backfilling</i> is the process
        /// of reading items from the table and determining whether they can be added to the index.
        /// (Not all items will qualify: For example, a hash key attribute cannot have any duplicates.)
        /// If an item can be added to the index, DynamoDB will do so. After all items have been
        /// processed, the backfilling operation is complete and <i>Backfilling</i> is false.
        /// </para>
        ///  <note>
        /// <para>
        /// For indexes that were created during a <i>CreateTable</i> operation, the <i>Backfilling</i>
        /// attribute does not appear in the <i>DescribeTable</i> output.
        /// </para>
        /// </note>
        /// </summary>
        public bool Backfilling
        {
            get { return this._backfilling.GetValueOrDefault(); }
            set { this._backfilling = value; }
        }

        // Check to see if Backfilling property is set
        internal bool IsSetBackfilling()
        {
            return this._backfilling.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property IndexArn. 
        /// <para>
        /// The Amazon Resource Name (ARN) that uniquely identifies the index.
        /// </para>
        /// </summary>
        public string IndexArn
        {
            get { return this._indexArn; }
            set { this._indexArn = value; }
        }

        // Check to see if IndexArn property is set
        internal bool IsSetIndexArn()
        {
            return this._indexArn != null;
        }

        /// <summary>
        /// Gets and sets the property IndexName. 
        /// <para>
        /// The name of the global secondary index.
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
        /// Gets and sets the property IndexSizeBytes. 
        /// <para>
        /// The total size of the specified index, in bytes. DynamoDB updates this value approximately
        /// every six hours. Recent changes might not be reflected in this value. 
        /// </para>
        /// </summary>
        public long IndexSizeBytes
        {
            get { return this._indexSizeBytes.GetValueOrDefault(); }
            set { this._indexSizeBytes = value; }
        }

        // Check to see if IndexSizeBytes property is set
        internal bool IsSetIndexSizeBytes()
        {
            return this._indexSizeBytes.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property IndexStatus. 
        /// <para>
        /// The current state of the global secondary index:
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
        ///  </li> </ul>
        /// </summary>
        public IndexStatus IndexStatus
        {
            get { return this._indexStatus; }
            set { this._indexStatus = value; }
        }

        // Check to see if IndexStatus property is set
        internal bool IsSetIndexStatus()
        {
            return this._indexStatus != null;
        }

        /// <summary>
        /// Gets and sets the property ItemCount. 
        /// <para>
        /// The number of items in the specified index. DynamoDB updates this value approximately
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
        /// The complete key schema for the global secondary index, consisting of one or more
        /// pairs of attribute names and key types (<code>HASH</code> or <code>RANGE</code>).
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
        /// Gets and sets the property Projection.
        /// </summary>
        public Projection Projection
        {
            get { return this._projection; }
            set { this._projection = value; }
        }

        // Check to see if Projection property is set
        internal bool IsSetProjection()
        {
            return this._projection != null;
        }

        /// <summary>
        /// Gets and sets the property ProvisionedThroughput.
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

    }
}