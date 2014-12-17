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
    /// The available AWS access control policy actions for Elastic Load Balancing.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class ElasticLoadBalancingActionIdentifiers
    {
        public static readonly ActionIdentifier AllElasticLoadBalancingActions = new ActionIdentifier("elasticloadbalancing:*");

        public static readonly ActionIdentifier ApplySecurityGroupsToLoadBalancer = new ActionIdentifier("elasticloadbalancing:ApplySecurityGroupsToLoadBalancer");
        public static readonly ActionIdentifier AttachLoadBalancerToSubnets = new ActionIdentifier("elasticloadbalancing:AttachLoadBalancerToSubnets");
        public static readonly ActionIdentifier ConfigureHealthCheck = new ActionIdentifier("elasticloadbalancing:ConfigureHealthCheck");
        public static readonly ActionIdentifier CreateAppCookieStickinessPolicy = new ActionIdentifier("elasticloadbalancing:CreateAppCookieStickinessPolicy");
        public static readonly ActionIdentifier CreateLBCookieStickinessPolicy = new ActionIdentifier("elasticloadbalancing:CreateLBCookieStickinessPolicy");
        public static readonly ActionIdentifier CreateLoadBalancer = new ActionIdentifier("elasticloadbalancing:CreateLoadBalancer");
        public static readonly ActionIdentifier CreateLoadBalancerListeners = new ActionIdentifier("elasticloadbalancing:CreateLoadBalancerListeners");
        public static readonly ActionIdentifier CreateLoadBalancerPolicy = new ActionIdentifier("elasticloadbalancing:CreateLoadBalancerPolicy");
        public static readonly ActionIdentifier DeleteLoadBalancer = new ActionIdentifier("elasticloadbalancing:DeleteLoadBalancer");
        public static readonly ActionIdentifier DeleteLoadBalancerListeners = new ActionIdentifier("elasticloadbalancing:DeleteLoadBalancerListeners");
        public static readonly ActionIdentifier DeleteLoadBalancerPolicy = new ActionIdentifier("elasticloadbalancing:DeleteLoadBalancerPolicy");
        public static readonly ActionIdentifier DeregisterInstancesFromLoadBalancer = new ActionIdentifier("elasticloadbalancing:DeregisterInstancesFromLoadBalancer");
        public static readonly ActionIdentifier DescribeInstanceHealth = new ActionIdentifier("elasticloadbalancing:DescribeInstanceHealth");
        public static readonly ActionIdentifier DescribeLoadBalancerAttributes = new ActionIdentifier("elasticloadbalancing:DescribeLoadBalancerAttributes");
        public static readonly ActionIdentifier DescribeLoadBalancerPolicyTypes = new ActionIdentifier("elasticloadbalancing:DescribeLoadBalancerPolicyTypes");
        public static readonly ActionIdentifier DescribeLoadBalancerPolicies = new ActionIdentifier("elasticloadbalancing:DescribeLoadBalancerPolicies");
        public static readonly ActionIdentifier DescribeLoadBalancers = new ActionIdentifier("elasticloadbalancing:DescribeLoadBalancers");
        public static readonly ActionIdentifier DetachLoadBalancerFromSubnets = new ActionIdentifier("elasticloadbalancing:DetachLoadBalancerFromSubnets");
        public static readonly ActionIdentifier DisableAvailabilityZonesForLoadBalancer = new ActionIdentifier("elasticloadbalancing:DisableAvailabilityZonesForLoadBalancer");
        public static readonly ActionIdentifier EnableAvailabilityZonesForLoadBalancer = new ActionIdentifier("elasticloadbalancing:EnableAvailabilityZonesForLoadBalancer");
        public static readonly ActionIdentifier ModifyLoadBalancerAttributes = new ActionIdentifier("elasticloadbalancing:ModifyLoadBalancerAttributes");
        public static readonly ActionIdentifier RegisterInstancesWithLoadBalancer = new ActionIdentifier("elasticloadbalancing:RegisterInstancesWithLoadBalancer");
        public static readonly ActionIdentifier SetLoadBalancerListenerSSLCertificate = new ActionIdentifier("elasticloadbalancing:SetLoadBalancerListenerSSLCertificate");
        public static readonly ActionIdentifier SetLoadBalancerPoliciesForBackendServer = new ActionIdentifier("elasticloadbalancing:SetLoadBalancerPoliciesForBackendServer");
        public static readonly ActionIdentifier SetLoadBalancerPoliciesOfListener = new ActionIdentifier("elasticloadbalancing:SetLoadBalancerPoliciesOfListener");
    }
}
