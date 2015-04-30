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

using Amazon.MobileAnalytics.MobileAnalyticsManager;
using Amazon.CognitoIdentity;
using Amazon.Util;﻿
using Amazon.Util.Internal;﻿
using Amazon.Runtime;
using UnityEngine;
using AWSSDK.Examples;

public class AmazonMobileAnalyticsSample : BaseSample
{
    
    bool started = false;
    
    public string appId = "YourAppId";
    public string cognitoIdentityPoolId = "YourPoolId";
    private MobileAnalyticsManager analyticsManager;
    
    // Use this for initialization
    void Start()
    {

#if UNITY_EDITOR
        /// This is just to spoof the application to think that its running on iOS platform
        AmazonHookedPlatformInfo.Instance.Platform = "iPhoneOS";
        AmazonHookedPlatformInfo.Instance.Model = "iPhone";
        AmazonHookedPlatformInfo.Instance.Make = "Apple";
        AmazonHookedPlatformInfo.Instance.Locale = "en_US";
        AmazonHookedPlatformInfo.Instance.PlatformVersion = "8.1.2";
        
        AmazonHookedPlatformInfo.Instance.Title =  "YourApp";
        AmazonHookedPlatformInfo.Instance.VersionName = "v1.0";
        AmazonHookedPlatformInfo.Instance.VersionCode = "1.0";
        AmazonHookedPlatformInfo.Instance.PackageName = "com.yourcompany.yourapp";
#endif

        analyticsManager = MobileAnalyticsManager.GetOrCreateInstance((CognitoAWSCredentials)GetCredentials(CredentialsType.Cognito),
                                                                            Amazon.RegionEndpoint.USEast1, appId);
        started = true;
    }


    void OnGUI ()
    {
        GUILayout.BeginArea (new Rect (0, 0, Screen.width * 0.5f, Screen.height));
        GUILayout.Label ("Amazon Mobile Analytics Operations");

        // record custom event
        if (GUILayout.Button ("Record Custom Event", GUILayout.MinHeight (Screen.height * 0.2f), GUILayout.Width (Screen.width * 0.4f)))
        {
            CustomEvent customEvent = new CustomEvent("level_complete");
            
            customEvent.AddAttribute("LevelName","Level1");
            customEvent.AddAttribute("CharacterClass","Warrior");
            customEvent.AddAttribute("Successful","True");
            customEvent.AddMetric("Score",12345);
            customEvent.AddMetric("TimeInLevel",64);
            
            analyticsManager.RecordEvent(customEvent);
        }
        
        // record monetization event
        if (GUILayout.Button ("Record Monetization Event", GUILayout.MinHeight (Screen.height * 0.2f), GUILayout.Width (Screen.width * 0.4f)))
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

        
        GUILayout.EndArea ();
    
    }

    void OnApplicationFocus(bool focus)
    {
        if (started)
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
