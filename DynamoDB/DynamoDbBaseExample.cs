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
using Amazon.DynamoDBv2;

namespace AWSSDK.Examples
{
    public class DynamoDbBaseExample : BaseSample {
        
        
        private static IAmazonDynamoDB _ddbClient;
        
        protected IAmazonDynamoDB Client
        {
            get
            {
                if(_ddbClient == null)
                {
                    _ddbClient = new AmazonDynamoDBClient(GetCredentials(CredentialsType.Cognito));
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