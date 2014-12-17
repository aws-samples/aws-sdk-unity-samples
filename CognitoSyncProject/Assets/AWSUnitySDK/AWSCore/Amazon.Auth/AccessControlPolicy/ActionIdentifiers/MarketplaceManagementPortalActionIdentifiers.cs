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
    /// The available AWS access control policy actions for AWS Marketplace Management Portal.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class MarketplaceManagementPortalActionIdentifiers
    {
        public static readonly ActionIdentifier AllMarketplaceManagementPortalActions = new ActionIdentifier("aws-marketplace-management:*");

        public static readonly ActionIdentifier uploadFiles = new ActionIdentifier("aws-marketplace-management:uploadFiles");
        public static readonly ActionIdentifier viewMarketing = new ActionIdentifier("aws-marketplace-management:viewMarketing");
        public static readonly ActionIdentifier viewReports = new ActionIdentifier("aws-marketplace-management:viewReports");
        public static readonly ActionIdentifier viewSupport = new ActionIdentifier("aws-marketplace-management:viewSupport");
    }
}
