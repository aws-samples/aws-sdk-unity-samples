#define AWS_APM_API
#define AWSSDK_UNITY
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
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// The HTTP handler contains common logic for issuing an HTTP request that is 
    /// independent of the underlying HTTP infrastructure.
    /// </summary>
    /// <typeparam name="TRequestContent"></typeparam>
    public class HttpHandler<TRequestContent> : PipelineHandler, IDisposable
    {
        private bool _disposed;
        private IHttpRequestFactory<TRequestContent> _requestFactory;

        /// <summary>
        /// The sender parameter used in any events raised by this handler.
        /// </summary>
        public object CallbackSender { get; private set; }

        /// <summary>
        /// The constructor for HttpHandler.
        /// </summary>
        /// <param name="requestFactory">The request factory used to create HTTP Requests.</param>
        /// <param name="callbackSender">The sender parameter used in any events raised by this handler.</param>
        public HttpHandler(IHttpRequestFactory<TRequestContent> requestFactory, object callbackSender)
        {
            _requestFactory = requestFactory;
            this.CallbackSender = callbackSender;
        }

        /// <summary>
        /// Issues an HTTP request for the current request context.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        public override void InvokeSync(IExecutionContext executionContext)
        {
            IHttpRequest<TRequestContent> httpRequest = null;
            try
            {
                SetMetrics(executionContext.RequestContext);
                IRequest wrappedRequest = executionContext.RequestContext.Request; 
                httpRequest = CreateWebRequest(executionContext.RequestContext);
                httpRequest.SetRequestHeaders(wrappedRequest.Headers);

                using (executionContext.RequestContext.Metrics.StartEvent(Metric.HttpRequestTime))
                {
                    // Send request body if present.
                    if (wrappedRequest.HasRequestBody())
                    {
                        try
                        {
                            var requestContent = httpRequest.GetRequestContent();
                            WriteContentToRequestBody(requestContent, httpRequest, executionContext.RequestContext);
                        }
                        catch
                        {
                            CompleteFailedRequest(httpRequest);
                            throw;
                        }
                    }

                    executionContext.ResponseContext.HttpResponse = httpRequest.GetResponse();
                }
            }
            finally
            {
                if (httpRequest != null)
                    httpRequest.Dispose();
            }
        }

        private static void CompleteFailedRequest(IHttpRequest<TRequestContent> httpRequest)
        {
            try
            {
                // In some cases where writing the request body fails, HttpWebRequest.Abort
                // may not dispose of the underlying Socket, so we need to retrieve and dispose
                // the web response to close the socket
                IWebResponseData response = null;
                try
                {
                    response = httpRequest.GetResponse();
                }
                catch { }
                finally
                {
                    if (response != null && response.ResponseBody != null)
                        response.ResponseBody.Dispose();
                }
            }
            catch { }
        }
		
#if AWS_ASYNC_API 

        /// <summary>
        /// Issues an HTTP request for the current request context.
        /// </summary>
        /// <typeparam name="T">The response type for the current request.</typeparam>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public override async System.Threading.Tasks.Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            IHttpRequest<TRequestContent> httpRequest = null;
            try
            {
                SetMetrics(executionContext.RequestContext);
                IRequest wrappedRequest = executionContext.RequestContext.Request;
                httpRequest = CreateWebRequest(executionContext.RequestContext);
                httpRequest.SetRequestHeaders(wrappedRequest.Headers);
                
                using(executionContext.RequestContext.Metrics.StartEvent(Metric.HttpRequestTime))
                {
                    // Send request body if present.
                    if (wrappedRequest.HasRequestBody())
                    {
                        System.Runtime.ExceptionServices.ExceptionDispatchInfo edi = null;
                        try
                        {
                            var requestContent = await httpRequest.GetRequestContentAsync().ConfigureAwait(false);
                            WriteContentToRequestBody(requestContent, httpRequest, executionContext.RequestContext);
                        }
                        catch(Exception e)
                        {
                            edi = System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(e);
                        }

                        if (edi != null)
                        {
                            await CompleteFailedRequest(executionContext, httpRequest);

                            edi.Throw();
                        }
                    }
                
                    var response = await httpRequest.GetResponseAsync(executionContext.RequestContext.CancellationToken).
                        ConfigureAwait(false);                
                    executionContext.ResponseContext.HttpResponse = response;     
                }
                // The response is not unmarshalled yet.
                return null;
            }            
            finally
            {
                if (httpRequest != null)
                    httpRequest.Dispose();
            }
        }

        private static async System.Threading.Tasks.Task CompleteFailedRequest(IExecutionContext executionContext, IHttpRequest<TRequestContent> httpRequest)
        {
            // In some cases where writing the request body fails, HttpWebRequest.Abort
            // may not dispose of the underlying Socket, so we need to retrieve and dispose
            // the web response to close the socket
            IWebResponseData iwrd = null;
            try
            {
                iwrd = await httpRequest.GetResponseAsync(executionContext.RequestContext.CancellationToken).ConfigureAwait(false);
            }
            catch { }
            finally
            {
                if (iwrd != null && iwrd.ResponseBody != null)
                    iwrd.ResponseBody.Dispose();
            }
        }

#elif AWS_APM_API

        /// <summary>
        /// Issues an HTTP request for the current request context.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        /// <returns>IAsyncResult which represent an async operation.</returns>
        public override IAsyncResult InvokeAsync(IAsyncExecutionContext executionContext)
        {
            IHttpRequest<TRequestContent> httpRequest = null;
            try
            {
                SetMetrics(executionContext.RequestContext);
                httpRequest = CreateWebRequest(executionContext.RequestContext);
                executionContext.RuntimeState = httpRequest;

                IRequest wrappedRequest = executionContext.RequestContext.Request;
                if (executionContext.RequestContext.Retries == 0)
                {
                    // First call, initialize an async result.
                    executionContext.ResponseContext.AsyncResult =
                        new RuntimeAsyncResult(executionContext.RequestContext.Callback, 
                            executionContext.RequestContext.State);

#if AWSSDK_UNITY
                    executionContext.ResponseContext.AsyncResult.AsyncOptions = executionContext.RequestContext.AsyncOptions;
                    executionContext.ResponseContext.AsyncResult.Action = executionContext.RequestContext.Action;
                    executionContext.ResponseContext.AsyncResult.Request = executionContext.RequestContext.OriginalRequest;
#endif 

                }

                // Set request headers
                httpRequest.SetRequestHeaders(executionContext.RequestContext.Request.Headers);

                executionContext.RequestContext.Metrics.StartEvent(Metric.HttpRequestTime);
                if (wrappedRequest.HasRequestBody())
                {
                    // Send request body if present.
                    httpRequest.BeginGetRequestContent(new AsyncCallback(GetRequestStreamCallback), executionContext);
                }
                else
                {
                    
                    // Get response if there is no response body to send.
                    httpRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), executionContext);
                }
                return executionContext.ResponseContext.AsyncResult;
            }
            catch (Exception exception)
            {
                if (executionContext.ResponseContext.AsyncResult != null)
                {
                    // An exception will be thrown back to the calling code.
                    // Dispose AsyncResult as it will not be used further.
                    executionContext.ResponseContext.AsyncResult.Dispose();
                    executionContext.ResponseContext.AsyncResult = null;
                }

                if (httpRequest != null)
                {                    
                    httpRequest.Dispose();
                }

                // Log this exception as it will not be caught by ErrorHandler.
                this.Logger.Error(exception, "An exception occured while initiating an asynchronous HTTP request.");
                throw;
            }
        }

        private void GetRequestStreamCallback(IAsyncResult result)
        {
            IAsyncExecutionContext executionContext = null;
            IHttpRequest<TRequestContent> httpRequest = null;
            try
            {
                executionContext = result.AsyncState as IAsyncExecutionContext;
                httpRequest = executionContext.RuntimeState as IHttpRequest<TRequestContent>;

                var requestContent = httpRequest.EndGetRequestContent(result);
                WriteContentToRequestBody(requestContent, httpRequest, executionContext.RequestContext);
                //var requestStream = httpRequest.EndSetRequestBody(result);                
                httpRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), executionContext);
            }
            catch(Exception exception)
            {   
                httpRequest.Dispose();

                // Capture the exception and invoke outer handlers to 
                // process the exception.
                executionContext.ResponseContext.AsyncResult.Exception = exception;
                base.InvokeAsyncCallback(executionContext);
            }
        }

        private void GetResponseCallback(IAsyncResult result)
        {
            IAsyncExecutionContext executionContext = null;
            IHttpRequest<TRequestContent> httpRequest = null;
            try
            {
                executionContext = result.AsyncState as IAsyncExecutionContext;
                httpRequest = executionContext.RuntimeState as IHttpRequest<TRequestContent>;

                var httpResponse = httpRequest.EndGetResponse(result);
                executionContext.ResponseContext.HttpResponse = httpResponse;
            }
            catch (Exception exception)
            {   
                // Capture the exception and invoke outer handlers to 
                // process the exception.
                executionContext.ResponseContext.AsyncResult.Exception = exception;
            }
            finally
            {
                executionContext.RequestContext.Metrics.StopEvent(Metric.HttpRequestTime);
                httpRequest.Dispose();
                base.InvokeAsyncCallback(executionContext);
            }
        }       

#endif

        private static void SetMetrics(IRequestContext requestContext)
        {
            requestContext.Metrics.AddProperty(Metric.ServiceName, requestContext.Request.ServiceName);
            requestContext.Metrics.AddProperty(Metric.ServiceEndpoint, requestContext.Request.Endpoint);
            requestContext.Metrics.AddProperty(Metric.MethodName, requestContext.Request.RequestName);
        }

        /// <summary>
        /// Determines the content for request body and uses the HTTP request
        /// to write the content to the HTTP request body.
        /// </summary>
        /// <param name="requestContent">Content to be written.</param>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="requestContext">The request context.</param>
        private void WriteContentToRequestBody(TRequestContent requestContent,
            IHttpRequest<TRequestContent> httpRequest,
            IRequestContext requestContext)
        {
            IRequest wrappedRequest = requestContext.Request;

            if (wrappedRequest.ContentStream == null)
            {
                byte[] requestData = wrappedRequest.Content;
                requestContext.Metrics.AddProperty(Metric.RequestSize, requestData.Length);
                httpRequest.WriteToRequestBody(requestContent, requestData, requestContext.Request.Headers);
            }
            else
            {
                var originalStream = wrappedRequest.ContentStream;
                var callback = ((Amazon.Runtime.Internal.IAmazonWebServiceRequest)wrappedRequest.OriginalRequest).StreamUploadProgressCallback;
                if (callback != null)
                {
                    var eventStream = new EventStream(originalStream, true);
                    var tracker = new StreamReadTracker(this.CallbackSender, callback, originalStream.Length,
                        requestContext.ClientConfig.ProgressUpdateInterval);
                    eventStream.OnRead += tracker.ReadProgress;
                    originalStream = eventStream;
                }

                var inputStream = wrappedRequest.UseChunkEncoding && wrappedRequest.AWS4SignerResult != null
                    ? new ChunkedUploadWrapperStream(originalStream,
                                                     requestContext.ClientConfig.BufferSize,
                                                     wrappedRequest.AWS4SignerResult)
                    : originalStream;

                httpRequest.WriteToRequestBody(requestContent, inputStream, 
                    requestContext.Request.Headers, requestContext);

            }
        }

        /// <summary>
        /// Creates the HttpWebRequest and configures the end point, content, user agent and proxy settings.
        /// </summary>
        /// <param name="requestContext">The async request.</param>
        /// <returns>The web request that actually makes the call.</returns>
        protected virtual IHttpRequest<TRequestContent> CreateWebRequest(IRequestContext requestContext)
        {
            IRequest request = requestContext.Request;
            Uri url = AmazonServiceClient.ComposeUrl(request);
            var httpRequest = _requestFactory.CreateHttpRequest(url);
            httpRequest.ConfigureRequest(requestContext);

            httpRequest.Method = request.HttpMethod;
            if (request.MayContainRequestBody())
            {
                var content = request.Content;
                if (request.SetContentFromParameters || (content == null && request.ContentStream == null))
                {
                    string queryString = AWSSDKUtils.GetParametersAsString(request.Parameters);
                    content = Encoding.UTF8.GetBytes(queryString);
                    request.Content = content;
                    request.SetContentFromParameters = true;
                }

                if (content != null)
                {
                    request.Headers[HeaderKeys.ContentLengthHeader] =
                        content.Length.ToString(CultureInfo.InvariantCulture);
                }
                else if (request.ContentStream != null && !request.Headers.ContainsKey(HeaderKeys.ContentLengthHeader))
                {
                    request.Headers[HeaderKeys.ContentLengthHeader] =
                        request.ContentStream.Length.ToString(CultureInfo.InvariantCulture);
                }
            }
            else if (request.UseQueryString &&
                (request.HttpMethod == "POST" ||
                 request.HttpMethod == "PUT"))
            {
                request.Content = new Byte[0];
            }

            if (requestContext.Unmarshaller is JsonResponseUnmarshaller)
            {
                // Currently the signature seems to be valid without including this header in the calculation.
                request.Headers["Accept"] = "application/json";
            }

            return httpRequest;
        }

        /// <summary>
        /// Disposes the HTTP handler.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_requestFactory != null)
                    _requestFactory.Dispose();

                _disposed = true;
            }
        }
    }
}
