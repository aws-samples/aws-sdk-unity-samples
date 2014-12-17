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
using System.Globalization;
using System.IO;
using System.Net;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;
using Amazon.Util;
using Amazon.Runtime.Internal.Util;
using Amazon.Unity;
using System.Collections;
using UnityEngine;
using Amazon.CognitoIdentity;
using System.Text;
using System.Xml;
using Amazon.Common;

namespace Amazon.Runtime
{
    /// <summary>
    /// A base class for service clients that handles making the actual requests
    /// and possibly retries if needed.
    /// </summary>
    public abstract partial class AmazonWebServiceClient : AbstractWebServiceClient
    {
        #region Constructors

        internal AmazonWebServiceClient(AWSCredentials credentials, ClientConfig config, AuthenticationTypes authenticationType)
            : base(credentials, config, authenticationType)
        {
        }

        internal AmazonWebServiceClient(string awsAccessKeyId, string awsSecretAccessKey, ClientConfig config, AuthenticationTypes authenticationType)
            : this((AWSCredentials)new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey),
                config, authenticationType)
        {
        }

        internal AmazonWebServiceClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, ClientConfig config, AuthenticationTypes authenticationType)
            : this(new SessionAWSCredentials(awsAccessKeyId, awsSecretAccessKey, awsSessionToken), config, authenticationType)
        {
        }

        internal AmazonWebServiceClient(string awsAccessKeyId, string awsSecretAccessKey, ClientConfig config)
            : this(new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey), config, AuthenticationTypes.User)
        {
        }

        internal AmazonWebServiceClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, ClientConfig config)
            : this(new SessionAWSCredentials(awsAccessKeyId, awsSecretAccessKey, awsSessionToken), config, AuthenticationTypes.User)
        {
        }

        #endregion


        protected void Invoke<T, R>(R request, AmazonServiceCallback callback, object state, IMarshaller<T, R> marshaller, ResponseUnmarshaller unmarshaller, AbstractAWSSigner signer)
            where T : IRequest
            where R : AmazonWebServiceRequest
        {
            AsyncResult result = null;
            try
            {

                ProcessPreRequestHandlers(request);

                IRequest irequest = marshaller.Marshall(request);
                result = new AsyncResult(irequest, request, callback, state, signer, unmarshaller);
                Invoke(result);
            }
            catch (Exception e)
            {
                AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Errors, "Runtime", e.Message);
                result.HandleException(e);
                result.IsCompleted = true;
            }
            //return result;
        }

		[Obsolete]
        protected void Invoke<T, R, A>(R request, Action<A> callback, IMarshaller<T, R> marshaller, ResponseUnmarshaller unmarshaller, AbstractAWSSigner signer)
            where T : IRequest
            where R : AmazonWebServiceRequest
            where A : class
        {
            /*
            AsyncResult result = null;
            try
            {

                ProcessPreRequestHandlers(request);

                IRequest irequest = marshaller.Marshall(request);
                result = new AsyncResult(irequest, signer, unmarshaller);
                result.VoidCallback = delegate
                {
                    if (callback != null)
                        callback(result.FinalResponse as A);
                };
                result.FinalResponseType = typeof(A);
                Invoke(result);
            }
            catch (Exception e)
            {
                AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Errors, "Runtime", e.Message);
                if (result.Exception == null)
                {
                    result.Exception = e;
                }
                result.IsCompleted = true;

                endAsync(result);
            }
            */
        }

        private void Invoke(AsyncResult asyncResult)
        {
            asyncResult.Metrics.StartEvent(Metric.ClientExecuteTime);
            asyncResult.Request.Endpoint = DetermineEndpoint(asyncResult.Request);
            if (Config.LogMetrics)
            {
                asyncResult.Metrics.IsEnabled = true;
                asyncResult.Metrics.AddProperty(Metric.ServiceName, asyncResult.Request.ServiceName);
                asyncResult.Metrics.AddProperty(Metric.ServiceEndpoint, asyncResult.Request.Endpoint);
                asyncResult.Metrics.AddProperty(Metric.MethodName, asyncResult.RequestName);
                asyncResult.Metrics.AddProperty(Metric.AsyncCall, !asyncResult.CompletedSynchronously);
            }
            ConfigureRequest(asyncResult);
            InvokeHelper(asyncResult);
        }

        protected Uri DetermineEndpoint(IRequest request)
        {
            return request.AlternateEndpoint != null
                ? new Uri(ClientConfig.GetUrl(request.AlternateEndpoint, Config.RegionEndpointServiceName, Config.UseHttp))
                : new Uri(this.Config.DetermineServiceURL());
        }

        private void InvokeHelper(AsyncResult asyncResult)
        {
            if (asyncResult.RetriesAttempt == 0 || Config.ResignRetries)
            {
                if (Credentials is CognitoAWSCredentials)
                {
                    var cred = Credentials as CognitoAWSCredentials;
                    // very hacky solution
                    cred.GetCredentialsAsync(delegate(AmazonServiceResult voidResult)
                    {
                        if (voidResult.Exception != null)
                        {

                            asyncResult.IsCompleted = true;
                            AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Errors, "CognitoAWSCredentials", voidResult.Exception.Message);
                            asyncResult.HandleException(voidResult.Exception);
                            return;
                        }
                        ProcessHttpRequest(asyncResult);
                    }, null);
                    return;
                }

            }
            ProcessHttpRequest(asyncResult);
            return;

        }


        #region UnityHTTP
        static byte[] GetBytes(string str)
        {

            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Perform signing, setup WWWRequestData with headers, binary data(for POST request)
        /// and enqueues to the RequestQueue for Main thread
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ProcessHttpRequest(AsyncResult asyncResult)
        {
            try
            {
                
                // prepare request
                IRequest wrappedRequest = asyncResult.Request;
                
                WWWRequestData requestData = new WWWRequestData();
                if (HttpOverrideSupportedServices.Contains(asyncResult.Request.ServiceName))
                {
                    asyncResult.Request.Headers.Add("X-HTTP-Method-Override", asyncResult.Request.HttpMethod);

                    if (asyncResult.Request.HttpMethod == "GET")
                    {
                        string emptyString = "{}";
                        asyncResult.Request.ContentStream = new MemoryStream(Encoding.UTF8.GetBytes(emptyString));
                        requestData.Data = Encoding.Default.GetBytes(emptyString);
                    }
                }

#if UNITY_WEBPLAYER
                wrappedRequest.Headers.Remove("User-Agent");
                wrappedRequest.Headers.Remove("Host");
#endif

                SignRequest(asyncResult);

                if (asyncResult.RetriesAttempt > 0)
                    HandleRetry(asyncResult);


                requestData.Url = ComposeUrl(wrappedRequest, wrappedRequest.Endpoint).ToString();

                if (!wrappedRequest.UseQueryString && !(wrappedRequest.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase)))
                {
                    if (wrappedRequest.ContentStream != null)
                    {
                        if (wrappedRequest.OriginalRequest.IncludeSHA256Header
                            && !wrappedRequest.Headers.ContainsKey(HeaderKeys.XAmzContentSha256Header))
                        {
                            requestData.Headers.Add(HeaderKeys.XAmzContentSha256Header, wrappedRequest.ComputeContentStreamHash());
                        }
                    }
                }

                AddHeaders2(requestData, wrappedRequest.Headers);

                if (asyncResult.Unmarshaller is JsonResponseUnmarshaller)
                {
                    requestData.Headers.Add(HeaderKeys.AcceptHeader, "application/json");
                }

                if (!asyncResult.Request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
                {
                    if (asyncResult.Request.Content != null && asyncResult.Request.Content.Length > 0)
                    {
                        requestData.Data = asyncResult.Request.Content;
                    }
                    else if (asyncResult.Request.ContentStream != null && asyncResult.Request.ContentStream.Length > 0)
                    {
                        using (asyncResult.Request.ContentStream)
                        {
                            requestData.Data = StreamToByteArray.Convert(asyncResult.Request.ContentStream, this.Config.BufferSize);
                            
                            StreamTransferProgressArgs args = new StreamTransferProgressArgs(asyncResult.Request.ContentStream.Length, asyncResult.Request.ContentStream.Length, asyncResult.Request.ContentStream.Length);
                            if (asyncResult.Request.OriginalRequest.StreamUploadProgressCallback != null)
                            {
                                asyncResult.Request.OriginalRequest.StreamUploadProgressCallback(this, args);
                            }
                            
                            if (args.PercentDone >= 100)
                            {
                                asyncResult.Request.OriginalRequest.StreamUploadProgressCallback = null;
                            }
                        }
                    }
                    else if (asyncResult.Request.Parameters != null && asyncResult.Request.Parameters.Count > 0)
                    {
                        requestData.Data = GetRequestData(asyncResult.Request);
                    }
                }

                asyncResult.RequestData = requestData;

                // setting up callback for response handling 
                asyncResult.WaitCallback = this.ProcessHttpResponse;

                // switching to main thread to make the network call
                AmazonMainThreadDispatcher.QueueAWSRequest(asyncResult);

            }
            catch (Exception e)
            {
                AmazonLogging.LogException(AmazonLogging.AmazonLoggingLevel.Errors, asyncResult.Request.ServiceName, e);

                asyncResult.IsCompleted = true;
                asyncResult.HandleException(e);
                return;
            }
        }

        /// <summary>
        /// Invoked as a callback from the AmazonMainThreadDispatcher 
        /// Processes the http response
        /// </summary>
        /// <param name="state">State is expected to be AsyncResult</param>
        private void ProcessHttpResponse(object state)
        {
            AsyncResult asyncResult = null;

            try
            {
                asyncResult = state as AsyncResult;
                AmazonWebServiceResponse response = null;
                UnmarshallerContext context = null;

                var responseData = asyncResult.ResponseData;

                if (!String.IsNullOrEmpty(responseData.Error))
                {
                    AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Critical,
                                           asyncResult.Request.ServiceName, responseData.Error);

                    if (HandleWWWErrorResponse(asyncResult))
                    {
                        if (++asyncResult.RetriesAttempt >= 3)
                        {
                            if (asyncResult.Exception == null)
                            {
                                asyncResult.Exception = new AmazonServiceException("Maximum retries attempts completed");
                            }
                            // maxretries
                            asyncResult.IsCompleted = true;
                            AmazonLogging.LogException(AmazonLogging.AmazonLoggingLevel.Errors,
                                                   asyncResult.Request.ServiceName, asyncResult.Exception);
                            asyncResult.HandleException(asyncResult.Exception);
                            return;
                        }
                        else
                        {
                            // retry here
                            InvokeHelper(asyncResult);
                            return;
                        }
                    }
                    else
                    {
                        // non-retriable error
                        asyncResult.IsCompleted = true;
                        asyncResult.HandleException(asyncResult.Exception);
                        return;
                    }
                }
                else
                {
                    using (asyncResult.Metrics.StartEvent(Metric.ResponseProcessingTime))
                    {
                        var unmarshaller = asyncResult.Unmarshaller;
                        LogResponse(asyncResult.Metrics, asyncResult.Request, HttpStatusCode.Accepted);

                        context = unmarshaller.CreateContext(responseData,
                                                             this.SupportResponseLogging &&
                                                             (Config.LogResponse || Config.ReadEntireResponse),
                                                             responseData.OpenResponse(),
                                                             asyncResult.Metrics);
                        try
                        {
                            using (asyncResult.Metrics.StartEvent(Metric.ResponseUnmarshallTime))
                            {
                                response = unmarshaller.Unmarshall(context);
                                if (responseData.IsHeaderPresent("STATUS"))
                                    response.HttpStatusCode = responseData.StatusCode;
                            }
                        }
                        catch (Exception e)
                        {
                            //unmarshalling exception
                            asyncResult.IsCompleted = true;
                            asyncResult.HandleException(e);
                            return;
                        }
                        context.ValidateCRC32IfAvailable();
                        if (responseData.IsHeaderPresent(HeaderKeys.ContentLengthHeader.ToUpper()))
                        {
                            response.ContentLength = Convert.ToInt32(responseData.GetHeaderValue(HeaderKeys.ContentLengthHeader.ToUpper()));
                        }
                        else
                        {
                            AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Warnings, asyncResult.Request.ServiceName, "cannot find CONTENT-LENGTH header");
                        }

                        asyncResult.ServiceResult.Response = response;

                        if (response.ResponseMetadata != null)
                        {
                            asyncResult.Metrics.AddProperty(Metric.AWSRequestID, response.ResponseMetadata.RequestId);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                AmazonLogging.LogException(AmazonLogging.AmazonLoggingLevel.Errors, asyncResult.Request.ServiceName, e);
                asyncResult.HandleException(e);
            }

            asyncResult.IsCompleted = true;
            asyncResult.InvokeCallback();
            return;
        }


        #endregion


        private bool HandleIOException(AsyncResult asyncResult, HttpWebResponse httpResponse, IOException e)
        {
            asyncResult.Metrics.AddProperty(Metric.Exception, e);

            if (IsInnerExceptionThreadAbort(e))
                throw e;

            this.logger.Error(e, "IOException making request {0} to {1}.", asyncResult.RequestName, asyncResult.Request.Endpoint.ToString());
            if (httpResponse != null)
            {
                //httpResponse.Close();
                httpResponse = null;
            }
            // Abort the unsuccessful request
            //asyncResult.RequestState.WebRequest.Abort();

            if (CanRetry(asyncResult) && asyncResult.RetriesAttempt < Config.MaxErrorRetry)
            {
                this.logger.Error(e, "IOException making request {0} to {1}. Attempting retry {2}.",
                        asyncResult.RequestName,
                        asyncResult.Request.Endpoint.ToString(),
                        asyncResult.RetriesAttempt);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The HandleWWWErrorResponse differs significantly from the error handling doing in .NET sdk
        /// since the www.error string message is incomplete 
        /// so this requires rewriting all responseunmarshallers.HandleErrorContext which is not part of this version
        /// hence exception thrown will always be of base type AmazonServiceException
        /// </summary>
        /// <returns>True if the error needs retry</returns>
        private bool HandleWWWErrorResponse(AsyncResult asyncResult)
        {
            WWWResponseData errorResponse = asyncResult.ResponseData;
            asyncResult.Metrics.AddProperty(Metric.Exception, errorResponse.Error);

            AmazonServiceException errorResponseException = null;
            errorResponseException = new AmazonServiceException(errorResponse.Error,
                                                                 new WebException(errorResponse.Error));
            
            errorResponseException.UnityStatusCode = errorResponse.Error;
            try
            {
                errorResponseException.StatusCode = errorResponse.ErrorStatusCode;
            }
            catch (Exception e)
            {
                // Parsing exception
                AmazonLogging.LogException(AmazonLogging.AmazonLoggingLevel.Errors, asyncResult.Request.RequestName, e);
            }
            
            string curl = "curl " + (asyncResult.Request.HttpMethod == "GET" && 
                                     !HttpOverrideSupportedServices.Contains(asyncResult.Request.ServiceName) ? 
                                     "-G " :  "-X POST ");
            foreach (string key in asyncResult.RequestData.Headers.Keys)
            {
                curl += " -H \"" + key + ": " + asyncResult.RequestData.Headers[key] + "\" ";
            }
            if (asyncResult.RequestData.Data != null)
                curl += " -d '" + System.Text.Encoding.Default.GetString(asyncResult.RequestData.Data) + "' ";

            curl += " " + asyncResult.RequestData.Url;
            Debug.LogError(curl);

            if (errorResponse.IsHeaderPresent(HeaderKeys.XAmzRequestIdHeader.ToUpper()))
                errorResponseException.RequestId = errorResponse.GetHeaderValue(HeaderKeys.XAmzRequestIdHeader);

            asyncResult.Exception = errorResponseException;

            // currently no retries are done
            return false;
        }

        internal static void AddHeaders2(WWWRequestData requestData, IDictionary<string, string> headersToAdd)
        {
            var headers = requestData.Headers;
            foreach (var kvp in headersToAdd)
            {
                if (WebHeaderCollection.IsRestricted(kvp.Key) || string.Equals(kvp.Key, "Range", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(kvp.Key, "Accept", StringComparison.OrdinalIgnoreCase))
                        headers.Add("Accept", kvp.Value);
                    else if (string.Equals(kvp.Key, "Connection", StringComparison.OrdinalIgnoreCase))
                        headers.Add("Connection", kvp.Value);
                    else if (string.Equals(kvp.Key, "Content-Type", StringComparison.OrdinalIgnoreCase))
                        headers.Add("Content-Type", kvp.Value);
                    else if (string.Equals(kvp.Key, "Content-Length", StringComparison.OrdinalIgnoreCase))
                        headers.Add("Content-Length", kvp.Value);
                    else if (string.Equals(kvp.Key, "Expect", StringComparison.OrdinalIgnoreCase))
                        headers.Add("Expect", kvp.Value);
                    else if (string.Equals(kvp.Key, "Date", StringComparison.OrdinalIgnoreCase))
                        headers.Add("Date", kvp.Value);
                    else if (string.Equals(kvp.Key, "User-Agent", StringComparison.OrdinalIgnoreCase))
                    {
                        #if !UNITY_WEBPLAYER
                            headers.Add("User-Agent", kvp.Value);
                        #endif  
                    }   
                    else if (string.Equals(kvp.Key, "Host", StringComparison.OrdinalIgnoreCase))
                    {
                        #if !UNITY_WEBPLAYER
                        headers.Add("Host", kvp.Value);                      
                        #endif  
                    }  
                    else if (string.Equals(kvp.Key, "Range", StringComparison.OrdinalIgnoreCase))
                        headers.Add("Range", kvp.Value);
                    else if (string.Equals(kvp.Key, "If-Modified-Since", StringComparison.OrdinalIgnoreCase))
                        headers.Add("If-Modified-Since", kvp.Value);
                    else
                        throw new NotSupportedException("Header with name " + kvp.Key + " is not supported");
                }
                else
                {
                    headers.Add(kvp.Key, kvp.Value);
                }
            }
        }

        protected static void LogResponse(RequestMetrics metrics, IRequest request, HttpStatusCode statusCode)
        {
            metrics.AddProperty(Metric.StatusCode, statusCode);
        }
    }
}
