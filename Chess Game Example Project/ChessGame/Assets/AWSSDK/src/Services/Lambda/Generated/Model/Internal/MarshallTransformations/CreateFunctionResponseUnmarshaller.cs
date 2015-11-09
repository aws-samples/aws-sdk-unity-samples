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
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

using Amazon.Lambda.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.Lambda.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Response Unmarshaller for CreateFunction operation
    /// </summary>  
    public class CreateFunctionResponseUnmarshaller : JsonResponseUnmarshaller    {
        /// <summary>
        /// Unmarshaller the response from the service to the response class.
        /// </summary>  
        /// <param name="context"></param>
        /// <returns></returns>
        public override AmazonWebServiceResponse Unmarshall(JsonUnmarshallerContext context)
        {
            CreateFunctionResponse response = new CreateFunctionResponse();

            context.Read();
            int targetDepth = context.CurrentDepth;
            while (context.ReadAtDepth(targetDepth))
            {
                if (context.TestExpression("CodeSize", targetDepth))
                {
                    var unmarshaller = LongUnmarshaller.Instance;
                    response.CodeSize = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("Description", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    response.Description = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("FunctionArn", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    response.FunctionArn = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("FunctionName", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    response.FunctionName = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("Handler", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    response.Handler = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("LastModified", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    response.LastModified = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("MemorySize", targetDepth))
                {
                    var unmarshaller = IntUnmarshaller.Instance;
                    response.MemorySize = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("Role", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    response.Role = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("Runtime", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    response.Runtime = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("Timeout", targetDepth))
                {
                    var unmarshaller = IntUnmarshaller.Instance;
                    response.Timeout = unmarshaller.Unmarshall(context);
                    continue;
                }
            }

            return response;
        }

        /// <summary>
        /// Unmarshaller error response to exception.
        /// </summary>  
        /// <param name="context"></param>
        /// <param name="innerException"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public override AmazonServiceException UnmarshallException(JsonUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            ErrorResponse errorResponse = JsonErrorResponseUnmarshaller.GetInstance().Unmarshall(context);
            if (errorResponse.Code != null && errorResponse.Code.Equals("CodeStorageExceededException"))
            {
                return new CodeStorageExceededException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("InvalidParameterValueException"))
            {
                return new InvalidParameterValueException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("ResourceConflictException"))
            {
                return new ResourceConflictException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("ResourceNotFoundException"))
            {
                return new ResourceNotFoundException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("ServiceException"))
            {
                return new ServiceException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("TooManyRequestsException"))
            {
                return new TooManyRequestsException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            return new AmazonLambdaException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
        }

        private static CreateFunctionResponseUnmarshaller _instance = new CreateFunctionResponseUnmarshaller();        

        internal static CreateFunctionResponseUnmarshaller GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// Gets the singleton.
        /// </summary>  
        public static CreateFunctionResponseUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

    }
}