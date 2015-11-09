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
    /// UpdateFunctionCode Request Marshaller
    /// </summary>       
    public class UpdateFunctionCodeRequestMarshaller : IMarshaller<IRequest, UpdateFunctionCodeRequest> , IMarshaller<IRequest,AmazonWebServiceRequest>
    {
        /// <summary>
        /// Marshaller the request object to the HTTP request.
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        public IRequest Marshall(AmazonWebServiceRequest input)
        {
            return this.Marshall((UpdateFunctionCodeRequest)input);
        }

        /// <summary>
        /// Marshaller the request object to the HTTP request.
        /// </summary>  
        /// <param name="publicRequest"></param>
        /// <returns></returns>
        public IRequest Marshall(UpdateFunctionCodeRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.Lambda");
            request.Headers["Content-Type"] = "application/x-amz-json-";
            request.HttpMethod = "PUT";

            string uriResourcePath = "/2015-03-31/functions/{FunctionName}/versions/HEAD/code";
            uriResourcePath = uriResourcePath.Replace("{FunctionName}", publicRequest.IsSetFunctionName() ? StringUtils.FromString(publicRequest.FunctionName) : string.Empty);
            request.ResourcePath = uriResourcePath;
            using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                JsonWriter writer = new JsonWriter(stringWriter);
                writer.WriteObjectStart();
                var context = new JsonMarshallerContext(request, writer);
                if(publicRequest.IsSetS3Bucket())
                {
                    context.Writer.WritePropertyName("S3Bucket");
                    context.Writer.Write(publicRequest.S3Bucket);
                }

                if(publicRequest.IsSetS3Key())
                {
                    context.Writer.WritePropertyName("S3Key");
                    context.Writer.Write(publicRequest.S3Key);
                }

                if(publicRequest.IsSetS3ObjectVersion())
                {
                    context.Writer.WritePropertyName("S3ObjectVersion");
                    context.Writer.Write(publicRequest.S3ObjectVersion);
                }

                if(publicRequest.IsSetZipFile())
                {
                    context.Writer.WritePropertyName("ZipFile");
                    context.Writer.Write(StringUtils.FromMemoryStream(publicRequest.ZipFile));
                }

        
                writer.WriteObjectEnd();
                string snippet = stringWriter.ToString();
                request.Content = System.Text.Encoding.UTF8.GetBytes(snippet);
            }


            return request;
        }


    }
}