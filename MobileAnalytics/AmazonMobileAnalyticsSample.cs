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
using System.Collections;
using Amazon;
using Amazon.MobileAnalyticsManager.Event;
using Amazon.MobileAnalyticsManager;
using Amazon.MobileAnalyticsManager.ClientContext;
using Amazon.MobileAnalyticsManager.Delivery;
using Amazon.Unity3D;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using System.Collections.Generic;
using System.Text;
﻿using UnityEngine;

public class AmazonMobileAnalyticsSample : MonoBehaviour
{
    
    bool started = false;
    
    public string appId = "YourAppId";
    public string cognitoIdentityPoolId = "YourPoolId";
    public AWSRegion cognitoRegion = AWSRegion.USEast1;
    public AWSRegion mobileAnalyticsRegion = AWSRegion.USEast1;

    private AmazonMobileAnalyticsManager analyticsManager;
    
    // Use this for initialization
    void Start()
    {
        // Set Unity SDK logging level
        AmazonLogging.Level = AmazonLogging.LoggingLevel.DEBUG;

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
        
        analyticsManager = AmazonMobileAnalyticsManager.GetOrCreateInstance(new CognitoAWSCredentials(cognitoIdentityPoolId, cognitoRegion.GetRegionEndpoint()),
                                                                            mobileAnalyticsRegion.GetRegionEndpoint(),appId);
        started = true;
    }


    void OnGUI ()
    {
        GUILayout.BeginArea (new Rect (0, 0, Screen.width * 0.5f, Screen.height));
        GUILayout.Label ("Amazon Mobile Analytics Operations");

        // record custom event
        if (GUILayout.Button ("Record Custom Event", GUILayout.MinHeight (Screen.height * 0.2f), GUILayout.Width (Screen.width * 0.4f)))
        {
            AmazonMobileAnalyticsEvent customEvent = new AmazonMobileAnalyticsEvent("level_complete");
            
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
            AmazonMobileAnalyticsMonetizationEvent monetizationEvent = new AmazonMobileAnalyticsMonetizationEvent();
            
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
        if(started)
        {
            if(focus)
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
