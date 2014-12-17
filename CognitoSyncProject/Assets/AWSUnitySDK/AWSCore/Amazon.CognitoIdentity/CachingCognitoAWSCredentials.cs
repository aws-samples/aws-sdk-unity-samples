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
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;

using Amazon.CognitoIdentity.Model;
using Amazon;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Amazon.Unity;
using Amazon.Common;
using Amazon.Unity.Storage;



namespace Amazon.CognitoIdentity
{
    /// <summary>
    /// Temporary, short-lived session credentials that are automatically retrieved from
    /// Amazon Cognito Identity Service and AWS Security Token Service.
    /// Depending on configured Logins, credentials may be authenticated or unauthenticated.
    /// </summary>
    public partial class CachingCognitoAWSCredentials : CognitoAWSCredentials
    {
        #region Private members

        private static readonly String ID_KEY = "identityId";
        private static readonly String AK_KEY = "accessKey";
        private static readonly String SK_KEY = "secretKey";
        private static readonly String ST_KEY = "sessionToken";
        private static readonly String EXP_KEY = "expirationDate";
        private static readonly String IP_KEY = "identityPoolID";

        private KVStore _persistentStore;

        #endregion

        #region Public properties, methods, classes, and events

        #endregion

        #region Constructors

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
        public CachingCognitoAWSCredentials(
            string accountId, string identityPoolId,
            string unAuthRoleArn, string authRoleArn,
            RegionEndpoint region)
            : this(
                unAuthRoleArn, authRoleArn,
                new AmazonCognitoIdentityProvider(accountId, identityPoolId, new AnonymousAWSCredentials(), region),
                new AmazonSecurityTokenServiceClient(new AnonymousAWSCredentials(), region))
        {
            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentNullException("accountId cannot be null");
            if (string.IsNullOrEmpty(identityPoolId))
                throw new ArgumentNullException("identityPoolId cannot be null");
            if (string.IsNullOrEmpty(unAuthRoleArn) && string.IsNullOrEmpty(authRoleArn))
                throw new ArgumentNullException("Both unAuthRoleArn and authRoleArn cannot be null");
#if UNITY_WEBPLAYER
            _persistentStore = new InMemoryKVStore();
#else
            _persistentStore = new SQLiteKVStore();
#endif

            string IP = _persistentStore.Get(IP_KEY);

            if (!string.IsNullOrEmpty(IP) && 0 == IP.CompareTo(identityPoolId))
            {
                IdentityProvider.UpdateIdentity(_persistentStore.Get(ID_KEY));

                AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CachingCognitoAWSCredentials", "Loaded Cached IdentityID from LocalStorage");
                loadCachedCredentials();

            }
            else if (!string.IsNullOrEmpty(IP))
            {
                // identity pool id is different from whats caching
                Clear();
            }

            IdentityProvider.IdentityChangedEvent += delegate(object sender, IdentityChangedArgs e)
            {
                if (!string.IsNullOrEmpty(e.OldIdentityId))
                {
                    this.Clear();
                }
                if (string.IsNullOrEmpty(_persistentStore.Get(IP_KEY)))
                {
                    // identity pool id is not cached
                    _persistentStore.Put(IP_KEY, this.IdentityProvider.IdentityPoolId);
                }
                // caching identity whenever new identity is found 
                _persistentStore.Put(ID_KEY, e.NewIdentityId);
                AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CachingCognitoAWSCredentials", "Saved identityID to LocalStorage");
            };
        }



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
        public CachingCognitoAWSCredentials(string unAuthRoleArn, string authRoleArn,
            AbstractCognitoIdentityProvider idClient, IAmazonSecurityTokenService stsClient)
            : base(unAuthRoleArn, authRoleArn, idClient, stsClient)
        {
        }


        public CachingCognitoAWSCredentials()
            : this(AmazonInitializer.AmazonAccountId, AmazonInitializer.IdentityPoolId, AmazonInitializer.DefaultUnAuthRole,
                   AmazonInitializer.DefaultAuthRole, AmazonInitializer.CognitoRegionEndpoint)
        {
            if (string.IsNullOrEmpty(AmazonInitializer.AmazonAccountId))
            {
                throw new ArgumentNullException("AmazonAccountId");
            }

            if (string.IsNullOrEmpty(AmazonInitializer.IdentityPoolId))
            {
                throw new ArgumentNullException("IdentityPoolId");
            }

            if (string.IsNullOrEmpty(AmazonInitializer.DefaultUnAuthRole) && string.IsNullOrEmpty(AmazonInitializer.DefaultAuthRole))
            {
                throw new ArgumentNullException("DefaultUnAuthRole or DefaultAuthRole is null");
            }

        }
        #endregion

        #region Sqlite

        private void loadCachedCredentials()
        {
            String AK = _persistentStore.Get(AK_KEY);
            String SK = _persistentStore.Get(SK_KEY);
            String ST = _persistentStore.Get(ST_KEY);
            String EXP = _persistentStore.Get(EXP_KEY);
            string IP = _persistentStore.Get(IP_KEY);

            long ticks = EXP != null ? long.Parse(EXP) : 0;

            // make sure we have valid data in prefs
            if (AK == null || SK == null ||
                ST == null || IP == null)
            {
                AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CachingCognitoAWSCredentials", "No valid credentials found in LocalStorage");
                _currentState = null;
                return;
            }

            _currentState = new CredentialsRefreshState
            {
                Credentials = new ImmutableCredentials(AK, SK, ST),
                Expiration = new DateTime(ticks)
            };
            AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CachingCognitoAWSCredentials", "Loaded credentials from LocalStorage");
        }

        private void saveCredentials()
        {

            if (_currentState != null)
            {
                _persistentStore.Put(AK_KEY, _currentState.Credentials.AccessKey);
                _persistentStore.Put(SK_KEY, _currentState.Credentials.SecretKey);
                _persistentStore.Put(ST_KEY, _currentState.Credentials.Token);
                _persistentStore.Put(EXP_KEY, _currentState.Expiration.Ticks.ToString());
                _persistentStore.Put(IP_KEY, IdentityProvider.IdentityPoolId);
                _persistentStore.Put(ID_KEY, IdentityProvider.GetCurrentIdentityId());
                AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CachingCognitoAWSCredentials", "Saved credentials to LocalStorage");
            }
        }

        #endregion

        #region Overrides

        public override ImmutableCredentials GetCredentials()
        {
            if (this._currentState == null || _currentState.Credentials == null)
                loadCachedCredentials();
            if (this._currentState == null || _currentState.Credentials == null)
                throw new InvalidOperationException("Invoke GetCredentialsAsync prior to this");
            return _currentState.Credentials.Copy();
        }

        // fetching/persisting new credentials in LocalStorage 
        public override void GetCredentialsAsync(AmazonServiceCallback callback, object state)
        //internal IEnumerator GetCredentialsCoroutine(VoidResponse voidResponse)
        {
            //lock (this._refreshLock)
            //{

            //
            if (_currentState == null)
                loadCachedCredentials();
            // If credentials are expired, update
            if (ShouldUpdate)
            {
                GenerateNewCredentialsAsync(delegate(AmazonServiceResult voidResult)
                {
                    if (voidResult.Exception != null)
                    {
                        AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Errors, "Cognito", "Error occured during GetCredentialsAsync");
                        AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonServiceResult(null, null, voidResult.Exception, state));
                        return;
                    }
                    // Check if the new credentials are already expired
                    if (ShouldUpdate)
                    {
                        voidResult.Exception = new AmazonServiceException("The retrieved credentials have already expired");
                        AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonServiceResult(null, null, voidResult.Exception, state));
                        return;
                    }

                    // Offset the Expiration by PreemptExpiryTime
                    _currentState.Expiration -= PreemptExpiryTime;

                    if (ShouldUpdate)
                    {
                        // This could happen if the default value of PreemptExpiryTime is
                        // overriden and set too high such that ShouldUpdate returns true.

                        voidResult.Exception = new AmazonClientException(String.Format(
                            "The preempt expiry time is set too high: Current time = {0}, Credentials expiry time = {1}, Preempt expiry time = {2}.",
                            DateTime.Now, _currentState.Expiration, PreemptExpiryTime));
                        AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonServiceResult(null, null, voidResult.Exception, state));
                        return;
                    }
                    saveCredentials();
                    AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonServiceResult(null, null, null, state));
                });
                return;
            }
            AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonServiceResult(null, null, null, state));


            //}
        }

        internal override void Clear()
        {
            base.Clear();
            _persistentStore.Clear(IP_KEY);
            _persistentStore.Clear(ID_KEY);
            _persistentStore.Clear(AK_KEY);
            _persistentStore.Clear(SK_KEY);
            _persistentStore.Clear(ST_KEY);
            _persistentStore.Clear(EXP_KEY);
            AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "CachingCognitoAWSCredentials", "Clear Cached Credentials");
        }

        #endregion
    }
}
