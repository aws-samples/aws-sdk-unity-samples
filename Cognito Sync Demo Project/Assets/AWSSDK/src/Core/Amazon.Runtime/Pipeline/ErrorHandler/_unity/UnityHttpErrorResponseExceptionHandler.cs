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
using System.Linq;
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
            LogCurlRequest(exception.Request);

            var requestContext = executionContext.RequestContext;
            var httpErrorResponse = exception.Response;

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
                var baseServiceException = new T();
                baseServiceException.RequestId = errorResponse.RequestId;
                baseServiceException.ErrorCode = errorResponse.Code;
                baseServiceException.StatusCode = httpErrorResponse.StatusCode;
                throw baseServiceException;
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

    }
}
