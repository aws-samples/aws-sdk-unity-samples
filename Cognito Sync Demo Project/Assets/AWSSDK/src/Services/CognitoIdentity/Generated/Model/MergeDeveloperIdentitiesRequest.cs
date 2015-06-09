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
    /// Container for the parameters to the MergeDeveloperIdentities operation.
    /// Merges two users having different <code>IdentityId</code>s, existing in the same identity
    /// pool, and identified by the same developer provider. You can use this action to request
    /// that discrete users be merged and identified as a single user in the Cognito environment.
    /// Cognito associates the given source user (<code>SourceUserIdentifier</code>) with
    /// the <code>IdentityId</code> of the <code>DestinationUserIdentifier</code>. Only developer-authenticated
    /// users can be merged. If the users to be merged are associated with the same public
    /// provider, but as two different users, an exception will be thrown.
    /// </summary>
    public partial class MergeDeveloperIdentitiesRequest : AmazonCognitoIdentityRequest
    {
        private string _destinationUserIdentifier;
        private string _developerProviderName;
        private string _identityPoolId;
        private string _sourceUserIdentifier;

        /// <summary>
        /// Gets and sets the property DestinationUserIdentifier. 
        /// <para>
        /// User identifier for the destination user. The value should be a <code>DeveloperUserIdentifier</code>.
        /// </para>
        /// </summary>
        public string DestinationUserIdentifier
        {
            get { return this._destinationUserIdentifier; }
            set { this._destinationUserIdentifier = value; }
        }

        // Check to see if DestinationUserIdentifier property is set
        internal bool IsSetDestinationUserIdentifier()
        {
            return this._destinationUserIdentifier != null;
        }

        /// <summary>
        /// Gets and sets the property DeveloperProviderName. 
        /// <para>
        /// The "domain" by which Cognito will refer to your users. This is a (pseudo) domain
        /// name that you provide while creating an identity pool. This name acts as a placeholder
        /// that allows your backend and the Cognito service to communicate about the developer
        /// provider. For the <code>DeveloperProviderName</code>, you can use letters as well
        /// as period (.), underscore (_), and dash (-).
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

        /// <summary>
        /// Gets and sets the property SourceUserIdentifier. 
        /// <para>
        /// User identifier for the source user. The value should be a <code>DeveloperUserIdentifier</code>.
        /// </para>
        /// </summary>
        public string SourceUserIdentifier
        {
            get { return this._sourceUserIdentifier; }
            set { this._sourceUserIdentifier = value; }
        }

        // Check to see if SourceUserIdentifier property is set
        internal bool IsSetSourceUserIdentifier()
        {
            return this._sourceUserIdentifier != null;
        }

    }
}
