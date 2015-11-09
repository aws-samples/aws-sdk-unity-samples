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
 * Do not modify this file. This file is generated from the dynamodb-2012-08-10.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.DynamoDBv2.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// BatchWriteItem Request Marshaller
    /// </summary>       
    public class BatchWriteItemRequestMarshaller : IMarshaller<IRequest, BatchWriteItemRequest> , IMarshaller<IRequest,AmazonWebServiceRequest>
    {
        /// <summary>
        /// Marshaller the request object to the HTTP request.
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
        public IRequest Marshall(AmazonWebServiceRequest input)
        {
            return this.Marshall((BatchWriteItemRequest)input);
        }

        /// <summary>
        /// Marshaller the request object to the HTTP request.
        /// </summary>  
        /// <param name="publicRequest"></param>
        /// <returns></returns>
        public IRequest Marshall(BatchWriteItemRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.DynamoDBv2");
            string target = "DynamoDB_20120810.BatchWriteItem";
            request.Headers["X-Amz-Target"] = target;
            request.Headers["Content-Type"] = "application/x-amz-json-1.0";
            request.HttpMethod = "POST";

            string uriResourcePath = "/";
            request.ResourcePath = uriResourcePath;
            using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                JsonWriter writer = new JsonWriter(stringWriter);
                writer.WriteObjectStart();
                var context = new JsonMarshallerContext(request, writer);
                if(publicRequest.IsSetRequestItems())
                {
                    context.Writer.WritePropertyName("RequestItems");
                    context.Writer.WriteObjectStart();
                    foreach (var publicRequestRequestItemsKvp in publicRequest.RequestItems)
                    {
                        context.Writer.WritePropertyName(publicRequestRequestItemsKvp.Key);
                        var publicRequestRequestItemsValue = publicRequestRequestItemsKvp.Value;

                        context.Writer.WriteArrayStart();
                        foreach(var publicRequestRequestItemsValueListValue in publicRequestRequestItemsValue)
                        {
                            context.Writer.WriteObjectStart();

                            var marshaller = WriteRequestMarshaller.Instance;
                            marshaller.Marshall(publicRequestRequestItemsValueListValue, context);

                            context.Writer.WriteObjectEnd();
                        }
                        context.Writer.WriteArrayEnd();
                    }
                    context.Writer.WriteObjectEnd();
                }

                if(publicRequest.IsSetReturnConsumedCapacity())
                {
                    context.Writer.WritePropertyName("ReturnConsumedCapacity");
                    context.Writer.Write(publicRequest.ReturnConsumedCapacity);
                }

                if(publicRequest.IsSetReturnItemCollectionMetrics())
                {
                    context.Writer.WritePropertyName("ReturnItemCollectionMetrics");
                    context.Writer.Write(publicRequest.ReturnItemCollectionMetrics);
                }

        
                writer.WriteObjectEnd();
                string snippet = stringWriter.ToString();
                request.Content = System.Text.Encoding.UTF8.GetBytes(snippet);
            }


            return request;
        }


    }
}