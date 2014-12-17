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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

using UnityEngine;

namespace Amazon.Runtime.Internal.Transform
{
    /// <summary>
    /// A container for response data
    /// </summary>
    internal class WWWResponseData : IWebResponseData
    {
        private Dictionary<string, string> _responseHeaders;
        private byte[] _bytes;

        public string Error { get; set; }

        private Stream _response = null;

        public WWWResponseData(WWW request)
        {
            if (!Amazon.Unity.AmazonMainThreadDispatcher.IsMainThread)
                throw new InvalidOperationException("Supported only on main(game) thread");

            this._responseHeaders = request.responseHeaders;
            this.Error = request.error;

            if (request.error == null)
                this._bytes = request.bytes;
        }

        public string ContentType
        {
            get { return this._responseHeaders.ContainsKey("CONTENT-TYPE") ? this._responseHeaders["CONTENT-TYPE"] : null; }
        }

        public byte[] GetBytes()
        {
            return this._bytes;
        }

        public Stream OpenResponse()
        {
            if (null == _response)
            {
                _response = new MemoryStream(this._bytes);
            }

            return _response;
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode),
                                         _responseHeaders["STATUS"].Substring(13).Replace(" ", ""));

            }
        }

        public HttpStatusCode ErrorStatusCode
        {
            get
            {
                int statusCode = 0;
                // Error is of the form : "400 Bad Request"
                if (string.IsNullOrEmpty(this.Error))
                    throw new Exception("WWW error is null, cannot parse error code");
                else if (Int32.TryParse(this.Error.Substring(0,3), out statusCode))
                    return (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode),
                                                  this.Error.Substring(3).Replace(" ", ""));
                else
                    return 0;

            }
        }

        public bool IsHeaderPresent(string headerName)
        {

            return _responseHeaders.ContainsKey(headerName);
        }

        public string[] GetHeaderNames()
        {
            return _responseHeaders.Keys.ToArray();
        }

        public string GetHeaderValue(string name)
        {
            if (this._responseHeaders.ContainsKey(name))
                return this._responseHeaders[name];
            else
                return null;
        }
    }
}