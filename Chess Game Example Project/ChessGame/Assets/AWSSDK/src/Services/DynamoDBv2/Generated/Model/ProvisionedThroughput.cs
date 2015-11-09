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
    /// Represents the provisioned throughput settings for a specified table or index. The
    /// settings can be modified using the <i>UpdateTable</i> operation.
    /// 
    ///  
    /// <para>
    /// For current minimum and maximum provisioned throughput values, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Limits.html">Limits</a>
    /// in the <i>Amazon DynamoDB Developer Guide</i>.
    /// </para>
    /// </summary>
    public partial class ProvisionedThroughput
    {
        private long? _readCapacityUnits;
        private long? _writeCapacityUnits;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public ProvisionedThroughput() { }

        /// <summary>
        /// Instantiates ProvisionedThroughput with the parameterized properties
        /// </summary>
        /// <param name="readCapacityUnits">The maximum number of strongly consistent reads consumed per second before DynamoDB returns a <i>ThrottlingException</i>. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#ProvisionedThroughput">Specifying Read and Write Requirements</a> in the <i>Amazon DynamoDB Developer Guide</i>.</param>
        /// <param name="writeCapacityUnits">The maximum number of writes consumed per second before DynamoDB returns a <i>ThrottlingException</i>. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#ProvisionedThroughput">Specifying Read and Write Requirements</a> in the <i>Amazon DynamoDB Developer Guide</i>.</param>
        public ProvisionedThroughput(long readCapacityUnits, long writeCapacityUnits)
        {
            _readCapacityUnits = readCapacityUnits;
            _writeCapacityUnits = writeCapacityUnits;
        }

        /// <summary>
        /// Gets and sets the property ReadCapacityUnits. 
        /// <para>
        /// The maximum number of strongly consistent reads consumed per second before DynamoDB
        /// returns a <i>ThrottlingException</i>. For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#ProvisionedThroughput">Specifying
        /// Read and Write Requirements</a> in the <i>Amazon DynamoDB Developer Guide</i>.
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
        /// For more information, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithTables.html#ProvisionedThroughput">Specifying
        /// Read and Write Requirements</a> in the <i>Amazon DynamoDB Developer Guide</i>.
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