#AWS SDK for Unity Lambda Sample

####This sample demonstrates how to use Amazon Lambda with the AWS Mobile SDK for Unity. 

#####Configure the Lambda Sample
Note: To run the Lambda Sample you will need to create a Cognito Identity Pool, to handle authentication with AWS.  A pool can be created on the [Cognito console]( https://console.aws.amazon.com/cognito/home). You will also need an AWS Lambda. By default, this example uses an AWS Lambda function named "helloWorld", which can be created on the [Lambda console]( https://console.aws.amazon.com/lambda/home) by clicking the 'Create Lambda function' button and selecting the "hello-world" template.

1. Open the LambdaExample scene file. 
2. Click the "Lambda" game object in "Hierarchy Pane". 
3. Add your Identity Pool Id to the "Identity Pool Id" field in "Inspector Pane". 
4. If your Cognito Identity Pool is in a region other than "us-east-1", change the "Cognito Identity Region" value in "Inspector Pane".
5. If you are using a region other than "us-east-1" for Lambda, you should change the "Lambda Region" value as well.
6. Set up a "link.xml" file as described on the [Unity SDK readme](https://github.com/aws/aws-sdk-net/blob/master/Unity.README.md#unity-sdk-fundamentals), which is necessary if you will be building with assembly stripping or IL2CPP. Be sure to add the line `<assembly fullname="AWSSDK.Lambda" preserve="all"/>`.



#####Run the sample scene
1. Click the "Run" button. 
2. In the game view, you can press two buttons. 
	1. "Invoke" will invoke the Lambda function named in the 'Function Name' text field, with parameters described by the JSON string in the "Event Json' text field, then display the results at the bottom of the screen. 
	2. "List Functions" will list all of your Lambda functions.
