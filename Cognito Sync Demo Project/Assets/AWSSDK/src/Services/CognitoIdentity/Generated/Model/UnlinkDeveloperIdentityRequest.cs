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
    /// Container for the parameters to the UnlinkDeveloperIdentity operation.
    /// Unlinks a <code>DeveloperUserIdentifier</code> from an existing identity. Unlinked
    /// developer users will be considered new identities next time they are seen. If, for
    /// a given Cognito identity, you remove all federated identities as well as the developer
    /// user identifier, the Cognito identity becomes inaccessible.
    /// </summary>
    public partial class UnlinkDeveloperIdentityRequest : AmazonCognitoIdentityRequest
    {
        private string _developerProviderName;
        private string _developerUserIdentifier;
        private string _identityId;
        private string _identityPoolId;

        /// <summary>
        /// Gets and sets the property DeveloperProviderName. 
        /// <para>
        /// The "domain" by which Cognito will refer to your users.
        /// </para>
        /// </summary>
        public string DeveloperProviderName
        {
            get { return this._developerProviderName; }
            set { this._developerProviderName = value; }
        }

        // Check to see if DeveloperProviderName property is set
        internal bool IsSetDeveloperProviderName()
        {
            return this._developerProviderName != null;
        }

        /// <summary>
        /// Gets and sets the property DeveloperUserIdentifier. A unique ID used by your backend
        /// authentication process to identify a user.
        /// </summary>
        public string DeveloperUserIdentifier
        {
            get { return this._developerUserIdentifier; }
            set { this._developerUserIdentifier = value; }
        }

        // Check to see if DeveloperUserIdentifier property is set
        internal bool IsSetDeveloperUserIdentifier()
        {
            return this._developerUserIdentifier != null;
        }

        /// <summary>
        /// Gets and sets the property IdentityId. 
        /// <para>
        /// A unique identifier in the format REGION:GUID.
        /// </para>
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
        /// Gets and sets the property IdentityPoolId. 
        /// <para>
        /// An identity pool ID in the format REGION:GUID.
        /// </para>
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
