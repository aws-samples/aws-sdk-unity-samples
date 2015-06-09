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

using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using Amazon.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// The web request factory for Unity.
    /// </summary>
    public sealed class UnityWebRequestFactory : IHttpRequestFactory<string>
    {
        /// <summary>
        /// Creates an HTTP request for the given URI.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <returns>An HTTP request.</returns>
        public IHttpRequest<string> CreateHttpRequest(Uri requestUri)
        {
            return new UnityWebRequest(requestUri);
        }

        public void Dispose()
        {
            // This class does not have any disposable resources.
        }
    }

    /// <summary>
    /// An IHttpRequest implementation for Unity's WWW API.
    /// </summary>
    public sealed class UnityWebRequest : IHttpRequest<string>
    {
        /// <summary>
        /// The request URI.
        /// </summary>
        public Uri RequestUri { get; private set; }

        /// <summary>
        /// The WWW instance for this web request.
        /// </summary>
        public WWW WwwRequest { get; internal set; }

        /// <summary>
        /// The content to be sent in the request body.
        /// </summary>
        public byte[] RequestContent { get; private set; }

        /// <summary>
        /// The headers to be sent for this request.
        /// </summary>
        public Dictionary<string, string> Headers { get; private set; }

        /// <summary>
        /// The callback to be invoked after BeginGetResponse completes.
        /// </summary>
        public AsyncCallback Callback { get; private set; }

        /// <summary>
        /// The IAsyncResult for the BeginGetResponse method.
        /// This stores the async state to be passed to the callback after this request
        /// is procesed by the UnityMainThreadDispatcher.
        /// </summary>
        public IAsyncResult AsyncResult { get; private set; }

        /// <summary>
        /// A wait handle that is used to implement the synchronous
        /// GetResponse method.
        /// </summary>
        public ManualResetEvent WaitHandle { get; private set; }

        /// <summary>
        /// A flag to indicate if this request is being invoked
        /// synchronously.
        /// </summary>
        public bool IsSync { get; set; }

        /// <summary>
        /// The response for this web request.
        /// </summary>
        public UnityWebResponseData Response { get; internal set; }

        /// <summary>
        /// Any exceptions resulting from this web request
        /// are set on this property.
        /// </summary>
        public Exception Exception { get; internal set; }

        /// <summary>
        /// The HTTP method or verb.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// The contructor for UnityWebRequest.
        /// </summary>
        /// <param name="requestUri">Uri for the request.</param>
        public UnityWebRequest(Uri requestUri)
        {         
            this.RequestUri = requestUri;
            this.Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Configures a request as per the request context.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        public void ConfigureRequest(IRequestContext requestContext)
        {
            // Advanced configurations (like timeout) are not supported
            // in the WWW API.
        }

        /// <summary>
        /// Sets the headers on the request.
        /// </summary>
        /// <param name="headers">A dictionary of header names and values.</param>
        public void SetRequestHeaders(IDictionary<string, string> headers)
        {            
            foreach (var item in headers)
            {
                this.Headers.Add(item);
            }
        }

        /// <summary>
        /// Gets a handle to the request content.
        /// </summary>
        /// <returns>The request content.</returns>
        public string GetRequestContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// Returns the HTTP response.
        /// </summary>
        /// <returns>The HTTP response.</returns>
        public IWebResponseData GetResponse()
        {
	    if (UnityInitializer.IsMainThread())
                throw new Exception("Network on game thread");

            this.IsSync = true;
            this.WaitHandle = new ManualResetEvent(false);
            try
            {
                UnityRequestQueue.Instance.EnqueueRequest(this);
                this.WaitHandle.WaitOne();

                if (this.Exception != null)
                    throw this.Exception;

                return this.Response;
            }
            finally
            {
                this.WaitHandle.Close();
            }
        }

        /// <summary>
        /// Writes a stream to the request body.
        /// </summary>
        /// <param name="requestContent">The destination where the content stream is written.</param>
        /// <param name="contentStream">The content stream to be written.</param>
        /// <param name="contentHeaders">HTTP content headers.</param>
        /// <param name="requestContext">The request context.</param>
        public void WriteToRequestBody(string requestContent, Stream contentStream,
            IDictionary<string, string> contentHeaders, IRequestContext requestContext)
        {
            var buffer = new byte[AWSSDKUtils.DefaultBufferSize];
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int read;
                while ((read = contentStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, read);
                }
                this.RequestContent = memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Writes a byte array to the request body.
        /// </summary>
        /// <param name="requestContent">The destination where the content is written.</param>
        /// <param name="content">The content to be written.</param>
        /// <param name="contentHeaders">HTTP content headers.</param>
        public void WriteToRequestBody(string requestContent, byte[] content,
            IDictionary<string, string> contentHeaders)
        {
            this.RequestContent = content;
        }

        /// <summary>
        /// Aborts the HTTP request.
        /// </summary>
        public void Abort()
        {
            // Not supported by the WWW API. 
        }

        /// <summary>
        /// Initiates the operation to gets a handle to the request content.
        /// </summary>
        /// <param name="callback">The async callback invoked when the operation completes.</param>
        /// <param name="state">The state object to be passed to the async callback.</param>
        /// <returns>IAsyncResult that represents an async operation.</returns>
        public IAsyncResult BeginGetRequestContent(AsyncCallback callback, object state)
        {
            // The WWW API only allows representing the request body as a string,
            // it does not have a method which returns the request stream.
            // Invoke the callback synchronously to indicate that this method has completed execution.
            var result = new SimpleAsyncResult(state);

            //Invoke the callback synchronously
            callback(result);
            return result;
        }

        /// <summary>
        /// Ends the operation to gets a handle to the request content.
        /// </summary>
        /// <param name="asyncResult">IAsyncResult that represents an async operation.</param>
        /// <returns>The request content.</returns>
        public string EndGetRequestContent(IAsyncResult asyncResult)
        {
            return string.Empty;
        }

        /// <summary>
        /// Initiates the operation to returns the HTTP response.
        /// </summary>
        /// <param name="callback">The async callback invoked when the operation completes.</param>
        /// <param name="state">The state object to be passed to the async callback.</param>
        /// <returns>IAsyncResult that represents an async operation.</returns>
        public IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            this.Callback = callback;
            this.AsyncResult = new SimpleAsyncResult(state);
            UnityRequestQueue.Instance.EnqueueRequest(this);
            return this.AsyncResult;
        }

        /// <summary>
        /// Ends the operation to Returns the HTTP response.
        /// </summary>
        /// <param name="asyncResult">IAsyncResult that represents an async operation.</param>
        /// <returns>The HTTP response.</returns>
        public IWebResponseData EndGetResponse(IAsyncResult asyncResult)
        {
            if (this.Exception != null)
            {
                var executionContext = asyncResult.AsyncState as IAsyncExecutionContext;
                executionContext.ResponseContext.AsyncResult.Exception = this.Exception;
            }

            return this.Response;
        }

        public void Dispose()
        {
            // This class does not have any disposable resources.
        }
    }

    /// <summary>
    /// Represents a simplified async result that stores the result from a completed operation.
    /// It does not provide an implemention of the AsyncWaitHandle property
    /// as the internal consumers of this class don't use this property.    
    /// </summary>
    internal class SimpleAsyncResult : IAsyncResult
    {
        public SimpleAsyncResult(object state)
        {
            this.AsyncState = state;
        }

        public object AsyncState { get; private set; }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get { throw new NotSupportedException(); }
        }

        public bool CompletedSynchronously
        {
            get { return true; }
        }

        public bool IsCompleted
        {
            get { return true; }
        }
    }
}
