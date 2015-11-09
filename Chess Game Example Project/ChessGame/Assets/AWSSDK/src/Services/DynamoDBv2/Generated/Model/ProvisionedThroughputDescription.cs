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
    /// Represents the provisioned throughput settings for the table, consisting of read and
    /// write capacity units, along with data about increases and decreases.
    /// </summary>
    public partial class ProvisionedThroughputDescription
    {
        private DateTime? _lastDecreaseDateTime;
        private DateTime? _lastIncreaseDateTime;
        private long? _numberOfDecreasesToday;
        private long? _readCapacityUnits;
        private long? _writeCapacityUnits;

        /// <summary>
        /// Gets and sets the property LastDecreaseDateTime. 
        /// <para>
        /// The date and time of the last provisioned throughput decrease for this table.
        /// </para>
        /// </summary>
        public DateTime LastDecreaseDateTime
        {
            get { return this._lastDecreaseDateTime.GetValueOrDefault(); }
            set { this._lastDecreaseDateTime = value; }
        }

        // Check to see if LastDecreaseDateTime property is set
        internal bool IsSetLastDecreaseDateTime()
        {
            return this._lastDecreaseDateTime.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property LastIncreaseDateTime. 
        /// <para>
        /// The date and time of the last provisioned throughput increase for this table.
        /// </para>
        /// </summary>
        public DateTime LastIncreaseDateTime
        {
            get { return this._lastIncreaseDateTime.GetValueOrDefault(); }
            set { this._lastIncreaseDateTime = value; }
        }

        // Check to see if LastIncreaseDateTime property is set
        internal bool IsSetLastIncreaseDateTime()
        {
            return this._lastIncreaseDateTime.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property NumberOfDecreasesToday. 
        /// <para>
        /// The number of provisioned throughput decreases for this table during this UTC calendar
        /// day. For current maximums on provisioned throughput decreases, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Limits.html">Limits</a>
        /// in the <i>Amazon DynamoDB Developer Guide</i>.
        /// </para>
        /// </summary>
        public long NumberOfDecreasesToday
        {
            get { return this._numberOfDecreasesToday.GetValueOrDefault(); }
            set { this._numberOfDecreasesToday = value; }
        }

        // Check to see if NumberOfDecreasesToday property is set
        internal bool IsSetNumberOfDecreasesToday()
        {
            return this._numberOfDecreasesToday.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property ReadCapacityUnits. 
        /// <para>
        /// The maximum number of strongly consistent reads consumed per second before DynamoDB
        /// returns a <i>ThrottlingException</i>. Eventually consistent reads require less effort
        /// than strongly consistent reads, so a setting of 50 <i>ReadCapacityUnits</i> per second
        /// provides 100 eventually consistent <i>ReadCapacityUnits</i> per second.
        /// </para>
        /// </summary>
        public long ReadCapacityUnits
        {
            get { return this._readCapacityUnits.GetValueOrDefault(); }
            set { this._readCapacityUnits = value; }
        }

        // Check to see if ReadCapacityUnits property is set
        internal bool IsSetReadCapacityUnits()
        {
            return this._readCapacityUnits.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property WriteCapacityUnits. 
        /// <para>
        /// The maximum number of writes consumed per second before DynamoDB returns a <i>ThrottlingException</i>.
        /// </para>
        /// </summary>
        public long WriteCapacityUnits
        {
            get { return this._writeCapacityUnits.GetValueOrDefault(); }
            set { this._writeCapacityUnits = value; }
        }

        // Check to see if WriteCapacityUnits property is set
        internal bool IsSetWriteCapacityUnits()
        {
            return this._writeCapacityUnits.HasValue; 
        }

    }
}