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
    /// The available AWS access control policy actions for Amazon Cognito Sync.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class CognitoSyncActionIdentifiers
    {
        public static readonly ActionIdentifier AllCognitoSyncActions = new ActionIdentifier("cognito-sync:*");

        public static readonly ActionIdentifier DeleteDataset = new ActionIdentifier("cognito-sync:DeleteDataset");
        public static readonly ActionIdentifier DescribeDataset = new ActionIdentifier("cognito-sync:DescribeDataset");
        public static readonly ActionIdentifier DescribeIdentityUsage = new ActionIdentifier("cognito-sync:DescribeIdentityUsage");
        public static readonly ActionIdentifier DescribeIdentityPoolUsage = new ActionIdentifier("cognito-sync:DescribeIdentityPoolUsage");
        public static readonly ActionIdentifier ListDatasets = new ActionIdentifier("cognito-sync:ListDatasets");
        public static readonly ActionIdentifier ListIdentityPoolUsage = new ActionIdentifier("cognito-sync:ListIdentityPoolUsage");
        public static readonly ActionIdentifier ListRecords = new ActionIdentifier("cognito-sync:ListRecords");
        public static readonly ActionIdentifier UpdateRecords = new ActionIdentifier("cognito-sync:UpdateRecords");
    }
}
