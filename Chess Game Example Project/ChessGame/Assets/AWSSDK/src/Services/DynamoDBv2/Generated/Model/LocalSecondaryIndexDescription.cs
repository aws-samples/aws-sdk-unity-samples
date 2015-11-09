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
    /// Represents the properties of a local secondary index.
    /// </summary>
    public partial class LocalSecondaryIndexDescription
    {
        private string _indexArn;
        private string _indexName;
        private long? _indexSizeBytes;
        private long? _itemCount;
        private List<KeySchemaElement> _keySchema = new List<KeySchemaElement>();
        private Projection _projection;

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
        /// Represents the name of the local secondary index.
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
        /// The complete index key schema, which consists of one or more pairs of attribute names
        /// and key types (<code>HASH</code> or <code>RANGE</code>).
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

    }
}