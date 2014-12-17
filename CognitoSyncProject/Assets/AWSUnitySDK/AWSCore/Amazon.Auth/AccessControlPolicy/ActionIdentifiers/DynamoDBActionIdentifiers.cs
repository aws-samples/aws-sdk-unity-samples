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
    /// The available AWS access control policy actions for Amazon DynamoDB.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class DynamoDBActionIdentifiers
    {
        public static readonly ActionIdentifier AllDynamoDBActions = new ActionIdentifier("dynamodb:*");

        public static readonly ActionIdentifier BatchGetItem = new ActionIdentifier("dynamodb:BatchGetItem");
        public static readonly ActionIdentifier BatchWriteItem = new ActionIdentifier("dynamodb:BatchWriteItem");
        public static readonly ActionIdentifier CreateTable = new ActionIdentifier("dynamodb:CreateTable");
        public static readonly ActionIdentifier DeleteItem = new ActionIdentifier("dynamodb:DeleteItem");
        public static readonly ActionIdentifier DeleteTable = new ActionIdentifier("dynamodb:DeleteTable");
        public static readonly ActionIdentifier DescribeTable = new ActionIdentifier("dynamodb:DescribeTable");
        public static readonly ActionIdentifier GetItem = new ActionIdentifier("dynamodb:GetItem");
        public static readonly ActionIdentifier ListTables = new ActionIdentifier("dynamodb:ListTables");
        public static readonly ActionIdentifier PutItem = new ActionIdentifier("dynamodb:PutItem");
        public static readonly ActionIdentifier Query = new ActionIdentifier("dynamodb:Query");
        public static readonly ActionIdentifier Scan = new ActionIdentifier("dynamodb:Scan");
        public static readonly ActionIdentifier UpdateItem = new ActionIdentifier("dynamodb:UpdateItem");
        public static readonly ActionIdentifier UpdateTable = new ActionIdentifier("dynamodb:UpdateTable");
    }
}
