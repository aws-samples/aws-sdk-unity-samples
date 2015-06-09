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
    /// Response Unmarshaller for Credentials Object
    /// </summary>  
    public class CredentialsUnmarshaller : IUnmarshaller<Credentials, XmlUnmarshallerContext>, IUnmarshaller<Credentials, JsonUnmarshallerContext>
    {
        public Credentials Unmarshall(XmlUnmarshallerContext context)
        {
            Credentials unmarshalledObject = new Credentials();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.ReadAtDepth(originalDepth))
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("AccessKeyId", targetDepth))
                    {
                        var unmarshaller = StringUnmarshaller.Instance;
                        unmarshalledObject.AccessKeyId = unmarshaller.Unmarshall(context);
                        continue;
                    }
                    if (context.TestExpression("Expiration", targetDepth))
                    {
                        var unmarshaller = DateTimeUnmarshaller.Instance;
                        unmarshalledObject.Expiration = unmarshaller.Unmarshall(context);
                        continue;
                    }
                    if (context.TestExpression("SecretAccessKey", targetDepth))
                    {
                        var unmarshaller = StringUnmarshaller.Instance;
                        unmarshalledObject.SecretAccessKey = unmarshaller.Unmarshall(context);
                        continue;
                    }
                    if (context.TestExpression("SessionToken", targetDepth))
                    {
                        var unmarshaller = StringUnmarshaller.Instance;
                        unmarshalledObject.SessionToken = unmarshaller.Unmarshall(context);
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

        public Credentials Unmarshall(JsonUnmarshallerContext context)
        {
            return null;
        }


        private static CredentialsUnmarshaller _instance = new CredentialsUnmarshaller();        

        public static CredentialsUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
