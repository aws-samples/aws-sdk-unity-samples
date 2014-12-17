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
using System.IO;

namespace Amazon.Runtime.Internal.Util
{
    public interface IHashingWrapper
    {
        void Clear();
        byte[] ComputeHash(byte[] buffer);
        byte[] ComputeHash(Stream stream);
        void AppendBlock(byte[] buffer);
        void AppendBlock(byte[] buffer, int offset, int count);
        byte[] AppendLastBlock(byte[] buffer);
        byte[] AppendLastBlock(byte[] buffer, int offset, int count);
    }
}
