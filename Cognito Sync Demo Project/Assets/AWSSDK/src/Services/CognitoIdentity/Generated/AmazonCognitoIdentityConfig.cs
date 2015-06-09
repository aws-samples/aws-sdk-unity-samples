//
// Copyright 2014-2015 Amazon.com, 
// Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the Amazon Software License (the "License"). 
// You may not use this file except in compliance with the 
// License. A copy of the License is located at
// 
//     http://aws.amazon.com/asl/
// 
// or in the "license" file accompanying this file. This file is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, express or implied. See the License 
// for the specific language governing permissions and 
// limitations under the License.
//

/*
 * Do not modify this file. This file is generated from the cognito-identity-2014-06-30.normal.json service model.
 */


using System;

using Amazon.Runtime;


namespace Amazon.CognitoIdentity
{
    /// <summary>
    /// Configuration for accessing Amazon CognitoIdentity service
    /// </summary>
    public partial class AmazonCognitoIdentityConfig : ClientConfig
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public AmazonCognitoIdentityConfig()
        {
            this.AuthenticationServiceName = "cognito-identity";
        }

        /// <summary>
        /// The constant used to lookup in the region hash the endpoint.
        /// </summary>
        public override string RegionEndpointServiceName
        {
            get
            {
                return "cognito-identity";
            }
        }

        /// <summary>
        /// Gets the ServiceVersion property.
        /// </summary>
        public override string ServiceVersion
        {
            get
            {
                return "2014-06-30";
            }
        }
    }
}
