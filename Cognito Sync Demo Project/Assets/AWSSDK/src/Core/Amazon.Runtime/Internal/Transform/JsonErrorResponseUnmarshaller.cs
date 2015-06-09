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

using Amazon.Runtime.Internal;
using System;
using System.Xml;


namespace Amazon.Runtime.Internal.Transform
{
    /// <summary>
    ///    Response Unmarshaller for all Errors
    /// </summary>
    public class JsonErrorResponseUnmarshaller : IUnmarshaller<ErrorResponse, JsonUnmarshallerContext>
    {
        /// <summary>
        /// Build an ErrorResponse from json 
        /// </summary>
        /// <param name="context">The json parsing context. 
        /// Usually an <c>Amazon.Runtime.Internal.JsonUnmarshallerContext</c>.</param>
        /// <returns>An <c>ErrorResponse</c> object.</returns>
        public ErrorResponse Unmarshall(JsonUnmarshallerContext context)
        {
            ErrorResponse response = new ErrorResponse();

            if (context.Peek() == 60) //starts with '<' so assuming XML.
            {
                ErrorResponseUnmarshaller xmlUnmarshaller = new ErrorResponseUnmarshaller();
                XmlUnmarshallerContext xmlContext = new XmlUnmarshallerContext(context.Stream, false, null);
                response = xmlUnmarshaller.Unmarshall(xmlContext);
            }
            else
            {
                while (context.Read())
                {
                    if (context.TestExpression("__type"))
                    {
                        string type = StringUnmarshaller.GetInstance().Unmarshall(context);
                        response.Code = type.Substring(type.LastIndexOf("#", StringComparison.Ordinal) + 1);
                        if (Enum.IsDefined(typeof(ErrorType), type))
                        {
                            response.Type = (ErrorType)Enum.Parse(typeof(ErrorType), type, true);
                        }
                        else
                        {
                            response.Type = ErrorType.Unknown;
                        }
                        continue;
                    }
                    if (context.TestExpression("code"))
                    {
                        response.Code = StringUnmarshaller.GetInstance().Unmarshall(context);
                        continue;
                    }
                    if (context.TestExpression("message"))
                    {
                        response.Message = StringUnmarshaller.GetInstance().Unmarshall(context);
                        continue;
                    }
                }
            }

            return response;
        }

        private static JsonErrorResponseUnmarshaller instance;

        /// <summary>
        /// Return an instance of JsonErrorResponseUnmarshaller.
        /// </summary>
        /// <returns></returns>
        public static JsonErrorResponseUnmarshaller GetInstance()
        {
            if (instance == null)
                instance = new JsonErrorResponseUnmarshaller();

            return instance;
        }
    }
}
