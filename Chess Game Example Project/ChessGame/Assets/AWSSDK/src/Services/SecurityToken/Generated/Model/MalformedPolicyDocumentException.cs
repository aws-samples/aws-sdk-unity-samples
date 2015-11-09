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
using System.Net;
using Amazon.Runtime;

namespace Amazon.SecurityToken.Model
{
    ///<summary>
    /// SecurityTokenService exception
    /// </summary>
    public class MalformedPolicyDocumentException : AmazonSecurityTokenServiceException 
    {
        /// <summary>
        /// Constructs a new MalformedPolicyDocumentException with the specified error
        /// message.
        /// </summary>
        /// <param name="message">
        /// Describes the error encountered.
        /// </param>
        public MalformedPolicyDocumentException(string message) 
            : base(message) {}
          
        /// <summary>
        /// Construct instance of MalformedPolicyDocumentException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MalformedPolicyDocumentException(string message, Exception innerException) 
            : base(message, innerException) {}
            
        /// <summary>
        /// Construct instance of MalformedPolicyDocumentException
        /// </summary>
        /// <param name="innerException"></param>
        public MalformedPolicyDocumentException(Exception innerException) 
            : base(innerException) {}
            
        /// <summary>
        /// Construct instance of MalformedPolicyDocumentException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="errorType"></param>
        /// <param name="errorCode"></param>
        /// <param name="requestId"></param>
        /// <param name="statusCode"></param>
        public MalformedPolicyDocumentException(string message, Exception innerException, ErrorType errorType, string errorCode, string requestId, HttpStatusCode statusCode) 
            : base(message, innerException, errorType, errorCode, requestId, statusCode) {}

        /// <summary>
        /// Construct instance of MalformedPolicyDocumentException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorType"></param>
        /// <param name="errorCode"></param>
        /// <param name="requestId"></param>
        /// <param name="statusCode"></param>
        public MalformedPolicyDocumentException(string message, ErrorType errorType, string errorCode, string requestId, HttpStatusCode statusCode) 
            : base(message, errorType, errorCode, requestId, statusCode) {}

    }
}