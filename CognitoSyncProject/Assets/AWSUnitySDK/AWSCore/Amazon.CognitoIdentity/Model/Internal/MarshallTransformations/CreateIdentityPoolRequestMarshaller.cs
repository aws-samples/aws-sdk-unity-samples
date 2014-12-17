/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
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
    /// CreateIdentityPool Request Marshaller
    /// </summary>       
    public class CreateIdentityPoolRequestMarshaller : IMarshaller<IRequest, CreateIdentityPoolRequest> 
    {
        public IRequest Marshall(CreateIdentityPoolRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.CognitoIdentity");
            string target = "AWSCognitoIdentityService.CreateIdentityPool";
            request.Headers["X-Amz-Target"] = target;
            request.Headers["Content-Type"] = "application/x-amz-json-1.1";
            request.HttpMethod = "POST";

            string uriResourcePath = "/";
            request.ResourcePath = uriResourcePath;
            using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                JsonWriter writer = new JsonWriter(stringWriter);
                writer.WriteObjectStart();
                if(publicRequest.IsSetAllowUnauthenticatedIdentities())
                {
                    writer.WritePropertyName("AllowUnauthenticatedIdentities");
                    writer.Write(publicRequest.AllowUnauthenticatedIdentities);
                }

                if(publicRequest.IsSetDeveloperProviderName())
                {
                    writer.WritePropertyName("DeveloperProviderName");
                    writer.Write(publicRequest.DeveloperProviderName);
                }

                if(publicRequest.IsSetIdentityPoolName())
                {
                    writer.WritePropertyName("IdentityPoolName");
                    writer.Write(publicRequest.IdentityPoolName);
                }

                if(publicRequest.IsSetOpenIdConnectProviderARNs())
                {
                    writer.WritePropertyName("OpenIdConnectProviderARNs");
                    writer.WriteArrayStart();
                    foreach(var publicRequestOpenIdConnectProviderARNsListValue in publicRequest.OpenIdConnectProviderARNs)
                    {
                        writer.Write(publicRequestOpenIdConnectProviderARNsListValue);
                    }
                    writer.WriteArrayEnd();
                }

                if(publicRequest.IsSetSupportedLoginProviders())
                {
                    writer.WritePropertyName("SupportedLoginProviders");
                    writer.WriteObjectStart();
                    foreach (var publicRequestSupportedLoginProvidersKvp in publicRequest.SupportedLoginProviders)
                    {
                        writer.WritePropertyName(publicRequestSupportedLoginProvidersKvp.Key);
                        var publicRequestSupportedLoginProvidersValue = publicRequestSupportedLoginProvidersKvp.Value;

                        writer.Write(publicRequestSupportedLoginProvidersValue);
                    }
                    writer.WriteObjectEnd();
                }

        
                writer.WriteObjectEnd();
                string snippet = stringWriter.ToString();
                request.Content = System.Text.Encoding.UTF8.GetBytes(snippet);
            }


            return request;
        }


    }
}