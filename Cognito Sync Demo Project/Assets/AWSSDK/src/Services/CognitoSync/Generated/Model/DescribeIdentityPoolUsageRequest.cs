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
    /// Container for the parameters to the DescribeIdentityPoolUsage operation.
    /// Gets usage details (for example, data storage) about a particular identity pool.
    /// 
    ///  
    /// <para>
    /// DescribeIdentityPoolUsage can only be called with developer credentials. You cannot
    /// make this API call with the temporary user credentials provided by Cognito Identity.
    /// </para>
    /// </summary>
    public partial class DescribeIdentityPoolUsageRequest : AmazonCognitoSyncRequest
    {
        private string _identityPoolId;

        /// <summary>
        /// Gets and sets the property IdentityPoolId. A name-spaced GUID (for example, us-east-1:23EC4050-6AEA-7089-A2DD-08002EXAMPLE)
        /// created by Amazon Cognito. GUID generation is unique within a region.
        /// </summary>
        public string IdentityPoolId
        {
            get { return this._identityPoolId; }
            set { this._identityPoolId = value; }
        }

        // Check to see if IdentityPoolId property is set
        internal bool IsSetIdentityPoolId()
        {
            return this._identityPoolId != null;
        }

    }
}
