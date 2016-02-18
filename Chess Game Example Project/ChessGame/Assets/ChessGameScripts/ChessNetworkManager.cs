using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoSync;
using Amazon.CognitoSync.SyncManager;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.MobileAnalytics.MobileAnalyticsManager;
using ThirdParty.Json.LitJson;
using UnityEngine;
using Facebook.Unity;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System;

namespace AWSSDK.Examples.ChessGame
{
    public class ChessNetworkManager : MonoBehaviour
    {
        # region Constants and configuration values
        private const string CognitoIdentityPoolId = "";
        private const string MobileAnaylticsAppId = "";

        // Needed only when building for Android
        private const string AndroidPlatformApplicationArn = "";
        private const string GoogleConsoleProjectId = "";

        // Needed only when building for iOS
        private const string IOSPlatformApplicationArn = null;

        private const string NewMatchLambdaFunctionName = "NewChessMatch";
        private const string PlayersDatasetName = "KnownPlayers";
        private const string MatchesDatasetName = "Matches";
        private const string FriendIdPropertyName = "friendId";
        private const string SelfIsWhitePRopertyName = "selfIsWhite";
        private const string ForsythEdwardsNotationPropertyName = "fen";
        private const string AlgebraicNotationPropertyName = "algNot";
        private const string RequesterIdPropertyName = "requesterId";
        private const string OpponentIdPropertyName = "opponentId";
        private const string WhitePlayerDynamoDBIndexKey = "WhitePlayerId-index";
        private const string BlackPlayerDynamoDBIndexKey = "BlackPlayerId-index";

        // By default, we use the Region Endpoint specified in the
        // AWSSDK/src/Core/Resource/awsconfig.xml file. If you are using the same region for all of
        // your services, just change the region value in awsconfig.xml. Otherwise, you can
        // replace the null values below with the correct region endpoints,
        // i.e. RegionEndpoint.USEast1.
        private static readonly RegionEndpoint _cognitoRegion = null;
        private static readonly RegionEndpoint _mobileAnalyticsRegion = null;
        private static readonly RegionEndpoint _dynamoDBRegion = null;
        private static readonly RegionEndpoint _lambdaRegion = null;
        private static readonly RegionEndpoint _snsRegion = null;
        private RegionEndpoint CognitoRegion { get { return _cognitoRegion != null ? _cognitoRegion : AWSConfigs.RegionEndpoint; } }
        private RegionEndpoint MobileAnalyticsRegion { get { return _mobileAnalyticsRegion != null ? _mobileAnalyticsRegion : AWSConfigs.RegionEndpoint; } }
        private RegionEndpoint DynamoDBRegion { get { return _dynamoDBRegion != null ? _dynamoDBRegion : AWSConfigs.RegionEndpoint; } }
        private RegionEndpoint LambdaRegion { get { return _lambdaRegion != null ? _lambdaRegion : AWSConfigs.RegionEndpoint; } }
        private RegionEndpoint SNSRegion { get { return _snsRegion != null ? _snsRegion : AWSConfigs.RegionEndpoint; } }
        # endregion

        # region AWS Clients, Managers, and Contexts, And Info
        private CognitoAWSCredentials _credentials;

        private CognitoAWSCredentials Credentials
        {
            get
            {
                if (_credentials == null)
                    _credentials = new CognitoAWSCredentials(CognitoIdentityPoolId, CognitoRegion);
                return _credentials;
            }
        }

        private MobileAnalyticsManager _analyticsManager;

        private CognitoSyncManager _cognitoSyncManager;

        private IAmazonDynamoDB _ddbClient;

        private DynamoDBContext _ddbContext;

        private IAmazonLambda _lambdaClient;

        private IAmazonSimpleNotificationService _snsClient;

        private MobileAnalyticsManager AnalyticsManager
        {
            get
            {
                if (_analyticsManager == null)
                {
                    _analyticsManager = MobileAnalyticsManager.GetOrCreateInstance(MobileAnaylticsAppId, Credentials, MobileAnalyticsRegion);
                }
                return _analyticsManager;
            }
        }

        private CognitoSyncManager CognitoSyncManager
        {
            get
            {
                if (_cognitoSyncManager == null)
                {
                    _cognitoSyncManager = new CognitoSyncManager(Credentials, new AmazonCognitoSyncConfig { RegionEndpoint = CognitoRegion });
                }
                return _cognitoSyncManager;
            }
        }

        private IAmazonDynamoDB DynamoDBClient
        {
            get
            {
                if (_ddbClient == null)
                {
                    _ddbClient = new AmazonDynamoDBClient(Credentials, DynamoDBRegion);
                }

                return _ddbClient;
            }
        }

        private DynamoDBContext DynamoDBContext
        {
            get
            {
                if (_ddbContext == null)
                    _ddbContext = new DynamoDBContext(DynamoDBClient);

                return _ddbContext;
            }
        }

        private IAmazonLambda LambdaClient
        {
            get
            {
                if (_lambdaClient == null)
                {
                    _lambdaClient = new AmazonLambdaClient(Credentials, LambdaRegion);
                }
                return _lambdaClient;
            }
        }

        private IAmazonSimpleNotificationService SNSClient
        {
            get
            {
                if (_snsClient == null)
                    _snsClient = new AmazonSimpleNotificationServiceClient(Credentials, SNSRegion);
                return _snsClient;
            }
        }

        private string SNSEndpointARN;
        # endregion

        # region One-time Creation
        public static ChessNetworkManager Instance { get; private set; }
        void Awake()
        {
            Instance = this;
            // This ChessNetworkManager object will persist until the game is closed
            DontDestroyOnLoad(gameObject);
            // Initialize AWS SDK
            UnityInitializer.AttachToGameObject(gameObject);
            // Keep track of this users SNS Endpoing in DynamoDB
            RegisterDeviceAsync();
            // Initialize AnalyticsManager so that we get the side effect of it firing at start event.
            _analyticsManager = AnalyticsManager;
            // Now that the network manager is ready, head to the menu
            Application.LoadLevel("MainMenu");
        }
        # endregion

        # region Callbacks
        public delegate void GameStateLoadedCallback(string error, GameState loadedGameState);
        public delegate void IdentityRegisterCallback(string error);
        public delegate void FindPlayerResponseCallback(GameState.PlayerInfo player);
        public delegate void NewMatchResponseCallback(string error, string matchId);
        public delegate void LoadMatchResponseCallback(string error, GameState.MatchState matchState);
        public delegate void GetOnlineMatchesResponseCallback(string error, List<GameState.MatchState> matchStates);
        public delegate void SaveOnlineMatchResponseCallback(string error);
        # endregion

        # region Using Amazon Cognito Sync
        // Save the game state to CognitoSync's local storage.
        public void SaveGameStateLocal(GameState gameState)
        {
            // Add all friends' PlayerInfos, and our own PlayerInfo, to the players dataset, in the
            // form of key: player's id, value: player's name
            using (Dataset playersDataset = CognitoSyncManager.OpenOrCreateDataset(PlayersDatasetName))
            {
                foreach (var friend in gameState.Friends.Values)
                {
                    playersDataset.Put(friend.Id, friend.Name);
                }
                if (gameState.Self != null)
                {
                    playersDataset.Put(gameState.Self.Id, gameState.Self.Name);
                }
            }
            // Add all offline matches to the mathes dataset, with the matchIDs as the keys and
            // a json mapping of other match info as the value.
            using (Dataset matchesDataset = CognitoSyncManager.OpenOrCreateDataset(MatchesDatasetName))
            {
                foreach (var matchState in gameState.MatchStates.Values)
                {
                    if (matchState.Opponent.IsLocalOpponent())
                    {
                        var stringBuilder = new StringBuilder();
                        var writer = new JsonWriter(stringBuilder);
                        writer.WriteObjectStart();
                        writer.WritePropertyName(FriendIdPropertyName);
                        writer.Write(matchState.Opponent.Id);
                        writer.WritePropertyName(SelfIsWhitePRopertyName);
                        writer.Write(matchState.SelfIsWhite);
                        writer.WritePropertyName(ForsythEdwardsNotationPropertyName);
                        writer.Write(matchState.BoardState.ToForsythEdwardsNotation());
                        writer.WritePropertyName(AlgebraicNotationPropertyName);
                        writer.Write(matchState.BoardState.PreviousMove.ToLongAlgebraicNotation());
                        writer.WriteObjectEnd();
                        matchesDataset.Put(matchState.Identifier, stringBuilder.ToString());
                    }
                }
            }
        }

        // Synchronize the locally saved data with Cognito
        public void SynchronizeLocalDataAsync()
        {
            Dataset playersDataset = CognitoSyncManager.OpenOrCreateDataset(PlayersDatasetName);
            Dataset matchesDataset = CognitoSyncManager.OpenOrCreateDataset(MatchesDatasetName);
            matchesDataset.OnSyncFailure += delegate
            {
                Debug.LogWarning("Failed to sync Match States, but they will still be saved locally.");
                matchesDataset.Dispose();
            };
            matchesDataset.OnSyncSuccess += delegate { matchesDataset.Dispose(); };
            matchesDataset.SynchronizeAsync();

            playersDataset.OnSyncFailure += delegate
            {
                Debug.LogWarning("Failed to sync Friends, but they will still be saved locally.");
                playersDataset.Dispose();
            };
            playersDataset.OnSyncSuccess += delegate { playersDataset.Dispose(); };
            playersDataset.SynchronizeAsync();
        }

        // Load friends and local matches with Cognito Sync. If there is no network available,
        // the CognitoSyncManager uses the locally saved data.
        public void LoadGameStateAsync(GameStateLoadedCallback loadedCallback)
        {
            // Open the datasets. 
            Dataset playersDataset = CognitoSyncManager.OpenOrCreateDataset(PlayersDatasetName);
            Dataset matchesDataset = CognitoSyncManager.OpenOrCreateDataset(MatchesDatasetName);

            playersDataset.OnSyncSuccess += delegate
            {
                GameState.PlayerInfo self;
                // After the players dataset is successfully synchronized, create a dictionary
                //with key: friend id, and value: friend's playerInfo object,
                // and also extract our own PlayerInfo. We then synchronize the matches Dataset
                Dictionary<string, GameState.PlayerInfo> friends = PlayersDatasetToDict(playersDataset, Credentials.GetCachedIdentityId(), out self);
                matchesDataset.OnSyncSuccess += delegate
                {
                    // After the matches dataset is successfully synchronized, create a dictionary
                    // with key: match id, and value: MatchState object created from the JSON data
                    // stored in the dataset.
                    Dictionary<string, GameState.MatchState> matchesDict = MatchesDatasetToDict(matchesDataset, friends);
                    loadedCallback(null, new GameState(self, friends, matchesDict));
                    playersDataset.Dispose();
                    matchesDataset.Dispose();
                };
                matchesDataset.OnSyncFailure += delegate
                {
                    // Even though the matches dataset failed to synchronize, create a dictionary
                    // with key: match id, and value: MatchState object created from the JSON data
                    // stored in the dataset that was locally saved.
                    Dictionary<string, GameState.MatchState> matchesDict = MatchesDatasetToDict(matchesDataset, friends);
                    loadedCallback("Failed to sync Matches, using locally saved matches instead.", new GameState(self, friends, matchesDict));
                    playersDataset.Dispose();
                    matchesDataset.Dispose();
                };
                matchesDataset.SynchronizeAsync();
            };
            playersDataset.OnSyncFailure += delegate
            {
                string ownId = Credentials.GetCachedIdentityId();
                GameState.PlayerInfo self;
                // Because the datasets failed to synchronize, use the locally saved data.
                Dictionary<string, GameState.PlayerInfo> friendsDict = PlayersDatasetToDict(playersDataset, ownId, out self);
                Dictionary<string, GameState.MatchState> matchesDict = MatchesDatasetToDict(matchesDataset, friendsDict);
                loadedCallback("Failed to sync Friends and Match State, using locally saved data instead.", new GameState(self, friendsDict, matchesDict));
                playersDataset.Dispose();
            };
            playersDataset.SynchronizeAsync();
        }

        // Extract a dictionary with keys: player Id and values: PlayerInfo objects from the
        // key-value pairs in the dataset.
        private static Dictionary<string, GameState.PlayerInfo> PlayersDatasetToDict(Dataset playersDataset, string selfId, out GameState.PlayerInfo self)
        {
            IDictionary<string, string> friendsStringDict = playersDataset.ActiveRecords;
            Dictionary<string, GameState.PlayerInfo> friendsDict = new Dictionary<string, GameState.PlayerInfo>();
            self = null;
            foreach (string friendId in friendsStringDict.Keys)
            {
                if (friendId == selfId)
                {
                    self = new GameState.PlayerInfo(friendId, friendsStringDict[friendId]);
                }
                else
                {
                    friendsDict.Add(friendId, new GameState.PlayerInfo(friendId, friendsStringDict[friendId]));
                }
            }

            if (string.IsNullOrEmpty(selfId))
            {
                self = null;
            }
            else if (self == null)
            {
                self = new GameState.PlayerInfo(selfId, "anonymous");
            }
            else
            {
                if (string.IsNullOrEmpty(self.Id))
                {
                    self = new GameState.PlayerInfo(selfId, self.Name);
                }
                if (string.IsNullOrEmpty(self.Name))
                {
                    self = new GameState.PlayerInfo(self.Id, "anonymous");
                }
            }
            return friendsDict;
        }

        // Extract a dictionary with keys: match Id and values: MatchState objects from the
        // key-value pairs in the dataset.
        private static Dictionary<string, GameState.MatchState> MatchesDatasetToDict(Dataset matchesDataset, Dictionary<string, GameState.PlayerInfo> friendsDict)
        {
            IDictionary<string, string> matchesStringDict = matchesDataset.ActiveRecords;
            var matchesDict = new Dictionary<string, GameState.MatchState>();
            // Convert each match state from JSON to MatchState object, using the friendsDict
            // to get the PlayerInfo object of the opponent.
            foreach (string matchId in matchesStringDict.Keys)
            {
                JsonData data = JsonMapper.ToObject(matchesStringDict[matchId]);
                string friendId = data[FriendIdPropertyName].ToString();
                if (friendsDict.ContainsKey(friendId))
                {
                    string fen = null;
                    string algNot = null;
                    bool selfIsWhite = false;
                    var friend = friendsDict[friendId];
                    if (data[ForsythEdwardsNotationPropertyName] != null && data[AlgebraicNotationPropertyName] != null && data[SelfIsWhitePRopertyName] != null)
                    {
                        fen = (string)data[ForsythEdwardsNotationPropertyName];
                        algNot = (string)data[AlgebraicNotationPropertyName];
                        selfIsWhite = (bool)data[SelfIsWhitePRopertyName];
                    }
                    matchesDict.Add(matchId, new GameState.MatchState(friend, fen, algNot, selfIsWhite, matchId));
                }
            }
            return matchesDict;
        }

        // Use the Facebook sdk to log in with Facebook credentials
        public void LogInToFacebookAsync()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(() =>
                        {
                            FB.LogInWithReadPermissions(null, FacebookLoginCallback);
                        });
            }
            else
            {
                FB.LogInWithReadPermissions(null, FacebookLoginCallback);
            }
        }

        // Attch the Facebook Login token to our Cognito Identity.
        private void FacebookLoginCallback(ILoginResult result)
        {
            if (result.Error != null || !FB.IsLoggedIn)
            {
                Debug.LogError(result.Error);
            }
            else
            {
                Debug.Log("Adding login to credentials");
                Credentials.AddLogin("graph.facebook.com", result.AccessToken.TokenString);
            }
            GameManager.Instance.Load();
        }
        # endregion

        # region Using AWS Lambda
        // Invoke our AWS Lambda function that creates a new game in a DynamoDB table and reponds
        // with the MatchId of the newly created match.
        public void NewMatchAsync(GameState.PlayerInfo opponent, NewMatchResponseCallback callback)
        {
            // Construct the parameters to the AWS Lambda Function in JSON Format.
            var payloadStringBuilder = new StringBuilder();
            var payloadJsonWriter = new JsonWriter(payloadStringBuilder);
            payloadJsonWriter.WriteObjectStart();
            // User's ID
            payloadJsonWriter.WritePropertyName(RequesterIdPropertyName);
            payloadJsonWriter.Write(Credentials.GetCachedIdentityId());
            // Potential Opponent's ID
            payloadJsonWriter.WritePropertyName(OpponentIdPropertyName);
            payloadJsonWriter.Write(opponent.Id);
            payloadJsonWriter.WriteObjectEnd();

            // Invoke the New Match AWS Lambda Function with the given parameters.
            LambdaClient.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest()
                                     {
                                         FunctionName = NewMatchLambdaFunctionName,
                                         Payload = payloadStringBuilder.ToString(),
                                         // Request Response is the default, but I am specifiying it to be clear. We expect
                                         // the AWS Lambda Function to respond to the event we are invoking.
                                         InvocationType = InvocationType.RequestResponse
                                     },
            (responseObject) =>
            {
                if (responseObject.Exception != null)
                {
                    callback(responseObject.Exception.Message, null);

                }
                else if (!string.IsNullOrEmpty(responseObject.Response.FunctionError))
                {
                    callback(responseObject.Response.FunctionError, null);
                }
                else
                {
                    // Call back with no error, and the matchId from the response.
                    callback(null, Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()).Trim('"'));
                }
            });
        }
        # endregion

        # region Using Amazon DynamoDB
        // An object that contains the neccesary information to recreate a Match. Saved to and loaded
        // from the ChessMatches DynamoDB table.
        [DynamoDBTable("ChessMatches")]
        private class SimpleMatchInfo
        {
            [DynamoDBHashKey]
            public string MatchId;
            [DynamoDBGlobalSecondaryIndexHashKey]
            public string BlackPlayerId;
            [DynamoDBGlobalSecondaryIndexHashKey]
            public string WhitePlayerId;
            [DynamoDBProperty]
            public string AlgebraicNotation;
            [DynamoDBProperty]
            public string FEN;

            // Convert to a MatchState object
            public GameState.MatchState ToMatchState(string selfId)
            {
                bool selfIsWhite = selfId == WhitePlayerId;
                string opponentId = selfIsWhite ? BlackPlayerId : WhitePlayerId;
                GameState.PlayerInfo opponent = GameManager.Instance.GetFriendIfKnown(opponentId);
                return new GameState.MatchState(opponent, FEN, AlgebraicNotation, selfIsWhite, MatchId);
            }
        }

        // An entry in the DynamoDB SNSEndpointLookup table so our Lambda function can look up
        // where to send a push notification when it is the corresponding user's turn in a match.
        [DynamoDBTable("SNSEndpointLookup")]
        private class SNSEndpointLookupEntry
        {
            [DynamoDBHashKey]
            public string PlayerId;
            [DynamoDBProperty]
            public string SNSEndpointARN;
        }

        // Get the current state of the online multiplayer match with the given game id.
        public void LoadMatchAsync(string matchId, LoadMatchResponseCallback callback)
        {
            // Load the individual match with the given ID from the DynamoDB table specified by
            // the SimpleMatchInfo class's attribute;
            DynamoDBContext.LoadAsync<SimpleMatchInfo>(matchId, (responseObject) =>
            {
                if (responseObject.Exception == null)
                {
                    var matchInfo = responseObject.Result as SimpleMatchInfo;
                    callback(null, matchInfo.ToMatchState(Credentials.GetCachedIdentityId()));
                }
                else
                {
                    callback(responseObject.Exception.Message, null);
                }
            });
        }

        // Get all game matches that the player is involved in
        public void GetOnlineMatchesAsync(GetOnlineMatchesResponseCallback callback)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                string selfId = Credentials.GetCachedIdentityId();
                var matchStates = new List<GameState.MatchState>();
                // Query matches to the WhitePlayerId 
                var asyncSearchWhitePlayer = DynamoDBContext.QueryAsync<SimpleMatchInfo>(Credentials.GetCachedIdentityId(), new DynamoDBOperationConfig()
                {
                    IndexName = WhitePlayerDynamoDBIndexKey
                });
                // Query matches to the BlackPlayerId 
                var asyncSearchBlackPlayer = DynamoDBContext.QueryAsync<SimpleMatchInfo>(Credentials.GetCachedIdentityId(), new DynamoDBOperationConfig()
                {
                    IndexName = BlackPlayerDynamoDBIndexKey
                });
                // Get the Matches from the first query
                asyncSearchWhitePlayer.GetRemainingAsync((responseWhitePlayer) =>
                {
                    if (responseWhitePlayer.Exception == null)
                    {
                        List<SimpleMatchInfo> matchInfosWhitePlayer = responseWhitePlayer.Result;
                        foreach (var matchInfo in matchInfosWhitePlayer)
                        {
                            matchStates.Add(matchInfo.ToMatchState(selfId));
                        }
                        // Get the Matches from the second query
                        asyncSearchBlackPlayer.GetRemainingAsync((responseBlackPlayer) =>
                        {
                            if (responseBlackPlayer.Exception == null)
                            {
                                List<SimpleMatchInfo> matchInfosBlackPlayer = responseBlackPlayer.Result;
                                foreach (var matchInfo in matchInfosBlackPlayer)
                                {
                                    matchStates.Add(matchInfo.ToMatchState(selfId));
                                }
                                // Send the match states back to the caller.
                                callback(null, matchStates);
                            }
                            else
                            {
                                callback(responseBlackPlayer.Exception.Message, null);
                            }
                        });
                    }
                    else
                    {
                        callback(responseWhitePlayer.Exception.Message, null);
                    }
                });
            });
        }

        // Write the match to the ChessMatches DynamoDB table
        public void SaveOnlineMatchAsync(GameState.MatchState matchState, SaveOnlineMatchResponseCallback callback)
        {
            // Copy information to a SimpleMatchInfo object so the DynamoDB context can write it
            // to a table based on the attributes attached to the SimpleMatchInfo.
            var matchInfo = new SimpleMatchInfo();
            matchInfo.MatchId = matchState.Identifier;
            matchInfo.FEN = matchState.BoardState.ToForsythEdwardsNotation();
            matchInfo.AlgebraicNotation = matchState.BoardState.PreviousMove.ToLongAlgebraicNotation();
            matchInfo.WhitePlayerId = matchState.SelfIsWhite ? Credentials.GetCachedIdentityId() : matchState.Opponent.Id;
            matchInfo.BlackPlayerId = matchState.SelfIsWhite ? matchState.Opponent.Id : Credentials.GetCachedIdentityId();
            // Save the match and notify the caller of success/failure.
            DynamoDBContext.SaveAsync<SimpleMatchInfo>(matchInfo, (responseObject) =>
            {
                if (responseObject.Exception == null)
                {
                    callback(null);
                }
                else
                {
                    callback(responseObject.Exception.Message);
                }
            });
        }

        // Save our Identity so that other users can find us and add us as friends.
        public void PubliclyRegisterIdentityAsync(GameState gamestate, IdentityRegisterCallback callback)
        {
            if (gamestate.Self == null)
            {
                return;
            }
            // Use the DynamoDB context to write your player information to a table based on
            // the attributes attached to the SimpleMatchInfo.
            DynamoDBContext.SaveAsync<GameState.PlayerInfo>(gamestate.Self, (idToNameResponse) =>
            {
                if (idToNameResponse.Exception == null)
                {
                    // If successful, also register our SNSEnpoint in another table.
                    if (!string.IsNullOrEmpty(SNSEndpointARN))
                    {
                        DynamoDBContext.SaveAsync<SNSEndpointLookupEntry>(new SNSEndpointLookupEntry
                        {
                            PlayerId = gamestate.Self.Id,
                            SNSEndpointARN = SNSEndpointARN
                        },
                        (idToEndpointResponse) =>
                        {
                            callback(idToEndpointResponse.Exception == null ? null : idToEndpointResponse.Exception.Message);
                        });
                    }
                }
                else
                {
                    callback(idToNameResponse.Exception.Message);
                }
            });
        }

        // Look up another player by their identity if the have registered their identity in the DynamoDB table.
        public void FindPlayerByIdAsync(string id, FindPlayerResponseCallback callback)
        {
            DynamoDBContext.LoadAsync<GameState.PlayerInfo>(id, (responseObject) =>
            {
                var player = responseObject.Result as GameState.PlayerInfo;
                callback(player);
            });
        }
        # endregion

        # region Using Amazon Simple Notification Service
        // Register the device with SNS using the iOS Platform Application ARN or Android Platform
        // Application ARN and Google Project Id, and obtain a topic endpoint ARN for the device
        // used.
        private void RegisterDeviceAsync()
        {
#if UNITY_ANDROID
            if (string.IsNullOrEmpty(AndroidPlatformApplicationArn) || string.IsNullOrEmpty(GoogleConsoleProjectId))
            {
                Debug.LogWarning("Will not regester with SNS. Both Android Platforn Application ARN and Google Console Project must be provided.");
            }
            else
            {
                if (string.IsNullOrEmpty(GoogleConsoleProjectId))
                {
                    Debug.Log("sender id is null");
                    return;
                }
                GCM.Register((regId) =>
                {

                    if (string.IsNullOrEmpty(regId))
                    {
                        return;
                    }

                    SNSClient.CreatePlatformEndpointAsync(
                        new CreatePlatformEndpointRequest
                        {
                            Token = regId,
                            PlatformApplicationArn = AndroidPlatformApplicationArn
                        },
                        (resultObject) =>
                        {
                            if (resultObject.Exception == null)
                            {
                                CreatePlatformEndpointResponse response = resultObject.Response;
                                SNSEndpointARN = response.EndpointArn;
                            }
                        }
                    );
                }, GoogleConsoleProjectId);
            }
#elif UNITY_IOS
            if (string.IsNullOrEmpty(IOSPlatformApplicationArn))
            {
                Debug.LogWarning("Will not regester for SNS. iOS Platform Application ARN must be provided.");
            }
            else
            {
                UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert | UnityEngine.iOS.NotificationType.Badge | UnityEngine.iOS.NotificationType.Sound);
                CancelInvoke("CheckForDeviceToken");
                InvokeRepeating("CheckForDeviceToken", 1f, 1f);
            }
#endif
        }
        string deviceToken = null;
        //to keep a track of max number of times the device token is polled
        int count = 0;
        private void CheckForDeviceToken()
        {
#if UNITY_IOS
            if (!string.IsNullOrEmpty(IOSPlatformApplicationArn))
            {
                var token = UnityEngine.iOS.NotificationServices.deviceToken;
                var error = UnityEngine.iOS.NotificationServices.registrationError;

                if (count >= 10 || !string.IsNullOrEmpty(error))
                {
                    CancelInvoke("CheckForDeviceToken");
                    Debug.Log(@"Cancel polling");
                    return;
                }

                if (token != null)
                {
                    deviceToken = System.BitConverter.ToString(token).Replace("-", "");
                    Debug.Log("device token  = " + deviceToken);
                    SNSClient.CreatePlatformEndpointAsync(
                        new CreatePlatformEndpointRequest
                        {
                            Token = deviceToken,
                            PlatformApplicationArn = IOSPlatformApplicationArn
                        },
                        (resultObject) =>
                        {
                            if (resultObject.Exception == null)
                            {
                                CreatePlatformEndpointResponse response = resultObject.Response;
                                SNSEndpointARN = response.EndpointArn;
                            }
                        }
                    );

                    CancelInvoke("CheckForDeviceToken");
                }
                count++;
            }
#endif
        }
        # endregion

        # region Using Amazon Mobile Analytics
        private bool firstFocus = true;
        // Send pause and resume events to mobile analytics when the game gains or loses focus.
        void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                if (firstFocus)
                {
                    // if this is the first time the application focuses, we do not resume,
                    // because creation of the AnalyticsManager object fires the session start
                    // event instead.
                    firstFocus = false;
                }
                else
                {
                    AnalyticsManager.ResumeSession();
                }
            }
            else
            {
                AnalyticsManager.PauseSession();
            }
        }
        # endregion
    }
}
