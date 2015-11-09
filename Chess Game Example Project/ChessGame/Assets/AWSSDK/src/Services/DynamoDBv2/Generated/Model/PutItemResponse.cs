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
    /// Represents the output of a <i>PutItem</i> operation.
    /// </summary>
    public partial class PutItemResponse : AmazonWebServiceResponse
    {
        private Dictionary<string, AttributeValue> _attributes = new Dictionary<string, AttributeValue>();
        private ConsumedCapacity _consumedCapacity;
        private ItemCollectionMetrics _itemCollectionMetrics;

        /// <summary>
        /// Gets and sets the property Attributes. 
        /// <para>
        /// The attribute values as they appeared before the <i>PutItem</i> operation, but only
        /// if <i>ReturnValues</i> is specified as <code>ALL_OLD</code> in the request. Each element
        /// consists of an attribute name and an attribute value.
        /// </para>
        /// </summary>
        public Dictionary<string, AttributeValue> Attributes
        {
            get { return this._attributes; }
            set { this._attributes = value; }
        }

        // Check to see if Attributes property is set
        internal bool IsSetAttributes()
        {
            return this._attributes != null && this._attributes.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property ConsumedCapacity.
        /// </summary>
        public ConsumedCapacity ConsumedCapacity
        {
            get { return this._consumedCapacity; }
            set { this._consumedCapacity = value; }
        }

        // Check to see if ConsumedCapacity property is set
        internal bool IsSetConsumedCapacity()
        {
            return this._consumedCapacity != null;
        }

        /// <summary>
        /// Gets and sets the property ItemCollectionMetrics. 
        /// <para>
        /// Information about item collections, if any, that were affected by the operation. <i>ItemCollectionMetrics</i>
        /// is only returned if the request asked for it. If the table does not have any local
        /// secondary indexes, this information is not returned in the response.
        /// </para>
        ///  
        /// <para>
        /// Each <i>ItemCollectionMetrics</i> element consists of:
        /// </para>
        ///  <ul> <li>
        /// <para>
        /// <i>ItemCollectionKey</i> - The hash key value of the item collection. This is the
        /// same as the hash key of the item.
        /// </para>
        /// </li> <li>
        /// <para>
        /// <i>SizeEstimateRange</i> - An estimate of item collection size, in gigabytes. This
        /// value is a two-element array containing a lower bound and an upper bound for the estimate.
        /// The estimate includes the size of all the items in the table, plus the size of all
        /// attributes projected into all of the local secondary indexes on that table. Use this
        /// estimate to measure whether a local secondary index is approaching its size limit.
        /// </para>
        ///  
        /// <para>
        /// The estimate is subject to change over time; therefore, do not rely on the precision
        /// or accuracy of the estimate.
        /// </para>
        ///  </li> </ul>
        /// </summary>
        public ItemCollectionMetrics ItemCollectionMetrics
        {
            get { return this._itemCollectionMetrics; }
            set { this._itemCollectionMetrics = value; }
        }

        // Check to see if ItemCollectionMetrics property is set
        internal bool IsSetItemCollectionMetrics()
        {
            return this._itemCollectionMetrics != null;
        }

    }
}