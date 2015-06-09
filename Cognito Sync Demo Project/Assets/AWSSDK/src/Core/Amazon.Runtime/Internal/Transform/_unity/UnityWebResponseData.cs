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

using Amazon.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using UnityEngine;
using Amazon.Runtime.Internal.Util;


namespace Amazon.Runtime.Internal.Transform
{
    /// <summary>
    /// Implementation of response interface for WWW API.
    /// </summary>
    public sealed class UnityWebResponseData : IWebResponseData, IHttpResponseBody
    {
        
        private Dictionary<string, string> _headers;
        private Stream _responseStream;
        private byte[] _responseBody;
        
        private ILogger _logger;
        /// <summary>
        /// The constructor for UnityWebResponseData.
        /// </summary>
        /// <param name="wwwRequest">
        /// An instance of WWW after the web request has 
        /// completed and response fields are set
        /// </param>
        public UnityWebResponseData(WWW wwwRequest)
        {
            _logger= Logger.GetLogger(this.GetType());
            _headers = wwwRequest.responseHeaders;
            
            if (wwwRequest.error == null)
            {
                _logger.DebugFormat(@"recieved successful response");
                _responseBody = wwwRequest.bytes;
            }
            else
            {
                _logger.DebugFormat(@"recieved error response");
            }

            if (_responseBody != null)
            {
                _logger.DebugFormat(@"{0}",System.Text.UTF8Encoding.UTF8.GetString(_responseBody));
                _responseStream = new MemoryStream(wwwRequest.bytes);
            }
            
            this.ContentLength = wwwRequest.bytesDownloaded;
            
            string contentType = null;
            this._headers.TryGetValue(
                HeaderKeys.ContentTypeHeader.ToUpperInvariant(), out contentType);
            this.ContentType = contentType;
            try
            {
                if(string.IsNullOrEmpty(wwwRequest.error))
                {
                    string statusHeader = string.Empty;
                    this._headers.TryGetValue(HeaderKeys.StatusHeader.ToUpperInvariant(),out statusHeader);
                    _logger.DebugFormat(@"Status = {0}",statusHeader );
                    if(!string.IsNullOrEmpty(statusHeader))
                    {
                        this.StatusCode = (HttpStatusCode)Enum.Parse(
                        typeof(HttpStatusCode),
                        statusHeader.Substring(9, 3).Trim());
                    }
                    else
                    {
                        this.StatusCode = 0;
                    }
                }
                else
                {
                    int statusCode;
                    if (Int32.TryParse(wwwRequest.error.Substring(0,3), out statusCode))
                        this.StatusCode =  (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode),
                                            wwwRequest.error.Substring(3).Replace(" ", "").Replace(":","").Trim(),true);//ignored case
                    else
                        this.StatusCode = 0;
                }
            }
            catch
            {
                this.StatusCode = 0;
            }
            this.IsSuccessStatusCode = wwwRequest.error == null?true:false;
        }

        /// <summary>
        /// Content length on the response.
        /// </summary>
        public long ContentLength { get; private set; }

        /// <summary>
        /// The content type of the response.
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// The HTTP status code for the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Returns a boolean value indicating if the status code 
        /// indicated a success.
        /// </summary>
        public bool IsSuccessStatusCode { get; private set; }

        /// <summary>
        /// Returns an array of header names.
        /// </summary>
        /// <returns>Array of header names.</returns>
        public string[] GetHeaderNames()
        {
            return _headers.Keys.ToArray();
        }

        /// <summary>
        /// Returns a boolean value indicating if the
        /// given header is present in the headers collection.
        /// </summary>
        /// <param name="headerName">Header name.</param>
        /// <returns>True if the header is present, else false.</returns>
        public bool IsHeaderPresent(string headerName)
        {
            return _headers.ContainsKey(headerName.ToUpperInvariant());
        }

        /// <summary>
        /// Gets the value for the given header.
        /// </summary>
        /// <param name="headerName">Header name.</param>
        /// <returns>Header value.</returns>
        public string GetHeaderValue(string headerName)
        {
            string headerValue = null;
            _headers.TryGetValue(headerName.ToUpperInvariant(), out headerValue);
            return headerValue;
        }

        /// <summary>
        /// Instance of IHttpResponseBody.
        /// </summary>
        public IHttpResponseBody ResponseBody
        {
            get { return this; }
        }

        #region IHttpResponseBody implementation

        /// <summary>
        /// Returns a stream that allows reading the response body.
        /// </summary>
        /// <returns>A stream representing the response body.</returns>
        public Stream OpenResponse()
        {            
            return _responseStream;
        }

        /// <summary>
        /// Disposes the response stream.
        /// </summary>
        public void Dispose()
        {
            if (_responseStream != null)
            {
                _responseStream.Dispose();
                _responseStream = null;
            }
        }

        #endregion
    }
}
