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
    /// The available AWS access control policy actions for Amazon SQS.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class SQSActionIdentifiers
    {
        public static readonly ActionIdentifier AllSQSActions = new ActionIdentifier("sqs:*");

        public static readonly ActionIdentifier AddPermission = new ActionIdentifier("sqs:AddPermission");
        public static readonly ActionIdentifier ChangeMessageVisibility = new ActionIdentifier("sqs:ChangeMessageVisibility");
        public static readonly ActionIdentifier CreateQueue = new ActionIdentifier("sqs:CreateQueue");
        public static readonly ActionIdentifier DeleteMessage = new ActionIdentifier("sqs:DeleteMessage");
        public static readonly ActionIdentifier DeleteQueue = new ActionIdentifier("sqs:DeleteQueue");
        public static readonly ActionIdentifier GetQueueAttributes = new ActionIdentifier("sqs:GetQueueAttributes");
        public static readonly ActionIdentifier GetQueueUrl = new ActionIdentifier("sqs:GetQueueUrl");
        public static readonly ActionIdentifier ListQueues = new ActionIdentifier("sqs:ListQueues");
        public static readonly ActionIdentifier ReceiveMessage = new ActionIdentifier("sqs:ReceiveMessage");
        public static readonly ActionIdentifier RemovePermission = new ActionIdentifier("sqs:RemovePermission");
        public static readonly ActionIdentifier SendMessage = new ActionIdentifier("sqs:SendMessage");
        public static readonly ActionIdentifier SetQueueAttributes = new ActionIdentifier("sqs:SetQueueAttributes");
    }
}
