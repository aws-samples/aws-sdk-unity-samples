#AWS SDK for Unity Mobile Analytics Sample

###How to Run the Amazon Mobile Analytics Unity Sample

#####Setting up the SDk and Samples
1. After importing the aws-sdk-unity-mobile-analytics-x.x.x.x.unitypackage, In the Project window, navigate to the AWSSDK/examples/Mobile Analytics folder.
2. Open the scene called AmazonMobileAnalyticsSample. 
3. Click on the AmazonMobileAnalyticsSample game object.
4. Specify your App Id (created in the Amazon Mobile Analytics console at https://console.aws.amazon.com/mobileanalytics/home/?region=us-east-1#/overview?consoleState=management) in the "App Id" field.
5. Specify your Cognito Identity Pool Id (created using the Cognito console at https://console.aws.amazon.com/cognito/home?region=us-east-1#) in the "Cognito Identity Pool Id" field.

#####Run the sample scene
1. Click the "Run" button. 
2. In the game view, you can press two buttons. One will record a custom event and the other will record a monetization event. The sample records session events in the OnApplicationFocus method of the AmazonMobileAnalyticsSample class. 
Note: The AWS Mobile Unity SDK sends events every 60 seconds. After clicking on one of the buttons to record an event, you need to wait up to 60 seconds for the events to be delivered (you should see a console message stating successful delivery when this happens). Once sent, it will take up to an hour for the events to show up on the Amazon Mobile Analytics console.

