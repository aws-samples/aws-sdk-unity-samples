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
    /// The available AWS access control policy actions for AWS Security Token Service.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class SecurityTokenServiceActionIdentifiers
    {
        public static readonly ActionIdentifier AllSecurityTokenServiceActions = new ActionIdentifier("sts:*");

        public static readonly ActionIdentifier GetFederationToken = new ActionIdentifier("sts:GetFederationToken");
        public static readonly ActionIdentifier AssumeRole = new ActionIdentifier("sts:AssumeRole");
    }
}
