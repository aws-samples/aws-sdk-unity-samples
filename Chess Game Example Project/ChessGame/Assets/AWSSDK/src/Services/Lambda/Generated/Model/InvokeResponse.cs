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
    /// Upon success, returns an empty response. Otherwise, throws an exception.
    /// </summary>
    public partial class InvokeResponse : AmazonWebServiceResponse
    {
        private string _functionError;
        private string _logResult;
        private MemoryStream _payload;
        private int? _statusCode;

        /// <summary>
        /// Gets and sets the property FunctionError. 
        /// <para>
        /// Indicates whether an error occurred while executing the Lambda function. If an error
        /// occurred this field will have one of two values; <code>Handled</code> or <code>Unhandled</code>.
        /// <code>Handled</code> errors are errors that are reported by the function while the
        /// <code>Unhandled</code> errors are those detected and reported by AWS Lambda. Unhandled
        /// errors include out of memory errors and function timeouts. For information about how
        /// to report an <code>Handled</code> error, see <a href="http://docs.aws.amazon.com/lambda/latest/dg/programming-model.html">Programming
        /// Model</a>. 
        /// </para>
        /// </summary>
        public string FunctionError
        {
            get { return this._functionError; }
            set { this._functionError = value; }
        }

        // Check to see if FunctionError property is set
        internal bool IsSetFunctionError()
        {
            return this._functionError != null;
        }

        /// <summary>
        /// Gets and sets the property LogResult. 
        /// <para>
        ///  It is the base64-encoded logs for the Lambda function invocation. This is present
        /// only if the invocation type is "RequestResponse" and the logs were requested. 
        /// </para>
        /// </summary>
        public string LogResult
        {
            get { return this._logResult; }
            set { this._logResult = value; }
        }

        // Check to see if LogResult property is set
        internal bool IsSetLogResult()
        {
            return this._logResult != null;
        }

        /// <summary>
        /// Gets and sets the property Payload. 
        /// <para>
        ///  It is the JSON representation of the object returned by the Lambda function. In This
        /// is present only if the invocation type is "RequestResponse". 
        /// </para>
        ///  
        /// <para>
        /// In the event of a function error this field contains a message describing the error.
        /// For the <code>Handled</code> errors the Lambda function will report this message.
        /// For <code>Unhandled</code> errors AWS Lambda reports the message. 
        /// </para>
        /// </summary>
        public MemoryStream Payload
        {
            get { return this._payload; }
            set { this._payload = value; }
        }

        // Check to see if Payload property is set
        internal bool IsSetPayload()
        {
            return this._payload != null;
        }

        /// <summary>
        /// Gets and sets the property StatusCode. 
        /// <para>
        /// The HTTP status code will be in the 200 range for successful request. For the "RequestResonse"
        /// invocation type this status code will be 200. For the "Event" invocation type this
        /// status code will be 202. For the "DryRun" invocation type the status code will be
        /// 204. 
        /// </para>
        /// </summary>
        public int StatusCode
        {
            get { return this._statusCode.GetValueOrDefault(); }
            set { this._statusCode = value; }
        }

        // Check to see if StatusCode property is set
        internal bool IsSetStatusCode()
        {
            return this._statusCode.HasValue; 
        }

    }
}