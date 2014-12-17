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
using System.Linq;
using System.Text;

namespace Amazon.Runtime.Internal.Auth
{
    /// <summary>
    /// Null Signer which does a no-op.
    /// </summary>
    internal class NullSigner : AbstractAWSSigner
    {
        public override void Sign(IRequest request, ClientConfig clientConfig, Util.RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            // This is a null signer which a does no-op
            return;
        }

        public override ClientProtocol Protocol
        {
            get { return ClientProtocol.Unknown; }
        }
    }
}
