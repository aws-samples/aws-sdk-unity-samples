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
 * Do not modify this file. This file is generated from the cognito-identity-2014-06-30.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.CognitoIdentity.Model
{
    /// <summary>
    /// A description of the identity pool.
    /// </summary>
    public partial class IdentityPoolShortDescription
    {
        private string _identityPoolId;
        private string _identityPoolName;

        /// <summary>
        /// Gets and sets the property IdentityPoolId. An identity pool ID in the format REGION:GUID.
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

        /// <summary>
        /// Gets and sets the property IdentityPoolName. A string that you provide.
        /// </summary>
        public string IdentityPoolName
        {
            get { return this._identityPoolName; }
            set { this._identityPoolName = value; }
        }

        // Check to see if IdentityPoolName property is set
        internal bool IsSetIdentityPoolName()
        {
            return this._identityPoolName != null;
        }

    }
}
