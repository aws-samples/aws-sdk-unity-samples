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
    /// An object representing a Cognito identity pool.
    /// </summary>
    public partial class UpdateIdentityPoolResponse : AmazonWebServiceResponse
    {
        private bool? _allowUnauthenticatedIdentities;
        private string _developerProviderName;
        private string _identityPoolId;
        private string _identityPoolName;
        private List<string> _openIdConnectProviderARNs = new List<string>();
        private Dictionary<string, string> _supportedLoginProviders = new Dictionary<string, string>();

        /// <summary>
        /// Gets and sets the property AllowUnauthenticatedIdentities. TRUE if the identity pool
        /// supports unauthenticated logins.
        /// </summary>
        public bool AllowUnauthenticatedIdentities
        {
            get { return this._allowUnauthenticatedIdentities.GetValueOrDefault(); }
            set { this._allowUnauthenticatedIdentities = value; }
        }

        // Check to see if AllowUnauthenticatedIdentities property is set
        internal bool IsSetAllowUnauthenticatedIdentities()
        {
            return this._allowUnauthenticatedIdentities.HasValue; 
        }

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
        /// Gets and sets the property IdentityPoolName. 
        /// <para>
        /// A string that you provide.
        /// </para>
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

        /// <summary>
        /// Gets and sets the property OpenIdConnectProviderARNs. 
        /// <para>
        /// A list of OpendID Connect provider ARNs.
        /// </para>
        /// </summary>
        public List<string> OpenIdConnectProviderARNs
        {
            get { return this._openIdConnectProviderARNs; }
            set { this._openIdConnectProviderARNs = value; }
        }

        // Check to see if OpenIdConnectProviderARNs property is set
        internal bool IsSetOpenIdConnectProviderARNs()
        {
            return this._openIdConnectProviderARNs != null && this._openIdConnectProviderARNs.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property SupportedLoginProviders. 
        /// <para>
        /// Optional key:value pairs mapping provider names to provider app IDs.
        /// </para>
        /// </summary>
        public Dictionary<string, string> SupportedLoginProviders
        {
            get { return this._supportedLoginProviders; }
            set { this._supportedLoginProviders = value; }
        }

        // Check to see if SupportedLoginProviders property is set
        internal bool IsSetSupportedLoginProviders()
        {
            return this._supportedLoginProviders != null && this._supportedLoginProviders.Count > 0; 
        }

    }
}
