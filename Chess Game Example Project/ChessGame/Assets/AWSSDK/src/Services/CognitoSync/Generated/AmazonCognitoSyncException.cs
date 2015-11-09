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
 * Do not modify this file. This file is generated from the cognito-sync-2014-06-30.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

using Amazon.Runtime;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.CognitoSync
{
    ///<summary>
    /// Common exception for the CognitoSync service.
    /// </summary>
    public class AmazonCognitoSyncException : AmazonServiceException
    {
        /// <summary>
        /// Construct instance of AmazonCognitoSyncException
        /// </summary>
        /// <param name="message"></param>
        public AmazonCognitoSyncException() : base(ResponseUnmarshaller.GetDefaultErrorMessage<AmazonCognitoSyncException>())
        {
        }

        public AmazonCognitoSyncException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Construct instance of AmazonCognitoSyncException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AmazonCognitoSyncException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Construct instance of AmazonCognitoSyncException
        /// </summary>
        /// <param name="innerException"></param>
        public AmazonCognitoSyncException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// Construct instance of AmazonCognitoSyncException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorType"></param>
        /// <param name="errorCode"></param>
        /// <param name="requestId"></param>
        /// <param name="statusCode"></param>
        public AmazonCognitoSyncException(string message, ErrorType errorType, string errorCode, string requestId, HttpStatusCode statusCode)
            : base(message, errorType, errorCode, requestId, statusCode)
        {
        }

        /// <summary>
        /// Construct instance of AmazonCognitoSyncException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="errorType"></param>
        /// <param name="errorCode"></param>
        /// <param name="requestId"></param>
        /// <param name="statusCode"></param>
        public AmazonCognitoSyncException(string message, Exception innerException, ErrorType errorType, string errorCode, string requestId, HttpStatusCode statusCode)
            : base(message, innerException, errorType, errorCode, requestId, statusCode)
        {
        }
    }
}