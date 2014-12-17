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
    /// The available AWS access control policy actions for Amazon CloudWatch.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class CloudWatchActionIdentifiers
    {
        public static readonly ActionIdentifier AllCloudWatchActions = new ActionIdentifier("cloudwatch:*");

        public static readonly ActionIdentifier DeleteAlarms = new ActionIdentifier("cloudwatch:DeleteAlarms");
        public static readonly ActionIdentifier DescribeAlarmHistory = new ActionIdentifier("cloudwatch:DescribeAlarmHistory");
        public static readonly ActionIdentifier DescribeAlarms = new ActionIdentifier("cloudwatch:DescribeAlarms");
        public static readonly ActionIdentifier DescribeAlarmsForMetric = new ActionIdentifier("cloudwatch:DescribeAlarmsForMetric");
        public static readonly ActionIdentifier DisableAlarmActions = new ActionIdentifier("cloudwatch:DisableAlarmActions");
        public static readonly ActionIdentifier EnableAlarmActions = new ActionIdentifier("cloudwatch:EnableAlarmActions");
        public static readonly ActionIdentifier GetMetricStatistics = new ActionIdentifier("cloudwatch:GetMetricStatistics");
        public static readonly ActionIdentifier ListMetrics = new ActionIdentifier("cloudwatch:ListMetrics");
        public static readonly ActionIdentifier PutMetricAlarm = new ActionIdentifier("cloudwatch:PutMetricAlarm");
        public static readonly ActionIdentifier PutMetricData = new ActionIdentifier("cloudwatch:PutMetricData");
        public static readonly ActionIdentifier SetAlarmState = new ActionIdentifier("cloudwatch:SetAlarmState");
    }
}
