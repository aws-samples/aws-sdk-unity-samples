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
    /// Container for the parameters to the RemovePermission operation.
    /// Removes a statement from a topic's access control policy.
    /// </summary>
    public partial class RemovePermissionRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _label;
        private string _topicArn;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public RemovePermissionRequest() { }

        /// <summary>
        /// Instantiates RemovePermissionRequest with the parameterized properties
        /// </summary>
        /// <param name="topicArn">The ARN of the topic whose access control policy you wish to modify.</param>
        /// <param name="label">The unique label of the statement you want to remove.</param>
        public RemovePermissionRequest(string topicArn, string label)
        {
            _topicArn = topicArn;
            _label = label;
        }

        /// <summary>
        /// Gets and sets the property Label. 
        /// <para>
        /// The unique label of the statement you want to remove.
        /// </para>
        /// </summary>
        public string Label
        {
            get { return this._label; }
            set { this._label = value; }
        }

        // Check to see if Label property is set
        internal bool IsSetLabel()
        {
            return this._label != null;
        }

        /// <summary>
        /// Gets and sets the property TopicArn. 
        /// <para>
        /// The ARN of the topic whose access control policy you wish to modify.
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