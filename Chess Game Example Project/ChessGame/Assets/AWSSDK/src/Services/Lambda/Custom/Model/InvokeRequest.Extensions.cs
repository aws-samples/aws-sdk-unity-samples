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
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Util;

namespace Amazon.Lambda.Model
{
    public partial class InvokeRequest : AmazonLambdaRequest
    {
        /// <summary>
        /// Gets and sets the property Payload. When this property is set the PayloadStream
        /// property is also set with a MemoryStream containing the contents of Payload.
        /// <para>
        /// JSON that you want to provide to your cloud function as input.
        /// </para>
        /// </summary>
        public string Payload
        {
            get
            {
                string content = null;
                if (this.PayloadStream != null)
                {
                    content = new StreamReader(this.PayloadStream).ReadToEnd();
                    this.PayloadStream.Position = 0;
                }
                return content;
            }
            set
            {
                if (value == null)
                    this.PayloadStream = null;
                else
                    this.PayloadStream = AWSSDKUtils.GenerateMemoryStreamFromString(value);
            }
        }

        /// <summary>
        /// Gets and sets the property ClientContext. When this property is set the ClientContextBase64
        /// property is also set with a base64-encoded string containing the contents of ClientContext.
        /// <para>
        /// Using the <code>ClientContext</code> you can pass client-specific information to the
        /// Lambda function you are invoking. You can then process the client information in your
        /// Lambda function as you choose through the context variable. For an example of a ClientContext
        /// JSON, go to <a href="http://docs.aws.amazon.com/mobileanalytics/latest/ug/PutEvents.html">PutEvents</a>
        /// in the <i>Amazon Mobile Analytics API Reference and User Guide</i>.
        /// </para>
        /// </summary>
        public string ClientContext
        {
            get
            {
                string content = null;
                if (this.ClientContextBase64 != null)
                {
                    var bytes = Convert.FromBase64String(ClientContextBase64);
                    content = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                }
                return content;
            }
            set
            {
                if (value == null)
                    this.ClientContextBase64 = null;
                else
                {
                    var bytes = Encoding.UTF8.GetBytes(value);
                    this.ClientContextBase64 = Convert.ToBase64String(bytes);
                }
            }
        }
    }
}