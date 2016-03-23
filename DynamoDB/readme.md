#AWS SDK for Unity DynamoDB Sample

The AWS SDK for Unity DynamoDB  uses the new UI Event System, so it requires Unity version >4.6 to run the sample. 

#####Configure the DynamoDB Sample
Open the HighLevelExample, LowLevelDynamoDBExample, and the TableQueryAndScanExample scenes and perform the following for each:


* Click the “HighLevel”, “LowLevel”, or “QueryAndScan” game object in "Hierarchy Pane".
* Add "Identity Pool Id" in "Inspector Pane". A pool can be created in the [Cognito console](https://console.aws.amazon.com/cognito/home).
* If your Cognito Identity Pool is in a region other than "us-east-1", change the "Cognito Identity Region" value in "Inspector Pane".
* If you want to use a region other than "us-east-1" for DynamoDB, you can change the "Dynamo Region" value as well.
* Set up a "link.xml" file as described on the [Unity SDK readme](https://github.com/aws/aws-sdk-net/blob/master/Unity.README.md#unity-sdk-fundamentals), which is necessary if you will be building with assembly stripping or IL2CPP. Be sure to add the line `<assembly fullname="AWSSDK.DynamoDBv2" preserve="all"/>`.



Then in the Unity Menu go to File->Build Settings and in ‘Scenes to Build’, add the scenes in the following order


    1. DynamoDbExample.unity
    2. LowLevelDynamoDbExample.unity
    3. TableQueryAndScanExample.unity
    4. HighLevelExample.unity   

This will allow you to execute all the operation as a single game.

* The project will create 4 tables - ProductCatalog, Forum, Thread & Reply. Make sure that these tables are not already present. You can check the tables at the [DynamoDB console]( https://console.aws.amazon.com/dynamodb/).



#####Run the sample scene
1. Open the DynamoDbExample.unity scene.
2. Click the “Run” button

