//
// Copyright 2014-2015 Amazon.com, Inc. or its affiliates. All Rights Reserved.
//
//
// Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
// You may not use this file except in compliance with the License.
// A copy of the License is located in the "license" file accompanying this file.
// See the License for the specific language governing permissions and limitations under the License.
//
//

using UnityEngine;
using System.Collections;
using Amazon.Runtime;
using System.IO;
using Amazon.CognitoIdentity;
using Amazon;
using ThirdParty.Json.LitJson;

namespace AWSSDK.Examples
{
    public abstract class BaseSample : MonoBehaviour
    {
        AWSCredentials _credentials = null;

        private string AccessKey { get; set; }
        private string SecretKey { get; set; }
        private string IdentityPoolId { get; set; }
        private bool loaded = false;


        private const string CREDENTIALS_FILE = "credentials";

        protected AWSCredentials GetCredentials(CredentialsType credType)
        {
            LoadCredentials();
            if (credType == CredentialsType.Basic)
            {
                _credentials = new BasicAWSCredentials(AccessKey, SecretKey);
            }
            else
            {
                _credentials = new CognitoAWSCredentials(IdentityPoolId, RegionEndpoint.USEast1);
            }

            return _credentials;
        }

        private void LoadCredentials()
        {
            if (!loaded)
            {
                TextAsset awsCredentials = Resources.Load(CREDENTIALS_FILE) as TextAsset;
                JsonData jsonConfig = JsonMapper.ToObject(new JsonReader(awsCredentials.text));

                JsonData credentialsData = jsonConfig["credentials"];

                AccessKey = credentialsData["accessKey"].ToString();
                SecretKey = credentialsData["secretKey"].ToString();
                IdentityPoolId = credentialsData["identityPoolId"].ToString();
                loaded = true;
            }
        }


        protected enum CredentialsType
        {
            Basic,
            Cognito
        }

    }
}