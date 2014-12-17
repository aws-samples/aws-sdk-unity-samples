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
using System.Security.Cryptography;
using System.Text;
using Amazon.Runtime;

namespace Amazon.Runtime.Internal.Util
{
    public class DecryptionWrapper : IDecryptionWrapper
    {
        private SymmetricAlgorithm algorithm;
        private ICryptoTransform decryptor;
        private const int encryptionKeySize = 256;

        public DecryptionWrapper(string algorithmName)
        {
            if (string.IsNullOrEmpty(algorithmName))
                throw new ArgumentNullException("algorithmName");

            algorithm = SymmetricAlgorithm.Create(algorithmName);
        }

        #region IDecryptionWrapper Members
        
        public ICryptoTransform Transformer
        {
            get { return this.decryptor; }
        }

        public void SetDecryptionData(byte[] key, byte[] IV)
        {
            algorithm.KeySize = encryptionKeySize;
            algorithm.Padding = PaddingMode.PKCS7;
            algorithm.Mode = CipherMode.CBC;
            algorithm.Key = key;
            algorithm.IV = IV;
        }

        public void CreateDecryptor()
        {
            decryptor = algorithm.CreateDecryptor();
        }
        #endregion
    }

    public class DecryptionWrapperAES : DecryptionWrapper
    {
        private const string aesAlgorithmName = "AES";
        public DecryptionWrapperAES()
            : base(aesAlgorithmName)
        { }
    }
}
