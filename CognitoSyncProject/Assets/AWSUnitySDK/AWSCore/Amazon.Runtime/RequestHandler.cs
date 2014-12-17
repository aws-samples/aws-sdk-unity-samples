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
using System.Text;
using Amazon.Runtime.Internal;
using System.Net;
using System.Collections.Specialized;

namespace Amazon.Runtime
{
    public class RequestEventArgs : EventArgs
    {
        #region Constructor

        protected RequestEventArgs() { }
        
        #endregion
    }

    public class WebServiceRequestEventArgs : RequestEventArgs
    {
        #region Constructor

        protected WebServiceRequestEventArgs() { }

        #endregion

        #region Properties

        public IDictionary<string, string> Headers { get; protected set; }
        public IDictionary<string, string> Parameters { get; protected set; }
        public string ServiceName { get; protected set; }
        public Uri Endpoint { get; protected set; }
        public AmazonWebServiceRequest Request { get; protected set; }
        [Obsolete("OriginalRequest property has been deprecated in favor of the Request property")]
        public AmazonWebServiceRequest OriginalRequest { get { return Request; } }

        #endregion

        #region Creator method

        internal static WebServiceRequestEventArgs Create(IRequest request)
        {
            WebServiceRequestEventArgs args = new WebServiceRequestEventArgs
            {
                Headers = request.Headers,
                Parameters = request.Parameters,
                ServiceName = request.ServiceName,
                Request = request.OriginalRequest,
                Endpoint = request.Endpoint
            };
            return args;
        }

        #endregion
    }

    public class HeadersRequestEventArgs : RequestEventArgs
    {
        protected HeadersRequestEventArgs() { }
        public IDictionary<string, string> Headers { get; protected set; }

        internal static HeadersRequestEventArgs Create(IDictionary<string, string> headers)
        {
            HeadersRequestEventArgs args = new HeadersRequestEventArgs
            {
                Headers = headers
            };
            return args;
        }
    }

    public delegate void RequestEventHandler(object sender, RequestEventArgs args);
}
