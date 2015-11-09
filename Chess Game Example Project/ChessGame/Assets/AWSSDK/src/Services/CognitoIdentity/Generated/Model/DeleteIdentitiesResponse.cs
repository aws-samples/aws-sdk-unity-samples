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
    /// Returned in response to a successful <code>DeleteIdentities</code> operation.
    /// </summary>
    public partial class DeleteIdentitiesResponse : AmazonWebServiceResponse
    {
        private List<UnprocessedIdentityId> _unprocessedIdentityIds = new List<UnprocessedIdentityId>();

        /// <summary>
        /// Gets and sets the property UnprocessedIdentityIds. 
        /// <para>
        /// An array of UnprocessedIdentityId objects, each of which contains an ErrorCode and
        /// IdentityId.
        /// </para>
        /// </summary>
        public List<UnprocessedIdentityId> UnprocessedIdentityIds
        {
            get { return this._unprocessedIdentityIds; }
            set { this._unprocessedIdentityIds = value; }
        }

        // Check to see if UnprocessedIdentityIds property is set
        internal bool IsSetUnprocessedIdentityIds()
        {
            return this._unprocessedIdentityIds != null && this._unprocessedIdentityIds.Count > 0; 
        }

    }
}