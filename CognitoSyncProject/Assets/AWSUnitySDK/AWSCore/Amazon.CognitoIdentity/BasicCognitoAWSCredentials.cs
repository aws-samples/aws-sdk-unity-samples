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
using UnityEngine;
using Amazon.Unity;



namespace Amazon.CognitoIdentity
{
    /// <summary>
    /// Temporary, short-lived session credentials that are automatically retrieved from
    /// Amazon Cognito Identity Service and AWS Security Token Service.
    /// Depending on configured Logins, credentials may be authenticated or unauthenticated.
    /// </summary>
    public class BasicCognitoAWSCredentials : CognitoAWSCredentials
    {
        #region Private members

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
        public BasicCognitoAWSCredentials(
            string accountId, string identityPoolId,
            string unAuthRoleArn, string authRoleArn,
            RegionEndpoint region)
            : this(
                unAuthRoleArn, authRoleArn,
                new AmazonCognitoIdentityProvider(accountId, identityPoolId, new AnonymousAWSCredentials(), region),
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
        public BasicCognitoAWSCredentials(string unAuthRoleArn, string authRoleArn,
            AbstractCognitoIdentityProvider idClient, IAmazonSecurityTokenService stsClient) :
            base(unAuthRoleArn, authRoleArn, idClient, stsClient)
        {
        }

        #endregion

        #region Overrides

        #endregion

    }
}
