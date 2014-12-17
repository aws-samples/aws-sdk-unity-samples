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

namespace Amazon.CognitoSync.Model
{
    /// <summary>
    /// Configuration for accessing Amazon DescribeIdentityPoolUsage service
    /// </summary>
    public partial class DescribeIdentityPoolUsageResponse : DescribeIdentityPoolUsageResult
    {
        /// <summary>
        /// Gets and sets the DescribeIdentityPoolUsageResult property.
        /// Represents the output of a DescribeIdentityPoolUsage operation.
        /// </summary>
        [Obsolete(@"This property has been deprecated. All properties of the DescribeIdentityPoolUsageResult class are now available on the DescribeIdentityPoolUsageResponse class. You should use the properties on DescribeIdentityPoolUsageResponse instead of accessing them through DescribeIdentityPoolUsageResult.")]
        public DescribeIdentityPoolUsageResult DescribeIdentityPoolUsageResult
        {
            get
            {
                return this;
            }
        }
    }
}