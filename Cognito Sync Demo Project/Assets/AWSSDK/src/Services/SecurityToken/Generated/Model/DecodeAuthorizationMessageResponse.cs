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
 * Do not modify this file. This file is generated from the sts-2011-06-15.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.SecurityToken.Model
{
    /// <summary>
    /// A document that contains additional information about the authorization status of
    /// a request from an encoded message that is returned in response to an AWS request.
    /// </summary>
    public partial class DecodeAuthorizationMessageResponse : AmazonWebServiceResponse
    {
        private string _decodedMessage;

        /// <summary>
        /// Gets and sets the property DecodedMessage. 
        /// <para>
        /// An XML document that contains the decoded message. For more information, see <code>DecodeAuthorizationMessage</code>.
        /// 
        /// </para>
        /// </summary>
        public string DecodedMessage
        {
            get { return this._decodedMessage; }
            set { this._decodedMessage = value; }
        }

        // Check to see if DecodedMessage property is set
        internal bool IsSetDecodedMessage()
        {
            return this._decodedMessage != null;
        }

    }
}
