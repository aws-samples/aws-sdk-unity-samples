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
    /// The available AWS access control policy actions for AWS Billing.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class BillingActionIdentifiers
    {
        public static readonly ActionIdentifier AllBillingActions = new ActionIdentifier("aws-portal:*");

        public static readonly ActionIdentifier ModifyAccount = new ActionIdentifier("aws-portal:ModifyAccount");
        public static readonly ActionIdentifier ModifyBilling = new ActionIdentifier("aws-portal:ModifyBilling");
        public static readonly ActionIdentifier ModifyPaymentMethods = new ActionIdentifier("aws-portal:ModifyPaymentMethods");
        public static readonly ActionIdentifier ViewAccount = new ActionIdentifier("aws-portal:ViewAccount");
        public static readonly ActionIdentifier ViewBilling = new ActionIdentifier("aws-portal:ViewBilling");
        public static readonly ActionIdentifier ViewPaymentMethods = new ActionIdentifier("aws-portal:ViewPaymentMethods");
        public static readonly ActionIdentifier ViewUsage = new ActionIdentifier("aws-portal:ViewUsage");
    }
}
