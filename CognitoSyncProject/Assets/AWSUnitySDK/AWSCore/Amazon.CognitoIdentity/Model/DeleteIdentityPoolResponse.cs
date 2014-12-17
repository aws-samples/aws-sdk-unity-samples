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
    /// Configuration for accessing Amazon DeleteIdentityPool service
    /// </summary>
    public partial class DeleteIdentityPoolResponse : DeleteIdentityPoolResult
    {
        /// <summary>
        /// Gets and sets the DeleteIdentityPoolResult property.
        /// Represents the output of a DeleteIdentityPool operation.
        /// </summary>
        [Obsolete(@"This property has been deprecated. All properties of the DeleteIdentityPoolResult class are now available on the DeleteIdentityPoolResponse class. You should use the properties on DeleteIdentityPoolResponse instead of accessing them through DeleteIdentityPoolResult.")]
        public DeleteIdentityPoolResult DeleteIdentityPoolResult
        {
            get
            {
                return this;
            }
        }
    }
}