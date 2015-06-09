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
    /// UpdateIdentityPool Request Marshaller
    /// </summary>       
    public class UpdateIdentityPoolRequestMarshaller : IMarshaller<IRequest, UpdateIdentityPoolRequest> , IMarshaller<IRequest,AmazonWebServiceRequest>
    {
        public IRequest Marshall(AmazonWebServiceRequest input)
        {
            return this.Marshall((UpdateIdentityPoolRequest)input);
        }

        public IRequest Marshall(UpdateIdentityPoolRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.CognitoIdentity");
            string target = "AWSCognitoIdentityService.UpdateIdentityPool";
            request.Headers["X-Amz-Target"] = target;
            request.Headers["Content-Type"] = "application/x-amz-json-1.1";
            request.HttpMethod = "POST";

            string uriResourcePath = "/";
            request.ResourcePath = uriResourcePath;
            using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                JsonWriter writer = new JsonWriter(stringWriter);
                writer.WriteObjectStart();
                var context = new JsonMarshallerContext(request, writer);
                if(publicRequest.IsSetAllowUnauthenticatedIdentities())
                {
                    context.Writer.WritePropertyName("AllowUnauthenticatedIdentities");
                    context.Writer.Write(publicRequest.AllowUnauthenticatedIdentities);
                }

                if(publicRequest.IsSetDeveloperProviderName())
                {
                    context.Writer.WritePropertyName("DeveloperProviderName");
                    context.Writer.Write(publicRequest.DeveloperProviderName);
                }

                if(publicRequest.IsSetIdentityPoolId())
                {
                    context.Writer.WritePropertyName("IdentityPoolId");
                    context.Writer.Write(publicRequest.IdentityPoolId);
                }

                if(publicRequest.IsSetIdentityPoolName())
                {
                    context.Writer.WritePropertyName("IdentityPoolName");
                    context.Writer.Write(publicRequest.IdentityPoolName);
                }

                if(publicRequest.IsSetOpenIdConnectProviderARNs())
                {
                    context.Writer.WritePropertyName("OpenIdConnectProviderARNs");
                    context.Writer.WriteArrayStart();
                    foreach(var publicRequestOpenIdConnectProviderARNsListValue in publicRequest.OpenIdConnectProviderARNs)
                    {
                            context.Writer.Write(publicRequestOpenIdConnectProviderARNsListValue);
                    }
                    context.Writer.WriteArrayEnd();
                }

                if(publicRequest.IsSetSupportedLoginProviders())
                {
                    context.Writer.WritePropertyName("SupportedLoginProviders");
                    context.Writer.WriteObjectStart();
                    foreach (var publicRequestSupportedLoginProvidersKvp in publicRequest.SupportedLoginProviders)
                    {
                        context.Writer.WritePropertyName(publicRequestSupportedLoginProvidersKvp.Key);
                        var publicRequestSupportedLoginProvidersValue = publicRequestSupportedLoginProvidersKvp.Value;

                            context.Writer.Write(publicRequestSupportedLoginProvidersValue);
                    }
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
