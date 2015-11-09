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
 * Do not modify this file. This file is generated from the dynamodb-2012-08-10.normal.json service model.
 */
using System;
using System.Net;
using Amazon.Runtime;

namespace Amazon.DynamoDBv2.Model
{
    ///<summary>
    /// DynamoDB exception
    /// </summary>
    public class InternalServerErrorException : AmazonDynamoDBException 
    {
        /// <summary>
        /// Constructs a new InternalServerErrorException with the specified error
        /// message.
        /// </summary>
        /// <param name="message">
        /// Describes the error encountered.
        /// </param>
        public InternalServerErrorException(string message) 
            : base(message) {}
          
        /// <summary>
        /// Construct instance of InternalServerErrorException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InternalServerErrorException(string message, Exception innerException) 
            : base(message, innerException) {}
            
        /// <summary>
        /// Construct instance of InternalServerErrorException
        /// </summary>
        /// <param name="innerException"></param>
        public InternalServerErrorException(Exception innerException) 
            : base(innerException) {}
            
        /// <summary>
        /// Construct instance of InternalServerErrorException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="errorType"></param>
        /// <param name="errorCode"></param>
        /// <param name="requestId"></param>
        /// <param name="statusCode"></param>
        public InternalServerErrorException(string message, Exception innerException, ErrorType errorType, string errorCode, string requestId, HttpStatusCode statusCode) 
            : base(message, innerException, errorType, errorCode, requestId, statusCode) {}

        /// <summary>
        /// Construct instance of InternalServerErrorException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorType"></param>
        /// <param name="errorCode"></param>
        /// <param name="requestId"></param>
        /// <param name="statusCode"></param>
        public InternalServerErrorException(string message, ErrorType errorType, string errorCode, string requestId, HttpStatusCode statusCode) 
            : base(message, errorType, errorCode, requestId, statusCode) {}

    }
}