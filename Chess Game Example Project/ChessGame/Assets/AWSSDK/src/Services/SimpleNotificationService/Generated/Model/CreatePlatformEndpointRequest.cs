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
    /// Container for the parameters to the CreatePlatformEndpoint operation.
    /// Creates an endpoint for a device and mobile app on one of the supported push notification
    /// services, such as GCM and APNS.       <code>CreatePlatformEndpoint</code> requires
    /// the PlatformApplicationArn that is returned from <code>CreatePlatformApplication</code>.
    /// The EndpointArn that is      returned when using <code>CreatePlatformEndpoint</code>
    /// can then be used by the <code>Publish</code> action to send a message to a mobile
    /// app or by the <code>Subscribe</code>       action for subscription to a topic. The
    /// <code>CreatePlatformEndpoint</code> action is idempotent, so if the requester already
    /// owns an endpoint with the same device token and attributes,       that endpoint's
    /// ARN is returned without creating a new endpoint.       For more information, see <a
    /// href="http://docs.aws.amazon.com/sns/latest/dg/SNSMobilePush.html">Using Amazon SNS
    /// Mobile Push Notifications</a>.    
    /// 
    ///     
    /// <para>
    /// When using <code>CreatePlatformEndpoint</code> with Baidu, two attributes must be
    /// provided: ChannelId and UserId. The token field must also contain the ChannelId. 
    ///      For more information, see <a href="http://docs.aws.amazon.com/sns/latest/dg/SNSMobilePushBaiduEndpoint.html">Creating
    /// an Amazon SNS Endpoint for Baidu</a>.          
    /// </para>
    /// </summary>
    public partial class CreatePlatformEndpointRequest : AmazonSimpleNotificationServiceRequest
    {
        private Dictionary<string, string> _attributes = new Dictionary<string, string>();
        private string _customUserData;
        private string _platformApplicationArn;
        private string _token;

        /// <summary>
        /// Gets and sets the property Attributes. 
        /// <para>
        /// For a list of attributes, see <a href="http://docs.aws.amazon.com/sns/latest/api/API_SetEndpointAttributes.html">SetEndpointAttributes</a>.
        /// </para>
        /// </summary>
        public Dictionary<string, string> Attributes
        {
            get { return this._attributes; }
            set { this._attributes = value; }
        }

        // Check to see if Attributes property is set
        internal bool IsSetAttributes()
        {
            return this._attributes != null && this._attributes.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property CustomUserData. 
        /// <para>
        /// Arbitrary user data to associate with the endpoint. Amazon SNS does not use this data.
        /// The data must be in UTF-8 format and less than 2KB.
        /// </para>
        /// </summary>
        public string CustomUserData
        {
            get { return this._customUserData; }
            set { this._customUserData = value; }
        }

        // Check to see if CustomUserData property is set
        internal bool IsSetCustomUserData()
        {
            return this._customUserData != null;
        }

        /// <summary>
        /// Gets and sets the property PlatformApplicationArn. 
        /// <para>
        /// PlatformApplicationArn returned from CreatePlatformApplication is used to create a
        /// an endpoint.
        /// </para>
        /// </summary>
        public string PlatformApplicationArn
        {
            get { return this._platformApplicationArn; }
            set { this._platformApplicationArn = value; }
        }

        // Check to see if PlatformApplicationArn property is set
        internal bool IsSetPlatformApplicationArn()
        {
            return this._platformApplicationArn != null;
        }

        /// <summary>
        /// Gets and sets the property Token. 
        /// <para>
        /// Unique identifier created by the notification service for an app on a device.    
        ///   The specific name for Token will vary, depending on which notification service is
        /// being used.       For example, when using APNS as the notification service, you need
        /// the device token.       Alternatively, when using GCM or ADM, the device token equivalent
        /// is called the registration ID.
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

    }
}