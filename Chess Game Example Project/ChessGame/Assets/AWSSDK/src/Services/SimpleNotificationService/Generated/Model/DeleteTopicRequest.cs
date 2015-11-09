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
    /// Container for the parameters to the DeleteTopic operation.
    /// Deletes a topic and all its subscriptions. Deleting a topic might      prevent some
    /// messages previously sent to the topic from being delivered to      subscribers. This
    /// action is idempotent, so deleting a topic that does not      exist does not result
    /// in an error.
    /// </summary>
    public partial class DeleteTopicRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _topicArn;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public DeleteTopicRequest() { }

        /// <summary>
        /// Instantiates DeleteTopicRequest with the parameterized properties
        /// </summary>
        /// <param name="topicArn">The ARN of the topic you want to delete.<examples> <queryrequest>  http://sns.us-east-1.amazonaws.com/  ?TopicArn=arn%3Aaws%3Asns%3Aus-east-1%3A123456789012%3AMy-Topic  &amp;Action=DeleteTopic  &amp;SignatureVersion=2  &amp;SignatureMethod=HmacSHA256  &amp;Timestamp=2010-03-31T12%3A00%3A00.000Z  &amp;AWSAccessKeyId=(AWS Access Key ID)  &amp;Signature=DjHBa%2BbYCKQAzctOPnLP7MbHnrHT3%2FK3kFEZjwcf9%2FU%3D </queryrequest> <queryresponse>  &lt;DeleteTopicResponse xmlns="http://sns.amazonaws.com/doc/2010-03-31/"&gt;  &lt;ResponseMetadata&gt; &lt;RequestId&gt;fba800b9-3765-11df-8cf3-c58c53254dfb&lt;/RequestId&gt;  &lt;/ResponseMetadata&gt;&lt;/DeleteTopicResponse&gt;   </queryresponse></examples></param>
        public DeleteTopicRequest(string topicArn)
        {
            _topicArn = topicArn;
        }

        /// <summary>
        /// Gets and sets the property TopicArn. 
        /// <para>
        /// The ARN of the topic you want to delete.
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