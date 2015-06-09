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
    public class AmazonCognitoSyncException : AmazonServiceException
    {    
        public AmazonCognitoSyncException() : base(ResponseUnmarshaller.GetDefaultErrorMessage<AmazonCognitoSyncException>())
        {
        }

        public AmazonCognitoSyncException(string message)
            : base(message)
        {
        }

        public AmazonCognitoSyncException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public AmazonCognitoSyncException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }

        public AmazonCognitoSyncException(string message, ErrorType errorType, string errorCode, string requestId, HttpStatusCode statusCode)
            : base(message, errorType, errorCode, requestId, statusCode)
        {
        }

        public AmazonCognitoSyncException(string message, Exception innerException, ErrorType errorType, string errorCode, string requestId, HttpStatusCode statusCode)
            : base(message, innerException, errorType, errorCode, requestId, statusCode)
        {
        }
    }
}
