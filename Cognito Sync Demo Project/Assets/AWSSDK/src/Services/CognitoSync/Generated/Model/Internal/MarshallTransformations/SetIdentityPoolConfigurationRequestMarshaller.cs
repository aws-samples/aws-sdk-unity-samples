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
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using Amazon.CognitoSync.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.CognitoSync.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// SetIdentityPoolConfiguration Request Marshaller
    /// </summary>       
    public class SetIdentityPoolConfigurationRequestMarshaller : IMarshaller<IRequest, SetIdentityPoolConfigurationRequest> , IMarshaller<IRequest,AmazonWebServiceRequest>
    {
        public IRequest Marshall(AmazonWebServiceRequest input)
        {
            return this.Marshall((SetIdentityPoolConfigurationRequest)input);
        }

        public IRequest Marshall(SetIdentityPoolConfigurationRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.CognitoSync");
            request.Headers["Content-Type"] = "application/x-amz-json-1.1";
            request.HttpMethod = "POST";

            string uriResourcePath = "/identitypools/{IdentityPoolId}/configuration";
            uriResourcePath = uriResourcePath.Replace("{IdentityPoolId}", publicRequest.IsSetIdentityPoolId() ? StringUtils.FromString(publicRequest.IdentityPoolId) : string.Empty);
            request.ResourcePath = uriResourcePath;
            using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                JsonWriter writer = new JsonWriter(stringWriter);
                writer.WriteObjectStart();
                var context = new JsonMarshallerContext(request, writer);
                if(publicRequest.IsSetCognitoStreams())
                {
                    context.Writer.WritePropertyName("CognitoStreams");
                    context.Writer.WriteObjectStart();

                    var marshaller = CognitoStreamsMarshaller.Instance;
                    marshaller.Marshall(publicRequest.CognitoStreams, context);

                    context.Writer.WriteObjectEnd();
                }

                if(publicRequest.IsSetPushSync())
                {
                    context.Writer.WritePropertyName("PushSync");
                    context.Writer.WriteObjectStart();

                    var marshaller = PushSyncMarshaller.Instance;
                    marshaller.Marshall(publicRequest.PushSync, context);

                    context.Writer.WriteObjectEnd();
                }

        
                writer.WriteObjectEnd();
                string snippet = stringWriter.ToString();
                request.Content = System.Text.Encoding.UTF8.GetBytes(snippet);
            }


            return request;
        }


    }
}
