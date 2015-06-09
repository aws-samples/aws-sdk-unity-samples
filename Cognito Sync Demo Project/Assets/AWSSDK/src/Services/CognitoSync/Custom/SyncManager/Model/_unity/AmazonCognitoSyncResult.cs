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
using Amazon.Runtime;

namespace Amazon.CognitoSync.SyncManager
{
    public delegate void AmazonCognitoSyncCallback<TResponse>(AmazonCognitoSyncResult<TResponse> result);

    public delegate void AmazonCognitoSyncCallback(AmazonCognitoSyncResult result);

    public class AmazonCognitoSyncResult<TResponse>
    {
        public TResponse Response { get; internal set; }

        public Exception Exception { get; internal set; }

        public object State { get; internal set; }

        public AmazonCognitoSyncResult(object state)
        {
            this.State = state;
        }

        public AmazonCognitoSyncResult(TResponse response, Exception exception, object state)
        {
            this.Response = response;
            this.Exception = exception;
            this.State = state;
        }
    }

    public class AmazonCognitoSyncResult
    {
        public Exception Exception { get; internal set; }

        public object State { get; internal set; }

        public AmazonCognitoSyncResult(object state)
        {
            this.State = state;
        }

        public AmazonCognitoSyncResult(Exception exception, object state)
        {
            this.Exception = exception;
            this.State = state;
        }
    }

}
