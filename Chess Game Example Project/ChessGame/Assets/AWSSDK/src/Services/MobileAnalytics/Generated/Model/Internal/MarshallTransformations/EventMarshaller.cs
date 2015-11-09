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
    /// Event Marshaller
    /// </summary>       
    public class EventMarshaller : IRequestMarshaller<Event, JsonMarshallerContext> 
    {
        /// <summary>
        /// Unmarshaller the response from the service to the response class.
        /// </summary>  
        /// <param name="requestObject"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public void Marshall(Event requestObject, JsonMarshallerContext context)
        {
            if(requestObject.IsSetAttributes())
            {
                context.Writer.WritePropertyName("attributes");
                context.Writer.WriteObjectStart();
                foreach (var requestObjectAttributesKvp in requestObject.Attributes)
                {
                    context.Writer.WritePropertyName(requestObjectAttributesKvp.Key);
                    var requestObjectAttributesValue = requestObjectAttributesKvp.Value;

                        context.Writer.Write(requestObjectAttributesValue);
                }
                context.Writer.WriteObjectEnd();
            }

            if(requestObject.IsSetEventType())
            {
                context.Writer.WritePropertyName("eventType");
                context.Writer.Write(requestObject.EventType);
            }

            if(requestObject.IsSetMetrics())
            {
                context.Writer.WritePropertyName("metrics");
                context.Writer.WriteObjectStart();
                foreach (var requestObjectMetricsKvp in requestObject.Metrics)
                {
                    context.Writer.WritePropertyName(requestObjectMetricsKvp.Key);
                    var requestObjectMetricsValue = requestObjectMetricsKvp.Value;

                        context.Writer.Write(requestObjectMetricsValue);
                }
                context.Writer.WriteObjectEnd();
            }

            if(requestObject.IsSetSession())
            {
                context.Writer.WritePropertyName("session");
                context.Writer.WriteObjectStart();

                var marshaller = SessionMarshaller.Instance;
                marshaller.Marshall(requestObject.Session, context);

                context.Writer.WriteObjectEnd();
            }

            if(requestObject.IsSetTimestamp())
            {
                context.Writer.WritePropertyName("timestamp");
                context.Writer.Write(Amazon.Runtime.Internal.Util.StringUtils.FromDateTime(requestObject.Timestamp));
            }

            if(requestObject.IsSetVersion())
            {
                context.Writer.WritePropertyName("version");
                context.Writer.Write(requestObject.Version);
            }

        }

        /// <summary>
        /// Singleton Marshaller.
        /// </summary>  
        public readonly static EventMarshaller Instance = new EventMarshaller();

    }
}