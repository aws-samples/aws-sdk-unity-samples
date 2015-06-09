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
    /// ListRecords Request Marshaller
    /// </summary>       
    public class ListRecordsRequestMarshaller : IMarshaller<IRequest, ListRecordsRequest> , IMarshaller<IRequest,AmazonWebServiceRequest>
    {
        public IRequest Marshall(AmazonWebServiceRequest input)
        {
            return this.Marshall((ListRecordsRequest)input);
        }

        public IRequest Marshall(ListRecordsRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.CognitoSync");
            request.HttpMethod = "GET";

            string uriResourcePath = "/identitypools/{IdentityPoolId}/identities/{IdentityId}/datasets/{DatasetName}/records";
            uriResourcePath = uriResourcePath.Replace("{DatasetName}", publicRequest.IsSetDatasetName() ? StringUtils.FromString(publicRequest.DatasetName) : string.Empty);
            uriResourcePath = uriResourcePath.Replace("{IdentityId}", publicRequest.IsSetIdentityId() ? StringUtils.FromString(publicRequest.IdentityId) : string.Empty);
            uriResourcePath = uriResourcePath.Replace("{IdentityPoolId}", publicRequest.IsSetIdentityPoolId() ? StringUtils.FromString(publicRequest.IdentityPoolId) : string.Empty);
            
            if (publicRequest.IsSetLastSyncCount())
                request.Parameters.Add("lastSyncCount", StringUtils.FromLong(publicRequest.LastSyncCount));
            
            if (publicRequest.IsSetMaxResults())
                request.Parameters.Add("maxResults", StringUtils.FromInt(publicRequest.MaxResults));
            
            if (publicRequest.IsSetNextToken())
                request.Parameters.Add("nextToken", StringUtils.FromString(publicRequest.NextToken));
            
            if (publicRequest.IsSetSyncSessionToken())
                request.Parameters.Add("syncSessionToken", StringUtils.FromString(publicRequest.SyncSessionToken));
            request.ResourcePath = uriResourcePath;
            request.UseQueryString = true;

            return request;
        }


    }
}
