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

using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using Amazon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// Unity specific implementation of the ErrorHandler class.
    /// </summary>
    /// <typeparam name="T">The default exception type to be thrown.</typeparam>
    public class ErrorHandler<T> : ErrorHandler where T : AmazonServiceException, new()
    {
        /// <summary>
        /// Constructor for ErrorHandler.
        /// </summary>
        /// <param name="logger">an ILogger instance.</param>
        public ErrorHandler(ILogger logger) : base (logger)
        {
            this.Logger = logger;

            this.ExceptionHandlers = new Dictionary<Type, IExceptionHandler>
            {                
                {typeof(UnityHttpErrorResponseException), new UnityHttpErrorResponseExceptionHandler<T>(this.Logger)}
            };
        }
    }
}
