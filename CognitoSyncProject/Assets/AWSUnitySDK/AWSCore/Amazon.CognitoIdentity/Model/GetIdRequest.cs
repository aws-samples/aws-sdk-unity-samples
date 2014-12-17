/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
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
    /// Container for the parameters to the GetId operation.
    /// Generates (or retrieves) a Cognito ID. Supplying multiple logins will create an implicit
    /// linked account.
    /// </summary>
    public partial class GetIdRequest : AmazonCognitoIdentityRequest
    {
        private string _accountId;
        private string _identityPoolId;
        private Dictionary<string, string> _logins = new Dictionary<string, string>();


        /// <summary>
        /// Gets and sets the property AccountId. A standard AWS account ID (9+ digits).
        /// </summary>
        public string AccountId
        {
            get { return this._accountId; }
            set { this._accountId = value; }
        }

        // Check to see if AccountId property is set
        internal bool IsSetAccountId()
        {
            return this._accountId != null;
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
        /// Gets and sets the property Logins. 
        /// <para>
        /// A set of optional name-value pairs that map provider names to provider tokens.
        /// </para>
        ///  
        /// <para>
        /// The available provider names for <code>Logins</code> are as follows: <ul> <li>Facebook:
        /// <code>graph.facebook.com</code> </li> <li>Google: <code>accounts.google.com</code>
        /// </li> <li>Amazon: <code>www.amazon.com</code> </li> </ul> 
        /// </para>
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