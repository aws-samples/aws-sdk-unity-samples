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
 * Do not modify this file. This file is generated from the mobileanalytics-2014-06-05.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

using Amazon.MobileAnalytics.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.MobileAnalytics.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Response Unmarshaller for PutEvents operation
    /// </summary>  
    public class PutEventsResponseUnmarshaller : JsonResponseUnmarshaller, ISimplifiedErrorUnmarshaller 
    {
        /// <summary>
        /// Unmarshaller the response from the service to the response class.
        /// </summary>  
        /// <param name="context"></param>
        /// <returns></returns>
        public override AmazonWebServiceResponse Unmarshall(JsonUnmarshallerContext context)
        {
            PutEventsResponse response = new PutEventsResponse();


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
            if (errorResponse.Code != null && errorResponse.Code.Equals("BadRequestException"))
            {
                return new BadRequestException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            return new AmazonMobileAnalyticsException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
        }

        public AmazonServiceException UnmarshallException(IWebResponseData response, ErrorResponse errorResponse, Exception innerException)
        {
            if (!string.IsNullOrEmpty(errorResponse.Code) && errorResponse.Code.StartsWith("BadRequestException", StringComparison.OrdinalIgnoreCase))
            {
                string message = string.IsNullOrEmpty(errorResponse.Message)?GetDefaultErrorMessage<AmazonMobileAnalyticsException>():errorResponse.Message;
                return new BadRequestException(message, innerException, ErrorType.Unknown, errorResponse.Code, errorResponse.RequestId, response.StatusCode);
            }
            return new AmazonMobileAnalyticsException(GetDefaultErrorMessage<AmazonMobileAnalyticsException>(), innerException, ErrorType.Unknown, errorResponse.Code, errorResponse.RequestId, response.StatusCode);
        }
        private static PutEventsResponseUnmarshaller _instance = new PutEventsResponseUnmarshaller();        

        internal static PutEventsResponseUnmarshaller GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// Gets the singleton.
        /// </summary>  
        public static PutEventsResponseUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

    }
}