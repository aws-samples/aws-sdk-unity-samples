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
using System.Text;

namespace Amazon.Runtime
{
    /// <summary>
    /// This exception is thrown if there are problems signing the request.
    /// </summary>
    public class SignatureException : Amazon.Runtime.Internal.Auth.SignatureException
    {
        public SignatureException(string message)
            : base(message)
        {
        }

        public SignatureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
