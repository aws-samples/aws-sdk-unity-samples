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

using Amazon.Runtime;
using Amazon.Unity;


namespace Amazon.SecurityToken
{
    /// <summary>
    /// Configuration for accessing Amazon SecurityTokenService service
    /// </summary>
    public partial class AmazonSecurityTokenServiceConfig : ClientConfig
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public AmazonSecurityTokenServiceConfig()
        {
            this.AuthenticationServiceName = "sts";
            if (this.RegionEndpoint == null)
				this.RegionEndpoint = AmazonInitializer.CognitoRegionEndpoint;
        }

        /// <summary>
        /// The constant used to lookup in the region hash the endpoint.
        /// </summary>
        internal override string RegionEndpointServiceName
        {
            get
            {
                return "sts";
            }
        }

        /// <summary>
        /// Gets the ServiceVersion property.
        /// </summary>
        public override string ServiceVersion
        {
            get
            {
                return "2011-06-15";
            }
        }
    }
}