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
using System.Linq;
using System.Text;

namespace Amazon.Runtime.SharedInterfaces
{
    /// <summary>
    /// ICoreAmazonSQS is not meant to use directly. It defines SQS with basic .NET types
    /// and allows other services to be able to use SQS as a runtime dependency. This interface
    /// is implemented by the AmazonSQSClient defined in the SQS assembly.
    /// </summary>
    public interface ICoreAmazonSQS
    {
#if BCL
        /// <summary>
        /// Get the attributes for the queue identified by the queue URL.
        /// </summary>
        /// <param name="queueUrl">The queue URL to get attributes for.</param>
        /// <returns>The attributes for the queue.</returns>
        Dictionary<string, string> GetAttributes(string queueUrl);

        /// <summary>
        /// Set the attributes on the queue identified by the queue URL.
        /// </summary>
        /// <param name="queueUrl">The queue URL to set the attributues.</param>
        /// <param name="attributes">The attributes to set.</param>
        void SetAttributes(string queueUrl, Dictionary<string, string> attributes);
#endif
    }
}
