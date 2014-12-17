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
using System.Collections.Generic;
using System.Text;
using System.Net;

using Amazon.Util;
using Amazon.Runtime.Internal.Util;

namespace Amazon.Runtime.Internal.Auth
{
    public enum ClientProtocol { QueryStringProtocol, RestProtocol, Unknown }

    public abstract class AbstractAWSSigner
    {
        private AWS4Signer _aws4Signer;
        private AWS4Signer AWS4SignerInstance
        {
            get
            {
                if (_aws4Signer == null)
                {
                    lock (this)
                    {
                        if (_aws4Signer == null)
                            _aws4Signer = new AWS4Signer();
                    }
                }

                return _aws4Signer;
            }
        }


        protected const string DEFAULT_ENCODING = "UTF-8";

        /// <summary>
        /// Computes RFC 2104-compliant HMAC signature.
        /// </summary>
        protected static string ComputeHash(string data, string secretkey, SigningAlgorithm algorithm)
        {
            try 
            {
                string signature = CryptoUtilFactory.CryptoInstance.HMACSign(data, secretkey, algorithm);

                return signature;
            } 
            catch (Exception e) 
            {
                throw new Amazon.Runtime.SignatureException("Failed to generate signature: " + e.Message, e);
            }
        }

        /// <summary>
        /// Computes RFC 2104-compliant HMAC signature.
        /// </summary>
        protected static string ComputeHash(byte[] data, string secretkey, SigningAlgorithm algorithm)
        {
            try
            {
                string signature = CryptoUtilFactory.CryptoInstance.HMACSign(data, secretkey, algorithm);

                return signature;
            }
            catch (Exception e)
            {
                throw new Amazon.Runtime.SignatureException("Failed to generate signature: " + e.Message, e);
            }
        }

        public abstract void Sign(IRequest request, ClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey);

        public abstract ClientProtocol Protocol { get; }

        /// <summary>
        /// Inspects the supplied evidence to return the signer appropriate for the operation
        /// </summary>
        /// <param name="useSigV4Setting">Global setting for the service</param>
        /// <param name="config">Configuration for the client</param>
        /// <returns>True if signature v4 request signing should be used</returns>
        protected static bool UseV4Signing(bool useSigV4Setting, ClientConfig config)
        {
            if (useSigV4Setting || config.SignatureVersion == "4")
                return true;

            // do a cascading series of checks to try and arrive at whether we have
            // a recognisable region; this is required to use the AWS4 signer
            RegionEndpoint r = null;
            if (!string.IsNullOrEmpty(config.AuthenticationRegion))
                r = RegionEndpoint.GetBySystemName(config.AuthenticationRegion);

            if (r == null && !string.IsNullOrEmpty(config.ServiceURL))
            {
                var parsedRegion = AWSSDKUtils.DetermineRegion(config.ServiceURL);
                if (!string.IsNullOrEmpty(parsedRegion))
                    r = RegionEndpoint.GetBySystemName(parsedRegion);
            }

            if (r == null && config.RegionEndpoint != null)
                r = config.RegionEndpoint;

            if (r != null)
            {
                var endpoint = r.GetEndpointForService(config.RegionEndpointServiceName);
                if (endpoint != null && endpoint.SignatureVersionOverride == "4")
                    return true;
            }

            return false;
        }

        
        protected AbstractAWSSigner SelectSigner(ClientConfig config)
        {
            return SelectSigner(this, useSigV4Setting: false, config: config);
        }
        protected AbstractAWSSigner SelectSigner(bool useSigV4Setting,  ClientConfig config)
        {
            return SelectSigner(this, useSigV4Setting, config);
        }
        protected AbstractAWSSigner SelectSigner(AbstractAWSSigner defaultSigner,bool useSigV4Setting,  ClientConfig config)
        {
            bool usev4Signing = UseV4Signing(useSigV4Setting, config);

            if (usev4Signing)
                return AWS4SignerInstance;
            else
                return defaultSigner;
        }
    }
}
