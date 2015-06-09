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

using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;

using Amazon.CognitoIdentity.Model;
using Amazon.Util.Storage;
using Amazon.Util.Storage.Internal;

namespace Amazon.CognitoIdentity
{
    /// <summary>
    /// Temporary, short-lived session credentials that are automatically retrieved from
    /// Amazon Cognito Identity Service and AWS Security Token Service.
    /// Depending on configured Logins, credentials may be authenticated or unauthenticated.
    /// </summary>
    public partial class CognitoAWSCredentials : RefreshingAWSCredentials
    {
        private static readonly String IDENTITY_ID_CACHE_KEY = "CognitoIdentity:IdentityId";

        private static KVStore _kvStore;
        private static object _lock = new object();

        private KVStore KvStore
        {
            get
            {
                lock (_lock)
                {
                    if (_kvStore == null)
                        _kvStore = new PlayerPreferenceKVStore();

                    return _kvStore;
                }
            }
        }

        #region private methods
        private Amazon.SecurityToken.Model.Credentials GetStsCredentials(AssumeRoleWithWebIdentityRequest assumeRequest)
        {
            var assumeResult = sts.AssumeRoleWithWebIdentity(assumeRequest);
            var credentials = assumeResult.Credentials;
            return credentials;
        }

        private GetCredentialsForIdentityResponse GetCredentialsForIdentity(GetCredentialsForIdentityRequest getCredentialsRequest)
        {
            var result = cib.GetCredentialsForIdentity(getCredentialsRequest);
            return result;
        }

        private GetOpenIdTokenResponse GetOpenId(GetOpenIdTokenRequest getTokenRequest)
        {
            var getTokenResult = cib.GetOpenIdToken(getTokenRequest);
            return getTokenResult;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the previously cached the identity id retrieved from Cognito. 
        /// For Unity, the Identity id is stored in PlayerPrefs
        /// </summary>
        /// <returns>The previously cached identity id</returns>
        public virtual string GetCachedIdentityId()
        {
            return KvStore.Get(GetNamespacedKey(IDENTITY_ID_CACHE_KEY));
        }

        /// <summary>
        /// Caches the identity id retrieved from Cognito. 
        /// <para>
        /// For Unity, the Identity id is stored in PlayerPrefs
        /// property.
        /// </para>
        /// </summary>
        /// <param name="identityId">The Cognito identity id to cache</param>
        public virtual void CacheIdentityId(string identityId)
        {
            KvStore.Put(GetNamespacedKey(IDENTITY_ID_CACHE_KEY), identityId);
        }

        /// <summary>
        /// Clears the currently identity id from the cache.
        /// </summary>
        public virtual void ClearIdentityCache()
        {
            KvStore.Clear(GetNamespacedKey(IDENTITY_ID_CACHE_KEY));
        }

        /// <summary>
        /// Caches the credentials to player pref's
        /// </summary>
        internal void CacheCredentials(CredentialsRefreshState credentialsState)
        {
            //TODO: add support for caching
        }

        /// <summary>
        /// Gets the cached credentials state
        /// </summary>
        internal CredentialsRefreshState GetCachedCredentials()
        {
            return null;
            //TODO: add support for caching
        }
        #endregion

        #region private methods

        private string GetNamespacedKey(string key)
        {
            return key + ":" + IdentityPoolId;
        }

        #endregion

        #region GetIdentityId

        /// <summary>
        /// Gets the Identity Id corresponding to the credentials retrieved from Cognito.
        /// Note: this setting may change during execution. To be notified of its
        /// new value, attach a listener to IdentityChangedEvent
        /// </summary>
        /// <param name="callback">The callback which is executed when the asynchronous operations is completed</param>
        /// <param name="options">Options for executing asynchronous operation</param>
        public void GetIdentityIdAsync(AmazonCognitoIdentityCallback<string> callback, AsyncOptions options = null)
        {
            options = options == null ? new AsyncOptions() : options;

            CognitoIdentityAsyncExecutor.ExecuteAsync<string>(() =>
            {
                return GetIdentityId();
            }, options, callback);
        }
        #endregion

        #region GetCredentials

        /// <summary>
        /// Returns an instance of ImmutableCredentials for this instance
        /// </summary>
        /// <param name="callback">The callback which is executed when the asynchronous operations is completed</param>
        /// <param name="options">Options for executing asynchronous operation</param>
        public void GetCredentialsAsync(AmazonCognitoIdentityCallback<ImmutableCredentials> callback, AsyncOptions options = null)
        {
            options = options == null ? new AsyncOptions() : options;

            CognitoIdentityAsyncExecutor.ExecuteAsync<ImmutableCredentials>(() =>
            {
                return GetCredentials();
            }, options, callback);
        }

        #endregion

    }
}
