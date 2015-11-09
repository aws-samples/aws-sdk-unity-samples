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
    /// Container for the parameters to the SetTopicAttributes operation.
    /// Allows a topic owner to set an attribute of the topic to a new value.
    /// </summary>
    public partial class SetTopicAttributesRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _attributeName;
        private string _attributeValue;
        private string _topicArn;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public SetTopicAttributesRequest() { }

        /// <summary>
        /// Instantiates SetTopicAttributesRequest with the parameterized properties
        /// </summary>
        /// <param name="topicArn">The ARN of the topic to modify.</param>
        /// <param name="attributeName">The name of the attribute you want to set. Only a subset of the topic's attributes are mutable. Valid values: <code>Policy</code> | <code>DisplayName</code> | <code>DeliveryPolicy</code></param>
        /// <param name="attributeValue">The new value for the attribute.</param>
        public SetTopicAttributesRequest(string topicArn, string attributeName, string attributeValue)
        {
            _topicArn = topicArn;
            _attributeName = attributeName;
            _attributeValue = attributeValue;
        }

        /// <summary>
        /// Gets and sets the property AttributeName. 
        /// <para>
        /// The name of the attribute you want to set. Only a subset of the topic's attributes
        /// are mutable.
        /// </para>
        ///     
        /// <para>
        /// Valid values: <code>Policy</code> | <code>DisplayName</code> | <code>DeliveryPolicy</code>
        /// </para>
        /// </summary>
        public string AttributeName
        {
            get { return this._attributeName; }
            set { this._attributeName = value; }
        }

        // Check to see if AttributeName property is set
        internal bool IsSetAttributeName()
        {
            return this._attributeName != null;
        }

        /// <summary>
        /// Gets and sets the property AttributeValue. 
        /// <para>
        /// The new value for the attribute.
        /// </para>
        /// </summary>
        public string AttributeValue
        {
            get { return this._attributeValue; }
            set { this._attributeValue = value; }
        }

        // Check to see if AttributeValue property is set
        internal bool IsSetAttributeValue()
        {
            return this._attributeValue != null;
        }

        /// <summary>
        /// Gets and sets the property TopicArn. 
        /// <para>
        /// The ARN of the topic to modify.
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