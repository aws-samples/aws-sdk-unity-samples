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
using System.Xml.Serialization;
using System.Text;
using System.IO;
using Amazon.Runtime;

namespace Amazon.SecurityToken.Model
{
    /// <summary>
    /// <para>AWS credentials for API authentication.</para>
    /// </summary>
    public partial class Credentials : AWSCredentials
    {
        private ImmutableCredentials _credentials = null;

        /// <summary>
        /// Returns a copy of ImmutableCredentials corresponding to these credentials
        /// </summary>
        /// <returns></returns>
        public override ImmutableCredentials GetCredentials()
        {
            if (_credentials == null)
                _credentials = new ImmutableCredentials(AccessKeyId, SecretAccessKey, SessionToken);
            return _credentials.Copy();
        }
    }
}
