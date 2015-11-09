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
    /// Container for the parameters to the Unsubscribe operation.
    /// Deletes a subscription. If the subscription requires authentication for       deletion,
    /// only the owner of the subscription or the topic's owner       can unsubscribe, and
    /// an AWS signature is required. If the       <code>Unsubscribe</code> call does not
    /// require authentication and the requester is not       the subscription owner, a final
    /// cancellation message is delivered to the       endpoint, so that the endpoint owner
    /// can easily resubscribe to the topic if       the <code>Unsubscribe</code> request
    /// was unintended.
    /// </summary>
    public partial class UnsubscribeRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _subscriptionArn;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public UnsubscribeRequest() { }

        /// <summary>
        /// Instantiates UnsubscribeRequest with the parameterized properties
        /// </summary>
        /// <param name="subscriptionArn">The ARN of the subscription to be deleted.</param>
        public UnsubscribeRequest(string subscriptionArn)
        {
            _subscriptionArn = subscriptionArn;
        }

        /// <summary>
        /// Gets and sets the property SubscriptionArn. 
        /// <para>
        /// The ARN of the subscription to be deleted.
        /// </para>
        /// </summary>
        public string SubscriptionArn
        {
            get { return this._subscriptionArn; }
            set { this._subscriptionArn = value; }
        }

        // Check to see if SubscriptionArn property is set
        internal bool IsSetSubscriptionArn()
        {
            return this._subscriptionArn != null;
        }

    }
}