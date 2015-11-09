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
 * Do not modify this file. This file is generated from the lambda-2015-03-31.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.Lambda.Model
{
    /// <summary>
    /// Container for the parameters to the GetEventSourceMapping operation.
    /// Returns configuration information for the specified event source mapping (see <a>CreateEventSourceMapping</a>).
    /// 
    ///  
    /// <para>
    /// This operation requires permission for the <code>lambda:GetEventSourceMapping</code>
    /// action.
    /// </para>
    /// </summary>
    public partial class GetEventSourceMappingRequest : AmazonLambdaRequest
    {
        private string _uuid;

        /// <summary>
        /// Gets and sets the property UUID. 
        /// <para>
        /// The AWS Lambda assigned ID of the event source mapping.
        /// </para>
        /// </summary>
        public string UUID
        {
            get { return this._uuid; }
            set { this._uuid = value; }
        }

        // Check to see if UUID property is set
        internal bool IsSetUUID()
        {
            return this._uuid != null;
        }

    }
}