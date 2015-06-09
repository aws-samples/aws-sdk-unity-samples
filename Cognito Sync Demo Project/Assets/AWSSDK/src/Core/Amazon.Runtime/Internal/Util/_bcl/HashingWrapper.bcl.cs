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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Amazon.Runtime;

namespace Amazon.Runtime.Internal.Util
{
    public partial class HashingWrapper : IHashingWrapper
    {
        private HashAlgorithm _algorithm;
        private void Init(string algorithmName)
        {
            _algorithm = HashAlgorithm.Create(algorithmName);
        }

        #region IHashingWrapper Members

        public void Clear()
        {
            _algorithm.Initialize();
        }

        public byte[] ComputeHash(byte[] buffer)
        {
            return _algorithm.ComputeHash(buffer);
        }

        public byte[] ComputeHash(Stream stream)
        {
            return _algorithm.ComputeHash(stream);
        }

        public void AppendBlock(byte[] buffer, int offset, int count)
        {
            _algorithm.TransformBlock(buffer, offset, count, null, 0);
        }

        public byte[] AppendLastBlock(byte[] buffer, int offset, int count)
        {
            _algorithm.TransformFinalBlock(buffer, offset, count);
            return _algorithm.Hash;
        }

        #endregion
    }

    public class HashingWrapperMD5 : HashingWrapper
    {
        private static string md5AlgorithmName = typeof(MD5).FullName;

        public HashingWrapperMD5()
            : base(md5AlgorithmName)
        { }
    }
}
