How to Run the Amazon Mobile Analytics Unity Sample
This sample demonstrates how to use Amazon Mobile Analytics with the AWS Mobile SDK for Unity. 

Step 1: Load and configure the sample scene in the Unity editor 
1. Unzip the AWS Unity SDK package to a folder on your computer.
2. With your project open in the Unity Editor, select Assets/Import Package/Custom Package...
3. In the Import Package file selection dialog, navigate to the  unity packages subfolder and open the aws-unity-sdk-x.x.x-MobileAnalytics.unitypackage.
4. In the subsequent "Importing Package" dialog, ensure all the items are selected and then click on the "Import" button.
5. In the Project window, navigate to the AWSUnitySDKSample/MobileAnalytics folder.
6. Open the scene called AmazonMobileAnalyticsSample. 
7. Click on the AmazonMobileAnalyticsSample game object.
8. Specify your App Id (created in the Amazon Mobile Analytics console at https://console.aws.amazon.com/mobileanalytics/home/?region=us-east-1#/overview?consoleState=management) in the "App Id" field.
9. Specify your Cognito Identity Pool Id (created using the Cognito console at https://console.aws.amazon.com/cognito/home?region=us-east-1#) in the "Cognito Identity Pool Id" field.


Step 2: Run the sample scene
1. Click the "Run" button. 
2. In the game view, you can press two buttons. One will record a custom event and the other will record a monetization event. The sample records session events in the OnApplicationFocus method of the AmazonMobileAnalyticsSample class. 
Note: The AWS Mobile Unity SDK sends events every 60 seconds. After clicking on one of the buttons to record an event, you need to wait up to 60 seconds for the events to be delivered (you should see a console message stating successful delivery when this happens). Once sent, it will take up to an hour for the events to show up on the Amazon Mobile Analytics console.

