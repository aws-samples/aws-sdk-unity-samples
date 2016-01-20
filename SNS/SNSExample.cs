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
using Amazon.SimpleNotificationService;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon;
using UnityEngine.UI;
using Amazon.SimpleNotificationService.Model;


namespace AWSSDK.Examples
{
    public class SNSExample : MonoBehaviour
    {
        //identity pool id for cognito credentials
        public string IdentityPoolId = "";
        
        //sns android platform arn
        public string AndroidPlatformApplicationArn = "";
        
        //sns ios platform arn
        public string iOSPlatformApplicationArn = "";
        
        //project id for android gcm
        public string GoogleConsoleProjectId = "";

        public string CognitoIdentityRegion = RegionEndpoint.USEast1.SystemName;

        private RegionEndpoint _CognitoIdentityRegion
        {
            get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
        }

        public string SNSRegion = RegionEndpoint.USEast1.SystemName;

        private RegionEndpoint _SNSRegion
        {
            get { return RegionEndpoint.GetBySystemName(SNSRegion); }
        }
            
        public Button RegisterButton;
        public Button UnregisterButton;
        
        public Text ResultText;
        
        private string _endpointArn;
        
        // Use this for initialization
        void Start()
        {
            UnityInitializer.AttachToGameObject(this.gameObject);
            RegisterButton.onClick.AddListener(RegisterDevice);
            UnregisterButton.onClick.AddListener(UnregisterDevice);
        }
        
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
        
        private IAmazonSimpleNotificationService _snsClient;
        
        private IAmazonSimpleNotificationService SnsClient
        {
            get
            {
                if (_snsClient == null)
                    _snsClient = new AmazonSimpleNotificationServiceClient(Credentials, _SNSRegion);
                return _snsClient;
            }
        }
        
        private void RegisterDevice()
        {
#if UNITY_ANDROID
            if(string.IsNullOrEmpty(GoogleConsoleProjectId))
            {
                Debug.Log("sender id is null");
                return;
            }
            GCM.Register((regId) =>
            {
                
                if(string.IsNullOrEmpty(regId))
                {
                    ResultText.text = string.Format("Failed to get the registration id");
                    return;
                }
                
                ResultText.text = string.Format(@"Your registration Id is = {0}", regId);
                
                SnsClient.CreatePlatformEndpointAsync(
                    new CreatePlatformEndpointRequest
                    {
                        Token = regId,
                        PlatformApplicationArn = AndroidPlatformApplicationArn
                    },
                    (resultObject) =>
                    {
                        if(resultObject.Exception==null)
                        {
                            CreatePlatformEndpointResponse response = resultObject.Response;
                            _endpointArn = response.EndpointArn;
                            ResultText.text += string.Format(@"Platform endpoint arn is = {0}", response.EndpointArn);
                        }
                    }
                );
            }, GoogleConsoleProjectId);
#elif UNITY_IOS
#if UNITY_5
            UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert | UnityEngine.iOS.NotificationType.Badge | UnityEngine.iOS.NotificationType.Sound);
#else
            NotificationServices.RegisterForRemoteNotificationTypes(RemoteNotificationType.Alert|RemoteNotificationType.Badge|RemoteNotificationType.Sound);
#endif
            CancelInvoke("CheckForDeviceToken");
            InvokeRepeating("CheckForDeviceToken",1f,1f);
#endif
        }
        string deviceToken = null;
        //to keep a track of max number of times the device token is polled
        int count = 0;
        private void CheckForDeviceToken()
        {
#if UNITY_IOS
#if UNITY_5
            var token = UnityEngine.iOS.NotificationServices.deviceToken;
            var error = UnityEngine.iOS.NotificationServices.registrationError;
#else
            var token = NotificationServices.deviceToken;
            var error = NotificationServices.registrationError;
#endif
            if(count>=10 || !string.IsNullOrEmpty(error))
            {
                CancelInvoke("CheckForDeviceToken");
                Debug.Log(@"Cancel polling");
                return;
            }
            
            if(token!=null)
            {
                deviceToken = System.BitConverter.ToString(token).Replace("-","");
                Debug.Log("device token  = " + deviceToken);
                ResultText.text = string.Format(@"Your device token is = {0}", deviceToken);
                SnsClient.CreatePlatformEndpointAsync(
                    new CreatePlatformEndpointRequest
                    {
                        Token = deviceToken,
                        PlatformApplicationArn = iOSPlatformApplicationArn
                    },
                    (resultObject) =>
                    {
                        if(resultObject.Exception==null)
                        {
                            CreatePlatformEndpointResponse response = resultObject.Response;
                            _endpointArn = response.EndpointArn;
                            ResultText.text += string.Format("\n Subscribed to Platform endpoint arn = {0}", response.EndpointArn);
                        }
                    }
                );
                
                CancelInvoke("CheckForDeviceToken");
            }
            count++;
#endif
        }
        
        private void UnregisterDevice()
        {
            if(!string.IsNullOrEmpty(_endpointArn))
            {
                SnsClient.DeleteEndpointAsync(
                    new DeleteEndpointRequest
                    {
                        EndpointArn = _endpointArn
                    },
                    (resultObject)=>{
                        if(resultObject.Exception == null)
                        {
                            Debug.Log("Deleted the endpoint. You will not get any new notifications");
                            ResultText.text = @"You will not get any new notifications";
                        }
                    }
                );
            }
        }
        
    }
}
