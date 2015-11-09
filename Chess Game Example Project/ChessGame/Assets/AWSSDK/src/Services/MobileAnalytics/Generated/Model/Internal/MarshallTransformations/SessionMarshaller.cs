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
 * Do not modify this file. This file is generated from the mobileanalytics-2014-06-05.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using Amazon.MobileAnalytics.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.MobileAnalytics.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Session Marshaller
    /// </summary>       
    public class SessionMarshaller : IRequestMarshaller<Session, JsonMarshallerContext> 
    {
        /// <summary>
        /// Unmarshaller the response from the service to the response class.
        /// </summary>  
        /// <param name="requestObject"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public void Marshall(Session requestObject, JsonMarshallerContext context)
        {
            if(requestObject.IsSetDuration())
            {
                context.Writer.WritePropertyName("duration");
                context.Writer.Write(requestObject.Duration);
            }

            if(requestObject.IsSetId())
            {
                context.Writer.WritePropertyName("id");
                context.Writer.Write(requestObject.Id);
            }

            if(requestObject.IsSetStartTimestamp())
            {
                context.Writer.WritePropertyName("startTimestamp");
                context.Writer.Write(Amazon.Runtime.Internal.Util.StringUtils.FromDateTime(requestObject.StartTimestamp));
            }

            if(requestObject.IsSetStopTimestamp())
            {
                context.Writer.WritePropertyName("stopTimestamp");
                context.Writer.Write(Amazon.Runtime.Internal.Util.StringUtils.FromDateTime(requestObject.StopTimestamp));
            }

        }

        /// <summary>
        /// Singleton Marshaller.
        /// </summary>  
        public readonly static SessionMarshaller Instance = new SessionMarshaller();

    }
}