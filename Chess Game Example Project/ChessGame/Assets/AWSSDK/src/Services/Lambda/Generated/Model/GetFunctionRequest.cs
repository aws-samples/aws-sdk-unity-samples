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
 * Do not modify this file. This file is generated from the lambda-2015-03-31.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.Lambda.Model
{
    /// <summary>
    /// Container for the parameters to the GetFunction operation.
    /// Returns the configuration information of the Lambda function and a presigned URL link
    /// to the .zip file you uploaded with <a>CreateFunction</a> so you can download the .zip
    /// file. Note that the URL is valid for up to 10 minutes. The configuration information
    /// is the same information you provided as parameters when uploading the function.
    /// 
    ///  
    /// <para>
    /// This operation requires permission for the <code>lambda:GetFunction</code> action.
    /// </para>
    /// </summary>
    public partial class GetFunctionRequest : AmazonLambdaRequest
    {
        private string _functionName;

        /// <summary>
        /// Gets and sets the property FunctionName. 
        /// <para>
        /// The Lambda function name. 
        /// </para>
        ///  
        /// <para>
        ///  You can specify an unqualified function name (for example, "Thumbnail") or you can
        /// specify Amazon Resource Name (ARN) of the function (for example, "arn:aws:lambda:us-west-2:account-id:function:ThumbNail").
        /// AWS Lambda also allows you to specify only the account ID qualifier (for example,
        /// "account-id:Thumbnail"). Note that the length constraint applies only to the ARN.
        /// If you specify only the function name, it is limited to 64 character in length. 
        /// </para>
        /// </summary>
        public string FunctionName
        {
            get { return this._functionName; }
            set { this._functionName = value; }
        }

        // Check to see if FunctionName property is set
        internal bool IsSetFunctionName()
        {
            return this._functionName != null;
        }

    }
}