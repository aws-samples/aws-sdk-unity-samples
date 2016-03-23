#AWS SDK for Unity Mobile Analytics Sample

###How to Run the Amazon Mobile Analytics Unity Sample

#####Setting up the SDK and Samples
1. After importing AWSSDK.MobileAnalytics.x.x.x.x.unitypackage, in the Project window, navigate to the AWSSDK/examples/Mobile Analytics folder.
2. Open the scene called AmazonMobileAnalyticsSample. 
3. Click on the AmazonMobileAnalyticsSample game object.
4. Specify your App Id (created in the [Amazon Mobile Analytics console](https://console.aws.amazon.com/mobileanalytics/home/?region=us-east-1#/overview?consoleState=management)) in the "App Id" field.
5. Specify your Cognito Identity Pool Id (created using the [Cognito console](https://console.aws.amazon.com/cognito/home)) in the "Cognito Identity Pool Id" field.
6. If your Cognito Identity Pool is in a region other than "us-east-1", change the "Cognito Identity Region" value in "Inspector Pane".
7. If you are using a region other than "us-east-1" for Mobile Analytics, you should change the "Analytics Region" value as well.
8. Set up a "link.xml" file as described on the [Unity SDK readme](https://github.com/aws/aws-sdk-net/blob/master/Unity.README.md#unity-sdk-fundamentals), which is necessary if you will be building with assembly stripping or IL2CPP. Be sure to add the line `<assembly fullname="AWSSDK.MobileAnalytics" preserve="all"/>`.

#####Build and run the sample scene
1. Go to Build Settings, choose iOS or Android, press "Switch Platform", and finally "Build and Run".
2. When the application is built and deployed to your device, you can press two buttons. One will record a custom event and the other will record a monetization event. The sample records session events in the OnApplicationFocus method of the AmazonMobileAnalyticsSample class. 

Notes: 
* The AWS Mobile Unity SDK sends events every 60 seconds. After clicking on one of the buttons to record an event, you need to wait up to 60 seconds for the events to be delivered (you should see a console message stating successful delivery when this happens). Once sent, it will take up to an hour for the events to show up on the Amazon Mobile Analytics console.
* The SDK does not currently have support for running directly in the Unity Editor. When running in the editor, events will be sent, but they will be missing client context and therefore will not show up in the Mobile Analytics Console.

