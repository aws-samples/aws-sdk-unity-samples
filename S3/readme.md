#AWS SDK for Unity S3 Sample

####This sample demonstrates how to use Amazon S3 with the AWS Mobile SDK for Unity. 

#####Configure the S3 Sample
Note: To run the S3 Sample you will need to create a Cognito Identity Pool, to handle authentication with AWS.  A pool can be created on the [Cognito console]( https://console.aws.amazon.com/cognito/home).
1. Open the S3Example scene file.
2. Click "S3" game object in "Hierarchy Pane".
3. Add "Identity Pool Id","S3 Bucket Name" and "Sample File Name" in "Inspector Pane".


#####Run the sample scene
1. Click the "Run" button. 
2. In the game view, you can press five buttons. 
	1. "List Objects" will list all objects in the specified S3 bucket. 
	2. "List Buckets" will list all S3 buckets. 
	3. "Post Object" will upload an object to the S3 bucket via browser-based POST. 
	4. "Get Object" will download an object from a bucket. 
	5. "Delete object" will delete an object in a bucket.
