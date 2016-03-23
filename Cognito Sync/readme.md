# Amazon Cognito Sync sample



This sample shows how to store data in the cloud using Amazon Cognito Sync. The sample demonstrates storing two text fields: player name and player alias.

#####Configure the Cognito Sync Sample
1. Open the CognitoSyncManagerSample scene file.
2. Click "CognitoSyncManagerSample" game object in "Hierarchy Pane".
3. Add "Identity Pool Id" in "Inspector Pane". A pool can be created in the [Cognito console]( https://console.aws.amazon.com/cognito/home).
4. If your Cognito Identity Pool is in a region other than "us-east-1", change the "Region" value in "Inspector Pane".
5. Set up a "link.xml" file as described on the [Unity SDK readme](https://github.com/aws/aws-sdk-net/blob/master/Unity.README.md#unity-sdk-fundamentals), which is necessary if you will be building with assembly stripping or IL2CPP. Be sure to add the line `<assembly fullname="AWSSDK.CognitoSync" preserve="all"/>`.


#####Run the sample scene
1. Click the "Run" button. 

2. When the "Save offline" button is pressed, the two fields are stored in a Cognito Sync dataset, but not pushed to the cloud yet. This is the way you persist data locally.

3. The "Sync with Amazon Cognito" button will synchronize (push and/or pull) the changes between your local dataset and the cloud.

4. "Delete local data" will wipe the local dataset, but you will still be able to recover the data from the cloud by synchronizing.
