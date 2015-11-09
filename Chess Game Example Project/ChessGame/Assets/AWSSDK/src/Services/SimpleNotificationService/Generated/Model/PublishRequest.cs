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
    /// Container for the parameters to the Publish operation.
    /// Sends a message to all of a topic's subscribed endpoints. When a    <code>messageId</code>
    /// is returned, the message has been saved and Amazon SNS will attempt to deliver it
    ///       to the topic's subscribers shortly. The format of the outgoing message to each
    ///      subscribed endpoint depends on the notification protocol selected.
    /// 
    ///     
    /// <para>
    /// To use the <code>Publish</code> action for sending a message to a mobile endpoint,
    /// such as an app on a Kindle device or mobile phone,       you must specify the EndpointArn.
    /// The EndpointArn is returned when making a call with the <code>CreatePlatformEndpoint</code>
    /// action.       The second example below shows a request and response for publishing
    /// to a mobile endpoint.    
    /// </para>
    /// </summary>
    public partial class PublishRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _message;
        private Dictionary<string, MessageAttributeValue> _messageAttributes = new Dictionary<string, MessageAttributeValue>();
        private string _messageStructure;
        private string _subject;
        private string _targetArn;
        private string _topicArn;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public PublishRequest() { }

        /// <summary>
        /// Instantiates PublishRequest with the parameterized properties
        /// </summary>
        /// <param name="topicArn">The topic you want to publish to.</param>
        /// <param name="message">The message you want to send to the topic. If you want to send the same message to all transport protocols,    include the text of the message as a String value. If you want to send different messages for each transport protocol,    set the value of the <code>MessageStructure</code> parameter to <code>json</code>    and use a JSON object for the <code>Message</code> parameter.    See the Examples section for the format of the JSON object.  Constraints: Messages must be UTF-8 encoded   strings at most 256 KB in size (262144 bytes, not 262144 characters). JSON-specific constraints:   <ul>  <li>Keys in the JSON object that correspond to supported transport   protocols must have simple JSON string values. </li>  <li>The values will be parsed (unescaped)  before they are used in outgoing messages.</li>  <li>Outbound notifications are JSON  encoded (meaning that the characters will be reescaped for sending).</li>  <li>Values have a minimum length of 0 (the empty string, "", is allowed).</li>  <li>Values have a maximum length bounded by the overall message size (so, including  multiple protocols may limit message sizes).</li>  <li>Non-string values will cause the key  to be ignored.</li>  <li>Keys that do not correspond to supported transport protocols are ignored.</li>  <li>Duplicate keys are not allowed.</li>  <li>Failure to parse or validate any key or    value in the message will cause the <code>Publish</code> call to return an error (no partial   delivery).</li>   </ul>  </param>
        public PublishRequest(string topicArn, string message)
        {
            _topicArn = topicArn;
            _message = message;
        }

        /// <summary>
        /// Instantiates PublishRequest with the parameterized properties
        /// </summary>
        /// <param name="topicArn">The topic you want to publish to.</param>
        /// <param name="message">The message you want to send to the topic. If you want to send the same message to all transport protocols,    include the text of the message as a String value. If you want to send different messages for each transport protocol,    set the value of the <code>MessageStructure</code> parameter to <code>json</code>    and use a JSON object for the <code>Message</code> parameter.    See the Examples section for the format of the JSON object.  Constraints: Messages must be UTF-8 encoded   strings at most 256 KB in size (262144 bytes, not 262144 characters). JSON-specific constraints:   <ul>  <li>Keys in the JSON object that correspond to supported transport   protocols must have simple JSON string values. </li>  <li>The values will be parsed (unescaped)  before they are used in outgoing messages.</li>  <li>Outbound notifications are JSON  encoded (meaning that the characters will be reescaped for sending).</li>  <li>Values have a minimum length of 0 (the empty string, "", is allowed).</li>  <li>Values have a maximum length bounded by the overall message size (so, including  multiple protocols may limit message sizes).</li>  <li>Non-string values will cause the key  to be ignored.</li>  <li>Keys that do not correspond to supported transport protocols are ignored.</li>  <li>Duplicate keys are not allowed.</li>  <li>Failure to parse or validate any key or    value in the message will cause the <code>Publish</code> call to return an error (no partial   delivery).</li>   </ul>  </param>
        /// <param name="subject">Optional parameter to be used as the "Subject" line when the message is   delivered to email endpoints. This field will also be included, if present,    in the standard JSON messages delivered to other endpoints. Constraints: Subjects must be ASCII text that begins with a letter, number,    or punctuation mark; must not include line breaks or control characters; and    must be less than 100 characters long.</param>
        public PublishRequest(string topicArn, string message, string subject)
        {
            _topicArn = topicArn;
            _message = message;
            _subject = subject;
        }

        /// <summary>
        /// Gets and sets the property Message. 
        /// <para>
        /// The message you want to send to the topic.
        /// </para>
        ///     
        /// <para>
        /// If you want to send the same message to all transport protocols,       include the
        /// text of the message as a String value.
        /// </para>
        ///     
        /// <para>
        /// If you want to send different messages for each transport protocol,       set the
        /// value of the <code>MessageStructure</code> parameter to <code>json</code>       and
        /// use a JSON object for the <code>Message</code> parameter.       See the Examples section
        /// for the format of the JSON object. 
        /// </para>
        ///     
        /// <para>
        /// Constraints: Messages must be UTF-8 encoded      strings at most 256 KB in size (262144
        /// bytes, not 262144 characters).
        /// </para>
        ///     
        /// <para>
        /// JSON-specific constraints:      <ul>        <li>Keys in the JSON object that correspond
        /// to supported transport      protocols must have simple JSON string values. </li> 
        ///       <li>The values will be parsed (unescaped)        before they are used in outgoing
        /// messages.</li>        <li>Outbound notifications are JSON        encoded (meaning
        /// that the characters will be reescaped for sending).</li>        <li>Values have a
        /// minimum length of 0 (the empty string, "", is allowed).</li>        <li>Values have
        /// a maximum length bounded by the overall message size (so, including        multiple
        /// protocols may limit message sizes).</li>        <li>Non-string values will cause the
        /// key        to be ignored.</li>        <li>Keys that do not correspond to supported
        /// transport protocols are ignored.</li>        <li>Duplicate keys are not allowed.</li>
        ///        <li>Failure to parse or validate any key or          value in the message will
        /// cause the <code>Publish</code> call to return an error (no partial      delivery).</li>
        ///      </ul>     
        /// </para>
        /// </summary>
        public string Message
        {
            get { return this._message; }
            set { this._message = value; }
        }

        // Check to see if Message property is set
        internal bool IsSetMessage()
        {
            return this._message != null;
        }

        /// <summary>
        /// Gets and sets the property MessageAttributes.
        /// </summary>
        public Dictionary<string, MessageAttributeValue> MessageAttributes
        {
            get { return this._messageAttributes; }
            set { this._messageAttributes = value; }
        }

        // Check to see if MessageAttributes property is set
        internal bool IsSetMessageAttributes()
        {
            return this._messageAttributes != null && this._messageAttributes.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property MessageStructure. 
        /// <para>
        /// Set <code>MessageStructure</code> to <code>json</code> if you want to send      a
        /// different message for each protocol. For example, using one publish action,      you
        /// can send a short message to your SMS subscribers and a longer message to      your
        /// email subscribers.      If you set <code>MessageStructure</code> to <code>json</code>,
        /// the value of       the <code>Message</code> parameter must:    
        /// </para>
        ///     <ul>      <li>be a syntactically valid JSON object; and</li>      <li>contain
        /// at least a top-level JSON key of "default" with a value that is a string.</li>   
        /// </ul>    
        /// <para>
        ///  You can define other top-level keys that define the message you want to send    
        ///  to a specific transport protocol (e.g., "http"). 
        /// </para>
        ///     
        /// <para>
        /// For information about sending different messages for each protocol using      the
        /// AWS Management Console, go to <a href="http://docs.aws.amazon.com/sns/latest/gsg/Publish.html#sns-message-formatting-by-protocol">Create
        ///         Different Messages for Each Protocol</a> in the <i>Amazon Simple Notification
        /// Service      Getting Started Guide</i>.    
        /// </para>
        ///       
        /// <para>
        /// Valid value: <code>json</code>
        /// </para>
        /// </summary>
        public string MessageStructure
        {
            get { return this._messageStructure; }
            set { this._messageStructure = value; }
        }

        // Check to see if MessageStructure property is set
        internal bool IsSetMessageStructure()
        {
            return this._messageStructure != null;
        }

        /// <summary>
        /// Gets and sets the property Subject. 
        /// <para>
        /// Optional parameter to be used as the "Subject" line when the message is      delivered
        /// to email endpoints. This field will also be included, if present,       in the standard
        /// JSON messages delivered to other endpoints.
        /// </para>
        ///     
        /// <para>
        /// Constraints: Subjects must be ASCII text that begins with a letter, number,      
        /// or punctuation mark; must not include line breaks or control characters; and     
        ///  must be less than 100 characters long.
        /// </para>
        /// </summary>
        public string Subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }

        // Check to see if Subject property is set
        internal bool IsSetSubject()
        {
            return this._subject != null;
        }

        /// <summary>
        /// Gets and sets the property TargetArn. 
        /// <para>
        /// Either TopicArn or EndpointArn, but not both.
        /// </para>
        /// </summary>
        public string TargetArn
        {
            get { return this._targetArn; }
            set { this._targetArn = value; }
        }

        // Check to see if TargetArn property is set
        internal bool IsSetTargetArn()
        {
            return this._targetArn != null;
        }

        /// <summary>
        /// Gets and sets the property TopicArn. 
        /// <para>
        /// The topic you want to publish to.
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