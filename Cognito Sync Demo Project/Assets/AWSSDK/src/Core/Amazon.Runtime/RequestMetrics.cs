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
using System.Linq;
using System.Text;

namespace Amazon.Runtime
{
    /// <summary>
    /// Metrics collected by the SDK on a per-request basis. 
    /// </summary>
    /// <remarks>
    /// Each request made to an AWS service by the SDK can have metrics
    /// collected and logged. This interface represents the collected 
    /// metrics for a request. The metrics include properties (i.e. request id 
    /// and other metadata), timings for each stage of the request, and counters.
    /// </remarks>
    public interface IRequestMetrics
    {
        /// <summary>
        /// Collection of properties being tracked
        /// </summary>
        Dictionary<Metric, List<object>> Properties { get; }

        /// <summary>
        /// Timings for metrics being tracked
        /// </summary>
        Dictionary<Metric, List<IMetricsTiming>> Timings { get; }

        /// <summary>
        /// Counters being tracked
        /// </summary>
        Dictionary<Metric, long> Counters { get; }

        /// <summary>
        /// Whether metrics are enabled for the request
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// JSON representation of the current metrics
        /// </summary>
        /// <returns>JSON string</returns>
        string ToJSON();
    }

    /// <summary>
    /// Represents how long a phase of an SDK request took.
    /// </summary>
    public interface IMetricsTiming
    {
        /// <summary>
        /// Whether the timing has been stopped
        /// </summary>
        bool IsFinished { get; }

        /// <summary>
        /// Elapsed ticks from start to stop.
        /// If timing hasn't been stopped yet, returns 0.
        /// </summary>
        long ElapsedTicks { get; }

        /// <summary>
        /// Elapsed time from start to stop.
        /// If timing hasn't been stopped yet, returns TimeSpan.Zero
        /// </summary>
        TimeSpan ElapsedTime { get; }
    }

    /// <summary>
    /// User supplied type to perform metrics formatting. 
    /// </summary>
    public interface IMetricsFormatter
    {
        /// <summary>
        /// Produce custom formatting for SDK metrics.
        /// </summary>
        /// <remarks>
        /// If defined, this method will be called for every request made by the SDK. 
        /// </remarks>
        /// <param name="metrics">An instance of IRequestMetrics produced by the SDK</param>
        /// <returns>formatted string representation of the metrics</returns>
        string FormatMetrics(IRequestMetrics metrics);
    }

    /// <summary>
    /// Predefined request metrics that are collected by the SDK.
    /// </summary>
    public enum Metric
    {
        // response enums
        AWSErrorCode,
        AWSRequestID,
        AmzId2,
        BytesProcessed,
        Exception,
        RedirectLocation,
        ResponseProcessingTime,
        ResponseUnmarshallTime,
        ResponseReadTime,
        StatusCode,

        // request enums
        AttemptCount,
        CredentialsRequestTime,
        HttpRequestTime,
        ProxyHost,
        ProxyPort,
        RequestSigningTime,
        RetryPauseTime,
        StringToSign,
        CanonicalRequest,

        // overall enums
        AsyncCall,
        ClientExecuteTime,
        MethodName,
        ServiceEndpoint,
        ServiceName,
        RequestSize,
    }
}
