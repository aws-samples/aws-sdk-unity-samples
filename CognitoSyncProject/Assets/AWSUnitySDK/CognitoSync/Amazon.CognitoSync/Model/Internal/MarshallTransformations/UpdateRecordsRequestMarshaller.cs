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

using Amazon.CognitoSync.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.CognitoSync.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// UpdateRecords Request Marshaller
    /// </summary>       
    public class UpdateRecordsRequestMarshaller : IMarshaller<IRequest, UpdateRecordsRequest> 
    {
        public IRequest Marshall(UpdateRecordsRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.CognitoSync");
            request.Headers["Content-Type"] = "application/x-amz-json-1.1";
            request.HttpMethod = "POST";

            string uriResourcePath = "/identitypools/{IdentityPoolId}/identities/{IdentityId}/datasets/{DatasetName}";
            uriResourcePath = uriResourcePath.Replace("{DatasetName}", publicRequest.IsSetDatasetName() ? StringUtils.FromString(publicRequest.DatasetName) : string.Empty);
            uriResourcePath = uriResourcePath.Replace("{IdentityId}", publicRequest.IsSetIdentityId() ? StringUtils.FromString(publicRequest.IdentityId) : string.Empty);
            uriResourcePath = uriResourcePath.Replace("{IdentityPoolId}", publicRequest.IsSetIdentityPoolId() ? StringUtils.FromString(publicRequest.IdentityPoolId) : string.Empty);
        
            if(publicRequest.IsSetClientContext())     
                request.Headers["x-amz-Client-Context"] = publicRequest.ClientContext;
            request.ResourcePath = uriResourcePath;
            using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                JsonWriter writer = new JsonWriter(stringWriter);
                writer.WriteObjectStart();
                if(publicRequest.IsSetRecordPatches())
                {
                    writer.WritePropertyName("RecordPatches");
                    writer.WriteArrayStart();
                    foreach(var publicRequestRecordPatchesListValue in publicRequest.RecordPatches)
                    {
                        writer.WriteObjectStart();
                        if(publicRequestRecordPatchesListValue.IsSetDeviceLastModifiedDate())
                        {
                            writer.WritePropertyName("DeviceLastModifiedDate");
                            writer.Write(publicRequestRecordPatchesListValue.DeviceLastModifiedDate);
                        }

                        if(publicRequestRecordPatchesListValue.IsSetKey())
                        {
                            writer.WritePropertyName("Key");
                            writer.Write(publicRequestRecordPatchesListValue.Key);
                        }

                        if(publicRequestRecordPatchesListValue.IsSetOp())
                        {
                            writer.WritePropertyName("Op");
                            writer.Write(publicRequestRecordPatchesListValue.Op);
                        }

                        if(publicRequestRecordPatchesListValue.IsSetSyncCount())
                        {
                            writer.WritePropertyName("SyncCount");
                            writer.Write(publicRequestRecordPatchesListValue.SyncCount);
                        }

                        if(publicRequestRecordPatchesListValue.IsSetValue())
                        {
                            writer.WritePropertyName("Value");
                            writer.Write(publicRequestRecordPatchesListValue.Value);
                        }

                        writer.WriteObjectEnd();
                    }
                    writer.WriteArrayEnd();
                }

                if(publicRequest.IsSetSyncSessionToken())
                {
                    writer.WritePropertyName("SyncSessionToken");
                    writer.Write(publicRequest.SyncSessionToken);
                }

        
                writer.WriteObjectEnd();
                string snippet = stringWriter.ToString();
                request.Content = System.Text.Encoding.UTF8.GetBytes(snippet);
            }


            return request;
        }


    }
}