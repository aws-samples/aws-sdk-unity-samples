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
    /// The response to a ListIdentities request.
    /// </summary>
    public partial class ListIdentitiesResult : AmazonWebServiceResponse
    {
        private List<IdentityDescription> _identities = new List<IdentityDescription>();
        private string _identityPoolId;
        private string _nextToken;

        /// <summary>
        /// Gets and sets the property Identities. An object containing a set of identities and
        /// associated mappings.
        /// </summary>
        public List<IdentityDescription> Identities
        {
            get { return this._identities; }
            set { this._identities = value; }
        }

        // Check to see if Identities property is set
        internal bool IsSetIdentities()
        {
            return this._identities != null && this._identities.Count > 0; 
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
        /// Gets and sets the property NextToken. A pagination token.
        /// </summary>
        public string NextToken
        {
            get { return this._nextToken; }
            set { this._nextToken = value; }
        }

        // Check to see if NextToken property is set
        internal bool IsSetNextToken()
        {
            return this._nextToken != null;
        }

    }
}
