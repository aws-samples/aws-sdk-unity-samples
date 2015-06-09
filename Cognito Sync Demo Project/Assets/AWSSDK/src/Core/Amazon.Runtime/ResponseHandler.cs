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
using System.Collections.Generic;
using System.Text;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.Runtime
{
    public delegate void ResponseEventHandler(object sender, ResponseEventArgs e);

    public class ResponseEventArgs : EventArgs
    {
        #region Constructor

        protected ResponseEventArgs() { }

        #endregion
    }

    public class WebServiceResponseEventArgs : ResponseEventArgs
    {
        #region Constructor

        protected WebServiceResponseEventArgs() { }

        #endregion

        #region Properties

        public IDictionary<string, string> RequestHeaders { get; private set; }
        public IDictionary<string, string> ResponseHeaders { get; private set; }
        public IDictionary<string, string> Parameters { get; private set; }
        public string ServiceName { get; private set; }
        public Uri Endpoint { get; private set; }
        public AmazonWebServiceRequest Request { get; private set; }
        public AmazonWebServiceResponse Response { get; private set; }

        #endregion

        #region Creator method

        internal static WebServiceResponseEventArgs Create(AmazonWebServiceResponse response, IRequest request, IWebResponseData webResponseData)
        {
            WebServiceResponseEventArgs args = new WebServiceResponseEventArgs
            {
                RequestHeaders = request.Headers,
                Parameters = request.Parameters,
                ServiceName = request.ServiceName,
                Request = request.OriginalRequest,
                Endpoint = request.Endpoint,
                Response = response
            };
            args.ResponseHeaders = new Dictionary<string, string>();
            if (webResponseData != null)
            {
                var headerNames = webResponseData.GetHeaderNames();
                foreach (var responseHeaderName in headerNames)
                {
                    string responseHeaderValue = webResponseData.GetHeaderValue(responseHeaderName);
                    args.ResponseHeaders[responseHeaderName] = responseHeaderValue;
                }
            }
            return args;
        }

        #endregion
    }
}
