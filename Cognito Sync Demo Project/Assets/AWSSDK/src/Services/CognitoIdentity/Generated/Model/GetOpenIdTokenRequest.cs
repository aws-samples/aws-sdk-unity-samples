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
    /// Container for the parameters to the GetOpenIdToken operation.
    /// Gets an OpenID token, using a known Cognito ID. This known Cognito ID is returned
    /// by <a>GetId</a>. You can optionally add additional logins for the identity. Supplying
    /// multiple logins creates an implicit link.
    /// 
    ///  
    /// <para>
    /// The OpenId token is valid for 15 minutes.
    /// </para>
    /// </summary>
    public partial class GetOpenIdTokenRequest : AmazonCognitoIdentityRequest
    {
        private string _identityId;
        private Dictionary<string, string> _logins = new Dictionary<string, string>();

        /// <summary>
        /// Gets and sets the property IdentityId. A unique identifier in the format REGION:GUID.
        /// </summary>
        public string IdentityId
        {
            get { return this._identityId; }
            set { this._identityId = value; }
        }

        // Check to see if IdentityId property is set
        internal bool IsSetIdentityId()
        {
            return this._identityId != null;
        }

        /// <summary>
        /// Gets and sets the property Logins. A set of optional name-value pairs that map provider
        /// names to provider tokens.
        /// </summary>
        public Dictionary<string, string> Logins
        {
            get { return this._logins; }
            set { this._logins = value; }
        }

        // Check to see if Logins property is set
        internal bool IsSetLogins()
        {
            return this._logins != null && this._logins.Count > 0; 
        }

    }
}
