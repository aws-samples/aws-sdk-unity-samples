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

namespace Amazon.CognitoIdentity.Model
{
    /// <summary>
    /// Configuration for accessing Amazon DescribeIdentityPool service
    /// </summary>
    public partial class DescribeIdentityPoolResponse : DescribeIdentityPoolResult
    {
        /// <summary>
        /// Gets and sets the DescribeIdentityPoolResult property.
        /// Represents the output of a DescribeIdentityPool operation.
        /// </summary>
        [Obsolete(@"This property has been deprecated. All properties of the DescribeIdentityPoolResult class are now available on the DescribeIdentityPoolResponse class. You should use the properties on DescribeIdentityPoolResponse instead of accessing them through DescribeIdentityPoolResult.")]
        public DescribeIdentityPoolResult DescribeIdentityPoolResult
        {
            get
            {
                return this;
            }
        }
    }
}