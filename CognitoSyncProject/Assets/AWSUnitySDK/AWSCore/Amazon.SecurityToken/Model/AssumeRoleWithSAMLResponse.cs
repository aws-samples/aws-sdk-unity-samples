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

namespace Amazon.SecurityToken.Model
{
    /// <summary>
    /// Configuration for accessing Amazon AssumeRoleWithSAML service
    /// </summary>
    public partial class AssumeRoleWithSAMLResponse : AssumeRoleWithSAMLResult
    {
        /// <summary>
        /// Gets and sets the AssumeRoleWithSAMLResult property.
        /// Represents the output of a AssumeRoleWithSAML operation.
        /// </summary>
        [Obsolete(@"This property has been deprecated. All properties of the AssumeRoleWithSAMLResult class are now available on the AssumeRoleWithSAMLResponse class. You should use the properties on AssumeRoleWithSAMLResponse instead of accessing them through AssumeRoleWithSAMLResult.")]
        public AssumeRoleWithSAMLResult AssumeRoleWithSAMLResult
        {
            get
            {
                return this;
            }
        }
    }
}