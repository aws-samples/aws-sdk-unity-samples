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
    /// The available AWS access control policy actions for Amazon AppStream.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class AppStreamActionIdentifiers
    {
        public static readonly ActionIdentifier AllAppStreamActions = new ActionIdentifier("appstream:*");

        public static readonly ActionIdentifier CreateApplication = new ActionIdentifier("appstream:CreateApplication");
        public static readonly ActionIdentifier CreateSession = new ActionIdentifier("appstream:CreateSession");
        public static readonly ActionIdentifier DeleteApplication = new ActionIdentifier("appstream:DeleteApplication");
        public static readonly ActionIdentifier GetApiRoot = new ActionIdentifier("appstream:GetApiRoot");
        public static readonly ActionIdentifier GetApplication = new ActionIdentifier("appstream:GetApplication");
        public static readonly ActionIdentifier GetApplications = new ActionIdentifier("appstream:GetApplications");
        public static readonly ActionIdentifier GetApplicationError = new ActionIdentifier("appstream:GetApplicationError");
        public static readonly ActionIdentifier GetApplicationErrors = new ActionIdentifier("appstream:GetApplicationErrors");
        public static readonly ActionIdentifier GetApplicationStatus = new ActionIdentifier("appstream:GetApplicationStatus");
        public static readonly ActionIdentifier GetSession = new ActionIdentifier("appstream:GetSession");
        public static readonly ActionIdentifier GetSessions = new ActionIdentifier("appstream:GetSessions");
        public static readonly ActionIdentifier GetSessionStatus = new ActionIdentifier("appstream:GetSessionStatus");
        public static readonly ActionIdentifier UpdateApplication = new ActionIdentifier("appstream:UpdateApplication");
        public static readonly ActionIdentifier UpdateApplicationState = new ActionIdentifier("appstream:UpdateApplicationState");
        public static readonly ActionIdentifier UpdateSessionState = new ActionIdentifier("appstream:UpdateSessionState");
    }
}
