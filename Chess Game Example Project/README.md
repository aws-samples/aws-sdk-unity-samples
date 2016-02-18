# AWS Unity Chess Game Sample
This is a chess game built with [Unity](http://unity3d.com/), using the [AWS Mobile SDK for Unity](https://github.com/aws/aws-sdk-unity). This sample's purpose is to demonstate how to add a wealth of backend features to a game by leveraging the power of AWS. 

## Why Are You Here?
I'm assuming you are here to learn how to make use of the AWS Mobile SDK for Unity in a game. If you want to learn by following a step-by-step tutorial that will take you from start to finish, you should ignore the [Quick Start](#quick-start) section below and skip to the [tutorial](#tutorial) section. If you would rather learn by taking the minimal amount of steps to get the game running, so that you can tinker around with the working sample, continue into the [Quick Start](#quick-start) section.

## Quick Start
This *quick start* assumes a familiarity with some AWS tools. If at any point you feel you are missing some prerequisite knowledge, head to the included [tutorial](#tutorial) section below for more context.

### Things to Download
* You need [Unity 5.2.2](http://unity3d.com/get-unity/download?) or greater to open and run the sample. 
* For the Amazon Simple Notification portion of this sample to work on Android, you will need `google-play-services.jar`, which you can find in the location `<android-sdk>/extras/google/google_play_services/libproject/google-play-services_lib/libs/`. Copy this jar file to the `ChessGame/Assets/Plugin/Android` folder in the project.

### Things to Create
#### AWS Account
If you do not already have an AWS account, you can create one [here](https://aws.amazon.com/getting-started/), and take advantage of the 
[AWS Free Usage Tier](http://aws.amazon.com/free/).

#### Amazon DynamoDB and AWS Lambda
Create a new **CloudFormation Stack** in the [AWS Cloud Formation Console](https://console.aws.amazon.com/cloudformation/home#/stacks/new) using the included `ChessGameCloudFormationTemplate.json` template. This will set up the three DynamoDB tables and two Lambda Functions that you need. You will be prompted for one parameter when creating the stack, `SNSRegion`, which you should set to the AWS Region Endpoint in which you have created your SNS Applications (or in which you intend to create them, see below).

The rest of the resources that are needed for the game cannot be created by CloudFormation (at least at the time this is being written).

#### Amazon DynamoDB Stream With AWS Lambda
The only remaining step with Lambda and DynamoDB is to allow one of our Lambda Functions to react to a DynamoDB Stream. From the `NewMoveNotifier` Lambda function (you can find this function's full name in the output of the CloudFormation stack), add an event source with `DynamoDB` as the *event source type* and `ChessMatches`as the *DynamoDB* table.

#### Amazon Cognito
You need an [Amazon Cognito](https://console.aws.amazon.com/cognito/home) Identity pool named `Chess`, with [Facebook as an authenticated provider](https://docs.aws.amazon.com/cognito/devguide/identity/external-providers/facebook/), and the following policy for authenticated and unauthenticated identities, with each instance of `<RESOURCE REGION>` replaced with the region you are using for the corresponding resource: 

```
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Effect": "Allow",
            "Action": [
                "dynamodb:GetItem",
                "dynamodb:PutItem",
                "dynamodb:Query",
                "dynamodb:UpdateItem",
                "dynamodb:DescribeTable"
            ],
            "Resource": [
                "arn:aws:dynamodb:<RESOURCE REGION>:*:table/ChessMatches",
                "arn:aws:dynamodb:<RESOURCE REGION>:*:table/ChessPlayers",
                "arn:aws:dynamodb:<RESOURCE REGION>:*:table/SNSEndpointLookup",
                "arn:aws:dynamodb:<RESOURCE REGION>:*:table/ChessMatches/index/*"
            ]
        },
        {
            "Effect": "Allow",
            "Action": [
                "lambda:InvokeFunction"
            ],
            "Resource": [
                "arn:aws:lambda:<RESOURCE REGION>:*:function:*NewChessMatch*"
            ]
        },
        {
            "Effect": "Allow",
            "Action": [
                "sns:CreatePlatformEndpoint"
            ],
            "Resource": [
                "arn:aws:sns:<RESOURCE REGION>:*:app/APNS_SANDBOX/ChessGame",
                "arn:aws:sns:<RESOURCE REGION>:*:app/GCM/ChessGame"
            ]
        },
        {
            "Effect": "Allow",
            "Action": [
                "mobileanalytics:PutEvents",
                "cognito-sync:*"
            ],
            "Resource": [
                "*"
            ]
        }
    ]
}
```

##### Amazon Mobile Analytics
You will need an [Amazon Mobile Analytics](https://console.aws.amazon.com/mobileanalytics/home/) App, with a name of your choosing.

##### Amazon Simple Notification Service
You will need two [Amazon Simple Notification Service](https://console.aws.amazon.com/sns/v2/home) Applications: one for each [Android](http://docs.aws.amazon.com/sns/latest/dg/mobile-push-gcm.html) and [iOS](http://docs.aws.amazon.com/sns/latest/dg/mobile-push-apns.html). If you only intend to build for one platform, feel free to skip creating a SNS application for the other platform. Also keep track of the **Google Project Number** for the project used to create your Android SNS Application.

#### Configuring the Game to use Your Resources
Now that you have all of the resources required, open up `ChessGame/Assets/ChessGameScripts/ChessNetworkManager.cs` and fill in the following lines to refer to your resources:

```
private const string CognitoIdentityPoolId = null;
private const string MobileAnaylticsAppId = null;

// Needed only when building for Android
private const string AndroidPlatformApplicationArn = null;
private const string GoogleConsoleProjectId = null;

// Needed only when building for iOS
private const string IOSPlatformApplicationArn = null;

private const string NewMatchLambdaFunctionName = "NewChessMatch";
```

You can find the `NewMatchLambdaFunctionName` value in the output of the CloudFormation stack.

If all your resources are in the same Region, change the region value in `ChessGame/Assets/AWSSDK/src/Core/Resources/awsconfig.xml`, which has the default `region ="us-east-1"`. If your resources have different endpoints, change the lines in `ChessNetworkManager.cs` in the following way:

```
private static readonly RegionEndpoint _cognitoRegion = null; // If null, the ChessNetworkManager uses the value from awsconfig.xml for the corresponding service.
private static readonly RegionEndpoint _mobileAnalyticsRegion = RegionEndpoint.APNortheast1; // ap-northeast-1
private static readonly RegionEndpoint _dynamoDBRegion = RegionEndpoint.USEast1; // us-east-1
private static readonly RegionEndpoint _lambdaRegion = null;
private static readonly RegionEndpoint _snsRegion = null;
```

### Build and Run
Open up the project in Unity, go to *Build Settings*, and drag in all the scenes in `ChessGame/Assets/ChessGameScenes` into *Scenes In Build*. Make sure the `PersistentObjectInit` is first in the *Scenes In Build* order. 

Now you can build and run for iOS or Android, or run the game in the Unity editor. If running in the Unity editor, make sure you have the `PersistentObjectInit` scene open when you press play.

# Tutorial - Building a Cross-Platform Mobile Game Using the AWS Mobile SDK for Unity
There are many reasons for the unprecedented success of mobile games and apps. The convenience and power of phones and tablets have improved at breakneck speed, and advances in cellular networking have dramatically increased the networking capabilities of these ubiquitous devices. With its powerful tools for graphics, physics, multiplatform support, and more, Unity does a lot of the heavy lifting and allows developers to bring their ideas to life. However, when developers decide they want custom back-end services in their game, it is easy to lose creative momentum when tasked with designing, developing, and hosting a back end capable of managing player identity, cross-platform saves, achievements, leaderboards, and push notifications. A great idea developed by a small team can quickly turn into a herculean effort that splits your team’s focus into developing and managing your game and the infrastructure that supports it.

Accessing the power, flexibility, and ease of use of AWS is the perfect solution to this problem. The AWS Mobile SDK for Unity allows developers to easily connect their Unity game to Amazon Web Services.

In this tutorial, we will consider a chess game I have created with Unity, and show how I have integrated the AWS Mobile SDK for Unity to give it awesome features.

## What We Will Add
For this example, we will add the following features to the chess game:

* Identity management using Amazon Cognito Identity.
* Cross-device syncing of a user's data using Amazon Cognito Sync.
* Creation of new multiplayer matches using AWS Lambda and Amazon DynamoDB.
* Saving and loading of public information and multiplayer matches using Amazon DynamoDB.
* Reacting to game saves to notify players it's their turn using AWS Lambda, Amazon DynamoDB Streams, and Amazon Simple Notification Service.
* Measuring game usage using Amazon Mobile Analytics.


## First Things First
Before we get to the good stuff, you will need some resources.

* [Here](https://github.com/awslabs/aws-sdk-unity-samples/tree/master/Chess%20Game%20Example%20Project) is the project in its final state. This tutorial is best followed with the **ChessGame** Unity project opened in the Unity Editor and the code open in your favorite editor or IDE. Take a look at the **Assets** folder:
	* The **AWSSDK** folder includes the parts of the AWS Mobile SDK for Unity we will be using for this game. When you make your own game, all you have to do is [download the AWS Mobile SDK for Unity](https://s3.amazonaws.com/aws-unity-sdk/latest/aws-unity-sdk.zip) and import the Unity packages for the services you are using. 
	* **ChessGameScenes** ccontains the Unity scenes that make up our game. Start the **PersistentObjectInit** scene first. It creates objects that exist throughout the execution of the application. This includes `AWSPrefab`, which any Unity game must have present to use the AWS Mobile SDK and `ChessNetworkManager`, which we will talk about throughout this tutorial. The rest of the scenes are fairly self-explanatory. For example, the **MainMenu** scene is the UI screen the user sees first. The **Board** scene displays a chess board for the match the user is playing.
	* **ChessGameScripts** contains C# files that drive the functionality of this sample game. The **BasicUI** and **ChessLogic** subfolders contain logic that describes the way our UI behaves and the logic of an actual chess match, respectively. We will mostly ignore these folders for this tutorial, because they are not directly related to using the AWS Mobile SDK. The remaining files are as follows:
		* `ChessNetworkManager` describes a Unity component that exists during the entire execution of the game. As the name implies, this game object handles all of the networking for the game. As you might have assumed, we do all of this networking using the AWS Mobile SDK, and most of this tutorial focuses on the `ChessNetworkManager` file.
		* `GameManager` is a singleton class that manages the state of the game. This includes deciding when and how to use the `ChessNetworkManager` so that individual scenes can be ignorant of how the network is used.
		* `GameState` encapsulates all of the application state information we want to save. Before integrating a back end, this class defines which information to save to disk. When the back end is added, all we have to do is save it over the network instead.
		* `GCM` is the same C# file in the [SNS Example](https://github.com/awslabs/aws-sdk-unity-samples/blob/master/SNS/GCM.cs),It is used to communicate with the Google Cloud Messaging Java libraries. (We'll talk about these in Prerequisites.)
	*  The **LambdaFunctions** folder contains Node.js code for our AWS Lambda functions. 

## Prerequisites
This game sample is built using Unity 5.2.2 and the AWS Mobile SDK for Unity 2.1.0.0. Do not use versions of Unity earlier than 5.2.2.

To get the game to work, you first need to set up your AWS services. We will go through these step by step, but this tutorial will not cover creating your AWS account. If you do not already have an AWS account, you can create one [here](https://aws.amazon.com/getting-started/), and take advantage of the [AWS Free Usage Tier](http://aws.amazon.com/free/).

For the Amazon Simple Notification portion of this sample to work on Android, you will need google-play-services.jar, which you can find in the location `<android-sdk>/extras/google/google_play_services/libproject/google-play-services_lib/libs/`. Copy the jar file to the `ChessGame/Assets/Plugin/Android` folder in the project. You can remove all SNS-related code from the game. Other functionalities do not depend on it.

## Regions
Throughout this tutorial, we will using the [AWS Console](https://console.aws.amazon.com/) to create our AWS resources. You'll see regions (e.g., N. Virginia, Ireland, Singapore) are displayed in the console's menu bar. The resources you create will be hosted in the region you choose, so you may want to choose one close to you (or, in the long term, to your customers). Because some services are available in certain regions only, you may have to create different resources in different regions.

All of the services we use in this tutorial are available in the `us-east-1` region. That is the way the sample is configured, but you can change the region.

To change the default region for all of your services, change the region value (`currently region="us-east-1"`) in `ChessGame/Assets/AWSSDK/src/Core/Resources/awsconfig.xml`. It is suggested that you use the same region for all services except for services that are unavailable in your preferred region. To have individual services use different regions, change the lines in `ChessNetworkManager.cs` in the following way:

```
private static readonly RegionEndpoint _cognitoRegion = null; // If null, the ChessNetworkManager uses the value from awsconfig.xml for the corresponding service.
private static readonly RegionEndpoint _mobileAnalyticsRegion = RegionEndpoint.APNortheast1; // ap-northeast-1
private static readonly RegionEndpoint _dynamoDBRegion = RegionEndpoint.USEast1; // us-east-1
private static readonly RegionEndpoint _lambdaRegion = null;
private static readonly RegionEndpoint _snsRegion = null;
```

Make sure to keep these values up-to-date as you create your services.

## User Identity
The first thing an online game needs is user identities. User identities are useful for keeping track of a user's information, allowing or restricting access to online assets, and much more. It's very simple to create user identities with Amazon Cognito Identity. Look in `ChessNetworkManager.cs` to find the following:

```
private CognitoAWSCredentials _credentials;

private CognitoAWSCredentials Credentials
{
    get
    {
        if (_credentials == null)
            _credentials = new CognitoAWSCredentials(
                      CognitoIdentityPoolId,
                      CognitoRegion);
            return _credentials;
    }
}
```

This CognitoAWSCredentials object is useful in many ways. For one, we can retrieve the unique user ID from it by calling `Credentials.GetIdentityId()`. We will also use it to access other AWS resources based on the permissions set for your Cognito identity pool.

You may be asking, "What Cognito identity pool and what permissions?" We are about to set that up in the [Cognito Console](https://console.aws.amazon.com/cognito/home)=.

In the console, choose **Create new identity pool**, name your pool "Chess," select **Enable access to unauthenticated identities**, and create the pool. We will add authenticated identities later.

You will be prompted to choose roles for identities in your pool. Create a role for each authenticated and unauthenticated identity. Edit each policy to look like the following, except with each instance of `<RESOURCE REGION>` replaced with the region you are using for the corresponding resource (for example, `us-east-1`):

```
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Effect": "Allow",
            "Action": [
                "dynamodb:GetItem",
                "dynamodb:PutItem",
                "dynamodb:Query",
                "dynamodb:UpdateItem",
                "dynamodb:DescribeTable"
            ],
            "Resource": [
                "arn:aws:dynamodb:<RESOURCE REGION>:*:table/ChessMatches",
                "arn:aws:dynamodb:<RESOURCE REGION>:*:table/ChessPlayers",
                "arn:aws:dynamodb:<RESOURCE REGION>:*:table/SNSEndpointLookup",
                "arn:aws:dynamodb:<RESOURCE REGION>:*:table/ChessMatches/index/*"
            ]
        },
        {
            "Effect": "Allow",
            "Action": [
                "lambda:InvokeFunction"
            ],
            "Resource": [
                "arn:aws:lambda:<RESOURCE REGION>:*:function:NewChessMatch"
            ]
        },
        {
            "Effect": "Allow",
            "Action": [
                "sns:CreatePlatformEndpoint"
            ],
            "Resource": [
                "arn:aws:<RESOURCE REGION>:*:app/APNS_SANDBOX/ChessGame",
                "arn:aws:<RESOURCE REGION>:*:app/GCM/ChessGame"
            ]
        },
        {
            "Effect": "Allow",
            "Action": [
                "mobileanalytics:PutEvents",
                "cognito-sync:*"
            ],
            "Resource": [
                "*"
            ]
        }
    ]
}
```
This policy allows any identity in your Cognito identity pool access to the resources we will create in the tutorial.

From the [Cognito Console](https://console.aws.amazon.com/cognito/home), choose the link for your "Chess" identity pool, choose the Edit identity pool button, and there you will find your identity pool ID. It should look something like this:

```
us-east-1a2b3c4c5-6789-abcd-0123-012345abcdef
```

Back in `ChessNetworkManager.cs`, find the declaration of the `CognitoIdentityPoolId` string, and provide your identity pool ID as the value. For example: 

```
private const string CognitoIdentityPoolId = "us-east-1a2b3c4d5-6789-abcd-0123-012345abcdef";
```

We've set up the user identity, so let's do something with it.

## Syncing User Data
A common use case for mobile games is saving user data in the cloud and synchronizing it across devices. For our chess game, we want to allow users to play a local game on their phone, and then switch to their tablet and pick up right where they left off.

Amazon Cognito Sync makes this easy. Because our Cognito identity pool is ready to go with a policy that allows access to Cognito Sync, we can dive right into the code in `ChessNetworkManager.cs`. Creation of the `CognitoSyncManager` is as simple as providing our credentials and region endpoint:

```
new CognitoSyncManager(Credentials, new AmazonCognitoSyncConfig { RegionEndpoint = CognitoRegion });
```

All of our AWS-related managers, clients, and contexts are created in a similar fashion. When you see references to `AnalyticsManager`, `DynamoDBClient`, `DynamoDBContext`, `DynamoDBContext`, `LambdaClient`, and `SNSClient` in this tutorial, take a look at their declarations and initializations first.

###Using the `CognitoSyncManager`

To understand how the chess game uses the `CognitoSyncManager`, take a look at the `SaveGameStateLocal` method in `ChessNetworkManager.cs`. In this method, we locally cache the current state of our game in a `Dataset` object. A `Dataset` allows us to store key-value pairs called `Records`, which we can synchronize with Cognito Sync later. For our use case, we create two `Dataset` objects:


* One that stores all of our friends in which a friend's ID is the key and the friend's name is the value. (We'll talk about adding friends later.)
* One that stores all of our local matches with the match ID as the key and a JSON representation of the match as the value.

When a player makes a move in a local game, we call `SaveGameStateLocal`, but do not synchronize the `Datasets` with Cognito Sync until the user exists the **Board** scene.

To synchronize the dataset with Cognito Sync, we call the `Synchronize()` method on the dataset. We also want to define the behavior of our game when the synchronization succeeds or fails. To see the syntax for defining this behavior, check out the `SynchronizeLocalDataAsync()` method in `ChessNetworkManager.cs`. In the case of our game, if the synchronization fails, all we do is log a warning because it is most likely due to a lack of network connection. Even if this is the case, the locally cached data remains available to the user.

Finally, we want to be able to load data from Cognito Sync. Take a look at the `LoadGameStateAsync` method in `ChessNetworkManager.cs`, where we use the `Synchronize()` method again. When the synchronization is successful, we open the `Dataset` we are synchronizing and use all of the `Records` in it to re-create our `GameState` object. On failure, we re-create the `GameState` using the locally cached `Datasets` instead.

We are now able to call these functions from the `GameManager` singleton when it determines it is a good time to save, sync, or load.

### User Sign-In
 Our credentials are valid only for the device they are created from, because we haven't given Cognito Identity a way to know which devices should use the same identity. The solution is to use authenticated identities. That is, we will have the user sign in using either a public identity provider like Facebook or a custom one. For this example, we use Facebook. To learn how to get your `Chess` Cognito identity pool set up to use Facebook as an authenticated provider, head over to [this page](https://docs.aws.amazon.com/cognito/devguide/identity/external-providers/facebook/). Check out the `LogInToFacebookAsync` and `FacebookLoginCallback` methods in `ChessNetworkManager.cs` to see how we use the Facebook SDK to get an access token and register that token with our Cognito credentials. The `LoginToFacebook` method is called when the **Sign In** button on the main menu is pressed. Now, when a user is signed in on multiple devices, we have full-featured, cross-device synchronization!

## Online Matches
Online multiplayer adds a whole new dimension to any game. Because chess is a turn-based game, a player can make a move whenever it is the player's turn, whether that be seconds or days after the player's opponent. This sort of multiplayer experience is popular with mobile games because users are often on the go and unable to play a full game in one session.

### Creating a Match Using AWS Lambda
 AWS Lambda is a powerful and versatile tool for creating back-end services, and it is immeasurably useful for building an online game. We use Lambda a couple ways in this sample, but the possible uses are endless. At the end of this tutorial, take a moment to consider how you might use Lambda to make the game more robust, secure, and full-featured.

The first step is to create an AWS Lambda function. For this sample, we are going to write our Lambda functions in [Node.js](http://docs.aws.amazon.com/lambda/latest/dg/authoring-function-in-nodejs.html), but you can choose [Node.js](http://docs.aws.amazon.com/lambda/latest/dg/authoring-function-in-nodejs.html), [Java](http://docs.aws.amazon.com/lambda/latest/dg/java-lambda.html), [Python](http://docs.aws.amazon.com/lambda/latest/dg/python-lambda.html), or even another JVM language like [Clojure](https://aws.amazon.com/blogs/compute/clojure/) for your game!

In the [Lambda console](https://console.aws.amazon.com/lambda/home), choose **Create a Lambda function**. When prompted to select a blueprint, choose **Skip**. On the **Configure function** page, name the function "NewChessMatch" and use the Node.js runtime. The easiest way to upload our code is to choose **Edit code** inline. Open the `NewChessMatch.js` file in the **LambdaFunctions** folder and just copy and paste it.

Before we finish our Lambda function configuration, we need to choose a role. This role will give the function access to the AWS resources it needs. This is separate from the Cognito Identity role. Our "NewChessMatch" function will need to access a DynamoDB table to create an entry for a new game (we'll talk about defining our DynamoDB table later), so let's add a "Basic with Dynamo" role. Under **Create new role**, from the **Role** drop-down list, choose "Basic with Dynamo". On the page that appears, view the new policy, and then choose **Create**. Choose **Next**, and then choose **Create function**. You now have an AWS Lambda function ready to invoke.

But wait! If you look in the Lambda function code, you will see it reads from and writes to a DynamoDB table called "ChessMatches." Let's create this table.

#### Creating a DynamoDB Table to Store Chess Matches

We want a table that holds the following information:

* A unique match ID.
* The ID of the player using white chess pieces.
* The ID of the player using black chess pieces.
* The Forsyth-Edwards Notation (FEN) that describes the state of the board.
* The long algebraic notation that describes the previous move.

We want to access the table in the following ways:

* Get all information about a match if we have the match ID.
* Insert or update matches.
* Get all match IDs in which a given player is either the white or black player.

To satisfy these requirements, we will design our table as follows:

* A string named `MatchId` as the **Primary Hash Key**. This means we can make a `GetItem` request on the table with a `MatchId` value, and get the item with that match ID.
* Two **Global Secondary Indexes** with **Index Hash Keys** `WhitePlayerId` and `BlackPlayerId` and **Index Names** `WhitePlayerId-index` and `BlackPlayerId-index`, respectively. This allows us to query on the table to find all matches for `WhitePlayerId` and `BlackPlayerId` values.
* We don't have to explicitly define `AlgebraicNotation` and `FEN` keys. We just assume any item put into this table will contain values for these keys

Create this table in the [DynamoDB console](https://console.aws.amazon.com/dynamodb/home). Choose **Create Table** and name the table "ChessMatches." For **Primary Key Type**, choose **Hash**. For **Hash Attribute Name**, use `MatchId`. Make sure **string** is selected because our `MatchId` values are strings.

Continue to **Add Indexes**. For **Index Type**, choose **Global Secondary Indexes**. For **Index Hash Key**, type `WhitePlayerId`. The console should automatically determine your **Index Name** is `WhitePlayerId-index`. Choose **Add Index To Table**, repeat for `BlackPlayerId`, then choose Continue.

On **Provisioned Throughput Capacity**, accept the defaults and press **Continue**. On the next page, select or clear **Use Basic Alarms**. We will not cover them in this tutorial. Choose **Continue**, confirm your table looks as expected, and then choose **Create**.

#### Invoking the NewChessMatch function
Now that our Lambda function and DynamoDB table are set up, we can invoke the `NewChessMatch` Lambda function from our game to create a new match. In the `ChessNetworkManager.cs` file, take a look at the `NewMatchAsync` method. In this method, we create a JSON string that specifies the `requesterId` (the identity of the player who is creating the match) and `opponentId` (the identity of the opponent), and then use the `LambdaClient` to call `InvokeAsync` with a request that specifies our function name, parameters in JSON format, and invocation type so that Lambda knows we are waiting for a response. When we get a response, we pull out the `MatchId` we have defined our Lambda function to return.

### Adding Friends
We are able to invoke an AWS Lambda function to create a new match with a friend, and we are able to keep track of our friends with Cognito Sync, but we have yet to define a way to add friends. We are going to take a simple approach in which we have a publicly accessible DynamoDB table that maps from player identities (specifically, Cognito identities) to player names. For this sample, we will allow users to directly access the DynamoDB table to update their info or find friends' names if they know their friends' IDs.

Creating this table is simple. Like before, head to the [DynamoDB console](https://console.aws.amazon.com/dynamodb/home), and then create a table named `ChessPlayers` with **Primary Hash Key** `Id`.

In `ChessNetworkManager.cs`, take a look at the `PubliclyRegisterIdentityAsync` method, in which we use our `DynamoDBContext` object to perform `SaveAsync`. When calling `SaveAsync`, we specify the type `GameState.PlayerInfo`. If you take a look at the `PlayerInfo` class in `GameState.cs`, you will see some attributes that provide `DynamoDBContext` with information about how to save the object in DynamoDB:


* `[DynamoDBTable("ChessPlayers")]` means the object should be saved to the `ChessPlayers` table.
* `[DynamoDBHashKey]` applied to the `Id` property means the table's **Primary Hash Key** is named `Id`.
* `[DynamoDBProperty]` applied to the `Name` property means the name should be a property of the item put to the table.
* These attributes work the same way when loading from the table, making it just as easy to create a `PlayerInfo` object from a DynamoDB item as it is to create a DynamoDB item from a `PlayerInfo` object.

If the `SaveAsync` call is successful, we make a similar call to a different table. We will talk about this when we set up Amazon Simple Notification Service.

Also check out the `FindPlayerByIdAsync` in `ChessNetworkManager.cs`, in which we load a `PlayerInfo` object, if supplied, with an ID.

These methods are called by our `GameManager` singleton. `PubliclyRegisterIdentityAsync` is called when the user changes his/her name or identity. `FindPlayerByIdAsync` is called when the user clicks the **Add Friend** button in the **SettingsMenu** scene.

### Playing Online Matches
 e have friends and a match ready to be played, so we need to be able to find, save, and load the matches we are involved in.

Let's start by taking a look at the `SimpleMatchInfo` class in `ChessNetworkManager.cs`. You should recognize some of the attributes from our `PlayerInfo class`. In addition, there are `[DynamoDBGlobalSecondaryIndexHashKey]` attributes attached to the `BlackPlayerId` and `WhitePlayerId` properties, which makes sense based on the way we created our `ChessMatches` table. There is also a method in this class to create a `GameState.MatchState` object from the data in the `SimpleMatchInfo`.

Let's say we just created a new match with our `NewMatchAsync` method. This method provides us with the `MatchId` value, so we will load the match with that ID. As you might have expected, this is what the `LoadMatchAsync` method in `ChessNetworkManager.cs` is for. Just like we did in `FindPlayerByIdAsync`, we use `DynamoDBContext.LoadAsync` to get the item from our table and make a `SimpleMatchInfo` out of it. `SaveOnlineMatchAsync` should also look very familiar to `PubliclyRegisterIdentityAsync` because `DynamoDBContext.SaveAsync` is used in the same way.

All that's left is to use our `GetOnlineMatchesAsync` method to find all matches in which either `BlackPlayerId` or `WhitePlayerId` match the user's ID. We make two `DynamoDBContext.QueryAsync` calls: one for black player ID and one for white player ID. For each, we specify which **Global Secondary Index** we are querying by providing a `DynamoDBOperationConfig` like this:
 
```
new DynamoDBOperationConfig()
{
    IndexName = WhitePlayerDynamoDBIndexKey
}
```
For each of the two `AsyncSearch` objects, we get all of the matches. We can now provide the user with the current state of all the matches he is playing, and allow the game to individually reload any match or update any match with a new move!

From the user's perspective, he/she selects a friend to create a new game in the **NewGameMenu** scene, and goes to the **Board** scene, which tells the user it is loading. While it waits for the Lambda function to create the match, the match with that ID is loaded from DynamoDB. The user then has a fresh game board. If it is his/her turn, the user makes a move, which saves the match to the DynamoDB table. The user's opponent can refresh the board to load the updated match state. It might be nice if we were able to notify a user when it is his/her turn...

## Notify Users When It Is Their Turn
You guessed it! This section is about telling a user that his/her friend has made a move and it's now his/her turn to prove he/she is a real chess master. We are going to use these tools to do this: AWS Lambda, a new Amazon DynamoDB table, Amazon DynamoDB streams, and Amazon Simple Notification Service. The plan is as follows:

1. Create a Google project with **Google Cloud Messaging** (GCM) access and an iOS app with **Apple Push Notification Service** (APNS) access.
2. Create an **Amazon Simple Notification Service** (SNS) application that targets our GCM and APNS applications.
3. Use the AWS Mobile SDK for Unity to create a SNS endpoint ARN for the user's device with those applications.
4. Create a DynamoDB table that maps from user identity to the SNS endpoint ARNs for that user's devices.
5. Create a Lambda function that responds to changes in our ChessMatches tables, determines which player to notify, and sends a notification through SNS, based on the endpoint ARNs in the DynamoDB table.


####Creating SNS Applications
We will refer you to some existing documentation for steps 1 and 2. For GCM, follow the first two steps [here](http://docs.aws.amazon.com/sns/latest/dg/mobile-push-gcm.html). For APNS, follow the first three steps [here](http://docs.aws.amazon.com/sns/latest/dg/mobile-push-apns.html). (You don't need to create a GCM or APNS application if you do not intend to develop for Android or iOS.) Register each with SNS by following the steps [here](http://docs.aws.amazon.com/sns/latest/dg/mobile-push-send-register.html). Finally, you should have the Android platform application ARN and iOS platform application ARN from SNS, as well as the Google console project ID from the Google Developers console (assuming you are developing for both platforms). Provide these values in `ChessNetworkManager.cs`:

```
// Needed only when building for Android
private const string AndroidPlatformApplicationArn = "arn:aws:sns:us-east-1:654321123456:app/GCM/ChessGame";
private const string GoogleConsoleProjectId = "1234567891011";

// Needed only when building for iOS
private const string IOSPlatformApplicationArn = "arn:aws:sns:us-east-1:654321123456:app/APNS_SANDBOX/ChessGame";
```

####Registering a Device with SNS
Check out the `RegisterDeviceAsync` code in `ChessNetworkManager.cs` to see how we use the `SNSClient`, the iOS notification services, and GCM utilities to register a device. We receive the `SNSEndpointARN` and keep a reference to it in `ChessNetworkManager` so we can write its value to a DynamoDB table next time `PubliclyRegisterIdentityAsync` is called.

####Keeping Track of a Tser's SNS Endpoints
We need a DynamoDB table that can hold zero-to-many SNS endpoint ARNs for a given user identity. Create a table named `SNSEndpointLookup` with a **Primary Key Type** of **Hash** and **Range Key** where the **Hash Attribute Name** is `PlayerId` and the **Range Attribute Name** is `SNSEndpointARN`. This allows us to put multiple items with the same player ID to the table as long as they have different SNS endpoint ARNs. We use the AWS Mobile SDK for Unity to save these items to DynamoDB the same way we did with `GameState.PlayerInfo` and `SimpleMatchInfo` objects, but now with `SNSEndpointLookupEntry` objects, which are defined in `ChessNetworkManager.cs`. The code for this update is in the `PubliclyRegisterIdentityAsync` method. Note that it is only executed if the update to the user's `PlayerInfo` succeeds.

####Detecting When to Notify the User
We want to notify the user when it's his/her turn in a match.

DynamoDB and Lambda work together when this is the case. DynamoDB offers a functionality called DynamoDB Streams and Lambda has the ability to react to DynamoDB stream events, so we are able to set up a Lambda function that performs an action whenever an `INPUT` or `MODIFY` operation occurs in the `ChessMatches` table.

Go back to the [Lambda console](https://console.aws.amazon.com/lambda/home), to create a new function. It will be different from our other Lambda function in the following ways:

* Name the function `NewMoveNotifier`.
* Use the code in `LambdaFunctions/NewMoveNotifier.js`. You may have to change the values for `SNS_REGION` and `DDB_REGION` if you are not using us-east-1 for SNS or DynamoDB.
* Create a new role.
	* This time, we are going to make a custom role to allow us to use the SNS and DynamoDB resources we need access to. Start by creating "DynamoDB event stream."

    Next, in the [IAM Management Console](https://console.aws.amazon.com/iam/home#roles), select the role you created, choose **Edit Policy**, and then modify the policy to look like this:
	
		```
	    {
	      "Version": "2012-10-17",
	      "Statement": [
	        {
	          "Effect": "Allow",
	          "Action": [
	            "lambda:InvokeFunction"
	          ],
	          "Resource": [
	            "*"
	          ]
	        },
	        {
	          "Effect": "Allow",
	          "Action": [
	            "dynamodb:GetRecords",
	            "dynamodb:GetShardIterator",
	            "dynamodb:DescribeStream",
	            "dynamodb:ListStreams",
	            "dynamodb:Query",
	            "sns:Publish",
	            "logs:CreateLogGroup",
	            "logs:CreateLogStream",
	            "logs:PutLogEvents"
	          ],
	          "Resource": "*"
	        }
	      ]
	    }
		```
	
* After the Lambda function is created, go to its **Event Sources** tab and choose **Add event source**. For the event source type, use DynamoDB. For the `DynamoDB` table, use `ChessMatches`. For **Enable event source**, make sure enable now is selected.

Skim through the Node.js code, or at least read its comments, to get an idea of how the DynamoDB event stream is processed and how an SNS message is published from it.

## Measuring Usage
 There is one last thing to add, and it won't take much work at all: Amazon Mobile Analytics. With Amazon Mobile Analytics, we are able to record and view how our users are using the application. In `ChessNetworkManager.cs`, you can see `OnApplicationFocus`, which is a method called by Unity on game components when the application gains or loses focus. All we have to do is call `AnalyticsManager.ResumeSession()` or `AnalyticsManager.PauseSession()` for these events to be recorded in Mobile Analytics. When we create the `AnalyticsManager` (when `ChessNetworkManager` awakes) the start of the session is automatically created. For this reason, we do not call `ResumeSession` the first time the application gains focus, because it doesn't make sense to resume a session that has started and not yet been paused. With the `AnalyticsManager`, you also have the option to send custom events, not just pause and resume.

Now that we have the code in place, all that's left to do is add an app in the [Mobile Analytics console](https://console.aws.amazon.com/mobileanalytics/home). Give the app any name you want. After it's created, add the app ID (which you can find [here](https://console.aws.amazon.com/mobileanalytics/home/?#/overview?consoleState=management)) to `ChessNetworkManager.cs`, like this:

```
private const string MobileAnaylticsAppId = "1234567890abcdefgh1234567890abcd";
```

## Build and run
Open the project in Unity, go to **Build Settings**, and drag in all of the scenes in `ChessGame/Assets/ChessGameScenes` into **Scenes In Build**. Make sure the `PersistentObjectInit` is first in the **Scenes In Build** order.

Now you can build and run for iOS or Android, or run the game in the Unity editor. If you run it in the Unity editor, make sure you have the `PersistentObjectInit` scene open when you press play.


## That's it?
That's it (for the scope of this sample)! We now have a game with a ton of networking functionality that takes advantage of Cognito Identity and Cognito Sync, Lambda, DynamoDB, SNS, and Mobile Analytics.

## What's next?
There is still some work to do to get your game ready for release and infinite possibilities for adding more cool features.

Our game is set up to do all of the checking for legal chess moves client-side. This opens up a lot of opportunities for users to cheat. Consider how we could use some of the tools we've talked about to keep users from cheating, and take a look [here](http://mobile.awsblog.com/post/TxH3SWPR48HGAO/Using-Amazon-Cognito-and-AWS-Lambda-to-Detect-Cheating).

Also, in this sample, we make users manually copy and paste their friends' IDs to add them. When making your own game, consider integrating the Facebook SDK or other social network tools to make finding and adding friends easy on the user. Or, you could use AWS to design a way for users to create unique user names.

Leaderboards? Tournaments? Watching other players' matches? Trophies and badges? Get creative and use Cognito, Lambda, DynamoDB, SNS, Mobile Analytics, and other AWS services to solve interesting problems and create awesome games!

