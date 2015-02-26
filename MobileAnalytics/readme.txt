Running the MobileAnalytics Sample
This sample demonstrates how to use Mobile Analytics in AWS Unity SDK. 

1. Config Sample
Double click "AmazonMobileAnalytics.unity" to open the scene. Single click "AmazonMobileAnalyticsSample" in hierarchy pane and then in inspector, add your app id, Cognito Identity pool id, Cognito region and Mobile Analytics region.
For Cognito config, you can get more info at
http://docs.aws.amazon.com/mobile/sdkforunity/developerguide/Unity-Plugin-Getting-Started.html#configuring-cognito and http://docs.aws.amazon.com/mobile/sdkforunity/developerguide/Unity-Plugin-Getting-Started.html#iam-roles-for-cognito .
For Mobile Analytics app id, you can see more information at http://docs.aws.amazon.com/mobileanalytics/latest/ug/set-up.html
For Mobile Analytics region, so far it only supports USEast1.


2. Run the Sample
Click "Run" botton. There are two buttons in UI. One is for recording custom event. Another is for recording monetization event. 
Pay attention: Mobile Analytics client sends events to server every 60 seconds. After you click buttons to record event, you need to wait 60 seconds to deliver the events and five to ten minutes to see the events in AWS console.