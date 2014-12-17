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

namespace Amazon.Runtime
{
    /// <summary>
    /// Exception thrown by the SDK for errors that occur within the SDK.
    /// </summary>
    public class AmazonClientException : Exception
    {
        public AmazonClientException(string message) : base(message) { }

        public AmazonClientException(string message, Exception innerException) : base(message, innerException) { }
    }
}
