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

#define DEVELOPER_AUTHENTICATED_IDENTITIES

#if DEVELOPER_AUTHENTICATED_IDENTITIES
using System;
using System.Collections.Generic;

using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.Unity3D;

/// <summary>
/// Simple example implementation for using BYOI 
/// change the getProviderName() & refreshAsync methods with the developer backend details
/// </summary>
public class ExampleBYOIProvider : AbstractCognitoIdentityProvider
{
    /// <summary>
    /// Initializes a new instance .
    /// </summary>
    public ExampleBYOIProvider (string accountId, string identityPoolId)
        : base(accountId, identityPoolId)
    {
    }
    #region implemented abstract members of AbstractCognitoIdentityProvider

    public override string getProviderName ()
    {
        return "com.example";
    }

    public override void RefreshAsync (AmazonServiceCallback callback, object state)
    {
        try
        {
            if (Logins == null)
                Logins = new Dictionary<string, string>();
            
               /// Using this call, get an identityId and a valid Cognito openid token from
            ///  your backend to get a reference to both for the local device.
            ///  Call update method to make sure the identityId and token are now
            ///  handy and are utilized appropriately
            /// http://mobile.awsblog.com/post/Tx1YVAQ4NZKBWF5/Amazon-Cognito-Announcing-Developer-Authenticated-Identities
            /// AmazonMainThreadDispatch.ExecCoroutine() is available in case you have to want to yield on a WWW request
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

    #endregion

}
#endif
