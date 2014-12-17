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
using System.IO;
using System.Linq;
using System.Text;

using Amazon.Runtime;

namespace Amazon.Util
{
    internal interface ICryptoUtil
    {
        string HMACSign(string data, string key, SigningAlgorithm algorithmName);
        string HMACSign(byte[] data, string key, SigningAlgorithm algorithmName);

        byte[] ComputeSHA256Hash(byte[] data);
        byte[] ComputeSHA256Hash(Stream steam);

        byte[] ComputeMD5Hash(byte[] data);
        byte[] ComputeMD5Hash(Stream steam);

        byte[] HMACSignBinary(byte[] data, byte[] key, SigningAlgorithm algorithmName);
    }
}
