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
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using Amazon.CognitoIdentity;
#if BCL
using System.Configuration;
#endif

using ThirdParty.Json.LitJson;
using Amazon.Runtime.Internal.Util;
using System.Globalization;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;

namespace Amazon.Runtime
{
    /// <summary>
    /// Immutable representation of AWS credentials.
    /// </summary>
    public class ImmutableCredentials
    {
        #region Properties

        /// <summary>
        /// Gets the AccessKey property for the current credentials.
        /// </summary>
        public string AccessKey { get; private set; }

        /// <summary>
        /// Gets the SecretKey property for the current credentials.
        /// </summary>
        public string SecretKey { get; private set; }

        /// <summary>
        /// Gets the Token property for the current credentials.
        /// </summary>
        public string Token { get; private set; }


        /// <summary>
        /// Gets the UseToken property for the current credentials.
        /// Specifies if Token property is non-emtpy.
        /// </summary>
        public bool UseToken { get { return !string.IsNullOrEmpty(Token); } }

        #endregion


        #region Constructors

        /// <summary>
        /// Constructs an ImmutableCredentials object with supplied accessKey, secretKey.
        /// </summary>
        /// <param name="awsAccessKeyId"></param>
        /// <param name="awsSecretAccessKey"></param>
        /// <param name="token">Optional. Can be set to null or empty for non-session credentials.</param>
        public ImmutableCredentials(string awsAccessKeyId, string awsSecretAccessKey, string token)
        {
            if (string.IsNullOrEmpty(awsAccessKeyId)) throw new ArgumentNullException("awsAccessKeyId");
            if (string.IsNullOrEmpty(awsSecretAccessKey)) throw new ArgumentNullException("awsSecretAccessKey");

            AccessKey = awsAccessKeyId;
            SecretKey = awsSecretAccessKey;

            Token = token ?? string.Empty;
        }

        private ImmutableCredentials() { }

        #endregion


        #region Public methods

        /// <summary>
        /// Returns a copy of the current credentials.
        /// </summary>
        /// <returns></returns>
        public ImmutableCredentials Copy()
        {
            ImmutableCredentials credentials = new ImmutableCredentials
            {
                AccessKey = this.AccessKey,
                SecretKey = this.SecretKey,
                Token = this.Token,
            };
            return credentials;
        }

        #endregion
    }

    /// <summary>
    /// Abstract class that represents a credentials object for AWS services.
    /// </summary>
    public abstract class AWSCredentials
    {
        /// <summary>
        /// Returns a copy of ImmutableCredentials
        /// </summary>
        /// <returns></returns>
        public abstract ImmutableCredentials GetCredentials();
    }

    /// <summary>
    /// Basic set of credentials consisting of an AccessKey and SecretKey
    /// </summary>
    public class BasicAWSCredentials : AWSCredentials
    {
        #region Private members

        private ImmutableCredentials _credentials;

        #endregion


        #region Constructors


        /// <summary>
        /// Constructs a BasicAWSCredentials object for the specified accessKey and secretKey.
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="secretKey"></param>
        public BasicAWSCredentials(string accessKey, string secretKey)
        {
            if (!string.IsNullOrEmpty(accessKey))
            {
                _credentials = new ImmutableCredentials(accessKey, secretKey, null);
            }
        }

        #endregion


        #region Abstract class overrides

        /// <summary>
        /// Returns an instance of ImmutableCredentials for this instance
        /// </summary>
        /// <returns></returns>
        public override ImmutableCredentials GetCredentials()
        {
            if (this._credentials == null)
                return null;

            return _credentials.Copy();
        }

        #endregion

    }

    /// <summary>
    /// Session credentials consisting of AccessKey, SecretKey and Token
    /// </summary>
    public class SessionAWSCredentials : AWSCredentials
    {
        #region Public constructors

        /// <summary>
        /// Constructs a SessionAWSCredentials object for the specified accessKey, secretKey.
        /// </summary>
        /// <param name="awsAccessKeyId"></param>
        /// <param name="awsSecretAccessKey"></param>
        /// <param name="token"></param>
        public SessionAWSCredentials(string awsAccessKeyId, string awsSecretAccessKey, string token)
        {
            if (string.IsNullOrEmpty(awsAccessKeyId)) throw new ArgumentNullException("awsAccessKeyId");
            if (string.IsNullOrEmpty(awsSecretAccessKey)) throw new ArgumentNullException("awsSecretAccessKey");
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");

            _lastCredentials = new ImmutableCredentials(awsAccessKeyId, awsSecretAccessKey, token);
        }

        #endregion


        #region Credentials data

        private ImmutableCredentials _lastCredentials;

        #endregion


        #region Abstract class overrides

        /// <summary>
        /// Returns an instance of ImmutableCredentials for this instance
        /// </summary>
        /// <returns></returns>
        public override ImmutableCredentials GetCredentials()
        {
            return _lastCredentials.Copy();
        }

        #endregion

    }

#if BCL

    /// <summary>
    /// Credentials that are retrieved using the stored profile. The SDK Store is searched which is the credentials store shared with the SDK, PowerShell CLI and Toolkit. 
    /// To manage the SDK Store with the SDK use Amazon.Util.ProfileManager. If the profile is not found in the SDK Store then credentials file shared with other AWS SDKs 
    /// is searched. The credentials file is stored in the .aws directory in the current user's home directory.
    /// <para>
    /// The profile name can be specified in the App.config using the AWSProfileName setting.
    /// </para>
    /// <para>
    /// The location to search for credentials can be overridden in the App.config using the AWSProfilesLocation setting.
    /// </para>
    /// </summary>
    public class StoredProfileAWSCredentials : AWSCredentials
    {
    #region Private members

        private const string DEFAULT_PROFILE_NAME = "default";

        private static readonly Logger LOGGER = Logger.GetLogger(typeof(AWSCredentials));

        private ImmutableCredentials _wrappedCredentials;

        #endregion

    #region Public constructors

        /// <summary>
        /// Constructs an instance of StoredProfileAWSCredentials. This constructor searches for credentials using the 
        /// account name specified in the App.config. If no account is specified then the default credentials are used.
        /// </summary>
        public StoredProfileAWSCredentials()
            : this(AWSConfigs.AWSProfileName)
        {

        }

        /// <summary>
        /// Constructs an instance of StoredProfileAWSCredentials. Credentials will be searched for using the profileName parameter.
        /// </summary>
        /// <param name="profileName">The profile name to search for credentials for</param>
        public StoredProfileAWSCredentials(string profileName)
            : this(profileName, AWSConfigs.AWSProfilesLocation)
        {

        }

        /// <summary>
        /// Constructs an instance of StoredProfileAWSCredentials. Credentials will be searched for using the profileName parameter.
        /// </summary>
        /// <param name="profileName">The profile name to search for credentials for</param>
        /// <param name="profilesLocation">Overrides the location to search for credentials</param>
        public StoredProfileAWSCredentials(string profileName, string profilesLocation)
        {
            NameValueCollection appConfig = ConfigurationManager.AppSettings;

            var lookupName = string.IsNullOrEmpty(profileName) ? DEFAULT_PROFILE_NAME : profileName;
            ProfileName = lookupName;
            ProfilesLocation = null;

            // If not overriding the credentials lookup location check the SDK Store for credentials. If an override is being used then
            // assume the intent is to use the credentials file.
            if (string.IsNullOrEmpty(profilesLocation))
            {
                AWSCredentials credentials;
                if (Amazon.Util.ProfileManager.TryGetAWSCredentials(lookupName, out credentials))
                {
                    this._wrappedCredentials = credentials.GetCredentials();
                    LOGGER.InfoFormat("Credentials found using account name {0} and looking in SDK account store.", lookupName);
                }
            }

            // If credentials weren't found in the SDK store then search the shared credentials file.
            if (this._wrappedCredentials == null)
            {
                var credentialsFilePath = DetermineCredentialsFilePath(profilesLocation);
                if (File.Exists(credentialsFilePath))
                {                    
                    string accessKeyId, secretKey;
                    SearchCredentialsFile(credentialsFilePath, lookupName, out accessKeyId, out secretKey);

                    if (accessKeyId != null && secretKey != null)
                    {
                        this._wrappedCredentials = new ImmutableCredentials(accessKeyId, secretKey, null);
                        LOGGER.InfoFormat("Credentials found using account name {0} and looking in {1}.", lookupName, credentialsFilePath);
                    }

                    ProfilesLocation = credentialsFilePath;
                }
            }

            // No credentials found so error out.
            if (this._wrappedCredentials == null)
            {
                throw new ArgumentException("App.config does not contain credentials information. Either add the AWSAccessKey and AWSSecretKey or AWSProfileName.");
            }
        }

        #endregion

    #region Public properties

        /// <summary>
        /// Name of the profile being used.
        /// </summary>
        public string ProfileName { get; private set; }

        /// <summary>
        /// Location of the profiles, if used.
        /// </summary>
        public string ProfilesLocation { get; private set; }

        #endregion

        /// <summary>
        /// Parse the credentials file for access and secret key
        /// </summary>
        /// <param name="credentialsFilePath">The path to the credentials file to parse</param>
        /// <param name="profileName">The profile name to search for</param>
        /// <param name="accessKeyId">The access key returned to the caller</param>
        /// <param name="secretKey">The secret key returned to the caller</param>
        private static void SearchCredentialsFile(string credentialsFilePath, string profileName, out string accessKeyId, out string secretKey)
        {
            accessKeyId = secretKey = null;

            // Add extra newline to make the logic simpler in case there is no newline after the last field in the CLI
            string text = File.ReadAllText(credentialsFilePath) + "\n";
            int sectionStart = text.IndexOf(string.Format(CultureInfo.InvariantCulture, "[{0}]", profileName), StringComparison.Ordinal);
            if (sectionStart == -1)
                return;

            Func<string, string> findValue = ((fieldName) =>
            {
                int startPos = text.IndexOf(fieldName, sectionStart, StringComparison.Ordinal);
                if (startPos == -1)
                    return null;

                int endPos = text.IndexOf('\n', startPos);
                string line = text.Substring(startPos, endPos - startPos);
                string[] tokens = line.Split('=');
                if (tokens.Length != 2)
                    return null;

                return tokens[1].Trim();
            });

            accessKeyId = findValue("aws_access_key_id");
            secretKey = findValue("aws_secret_access_key");
        }

        /// <summary>
        /// Determine the location of the shared credentials file.
        /// </summary>
        /// <param name="profilesLocation">If accountsLocation is null then the shared credentials file stored .aws directory under the home directory.</param>
        /// <returns>The file path to the credentials file to be used.</returns>
        private static string DetermineCredentialsFilePath(string profilesLocation)
        {
            if (!string.IsNullOrEmpty(profilesLocation))
            {
                if (Directory.Exists(profilesLocation))
                    return Path.Combine(profilesLocation, "credentials");
                else
                    return profilesLocation;
            }
            else
            {
                return Path.Combine(
                    Directory.GetParent(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)).FullName,
                    ".aws/credentials");
            }
        }

    #region Abstract class overrides

        /// <summary>
        /// Returns an instance of ImmutableCredentials for this instance
        /// </summary>
        /// <returns></returns>
        public override ImmutableCredentials GetCredentials()
        {
            return this._wrappedCredentials.Copy();
        }

        #endregion
    }

    /// <summary>
    /// Uses aws credentials stored in environment variables to construct the credentials object.
    /// AWS_ACCESS_KEY_ID and AWS_SECRET_ACCESS_KEY are used for the access key id and secret key. If the variable AWS_SESSION_TOKEN exists
    /// then it will be used to create temporary session credentials.
    /// </summary>
    public class EnvironmentVariablesAWSCredentials : AWSCredentials
    {
        private const string ENVIRONMENT_VARIABLE_ACCESSKEY = "AWS_ACCESS_KEY_ID";
        private const string ENVIRONMENT_VARIABLE_SECRETKEY = "AWS_SECRET_ACCESS_KEY";
        private const string ENVIRONMENT_VARIABLE_SESSION_TOKEN = "AWS_SESSION_TOKEN";

        private static readonly Logger LOGGER = Logger.GetLogger(typeof(AWSCredentials));
        private ImmutableCredentials _wrappedCredentials;

    #region Public constructors

        /// <summary>
        /// Constructs an instance of EnvironmentVariablesAWSCredentials. If no credentials are found in the environment variables 
        /// then an InvalidOperationException.
        /// </summary>
        public EnvironmentVariablesAWSCredentials()
        {
            string accessKeyId = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_ACCESSKEY);
            string secretKey = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_SECRETKEY);
            if (string.IsNullOrEmpty(accessKeyId) || string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                    "The environment variables {0} and {1} were not set with AWS credentials.", ENVIRONMENT_VARIABLE_ACCESSKEY, ENVIRONMENT_VARIABLE_SECRETKEY));
            }

            string sessionToken = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_SESSION_TOKEN);

            this._wrappedCredentials = new ImmutableCredentials(accessKeyId, secretKey, sessionToken);
            LOGGER.InfoFormat("Credentials found using environment variables.");
        }

        #endregion

        /// <summary>
        /// Returns an instance of ImmutableCredentials for this instance
        /// </summary>
        /// <returns></returns>
        public override ImmutableCredentials GetCredentials()
        {
            return this._wrappedCredentials.Copy();
        }
    }

    /// <summary>
    /// Credentials that are retrieved from ConfigurationManager.AppSettings
    /// </summary>
    public class EnvironmentAWSCredentials : AWSCredentials
    {
        private const string ACCESSKEY = "AWSAccessKey";
        private const string SECRETKEY = "AWSSecretKey";

        private ImmutableCredentials _wrappedCredentials;
        private static readonly Logger LOGGER = Logger.GetLogger(typeof(AWSCredentials));

    #region Public constructors

        /// <summary>
        /// Constructs an instance of EnvironmentAWSCredentials and attempts
        /// to load AccessKey and SecretKey from ConfigurationManager.AppSettings
        /// </summary>
        public EnvironmentAWSCredentials()
        {
            NameValueCollection appConfig = ConfigurationManager.AppSettings;

            // Use hardcoded credentials
            if (!string.IsNullOrEmpty(appConfig[ACCESSKEY]) && !string.IsNullOrEmpty(appConfig[SECRETKEY]))
            {
                var accessKey = appConfig[ACCESSKEY];
                var secretKey = appConfig[SECRETKEY];
                this._wrappedCredentials = new ImmutableCredentials(accessKey, secretKey, null);
                LOGGER.InfoFormat("Credentials found with {0} and {1} app settings", ACCESSKEY, SECRETKEY);
            }
            // Fallback to the StoredProfileAWSCredentials provider
            else
            {
                this._wrappedCredentials = new StoredProfileAWSCredentials().GetCredentials();
            }
        }

        #endregion


    #region Abstract class overrides

        /// <summary>
        /// Returns an instance of ImmutableCredentials for this instance
        /// </summary>
        /// <returns></returns>
        public override ImmutableCredentials GetCredentials()
        {
            return this._wrappedCredentials.Copy();
        }

        #endregion
    }

#endif

    /// <summary>
    /// Abstract class for automatically refreshing AWS credentials
    /// </summary>
    public abstract class RefreshingAWSCredentials : AWSCredentials
    {
        #region Refresh data

        /// <summary>
        /// Refresh state container consisting of credentials
        /// and the date of the their expiration
        /// </summary>
        protected class CredentialsRefreshState
        {
            public ImmutableCredentials Credentials { get; set; }
            public DateTime Expiration { get; set; }
        }


        protected CredentialsRefreshState _currentState = null;
        protected object _refreshLock = new object();

        #endregion

        #region Private members

        private static Logger _logger = Logger.GetLogger(typeof(RefreshingAWSCredentials));
        private TimeSpan _preemptExpiryTime = TimeSpan.FromMinutes(0);

        #endregion

        #region Properties

        /// <summary>
        /// The time before actual expiration to expire the credentials.        
        /// Property cannot be set to a negative TimeSpan.
        /// </summary>
        public TimeSpan PreemptExpiryTime
        {
            get { return _preemptExpiryTime; }
            set
            {
                if (value < TimeSpan.Zero) throw new ArgumentOutOfRangeException("value", "PreemptExpiryTime cannot be negative");
                _preemptExpiryTime = value;
            }
        }

        #endregion

        #region Override methods

        /// <summary>
        /// Returns an instance of ImmutableCredentials for this instance
        /// </summary>
        /// <returns></returns>
        public override ImmutableCredentials GetCredentials()
        {
            lock (this._refreshLock)
            {
                // If credentials are expired, update
                if (ShouldUpdate)
                {
                    _currentState = GenerateNewCredentials();

                    // Check if the new credentials are already expired
                    if (ShouldUpdate)
                    {
                        throw new AmazonClientException("The retrieved credentials have already expired");
                    }

                    // Offset the Expiration by PreemptExpiryTime
                    _currentState.Expiration -= PreemptExpiryTime;

                    if (ShouldUpdate)
                    {
                        // This could happen if the default value of PreemptExpiryTime is
                        // overriden and set too high such that ShouldUpdate returns true.
                        _logger.InfoFormat(
                            "The preempt expiry time is set too high: Current time = {0}, Credentials expiry time = {1}, Preempt expiry time = {2}.",
                            DateTime.Now,
                            _currentState.Expiration,
                            PreemptExpiryTime);
                    }
                }

                return _currentState.Credentials.Copy();
            }
        }

        #endregion


        #region Private/protected credential update methods

        // Test credentials existence and expiration time
        protected bool ShouldUpdate
        {
            get
            {
                return
                    (                                                   // should update if:
                        _currentState == null ||                        //  credentials have not been loaded yet
                        DateTime.Now >= this._currentState.Expiration   //  past the expiration time
                    );
            }
        }

        /// <summary>
        /// When overridden in a derived class, generates new credentials and new expiration date.
        /// 
        /// Called on first credentials request and when expiration date is in the past.
        /// </summary>
        /// <returns></returns>
        protected virtual CredentialsRefreshState GenerateNewCredentials()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    /// <summary>
    /// Credentials that are retrieved from the Instance Profile service on an EC2 instance
    /// </summary>
    public class InstanceProfileAWSCredentials : RefreshingAWSCredentials
    {
        #region Private members

        // Set preempt expiry to 15 minutes. New access keys are available at least 15 minutes before expiry time.
        // http://docs.aws.amazon.com/IAM/latest/UserGuide/role-usecase-ec2app.html
        private static TimeSpan _preemptExpiryTime = TimeSpan.FromMinutes(15);

        private CredentialsRefreshState _currentRefreshState = null;
        private static TimeSpan _refreshAttemptPeriod = TimeSpan.FromHours(1);
        private static Logger _logger = Logger.GetLogger(typeof(InstanceProfileAWSCredentials));

        #endregion

        #region Properties

        /// <summary>
        /// Role for which the credentials are retrieved
        /// </summary>
        public string Role { get; set; }

        #endregion


        #region Overrides

        protected override CredentialsRefreshState GenerateNewCredentials()
        {
            CredentialsRefreshState newState = null;
            try
            {
                // Attempt to get early credentials. OK to fail at this point.
                newState = GetRefreshState();
            }
            catch (Exception e)
            {
                _logger.InfoFormat("Error getting credentials from Instance Profile service: {0}", e);
            }

            // If successful, save new credentials
            if (newState != null)
                _currentRefreshState = newState;

            // If still not successful (no credentials available at start), attempt once more to
            // get credentials, but now without swallowing exception
            if (_currentRefreshState == null)
                _currentRefreshState = GetRefreshState();

            // Return credentials that will expire in at most one hour
            CredentialsRefreshState state = GetEarlyRefreshState(_currentRefreshState);
            return state;
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Constructs a InstanceProfileAWSCredentials object for specific role
        /// </summary>
        /// <param name="role">Role to use</param>
        public InstanceProfileAWSCredentials(string role)
        {
            Role = role;
            this.PreemptExpiryTime = _preemptExpiryTime;
        }

        /// <summary>
        /// Constructs a InstanceProfileAWSCredentials object for the first found role
        /// </summary>
        public InstanceProfileAWSCredentials()
            : this(GetFirstRole()) { }

        #endregion


        #region Public static methods

        /// <summary>
        /// Retrieves a list of all roles available through current InstanceProfile service
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetAvailableRoles()
        {
            string allAliases = GetContents(RolesUri);
            if (string.IsNullOrEmpty(allAliases))
                yield break;

            string[] parts = allAliases.Split(AliasSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                var trim = part.Trim();
                if (!string.IsNullOrEmpty(trim))
                    yield return trim;
            }
        }

        #endregion


        #region Private members

        private static string[] AliasSeparators = new string[] { "<br/>" };
        private static string Server = "http://169.254.169.254";
        private static string RolesPath = "/latest/meta-data/iam/security-credentials/";
        private static string InfoPath = "/latest/meta-data/iam/info";
        private static string SuccessCode = "Success";

        private static Uri RolesUri
        {
            get
            {
                return new Uri(Server + RolesPath);
            }
        }
        private Uri CurrentRoleUri
        {
            get
            {
                return new Uri(Server + RolesPath + Role);
            }
        }
        private static Uri InfoUri
        {
            get
            {
                return new Uri(Server + InfoPath);
            }
        }

        private CredentialsRefreshState GetEarlyRefreshState(CredentialsRefreshState state)
        {
            // New expiry time = Now + _refreshAttemptPeriod + PreemptExpiryTime
            var newExpiryTime = DateTime.Now + _refreshAttemptPeriod + PreemptExpiryTime;
            // Use this only if the time is earlier than the default expiration time
            if (newExpiryTime > state.Expiration)
                newExpiryTime = state.Expiration;

            return new CredentialsRefreshState
            {
                Credentials = state.Credentials.Copy(),
                Expiration = newExpiryTime
            };
        }

        private CredentialsRefreshState GetRefreshState()
        {
            SecurityInfo info = GetServiceInfo();
            if (!string.IsNullOrEmpty(info.Message))
            {
                throw new AmazonServiceException(string.Format(CultureInfo.InvariantCulture,
                    "Unable to retrieve credentials. Message = \"{0}\".",
                    info.Message));
            }
            SecurityCredentials credentials = GetRoleCredentials();

            CredentialsRefreshState refreshState = new CredentialsRefreshState
            {
                Credentials = new ImmutableCredentials(credentials.AccessKeyId, credentials.SecretAccessKey, credentials.Token),
                Expiration = credentials.Expiration
            };

            return refreshState;
        }

        private static SecurityInfo GetServiceInfo()
        {
            string json = GetContents(InfoUri);
            SecurityInfo info = JsonMapper.ToObject<SecurityInfo>(json);
            ValidateResponse(info);
            return info;
        }

        private SecurityCredentials GetRoleCredentials()
        {
            string json = GetContents(CurrentRoleUri);
            SecurityCredentials credentials = JsonMapper.ToObject<SecurityCredentials>(json);
            ValidateResponse(credentials);
            return credentials;
        }

        private static void ValidateResponse(SecurityBase response)
        {
            if (!string.Equals(response.Code, SuccessCode, StringComparison.OrdinalIgnoreCase))
            {
                throw new AmazonServiceException(string.Format(CultureInfo.InvariantCulture,
                    "Unable to retrieve credentials. Code = \"{0}\". Message = \"{1}\".",
                    response.Code, response.Message));
            }
        }

        private static string GetContents(Uri uri)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
                var asyncResult = request.BeginGetResponse(null, null);
                using (HttpWebResponse response = request.EndGetResponse(asyncResult) as HttpWebResponse)
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException)
            {
                throw new AmazonServiceException("Unable to reach credentials server");
            }
        }

        private static string GetFirstRole()
        {
            IEnumerable<string> roles = GetAvailableRoles();
            foreach (string role in roles)
            {
                return role;
            }

            // no roles found
            throw new InvalidOperationException("No roles found");
        }

        #endregion


        #region Private serialization classes

        private class SecurityBase
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public DateTime LastUpdated { get; set; }
        }

        private class SecurityInfo : SecurityBase
        {
            public string InstanceProfileArn { get; set; }
            public string InstanceProfileId { get; set; }
        }

        private class SecurityCredentials : SecurityBase
        {
            public string Type { get; set; }
            public string AccessKeyId { get; set; }
            public string SecretAccessKey { get; set; }
            public string Token { get; set; }
            public DateTime Expiration { get; set; }
        }

        #endregion
    }

    /// <summary>
    /// Credentials that are retrieved by invoking AWS Security Token Service
    /// AssumeRole or AssumeRoleWithSAML action.
    /// </summary>
    public partial class AssumeRoleAWSCredentials : RefreshingAWSCredentials, IDisposable
    {
        private IAmazonSecurityTokenService _stsClient;
        private bool _isDisposed = false;
        private static TimeSpan _defaultPreemptExpiryTime = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Instantiates AssumeRoleAWSCredentials which automatically assumes a specified role.
        /// The credentials are refreshed before expiration.
        /// </summary>
        /// <param name="sts">
        /// Instance of IAmazonSecurityTokenService that will be used to make the AssumeRole service call.
        /// </param>
        /// <param name="assumeRoleRequest">Configuration for the role to assume.</param>
        public AssumeRoleAWSCredentials(IAmazonSecurityTokenService sts, AssumeRoleRequest assumeRoleRequest)
        {
            if (sts == null) throw new ArgumentNullException("sts");
            if (assumeRoleRequest == null) throw new ArgumentNullException("assumeRoleRequest");

            _stsClient = sts;
            PreemptExpiryTime = _defaultPreemptExpiryTime;
        }

        /// <summary>
        /// Instantiates AssumeRoleAWSCredentials which automatically assumes a specified SAML role.
        /// The credentials are refreshed before expiration.
        /// </summary>
        /// <param name="assumeRoleWithSamlRequest">Configuration for the SAML role to assume.</param>
        public AssumeRoleAWSCredentials(AssumeRoleWithSAMLRequest assumeRoleWithSamlRequest)
        {
            if (assumeRoleWithSamlRequest == null) throw new ArgumentNullException("assumeRoleWithSamlRequest");

            _stsClient = new AmazonSecurityTokenServiceClient(new AnonymousAWSCredentials());
            PreemptExpiryTime = _defaultPreemptExpiryTime;
        }

        protected override CredentialsRefreshState GenerateNewCredentials()
        {
            Credentials credentials = GetServiceCredentials();
            return new CredentialsRefreshState
            {
                Expiration = credentials.Expiration,
                Credentials = credentials.GetCredentials()
            };
        }

        #region Dispose Pattern Implementation

        /// <summary>
        /// Implements the Dispose pattern
        /// </summary>
        /// <param name="disposing">Whether this object is being disposed via a call to Dispose
        /// or garbage collected.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing && _stsClient != null)
                {
                    _stsClient.Dispose();
                    _stsClient = null;
                }
                this._isDisposed = true;
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

        #endregion
    }

    /// <summary>
    /// Anonymous credentials.
    /// Using these credentials, the client does not sign the request.
    /// </summary>
    public class AnonymousAWSCredentials : AWSCredentials
    {
        #region Abstract class overrides

        /// <summary>
        /// Returns an instance of ImmutableCredentials for this instance
        /// </summary>
        /// <returns></returns>
        public override ImmutableCredentials GetCredentials()
        {
            throw new NotSupportedException("AnonymousAWSCredentials do not support this operation");
        }

        #endregion
    }


    // Credentials fallback mechanism
    internal static class FallbackCredentialsFactory
    {
        static FallbackCredentialsFactory()
        {
            Reset();
        }

        public delegate AWSCredentials CredentialsGenerator();
        public static List<CredentialsGenerator> CredentialsGenerators { get; set; }
        public static void Reset()
        {
            cachedCredentials = null;
            CredentialsGenerators = new List<CredentialsGenerator>
            {
#if BCL
                () => new EnvironmentAWSCredentials(),
                () => new StoredProfileAWSCredentials(),
                () => new EnvironmentVariablesAWSCredentials(),
                () => new InstanceProfileAWSCredentials()
#endif
				() => new CachingCognitoAWSCredentials()
            };
        }

        private static AWSCredentials cachedCredentials;
        internal static AWSCredentials GetCredentials(bool fallbackToAnonymous = false)
        {
            if (cachedCredentials != null)
                return cachedCredentials;

            List<Exception> errors = new List<Exception>();

            foreach (CredentialsGenerator generator in CredentialsGenerators)
            {
                try
                {
                    cachedCredentials = generator();
                }
                catch (Exception e)
                {
                    cachedCredentials = null;
                    errors.Add(e);
                }

                if (cachedCredentials != null)
                    break;
            }

            if (cachedCredentials == null)
            {
                if (fallbackToAnonymous)
                {
                    return new AnonymousAWSCredentials();
                }

                using (StringWriter writer = new StringWriter(CultureInfo.InvariantCulture))
                {
                    writer.WriteLine("Unable to find credentials");
                    writer.WriteLine();
                    for (int i = 0; i < errors.Count; i++)
                    {
                        Exception e = errors[i];
                        writer.WriteLine("Exception {0} of {1}:", i + 1, errors.Count);
                        writer.WriteLine(e.ToString());
                        writer.WriteLine();
                    }

                    throw new AmazonServiceException(writer.ToString());
                }
            }

            return cachedCredentials;
        }
    }
}
