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
    /// Credentials for the the provided identity ID.
    /// </summary>
    public partial class Credentials
    {
        private string _accessKeyId;
        private DateTime? _expiration;
        private string _secretKey;
        private string _sessionToken;

        /// <summary>
        /// Gets and sets the property AccessKeyId. 
        /// <para>
        /// The Access Key portion of the credentials.
        /// </para>
        /// </summary>
        public string AccessKeyId
        {
            get { return this._accessKeyId; }
            set { this._accessKeyId = value; }
        }

        // Check to see if AccessKeyId property is set
        internal bool IsSetAccessKeyId()
        {
            return this._accessKeyId != null;
        }

        /// <summary>
        /// Gets and sets the property Expiration. 
        /// <para>
        /// The date at which these credentials will expire.
        /// </para>
        /// </summary>
        public DateTime Expiration
        {
            get { return this._expiration.GetValueOrDefault(); }
            set { this._expiration = value; }
        }

        // Check to see if Expiration property is set
        internal bool IsSetExpiration()
        {
            return this._expiration.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property SecretKey. 
        /// <para>
        /// The Secret Access Key portion of the credentials
        /// </para>
        /// </summary>
        public string SecretKey
        {
            get { return this._secretKey; }
            set { this._secretKey = value; }
        }

        // Check to see if SecretKey property is set
        internal bool IsSetSecretKey()
        {
            return this._secretKey != null;
        }

        /// <summary>
        /// Gets and sets the property SessionToken. 
        /// <para>
        /// The Session Token portion of the credentials
        /// </para>
        /// </summary>
        public string SessionToken
        {
            get { return this._sessionToken; }
            set { this._sessionToken = value; }
        }

        // Check to see if SessionToken property is set
        internal bool IsSetSessionToken()
        {
            return this._sessionToken != null;
        }

    }
}
