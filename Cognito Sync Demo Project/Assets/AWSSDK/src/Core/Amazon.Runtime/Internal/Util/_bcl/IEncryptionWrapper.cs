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

using System.IO;
using System.Security.Cryptography;

namespace Amazon.Runtime.Internal.Util
{
    public interface IEncryptionWrapper
    {
        void Reset();
        int AppendBlock(byte[] buffer, int offset, int count, byte[] target, int targetOffset);
        byte[] AppendLastBlock(byte[] buffer, int offset, int count);
        void SetEncryptionData(byte[] key, byte[] IV);
        void CreateEncryptor();
    }
}
