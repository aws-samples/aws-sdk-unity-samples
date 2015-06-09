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
    /// Container for the parameters to the UnlinkIdentity operation.
    /// Unlinks a federated identity from an existing account. Unlinked logins will be considered
    /// new identities next time they are seen. Removing the last linked login will make this
    /// identity inaccessible.
    /// </summary>
    public partial class UnlinkIdentityRequest : AmazonCognitoIdentityRequest
    {
        private string _identityId;
        private Dictionary<string, string> _logins = new Dictionary<string, string>();
        private List<string> _loginsToRemove = new List<string>();

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

        /// <summary>
        /// Gets and sets the property LoginsToRemove. Provider names to unlink from this identity.
        /// </summary>
        public List<string> LoginsToRemove
        {
            get { return this._loginsToRemove; }
            set { this._loginsToRemove = value; }
        }

        // Check to see if LoginsToRemove property is set
        internal bool IsSetLoginsToRemove()
        {
            return this._loginsToRemove != null && this._loginsToRemove.Count > 0; 
        }

    }
}
