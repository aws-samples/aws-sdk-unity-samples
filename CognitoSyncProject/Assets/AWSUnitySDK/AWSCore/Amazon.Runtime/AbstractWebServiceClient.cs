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
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using Amazon.Util;
using System.Globalization;

namespace Amazon.Runtime
{
    public class AbstractWebServiceClient : IDisposable
    {
        [Flags]
        internal enum AuthenticationTypes
        {
            User = 0x1,
            Session = 0x2
        }

        internal static List<string> ErrorCodesToRetryOn = new List<string>
        {
            "Throttling",
            "ProvisionedThroughputExceededException",
            "RequestTimeout"
        };

        // adding the configuration here to avoid causing disrupting changes in all the Marshall/Unmarshallers
        // TODO refactor the Runtime to elegantly fit this change specific to Unity
        internal static List<string> HttpOverrideSupportedServices = new List<string>
        {
            "Amazon.CognitoIdentity",
            "Amazon.CognitoSync",
            "Amazon.SecurityToken"
        };

        // Set of status codes to retry on.
        internal static ICollection<WebExceptionStatus> WebExceptionStatusesToRetryOn = new HashSet<WebExceptionStatus>
        {
            WebExceptionStatus.ConnectFailure,

#if (!WIN_RT) // These statuses are not available on WinRT
            WebExceptionStatus.ConnectionClosed,
            WebExceptionStatus.KeepAliveFailure,
            WebExceptionStatus.NameResolutionFailure
#endif
        };

        // Set of status codes where we don't retry.
        internal static ICollection<WebExceptionStatus> WebExceptionStatusesToThrowOn = new HashSet<WebExceptionStatus>
        {
            WebExceptionStatus.RequestCanceled,
#if (!WIN_RT)
            WebExceptionStatus.Timeout,     // Timeout status not available on WinRT       
#endif
        };

        protected string UNITY_USER_AGENT = "AWS-SDK-UNITY/1.0.0";
        protected const int MAX_BACKOFF_IN_MILLISECONDS = 30 * 1000;
        protected internal ClientConfig Config { get; private set; }
        protected AWSCredentials Credentials { get; private set; }
        internal AuthenticationTypes AuthenticationType;
        internal Logger logger;
        private bool disposed;

        internal AWSCredentials GetCredentials()
        {
            return Credentials;
        }

        #region Dispose Pattern Implementation

        /// <summary>
        /// Implements the Dispose pattern for the AmazonWebServiceClient
        /// </summary>
        /// <param name="disposing">Whether this object is being disposed via a call to Dispose
        /// or garbage collected.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && logger != null)
                {
                    logger.Flush();
                    logger = null;
                }
                this.disposed = true;
            }
        }

        /// <summary>
        /// Disposes of all managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The destructor for the client class.
        /// </summary>
        ~AbstractWebServiceClient()
        {
            this.Dispose(false);
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs before a request is marshalled.
        /// </summary>
        internal event PreRequestEventHandler BeforeMarshallingEvent;

        /// <summary>
        /// Occurs before a request is issued against the service.
        /// </summary>
        public event RequestEventHandler BeforeRequestEvent;

        /// <summary>
        /// Occurs after a response is received from the service.
        /// </summary>
        public event ResponseEventHandler AfterResponseEvent;

        /// <summary>
        /// Occurs after an exception is encountered.
        /// </summary>
        public event ExceptionEventHandler ExceptionEvent;

        #endregion

        #region Constructors
        internal AbstractWebServiceClient(AWSCredentials credentials, ClientConfig config, AuthenticationTypes authenticationType)
        {
            if (config.DisableLogging)
                this.logger = Logger.EmptyLogger;
            else
                this.logger = Logger.GetLogger(this.GetType());

            config.Validate();
            this.Config = config;
            this.AuthenticationType = authenticationType;
            this.Credentials = credentials;

            Initialize();
        }

        internal AbstractWebServiceClient(string awsAccessKeyId, string awsSecretAccessKey, ClientConfig config, AuthenticationTypes authenticationType)
            : this((AWSCredentials)new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey),
                config, authenticationType)
        {
        }

        internal AbstractWebServiceClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, ClientConfig config, AuthenticationTypes authenticationType)
            : this(new SessionAWSCredentials(awsAccessKeyId, awsSecretAccessKey, awsSessionToken), config, authenticationType)
        {
        }

        internal AbstractWebServiceClient(string awsAccessKeyId, string awsSecretAccessKey, ClientConfig config)
            : this(new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey), config, AuthenticationTypes.User)
        {
        }

        internal AbstractWebServiceClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, ClientConfig config)
            : this(new SessionAWSCredentials(awsAccessKeyId, awsSecretAccessKey, awsSessionToken), config, AuthenticationTypes.User)
        {
        }

        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Patches the in-flight uri to stop it unescaping the path etc (what Uri did before
        /// Microsoft deprecated the constructor flag). This is particularly important for
        /// Amazon S3 customers who want to use backslash (\) in their key names.
        /// </summary>
        /// <remarks>
        /// Different behavior in the various runtimes has been observed and in addition some 
        /// 'documented' ways of doing this between 2.x and 4.x runtimes has also been observed 
        /// to not be reliable.
        /// 
        /// This patch effectively emulates what adding a schemesettings element to the 
        /// app.config file with value 'name="http" genericUriParserOptions="DontUnescapePathDotsAndSlashes"'
        /// does. As we're a dll, that avenue is not open to us.
        /// </remarks>
        /// <param name="uri"></param>
        public void DontUnescapePathDotsAndSlashes(Uri uri)
        {
#if BCL
            // System.UriSyntaxFlags is internal
            const int UnEscapeDotsAndSlashes = 0x2000000;

            if (uri == null)
                throw new ArgumentNullException("uri");

            try
            {
                // currently prefer silent return than exceptions or log messages if reflection fails to
                // find the fields we need, otherwise we could generate a lot of noise if someone
                // runs on a platform without these fields
                FieldInfo fieldInfo = uri.GetType().GetField("m_Syntax", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo == null)
                    return;

                var uriParser = fieldInfo.GetValue(uri);

                fieldInfo = typeof(UriParser).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo == null)
                    return;

                var uriSyntaxFlags = fieldInfo.GetValue(uriParser);
                uriSyntaxFlags = (int)uriSyntaxFlags & ~UnEscapeDotsAndSlashes;

                fieldInfo.SetValue(uriParser, uriSyntaxFlags);
            }
            catch (Exception e)
            {
                // swallow the exception, but log it for reference if a user
                // complains of errors
                logger.InfoFormat("Caught exception attempting to set DontUnescapePathDotsAndSlashes mode on uri: {0}", e.Message);
            }
#endif
        }
        #endregion

        /// <summary>
        /// Gets the service url endpoint used by this client.
        /// </summary>
        internal string ServiceURL
        {
            get { return this.Config.DetermineServiceURL(); }
        }

        protected void SignRequest(IRequestData requestData)
        {
            // credentials would be null in the case of anonymous users getting public resources from S3
            if (Credentials == null || Credentials is AnonymousAWSCredentials)
                return;

            using (requestData.Metrics.StartEvent(Metric.RequestSigningTime))
            {
                requestData.Metrics.StartEvent(Metric.CredentialsRequestTime);
                ImmutableCredentials immutableCredentials = Credentials.GetCredentials();
                // credentials would be null in the case of anonymous users getting public resources from S3
                if (immutableCredentials == null)
                    return;
                requestData.Metrics.StopEvent(Metric.CredentialsRequestTime);
                if (immutableCredentials.UseToken)
                {
                    ClientProtocol protocol = requestData.Signer.Protocol;
                    switch (protocol)
                    {
                        case ClientProtocol.QueryStringProtocol:
                            requestData.Request.Parameters["SecurityToken"] = immutableCredentials.Token;
                            break;
                        case ClientProtocol.RestProtocol:
                            requestData.Request.Headers[HeaderKeys.XAmzSecurityTokenHeader] = immutableCredentials.Token;
                            break;
                        default:
							throw new InvalidOperationException("Cannot determine protocol");
                    }
                }
                requestData.Signer.Sign(requestData.Request, this.Config, requestData.Metrics, immutableCredentials.AccessKey, immutableCredentials.SecretKey);
            }
        }

        protected void ConfigureRequest(IRequestData requestData)
        {
            requestData.Request.Headers[HeaderKeys.UserAgentHeader] = UNITY_USER_AGENT;
            
            var method = requestData.Request.HttpMethod.ToUpper(CultureInfo.InvariantCulture);
            if (method != "GET" && method != "DELETE" && method != "HEAD")
            {
                if (!requestData.Request.Headers.ContainsKey(HeaderKeys.ContentTypeHeader))
                {
                    if (requestData.Request.UseQueryString)
                        requestData.Request.Headers[HeaderKeys.ContentTypeHeader] = "application/x-amz-json-1.0";
                    else
                        requestData.Request.Headers[HeaderKeys.ContentTypeHeader] = AWSSDKUtils.UrlEncodedContent;
                }
            }

            ProcessRequestHandlers(requestData.Request);
        }

        protected static byte[] GetRequestData(IRequest request)
        {
            byte[] requestData;
            if (request.Content == null)
            {
                string queryString = AWSSDKUtils.GetParametersAsString(request.Parameters);
                requestData = Encoding.UTF8.GetBytes(queryString);
            }
            else
            {
                requestData = request.Content;
            }

            return requestData;
        }

        protected Uri ComposeUrl(IRequest iRequest, Uri endpoint)
        {
            var url = endpoint;
            var resourcePath = iRequest.ResourcePath;
            if (resourcePath == null)
                resourcePath = string.Empty;
            else
            {
                if (resourcePath.StartsWith("//", StringComparison.Ordinal))
                    resourcePath = resourcePath.Substring(2);
                else if (resourcePath.StartsWith("/", StringComparison.Ordinal))
                    resourcePath = resourcePath.Substring(1);
            }

            // Construct any sub resource/query parameter additions to append to the
            // resource path. Services like S3 which allow '?' and/or '&' in resource paths 
            // should use SubResources instead of appending them to the resource path with 
            // query string delimiters during request marshalling.

            var delim = "?";
            var sb = new StringBuilder();

            if (iRequest.SubResources.Count > 0)
            {
                foreach (var subResource in iRequest.SubResources)
                {
                    sb.AppendFormat("{0}{1}", delim, subResource.Key);
                    if (subResource.Value != null)
                        sb.AppendFormat("={0}", subResource.Value);
                    delim = "&";
                }
            }

            if (iRequest.UseQueryString && iRequest.Parameters.Count > 0)
            {
                var queryString = AWSSDKUtils.GetParametersAsString(iRequest.Parameters);
                sb.AppendFormat("{0}{1}", delim, queryString);
            }

            var parameterizedPath = string.Concat(AWSSDKUtils.UrlEncode(resourcePath, true), sb);
            var uri= new Uri(url.AbsoluteUri + parameterizedPath);
            DontUnescapePathDotsAndSlashes(uri);
            return uri;
        }

        /// <summary>
        /// Used to create a copy of the config for a different service then the current instance.
        /// </summary>
        /// <typeparam name="C"></typeparam>
        /// <returns></returns>
        internal C CloneConfig<C>() 
            where C : ClientConfig, new()
        {
            var config = new C();
            if (!string.IsNullOrEmpty(this.Config.ServiceURL))
            {
                var regionName = Util.AWSSDKUtils.DetermineRegion(this.ServiceURL);
                RegionEndpoint region = RegionEndpoint.GetBySystemName(regionName);
                config.RegionEndpoint = region;
            }
            else
            {
                config.RegionEndpoint = this.Config.RegionEndpoint;
            }

            config.UseHttp = this.Config.UseHttp;


            config.ProxyCredentials = this.Config.ProxyCredentials;
#if BCL
            config.ProxyHost = this.Config.ProxyHost;
            config.ProxyPort = this.Config.ProxyPort;
#endif
            return config;
        }

        #region Process Handlers

        protected virtual void ProcessPreRequestHandlers(AmazonWebServiceRequest request)
        {
            if (request == null)
                return;
            if (BeforeMarshallingEvent == null)
                return;

            PreRequestEventArgs args = PreRequestEventArgs.Create(request);
            BeforeMarshallingEvent(this, args);
        }

        protected virtual void ProcessRequestHandlers(IRequest request)
        {
            if (request == null)
                return;

            WebServiceRequestEventArgs args = WebServiceRequestEventArgs.Create(request);

            if (request.OriginalRequest != null)
                request.OriginalRequest.FireBeforeRequestEvent(this, args);

            if (BeforeRequestEvent != null)
                BeforeRequestEvent(this, args);
        }

        protected virtual void ProcessResponseHandlers(AmazonWebServiceResponse response, IRequest request, IWebResponseData webResponseData)
        {
            if (request == null)
                return;
            if (AfterResponseEvent == null)
                return;

            WebServiceResponseEventArgs args = WebServiceResponseEventArgs.Create(response, request, webResponseData);
            AfterResponseEvent(this, args);
        }

        protected virtual void ProcessExceptionHandlers(Exception exception, IRequest request)
        {
            if (request == null)
                return;
            if (ExceptionEvent == null)
                return;

            WebServiceExceptionEventArgs args = WebServiceExceptionEventArgs.Create(exception, request);
            ExceptionEvent(this, args);
        }
        #endregion

        #region Logging Methods

        protected virtual bool SupportResponseLogging
        {
            get { return true; }
        }

        protected void LogFinalMetrics(RequestMetrics metrics)
        {
            metrics.StopEvent(Metric.ClientExecuteTime);
            if (Config.LogMetrics)
            {
                string errors = metrics.GetErrors();
                if (!string.IsNullOrEmpty(errors))
                    logger.InfoFormat("Request metrics errors: {0}", errors);
                logger.InfoFormat("Request metrics: {0}", metrics);
            }
        }

        protected void LogFinishedResponse(RequestMetrics metrics, UnmarshallerContext context, long contentLength)
        {
            metrics.AddProperty(Metric.BytesProcessed, contentLength);
            if (SupportResponseLogging && (Config.LogResponse || AWSConfigs.LoggingConfig.LogResponses == ResponseLoggingOption.Always))
            {
                this.logger.DebugFormat("Received response: [{0}]", context.ResponseBody);
            }
        }
        #endregion

        #region Error Handling Utility Methods
        protected void pauseExponentially(IRequestData requestData)
        {
            using (requestData.Metrics.StartEvent(Metric.RetryPauseTime))
            {
                pauseExponentially(requestData.RetriesAttempt);
            }
        }

        protected static bool isTemporaryRedirect(HttpStatusCode statusCode, string redirectedLocation)
        {
            return statusCode == HttpStatusCode.TemporaryRedirect && redirectedLocation != null;
        }

        /// <summary>
        /// Returns true if a failed request should be retried.
        /// </summary>
        /// <param name="statusCode">The HTTP status code from the failed request</param>
        /// <param name="config">The client config</param>
        /// <param name="errorResponseException">The exception from the failed request</param>
        /// <param name="retries">The number of times the current request has been attempted</param>
        /// <returns>True if the failed request should be retried.</returns>
        public static bool ShouldRetry(HttpStatusCode statusCode, ClientConfig config, AmazonServiceException errorResponseException, int retries)
        {
            if (retries >= config.MaxErrorRetry)
            {
                return false;
            }

            /*
             * For 500 internal server errors and 503 service
             * unavailable errors, we want to retry, but we need to use
             * an exponential back-off strategy so that we don't overload
             * a server with a flood of retries. If we've surpassed our
             * retry limit we handle the error response as a non-retryable
             * error and go ahead and throw it back to the user as an exception.
             */
            if (statusCode == HttpStatusCode.InternalServerError ||
                statusCode == HttpStatusCode.ServiceUnavailable)
            {
                return true;
            }

#if !WIN_RT
            if (errorResponseException.InnerException is WebException &&
                (((WebException)(errorResponseException.InnerException)).Status == WebExceptionStatus.KeepAliveFailure))
            {
                return true;
            }
#endif

            /*
             * Throttling is reported as a 400 or 503 error from services. To try and
             * smooth out an occasional throttling error, we'll pause and retry,
             * hoping that the pause is long enough for the request to get through
             * the next time.
             */
            if ((statusCode == HttpStatusCode.BadRequest || statusCode == HttpStatusCode.ServiceUnavailable) &&
                errorResponseException != null)
            {
                string errorCode = errorResponseException.ErrorCode;
                if (ErrorCodesToRetryOn.Contains(errorCode))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if the request is in a state where it can be retried.
        /// False otherwise.
        /// </summary>
        /// <param name="irequest"></param>
        /// <returns></returns>
        protected static bool CanRetry(IRequestData irequest)
        {
            var request = irequest.Request;
            var stream = request.ContentStream;
            // Retries may not be possible with a stream
            if (stream != null)
            {
                // Pull out the underlying non-wrapper stream
                stream = WrapperStream.GetNonWrapperBaseStream(stream);

                // Retry is possible if stream is seekable
                return stream.CanSeek;
            }
            return true;
        }

#if (!WIN_RT)
        protected static bool IsInnerExceptionThreadAbort(Exception e)
        {
            var exception = e;
            while (exception != null)
            {
                if (exception.InnerException is ThreadAbortException)
                { return true; }

                exception = exception.InnerException;
            }
            return false;
        }
#endif
        protected static void HandleRetry(IRequestData state)
        {
            state.Metrics.SetCounter(Metric.AttemptCount, state.RetriesAttempt);
            if (state.RetriesAttempt > 0 && state.Request.ContentStream != null && state.Request.OriginalStreamPosition >= 0)
            {
                var stream = state.Request.ContentStream;

                // If the stream is wrapped in a HashStream, reset the HashStream
                var hashStream = stream as HashStream;
                if (hashStream != null)
                {
                    hashStream.Reset();
                    stream = hashStream.GetNonWrapperBaseStream();
                }
                stream.Position = state.Request.OriginalStreamPosition;
            }
        }

        /// <summary> 
        /// Exponential sleep on failed request to avoid flooding a service with
        /// retries.
        /// </summary>
        /// <param name="retries">Current retry count.</param>
        protected virtual void pauseExponentially(int retries)
        {
            int delay = (int)(Math.Pow(4, retries) * 100);
            delay = Math.Min(delay, MAX_BACKOFF_IN_MILLISECONDS);
            AWSSDKUtils.Sleep(delay);
        }

        #endregion
    }
}
