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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

using Microsoft.Win32;
using System.Globalization;

namespace Amazon.Util
{
    public static partial class AWSSDKUtils
    {
#if BCL45
        static string _userAgentBaseName = "aws-sdk-dotnet-45";
#else
#if AWSSDK_UNITY
        static string _userAgentBaseName = "AWS-SDK-UNITY";
#else
        static string _userAgentBaseName = "aws-sdk-dotnet-35";
#endif
#endif
        static string DetermineRuntime()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", Environment.Version.Major, Environment.Version.MajorRevision);
        }

        static string DetermineFramework()
        {
            try
            {
                if (Environment.Version.Major >= 4 && Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Net Framework Setup\\NDP\\v4") != null)
                    return "4.0";
                if (Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Net Framework Setup\\NDP\\v3.5") != null)
                    return "3.5";
                if (Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Net Framework Setup\\NDP\\v3.0") != null)
                    return "3.0";
                if (Registry.LocalMachine.OpenSubKey(@"Software\\Microsoft\\Net Framework Setup\\NDP\\v2.0.50727") != null)
                    return "2.0";
            }
            catch
            {
            }

            return "Unknown";
        }

        static string DetermineOSVersion()
        {
            return Environment.OSVersion.Version.ToString();
        }

        public static void ForceCanonicalPathAndQuery(Uri uri)
        {
            try
            {
                FieldInfo flagsFieldInfo = typeof(Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
                ulong flags = (ulong)flagsFieldInfo.GetValue(uri);
                flags &= ~((ulong)0x30); // Flags.PathNotCanonical|Flags.QueryNotCanonical
                flagsFieldInfo.SetValue(uri, flags);
            }
            catch { }
        }

        static readonly object _preserveStackTraceLookupLock = new object();
        static bool _preserveStackTraceLookup = false;
        static MethodInfo _preserveStackTrace;
        /// <summary>
        /// This method is used preserve the stacktrace used from clients that support async calls.  This 
        /// make sure that exceptions thrown during EndXXX methods has the orignal stacktrace that happen 
        /// in the background thread.
        /// </summary>
        /// <param name="exception"></param>
        public static void PreserveStackTrace(Exception exception)
        {
            if (!_preserveStackTraceLookup)
            {
                lock (_preserveStackTraceLookupLock)
                {
                    _preserveStackTraceLookup = true;
                    try
                    {
                        _preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace",
                            BindingFlags.Instance | BindingFlags.NonPublic);
                    }
                    catch { }
                }
            }

            if (_preserveStackTrace != null)
            {
                _preserveStackTrace.Invoke(exception, null);
            }
        }

        private const int _defaultDefaultConnectionLimit = 2;
        internal static int GetConnectionLimit(int? clientConfigValue)
        {
            // Connection limit has been explicitly set on the client.
            if (clientConfigValue.HasValue)
                return clientConfigValue.Value;

            // If default has been left at the system default return the SDK default.
            if (ServicePointManager.DefaultConnectionLimit == _defaultDefaultConnectionLimit)
                return AWSSDKUtils.DefaultConnectionLimit;

            // The system default has been explicitly changed so we will honor that value.
            return ServicePointManager.DefaultConnectionLimit;
        }

        private const int _defaultMaxIdleTime = 100 * 1000;
        internal static int GetMaxIdleTime(int? clientConfigValue)
        {
            // MaxIdleTime has been explicitly set on the client.
            if (clientConfigValue.HasValue)
                return clientConfigValue.Value;

            // If default has been left at the system default return the SDK default.
            if (ServicePointManager.MaxServicePointIdleTime == _defaultMaxIdleTime)
                return AWSSDKUtils.DefaultMaxIdleTime;

            // The system default has been explicitly changed so we will honor that value.
            return ServicePointManager.MaxServicePointIdleTime;
        }

        public static void Sleep(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }
    }
}
