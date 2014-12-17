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
using System.Text;

namespace Amazon.Auth.AccessControlPolicy.ActionIdentifiers
{
    /// <summary>
    /// The available AWS access control policy actions for Amazon Cognito Identity.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class CognitoIdentityActionIdentifiers
    {
        public static readonly ActionIdentifier AllCognitoIdentityActions = new ActionIdentifier("cognito-identity:*");

        public static readonly ActionIdentifier CreateIdentityPool = new ActionIdentifier("cognito-identity:CreateIdentityPool");
        public static readonly ActionIdentifier DeleteIdentityPool = new ActionIdentifier("cognito-identity:DeleteIdentityPool");
        public static readonly ActionIdentifier DescribeIdentityPool = new ActionIdentifier("cognito-identity:DescribeIdentityPool");
        public static readonly ActionIdentifier ListIdentities = new ActionIdentifier("cognito-identity:ListIdentities");
        public static readonly ActionIdentifier ListIdentityPools = new ActionIdentifier("cognito-identity:ListIdentityPools");
        public static readonly ActionIdentifier UpdateIdentityPool = new ActionIdentifier("cognito-identity:UpdateIdentityPool");
    }
}
