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
using Amazon.Runtime;

namespace Amazon.CognitoSync.SyncManager
{
    public delegate void AmazonCognitoCallback(AmazonCognitoResult result);

    public class AmazonCognitoResult
    {
        public AmazonCognitoResponse Response { get; internal set; }

        public Exception Exception { get; internal set; }

        public object State { get; internal set; }

        public AmazonCognitoResult(object state)
        {
            this.State = state;
        }

        public AmazonCognitoResult(AmazonCognitoResponse response, Exception exception, object state)
        {
            this.Response = response;
            this.Exception = exception;
            this.State = state;
        }
    }
}