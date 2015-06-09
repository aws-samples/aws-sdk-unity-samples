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
 * Do not modify this file. This file is generated from the cognito-sync-2014-06-30.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.CognitoSync.Model
{
    /// <summary>
    /// Response to a successful DescribeIdentityPoolUsage request.
    /// </summary>
    public partial class DescribeIdentityPoolUsageResult : AmazonWebServiceResponse
    {
        private IdentityPoolUsage _identityPoolUsage;

        /// <summary>
        /// Gets and sets the property IdentityPoolUsage. Information about the usage of the identity
        /// pool.
        /// </summary>
        public IdentityPoolUsage IdentityPoolUsage
        {
            get { return this._identityPoolUsage; }
            set { this._identityPoolUsage = value; }
        }

        // Check to see if IdentityPoolUsage property is set
        internal bool IsSetIdentityPoolUsage()
        {
            return this._identityPoolUsage != null;
        }

    }
}
