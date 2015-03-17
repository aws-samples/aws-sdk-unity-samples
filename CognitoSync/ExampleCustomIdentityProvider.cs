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

using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.Unity3D;
using Amazon;

/// <summary>
/// Example for using developer authenticated identitites.
/// You will have to implement getProviderName and RefreshAsync.
/// </summary>
public class ExampleCustomIdentityProvider : AbstractCognitoIdentityProvider
{

	public ExampleCustomIdentityProvider (string identityPoolId, RegionEndpoint endpoint)
		: base(null, identityPoolId, endpoint)
    {
    }

    public override string getProviderName ()
    {
        return "com.example";
    }

    public override void RefreshAsync (AmazonServiceCallback callback, object state)
    {
        try
        {
            /// Using this call, get an identityId and a valid Cognito openid token from your
			/// backend to get a reference to both for the local device and call UpdateIdentity
            /// http://mobile.awsblog.com/post/Tx1YVAQ4NZKBWF5/Amazon-Cognito-Announcing-Developer-Authenticated-Identities
			/// AmazonMainThreadDispatcher.ExecCoroutine() can be useful in case you have to want
			/// to yield on a WWW request: AmazonMainThreadDispatcher.ExecCoroutine(ContactWebBackend(callback)); 
            _identityId = "retrievedIdentityID";
            _token = "retrivedDeveloperToken";
            
            Logins[getProviderName()] = _token;
            UpdateIdentity(_identityId);
            AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonServiceResult(null, null, null, state));
        }
        catch (Exception ex)
        {
            AmazonMainThreadDispatcher.ExecCallback(callback, new AmazonServiceResult(null, null, new AmazonServiceException(ex), state));
        }
    }

}
