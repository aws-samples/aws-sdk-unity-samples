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

using Amazon;
using Amazon.CognitoIdentity;
using Amazon.MobileAnalytics.MobileAnalyticsManager;
using Amazon.Util.Internal;
using UnityEngine;

namespace AWSSDK.Examples
{
    public class AmazonMobileAnalyticsSample : MonoBehaviour
    {

        public string IdentityPoolId = "YourIdentityPoolId";

        public string appId = "YourAppId";

        private MobileAnalyticsManager analyticsManager;

        private CognitoAWSCredentials _credentials;

        // Use this for initialization
        void Start()
        {
            UnityInitializer.AttachToGameObject(this.gameObject);

            _credentials = new CognitoAWSCredentials(IdentityPoolId, Amazon.RegionEndpoint.USEast1);
            analyticsManager = MobileAnalyticsManager.GetOrCreateInstance(appId, _credentials,
                                                                                Amazon.RegionEndpoint.USEast1);
        }


        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width * 0.5f, Screen.height));
            GUILayout.Label("Amazon Mobile Analytics Operations");

            // record custom event
            if (GUILayout.Button("Record Custom Event", GUILayout.MinHeight(Screen.height * 0.2f), GUILayout.Width(Screen.width * 0.4f)))
            {
                CustomEvent customEvent = new CustomEvent("level_complete");

                customEvent.AddAttribute("LevelName", "Level1");
                customEvent.AddAttribute("CharacterClass", "Warrior");
                customEvent.AddAttribute("Successful", "True");
                customEvent.AddMetric("Score", 12345);
                customEvent.AddMetric("TimeInLevel", 64);

                analyticsManager.RecordEvent(customEvent);
            }

            // record monetization event
            if (GUILayout.Button("Record Monetization Event", GUILayout.MinHeight(Screen.height * 0.2f), GUILayout.Width(Screen.width * 0.4f)))
            {
                MonetizationEvent monetizationEvent = new MonetizationEvent();

                monetizationEvent.Quantity = 3.0;
                monetizationEvent.ItemPrice = 1.99;
                monetizationEvent.ProductId = "ProductId123";
                monetizationEvent.ItemPriceFormatted = "$1.99";
                monetizationEvent.Store = "Apple";
                monetizationEvent.TransactionId = "TransactionId123";
                monetizationEvent.Currency = "USD";

                analyticsManager.RecordEvent(monetizationEvent);
            }


            GUILayout.EndArea();

        }

        void OnApplicationFocus(bool focus)
        {
            if (analyticsManager != null)
            {
                if (focus)
                {
                    analyticsManager.ResumeSession();
                }
                else
                {
                    analyticsManager.PauseSession();
                }
            }
        }

    }
}
