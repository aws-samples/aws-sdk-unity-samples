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
using System.Globalization;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using Amazon.Util;

namespace Amazon.Runtime
{
    /// <summary>
    /// A retry policy specifies all aspects of retry behavior. This includes conditions when the request should be retried,
    /// checks of retry limit, preparing the request before retry and introducing delay (backoff) before retries.
    /// </summary>
    public abstract class RetryPolicy
    {
        /// <summary>
        /// Maximum number of retries to be performed.
        /// This does not count the initial request.
        /// </summary>
        public int MaxRetries { get; protected set; }

        /// <summary>
        /// The logger used to log messages.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Checks if a retry should be performed with the given execution context and exception.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        /// <param name="exception">The exception throw after issuing the request.</param>
        /// <returns>Returns true if the request should be retried, else false.</returns>
        public bool Retry(IExecutionContext executionContext, Exception exception)
        {
            return
                !RetryLimitReached(executionContext) &&
                CanRetry(executionContext) &&
                RetryForExceptionHelper(executionContext, exception);
        }

        /// <summary>
        /// Returns true if the request is in a state where it can be retried, else false.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        /// <returns>Returns true if the request is in a state where it can be retried, else false.</returns>
        public abstract bool CanRetry(IExecutionContext executionContext);

        /// <summary>
        /// Return true if the request should be retried for the given exception.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        /// <param name="exception">The exception thrown by the previous request.</param>
        /// <returns>Return true if the request should be retried.</returns>
        public abstract bool RetryForException(IExecutionContext executionContext, Exception exception);

        /// <summary>
        /// Checks if the retry limit is reached.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        /// <returns>Return false if the request can be retried, based on number of retries.</returns>
        public abstract bool RetryLimitReached(IExecutionContext executionContext);

        /// <summary>
        /// Waits before retrying a request.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        public abstract void WaitBeforeRetry(IExecutionContext executionContext);


        private bool RetryForExceptionHelper(IExecutionContext executionContext, Exception exception)
        {
            if (IsClockskew(executionContext, exception))
                return true;
            return RetryForException(executionContext, exception);
        }

        #region Clock skew correction

        private static HashSet<string> clockSkewErrorCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "RequestTimeTooSkewed",
            "RequestExpired",
            "InvalidSignatureException",
            "SignatureDoesNotMatch",
            "AuthFailure",
            "RequestExpired",
            "RequestInTheFuture",
        };
        private const string dateHeaderName = "Date";
        private const string clockSkewMessageFormat = "Identified clock skew: local time = {0}, local time with correction = {1}, current clock skew correction = {2}, server time = {3}.";
        private const string clockSkewUpdatedFormat = "Setting clock skew correction: new clock skew correction = {0}.";
        private const string clockSkewMessageParen = "(";
        private const string clockSkewMessagePlusSeparator = " + ";
        private const string clockSkewMessageMinusSeparator = " - ";
        private static TimeSpan clockSkewMaxThreshold = TimeSpan.FromMinutes(5);

        private bool IsClockskew(IExecutionContext executionContext, Exception exception)
        {
            var ase = exception as AmazonServiceException;

            var isHead =
                executionContext.RequestContext.Request != null &&
                string.Equals(executionContext.RequestContext.Request.HttpMethod, "HEAD", StringComparison.Ordinal);
            var isClockskewErrorCode =
                ase != null &&
                (ase.ErrorCode == null || clockSkewErrorCodes.Contains(ase.ErrorCode));

            if (isHead || isClockskewErrorCode)
            {
                var realNow = DateTime.UtcNow;
                var correctedNow = AWSSDKUtils.CorrectedUtcNow;

                DateTime serverTime;

                // Try getting server time from the headers
                bool serverTimeDetermined = TryParseDateHeader(ase, out serverTime);

                // If that fails, try to parse it from the exception message
                if (!serverTimeDetermined)
                    serverTimeDetermined = TryParseExceptionMessage(ase, out serverTime);

                if (serverTimeDetermined)
                {
                    // using accurate server time, calculate correction if local time is off
                    serverTime = serverTime.ToUniversalTime();
                    var diff = correctedNow - serverTime;
                    var absDiff = diff.Ticks < 0 ? -diff : diff;
                    if (absDiff > clockSkewMaxThreshold)
                    {
                        var newCorrection = serverTime - realNow;
                        Logger.InfoFormat(clockSkewMessageFormat,
                            realNow, correctedNow, AWSConfigs.ClockOffset, serverTime);

                        // Always set the correction, for informational purposes
                        AWSConfigs.ClockOffset = newCorrection;
                        var shouldRetry = AWSConfigs.CorrectForClockSkew;

                        // Only retry if clock skew correction is not disabled
                        if (shouldRetry)
                        {
                            // Set clock skew correction
                            Logger.InfoFormat(clockSkewUpdatedFormat, newCorrection);
                            executionContext.RequestContext.IsSigned = false;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        private static bool TryParseDateHeader(AmazonServiceException ase, out DateTime serverTime)
        {
            var webData = GetWebData(ase);

            if (webData != null)
            {
                // parse server time from "Date" header, if possible
                var dateValue = webData.GetHeaderValue(dateHeaderName);
                if (!string.IsNullOrEmpty(dateValue))
                {
                    if (DateTime.TryParseExact(
                            dateValue,
                            AWSSDKUtils.GMTDateFormat,
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.AssumeUniversal,
                            out serverTime))
                    {
                        return true;
                    }
                }
            }

            serverTime = DateTime.MinValue;
            return false;
        }
        private static bool TryParseExceptionMessage(AmazonServiceException ase, out DateTime serverTime)
        {
            var message = ase.Message;
            if (!string.IsNullOrEmpty(message))
            {
                // parse server time from exception message, if possible
                var parenIndex = message.IndexOf(clockSkewMessageParen, StringComparison.Ordinal);
                if (parenIndex >= 0)
                {
                    parenIndex++;

                    // Locate " + " or " - " separator that follows the server time string
                    var separatorIndex = message.IndexOf(clockSkewMessagePlusSeparator, parenIndex, StringComparison.Ordinal);
                    if (separatorIndex < 0)
                        separatorIndex = message.IndexOf(clockSkewMessageMinusSeparator, parenIndex, StringComparison.Ordinal);

                    // Get the server time string and parse it
                    if (separatorIndex > parenIndex)
                    {
                        var timestamp = message.Substring(parenIndex, separatorIndex - parenIndex);
                        if (DateTime.TryParseExact(
                                timestamp,
                                AWSSDKUtils.ISO8601BasicDateTimeFormat,
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.AssumeUniversal,
                                out serverTime))
                        {
                            return true;
                        }
                    }
                }
            }

            serverTime = DateTime.MinValue;
            return false;
        }
        private static IWebResponseData GetWebData(AmazonServiceException ase)
        {
            if (ase != null)
            {
                Exception e = ase;
                do
                {
                    var here = e as HttpErrorResponseException;
                    if (here != null)
                        return here.Response;
                    e = e.InnerException;
                } while (e != null);
            }

            return null;
        }

        #endregion
    }
}
