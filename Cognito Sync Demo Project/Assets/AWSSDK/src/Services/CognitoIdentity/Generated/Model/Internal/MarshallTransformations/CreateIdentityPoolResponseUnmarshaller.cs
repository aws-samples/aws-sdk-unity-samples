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
 * Do not modify this file. This file is generated from the cognito-identity-2014-06-30.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

using Amazon.CognitoIdentity.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.CognitoIdentity.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Response Unmarshaller for CreateIdentityPool operation
    /// </summary>  
    public class CreateIdentityPoolResponseUnmarshaller : JsonResponseUnmarshaller, ISimplifiedErrorUnmarshaller 
    {
        public override AmazonWebServiceResponse Unmarshall(JsonUnmarshallerContext context)
        {
            CreateIdentityPoolResponse response = new CreateIdentityPoolResponse();

            context.Read();
            int targetDepth = context.CurrentDepth;
            while (context.ReadAtDepth(targetDepth))
            {
                if (context.TestExpression("AllowUnauthenticatedIdentities", targetDepth))
                {
                    var unmarshaller = BoolUnmarshaller.Instance;
                    response.AllowUnauthenticatedIdentities = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("DeveloperProviderName", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    response.DeveloperProviderName = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("IdentityPoolId", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    response.IdentityPoolId = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("IdentityPoolName", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    response.IdentityPoolName = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("OpenIdConnectProviderARNs", targetDepth))
                {
                    var unmarshaller = new ListUnmarshaller<string, StringUnmarshaller>(StringUnmarshaller.Instance);
                    response.OpenIdConnectProviderARNs = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("SupportedLoginProviders", targetDepth))
                {
                    var unmarshaller = new DictionaryUnmarshaller<string, string, StringUnmarshaller, StringUnmarshaller>(StringUnmarshaller.Instance, StringUnmarshaller.Instance);
                    response.SupportedLoginProviders = unmarshaller.Unmarshall(context);
                    continue;
                }
            }

            return response;
        }

        public override AmazonServiceException UnmarshallException(JsonUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            ErrorResponse errorResponse = JsonErrorResponseUnmarshaller.GetInstance().Unmarshall(context);
            if (errorResponse.Code != null && errorResponse.Code.Equals("InternalErrorException"))
            {
                return new InternalErrorException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("InvalidParameterException"))
            {
                return new InvalidParameterException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("LimitExceededException"))
            {
                return new LimitExceededException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("NotAuthorizedException"))
            {
                return new NotAuthorizedException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("ResourceConflictException"))
            {
                return new ResourceConflictException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("TooManyRequestsException"))
            {
                return new TooManyRequestsException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            return new AmazonCognitoIdentityException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
        }

        public AmazonServiceException UnmarshallException(IWebResponseData response, ErrorResponse errorResponse, Exception innerException)
        {
            if (!string.IsNullOrEmpty(errorResponse.Code) && errorResponse.Code.StartsWith("InternalErrorException"))
            {
                string message = string.IsNullOrEmpty(errorResponse.Message)?GetDefaultErrorMessage<AmazonCognitoIdentityException>():errorResponse.Message;
                return new InternalErrorException(message, innerException, ErrorType.Unknown, errorResponse.Code, errorResponse.RequestId, response.StatusCode);
            }
            if (!string.IsNullOrEmpty(errorResponse.Code) && errorResponse.Code.StartsWith("InvalidParameterException"))
            {
                string message = string.IsNullOrEmpty(errorResponse.Message)?GetDefaultErrorMessage<AmazonCognitoIdentityException>():errorResponse.Message;
                return new InvalidParameterException(message, innerException, ErrorType.Unknown, errorResponse.Code, errorResponse.RequestId, response.StatusCode);
            }
            if (!string.IsNullOrEmpty(errorResponse.Code) && errorResponse.Code.StartsWith("LimitExceededException"))
            {
                string message = string.IsNullOrEmpty(errorResponse.Message)?GetDefaultErrorMessage<AmazonCognitoIdentityException>():errorResponse.Message;
                return new LimitExceededException(message, innerException, ErrorType.Unknown, errorResponse.Code, errorResponse.RequestId, response.StatusCode);
            }
            if (!string.IsNullOrEmpty(errorResponse.Code) && errorResponse.Code.StartsWith("NotAuthorizedException"))
            {
                string message = string.IsNullOrEmpty(errorResponse.Message)?GetDefaultErrorMessage<AmazonCognitoIdentityException>():errorResponse.Message;
                return new NotAuthorizedException(message, innerException, ErrorType.Unknown, errorResponse.Code, errorResponse.RequestId, response.StatusCode);
            }
            if (!string.IsNullOrEmpty(errorResponse.Code) && errorResponse.Code.StartsWith("ResourceConflictException"))
            {
                string message = string.IsNullOrEmpty(errorResponse.Message)?GetDefaultErrorMessage<AmazonCognitoIdentityException>():errorResponse.Message;
                return new ResourceConflictException(message, innerException, ErrorType.Unknown, errorResponse.Code, errorResponse.RequestId, response.StatusCode);
            }
            if (!string.IsNullOrEmpty(errorResponse.Code) && errorResponse.Code.StartsWith("TooManyRequestsException"))
            {
                string message = string.IsNullOrEmpty(errorResponse.Message)?GetDefaultErrorMessage<AmazonCognitoIdentityException>():errorResponse.Message;
                return new TooManyRequestsException(message, innerException, ErrorType.Unknown, errorResponse.Code, errorResponse.RequestId, response.StatusCode);
            }
            return new AmazonCognitoIdentityException(GetDefaultErrorMessage<AmazonCognitoIdentityException>(), innerException, ErrorType.Unknown, errorResponse.Code, errorResponse.RequestId, response.StatusCode);
        }

        private static CreateIdentityPoolResponseUnmarshaller _instance = new CreateIdentityPoolResponseUnmarshaller();        

        internal static CreateIdentityPoolResponseUnmarshaller GetInstance()
        {
            return _instance;
        }
        public static CreateIdentityPoolResponseUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

    }
}
