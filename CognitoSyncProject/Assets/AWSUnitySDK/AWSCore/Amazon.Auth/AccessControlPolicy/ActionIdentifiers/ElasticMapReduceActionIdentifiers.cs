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
    /// The available AWS access control policy actions for Amazon Elastic MapReduce.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class ElasticMapReduceActionIdentifiers
    {
        public static readonly ActionIdentifier AllElasticMapReduceActions = new ActionIdentifier("elasticmapreduce:*");

        public static readonly ActionIdentifier AddInstanceGroups = new ActionIdentifier("elasticmapreduce:AddInstanceGroups");
        public static readonly ActionIdentifier AddTags = new ActionIdentifier("elasticmapreduce:AddTags");
        public static readonly ActionIdentifier AddJobFlowSteps = new ActionIdentifier("elasticmapreduce:AddJobFlowSteps");
        public static readonly ActionIdentifier DescribeCluster = new ActionIdentifier("elasticmapreduce:DescribeCluster");
        public static readonly ActionIdentifier DescribeJobFlows = new ActionIdentifier("elasticmapreduce:DescribeJobFlows");
        public static readonly ActionIdentifier DescribeStep = new ActionIdentifier("elasticmapreduce:DescribeStep");
        public static readonly ActionIdentifier ListBootstrapActions = new ActionIdentifier("elasticmapreduce:ListBootstrapActions");
        public static readonly ActionIdentifier ListClusters = new ActionIdentifier("elasticmapreduce:ListClusters");
        public static readonly ActionIdentifier ListInstanceGroups = new ActionIdentifier("elasticmapreduce:ListInstanceGroups");
        public static readonly ActionIdentifier ListInstances = new ActionIdentifier("elasticmapreduce:ListInstances");
        public static readonly ActionIdentifier ListSteps = new ActionIdentifier("elasticmapreduce:ListSteps");
        public static readonly ActionIdentifier ModifyInstanceGroups = new ActionIdentifier("elasticmapreduce:ModifyInstanceGroups");
        public static readonly ActionIdentifier RemoveTags = new ActionIdentifier("elasticmapreduce:RemoveTags");
        public static readonly ActionIdentifier RunJobFlow = new ActionIdentifier("elasticmapreduce:RunJobFlow");
        public static readonly ActionIdentifier SetTerminationProtection = new ActionIdentifier("elasticmapreduce:SetTerminationProtection");
        public static readonly ActionIdentifier TerminateJobFlows = new ActionIdentifier("elasticmapreduce:TerminateJobFlows");
    }
}
