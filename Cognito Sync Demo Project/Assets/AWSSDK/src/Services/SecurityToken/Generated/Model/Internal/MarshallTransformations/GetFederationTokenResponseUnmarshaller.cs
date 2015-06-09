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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

using Amazon.SecurityToken.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
namespace Amazon.SecurityToken.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Response Unmarshaller for GetFederationToken operation
    /// </summary>  
    public class GetFederationTokenResponseUnmarshaller : XmlResponseUnmarshaller
    {
        public override AmazonWebServiceResponse Unmarshall(XmlUnmarshallerContext context)
        {
            GetFederationTokenResponse response = new GetFederationTokenResponse();

            context.Read();
            int targetDepth = context.CurrentDepth;
            while (context.ReadAtDepth(targetDepth))
            {
                if (context.IsStartElement)
                {                    
                    if(context.TestExpression("GetFederationTokenResult", 2))
                    {
                        UnmarshallResult(context, response);                        
                        continue;
                    }
                    
                    if (context.TestExpression("ResponseMetadata", 2))
                    {
                        response.ResponseMetadata = ResponseMetadataUnmarshaller.Instance.Unmarshall(context);
                    }
                }
            }

            return response;
        }

        private static void UnmarshallResult(XmlUnmarshallerContext context, GetFederationTokenResponse response)
        {
            
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.ReadAtDepth(originalDepth))
            {
                if (context.IsStartElement || context.IsAttribute)
                {

                    if (context.TestExpression("Credentials", targetDepth))
                    {
                        var unmarshaller = CredentialsUnmarshaller.Instance;
                        response.Credentials = unmarshaller.Unmarshall(context);
                        continue;
                    }
                    if (context.TestExpression("FederatedUser", targetDepth))
                    {
                        var unmarshaller = FederatedUserUnmarshaller.Instance;
                        response.FederatedUser = unmarshaller.Unmarshall(context);
                        continue;
                    }
                    if (context.TestExpression("PackedPolicySize", targetDepth))
                    {
                        var unmarshaller = IntUnmarshaller.Instance;
                        response.PackedPolicySize = unmarshaller.Unmarshall(context);
                        continue;
                    }
                } 
           }

            return;
        }


        public override AmazonServiceException UnmarshallException(XmlUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            ErrorResponse errorResponse = ErrorResponseUnmarshaller.GetInstance().Unmarshall(context);
            if (errorResponse.Code != null && errorResponse.Code.Equals("MalformedPolicyDocument"))
            {
                return new MalformedPolicyDocumentException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("PackedPolicyTooLarge"))
            {
                return new PackedPolicyTooLargeException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            return new AmazonSecurityTokenServiceException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
        }
        private static GetFederationTokenResponseUnmarshaller _instance = new GetFederationTokenResponseUnmarshaller();        

        internal static GetFederationTokenResponseUnmarshaller GetInstance()
        {
            return _instance;
        }
        public static GetFederationTokenResponseUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

    }
}
