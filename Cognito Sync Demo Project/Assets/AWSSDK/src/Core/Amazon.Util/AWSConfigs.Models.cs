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
using System.Xml.Linq;

namespace Amazon.Util
{

    #region Basic sections

    /// <summary>
    /// Settings for configuring a proxy for the SDK to use.
    /// </summary>
    public partial class ProxyConfig
    {
        /// <summary>
        /// The host name or IP address of the proxy server.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The port number of the proxy.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// The username to authenticate with the proxy server.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password to authenticate with the proxy server.
        /// </summary>
        public string Password { get; set; }


        internal ProxyConfig()
        {
        }
    }

    /// <summary>
    /// Settings for logging in the SDK.
    /// </summary>
    public partial class LoggingConfig
    {
        private LoggingOptions _logTo;

        /// <summary>
        /// Logging destination.
        /// </summary>
        public LoggingOptions LogTo
        {
            get { return _logTo; }
            set
            {
                _logTo = value;
                AWSConfigs.OnPropertyChanged(AWSConfigs.LoggingDestinationProperty);
            }
        }
        /// <summary>
        /// When to log responses.
        /// </summary>
        public ResponseLoggingOption LogResponses { get; set; }
        /// <summary>
        /// Whether or not to log SDK metrics.
        /// </summary>
        public bool LogMetrics { get; set; }

        public LogMetricsFormatOption LogMetricsFormat { get; set; }

        /// <summary>
        /// A custom formatter added through Configuration
        /// </summary>
        public Amazon.Runtime.IMetricsFormatter LogMetricsCustomFormatter { get; set; }

        internal LoggingConfig()
        {
            LogTo = AWSConfigs._logging;
            LogResponses = AWSConfigs._responseLogging;
            LogMetrics = AWSConfigs._logMetrics;
        }
    }

    #endregion

}
