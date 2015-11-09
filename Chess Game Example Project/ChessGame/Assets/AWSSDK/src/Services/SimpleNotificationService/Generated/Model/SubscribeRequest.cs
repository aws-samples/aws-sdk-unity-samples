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
    /// Container for the parameters to the Subscribe operation.
    /// Prepares to subscribe an endpoint by sending the endpoint a confirmation message.
    /// To actually create a      subscription, the endpoint owner must call the <code>ConfirmSubscription</code>
    ///      action with the token from the confirmation message. Confirmation tokens are
    ///      valid for three days.
    /// </summary>
    public partial class SubscribeRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _endpoint;
        private string _protocol;
        private string _topicArn;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public SubscribeRequest() { }

        /// <summary>
        /// Instantiates SubscribeRequest with the parameterized properties
        /// </summary>
        /// <param name="topicArn">The ARN of the topic you want to subscribe to.</param>
        /// <param name="protocol">The protocol you want to use. Supported protocols include: <ul>   <li><code>http</code> -- delivery of JSON-encoded message via HTTP POST</li>   <li><code>https</code> -- delivery of JSON-encoded message via HTTPS POST</li>   <li><code>email</code> -- delivery of message via SMTP</li>   <li><code>email-json</code> -- delivery of JSON-encoded message via SMTP</li>   <li><code>sms</code> -- delivery of message via SMS</li>   <li><code>sqs</code> -- delivery of JSON-encoded message to an Amazon SQS queue</li>   <li><code>application</code> -- delivery of JSON-encoded message to an EndpointArn for a mobile app and device.</li> </ul></param>
        /// <param name="endpoint">The endpoint that you want to receive notifications. Endpoints vary by protocol: <ul>   <li>For the <code>http</code> protocol, the endpoint is an URL beginning with "http://"</li>   <li>For the <code>https</code> protocol, the endpoint is a URL beginning with "https://"</li>   <li>For the <code>email</code> protocol, the endpoint is an email address</li>   <li>For the <code>email-json</code> protocol, the endpoint is an email address</li>   <li>For the <code>sms</code> protocol, the endpoint is a phone number of an SMS-enabled device</li>   <li>For the <code>sqs</code> protocol, the endpoint is the ARN of an Amazon SQS queue</li>   <li>For the <code>application</code> protocol, the endpoint is the EndpointArn of a mobile app and device.</li> </ul></param>
        public SubscribeRequest(string topicArn, string protocol, string endpoint)
        {
            _topicArn = topicArn;
            _protocol = protocol;
            _endpoint = endpoint;
        }

        /// <summary>
        /// Gets and sets the property Endpoint. 
        /// <para>
        /// The endpoint that you want to receive notifications. Endpoints vary by protocol:
        /// </para>
        ///     <ul>      <li>For the <code>http</code> protocol, the endpoint is an URL beginning
        /// with "http://"</li>      <li>For the <code>https</code> protocol, the endpoint is
        /// a URL beginning with "https://"</li>      <li>For the <code>email</code> protocol,
        /// the endpoint is an email address</li>      <li>For the <code>email-json</code> protocol,
        /// the endpoint is an email address</li>      <li>For the <code>sms</code> protocol,
        /// the endpoint is a phone number of an SMS-enabled device</li>      <li>For the <code>sqs</code>
        /// protocol, the endpoint is the ARN of an Amazon SQS queue</li>      <li>For the <code>application</code>
        /// protocol, the endpoint is the EndpointArn of a mobile app and device.</li>    </ul>
        /// </summary>
        public string Endpoint
        {
            get { return this._endpoint; }
            set { this._endpoint = value; }
        }

        // Check to see if Endpoint property is set
        internal bool IsSetEndpoint()
        {
            return this._endpoint != null;
        }

        /// <summary>
        /// Gets and sets the property Protocol. 
        /// <para>
        /// The protocol you want to use. Supported protocols include:
        /// </para>
        ///     <ul>      <li><code>http</code> -- delivery of JSON-encoded message via HTTP POST</li>
        ///      <li><code>https</code> -- delivery of JSON-encoded message via HTTPS POST</li>
        ///      <li><code>email</code> -- delivery of message via SMTP</li>      <li><code>email-json</code>
        /// -- delivery of JSON-encoded message via SMTP</li>      <li><code>sms</code> -- delivery
        /// of message via SMS</li>      <li><code>sqs</code> -- delivery of JSON-encoded message
        /// to an Amazon SQS queue</li>      <li><code>application</code> -- delivery of JSON-encoded
        /// message to an EndpointArn for a mobile app and device.</li>    </ul>
        /// </summary>
        public string Protocol
        {
            get { return this._protocol; }
            set { this._protocol = value; }
        }

        // Check to see if Protocol property is set
        internal bool IsSetProtocol()
        {
            return this._protocol != null;
        }

        /// <summary>
        /// Gets and sets the property TopicArn. 
        /// <para>
        /// The ARN of the topic you want to subscribe to.
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