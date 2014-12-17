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
    /// The available AWS access control policy actions for Amazon Mobile Analytics.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class MobileAnalyticsActionIdentifiers
    {
        public static readonly ActionIdentifier AllMobileAnalyticsActions = new ActionIdentifier("mobileanalytics:*");

        public static readonly ActionIdentifier PutEvents = new ActionIdentifier("mobileanalytics:PutEvents");
        public static readonly ActionIdentifier GetReports = new ActionIdentifier("mobileanalytics:GetReports");
        public static readonly ActionIdentifier GetFinancialReports = new ActionIdentifier("mobileanalytics:GetFinancialReports");
    }
}
