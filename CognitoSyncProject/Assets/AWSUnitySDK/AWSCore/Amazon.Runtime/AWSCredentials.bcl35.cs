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

using Amazon.SecurityToken.Model;

namespace Amazon.Runtime
{
    /// <summary>
    /// Assumed role credentials retrieved and automatically refreshed from
    /// an instance of IAmazonSecurityTokenService.
    /// </summary>
    public partial class AssumeRoleAWSCredentials : RefreshingAWSCredentials, IDisposable
    {
        private Credentials GetServiceCredentials()
        {
            /*
            Credentials credentials;
            
            if (_assumeRequest != null)
                credentials = _stsClient.AssumeRole(_assumeRequest).Credentials;
            else
                credentials = _stsClient.AssumeRoleWithSAML(_assumeSamlRequest).Credentials;
            
            return credentials;
             * */
            throw new NotImplementedException("figure this out");
        }
    }
}
