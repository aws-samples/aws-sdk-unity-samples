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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;


namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// The exception handler for UnityHttpErrorResponseException.
    /// </summary>
    public class UnityHttpErrorResponseExceptionHandler<T> : ExceptionHandler<UnityHttpErrorResponseException>
        where T : AmazonServiceException, new()
    {

        /// <summary>
        /// The constructor for UnityHttpErrorResponseExceptionHandler.
        /// </summary>
        /// <param name="logger">Instance of ILogger.</param>
        public UnityHttpErrorResponseExceptionHandler(ILogger logger) :
            base(logger)
        {
        }


        /// <summary>
        /// Handles an exception for the given execution context.
        /// </summary>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <returns>
        /// Returns a boolean value which indicates if the original exception
        /// should be rethrown.
        /// This method can also throw a new exception to replace the original exception.
        /// </returns>
        public override bool HandleException(IExecutionContext executionContext, UnityHttpErrorResponseException exception)
        {
            var requestContext = executionContext.RequestContext;
            var httpErrorResponse = exception.Response;

            var unityResponseData = (UnityWebResponseData)httpErrorResponse.ResponseBody;

            if (!unityResponseData.IsResponseBodyPresent)
            {
                //for backward compatibility when the response error message is missing
                LogCurlRequest(exception.Request);
                var errorResponse = new ErrorResponse();
                if (httpErrorResponse.IsHeaderPresent(HeaderKeys.XAmzRequestIdHeader))
                {
                    errorResponse.RequestId = httpErrorResponse.GetHeaderValue(HeaderKeys.XAmzRequestIdHeader);
                    requestContext.Metrics.AddProperty(Metric.AWSRequestID, httpErrorResponse.GetHeaderValue(HeaderKeys.XAmzRequestIdHeader));
                }

                if (httpErrorResponse.IsHeaderPresent(HeaderKeys.XAmzErrorTypeHeader))
                {
                    errorResponse.Code = httpErrorResponse.GetHeaderValue(HeaderKeys.XAmzErrorTypeHeader);
                }

                if (httpErrorResponse.IsHeaderPresent(HeaderKeys.XAmzErrorMessageHeader))
                {
                    errorResponse.Message = httpErrorResponse.GetHeaderValue(HeaderKeys.XAmzErrorMessageHeader);
                }

                if (httpErrorResponse.IsHeaderPresent(HeaderKeys.XAmzId2Header))
                {
                    requestContext.Metrics.AddProperty(Metric.AmzId2, httpErrorResponse.GetHeaderValue(HeaderKeys.XAmzId2Header));
                }

                requestContext.Metrics.AddProperty(Metric.StatusCode, httpErrorResponse.StatusCode);

                Exception unmarshalledException = null;
                var unmarshaller = requestContext.Unmarshaller as ISimplifiedErrorUnmarshaller;
                if (unmarshaller != null)
                {
                    unmarshalledException = unmarshaller.UnmarshallException(httpErrorResponse, errorResponse, exception);
                    LogErrorMessage(unmarshalledException, errorResponse);
                    throw unmarshalledException;
                }
                else
                {
                    var baseServiceException = new AmazonServiceException();
                    baseServiceException.RequestId = errorResponse.RequestId;
                    baseServiceException.ErrorCode = errorResponse.Code;
                    baseServiceException.StatusCode = httpErrorResponse.StatusCode;
                    throw baseServiceException;
                }
            }
            else
            {
                // If 404 was suppressed and successfully unmarshalled,
                // don't rethrow the original exception.
                if (HandleSuppressed404(executionContext, httpErrorResponse))
                    return false;

                requestContext.Metrics.AddProperty(Metric.StatusCode, httpErrorResponse.StatusCode);

                AmazonServiceException errorResponseException = null;
                // Unmarshall the service error response and throw the corresponding service exception.
                try
                {
                    using (httpErrorResponse.ResponseBody)
                    {
                        var unmarshaller = requestContext.Unmarshaller;
                        var readEntireResponse = true;

                        var errorContext = unmarshaller.CreateContext(httpErrorResponse,
                            readEntireResponse,
                            httpErrorResponse.ResponseBody.OpenResponse(),
                            requestContext.Metrics);

                        try
                        {
                            errorResponseException = unmarshaller.UnmarshallException(errorContext,
                                exception, httpErrorResponse.StatusCode);
                        }
                        catch (Exception e)
                        {
                            // Rethrow Amazon service or client exceptions 
                            if (e is AmazonServiceException ||
                                e is AmazonClientException)
                            {
                                throw;
                            }

                            // Else, there was an issue with the response body, throw AmazonUnmarshallingException
                            var requestId = httpErrorResponse.GetHeaderValue(HeaderKeys.RequestIdHeader);
                            var body = errorContext.ResponseBody;
                            throw new AmazonUnmarshallingException(requestId, lastKnownLocation: null, responseBody: body, innerException: e);
                        }

                        Debug.Assert(errorResponseException != null);

                        requestContext.Metrics.AddProperty(Metric.AWSRequestID, errorResponseException.RequestId);
                        requestContext.Metrics.AddProperty(Metric.AWSErrorCode, errorResponseException.ErrorCode);

                        var logResponseBody = requestContext.ClientConfig.LogResponse ||
                            AWSConfigs.LoggingConfig.LogResponses != ResponseLoggingOption.Never;
                        if (logResponseBody)
                        {
                            this.Logger.Error(errorResponseException, "Received error response: [{0}]",
                                errorContext.ResponseBody);
                        }
                    }
                }
                catch (Exception unmarshallException)
                {
                    this.Logger.Error(unmarshallException, "Failed to unmarshall a service error response.");
                    throw;
                }

                throw errorResponseException;
            }
        }

        private void LogErrorMessage(Exception exception, ErrorResponse errorResponse)
        {
            if (!string.IsNullOrEmpty(errorResponse.Code))
            {
                if (!string.IsNullOrEmpty(errorResponse.Message))
                {
                    this.Logger.Error(exception, "Recieved Exception of type {0}, Message {1}, request id {2}", errorResponse.Code, errorResponse.Message, errorResponse.RequestId);
                }
                else
                {
                    this.Logger.Error(exception, "Recieved Exception of type {0}, request id {1}", errorResponse.Code, errorResponse.RequestId);
                }
            }
        }

        private void LogCurlRequest(UnityWebRequest request)
        {
            string curl = "curl " + (request.Method == "GET" ? "-G " : "-X POST ");
            foreach (string key in request.Headers.Keys)
            {
                curl += " -H \"" + key + ": " + request.Headers[key] + "\" ";
            }
            if (request.RequestContent != null)
                curl += " -d '" + System.Text.Encoding.Default.GetString(request.RequestContent) + "' ";

            curl += " " + request.RequestUri;
            this.Logger.DebugFormat("{0}", curl);
        }

        /// <summary>
        /// Checks if a HTTP 404 status code is returned which needs to be suppressed and 
        /// processes it.
        /// If a suppressed 404 is present, it unmarshalls the response and returns true to 
        /// indicate that a suppressed 404 was processed, else returns false.
        /// </summary>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        /// <param name="httpErrorResponse"></param>
        /// <returns>
        /// If a suppressed 404 is present, returns true, else returns false.
        /// </returns>
        private bool HandleSuppressed404(IExecutionContext executionContext, IWebResponseData httpErrorResponse)
        {
            var requestContext = executionContext.RequestContext;
            var responseContext = executionContext.ResponseContext;

            // If the error is a 404 and the request is configured to supress it,
            // then unmarshall as much as we can.
            if (httpErrorResponse != null &&
                httpErrorResponse.StatusCode == HttpStatusCode.NotFound &&
                requestContext.Request.Suppress404Exceptions)
            {
                using (httpErrorResponse.ResponseBody)
                {
                    var unmarshaller = requestContext.Unmarshaller;
                    var readEntireResponse = requestContext.ClientConfig.LogResponse ||
                            requestContext.ClientConfig.ReadEntireResponse ||
                            AWSConfigs.LoggingConfig.LogResponses != ResponseLoggingOption.Never;

                    UnmarshallerContext errorContext = unmarshaller.CreateContext(
                        httpErrorResponse,
                        readEntireResponse,
                        httpErrorResponse.ResponseBody.OpenResponse(),
                        requestContext.Metrics);
                    try
                    {
                        responseContext.Response = unmarshaller.Unmarshall(errorContext);
                        responseContext.Response.ContentLength = httpErrorResponse.ContentLength;
                        responseContext.Response.HttpStatusCode = httpErrorResponse.StatusCode;
                        return true;
                    }
                    catch (Exception unmarshallException)
                    {
                        this.Logger.Debug(unmarshallException, "Failed to unmarshall 404 response when it was supressed.");
                    }
                }
            }
            return false;
        }

    }
}
