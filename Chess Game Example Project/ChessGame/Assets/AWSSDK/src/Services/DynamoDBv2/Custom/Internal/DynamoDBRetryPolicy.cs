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

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.DynamoDBv2.Internal
{
    /// <summary>
    /// Custom DynamoDB retry policy.
    /// This policy has a slightly different exponential backoff logic compared
    /// to the default retry policy.
    /// </summary>
    public class DynamoDBRetryPolicy : DefaultRetryPolicy
    {
        /// <summary>
        /// Construct instance of DynamoDBRetryPolicy.
        /// </summary>
        /// <param name="maxRetries"></param>
        public DynamoDBRetryPolicy(int maxRetries)
            : base(maxRetries)
        { }

        /// <summary>
        /// Overriden to cause a pause between retries.
        /// </summary>
        /// <param name="executionContext"></param>
        public override void WaitBeforeRetry(IExecutionContext executionContext)
        {
            pauseExponentially(executionContext.RequestContext.Retries);
        }

        /// <summary>
        /// Override the pausing function so retries would happen more frequent then the default operation.
        /// </summary>
        /// <param name="retries">Current number of retries.</param>
        private void pauseExponentially(int retries)
        {
            int delay = (retries == 0) ? 0 : 50 * (int)Math.Pow(2, retries - 1);
            delay = Math.Min(delay, MaxBackoffInMilliseconds);
            Amazon.Util.AWSSDKUtils.Sleep(delay);
        }
    }
}
