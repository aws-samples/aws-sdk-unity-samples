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
using Amazon.CognitoIdentity.Model;
using Amazon.Runtime;

namespace Amazon.CognitoIdentity
{
    public interface IAmazonIdentityProvider
    {
        string getProviderName();

        void RefreshAsync(AmazonServiceCallback callback, object state);

        string GetCurrentIdentityId();

        string GetCurrentOpenIdToken();

        Dictionary<string, string> Logins { get; set; }

        event EventHandler<IdentityChangedArgs> IdentityChangedEvent;
    }
}
