#AWS SDK for Unity Kinesis Streams Sample

####This sample demonstrates how to use Amazon Kinesis Streams with the AWS Mobile SDK for Unity.

#####Configure the Kinesis Streams Sample
Note: To run the Kinesis Streams Sample you will need to create a Cognito Identity Pool, to handle authentication with AWS.  A pool can be created on the [Cognito console]( https://console.aws.amazon.com/cognito/home). You will also need an Amazon Kinesis Stream. You can be create a Kinesis Stream on the [Kinesis console]( https://console.aws.amazon.com/kinesis/home) by clicking the "Create Stream" button.

1. Open the KinesisExample scene file.
2. Click the "Kinesis" game object in "Hierarchy Pane".
3. Add your Identity Pool Id to the "Identity Pool Id" field in "Inspector Pane".
4. If your Cognito Identity Pool is in a region other than "us-east-1", change the "Cognito Identity Region" value in "Inspector Pane".
5. If you are using a region other than "us-east-1" for Kinesis, you should change the "Kinesis Region" value as well.
6. You might start with a landscape view, which may crop some of the example's UI. If this is the case, go to your Unity Build Settings, select Android or iOS, then press the "Switch Platforms" button.
7. Set up a "link.xml" file as described on the [Unity SDK readme](https://github.com/aws/aws-sdk-net/blob/master/Unity.README.md#unity-sdk-fundamentals), which is necessary if you will be building with assembly stripping or IL2CPP. Be sure to add the line `<assembly fullname="AWSSDK.Kinesis" preserve="all"/>`.


#####Run the sample scene
1. Click the "Run" button.
2. In the game view, you can press three buttons.
       1. "List Streams" will list all of the streams that your Cognito Identity has access to.
       2. "Describe Stream" will retrieve and print information about the stream specified in the "Stream Name" Text Input Field.
       3. "Put Record" will send the data specified in the "Record Data" Input Text Field to the Kinesis Stream specified in the "Stream Name" Text Input Field.

