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
    /// The result of a successful ListIdentityPools action.
    /// </summary>
    public partial class ListIdentityPoolsResult : AmazonWebServiceResponse
    {
        private List<IdentityPoolShortDescription> _identityPools = new List<IdentityPoolShortDescription>();
        private string _nextToken;

        /// <summary>
        /// Gets and sets the property IdentityPools. The identity pools returned by the ListIdentityPools
        /// action.
        /// </summary>
        public List<IdentityPoolShortDescription> IdentityPools
        {
            get { return this._identityPools; }
            set { this._identityPools = value; }
        }

        // Check to see if IdentityPools property is set
        internal bool IsSetIdentityPools()
        {
            return this._identityPools != null && this._identityPools.Count > 0; 
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
