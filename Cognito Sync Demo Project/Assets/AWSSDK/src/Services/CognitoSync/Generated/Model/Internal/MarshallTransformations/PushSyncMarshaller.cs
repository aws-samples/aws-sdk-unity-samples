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
    /// PushSync Marshaller
    /// </summary>       
    public class PushSyncMarshaller : IRequestMarshaller<PushSync, JsonMarshallerContext> 
    {
        public void Marshall(PushSync requestObject, JsonMarshallerContext context)
        {
            if(requestObject.IsSetApplicationArns())
            {
                context.Writer.WritePropertyName("ApplicationArns");
                context.Writer.WriteArrayStart();
                foreach(var requestObjectApplicationArnsListValue in requestObject.ApplicationArns)
                {
                        context.Writer.Write(requestObjectApplicationArnsListValue);
                }
                context.Writer.WriteArrayEnd();
            }

            if(requestObject.IsSetRoleArn())
            {
                context.Writer.WritePropertyName("RoleArn");
                context.Writer.Write(requestObject.RoleArn);
            }

        }

        public readonly static PushSyncMarshaller Instance = new PushSyncMarshaller();

    }
}
