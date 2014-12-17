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
    /// Configuration for accessing Amazon GetFederationToken service
    /// </summary>
    public partial class GetFederationTokenResponse : GetFederationTokenResult
    {
        /// <summary>
        /// Gets and sets the GetFederationTokenResult property.
        /// Represents the output of a GetFederationToken operation.
        /// </summary>
        [Obsolete(@"This property has been deprecated. All properties of the GetFederationTokenResult class are now available on the GetFederationTokenResponse class. You should use the properties on GetFederationTokenResponse instead of accessing them through GetFederationTokenResult.")]
        public GetFederationTokenResult GetFederationTokenResult
        {
            get
            {
                return this;
            }
        }
    }
}