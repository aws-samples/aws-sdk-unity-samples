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
    /// Container for the parameters to the ListSubscriptionsByTopic operation.
    /// Returns a list of the subscriptions to a specific topic. Each call returns       a
    /// limited list of subscriptions, up to 100. If there are more subscriptions, a <code>NextToken</code>
    /// is also returned. Use the <code>NextToken</code>       parameter in a new <code>ListSubscriptionsByTopic</code>
    /// call to get further results.
    /// </summary>
    public partial class ListSubscriptionsByTopicRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _nextToken;
        private string _topicArn;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public ListSubscriptionsByTopicRequest() { }

        /// <summary>
        /// Instantiates ListSubscriptionsByTopicRequest with the parameterized properties
        /// </summary>
        /// <param name="topicArn">The ARN of the topic for which you wish to find subscriptions.</param>
        public ListSubscriptionsByTopicRequest(string topicArn)
        {
            _topicArn = topicArn;
        }

        /// <summary>
        /// Instantiates ListSubscriptionsByTopicRequest with the parameterized properties
        /// </summary>
        /// <param name="topicArn">The ARN of the topic for which you wish to find subscriptions.</param>
        /// <param name="nextToken">Token returned by the previous <code>ListSubscriptionsByTopic</code> request.</param>
        public ListSubscriptionsByTopicRequest(string topicArn, string nextToken)
        {
            _topicArn = topicArn;
            _nextToken = nextToken;
        }

        /// <summary>
        /// Gets and sets the property NextToken. 
        /// <para>
        /// Token returned by the previous <code>ListSubscriptionsByTopic</code> request.
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

        /// <summary>
        /// Gets and sets the property TopicArn. 
        /// <para>
        /// The ARN of the topic for which you wish to find subscriptions.
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