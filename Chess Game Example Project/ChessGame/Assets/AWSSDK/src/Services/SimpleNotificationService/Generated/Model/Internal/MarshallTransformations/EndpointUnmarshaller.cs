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
 * Do not modify this file. This file is generated from the sns-2010-03-31.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

using Amazon.SimpleNotificationService.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
namespace Amazon.SimpleNotificationService.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Response Unmarshaller for Endpoint Object
    /// </summary>  
    public class EndpointUnmarshaller : IUnmarshaller<Endpoint, XmlUnmarshallerContext>, IUnmarshaller<Endpoint, JsonUnmarshallerContext>
    {
        /// <summary>
        /// Unmarshaller the response from the service to the response class.
        /// </summary>  
        /// <param name="context"></param>
        /// <returns></returns>
        public Endpoint Unmarshall(XmlUnmarshallerContext context)
        {
            Endpoint unmarshalledObject = new Endpoint();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.ReadAtDepth(originalDepth))
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Attributes/entry", targetDepth))
                    {
                        var unmarshaller = new KeyValueUnmarshaller<string, string, StringUnmarshaller, StringUnmarshaller>(StringUnmarshaller.Instance, StringUnmarshaller.Instance);
                        var item = unmarshaller.Unmarshall(context);
                        unmarshalledObject.Attributes.Add(item);
                        continue;
                    }
                    if (context.TestExpression("EndpointArn", targetDepth))
                    {
                        var unmarshaller = StringUnmarshaller.Instance;
                        unmarshalledObject.EndpointArn = unmarshaller.Unmarshall(context);
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return unmarshalledObject;
                }
            }

            return unmarshalledObject;
        }

        /// <summary>
        /// Unmarshaller error response to exception.
        /// </summary>  
        /// <param name="context"></param>
        /// <returns></returns>
        public Endpoint Unmarshall(JsonUnmarshallerContext context)
        {
            return null;
        }


        private static EndpointUnmarshaller _instance = new EndpointUnmarshaller();        

        /// <summary>
        /// Gets the singleton.
        /// </summary>  
        public static EndpointUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}