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
    /// Container for the parameters to the GetPlatformApplicationAttributes operation.
    /// Retrieves the attributes of the platform application object for the supported push
    /// notification services,       such as APNS and GCM.      For more information, see
    /// <a href="http://docs.aws.amazon.com/sns/latest/dg/SNSMobilePush.html">Using Amazon
    /// SNS Mobile Push Notifications</a>.
    /// </summary>
    public partial class GetPlatformApplicationAttributesRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _platformApplicationArn;

        /// <summary>
        /// Gets and sets the property PlatformApplicationArn. 
        /// <para>
        /// PlatformApplicationArn for GetPlatformApplicationAttributesInput.
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

    }
}