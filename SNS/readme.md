#AWS SDK for Unity SNS Sample

####This sample demonstrates how to use Amazon SNS with the AWS Mobile SDK for Unity and send push notifications to Android & iOS 

#####Configure the SNS Sample
Note: To run the SNS Sample you will need to create a Cognito Identity Pool, to handle authentication with AWS.  A pool can be created on the [Cognito console]( https://console.aws.amazon.com/cognito/home).You will also need to create platform applications for Android & iOS to register the device token for iOS and Registration id for Android. The platform application can be created on the [SNS console](https://console.aws.amazon.com/sns/v2/home), you can refer SNS mobile push guidelines [here](http://docs.aws.amazon.com/sns/latest/dg/SNSMobilePush.html). 

1. Open the SNSExample unity scene
2. Click "SNSExample" game object in "Hierarchy Pane".
3. Add "Identity Pool Id","Android Platform Application Arn","iOS Platform Application Arn" and "Google Console Project Id" in "Inspector Pane". 
4. If your Cognito Identity Pool is in a region other than "us-east-1", change the "Cognito Identity Region" value in "Inspector Pane".
5. If you are using a region other than "us-east-1" for SNS, you should change the "SNS Region" value as well.
6. You will need google-play-services.jar & android-support-v4.jar to run this project. You can find the play services library in the location `<android-sdk>/extras/google/google_play_services/libproject/google-play-services_lib/`. Import this project to eclipse and export the jar . Make sure that you use the latest version of google play services. Copy these jar files to the Assets/Plugin/Android folder in the project.
7. For Android - if you are using the same AndroidManifest.xml as shown [here](https://github.com/aws/aws-sdk-unity/blob/master/Assets/Plugins/Android/AndroidManifest.xml) do a replace of all occurances of com.amazonaws.unity to <your package name>. This includes
8. Set up a "link.xml" file as described on the [Unity SDK readme](https://github.com/aws/aws-sdk-net/blob/master/Unity.README.md#unity-sdk-fundamentals), which is necessary if you will be building with assembly stripping or IL2CPP. Be sure to add the line `<assembly fullname="AWSSDK.SimpleNotificationService" preserve="all"/>`.


```
<manifest
    xmlns:android="http://schemas.android.com/apk/res/android"
    package="<your package name>"
...
<permission android:name="<your-package-name>.permission.C2D_MESSAGE"
      android:protectionLevel="signature" />
  <uses-permission android:name="<your-package-name>.permission.C2D_MESSAGE" />
...
 <receiver
        android:name="<package name for broadcastcast receiever>.GCMBroadcastReceiver"
...
 <category android:name="<your package name>" />
...
<service android:name="<packge name for service>.GCMIntentService" />
```

#####Run the sample scene
The SNS Sample can only run on the Android or iOS device. Go to File->Build Settings and switch the player to Android or iOS and Build & Run the project.
