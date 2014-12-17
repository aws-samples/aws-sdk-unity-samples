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
    /// The available AWS access control policy actions for AWS Direct Connect.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class DirectConnectActionIdentifiers
    {
        public static readonly ActionIdentifier AllDirectConnectActions = new ActionIdentifier("directconnect:*");

        public static readonly ActionIdentifier CreateConnection = new ActionIdentifier("directconnect:CreateConnection");
        public static readonly ActionIdentifier CreatePrivateVirtualInterface = new ActionIdentifier("directconnect:CreatePrivateVirtualInterface");
        public static readonly ActionIdentifier CreatePublicVirtualInterface = new ActionIdentifier("directconnect:CreatePublicVirtualInterface");
        public static readonly ActionIdentifier DeleteConnection = new ActionIdentifier("directconnect:DeleteConnection");
        public static readonly ActionIdentifier DeleteVirtualInterface = new ActionIdentifier("directconnect:DeleteVirtualInterface");
        public static readonly ActionIdentifier DescribeConnectionDetail = new ActionIdentifier("directconnect:DescribeConnectionDetail");
        public static readonly ActionIdentifier DescribeConnections = new ActionIdentifier("directconnect:DescribeConnections");
        public static readonly ActionIdentifier DescribeOfferingDetail = new ActionIdentifier("directconnect:DescribeOfferingDetail");
        public static readonly ActionIdentifier DescribeOfferings = new ActionIdentifier("directconnect:DescribeOfferings");
        public static readonly ActionIdentifier DescribeVirtualGateways = new ActionIdentifier("directconnect:DescribeVirtualGateways");
        public static readonly ActionIdentifier DescribeVirtualInterfaces = new ActionIdentifier("directconnect:DescribeVirtualInterfaces");
    }
}
