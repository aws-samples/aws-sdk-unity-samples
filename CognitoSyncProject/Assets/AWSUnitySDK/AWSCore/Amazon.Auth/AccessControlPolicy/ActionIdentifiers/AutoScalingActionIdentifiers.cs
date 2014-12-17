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
    /// The available AWS access control policy actions for Auto Scaling.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class AutoScalingActionIdentifiers
    {
        public static readonly ActionIdentifier AllAutoScalingActions = new ActionIdentifier("autoscaling:*");

        public static readonly ActionIdentifier CreateAutoScalingGroup = new ActionIdentifier("autoscaling:CreateAutoScalingGroup");
        public static readonly ActionIdentifier CreateLaunchConfiguration = new ActionIdentifier("autoscaling:CreateLaunchConfiguration");
        public static readonly ActionIdentifier CreateOrUpdateScalingTrigger = new ActionIdentifier("autoscaling:CreateOrUpdateScalingTrigger");
        public static readonly ActionIdentifier CreateOrUpdateTags = new ActionIdentifier("autoscaling:CreateOrUpdateTags");
        public static readonly ActionIdentifier DeleteAutoScalingGroup = new ActionIdentifier("autoscaling:DeleteAutoScalingGroup");
        public static readonly ActionIdentifier DeleteLaunchConfiguration = new ActionIdentifier("autoscaling:DeleteLaunchConfiguration");
        public static readonly ActionIdentifier DeleteNotificationConfiguration = new ActionIdentifier("autoscaling:DeleteNotificationConfiguration");
        public static readonly ActionIdentifier DeletePolicy = new ActionIdentifier("autoscaling:DeletePolicy");
        public static readonly ActionIdentifier DeleteScheduledAction = new ActionIdentifier("autoscaling:DeleteScheduledAction");
        public static readonly ActionIdentifier DeleteTags = new ActionIdentifier("autoscaling:DeleteTags");
        public static readonly ActionIdentifier DeleteTrigger = new ActionIdentifier("autoscaling:DeleteTrigger");
        public static readonly ActionIdentifier DescribeAdjustmentTypes = new ActionIdentifier("autoscaling:DescribeAdjustmentTypes");
        public static readonly ActionIdentifier DescribeAutoScalingGroups = new ActionIdentifier("autoscaling:DescribeAutoScalingGroups");
        public static readonly ActionIdentifier DescribeAutoScalingInstances = new ActionIdentifier("autoscaling:DescribeAutoScalingInstances");
        public static readonly ActionIdentifier DescribeAutoScalingNotificationTypes = new ActionIdentifier("autoscaling:DescribeAutoScalingNotificationTypes");
        public static readonly ActionIdentifier DescribeLaunchConfigurations = new ActionIdentifier("autoscaling:DescribeLaunchConfigurations");
        public static readonly ActionIdentifier DescribeMetricCollectionTypes = new ActionIdentifier("autoscaling:DescribeMetricCollectionTypes");
        public static readonly ActionIdentifier DescribeNotificationConfigurations = new ActionIdentifier("autoscaling:DescribeNotificationConfigurations");
        public static readonly ActionIdentifier DescribePolicies = new ActionIdentifier("autoscaling:DescribePolicies");
        public static readonly ActionIdentifier DescribeScalingActivities = new ActionIdentifier("autoscaling:DescribeScalingActivities");
        public static readonly ActionIdentifier DescribeScalingProcessTypes = new ActionIdentifier("autoscaling:DescribeScalingProcessTypes");
        public static readonly ActionIdentifier DescribeScheduledActions = new ActionIdentifier("autoscaling:DescribeScheduledActions");
        public static readonly ActionIdentifier DescribeTags = new ActionIdentifier("autoscaling:DescribeTags");
        public static readonly ActionIdentifier DescribeTriggers = new ActionIdentifier("autoscaling:DescribeTriggers");
        public static readonly ActionIdentifier DisableMetricsCollection = new ActionIdentifier("autoscaling:DisableMetricsCollection");
        public static readonly ActionIdentifier EnableMetricsCollection = new ActionIdentifier("autoscaling:EnableMetricsCollection");
        public static readonly ActionIdentifier ExecutePolicy = new ActionIdentifier("autoscaling:ExecutePolicy");
        public static readonly ActionIdentifier PutNotificationConfiguration = new ActionIdentifier("autoscaling:PutNotificationConfiguration");
        public static readonly ActionIdentifier PutScalingPolicy = new ActionIdentifier("autoscaling:PutScalingPolicy");
        public static readonly ActionIdentifier PutScheduledUpdateGroupAction = new ActionIdentifier("autoscaling:PutScheduledUpdateGroupAction");
        public static readonly ActionIdentifier ResumeProcesses = new ActionIdentifier("autoscaling:ResumeProcesses");
        public static readonly ActionIdentifier SetDesiredCapacity = new ActionIdentifier("autoscaling:SetDesiredCapacity");
        public static readonly ActionIdentifier SetInstanceHealth = new ActionIdentifier("autoscaling:SetInstanceHealth");
        public static readonly ActionIdentifier SuspendProcesses = new ActionIdentifier("autoscaling:SuspendProcesses");
        public static readonly ActionIdentifier TerminateInstanceInAutoScalingGroup = new ActionIdentifier("autoscaling:TerminateInstanceInAutoScalingGroup");
        public static readonly ActionIdentifier UpdateAutoScalingGroup = new ActionIdentifier("autoscaling:UpdateAutoScalingGroup");
    }
}
