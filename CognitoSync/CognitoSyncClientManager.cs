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
//#define DEVELOPER_AUTHENTICATED_IDENTITIES 

using System;
using Amazon.CognitoIdentity;
using Amazon.CognitoSync.SyncManager;
using Amazon.CognitoSync;
using Amazon.Unity3D;
using Amazon.SecurityToken;
using Amazon.Common;
using Amazon.Runtime;

public class CognitoSyncClientManager
{
    private static CognitoAWSCredentials _credentials;
    private static CognitoSyncManager _syncManager;

    public static void init()
    {
		String identityPoolId = "YOUR_IDENTITY_POOL_ID"; //Set your Identity Pool here
		Amazon.RegionEndpoint region = Amazon.RegionEndpoint.USEast1;

		if (String.IsNullOrEmpty(identityPoolId)) {
			throw new Exception("You need to set up an identity pool in CognitoSyncClientManager.cs");
		}

        // CognitoAWSCredentials is recommended in the place of Developer credentials{accessKey,secretKey} for reasons of security best practices
        // CognitoAWSCredentials uses the CognitoIdentityProvider & provides temporary scoped AWS credentials from AssumeRoleWithWebIdentity 
        // ref: http://mobile.awsblog.com/post/TxR1UCU80YEJJZ/Using-the-Amazon-Cognito-Credentials-Provider
        
#if DEVELOPER_AUTHENTICATED_IDENTITIES
        // or to use developer authenticated identities 
        CognitoAWSCredentials _credentials = new CognitoAWSCredentials(new ExampleIdentityProvider());
#else
        
        // Ref: http://docs.aws.amazon.com/mobile/sdkforandroid/developerguide/cognito-auth.html#create-an-identity-pool
        // for setting up Cognito Identity Pools, can you use the sample code for .NET SDK
		_credentials = new CognitoAWSCredentials(identityPoolId, region);
#endif
        // DefaultCognitoSyncManager is a high level CognitoSync Client which handles all Sync operations at a Dataset level. 
        // Additionally, it also provides local storage of the Datasets which can be later Synchronized with the cloud(CognitoSync service)
        // This feature allows the user to continue working w/o internet access and sync with CognitoSync whenever possible
		_syncManager = new DefaultCognitoSyncManager(_credentials,  new AmazonCognitoSyncConfig { RegionEndpoint = region });
    }

    public static CognitoSyncManager CognitoSyncManagerInstance
    {
        get
        {
            if (_syncManager == null)
            {
                throw new InvalidOperationException("not initialized yet");
            }
            return _syncManager;
        }
    }

    public static CognitoAWSCredentials CognitoAWSCredentialsInstance
    {
        get
        {
            if (_credentials == null)
            {
                throw new InvalidOperationException("not initialized yet");
            }
            return _credentials;
        }
    }
}

