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
    /// The available AWS access control policy actions for AWS ElastiCache.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class ElastiCacheActionIdentifiers
    {
        public static readonly ActionIdentifier AllElastiCacheActions = new ActionIdentifier("elasticache:*");

        public static readonly ActionIdentifier AuthorizeCacheSecurityGroupIngress = new ActionIdentifier("elasticache:AuthorizeCacheSecurityGroupIngress");
        public static readonly ActionIdentifier CreateCacheCluster = new ActionIdentifier("elasticache:CreateCacheCluster");
        public static readonly ActionIdentifier CreateCacheParameterGroup = new ActionIdentifier("elasticache:CreateCacheParameterGroup");
        public static readonly ActionIdentifier CreateCacheSecurityGroup = new ActionIdentifier("elasticache:CreateCacheSecurityGroup");
        public static readonly ActionIdentifier DeleteCacheCluster = new ActionIdentifier("elasticache:DeleteCacheCluster");
        public static readonly ActionIdentifier DeleteCacheParameterGroup = new ActionIdentifier("elasticache:DeleteCacheParameterGroup");
        public static readonly ActionIdentifier DeleteCacheSecurityGroup = new ActionIdentifier("elasticache:DeleteCacheSecurityGroup");
        public static readonly ActionIdentifier DescribeCacheClusters = new ActionIdentifier("elasticache:DescribeCacheClusters");
        public static readonly ActionIdentifier DescribeCacheParameterGroups = new ActionIdentifier("elasticache:DescribeCacheParameterGroups");
        public static readonly ActionIdentifier DescribeCacheParameters = new ActionIdentifier("elasticache:DescribeCacheParameters");
        public static readonly ActionIdentifier DescribeCacheSecurityGroups = new ActionIdentifier("elasticache:DescribeCacheSecurityGroups");
        public static readonly ActionIdentifier DescribeEngineDefaultParameters = new ActionIdentifier("elasticache:DescribeEngineDefaultParameters");
        public static readonly ActionIdentifier DescribeEvents = new ActionIdentifier("elasticache:DescribeEvents");
        public static readonly ActionIdentifier ModifyCacheCluster = new ActionIdentifier("elasticache:ModifyCacheCluster");
        public static readonly ActionIdentifier ModifyCacheParameterGroup = new ActionIdentifier("elasticache:ModifyCacheParameterGroup");
        public static readonly ActionIdentifier RebootCacheCluster = new ActionIdentifier("elasticache:RebootCacheCluster");
        public static readonly ActionIdentifier ResetCacheParameterGroup = new ActionIdentifier("elasticache:ResetCacheParameterGroup");
        public static readonly ActionIdentifier RevokeCacheSecurityGroupIngress = new ActionIdentifier("elasticache:RevokeCacheSecurityGroupIngress");
    }
}
