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
using System.Threading;

namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// The default implementation of the retry policy.
    /// </summary>
    public class DefaultRetryPolicy : RetryPolicy
    {
        private int _maxBackoffInMilliseconds = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;

        // Set of web exception status codes to retry on.
        private ICollection<WebExceptionStatus> _webExceptionStatusesToRetryOn = new HashSet<WebExceptionStatus>
        {
            WebExceptionStatus.ConnectFailure,

#if (!WIN_RT) // These statuses are not available on WinRT
            WebExceptionStatus.ConnectionClosed,
            WebExceptionStatus.KeepAliveFailure,
            WebExceptionStatus.NameResolutionFailure,
            WebExceptionStatus.ReceiveFailure,
#else // WinRT does not expose the status as public enums so we hard code the status numbers.
            (WebExceptionStatus)8,
            (WebExceptionStatus)12,
            (WebExceptionStatus)1,
            (WebExceptionStatus)3,
#endif
        };

        // Set of AWS error codes to retry on.
        private ICollection<string> _errorCodesToRetryOn = new HashSet<string>
        {
            "Throttling",
            "ProvisionedThroughputExceededException",
            "RequestTimeout"
        };

        /// <summary>
        /// The maximum value of exponential backoff in milliseconds, which will be used to wait
        /// before retrying a request.
        /// </summary>
        public int MaxBackoffInMilliseconds
        {
            get { return _maxBackoffInMilliseconds; }
            set { _maxBackoffInMilliseconds = value; }
        }

        /// <summary>
        /// List of AWS specific error codes which are returned as part of the error response.
        /// These error codes will be retried.
        /// </summary>
        public ICollection<string> ErrorCodesToRetryOn
        {
            get { return _errorCodesToRetryOn; }
        }

        /// <summary>
        /// List of WebExceptionStatus for a WebException which will be retried.
        /// </summary>
        public ICollection<WebExceptionStatus> WebExceptionStatusesToRetryOn
        {
            get { return _webExceptionStatusesToRetryOn; }
        }

        /// <summary>
        /// Constructor for DefaultRetryPolicy.
        /// </summary>
        /// <param name="maxRetries">The maximum number of retries before throwing
        /// back a exception. This does not count the initial request.</param>
        public DefaultRetryPolicy(int maxRetries)
        {
            this.MaxRetries = maxRetries;
        }

        /// <summary>
        /// Returns true if the request is in a state where it can be retried, else false.
        /// </summary>
        /// <param name="executionContext">Request context containing the state of the request.</param>
        /// <returns>Returns true if the request is in a state where it can be retried, else false.</returns>
        public override bool CanRetry(IExecutionContext executionContext)
        {            
            return executionContext.RequestContext.Request.IsRequestStreamRewindable();
        }

        /// <summary>
        /// Return true if the request should be retried.
        /// </summary>
        /// <param name="executionContext">Request context containing the state of the request.</param>
        /// <param name="exception">The exception thrown by the previous request.</param>
        /// <returns>Return true if the request should be retried.</returns>
        public override bool RetryForException(IExecutionContext executionContext, Exception exception)
        {            
            // An IOException was thrown by the underlying http client.
            if (exception is IOException)
            {

#if (!WIN_RT)   // ThreadAbortException is not available on WIN RT

                // Don't retry IOExceptions that are caused by a ThreadAbortException
                if (IsInnerException<ThreadAbortException>(exception))
                    return false;

#endif
                // Retry all other IOExceptions
                return true;
            }
                        
            // A AmazonServiceException was thrown by ErrorHandler
            var serviceException = exception as AmazonServiceException;
            if (serviceException != null)
            {
                /*
                * For 500 internal server errors and 503 service
                * unavailable errors, we want to retry, but we need to use
                * an exponential back-off strategy so that we don't overload
                * a server with a flood of retries. If we've surpassed our
                * retry limit we handle the error response as a non-retryable
                * error and go ahead and throw it back to the user as an exception.
                */
                if (serviceException.StatusCode == HttpStatusCode.InternalServerError ||
                    serviceException.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    return true;
                }

                /*
                 * Throttling is reported as a 400 or 503 error from services. To try and
                 * smooth out an occasional throttling error, we'll pause and retry,
                 * hoping that the pause is long enough for the request to get through
                 * the next time.
                */
                if ((serviceException.StatusCode == HttpStatusCode.BadRequest ||
                    serviceException.StatusCode == HttpStatusCode.ServiceUnavailable))
                {
                    string errorCode = serviceException.ErrorCode;
                    if (this.ErrorCodesToRetryOn.Contains(errorCode))
                    {
                        return true;
                    }
                }

                WebException webException;
                if (IsInnerException<WebException>(exception, out webException))
                {
                    if (this.WebExceptionStatusesToRetryOn.Contains(webException.Status))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the retry limit is reached.
        /// </summary>
        /// <param name="executionContext">Request context containing the state of the request.</param>
        /// <returns>Return false if the request can be retried, based on number of retries.</returns>
        public override bool RetryLimitReached(IExecutionContext executionContext)
        {
            return executionContext.RequestContext.Retries >= this.MaxRetries;
        }

        /// <summary>
        /// Waits before retrying a request. The default policy implements a exponential backoff.
        /// </summary>
        /// <param name="executionContext">Request context containing the state of the request.</param>
        public override void WaitBeforeRetry(IExecutionContext executionContext)
        {            
            DefaultRetryPolicy.WaitBeforeRetry(executionContext.RequestContext.Retries, this.MaxBackoffInMilliseconds);
        }

        public static void WaitBeforeRetry(int retries, int maxBackoffInMilliseconds)
        {
            int delay = (int)(Math.Pow(4, retries) * 100);
            delay = Math.Min(delay, maxBackoffInMilliseconds);
            AWSSDKUtils.Sleep(delay);
        }

        protected static bool IsInnerException<T>(Exception exception)
            where T : Exception
        {
            T innerException;
            return IsInnerException<T>(exception, out innerException);
        }

        protected static bool IsInnerException<T>(Exception exception, out T inner)
            where T : Exception
        {
            inner = null;
            var currentException = exception;
            while (currentException.InnerException != null)
            {
                inner = currentException.InnerException as T;
                if (inner != null)
                {
                    return true;
                }
                currentException = currentException.InnerException;
            }
            return false;
        }
    }
}
