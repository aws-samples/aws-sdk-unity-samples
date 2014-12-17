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
using Amazon.Runtime.Internal.Util;
using Amazon.Util;

namespace Amazon.Runtime.Internal.Auth
{
    internal class EC2Signer : AbstractAWSSigner
    {
        // QueryStringSigner
        private QueryStringSigner querySigner = new QueryStringSigner();

        private bool _useSigV4 = false;

        /// <summary>
        /// EC2 signer constructor
        /// </summary>
        public EC2Signer()
        {
            _useSigV4 = AWSConfigs.EC2Config.UseSignatureVersion4;
        }

        public override ClientProtocol Protocol
        {
            get { return ClientProtocol.QueryStringProtocol; }
        }

        /// <summary>
        /// Determines the appropriate signer and signs the request.
        /// </summary>
        /// <param name="awsAccessKeyId">The AWS public key</param>
        /// <param name="awsSecretAccessKey">The AWS secret key used to sign the request in clear text</param>
        /// <param name="metrics">Request metrics</param>
        /// <param name="clientConfig">The configuration that specifies which hashing algorithm to use</param>
        /// <param name="request">The request to have the signature compute for</param>
        /// <exception cref="Amazon.Runtime.SignatureException">If any problems are encountered while signing the request</exception>
        public override void Sign(IRequest request, ClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            var signer = SelectSigner(querySigner, _useSigV4, clientConfig);
            var useV4 = signer is AWS4Signer;

            if (useV4)
            {
                if (request.Parameters.ContainsKey("SecurityToken"))
                {
                    var token = request.Parameters["SecurityToken"];
                    request.Parameters.Remove("SecurityToken");

                    request.Headers[HeaderKeys.XAmzSecurityTokenHeader] = token;
                }
            }

            signer.Sign(request, clientConfig, metrics, awsAccessKeyId, awsSecretAccessKey);
        }
    }
}
