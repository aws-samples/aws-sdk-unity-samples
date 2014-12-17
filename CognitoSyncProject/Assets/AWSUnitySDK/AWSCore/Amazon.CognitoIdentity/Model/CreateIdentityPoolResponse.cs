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
    /// Configuration for accessing Amazon CreateIdentityPool service
    /// </summary>
    public partial class CreateIdentityPoolResponse : CreateIdentityPoolResult
    {
        /// <summary>
        /// Gets and sets the CreateIdentityPoolResult property.
        /// Represents the output of a CreateIdentityPool operation.
        /// </summary>
        [Obsolete(@"This property has been deprecated. All properties of the CreateIdentityPoolResult class are now available on the CreateIdentityPoolResponse class. You should use the properties on CreateIdentityPoolResponse instead of accessing them through CreateIdentityPoolResult.")]
        public CreateIdentityPoolResult CreateIdentityPoolResult
        {
            get
            {
                return this;
            }
        }
    }
}