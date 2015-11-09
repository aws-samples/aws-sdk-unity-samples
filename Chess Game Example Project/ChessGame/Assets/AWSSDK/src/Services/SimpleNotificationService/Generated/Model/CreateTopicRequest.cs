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
    /// Container for the parameters to the CreateTopic operation.
    /// Creates a topic to which notifications can be published. Users can create     at most
    /// 3000 topics. For more information, see <a href="http://aws.amazon.com/sns/">http://aws.amazon.com/sns</a>.
    ///      This action is idempotent, so if the requester already owns a topic with the
    /// specified name, that topic's ARN is      returned without creating a new topic.
    /// </summary>
    public partial class CreateTopicRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _name;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public CreateTopicRequest() { }

        /// <summary>
        /// Instantiates CreateTopicRequest with the parameterized properties
        /// </summary>
        /// <param name="name">The name of the topic you want to create. Constraints: Topic names must be made up of  only uppercase and lowercase ASCII letters, numbers, underscores, and hyphens, and must be  between 1 and 256 characters long. </param>
        public CreateTopicRequest(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Gets and sets the property Name. 
        /// <para>
        /// The name of the topic you want to create.
        /// </para>
        ///     
        /// <para>
        /// Constraints: Topic names must be made up of     only uppercase and lowercase ASCII
        /// letters, numbers, underscores, and hyphens, and must be     between 1 and 256 characters
        /// long. 
        /// </para>
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        // Check to see if Name property is set
        internal bool IsSetName()
        {
            return this._name != null;
        }

    }
}