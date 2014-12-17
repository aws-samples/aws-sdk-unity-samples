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
    /// The available AWS access control policy actions for Amazon SES.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class SESActionIdentifiers
    {
        public static readonly ActionIdentifier AllSESActions = new ActionIdentifier("ses:*");

        public static readonly ActionIdentifier DeleteIdentity = new ActionIdentifier("ses:DeleteIdentity");
        public static readonly ActionIdentifier DeleteVerifiedEmailAddress = new ActionIdentifier("ses:DeleteVerifiedEmailAddress");
        public static readonly ActionIdentifier GetIdentityDkimAttributes = new ActionIdentifier("ses:GetIdentityDkimAttributes");
        public static readonly ActionIdentifier GetIdentityNotificationAttributes = new ActionIdentifier("ses:GetIdentityNotificationAttributes");
        public static readonly ActionIdentifier GetIdentityVerificationAttributes = new ActionIdentifier("ses:GetIdentityVerificationAttributes");
        public static readonly ActionIdentifier GetSendQuota = new ActionIdentifier("ses:GetSendQuota");
        public static readonly ActionIdentifier GetSendStatistics = new ActionIdentifier("ses:GetSendStatistics");
        public static readonly ActionIdentifier ListIdentities = new ActionIdentifier("ses:ListIdentities");
        public static readonly ActionIdentifier ListVerifiedEmailAddresses = new ActionIdentifier("ses:ListVerifiedEmailAddresses");
        public static readonly ActionIdentifier SendEmail = new ActionIdentifier("ses:SendEmail");
        public static readonly ActionIdentifier SendRawEmail = new ActionIdentifier("ses:SendRawEmail");
        public static readonly ActionIdentifier SetIdentityDkimEnabled = new ActionIdentifier("ses:SetIdentityDkimEnabled");
        public static readonly ActionIdentifier SetIdentityNotificationTopic = new ActionIdentifier("ses:SetIdentityNotificationTopic");
        public static readonly ActionIdentifier SetIdentityFeedbackForwardingEnabled = new ActionIdentifier("ses:SetIdentityFeedbackForwardingEnabled");
        public static readonly ActionIdentifier VerifyDomainDkim = new ActionIdentifier("ses:VerifyDomainDkim");
        public static readonly ActionIdentifier VerifyDomainIdentity = new ActionIdentifier("ses:VerifyDomainIdentity");
        public static readonly ActionIdentifier VerifyEmailAddress = new ActionIdentifier("ses:VerifyEmailAddress");
        public static readonly ActionIdentifier VerifyEmailIdentity = new ActionIdentifier("ses:VerifyEmailIdentity");
    }
}
