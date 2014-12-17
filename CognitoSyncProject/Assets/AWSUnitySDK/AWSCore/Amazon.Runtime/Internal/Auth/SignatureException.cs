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
using System.Text;

namespace Amazon.Runtime.Internal.Auth
{
    /// <summary>
    /// This exception is thrown if there are problems signing the request.
    /// </summary>
    public class SignatureException : Exception
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
