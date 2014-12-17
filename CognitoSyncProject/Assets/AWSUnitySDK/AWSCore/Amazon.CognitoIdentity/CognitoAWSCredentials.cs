using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
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
using Amazon.CognitoIdentity.Model;
using Amazon;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using UnityEngine;
using Amazon.Unity;
using Amazon.Common;

namespace Amazon.CognitoIdentity
{
    /// <summary>
    /// Temporary, short-lived session credentials that are automatically retrieved from
    /// Amazon Cognito Identity Service and AWS Security Token Service.
    /// Depending on configured Logins, credentials may be authenticated or unauthenticated.
    /// </summary>
    public abstract class CognitoAWSCredentials : RefreshingAWSCredentials
    {
        #region Private members

        private static int DefaultDurationSeconds = (int)TimeSpan.FromHours(1).TotalSeconds;
        private IAmazonSecurityTokenService sts;
        private AbstractCognitoIdentityProvider _identityProvider;

        #endregion

        #region Public properties, methods, classes, and events

        /// <summary>
        /// The ARN of the IAM Role that will be assumed when unauthenticated
        /// </summary>
        public string UnAuthRoleArn { get; protected set; }

        /// <summary>
        /// The ARN of the IAM Role that will be assumed when authenticated
        /// </summary>
        public string AuthRoleArn { get; protected set; }

        public AbstractCognitoIdentityProvider IdentityProvider { get { return this._identityProvider; } protected set { this._identityProvider = value; } }

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
        public CognitoAWSCredentials(
            string accountId, string identityPoolId,
            string unAuthRoleArn, string authRoleArn,
            RegionEndpoint region)
            : this(
                unAuthRoleArn, authRoleArn,
                new AmazonCognitoIdentityProvider(accountId, identityPoolId, new AnonymousAWSCredentials(), region),
                new AmazonSecurityTokenServiceClient(new AnonymousAWSCredentials(), region))
        {
        }

        /// <summary>
        /// Constructs a new CognitoAWSCredentials instance, which will use the
        /// specified Amazon Cognito identity pool to make a requests to the
        /// AWS Security Token Service (STS) to request short lived session credentials.
        /// </summary>
        /// <param name="unAuthRoleArn">The ARN of the IAM Role that will be assumed when unauthenticated</param>
        /// <param name="authRoleArn">The ARN of the IAM Role that will be assumed when authenticated</param>
        /// <param name="cibClient">Preconfigured Cognito Identity client to make requests with</param>
        /// <param name="stsClient">>Preconfigured STS client to make requests with</param>
        public CognitoAWSCredentials(string unAuthRoleArn, string authRoleArn,
            AbstractCognitoIdentityProvider idClient, IAmazonSecurityTokenService stsClient)
        {
            if (string.IsNullOrEmpty(unAuthRoleArn) && string.IsNullOrEmpty(authRoleArn))
                throw new InvalidOperationException("At least one of unAuthRoleArn or authRoleArn must be specified");
            if (idClient == null)
                throw new ArgumentNullException("idClient");
            if (stsClient == null)
                throw new ArgumentNullException("stsClient");

            UnAuthRoleArn = unAuthRoleArn;
            AuthRoleArn = authRoleArn;
            IdentityProvider = idClient;
            sts = stsClient;
        }

        #endregion

        #region Overrides

        public override ImmutableCredentials GetCredentials()
        {
            if (this._currentState == null || _currentState.Credentials == null)
				throw new InvalidOperationException("Invoke GetCredentialsAsync prior to this");
            return _currentState.Credentials.Copy();
        }

        /// <summary>
        /// Returns an instance of ImmutableCredentials for this instance
        /// </summary>
        /// <returns></returns>
        public virtual void GetCredentialsAsync(AmazonServiceCallback callback, object state)
        //internal IEnumerator GetCredentialsCoroutine(VoidResponse voidResponse)
        {
            // TODO support locking async methods in next version
            //lock (this._refreshLock)
            //{
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
					AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonServiceResult(null, null, null, state));
                });
                return;
            }
			AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonServiceResult(null, null, null, state));
        }

        protected void GenerateNewCredentialsAsync(AmazonServiceCallback callback)
        {
            AmazonServiceResult voidResult = new AmazonServiceResult(null, null);

            IdentityProvider.RefreshAsync(delegate(AmazonServiceResult refreshResult)
            {
                if (refreshResult.Exception != null)
                {
                    voidResult.Exception = refreshResult.Exception;
					AmazonMainThreadDispatcher.ExecCallback(callback, voidResult);
                    return;
                }


                // Pick role to use, depending on Logins
                string roleArn = UnAuthRoleArn;
                if (IdentityProvider.Logins.Count > 0)
                    roleArn = AuthRoleArn;
                if (string.IsNullOrEmpty(roleArn))
                {
                    voidResult.Exception = new AmazonServiceException(
                        new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                                                                "Unable to determine Role ARN. AuthRoleArn = [{0}], UnAuthRoleArn = [{1}], Logins.Count = {2}",
                                                                AuthRoleArn, UnAuthRoleArn, IdentityProvider.Logins.Count)));
					AmazonMainThreadDispatcher.ExecCallback(callback, voidResult);
                    return;
                }

                // Assume role with Open Id Token
                var assumeRequest = new AssumeRoleWithWebIdentityRequest
                {
                    WebIdentityToken = IdentityProvider.GetCurrentOpenIdToken(),
                    RoleArn = roleArn,
                    RoleSessionName = "UnityProviderSession",
                    DurationSeconds = DefaultDurationSeconds

                };

                sts.AssumeRoleWithWebIdentityAsync(assumeRequest, delegate(AmazonServiceResult result)
                {
                    if (result.Exception != null)
                    {
                        voidResult.Exception = result.Exception;
                        AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Errors, "STS", result.Exception.Message);
						AmazonMainThreadDispatcher.ExecCallback(callback, voidResult);
                        return;
                    }
                    AssumeRoleWithWebIdentityResponse assumeRoleWithWebIdentityResponse = result.Response as AssumeRoleWithWebIdentityResponse;
                    this._currentState = new CredentialsRefreshState
                    {
                        Credentials = assumeRoleWithWebIdentityResponse.Credentials.GetCredentials(),
                        Expiration = assumeRoleWithWebIdentityResponse.Credentials.Expiration
                    };
                    // success - FinalResponse
					AmazonMainThreadDispatcher.ExecCallback(callback, voidResult);
                    return;
                }, null);

            }, null);
        }

        internal virtual void Clear()
        {
            _currentState = null;
            //var fi = IdentityProvider.GetType().GetEvent("IdentityChangedEvent");
            //if (fi != null)
            //    fi.RemoveEventHandler()

        }
        #endregion

    }
}
