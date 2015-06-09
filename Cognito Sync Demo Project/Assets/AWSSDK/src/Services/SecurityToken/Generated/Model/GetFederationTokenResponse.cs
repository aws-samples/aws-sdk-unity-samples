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
    /// Contains the result of a successful call to the <a>GetFederationToken</a> action,
    /// including temporary AWS credentials that can be used to make AWS requests.
    /// </summary>
    public partial class GetFederationTokenResponse : AmazonWebServiceResponse
    {
        private Credentials _credentials;
        private FederatedUser _federatedUser;
        private int? _packedPolicySize;

        /// <summary>
        /// Gets and sets the property Credentials. 
        /// <para>
        /// Credentials for the service API authentication. 
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
        /// Gets and sets the property FederatedUser. 
        /// <para>
        /// Identifiers for the federated user associated with the credentials (such as <code>arn:aws:sts::123456789012:federated-user/Bob</code>
        /// or <code>123456789012:Bob</code>). You can use the federated user's ARN in your resource-based
        /// policies, such as an Amazon S3 bucket policy. 
        /// </para>
        /// </summary>
        public FederatedUser FederatedUser
        {
            get { return this._federatedUser; }
            set { this._federatedUser = value; }
        }

        // Check to see if FederatedUser property is set
        internal bool IsSetFederatedUser()
        {
            return this._federatedUser != null;
        }

        /// <summary>
        /// Gets and sets the property PackedPolicySize. 
        /// <para>
        /// A percentage value indicating the size of the policy in packed form. The service rejects
        /// policies for which the packed size is greater than 100 percent of the allowed value.
        /// 
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

    }
}
