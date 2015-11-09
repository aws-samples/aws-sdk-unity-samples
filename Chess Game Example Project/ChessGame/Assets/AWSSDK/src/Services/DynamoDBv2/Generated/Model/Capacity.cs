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
    /// Represents the amount of provisioned throughput capacity consumed on a table or an
    /// index.
    /// </summary>
    public partial class Capacity
    {
        private double? _capacityUnits;

        /// <summary>
        /// Gets and sets the property CapacityUnits. 
        /// <para>
        /// The total number of capacity units consumed on a table or an index.
        /// </para>
        /// </summary>
        public double CapacityUnits
        {
            get { return this._capacityUnits.GetValueOrDefault(); }
            set { this._capacityUnits = value; }
        }

        // Check to see if CapacityUnits property is set
        internal bool IsSetCapacityUnits()
        {
            return this._capacityUnits.HasValue; 
        }

    }
}