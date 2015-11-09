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
    /// Container for the parameters to the UpdateFunctionConfiguration operation.
    /// Updates the configuration parameters for the specified Lambda function by using the
    /// values provided in the request. You provide only the parameters you want to change.
    /// This operation must only be used on an existing Lambda function and cannot be used
    /// to update the function's code. 
    /// 
    ///  
    /// <para>
    /// This operation requires permission for the <code>lambda:UpdateFunctionConfiguration</code>
    /// action.
    /// </para>
    /// </summary>
    public partial class UpdateFunctionConfigurationRequest : AmazonLambdaRequest
    {
        private string _description;
        private string _functionName;
        private string _handler;
        private int? _memorySize;
        private string _role;
        private int? _timeout;

        /// <summary>
        /// Gets and sets the property Description. 
        /// <para>
        /// A short user-defined function description. AWS Lambda does not use this value. Assign
        /// a meaningful description as you see fit.
        /// </para>
        /// </summary>
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        // Check to see if Description property is set
        internal bool IsSetDescription()
        {
            return this._description != null;
        }

        /// <summary>
        /// Gets and sets the property FunctionName. 
        /// <para>
        /// The name of the Lambda function.
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
        /// Gets and sets the property Handler. 
        /// <para>
        /// The function that Lambda calls to begin executing your function. For Node.js, it is
        /// the <i>module-name.export</i> value in your function. 
        /// </para>
        /// </summary>
        public string Handler
        {
            get { return this._handler; }
            set { this._handler = value; }
        }

        // Check to see if Handler property is set
        internal bool IsSetHandler()
        {
            return this._handler != null;
        }

        /// <summary>
        /// Gets and sets the property MemorySize. 
        /// <para>
        /// The amount of memory, in MB, your Lambda function is given. AWS Lambda uses this memory
        /// size to infer the amount of CPU allocated to your function. Your function use-case
        /// determines your CPU and memory requirements. For example, a database operation might
        /// need less memory compared to an image processing function. The default value is 128
        /// MB. The value must be a multiple of 64 MB.
        /// </para>
        /// </summary>
        public int MemorySize
        {
            get { return this._memorySize.GetValueOrDefault(); }
            set { this._memorySize = value; }
        }

        // Check to see if MemorySize property is set
        internal bool IsSetMemorySize()
        {
            return this._memorySize.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property Role. 
        /// <para>
        /// The Amazon Resource Name (ARN) of the IAM role that Lambda will assume when it executes
        /// your function. 
        /// </para>
        /// </summary>
        public string Role
        {
            get { return this._role; }
            set { this._role = value; }
        }

        // Check to see if Role property is set
        internal bool IsSetRole()
        {
            return this._role != null;
        }

        /// <summary>
        /// Gets and sets the property Timeout. 
        /// <para>
        /// The function execution time at which AWS Lambda should terminate the function. Because
        /// the execution time has cost implications, we recommend you set this value based on
        /// your expected execution time. The default is 3 seconds. 
        /// </para>
        /// </summary>
        public int Timeout
        {
            get { return this._timeout.GetValueOrDefault(); }
            set { this._timeout = value; }
        }

        // Check to see if Timeout property is set
        internal bool IsSetTimeout()
        {
            return this._timeout.HasValue; 
        }

    }
}