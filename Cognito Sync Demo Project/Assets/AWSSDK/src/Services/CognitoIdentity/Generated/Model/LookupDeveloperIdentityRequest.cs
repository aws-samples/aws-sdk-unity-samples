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
    /// Container for the parameters to the LookupDeveloperIdentity operation.
    /// Retrieves the <code>IdentityID</code> associated with a <code>DeveloperUserIdentifier</code>
    /// or the list of <code>DeveloperUserIdentifier</code>s associated with an <code>IdentityId</code>
    /// for an existing identity. Either <code>IdentityID</code> or <code>DeveloperUserIdentifier</code>
    /// must not be null. If you supply only one of these values, the other value will be
    /// searched in the database and returned as a part of the response. If you supply both,
    /// <code>DeveloperUserIdentifier</code> will be matched against <code>IdentityID</code>.
    /// If the values are verified against the database, the response returns both values
    /// and is the same as the request. Otherwise a <code>ResourceConflictException</code>
    /// is thrown.
    /// </summary>
    public partial class LookupDeveloperIdentityRequest : AmazonCognitoIdentityRequest
    {
        private string _developerUserIdentifier;
        private string _identityId;
        private string _identityPoolId;
        private int? _maxResults;
        private string _nextToken;

        /// <summary>
        /// Gets and sets the property DeveloperUserIdentifier. 
        /// <para>
        /// A unique ID used by your backend authentication process to identify a user. Typically,
        /// a developer identity provider would issue many developer user identifiers, in keeping
        /// with the number of users.
        /// </para>
        /// </summary>
        public string DeveloperUserIdentifier
        {
            get { return this._developerUserIdentifier; }
            set { this._developerUserIdentifier = value; }
        }

        // Check to see if DeveloperUserIdentifier property is set
        internal bool IsSetDeveloperUserIdentifier()
        {
            return this._developerUserIdentifier != null;
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
        /// Gets and sets the property IdentityPoolId. 
        /// <para>
        /// An identity pool ID in the format REGION:GUID.
        /// </para>
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
        /// Gets and sets the property MaxResults. 
        /// <para>
        /// The maximum number of identities to return.
        /// </para>
        /// </summary>
        public int MaxResults
        {
            get { return this._maxResults.GetValueOrDefault(); }
            set { this._maxResults = value; }
        }

        // Check to see if MaxResults property is set
        internal bool IsSetMaxResults()
        {
            return this._maxResults.HasValue; 
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
