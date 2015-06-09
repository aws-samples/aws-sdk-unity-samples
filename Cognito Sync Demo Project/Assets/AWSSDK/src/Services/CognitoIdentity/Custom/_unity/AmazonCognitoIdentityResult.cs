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

namespace Amazon.CognitoIdentity
{
    public delegate void AmazonCognitoIdentityCallback<TResponse>(AmazonCognitoIdentityResult<TResponse> result);

    public class AmazonCognitoIdentityResult<TResponse>
    {
        public TResponse Response { get; internal set; }

        public Exception Exception { get; internal set; }

        public object State { get; internal set; }

        public AmazonCognitoIdentityResult(object state)
        {
            this.State = state;
        }

        public AmazonCognitoIdentityResult(TResponse response, Exception exception, object state)
        {
            this.Response = response;
            this.Exception = exception;
            this.State = state;
        }
    }
}
