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
