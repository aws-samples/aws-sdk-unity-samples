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
 * Do not modify this file. This file is generated from the cognito-sync-2014-06-30.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.CognitoSync.Model
{
    /// <summary>
    /// Container for the parameters to the SetCognitoEvents operation.
    /// Sets the AWS Lambda function for a given event type for an identity pool. This request
    /// only updates the key/value pair specified. Other key/values pairs are not updated.
    /// To remove a key value pair, pass a empty value for the particular key.
    /// </summary>
    public partial class SetCognitoEventsRequest : AmazonCognitoSyncRequest
    {
        private Dictionary<string, string> _events = new Dictionary<string, string>();
        private string _identityPoolId;

        /// <summary>
        /// Gets and sets the property Events. 
        /// <para>
        /// The events to configure
        /// </para>
        /// </summary>
        public Dictionary<string, string> Events
        {
            get { return this._events; }
            set { this._events = value; }
        }

        // Check to see if Events property is set
        internal bool IsSetEvents()
        {
            return this._events != null && this._events.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property IdentityPoolId. 
        /// <para>
        /// The Cognito Identity Pool to use when configuring Cognito Events
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

    }
}
