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
    /// AWS credentials for API authentication.
    /// </summary>
    public partial class Credentials
    {
        private string _accessKeyId;
        private DateTime? _expiration;
        private string _secretAccessKey;
        private string _sessionToken;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public Credentials() { }

        /// <summary>
        /// Instantiates Credentials with the parameterized properties
        /// </summary>
        /// <param name="accessKeyId">The access key ID that identifies the temporary security credentials.</param>
        /// <param name="secretAccessKey">The secret access key that can be used to sign requests.</param>
        /// <param name="sessionToken">The token that users must pass to the service API to use the temporary credentials.</param>
        /// <param name="expiration">The date on which the current credentials expire.</param>
        public Credentials(string accessKeyId, string secretAccessKey, string sessionToken, DateTime expiration)
        {
            _accessKeyId = accessKeyId;
            _secretAccessKey = secretAccessKey;
            _sessionToken = sessionToken;
            _expiration = expiration;
        }

        /// <summary>
        /// Gets and sets the property AccessKeyId. 
        /// <para>
        /// The access key ID that identifies the temporary security credentials.
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
        /// The date on which the current credentials expire.
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
        /// Gets and sets the property SecretAccessKey. 
        /// <para>
        /// The secret access key that can be used to sign requests.
        /// </para>
        /// </summary>
        public string SecretAccessKey
        {
            get { return this._secretAccessKey; }
            set { this._secretAccessKey = value; }
        }

        // Check to see if SecretAccessKey property is set
        internal bool IsSetSecretAccessKey()
        {
            return this._secretAccessKey != null;
        }

        /// <summary>
        /// Gets and sets the property SessionToken. 
        /// <para>
        /// The token that users must pass to the service API to use the temporary credentials.
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
