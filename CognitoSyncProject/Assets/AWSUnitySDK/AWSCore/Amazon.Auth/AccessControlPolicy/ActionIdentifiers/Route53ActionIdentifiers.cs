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
    /// The available AWS access control policy actions for Amazon Route 53.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class Route53ActionIdentifiers
    {
        public static readonly ActionIdentifier AllRoute53Actions = new ActionIdentifier("route53:*");

        public static readonly ActionIdentifier ChangeResourceRecordSets = new ActionIdentifier("route53:ChangeResourceRecordSets");
        public static readonly ActionIdentifier CreateHostedZone = new ActionIdentifier("route53:CreateHostedZone");
        public static readonly ActionIdentifier DeleteHostedZone = new ActionIdentifier("route53:DeleteHostedZone");
        public static readonly ActionIdentifier GetChange = new ActionIdentifier("route53:GetChange");
        public static readonly ActionIdentifier GetHostedZone = new ActionIdentifier("route53:GetHostedZone");
        public static readonly ActionIdentifier ListHostedZones = new ActionIdentifier("route53:ListHostedZones");
        public static readonly ActionIdentifier ListResourceRecordSets = new ActionIdentifier("route53:ListResourceRecordSets");
    }
}
