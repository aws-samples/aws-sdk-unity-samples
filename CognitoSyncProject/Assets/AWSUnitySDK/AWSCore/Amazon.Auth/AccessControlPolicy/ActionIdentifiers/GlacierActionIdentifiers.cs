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
    /// The available AWS access control policy actions for Amazon Glacier.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class GlacierActionIdentifiers
    {
        public static readonly ActionIdentifier AllGlacierActions = new ActionIdentifier("glacier:*");

        public static readonly ActionIdentifier AbortMultipartUpload = new ActionIdentifier("glacier:AbortMultipartUpload");
        public static readonly ActionIdentifier CompleteMultipartUpload = new ActionIdentifier("glacier:CompleteMultipartUpload");
        public static readonly ActionIdentifier CreateVault = new ActionIdentifier("glacier:CreateVault");
        public static readonly ActionIdentifier DeleteArchive = new ActionIdentifier("glacier:DeleteArchive");
        public static readonly ActionIdentifier DeleteVault = new ActionIdentifier("glacier:DeleteVault");
        public static readonly ActionIdentifier DeleteVaultNotifications = new ActionIdentifier("glacier:DeleteVaultNotifications");
        public static readonly ActionIdentifier DescribeJob = new ActionIdentifier("glacier:DescribeJob");
        public static readonly ActionIdentifier DescribeVault = new ActionIdentifier("glacier:DescribeVault");
        public static readonly ActionIdentifier GetJobOutput = new ActionIdentifier("glacier:GetJobOutput");
        public static readonly ActionIdentifier GetVaultNotifications = new ActionIdentifier("glacier:GetVaultNotifications");
        public static readonly ActionIdentifier InitiateMultipartUpload = new ActionIdentifier("glacier:InitiateMultipartUpload");
        public static readonly ActionIdentifier InitiateJob = new ActionIdentifier("glacier:InitiateJob");
        public static readonly ActionIdentifier ListJobs = new ActionIdentifier("glacier:ListJobs");
        public static readonly ActionIdentifier ListMultipartUploads = new ActionIdentifier("glacier:ListMultipartUploads");
        public static readonly ActionIdentifier ListParts = new ActionIdentifier("glacier:ListParts");
        public static readonly ActionIdentifier ListVaults = new ActionIdentifier("glacier:ListVaults");
        public static readonly ActionIdentifier SetVaultNotifications = new ActionIdentifier("glacier:SetVaultNotifications");
        public static readonly ActionIdentifier UploadArchive = new ActionIdentifier("glacier:UploadArchive");
        public static readonly ActionIdentifier UploadMultipartPart = new ActionIdentifier("glacier:UploadMultipartPart");
    }
}
