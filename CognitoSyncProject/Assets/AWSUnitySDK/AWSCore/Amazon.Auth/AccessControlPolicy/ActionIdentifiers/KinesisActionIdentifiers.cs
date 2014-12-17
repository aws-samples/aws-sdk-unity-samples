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
    /// The available AWS access control policy actions for Amazon Kinesis.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class KinesisActionIdentifiers
    {
        public static readonly ActionIdentifier AllKinesisActions = new ActionIdentifier("kinesis:*");

        public static readonly ActionIdentifier CreateStream = new ActionIdentifier("kinesis:CreateStream");
        public static readonly ActionIdentifier DeleteStream = new ActionIdentifier("kinesis:DeleteStream");
        public static readonly ActionIdentifier DescribeStream = new ActionIdentifier("kinesis:DescribeStream");
        public static readonly ActionIdentifier ListStreams = new ActionIdentifier("kinesis:ListStreams");
        public static readonly ActionIdentifier PutRecord = new ActionIdentifier("kinesis:PutRecord");
        public static readonly ActionIdentifier GetShardIterator = new ActionIdentifier("kinesis:GetShardIterator");
        public static readonly ActionIdentifier GetRecords = new ActionIdentifier("kinesis:GetRecords");
        public static readonly ActionIdentifier MergeShards = new ActionIdentifier("kinesis:MergeShards");
        public static readonly ActionIdentifier SplitShard = new ActionIdentifier("kinesis:SplitShard");
    }
}
