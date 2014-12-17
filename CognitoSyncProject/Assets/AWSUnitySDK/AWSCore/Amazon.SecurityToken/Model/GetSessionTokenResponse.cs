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
    /// Configuration for accessing Amazon GetSessionToken service
    /// </summary>
    public partial class GetSessionTokenResponse : GetSessionTokenResult
    {
        /// <summary>
        /// Gets and sets the GetSessionTokenResult property.
        /// Represents the output of a GetSessionToken operation.
        /// </summary>
        [Obsolete(@"This property has been deprecated. All properties of the GetSessionTokenResult class are now available on the GetSessionTokenResponse class. You should use the properties on GetSessionTokenResponse instead of accessing them through GetSessionTokenResult.")]
        public GetSessionTokenResult GetSessionTokenResult
        {
            get
            {
                return this;
            }
        }
    }
}