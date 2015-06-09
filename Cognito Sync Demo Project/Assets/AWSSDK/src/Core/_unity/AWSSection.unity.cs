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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Amazon
{
    /// <summary>
    /// Root AWS config section
    /// </summary>
    internal class AWSSection
    {
        public LoggingSection Logging
        {
            get;
            set;
        }

        public string EndpointDefinition
        {
            get;
            set;
        }

        public string Region
        {
            get;
            set;
        }

        public bool? UseSdkCache
        {
            get;
            set;
        }

        public bool? CorrectForClockSkew
        {
            get;
            set;
        }

        public ProxySection Proxy
        {
            get;
            set;
        }

        public string ProfileName
        {
            get;
            set;
        }

        public string ProfilesLocation
        {
            get;
            set;
        }

        private IDictionary<string, XElement> _serviceSections = null;
        public IDictionary<string, XElement> ServiceSections
        {
            get
            {
                if (_serviceSections == null)
                    _serviceSections = new Dictionary<string, XElement>(StringComparer.Ordinal);
                return _serviceSections;
            }
            set
            {
                _serviceSections = value;
            }
        }
    }

    /// <summary>
    /// Settings for configuring a proxy for the SDK to use.
    /// </summary>
    internal class ProxySection
    {
        public const string hostSection = "host";
        public const string portSection = "port";
        public const string usernameSection = "username";
        public const string passwordSection = "password";

        /// <summary>
        /// Gets and sets the host name or IP address of the proxy server.
        /// </summary>
        public string Host
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the port number of the proxy.
        /// </summary>
        public int? Port
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the username to authenticate with the proxy server.
        /// </summary>
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the password to authenticate with the proxy server.
        /// </summary>
        public string Password
        {
            get;
            set;
        }

    }

    /// <summary>
    /// Logging section
    /// </summary>
    internal class LoggingSection
    {
        public const string logToKey = "logTo";
        public const string logResponsesKey = "logResponses";
        public const string logMetricsKey = "logMetrics";
        public const string logMetricsFormatKey = "logMetricsFormat";
        public const string logMetricsCustomFormatterKey = "logMetricsCustomFormatter";

        public LoggingOptions LogTo
        {
            get;
            set;
        }

        public ResponseLoggingOption LogResponses
        {
            get;
            set;
        }

        public bool? LogMetrics
        {
            get;
            set;
        }

        public LogMetricsFormatOption LogMetricsFormat
        {
            get;
            set;
        }


        public Type LogMetricsCustomFormatter
        {
            get;
            set;
        }
    }

}
