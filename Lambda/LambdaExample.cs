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
using UnityEngine.UI;
using Amazon.Lambda;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon;
using System.Text;
using Amazon.Lambda.Model;

namespace AWSSDK.Examples
{
    public class LambdaExample : MonoBehaviour
    {
        public string IdentityPoolId = "";
        public string CognitoIdentityRegion = RegionEndpoint.USEast1.SystemName;
        private RegionEndpoint _CognitoIdentityRegion
        {
            get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
        }
        public string LambdaRegion = RegionEndpoint.USEast1.SystemName;
        private RegionEndpoint _LambdaRegion
        {
            get { return RegionEndpoint.GetBySystemName(LambdaRegion); }
        }


        public Button InvokeButton = null;
        public Button ListFunctionsButton = null;
        public InputField FunctionNameText = null;
        public InputField EventText = null;
        public Text ResultText = null;

        void Start()
        {
            UnityInitializer.AttachToGameObject(this.gameObject);
            InvokeButton.onClick.AddListener(() => { Invoke(); });
            ListFunctionsButton.onClick.AddListener(() => { ListFunctions(); });
        }

        #region private members

        private IAmazonLambda _lambdaClient;
        private AWSCredentials _credentials;

        private AWSCredentials Credentials
        {
            get
            {
                if (_credentials == null)
                    _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
                return _credentials;
            }
        }

        private IAmazonLambda Client
        {
            get
            {
                if (_lambdaClient == null)
                {
                    _lambdaClient = new AmazonLambdaClient(Credentials, _LambdaRegion);
                }
                return _lambdaClient;
            }
        }

        #endregion

        #region Invoke
        /// <summary>
        /// Example method to demostrate Invoke. Invokes the Lambda function with the specified
        /// function name (e.g. helloWorld) with the parameters specified in the Event JSON.
        /// Because no InvokationType is specified, the default 'RequestResponse' is used, meaning
        /// that we expect the AWS Lambda function to return a value.
        /// </summary>
        public void Invoke()
        {
            ResultText.text = "Invoking '" + FunctionNameText.text + " function in Lambda... \n";
            Client.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest()
            {
                FunctionName = FunctionNameText.text,
                Payload = EventText.text
            },
            (responseObject) =>
            {
                ResultText.text += "\n";
                if (responseObject.Exception == null)
                {
                    ResultText.text += Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()) + "\n";
                }
                else
                {
                    ResultText.text += responseObject.Exception + "\n";
                }
            }
            );
        }

        #endregion

        #region List Functions
        /// <summary>
        /// Example method to demostrate ListFunctions
        /// </summary>
        public void ListFunctions()
        {
            ResultText.text = "Listing all of your Lambda functions... \n";
            Client.ListFunctionsAsync(new Amazon.Lambda.Model.ListFunctionsRequest(),
            (responseObject) =>
            {
                ResultText.text += "\n";
                if (responseObject.Exception == null)
                {
                    ResultText.text += "Functions: \n";
                    foreach (FunctionConfiguration function in responseObject.Response.Functions)
                    {
                        ResultText.text += "    " + function.FunctionName + "\n";
                    }
                }
                else
                {
                    ResultText.text += responseObject.Exception + "\n";
                }
            }
            );
        }

        #endregion
    }
}
