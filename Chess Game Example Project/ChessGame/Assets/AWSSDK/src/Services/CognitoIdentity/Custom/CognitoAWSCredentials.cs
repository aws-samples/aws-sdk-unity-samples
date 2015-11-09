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
using System.Linq;
using System.Text;

using Amazon.CognitoIdentity.Model;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;

namespace Amazon.CognitoIdentity
{
    /// <summary>
    /// Temporary, short-lived session credentials that are automatically retrieved from
    /// Amazon Cognito Identity Service and AWS Security Token Service.
    /// Depending on configured Logins, credentials may be authenticated or unauthenticated.
    /// </summary>
    public partial class CognitoAWSCredentials : RefreshingAWSCredentials
    {
        #region Private members
        private static object refreshIdLock = new object();
        private string identityId;
        private static int DefaultDurationSeconds = (int)TimeSpan.FromHours(1).TotalSeconds;
#if !AWSSDK_UNITY
        private IAmazonCognitoIdentity cib;
#else
        private AmazonCognitoIdentityClient cib;
#endif
#if !AWSSDK_UNITY
        private IAmazonSecurityTokenService sts;
#else
        private AmazonSecurityTokenServiceClient sts;
#endif
        private bool IsIdentitySet
        {
            get
            {
                if (string.IsNullOrEmpty(identityId))
                {
                    identityId = GetCachedIdentityId();
                }
                return !string.IsNullOrEmpty(identityId);
            }
        }

        // Updates IdentityId to new value and fires IdentityChangedEvent
        private void UpdateIdentity(string newIdentityId)
        {
            // No-op if new IdentityId is same as old
            if (string.Equals(identityId, newIdentityId, StringComparison.Ordinal))
                return;

            //save the new identity id and destroy the credentials associated with the old id.
            CacheIdentityId(newIdentityId);
            ClearCredentials();

            // Swap in new identity
            string oldIdentityId = identityId;
            identityId = newIdentityId;
            // Fire the event
            var handler = IdentityChangedEvent;
            if (handler != null)
            {
                var args = new IdentityChangedArgs(oldIdentityId, newIdentityId);
                handler(this, args);
            }
        }

        #endregion

        #region protected methods and enum

        /// <summary>
        /// Gives a namespaced key for supporting multiple identity pool id's
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetNamespacedKey(string key)
        {
            return key + ":" + IdentityPoolId;
        }

        [Flags]
        private enum RefreshIdentityOptions
        {
            /// <summary>
            /// Dont refresh identity. 
            /// </summary>
            None = 0,

            /// <summary>
            /// Refresh if Id not set or If Identity Provider is BYOI
            /// </summary>
            Refresh = 1
        }


        #endregion

        #region Public properties, methods, classes, and events

        /// <summary>
        /// Information about an identity change in the CognitoAWSCredentials.
        /// </summary>
        public class IdentityChangedArgs : EventArgs
        {
            public string OldIdentityId { get; private set; }
            public string NewIdentityId { get; private set; }

            internal IdentityChangedArgs(string oldIdentityId, string newIdentityId)
            {
                OldIdentityId = oldIdentityId;
                NewIdentityId = newIdentityId;
            }
        }

        /// <summary>
        /// Information about the state of the identity
        /// </summary>
        public class IdentityState
        {
            /// <summary>
            /// Gets the Identity Id
            /// </summary>
            public string IdentityId { get; private set; }

            /// <summary>
            /// Gets the Login Provider
            /// </summary>
            public string LoginProvider { get; private set; }

            /// <summary>
            /// Gets the Login Token
            /// </summary>
            public string LoginToken { get; private set; }

            /// <summary>
            /// Indicates if the identity Id is from cache
            /// </summary>
            public bool FromCache { get; private set; }

            /// <summary>
            /// Creates an instance of the Identity State using identity id , token, provider, fromCache flag
            /// </summary>
            /// <param name="identityId"></param>
            /// <param name="provider"></param>
            /// <param name="token"></param>
            /// <param name="fromCache"></param>
            public IdentityState(string identityId, string provider, string token, bool fromCache)
            {
                IdentityId = identityId;
                LoginProvider = provider;
                LoginToken = token;
                FromCache = fromCache;
            }

            /// <summary>
            /// Creates an instance using the identity id and from cache flag
            /// </summary>
            /// <param name="identityId"></param>
            /// <param name="fromCache"></param>
            public IdentityState(string identityId, bool fromCache)
            {
                IdentityId = identityId;
                FromCache = fromCache;
            }

            /// <summary>
            /// returns true is the Login provider and login token values are present
            /// </summary>
            public bool LoginSpecified
            {
                get
                {
                    return (!string.IsNullOrEmpty(LoginProvider) && string.IsNullOrEmpty(LoginToken));
                }
            }
        }


        /// <summary>
        /// The AWS accountId for the account with Amazon Cognito
        /// </summary>
        public string AccountId { get; private set; }
        /// <summary>
        /// The Amazon Cogntio identity pool to use
        /// </summary>
        public string IdentityPoolId { get; private set; }

        /// <summary>
        /// The ARN of the IAM Role that will be assumed when unauthenticated
        /// </summary>
        public string UnAuthRoleArn { get; private set; }

        /// <summary>
        /// The ARN of the IAM Role that will be assumed when authenticated
        /// </summary>
        public string AuthRoleArn { get; private set; }

        /// <summary>
        /// Logins map used to authenticated with Amazon Cognito.
        /// Note: After modifying this field, you must manually call Clear on this
        /// instance of the CognitoAWSCredentials, as your Identity Id may have changed.
        /// </summary>
        private Dictionary<string, string> Logins { get; set; }

        /// <summary>
        /// Clears current credentials state. This will reset the IdentityId.
        /// Use <see cref="Amazon.Runtime.RefreshingAWSCredentials.ClearCredentials()"/> instead if you just want to trigger a credentials refresh.
        /// </summary>
        public void Clear()
        {
            identityId = null;
            ClearCredentials();
            ClearIdentityCache();
            Logins.Clear();
        }

        /// <summary>
        /// The list of current providers that are used for authenticated credentials.
        /// </summary>
        public string[] CurrentLoginProviders
        {
            get { return this.Logins.Keys.ToArray(); }
        }

        /// <summary>
        /// Returns if the providerName is present in the Logins Collection.
        /// </summary>
        /// <param name="providerName">The provider name for the login (i.e. graph.facebook.com)</param>
        /// <returns>true if the provider name is present in the logins collection, else false</returns>
        public bool ContainsProvider(string providerName)
        {
            return Logins.ContainsKey(providerName);
        }

        /// <summary>
        /// Removes a provider from the collection of logins.
        /// </summary>
        /// <param name="providerName">The provider name for the login (i.e. graph.facebook.com)</param>
        public void RemoveLogin(string providerName)
        {
            this.Logins.Remove(providerName);
            this.ClearCredentials();
        }

        /// <summary>
        /// Adds a login to be used for authenticated requests.
        /// </summary>
        /// <param name="providerName">The provider name for the login (i.e. graph.facebook.com)</param>
        /// <param name="token">The token provided by the identity provider.</param>
        public void AddLogin(string providerName, string token)
        {
            Logins[providerName] = token;
            this.ClearCredentials();
        }

        /// <summary>
        /// Returns count of Login Providers.
        /// </summary>
        /// <returns>The count of the login provider.</returns>
        public int LoginsCount
        {
            get
            {
                return Logins.Count;
            }
        }


        private IdentityState _identityState;


        /// <summary>
        /// Gets the Identity Id corresponding to the credentials retrieved from Cognito.
        /// Note: this setting may change during execution. To be notified of its
        /// new value, attach a listener to IdentityChangedEvent
        /// </summary>
        public string GetIdentityId()
        {
            return GetIdentityId(RefreshIdentityOptions.None);
        }

        private string GetIdentityId(RefreshIdentityOptions options)
        {
            // Locking so that concurrent calls do not make separate network calls,
            // and instead wait for the first caller to cache the Identity ID.
            lock (refreshIdLock)
            {
                if (!IsIdentitySet || options == RefreshIdentityOptions.Refresh)
                {
                    _identityState = RefreshIdentity();

                    if (!string.IsNullOrEmpty(_identityState.LoginProvider))
                    {
                        Logins[_identityState.LoginProvider] = _identityState.LoginToken;
                    }
                    UpdateIdentity(_identityState.IdentityId);
                }
            }
            return identityId;
        }


        /// <summary>
        /// Provides a way to override fetching the identity in case of developer authenticated identities.
        /// The default behaviour will be using Cognito to retrieve the identity id.
        /// </summary>
        /// <returns>returns a <see cref="IdentityState"/></returns>
        protected virtual IdentityState RefreshIdentity()
        {
            bool isCached = true;
            if (!IsIdentitySet)
            {
                var getIdRequest = new GetIdRequest
                {
                    AccountId = AccountId,
                    IdentityPoolId = IdentityPoolId,
                    Logins = Logins
                };
#if BCL || AWSSDK_UNITY
                var response = cib.GetId(getIdRequest);
#else
                var response = Amazon.Runtime.Internal.Util.AsyncHelpers.RunSync<GetIdResponse>(() => cib.GetIdAsync(getIdRequest));
#endif
                isCached = false;
                UpdateIdentity(response.IdentityId);

            }
            return new IdentityState(identityId, isCached);
        }

#if AWS_ASYNC_API
        /// <summary>
        /// Gets the Identity Id corresponding to the credentials retrieved from Cognito.
        /// Note: this setting may change during execution. To be notified of its
        /// new value, attach a listener to IdentityChangedEvent
        /// </summary>
        public async System.Threading.Tasks.Task<string> GetIdentityIdAsync()
        {
            return await GetIdentityIdAsync(false);
        }

        private async System.Threading.Tasks.Task<string> GetIdentityIdAsync(RefreshIdentityOptions options)
        {
            if (!IsIdentitySet || options == RefreshIdentityOptions.Refresh)
            {
                _identityState = await RefreshIdentityAsync().ConfigureAwait(false);
                if (!string.IsNullOrEmpty(_identityState.LoginProvider))
                {
                    Logins[_identityState.LoginProvider] = _identityState.LoginToken;
                }
                UpdateIdentity(_identityState.IdentityId);
            }
            return identityId;
        }


        /// <summary>
        /// Provides a way to override fetching the identity in case of developer authenticated identities.
        /// The default behaviour will be using Cognito to retrieve the identity id.
        /// </summary>
        /// <returns>returns a <see cref="IdentityState"/></returns>
        public virtual async System.Threading.Tasks.Task<IdentityState> RefreshIdentityAsync()
        {
            bool isCached = true;
            if (!IsIdentitySet)
            {
                var getIdRequest = new GetIdRequest
                {
                    AccountId = AccountId,
                    IdentityPoolId = IdentityPoolId,
                    Logins = Logins
                };
                var response = await cib.GetIdAsync(getIdRequest).ConfigureAwait(false);
                isCached = false;
                UpdateIdentity(response.IdentityId);
            }
            return new IdentityState(identityId, isCached);
        }

#endif
        /// <summary>
        /// Checks the exception from a call that used an identity id and determines if the 
        /// failure was caused by a cached identity id. If it was determined then the cache
        /// is cleared and true is return.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private bool ShouldRetry(AmazonCognitoIdentityException e)
        {
            if ((_identityState.LoginSpecified) &&
                    ((e is NotAuthorizedException && e.Message.StartsWith("Access to Identity", StringComparison.OrdinalIgnoreCase)) ||
                      e is ResourceNotFoundException)
               )
            {
                this.identityId = null;
                this.ClearIdentityCache();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Event for identity change notifications.
        /// This event will fire whenever the Identity Id changes.
        /// </summary>
        public event EventHandler<IdentityChangedArgs> IdentityChangedEvent;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new CognitoAWSCredentials instance, which will use the
        /// specified Amazon Cognito identity pool to get short lived session credentials.
        /// </summary>
        /// <param name="identityPoolId">The Amazon Cogntio identity pool to use</param>
        /// <param name="region">Region to use when accessing Amazon Cognito and AWS Security Token Service.</param>
        public CognitoAWSCredentials(string identityPoolId, RegionEndpoint region)
            : this(
                accountId: null, identityPoolId: identityPoolId,
                unAuthRoleArn: null, authRoleArn: null,
                region: region)
        { }

        /// <summary>
        /// Constructs a new CognitoAWSCredentials instance, which will use the
        /// specified Amazon Cognito identity pool to make a requests to the
        /// AWS Security Token Service (STS) to request short lived session credentials.
        /// </summary>
        /// <param name="accountId">The AWS accountId for the account with Amazon Cognito</param>
        /// <param name="identityPoolId">The Amazon Cogntio identity pool to use</param>
        /// <param name="unAuthRoleArn">The ARN of the IAM Role that will be assumed when unauthenticated</param>
        /// <param name="authRoleArn">The ARN of the IAM Role that will be assumed when authenticated</param>
        /// <param name="region">Region to use when accessing Amazon Cognito and AWS Security Token Service.</param>
        public CognitoAWSCredentials(
            string accountId, string identityPoolId,
            string unAuthRoleArn, string authRoleArn,
            RegionEndpoint region)
            : this(
                accountId, identityPoolId,
                unAuthRoleArn, authRoleArn,
                new AmazonCognitoIdentityClient(new AnonymousAWSCredentials(), region),
                new AmazonSecurityTokenServiceClient(new AnonymousAWSCredentials(), region))
        { }

        /// <summary>
        /// Constructs a new CognitoAWSCredentials instance, which will use the
        /// specified Amazon Cognito identity pool to make a requests to the
        /// AWS Security Token Service (STS) to request short lived session credentials.
        /// </summary>
        /// <param name="accountId">The AWS accountId for the account with Amazon Cognito</param>
        /// <param name="identityPoolId">The Amazon Cogntio identity pool to use</param>
        /// <param name="unAuthRoleArn">The ARN of the IAM Role that will be assumed when unauthenticated</param>
        /// <param name="authRoleArn">The ARN of the IAM Role that will be assumed when authenticated</param>
        /// <param name="cibClient">Preconfigured Cognito Identity client to make requests with</param>
        /// <param name="stsClient">>Preconfigured STS client to make requests with</param>
        public CognitoAWSCredentials(
            string accountId, string identityPoolId,
            string unAuthRoleArn, string authRoleArn,
            IAmazonCognitoIdentity cibClient, IAmazonSecurityTokenService stsClient)
        {
            if (string.IsNullOrEmpty(identityPoolId)) throw new ArgumentNullException("identityPoolId");
            if (cibClient == null) throw new ArgumentNullException("cibClient");
            if ((unAuthRoleArn != null || authRoleArn != null) && stsClient == null) throw new ArgumentNullException("stsClient");
            AccountId = accountId;
            IdentityPoolId = identityPoolId;
            UnAuthRoleArn = unAuthRoleArn;
            AuthRoleArn = authRoleArn;
            Logins = new Dictionary<string, string>(StringComparer.Ordinal);
#if !AWSSDK_UNITY
            cib = cibClient;
            sts = stsClient;
#else
            cib = (AmazonCognitoIdentityClient)cibClient;
            sts = (AmazonSecurityTokenServiceClient)stsClient;
#endif
            //check cache for identity id
            string cachedIdentity = GetCachedIdentityId();

            if (!string.IsNullOrEmpty(cachedIdentity))
            {
                UpdateIdentity(cachedIdentity);
                //update the credentials from cache
                _currentState = GetCachedCredentials();
            }
        }

        #endregion

        #region Overrides

#if AWS_ASYNC_API

        protected override async System.Threading.Tasks.Task<CredentialsRefreshState> GenerateNewCredentialsAsync()
        {
            CredentialsRefreshState credentialsState;

            // Pick role to use, depending on Logins
            string roleArn = UnAuthRoleArn;
            if (Logins.Count > 0)
                roleArn = AuthRoleArn;
            bool roleSpecified = !string.IsNullOrEmpty(roleArn);

            // Get credentials from determined role or from identity pool
            if (roleSpecified)
                credentialsState = await GetCredentialsForRoleAsync(roleArn);
            else
                credentialsState = await GetPoolCredentialsAsync();

            CacheCredentials(credentialsState);

            return credentialsState;
        }

        private async System.Threading.Tasks.Task<CredentialsRefreshState> GetCredentialsForRoleAsync(string roleArn)
        {
            CredentialsRefreshState credentialsState;
            // Retrieve Open Id Token
            // (Reuses existing IdentityId or creates a new one)
            var identity = await GetIdentityIdAsync(RefreshIdentityOptions.Refresh).ConfigureAwait(false);
            var getTokenRequest = new GetOpenIdTokenRequest { IdentityId = identity };
            // If logins are set, pass them to the GetOpenId call
            if (Logins.Count > 0)
                getTokenRequest.Logins = Logins;

            bool retry = false;
            GetOpenIdTokenResponse getTokenResult = null;
            try
            {
                getTokenResult = await cib.GetOpenIdTokenAsync(getTokenRequest).ConfigureAwait(false);
            }
            catch (AmazonCognitoIdentityException e)
            {
                if (ShouldRetry(e))
                    retry = true;
                else
                    throw;
            }

            if (retry)
            {
                return await GetCredentialsForRoleAsync(roleArn);
            }

            string token = getTokenResult.Token;

            // IdentityId may have changed, save the new value
            UpdateIdentity(getTokenResult.IdentityId);

            // Assume role with Open Id Token
            var assumeRequest = new AssumeRoleWithWebIdentityRequest
            {
                WebIdentityToken = token,
                RoleArn = roleArn,
                RoleSessionName = "NetProviderSession",
                DurationSeconds = DefaultDurationSeconds
            };
            var credentials = (await sts.AssumeRoleWithWebIdentityAsync(assumeRequest).ConfigureAwait(false)).Credentials;

            // Return new refresh state (credentials and expiration)
            credentialsState = new CredentialsRefreshState(credentials.GetCredentials(), credentials.Expiration);
            return credentialsState;
        }

        // Retrieves credentials for the roles defined on the identity pool
        private async System.Threading.Tasks.Task<CredentialsRefreshState> GetPoolCredentialsAsync()
        {
            CredentialsRefreshState credentialsState;

            var identity = await GetIdentityIdAsync(RefreshIdentityOptions.Refresh).ConfigureAwait(false);
            var getCredentialsRequest = new GetCredentialsForIdentityRequest { IdentityId = identity };
            if (Logins.Count > 0)
                getCredentialsRequest.Logins = Logins;
            if (_identityState != null && !string.IsNullOrEmpty(_identityState.LoginToken))
            {
                getCredentialsRequest.Logins = new Dictionary<string, string>();
                getCredentialsRequest.Logins.Add("cognito-identity.amazonaws.com", _identityState.LoginToken);
            }

            bool retry = false;
            GetCredentialsForIdentityResponse response = null;
            try
            {
                response = (await cib.GetCredentialsForIdentityAsync(getCredentialsRequest).ConfigureAwait(false));
                // IdentityId may have changed, save the new value
                UpdateIdentity(response.IdentityId);
            }
            catch (AmazonCognitoIdentityException e)
            {
                if (ShouldRetry(e))
                    retry = true;
                else
                    throw;
            }

            if (retry)
            {
                return await GetPoolCredentialsAsync();
            }


            var credentials = response.Credentials;
            credentialsState = new CredentialsRefreshState(credentials.GetCredentials(), credentials.Expiration);
            return credentialsState;
        }

#endif

        // Retrieves credentials from Cognito Identity and optionally STS
        protected override CredentialsRefreshState GenerateNewCredentials()
        {
            CredentialsRefreshState credentialsState;

            // Pick role to use, depending on Logins
            string roleArn = UnAuthRoleArn;
            if (Logins.Count > 0)
                roleArn = AuthRoleArn;
            bool roleSpecified = !string.IsNullOrEmpty(roleArn);

            // Get credentials from determined role or from identity pool
            if (roleSpecified)
                credentialsState = GetCredentialsForRole(roleArn);
            else
                credentialsState = GetPoolCredentials();

            CacheCredentials(credentialsState);

            // Return new refresh state (credentials and expiration)
            return credentialsState;
        }

        // Retrieves credentials for the roles defined on the identity pool
        private CredentialsRefreshState GetPoolCredentials()
        {
            CredentialsRefreshState credentialsState;
            var identity = this.GetIdentityId(RefreshIdentityOptions.Refresh);
            var getCredentialsRequest = new GetCredentialsForIdentityRequest { IdentityId = identity };

            if (Logins.Count > 0)
                getCredentialsRequest.Logins = Logins;

            //incase its BYOI provider override the logins dictionary with the new instance and set the values for cognito-identity provider
            if (_identityState != null && !string.IsNullOrEmpty(_identityState.LoginToken))
            {
                getCredentialsRequest.Logins = new Dictionary<string, string>();
                getCredentialsRequest.Logins["cognito-identity.amazonaws.com"] = _identityState.LoginToken;
            }

            bool retry = false;
            GetCredentialsForIdentityResponse response = null;
            try
            {
                response = GetCredentialsForIdentity(getCredentialsRequest);
            }
            catch (AmazonCognitoIdentityException e)
            {
                if (ShouldRetry(e))
                    retry = true;
                else
                    throw;
            }

            if (retry)
            {
                return GetPoolCredentials();
            }

            // IdentityId may have changed, save the new value
            UpdateIdentity(response.IdentityId);

            var credentials = response.Credentials;
            credentialsState = new CredentialsRefreshState(credentials.GetCredentials(), credentials.Expiration);
            return credentialsState;
        }

        // Retrieves credentials for the specific role, by making a call to STS
        private CredentialsRefreshState GetCredentialsForRole(string roleArn)
        {
            CredentialsRefreshState credentialsState;
            // Retrieve Open Id Token
            // (Reuses existing IdentityId or creates a new one)
            var identity = this.GetIdentityId(RefreshIdentityOptions.Refresh);
            var getTokenRequest = new GetOpenIdTokenRequest { IdentityId = identity };
            // If logins are set, pass them to the GetOpenId call
            if (Logins.Count > 0)
                getTokenRequest.Logins = Logins;

            bool retry = false;
            GetOpenIdTokenResponse getTokenResult = null;
            try
            {
                getTokenResult = GetOpenId(getTokenRequest);
            }
            catch (AmazonCognitoIdentityException e)
            {
                if (ShouldRetry(e))
                    retry = true;
                else
                    throw;
            }

            if (retry)
            {
                return GetCredentialsForRole(roleArn);
            }

            string token = getTokenResult.Token;

            // IdentityId may have changed, save the new value
            UpdateIdentity(getTokenResult.IdentityId);

            // Assume role with Open Id Token
            var assumeRequest = new AssumeRoleWithWebIdentityRequest
            {
                WebIdentityToken = token,
                RoleArn = roleArn,
                RoleSessionName = "NetProviderSession",
                DurationSeconds = DefaultDurationSeconds
            };
            var credentials = GetStsCredentials(assumeRequest);

            credentialsState = new CredentialsRefreshState(credentials.GetCredentials(), credentials.Expiration);
            return credentialsState;
        }

        #endregion
    }
}
