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
    /// Configuration for accessing Amazon DescribeIdentityUsage service
    /// </summary>
    public partial class DescribeIdentityUsageResponse : DescribeIdentityUsageResult
    {
        /// <summary>
        /// Gets and sets the DescribeIdentityUsageResult property.
        /// Represents the output of a DescribeIdentityUsage operation.
        /// </summary>
        [Obsolete(@"This property has been deprecated. All properties of the DescribeIdentityUsageResult class are now available on the DescribeIdentityUsageResponse class. You should use the properties on DescribeIdentityUsageResponse instead of accessing them through DescribeIdentityUsageResult.")]
        public DescribeIdentityUsageResult DescribeIdentityUsageResult
        {
            get
            {
                return this;
            }
        }
    }
}