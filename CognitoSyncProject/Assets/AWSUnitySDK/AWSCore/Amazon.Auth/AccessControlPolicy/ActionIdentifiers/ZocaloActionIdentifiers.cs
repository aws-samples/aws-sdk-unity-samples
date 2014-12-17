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
    /// The available AWS access control policy actions for Amazon Zocalo.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class ZocaloActionIdentifiers
    {
        public static readonly ActionIdentifier AllZocaloActions = new ActionIdentifier("zocalo:*");

        public static readonly ActionIdentifier ActivateUser = new ActionIdentifier("zocalo:ActivateUser");
        public static readonly ActionIdentifier AddUserToGroup = new ActionIdentifier("zocalo:AddUserToGroup");
        public static readonly ActionIdentifier CheckAlias = new ActionIdentifier("zocalo:CheckAlias");
        public static readonly ActionIdentifier CreateInstance = new ActionIdentifier("zocalo:CreateInstance");
        public static readonly ActionIdentifier DeactivateUser = new ActionIdentifier("zocalo:DeactivateUser");
        public static readonly ActionIdentifier DeleteInstance = new ActionIdentifier("zocalo:DeleteInstance");
        public static readonly ActionIdentifier DeregisterDirectory = new ActionIdentifier("zocalo:DeregisterDirectory");
        public static readonly ActionIdentifier DescribeAvailableDirectories = new ActionIdentifier("zocalo:DescribeAvailableDirectories");
        public static readonly ActionIdentifier DescribeInstances = new ActionIdentifier("zocalo:DescribeInstances");
        public static readonly ActionIdentifier RegisterDirectory = new ActionIdentifier("zocalo:RegisterDirectory");
        public static readonly ActionIdentifier RemoveUserFromGroup = new ActionIdentifier("zocalo:RemoveUserFromGroup");
        public static readonly ActionIdentifier UpdateInstanceAlias = new ActionIdentifier("zocalo:UpdateInstanceAlias");
    }
}
