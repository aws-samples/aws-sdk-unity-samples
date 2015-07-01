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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using Amazon.Runtime;
using Amazon.Runtime.Internal.Util;
using System.Reflection;

namespace Amazon.Util
{
    /// <summary>
    /// This class defines utilities and constants that can be used by 
    /// all the client libraries of the SDK.
    /// </summary>
    public static partial class AWSSDKUtils
    {
        #region Internal Constants

        internal const string DefaultRegion = "us-east-1";
        internal const string DefaultGovRegion = "us-gov-west-1";

        internal const string SDKVersionNumber = "2.0.0.2";

        internal const int DefaultMaxRetry = 3;
        private const int DefaultConnectionLimit = 50;
        private const int DefaultMaxIdleTime = 50 * 1000; // 50 seconds

        public static readonly DateTime EPOCH_START = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public const int DefaultBufferSize = 8192;

        // Default value of progress update interval for streaming is 100KB.
        public const long DefaultProgressUpdateInterval = 102400;

        internal static Dictionary<int, string> RFCEncodingSchemes = new Dictionary<int, string>
        {
            { 3986,  ValidUrlCharacters },
            { 1738,  ValidUrlCharactersRFC1738 }
        };

        #endregion

        #region Public Constants


        /// <summary>
        /// The user agent string header
        /// </summary>
        public const string UserAgentHeader = "User-Agent";


        /// <summary>
        /// The Set of accepted and valid Url characters per RFC3986. 
        /// Characters outside of this set will be encoded.
        /// </summary>
        public const string ValidUrlCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        /// <summary>
        /// The Set of accepted and valid Url characters per RFC1738. 
        /// Characters outside of this set will be encoded.
        /// </summary>
        public const string ValidUrlCharactersRFC1738 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.";

        /// <summary>
        /// The set of accepted and valid Url path characters per RFC3986.
        /// </summary>
        private static string ValidPathCharacters = DetermineValidPathCharacters();

        // Checks which path characters should not be encoded
        // This set will be different for .NET 4 and .NET 4.5, as
        // per http://msdn.microsoft.com/en-us/library/hh367887%28v=vs.110%29.aspx
        private static string DetermineValidPathCharacters()
        {
            const string basePathCharacters = "/:'()!*[]";

            var sb = new StringBuilder();
            foreach (var c in basePathCharacters)
            {
                var escaped = Uri.EscapeUriString(c.ToString());
                if (escaped.Length == 1 && escaped[0] == c)
                    sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// The string representing Url Encoded Content in HTTP requests
        /// </summary>
        public const string UrlEncodedContent = "application/x-www-form-urlencoded; charset=utf-8";

        /// <summary>
        /// The GMT Date Format string. Used when parsing date objects
        /// </summary>
        public const string GMTDateFormat = "ddd, dd MMM yyyy HH:mm:ss \\G\\M\\T";

        /// <summary>
        /// The ISO8601Date Format string. Used when parsing date objects
        /// </summary>
        public const string ISO8601DateFormat = "yyyy-MM-dd\\THH:mm:ss.fff\\Z";

        /// <summary>
        /// The ISO8601Date Format string. Used when parsing date objects
        /// </summary>
        public const string ISO8601DateFormatNoMS = "yyyy-MM-dd\\THH:mm:ss\\Z";

        /// <summary>
        /// The ISO8601 Basic date/time format string. Used when parsing date objects
        /// </summary>
        public const string ISO8601BasicDateTimeFormat = "yyyyMMddTHHmmssZ";

        /// <summary>
        /// The ISO8601 basic date format. Used during AWS4 signature computation.
        /// </summary>
        public const string ISO8601BasicDateFormat = "yyyyMMdd";

        /// <summary>
        /// The RFC822Date Format string. Used when parsing date objects
        /// </summary>
        public const string RFC822DateFormat = "ddd, dd MMM yyyy HH:mm:ss \\G\\M\\T";

        #endregion

        #region UserAgent

        static string _versionNumber;
        static string _sdkUserAgent;
        static string _customData;
        
        /// <summary>
        /// The AWS SDK User Agent
        /// </summary>        
        public static string SDKUserAgent
        {
            get
            {
                return _sdkUserAgent;
            }
        }

        static AWSSDKUtils()
        {
            BuildUserAgentString();
        }

        public static void SetUserAgent(string productName, string versionNumber)
        {
            SetUserAgent(productName, versionNumber, null);
        }

        public static void SetUserAgent(string productName, string versionNumber, string customData)
        {
            _userAgentBaseName = productName;
            _versionNumber = versionNumber;
            _customData = customData;
            
            BuildUserAgentString();
        }
        
        static void BuildUserAgentString()
        {
            if (_versionNumber == null)
            {
                _versionNumber = SDKVersionNumber;
            }

            _sdkUserAgent = string.Format(CultureInfo.InvariantCulture, "{0}/{1} .NET Runtime/{2} .NET Framework/{3} OS/{4} {5}",
                _userAgentBaseName,
                _versionNumber,
                DetermineRuntime(),
                DetermineFramework(),
                DetermineOSVersion(),
                _customData).Trim();
        }



        #endregion

        #region Internal Methods

        /// <summary>
        /// Returns an extension of a path.
        /// This has the same behavior as System.IO.Path.GetExtension, but does not
        /// check the path for invalid characters.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetExtension(string path)
        {
            if (path == null)
                return null;
            int length = path.Length;
            int index = length;

            while (--index >= 0)
            {
                char ch = path[index];
                if (ch == '.')
                {
                    if (index != length - 1)
                        return path.Substring(index, length - index);
                    else
                        return string.Empty;
                }
                else if (IsPathSeparator(ch))
                    break;
            }
            return string.Empty;
        }

        // Checks if the character is one \ / :
        private static bool IsPathSeparator(char ch)
        {
            return (ch == '\\' ||
                    ch == '/' ||
                    ch == ':');
        }

        /*
         * Determines the string to be signed based on the input parameters for
         * AWS Signature Version 2
         */
        internal static string CalculateStringToSignV2(IDictionary<string, string> parameters, string serviceUrl)
        {
            StringBuilder data = new StringBuilder("POST\n", 512);
            IDictionary<string, string> sorted =
                  new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            Uri endpoint = new Uri(serviceUrl);

            data.Append(endpoint.Host);
            data.Append("\n");
            string uri = endpoint.AbsolutePath;
            if (uri == null || uri.Length == 0)
            {
                uri = "/";
            }

            data.Append(AWSSDKUtils.UrlEncode(uri, true));
            data.Append("\n");
            foreach (KeyValuePair<string, string> pair in sorted)
            {
                if (pair.Value != null)
                {
                    data.Append(AWSSDKUtils.UrlEncode(pair.Key, false));
                    data.Append("=");
                    data.Append(AWSSDKUtils.UrlEncode(pair.Value, false));
                    data.Append("&");
                }
            }

            string result = data.ToString();
            return result.Remove(result.Length - 1);
        }

        /**
         * Convert Dictionary of paremeters to Url encoded query string
         */
        internal static string GetParametersAsString(IDictionary<string, string> parameters)
        {
            string[] keys = new string[parameters.Keys.Count];
            parameters.Keys.CopyTo(keys, 0);
            Array.Sort<string>(keys);

            StringBuilder data = new StringBuilder(512);
            foreach (string key in keys)
            {
                string value = parameters[key];
                if (value != null)
                {
                    data.Append(key);
                    data.Append('=');
                    data.Append(AWSSDKUtils.UrlEncode(value, false));
                    data.Append('&');
                }
            }
            string result = data.ToString();
            if (result.Length == 0)
                return string.Empty;

            return result.Remove(result.Length - 1);
        }

        /// <summary>
        /// Returns a new string created by joining each of the strings in the
        /// specified list together, with a comma between them.
        /// </summary>
        /// <parma name="strings">The list of strings to join into a single, comma delimited
        /// string list.</parma>
        /// <returns> A new string created by joining each of the strings in the
        /// specified list together, with a comma between strings.</returns>
        public static String Join(List<String> strings)
        {
            StringBuilder result = new StringBuilder();

            Boolean first = true;
            foreach (String s in strings)
            {
                if (!first) result.Append(", ");

                result.Append(s);
                first = false;
            }

            return result.ToString();
        }

        /// <summary>
        /// Attempt to infer the region for a service request based on the endpoint
        /// </summary>
        /// <param name="url">Endpoint to the service to be called</param>
        /// <returns>
        /// Region parsed from the endpoint; DefaultRegion (or DefaultGovRegion) 
        /// if it cannot be determined/is not explicit
        /// </returns>
        public static string DetermineRegion(string url)
        {
            int delimIndex = url.IndexOf("//", StringComparison.Ordinal);
            if (delimIndex >= 0)
                url = url.Substring(delimIndex + 2);

            if(url.EndsWith("/", StringComparison.Ordinal))
                url = url.Substring(0, url.Length - 1);

            int awsIndex = url.IndexOf(".amazonaws.com", StringComparison.Ordinal);
            if (awsIndex < 0)
                return DefaultRegion;
            string serviceAndRegion = url.Substring(0, awsIndex);

            int cloudSearchIndex = url.IndexOf(".cloudsearch.amazonaws.com", StringComparison.Ordinal);
            if (cloudSearchIndex > 0)
                serviceAndRegion = url.Substring(0, cloudSearchIndex);

            int queueIndex = serviceAndRegion.IndexOf("queue", StringComparison.Ordinal);
            if (queueIndex == 0)
                return DefaultRegion;
            if (queueIndex > 0)
                return serviceAndRegion.Substring(0, queueIndex - 1);

            char separator;
            if (serviceAndRegion.StartsWith("s3-", StringComparison.Ordinal))
                separator = '-';
            else
                separator = '.';

            int separatorIndex = serviceAndRegion.IndexOf(separator);
            if (separatorIndex == -1)
                return DefaultRegion;

            string region = serviceAndRegion.Substring(separatorIndex + 1);
            if (region.Equals("external-1"))
                return RegionEndpoint.USEast1.SystemName;

            if (string.Equals(region, "us-gov", StringComparison.Ordinal))
                return DefaultGovRegion;

            return region;
        }

        /// <summary>
        /// Attempt to infer the service name for a request (in short form, eg 'iam') from the
        /// service endpoint.
        /// </summary>
        /// <param name="url">Endpoint to the service to be called</param>
        /// <returns>
        /// Short-form name of the service parsed from the endpoint; empty string if it cannot 
        /// be determined
        /// </returns>
        public static string DetermineService(string url)
        {
            int delimIndex = url.IndexOf("//", StringComparison.Ordinal);
            if (delimIndex >= 0)
                url = url.Substring(delimIndex + 2);

            string[] urlParts = url.Split(new char[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
            if (urlParts == null || urlParts.Length == 0)
                return string.Empty;

            string servicePart = urlParts[0];
            int hyphenated = servicePart.IndexOf('-');
            string service;
            if (hyphenated < 0)
            { service = servicePart; }
            else
            { service = servicePart.Substring(0, hyphenated); }

            // Check for SQS : return "sqs" incase service is determined to be "queue" as per the URL.
            if (service.Equals("queue"))
            {
                return "sqs";
            }
            else
            {
                return service;
            }
        }

        /// <summary>
        /// Utility method for converting Unix epoch seconds to DateTime structure.
        /// </summary>
        /// <param name="seconds">The number of seconds since January 1, 1970.</param>
        /// <returns>Converted DateTime structure</returns>
        public static DateTime ConvertFromUnixEpochSeconds(int seconds)
        {
            return new DateTime(seconds * 10000000L + EPOCH_START.Ticks, DateTimeKind.Utc).ToLocalTime();
        }

        public static int ConvertToUnixEpochSeconds(DateTime dateTime)
        {
            return (int)ConvertToUnixEpochMilliSeconds(dateTime);
        }

        public static double ConvertToUnixEpochMilliSeconds(DateTime dateTime)
        {
            TimeSpan ts = new TimeSpan(dateTime.ToUniversalTime().Ticks - EPOCH_START.Ticks);
            double milli = Math.Round(ts.TotalMilliseconds, 0) / 1000.0;
            return milli;
        }

        /// <summary>
        /// Helper function to format a byte array into string
        /// </summary>
        /// <param name="data">The data blob to process</param>
        /// <param name="lowercase">If true, returns hex digits in lower case form</param>
        /// <returns>String version of the data</returns>
        internal static string ToHex(byte[] data, bool lowercase)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString(lowercase ? "x2" : "X2", CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calls a specific EventHandler in a background thread
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="args"></param>
        /// <param name="sender"></param>
        public static void InvokeInBackground<T>(EventHandler<T> handler, T args, object sender) where T : EventArgs
        {
            if (handler == null) return;


            var list = handler.GetInvocationList();
            foreach (var call in list)
            {
                var eventHandler = ((EventHandler<T>)call);
                if (eventHandler != null)
                {
                    Dispatcher.Dispatch(() => eventHandler(sender, args));
                }
            }
        }

        private static BackgroundInvoker _dispatcher;

        private static BackgroundInvoker Dispatcher
        {
            get
            {
                if (_dispatcher == null)
                {
                    _dispatcher = new BackgroundInvoker();
                }

                return _dispatcher;
            }
        }

        /// <summary>
        /// Parses a query string of a URL and returns the parameters as a string-to-string dictionary.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseQueryParameters(string url)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(url))
            {
                int queryIndex = url.IndexOf('?');
                if (queryIndex >= 0)
                {
                    string queryString = url.Substring(queryIndex + 1);
                    string[] kvps = queryString.Split(new char[] { '&' }, StringSplitOptions.None);
                    foreach (string kvp in kvps)
                    {
                        if (string.IsNullOrEmpty(kvp))
                            continue;

                        string[] nameValuePair = kvp.Split(new char[] { '=' }, 2);
                        string name = nameValuePair[0];
                        string value = nameValuePair.Length == 1 ? null : nameValuePair[1];
                        parameters[name] = value;
                    }
                }
            }

            return parameters;
        }

        internal static bool AreEqual(object[] itemsA, object[] itemsB)
        {
            if (itemsA == null || itemsB == null)
                return (itemsA == itemsB);

            if (itemsA.Length != itemsB.Length)
                return false;

            var length = itemsA.Length;
            for (int i = 0; i < length; i++)
            {
                var a = itemsA[i];
                var b = itemsB[i];
                if (!AreEqual(a, b))
                    return false;
            }

            return true;
        }

        internal static bool AreEqual(object a, object b)
        {
            if (a == null || b == null)
                return (a == b);

            if (object.ReferenceEquals(a, b))
                return true;

            return (a.Equals(b));
        }

        /// <summary>
        /// Utility method for converting a string to a MemoryStream.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static MemoryStream GenerateMemoryStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Utility method for copy the contents of the source stream to the destination stream.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="bufferSize"></param>
        public static void CopyStream(Stream source, Stream destination, int bufferSize = 8192)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize");

            byte[] array = new byte[bufferSize];
            int count;
            while ((count = source.Read(array, 0, array.Length)) != 0)
            {
                destination.Write(array, 0, count);
            }
        }        
	#endregion

        #region Public Methods and Properties

        /// <summary>
        /// Formats the current date as a GMT timestamp
        /// </summary>
        /// <returns>A GMT formatted string representation
        /// of the current date and time
        /// </returns>
        public static string FormattedCurrentTimestampGMT
        {
            get
            {
                DateTime dateTime = AWSSDKUtils.CorrectedUtcNow;
                DateTime formatted = new DateTime(
                    dateTime.Year,
                    dateTime.Month,
                    dateTime.Day,
                    dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second,
                    dateTime.Millisecond,
                    DateTimeKind.Local
                    );
                return formatted.ToString(
                    GMTDateFormat,
                    CultureInfo.InvariantCulture
                    );
            }
        }

        /// <summary>
        /// Formats the current date as ISO 8601 timestamp
        /// </summary>
        /// <returns>An ISO 8601 formatted string representation
        /// of the current date and time
        /// </returns>
        public static string FormattedCurrentTimestampISO8601
        {
            get
            {
                return GetFormattedTimestampISO8601(0);
            }
        }

        /// <summary>
        /// Gets the ISO8601 formatted timestamp that is minutesFromNow
        /// in the future.
        /// </summary>
        /// <param name="minutesFromNow">The number of minutes from the current instant
        /// for which the timestamp is needed.</param>
        /// <returns>The ISO8601 formatted future timestamp.</returns>
        public static string GetFormattedTimestampISO8601(int minutesFromNow)
        {
            DateTime dateTime = AWSSDKUtils.CorrectedUtcNow.AddMinutes(minutesFromNow);
            DateTime formatted = new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                dateTime.Millisecond,
                DateTimeKind.Local
                );
            return formatted.ToString(
                AWSSDKUtils.ISO8601DateFormat,
                CultureInfo.InvariantCulture
                );
        }

        /// <summary>
        /// Formats the current date as ISO 8601 timestamp
        /// </summary>
        /// <returns>An ISO 8601 formatted string representation
        /// of the current date and time
        /// </returns>
        public static string FormattedCurrentTimestampRFC822
        {
            get
            {
                return GetFormattedTimestampRFC822(0);
            }
        }

        /// <summary>
        /// Gets the RFC822 formatted timestamp that is minutesFromNow
        /// in the future.
        /// </summary>
        /// <param name="minutesFromNow">The number of minutes from the current instant
        /// for which the timestamp is needed.</param>
        /// <returns>The ISO8601 formatted future timestamp.</returns>
        public static string GetFormattedTimestampRFC822(int minutesFromNow)
        {
            DateTime dateTime = AWSSDKUtils.CorrectedUtcNow.AddMinutes(minutesFromNow);
            DateTime formatted = new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                dateTime.Millisecond,
                DateTimeKind.Local
                );
            return formatted.ToString(
                AWSSDKUtils.RFC822DateFormat,
                CultureInfo.InvariantCulture
                );
        }


        /// <summary>
        /// URL encodes a string per RFC3986. If the path property is specified,
        /// the accepted path characters {/+:} are not encoded.
        /// </summary>
        /// <param name="data">The string to encode</param>
        /// <param name="path">Whether the string is a URL path or not</param>
        /// <returns>The encoded string</returns>
        public static string UrlEncode(string data, bool path)
        {
            return UrlEncode(3986, data, path);
        }

        /// <summary>
        /// URL encodes a string per the specified RFC. If the path property is specified,
        /// the accepted path characters {/+:} are not encoded.
        /// </summary>
        /// <param name="rfcNumber">RFC number determing safe characters</param>
        /// <param name="data">The string to encode</param>
        /// <param name="path">Whether the string is a URL path or not</param>
        /// <returns>The encoded string</returns>
        /// <remarks>
        /// Currently recognised RFC versions are 1738 (Dec '94) and 3986 (Jan '05). 
        /// If the specified RFC is not recognised, 3986 is used by default.
        /// </remarks>
        public static string UrlEncode(int rfcNumber, string data, bool path)
        {
            StringBuilder encoded = new StringBuilder(data.Length * 2);
            string validUrlCharacters;
            if (!RFCEncodingSchemes.TryGetValue(rfcNumber, out validUrlCharacters))
                validUrlCharacters = ValidUrlCharacters;

            string unreservedChars = String.Concat(validUrlCharacters, (path ? ValidPathCharacters : ""));

            foreach (char symbol in System.Text.Encoding.UTF8.GetBytes(data))
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    encoded.Append(symbol);
                }
                else
                {
                    encoded.Append("%").Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", (int)symbol));
                }
            }

            return encoded.ToString();
        }

        public static void Sleep(TimeSpan ts)
        {
            Sleep((int)ts.TotalMilliseconds);
        }

        /// <summary>
        /// Convert bytes to a hex string
        /// </summary>
        /// <param name="value">Bytes to convert.</param>
        /// <returns>Hexadecimal string representing the byte array.</returns>
        public static string BytesToHexString(byte[] value)
        {
            string hex = BitConverter.ToString(value);
            hex = hex.Replace("-", string.Empty);
            return hex;
        }

        /// <summary>
        /// Convert a hex string to bytes
        /// </summary>
        /// <param name="hex">Hexadecimal string</param>
        /// <returns>Byte array corresponding to the hex string.</returns>
        public static byte[] HexStringToBytes(string hex)
        {
            if (string.IsNullOrEmpty(hex) || hex.Length % 2 == 1)
                throw new ArgumentOutOfRangeException("hex");

            int count = 0;
            byte[] buffer = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                string sub = hex.Substring(i, 2);
                byte b = Convert.ToByte(sub, 16);
                buffer[count] = b;
                count++;
            }

            return buffer;
        }

        /// <summary>
        /// Returns DateTime.UtcNow + ClockOffset when
        /// <seealso cref="AWSConfigs.CorrectForClockSkew"/> is true.
        /// This value should be used when constructing requests, as it
        /// will represent accurate time w.r.t. AWS servers.
        /// </summary>
        public static DateTime CorrectedUtcNow
        {
            get
            {
                var now = DateTime.UtcNow;
                if (AWSConfigs.CorrectForClockSkew)
                    now += AWSConfigs.ClockOffset;
                return now;
            }
        }

        #endregion
        
        
        #region unity utils
        
        #if AWSSDK_UNITY
        
        /// <summary>
        /// Determines if Unity scripting backend is IL2CPP.
        /// </summary>
        /// <returns><c>true</c>If scripting backend is IL2CPP; otherwise, <c>false</c>.</returns>
        internal static bool IsIL2CPP
        {
            get{
                Type type = Type.GetType("Mono.Runtime");
                if (type != null)
                {                                          
                    MethodInfo displayName = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static); 
                    if (displayName != null)
                    {
                        string name = null;
                        try
                        {
                            name = displayName.Invoke(null, null).ToString();
                        }
                        catch(Exception)
                        {
                            return false;
                        }
                
                        if(name != null && name.ToUpper().Contains("IL2CPP"))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        #endif
        
        #endregion
    }
}
