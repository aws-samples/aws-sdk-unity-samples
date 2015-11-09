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
 * Do not modify this file. This file is generated from the sns-2010-03-31.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.SimpleNotificationService.Model
{
    /// <summary>
    /// Container for the parameters to the ConfirmSubscription operation.
    /// Verifies an endpoint owner's intent to receive messages by validating      the token
    /// sent to the endpoint by an earlier <code>Subscribe</code> action. If the      token
    /// is valid, the action creates a new subscription and returns its      Amazon Resource
    /// Name (ARN). This call requires an AWS signature only when the <code>AuthenticateOnUnsubscribe</code>
    /// flag is set to "true".
    /// </summary>
    public partial class ConfirmSubscriptionRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _authenticateOnUnsubscribe;
        private string _token;
        private string _topicArn;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public ConfirmSubscriptionRequest() { }

        /// <summary>
        /// Instantiates ConfirmSubscriptionRequest with the parameterized properties
        /// </summary>
        /// <param name="topicArn">The ARN of the topic for which you wish to confirm a subscription.</param>
        /// <param name="token">Short-lived token sent to an endpoint during the <code>Subscribe</code> action.</param>
        public ConfirmSubscriptionRequest(string topicArn, string token)
        {
            _topicArn = topicArn;
            _token = token;
        }

        /// <summary>
        /// Instantiates ConfirmSubscriptionRequest with the parameterized properties
        /// </summary>
        /// <param name="topicArn">The ARN of the topic for which you wish to confirm a subscription.</param>
        /// <param name="token">Short-lived token sent to an endpoint during the <code>Subscribe</code> action.</param>
        /// <param name="authenticateOnUnsubscribe">Disallows unauthenticated unsubscribes of the subscription.  If the value of this parameter is <code>true</code> and the request has an AWS signature, then only the topic owner and the subscription owner can unsubscribe the endpoint.  The unsubscribe action requires AWS authentication. </param>
        public ConfirmSubscriptionRequest(string topicArn, string token, string authenticateOnUnsubscribe)
        {
            _topicArn = topicArn;
            _token = token;
            _authenticateOnUnsubscribe = authenticateOnUnsubscribe;
        }

        /// <summary>
        /// Gets and sets the property AuthenticateOnUnsubscribe. 
        /// <para>
        /// Disallows unauthenticated unsubscribes of the subscription.     If the value of this
        /// parameter is <code>true</code> and the request has an AWS signature, then only the
        /// topic owner    and the subscription owner can unsubscribe the endpoint.  The unsubscribe
        ///    action requires AWS authentication. 
        /// </para>
        /// </summary>
        public string AuthenticateOnUnsubscribe
        {
            get { return this._authenticateOnUnsubscribe; }
            set { this._authenticateOnUnsubscribe = value; }
        }

        // Check to see if AuthenticateOnUnsubscribe property is set
        internal bool IsSetAuthenticateOnUnsubscribe()
        {
            return this._authenticateOnUnsubscribe != null;
        }

        /// <summary>
        /// Gets and sets the property Token. 
        /// <para>
        /// Short-lived token sent to an endpoint during the <code>Subscribe</code> action.
        /// </para>
        /// </summary>
        public string Token
        {
            get { return this._token; }
            set { this._token = value; }
        }

        // Check to see if Token property is set
        internal bool IsSetToken()
        {
            return this._token != null;
        }

        /// <summary>
        /// Gets and sets the property TopicArn. 
        /// <para>
        /// The ARN of the topic for which you wish to confirm a subscription.
        /// </para>
        /// </summary>
        public string TopicArn
        {
            get { return this._topicArn; }
            set { this._topicArn = value; }
        }

        // Check to see if TopicArn property is set
        internal bool IsSetTopicArn()
        {
            return this._topicArn != null;
        }

    }
}