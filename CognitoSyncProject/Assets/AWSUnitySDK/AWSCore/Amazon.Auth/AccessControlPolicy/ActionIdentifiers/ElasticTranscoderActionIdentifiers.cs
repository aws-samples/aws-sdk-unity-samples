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
    /// The available AWS access control policy actions for Amazon Elastic Transcoder.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class ElasticTranscoderActionIdentifiers
    {
        public static readonly ActionIdentifier AllElasticTranscoderActions = new ActionIdentifier("elastictranscoder:*");

        public static readonly ActionIdentifier CancelJob = new ActionIdentifier("elastictranscoder:CancelJob");
        public static readonly ActionIdentifier CreateJob = new ActionIdentifier("elastictranscoder:CreateJob");
        public static readonly ActionIdentifier CreatePipeline = new ActionIdentifier("elastictranscoder:CreatePipeline");
        public static readonly ActionIdentifier CreatePreset = new ActionIdentifier("elastictranscoder:CreatePreset");
        public static readonly ActionIdentifier DeletePipeline = new ActionIdentifier("elastictranscoder:DeletePipeline");
        public static readonly ActionIdentifier DeletePreset = new ActionIdentifier("elastictranscoder:DeletePreset");
        public static readonly ActionIdentifier ListJobsByPipeline = new ActionIdentifier("elastictranscoder:ListJobsByPipeline");
        public static readonly ActionIdentifier ListJobsByStatus = new ActionIdentifier("elastictranscoder:ListJobsByStatus");
        public static readonly ActionIdentifier ListPipelines = new ActionIdentifier("elastictranscoder:ListPipelines");
        public static readonly ActionIdentifier ListPresets = new ActionIdentifier("elastictranscoder:ListPresets");
        public static readonly ActionIdentifier ReadJob = new ActionIdentifier("elastictranscoder:ReadJob");
        public static readonly ActionIdentifier ReadPipeline = new ActionIdentifier("elastictranscoder:ReadPipeline");
        public static readonly ActionIdentifier ReadPreset = new ActionIdentifier("elastictranscoder:ReadPreset");
        public static readonly ActionIdentifier TestRole = new ActionIdentifier("elastictranscoder:TestRole");
        public static readonly ActionIdentifier UpdatePipeline = new ActionIdentifier("elastictranscoder:UpdatePipeline");
        public static readonly ActionIdentifier UpdatePipelineNotifications = new ActionIdentifier("elastictranscoder:UpdatePipelineNotifications");
        public static readonly ActionIdentifier UpdatePipelineStatus = new ActionIdentifier("elastictranscoder:UpdatePipelineStatus");
    }
}
