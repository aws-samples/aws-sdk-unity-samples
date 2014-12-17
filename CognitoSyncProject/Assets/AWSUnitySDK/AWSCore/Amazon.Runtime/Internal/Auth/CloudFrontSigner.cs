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
using System.Security;
using System.Text;

using Amazon.Util;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Util;

using System.Globalization;

namespace Amazon.Runtime.Internal.Auth
{
    internal class CloudFrontSigner : AbstractAWSSigner
    {
        public override ClientProtocol Protocol
        {
            get { return ClientProtocol.RestProtocol; }
        }

        public override void Sign(IRequest request, ClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            if (String.IsNullOrEmpty(awsAccessKeyId))
            {
                throw new ArgumentOutOfRangeException("awsAccessKeyId", "The AWS Access Key ID cannot be NULL or a Zero length string");
            }

            string dateTime = AWSSDKUtils.GetFormattedTimestampRFC822(0);
            request.Headers.Add(HeaderKeys.XAmzDateHeader, dateTime);

            string signature = ComputeHash(dateTime, awsSecretAccessKey, SigningAlgorithm.HmacSHA1);

            request.Headers.Add(HeaderKeys.AuthorizationHeader, "AWS " + awsAccessKeyId + ":" + signature);
        }
    }
}
