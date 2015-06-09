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
 * Do not modify this file. This file is generated from the sts-2011-06-15.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.SecurityToken.Model
{
    /// <summary>
    /// Contains the result of a successful call to the <a>AssumeRoleWithWebIdentity</a> action,
    /// including temporary AWS credentials that can be used to make AWS requests.
    /// </summary>
    public partial class AssumeRoleWithWebIdentityResult : AmazonWebServiceResponse
    {
        private AssumedRoleUser _assumedRoleUser;
        private string _audience;
        private Credentials _credentials;
        private int? _packedPolicySize;
        private string _provider;
        private string _subjectFromWebIdentityToken;

        /// <summary>
        /// Gets and sets the property AssumedRoleUser. 
        /// <para>
        /// The Amazon Resource Name (ARN) and the assumed role ID, which are identifiers that
        /// you can use to refer to the resulting temporary security credentials. For example,
        /// you can reference these credentials as a principal in a resource-based policy by using
        /// the ARN or assumed role ID. The ARN and ID include the <code>RoleSessionName</code>
        /// that you specified when you called <code>AssumeRole</code>. 
        /// </para>
        /// </summary>
        public AssumedRoleUser AssumedRoleUser
        {
            get { return this._assumedRoleUser; }
            set { this._assumedRoleUser = value; }
        }

        // Check to see if AssumedRoleUser property is set
        internal bool IsSetAssumedRoleUser()
        {
            return this._assumedRoleUser != null;
        }

        /// <summary>
        /// Gets and sets the property Audience. 
        /// <para>
        ///  The intended audience of the web identity token. This is traditionally the client
        /// identifier issued to the application that requested the web identity token.
        /// </para>
        /// </summary>
        public string Audience
        {
            get { return this._audience; }
            set { this._audience = value; }
        }

        // Check to see if Audience property is set
        internal bool IsSetAudience()
        {
            return this._audience != null;
        }

        /// <summary>
        /// Gets and sets the property Credentials. 
        /// <para>
        /// The temporary security credentials, which include an access key ID, a secret access
        /// key, and a security token.
        /// </para>
        /// </summary>
        public Credentials Credentials
        {
            get { return this._credentials; }
            set { this._credentials = value; }
        }

        // Check to see if Credentials property is set
        internal bool IsSetCredentials()
        {
            return this._credentials != null;
        }

        /// <summary>
        /// Gets and sets the property PackedPolicySize. 
        /// <para>
        /// A percentage value that indicates the size of the policy in packed form. The service
        /// rejects any policy with a packed size greater than 100 percent, which means the policy
        /// exceeded the allowed space. 
        /// </para>
        /// </summary>
        public int PackedPolicySize
        {
            get { return this._packedPolicySize.GetValueOrDefault(); }
            set { this._packedPolicySize = value; }
        }

        // Check to see if PackedPolicySize property is set
        internal bool IsSetPackedPolicySize()
        {
            return this._packedPolicySize.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property Provider. 
        /// <para>
        ///  The issuing authority of the web identity token presented. For OpenID Connect ID
        /// Tokens this contains the value of the <code>iss</code> field. For OAuth 2.0 Access
        /// Tokens, this contains the value of the <code>ProviderId</code> parameter that was
        /// passed in the <code>AssumeRoleWithWebIdentity</code> request.
        /// </para>
        /// </summary>
        public string Provider
        {
            get { return this._provider; }
            set { this._provider = value; }
        }

        // Check to see if Provider property is set
        internal bool IsSetProvider()
        {
            return this._provider != null;
        }

        /// <summary>
        /// Gets and sets the property SubjectFromWebIdentityToken. 
        /// <para>
        /// The unique user identifier that is returned by the identity provider. This identifier
        /// is associated with the <code>WebIdentityToken</code> that was submitted with the <code>AssumeRoleWithWebIdentity</code>
        /// call. The identifier is typically unique to the user and the application that acquired
        /// the <code>WebIdentityToken</code> (pairwise identifier). If an OpenID Connect ID token
        /// was submitted in the <code>WebIdentityToken</code>, this value is returned by the
        /// identity provider as the token's <code>sub</code> (Subject) claim. 
        /// </para>
        /// </summary>
        public string SubjectFromWebIdentityToken
        {
            get { return this._subjectFromWebIdentityToken; }
            set { this._subjectFromWebIdentityToken = value; }
        }

        // Check to see if SubjectFromWebIdentityToken property is set
        internal bool IsSetSubjectFromWebIdentityToken()
        {
            return this._subjectFromWebIdentityToken != null;
        }

    }
}
