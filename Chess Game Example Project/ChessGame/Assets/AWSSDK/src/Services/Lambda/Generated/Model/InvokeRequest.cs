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
    /// Container for the parameters to the Invoke operation.
    /// Invokes a specified Lambda function. 
    /// 
    ///  
    /// <para>
    /// This operation requires permission for the <code>lambda:InvokeFunction</code> action.
    /// </para>
    /// </summary>
    public partial class InvokeRequest : AmazonLambdaRequest
    {
        private string _clientContextBase64;
        private string _functionName;
        private InvocationType _invocationType;
        private LogType _logType;
        private MemoryStream _payloadStream;

        /// <summary>
        /// Gets and sets the property ClientContextBase64. 
        /// <para>
        /// Using the <code>ClientContext</code> you can pass client-specific information to the
        /// Lambda function you are invoking. You can then process the client information in your
        /// Lambda function as you choose through the context variable. For an example of a ClientContext
        /// JSON, go to <a href="http://docs.aws.amazon.com/mobileanalytics/latest/ug/PutEvents.html">PutEvents</a>
        /// in the <i>Amazon Mobile Analytics API Reference and User Guide</i>.
        /// </para>
        ///  
        /// <para>
        /// The ClientContext JSON must be base64-encoded.
        /// </para>
        /// </summary>
        public string ClientContextBase64
        {
            get { return this._clientContextBase64; }
            set { this._clientContextBase64 = value; }
        }

        // Check to see if ClientContextBase64 property is set
        internal bool IsSetClientContextBase64()
        {
            return this._clientContextBase64 != null;
        }

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

        /// <summary>
        /// Gets and sets the property InvocationType. 
        /// <para>
        /// By default, the <code>Invoke</code> API assumes "RequestResponse" invocation type.
        /// You can optionally request asynchronous execution by specifying "Event" as the <code>InvocationType</code>.
        /// You can also use this parameter to request AWS Lambda to not execute the function
        /// but do some verification, such as if the caller is authorized to invoke the function
        /// and if the inputs are valid. You request this by specifying "DryRun" as the <code>InvocationType</code>.
        /// This is useful in a cross-account scenario when you want to verify access to a function
        /// without running it. 
        /// </para>
        /// </summary>
        public InvocationType InvocationType
        {
            get { return this._invocationType; }
            set { this._invocationType = value; }
        }

        // Check to see if InvocationType property is set
        internal bool IsSetInvocationType()
        {
            return this._invocationType != null;
        }

        /// <summary>
        /// Gets and sets the property LogType. 
        /// <para>
        /// You can set this optional parameter to "Tail" in the request only if you specify the
        /// <code>InvocationType</code> parameter with value "RequestResponse". In this case,
        /// AWS Lambda returns the base64-encoded last 4 KB of log data produced by your Lambda
        /// function in the <code>x-amz-log-results</code> header. 
        /// </para>
        /// </summary>
        public LogType LogType
        {
            get { return this._logType; }
            set { this._logType = value; }
        }

        // Check to see if LogType property is set
        internal bool IsSetLogType()
        {
            return this._logType != null;
        }

        /// <summary>
        /// Gets and sets the property PayloadStream. 
        /// <para>
        /// JSON that you want to provide to your Lambda function as input.
        /// </para>
        /// </summary>
        public MemoryStream PayloadStream
        {
            get { return this._payloadStream; }
            set { this._payloadStream = value; }
        }

        // Check to see if PayloadStream property is set
        internal bool IsSetPayloadStream()
        {
            return this._payloadStream != null;
        }

    }
}