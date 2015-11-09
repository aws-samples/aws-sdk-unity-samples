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

using Amazon.Util;

namespace Amazon.Lambda.Model
{
    public partial class InvokeAsyncRequest : AmazonLambdaRequest
    {
        /// <summary>
        /// Gets and sets the property InvokeArgs. When this property is set the InvokeArgsStream
        /// property is also set with a MemoryStream containing the contents InvokeArgs
        /// <para>
        /// JSON that you want to provide to your cloud function as input.
        /// </para>
        /// </summary>
        public string InvokeArgs
        {
            get
            {
                string content = null;
                if (this.InvokeArgsStream != null)
                {
                    content = new StreamReader(this.InvokeArgsStream).ReadToEnd();
                    this.InvokeArgsStream.Position = 0;
                }
                return content;
            }
            set
            {
                if (value == null)
                    this.InvokeArgsStream = null;
                else
                    this.InvokeArgsStream = AWSSDKUtils.GenerateMemoryStreamFromString(value);
            }
        }
    }
}