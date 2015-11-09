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
using System;
using Amazon.Runtime;
using Amazon.Util;
using ThirdParty.Json.LitJson;
using System.IO;
using Amazon.MobileAnalytics.Model.Internal.MarshallTransformations;
using Amazon.Runtime.Internal.Transform;
using System.Net;
using System.Globalization;

namespace Amazon.MobileAnalytics.Model
{
    public partial class Event
    {

        private class DummyResponse : IWebResponseData
        {
            long IWebResponseData.ContentLength { get { return 0; } }
            string IWebResponseData.ContentType { get { return ""; } }
            HttpStatusCode IWebResponseData.StatusCode { get { return HttpStatusCode.OK; } }
            bool IWebResponseData.IsSuccessStatusCode { get { return false; } }
            IHttpResponseBody IWebResponseData.ResponseBody { get { return null; } }
            bool IWebResponseData.IsHeaderPresent(string headerName)
            {
                return false;
            }
            string IWebResponseData.GetHeaderValue(string headerName)
            {
                return null;
            }
            string[] IWebResponseData.GetHeaderNames()
            {
                return new string[0];
            }
        }


        /// <summary>
        /// Creates a Json string from the Event. Expects Event and Session Timestamps to be in UTC.
        /// </summary>
        public string MarshallToJson()
        {
            using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                JsonWriter writer = new JsonWriter(stringWriter);
                writer.WriteObjectStart();
                EventMarshaller.Instance.Marshall(this, new Runtime.Internal.Transform.JsonMarshallerContext(null, writer));
                writer.WriteObjectEnd();
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Creates an Event object from Json.
        /// </summary>
        /// <param name="eventValue">
        /// The Json string representing the Event.
        /// </param>
        public static Event UnmarshallFromJson(String eventValue)
        {
            using (MemoryStream responseStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(responseStream))
                {
                    writer.Write(eventValue);
                    writer.Flush();
                    responseStream.Position = 0;
                    return EventUnmarshaller.Instance.Unmarshall(new Runtime.Internal.Transform.JsonUnmarshallerContext(responseStream, false, new DummyResponse()));
                }
            }
        }
    }
}
