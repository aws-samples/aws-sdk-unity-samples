using UnityEngine;
using System;
using System.Collections.Generic;

namespace AWSSDK.Examples.ChessGame
{
    // A singletoon that provides a layer between the game scenes and the AWS network calls, while
    // also keeping track of the state of the game.
    public class GameManager
    {
        # region private members
        private GameState GameState;
        private bool IdentityRegistered;
        private string MostRecentMatchIdentifier;
        private GameState.MatchState UnconsumedCurrentMatchState;
        private string UnconsumedCurrentMatchStateError;
        # endregion

        # region Handlers and events
        public delegate void StatesAvailableHandler(List<GameState.MatchState> MatchStates);
        public delegate void FriendsAvailableHandler(List<GameState.PlayerInfo> Friends);
        public delegate void SelfAvailableHandler(GameState.PlayerInfo Self);
        public delegate void FriendAddedHandler(string RequestedId, GameState.PlayerInfo Friend);
        public delegate void CurrentMatchStateAvailableHandler(string Error, GameState.MatchState currentMatchState);

        private event StatesAvailableHandler OnStatesAvailable;
        private event FriendsAvailableHandler OnFriendsAvailable;
        private event SelfAvailableHandler OnSelfAvailable;
        private event FriendAddedHandler OnFriendAdded;
        private event CurrentMatchStateAvailableHandler OnCurrentMatchStateAvailable;
        # endregion

        # region Singleton
        // Manager is a singleton throughout the game lifecycle.
        private static GameManager _instance = null;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        private GameManager()
        {
            IdentityRegistered = false;
            MostRecentMatchIdentifier = "";
        }
        # endregion

        # region Methods to (un)register handlers

        // Registering and unregistering handlers allows game objects to be notified when
        // asynchronous actions (like network calls) complete.

        public void RegisterOnStatesAvailableHandler(StatesAvailableHandler handler)
        {
            OnStatesAvailable += handler;
            if (GameState != null)
            {
                handler(getMatchStates());
            }
        }

        public void RegisterOnFriendsAvailableHandler(FriendsAvailableHandler handler)
        {
            OnFriendsAvailable += handler;
            if (GameState != null)
            {
                handler(getFriends());
            }
        }

        public void RegisterOnSelfAvailableHandler(SelfAvailableHandler handler)
        {
            OnSelfAvailable += handler;
            if (GameState != null)
            {
                handler(GameState.Self);
            }
        }

        public void RegisterFriendAddedHandler(FriendAddedHandler handler)
        {
            OnFriendAdded += handler;
        }

        public void RegisterOnCurrentMatchStateAvailableHandler(CurrentMatchStateAvailableHandler handler)
        {
            if (UnconsumedCurrentMatchState != null || UnconsumedCurrentMatchStateError != null)
            {
                handler(UnconsumedCurrentMatchStateError, UnconsumedCurrentMatchState);
                UnconsumedCurrentMatchState = null;
                UnconsumedCurrentMatchStateError = null;
            }
            OnCurrentMatchStateAvailable += handler;
        }

        public void UnregisterOnStatesAvailableHandler(StatesAvailableHandler handler)
        {
            OnStatesAvailable -= handler;
        }

        public void UnregisterOnFriendsAvailableHandler(FriendsAvailableHandler handler)
        {
            OnFriendsAvailable -= handler;
        }

        public void UnregisterOnSelfAvailableHandler(SelfAvailableHandler handler)
        {
            OnSelfAvailable -= handler;
        }

        public void UnregisterOnFriendAddedHandler(FriendAddedHandler handler)
        {
            OnFriendAdded -= handler;
        }

        public void UnregisterOnCurrentMatchStateAvailableHandler(CurrentMatchStateAvailableHandler handler)
        {
            OnCurrentMatchStateAvailable -= handler;
        }
        # endregion

        private void SaveLocal()
        {
            ChessNetworkManager.Instance.SaveGameStateLocal(GameState);
        }

        public void SyncLocallySaved()
        {
            ChessNetworkManager.Instance.SynchronizeLocalDataAsync();
            PubliclyRegisterIdentity();
        }

        private void PubliclyRegisterIdentity()
        {
            if (!IdentityRegistered)
            {
                // So two registrations do not overlap, as well as to avoid extraneous DDB calls on saving data.
                IdentityRegistered = true;
                ChessNetworkManager.Instance.PubliclyRegisterIdentityAsync(GameState, (error) =>
                {
                    if (error == null)
                    {
                        IdentityRegistered = true;
                        Debug.Log("Successfully publicly registered player identity");
                    }
                    else
                    {
                        IdentityRegistered = false;
                        Debug.LogWarning(string.Format("Could not publicly register player identity. Got error:\n{0}", error));
                    }
                });
            }
        }

        public void LogInToFacebook()
        {
            ChessNetworkManager.Instance.LogInToFacebookAsync();
        }

        public void Load()
        {
            // Load friends and local games with this call.
            ChessNetworkManager.Instance.LoadGameStateAsync(delegate(string warning, GameState loadedGameState)
            {
                GameState oldGameState = GameState;
                GameState = loadedGameState;
                if (!string.IsNullOrEmpty(warning))
                {
                    Debug.LogWarning(warning);
                    AfterLoad();
                }
                else
                {
                    // If we successfully loaded friends and local games, load online games with
                    // this call.
                    ChessNetworkManager.Instance.GetOnlineMatchesAsync(delegate(string error, List<GameState.MatchState> matchStates)
                    {
                        if (string.IsNullOrEmpty(error))
                        {
                            foreach (var matchState in matchStates)
                            {
                                GameState.MatchStates[matchState.Identifier] = matchState;
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Error loading Online Matches (is there internet connectectivity?): " + error);
                        }
                        if (oldGameState == null || oldGameState.Self != GameState.Self)
                        {
                            // Player's info has changed, so register it
                            IdentityRegistered = false;
                            PubliclyRegisterIdentity();
                        }
                        AfterLoad();
                    });
                }
            });
        }

        // When we have loaded a game state, notify all listeners
        private void AfterLoad()
        {
            if (GameState.Friends.Count == 0)
            {
                GameState.AddLocalOpponent();
            }
            if (OnFriendsAvailable != null)
            {
                OnFriendsAvailable(getFriends());
            }
            if (OnStatesAvailable != null)
            {
                OnStatesAvailable(getMatchStates());
            }
            if (OnSelfAvailable != null)
            {
                OnSelfAvailable(GameState.Self);
            }
        }

        // Get the friend's info and save them
        public void AddFriend(string id)
        {
            ChessNetworkManager.Instance.FindPlayerByIdAsync(id, (player) =>
            {
                if (player != null)
                {
                    if (GameState.Self != null && player.Id == GameState.Self.Id)
                    {
                        player = null;
                    }
                    else
                    {
                        GameState.Friends[player.Id] = player;
                    }
                }
                if (OnFriendAdded != null)
                {
                    OnFriendAdded(id, player);
                }
                SaveLocal();
                SyncLocallySaved();
            });
        }

        // When a move has been made, update the game manager's GameState and save either locally or
        // overnetwork depending on whether the game is a local game or an online game.
        //TODO: match state should be updated on the server, client should only pass the moves that were made
        public void UpdateMatchState(GameState.MatchState matchState)
        {
            if (matchState.Opponent.IsLocalOpponent())
            {
                GameState.MatchStates[matchState.Identifier] = matchState;
                if (OnCurrentMatchStateAvailable == null)
                {
                    UnconsumedCurrentMatchState = matchState;
                    UnconsumedCurrentMatchStateError = null;
                }
                else
                {
                    OnCurrentMatchStateAvailable(null, matchState);
                }
            }
            else
            {
                ChessNetworkManager.Instance.SaveOnlineMatchAsync(matchState, (error) =>
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        GameState.MatchStates[matchState.Identifier] = matchState;
                    }

                    if (OnCurrentMatchStateAvailable == null)
                    {
                        UnconsumedCurrentMatchState = matchState;
                        UnconsumedCurrentMatchStateError = error;
                    }
                    else
                    {
                        OnCurrentMatchStateAvailable(error, matchState);
                    }
                });
            }
            SaveLocal();
        }

        // Change the users name and make appropriate changes over network.
        public void UpdateName(string name)
        {
            GameState.Self = new GameState.PlayerInfo(GameState.Self.Id, name);
            IdentityRegistered = false;
            SaveLocal();
            SyncLocallySaved();
        }

        public List<GameState.MatchState> getMatchStates()
        {
            return new List<GameState.MatchState>(GameState.MatchStates.Values);
        }

        public List<GameState.PlayerInfo> getFriends()
        {
            return new List<GameState.PlayerInfo>(GameState.Friends.Values);
        }

        public GameState.PlayerInfo GetFriendIfKnown(string id)
        {
            if (GameState.Friends.ContainsKey(id))
            {
                return GameState.Friends[id];
            }
            else
            {
                var unknownPlayer = new GameState.PlayerInfo(id, "unknown player");
                GameState.Friends[id] = unknownPlayer;
                return unknownPlayer;
            }
        }

        // Load the board scene, make sure that the match is up to date, and inform any listeners
        // (particularly, the board scene) that it is available.
        public void LoadMatch(GameState.MatchState matchState)
        {
            Application.LoadLevel("Board");
            if (matchState.Opponent.IsLocalOpponent())
            {
                if (OnCurrentMatchStateAvailable == null)
                {
                    UnconsumedCurrentMatchState = matchState;
                    UnconsumedCurrentMatchStateError = null;
                }
                else
                {
                    OnCurrentMatchStateAvailable(null, matchState);
                }
            }
            else
            {
                ChessNetworkManager.Instance.LoadMatchAsync(matchState.Identifier, (error, loadedMatchState) =>
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        GameState.MatchStates[loadedMatchState.Identifier] = loadedMatchState;
                    }
                    if (OnCurrentMatchStateAvailable == null)
                    {
                        UnconsumedCurrentMatchState = loadedMatchState;
                        UnconsumedCurrentMatchStateError = error;
                    }
                    else
                    {
                        OnCurrentMatchStateAvailable(error, loadedMatchState);
                    }
                    SaveLocal();
                });
            }
        }

        // Load the board scene, create a new match against the given opponent, and inform any
        // listeners (particularly, the board scene) that it is available.
        public void LoadNewMatch(GameState.PlayerInfo opponent)
        {
            Application.LoadLevel("Board");
            if (opponent.IsLocalOpponent())
            {
                var matchState = new GameState.MatchState(opponent, BoardState.InitialGameFen, BoardState.InitialLongAlgNotation, true, opponent.Id + DateTime.Now.ToUniversalTime().ToString());
                GameState.MatchStates[matchState.Identifier] = matchState;
                if (OnCurrentMatchStateAvailable == null)
                {
                    UnconsumedCurrentMatchState = matchState;
                    UnconsumedCurrentMatchStateError = null;
                }
                else
                {
                    OnCurrentMatchStateAvailable(UnconsumedCurrentMatchStateError, matchState);
                }
                SaveLocal();
                SyncLocallySaved();
            }
            else
            {
                ChessNetworkManager.Instance.NewMatchAsync(opponent, (newMatchError, matchId) =>
                {
                    if (string.IsNullOrEmpty(newMatchError))
                    {
                        ChessNetworkManager.Instance.LoadMatchAsync(matchId, (loadMatchError, loadedMatchState) =>
                        {
                            GameState.MatchStates[loadedMatchState.Identifier] = loadedMatchState;
                            if (OnCurrentMatchStateAvailable == null)
                            {
                                UnconsumedCurrentMatchState = loadedMatchState;
                                UnconsumedCurrentMatchStateError = null;
                            }
                            else
                            {
                                OnCurrentMatchStateAvailable(loadMatchError, loadedMatchState);
                            }
                            SaveLocal();
                            SyncLocallySaved();
                        });
                    }
                    else
                    {
                        OnCurrentMatchStateAvailable(newMatchError, null);
                    }
                });
            }
        }

        public GameState.MatchState GetCurrentMatchState()
        {
            if (GameState == null)
            {
                return null;
            }
            return GameState.getWithIdentifier(MostRecentMatchIdentifier);
        }

        public void DereferenceGameState()
        {
            GameState = null;
        }
    }
}