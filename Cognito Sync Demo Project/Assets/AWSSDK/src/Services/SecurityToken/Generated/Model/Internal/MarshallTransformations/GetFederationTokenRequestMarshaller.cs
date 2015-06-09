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
    /// GetFederationToken Request Marshaller
    /// </summary>       
    public class GetFederationTokenRequestMarshaller : IMarshaller<IRequest, GetFederationTokenRequest> , IMarshaller<IRequest,AmazonWebServiceRequest>
    {
        public IRequest Marshall(AmazonWebServiceRequest input)
        {
            return this.Marshall((GetFederationTokenRequest)input);
        }
    
        public IRequest Marshall(GetFederationTokenRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.SecurityToken");
            request.Parameters.Add("Action", "GetFederationToken");
            request.Parameters.Add("Version", "2011-06-15");

            if(publicRequest != null)
            {
                if(publicRequest.IsSetDurationSeconds())
                {
                    request.Parameters.Add("DurationSeconds", StringUtils.FromInt(publicRequest.DurationSeconds));
                }
                if(publicRequest.IsSetName())
                {
                    request.Parameters.Add("Name", StringUtils.FromString(publicRequest.Name));
                }
                if(publicRequest.IsSetPolicy())
                {
                    request.Parameters.Add("Policy", StringUtils.FromString(publicRequest.Policy));
                }
            }
            return request;
        }
    }
}
