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
    /// Returned in response to a successful <code>LookupDeveloperIdentity</code> action.
    /// </summary>
    public partial class LookupDeveloperIdentityResponse : AmazonWebServiceResponse
    {
        private List<string> _developerUserIdentifierList = new List<string>();
        private string _identityId;
        private string _nextToken;

        /// <summary>
        /// Gets and sets the property DeveloperUserIdentifierList. 
        /// <para>
        /// This is the list of developer user identifiers associated with an identity ID. Cognito
        /// supports the association of multiple developer user identifiers with an identity ID.
        /// </para>
        /// </summary>
        public List<string> DeveloperUserIdentifierList
        {
            get { return this._developerUserIdentifierList; }
            set { this._developerUserIdentifierList = value; }
        }

        // Check to see if DeveloperUserIdentifierList property is set
        internal bool IsSetDeveloperUserIdentifierList()
        {
            return this._developerUserIdentifierList != null && this._developerUserIdentifierList.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property IdentityId. 
        /// <para>
        /// A unique identifier in the format REGION:GUID.
        /// </para>
        /// </summary>
        public string IdentityId
        {
            get { return this._identityId; }
            set { this._identityId = value; }
        }

        // Check to see if IdentityId property is set
        internal bool IsSetIdentityId()
        {
            return this._identityId != null;
        }

        /// <summary>
        /// Gets and sets the property NextToken. 
        /// <para>
        /// A pagination token. The first call you make will have <code>NextToken</code> set to
        /// null. After that the service will return <code>NextToken</code> values as needed.
        /// For example, let's say you make a request with <code>MaxResults</code> set to 10,
        /// and there are 20 matches in the database. The service will return a pagination token
        /// as a part of the response. This token can be used to call the API again and get results
        /// starting from the 11th match.
        /// </para>
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
