//
// Copyright 2014-2015 Amazon.com, 
// Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the AWS Mobile SDK For Unity 
// Sample Application License Agreement (the "License"). 
// You may not use this file except in compliance with the 
// License. A copy of the License is located 
// in the "license" file accompanying this file. This file is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, express or implied. See the License 
// for the specific language governing permissions and 
// limitations under the License.
//

using UnityEngine;
using System.Collections;
using Amazon.DynamoDBv2;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon;

namespace AWSSDK.Examples
{

    public class DynamoDbBaseExample : MonoBehaviour
    {
        public string IdentityPoolId = "";
        public string CognitoPoolRegion = RegionEndpoint.USEast1.SystemName;
        public string DynamoRegion = RegionEndpoint.USEast1.SystemName;

        private RegionEndpoint _CognitoPoolRegion
        {
            get { return RegionEndpoint.GetBySystemName(CognitoPoolRegion); }
        }

        private RegionEndpoint _DynamoRegion
        {
            get { return RegionEndpoint.GetBySystemName(DynamoRegion); }
        }

        private static IAmazonDynamoDB _ddbClient;

        private AWSCredentials _credentials;

        private AWSCredentials Credentials
        {
            get
            {
                if (_credentials == null)
                    _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoPoolRegion);
                return _credentials;
            }
        }

        protected IAmazonDynamoDB Client
        {
            get
            {
                if (_ddbClient == null)
                {
                    _ddbClient = new AmazonDynamoDBClient(Credentials, _DynamoRegion);
                }

                return _ddbClient;
            }
        }

        protected void BackListener()
        {
            Application.LoadLevel(0);
        }

    }
}
