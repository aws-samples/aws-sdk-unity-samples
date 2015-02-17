/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
 */
// to use facebook authentication
//#define USE_FACEBOOK_LOGIN

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Amazon;
using Amazon.CognitoSync;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentity.Model;
using Amazon.CognitoSync.SyncManager;
using Amazon.Common;
using Amazon.Unity3D;

public class AmazonCognitoSyncManagerSample : MonoBehaviour
{
    private Dataset playerInfo, playerSettings;

    private string playerName, alias, statusMessage;

    private bool disableButton = false;

    private string[] difficultyLevels;

    private int selectedDifficultyLevel;

    private CognitoSyncManager syncManager;

    // Use this for initialization
    void Start()
    {
        // Enabling Logs
        AmazonLogging.EnableSDKLogging = true;

        difficultyLevels = new string[] { "Expert", "Medium", "Easy" };
    
        try
        {
            CognitoSyncClientManager.init();
            syncManager = CognitoSyncClientManager.CognitoSyncManagerInstance;
        }
        catch(Exception ex)
        {
            statusMessage = "Please setup the Cognito credentials for the AWSPrefab in the scene";
            Debug.LogException(ex);
            return;
        }

        // Loading the datasets
        playerInfo = syncManager.OpenOrCreateDataset("PlayerInfo");
        playerSettings = syncManager.OpenOrCreateDataset("PlayerSettings");

        // fetching locally stored data
        alias = string.IsNullOrEmpty(playerInfo.Get("alias")) ? "Enter your alias" : playerInfo.Get("alias");
        playerName = string.IsNullOrEmpty(playerInfo.Get("playerName")) ? "Enter your full name" : playerInfo.Get("playerName");

        selectedDifficultyLevel = string.IsNullOrEmpty(playerSettings.Get("selectedDifficultyLevel")) ? 0 : int.Parse(playerSettings.Get("selectedDifficultyLevel"));

        statusMessage = "Welcome ..";
        // when ds.Synchronize() is called the localDataset is merged with the remoteDataset 
        // OnDatasetDeleted, OnDatasetMerged, OnDatasetSuccess,  the corresponding callback is fired.
        // The developer has the freedom of handling these events needed for the Dataset
        playerInfo.OnSyncSuccess += this.HandleSyncSuccess; // OnSyncSucess uses events/delegates pattern
        playerInfo.OnSyncFailure += this.HandleSyncFailure; // OnSyncFailure uses events/delegates pattern
        playerInfo.OnSyncConflict = this.HandleSyncConflict;
        playerInfo.OnDatasetMerged = this.HandleDatasetMerged;
        playerInfo.OnDatasetDeleted = this.HandleDatasetDeleted;

        // playerSettings Dataset shows the use of default callback for syncManager
        // The OnSyncConflict event defaults to last writer wins policy
        //     OnSyncSucess, OnSyncFailure, OnDatasetMerged events are ignored
        //     OnDatasetDeleted defaults to false
    }

    void OnGUI()
    {
        GUI.color = Color.gray;
        GUILayout.BeginArea(new Rect(Screen.width * 0.2f, 0, Screen.width - Screen.width * 0.2f, Screen.height));
        GUILayout.Space(20);
        GUILayout.Label(statusMessage);

        if (syncManager == null)
        {
            GUILayout.EndArea();
            return;
        }
        
        GUI.color = Color.white;
        #if USE_FACEBOOK_LOGIN
        GUI.enabled = true;
        #else
        GUI.enabled = false;
        #endif
        if (GUILayout.Button("Connect to Facebook", GUILayout.MinHeight(20), GUILayout.Width(Screen.width * 0.6f)))
        {
            #if USE_FACEBOOK_LOGIN
            statusMessage = "Connecting to Facebook";
            disableButton = true;
            if (!FB.IsInitialized)
            {
                FB.Init(delegate() {
                    Debug.Log("starting thread");
                    
                    // shows to connect the current identityid or create a new identityid with facebook authentication
                    FB.Login ("email", FacebookLoginCallback);
                });
            }
            else
            {
                FB.Login ("email", FacebookLoginCallback);
            }
            #endif
        }

        if (disableButton)    
            GUI.enabled = false;
        else
            GUI.enabled = true;
        GUILayout.Label("Enter PlayerInfo");
        playerName = GUILayout.TextField(playerName, GUILayout.MinHeight(20), GUILayout.Width(Screen.width * 0.6f));
        alias = GUILayout.TextField(alias, GUILayout.MinHeight(20), GUILayout.Width(Screen.width * 0.6f));

        GUILayout.Space(20);

        GUILayout.Label("Select Difficulty Level");
        selectedDifficultyLevel = GUILayout.SelectionGrid(selectedDifficultyLevel, difficultyLevels, 3, GUILayout.MinHeight(20), GUILayout.Width(Screen.width * 0.6f));
        GUILayout.Space(20);

        if (GUILayout.Button("Save offline using SyncManager", GUILayout.MinHeight(20), GUILayout.Width(Screen.width * 0.6f)))
        {
            statusMessage = "Saving offline..";
            disableButton = true;
            // syncManager creates or fetches the Dataset locally using SQLiteStorage
            // OpenOrCreateDataset(datasetName) takes the datasetName which may be like "UserSettings", "UserState"

            playerInfo.Put("playerName", playerName);
            playerInfo.Put("alias", alias);

            playerSettings.Put("selectedDifficultyLevel", selectedDifficultyLevel.ToString());

            alias = string.IsNullOrEmpty(playerInfo.Get("alias")) ? "Enter ur alias" : playerInfo.Get("alias");
            playerName = string.IsNullOrEmpty(playerInfo.Get("playerName")) ? "Enter ur fullName" : playerInfo.Get("playerName");

            selectedDifficultyLevel = string.IsNullOrEmpty(playerSettings.Get("selectedDifficultyLevel")) ? 0 : int.Parse(playerSettings.Get("selectedDifficultyLevel"));
            statusMessage = "Saved offline !";
            disableButton = false;
        }
        else if (GUILayout.Button("Sync with CognitoSync Cloud", GUILayout.MinHeight(20), GUILayout.Width(Screen.width * 0.6f)))
        {
            statusMessage = "Saving to CognitoSync Cloud..";
            disableButton = true;
            playerInfo.Put("alias", alias);
            playerInfo.Synchronize();
            playerInfo.Put ("playerName", playerName);

            playerSettings.Put("difficultyLevel", selectedDifficultyLevel.ToString());
            playerSettings.Synchronize();
        }
        else if (GUILayout.Button("Refresh and Sync to CognitoSync Cloud", GUILayout.MinHeight(20), GUILayout.Width(Screen.width * 0.6f)))
        {
            statusMessage = "Refreshing metadata for current player..";
            disableButton = true;
            // RefreshDatasetMetadataAsync fetches all datasets for the current IdentityID
            // Remote DatasetMetadata is fetched and stored in SQLiteStorage
            // Records are not fetched until ds.Synchronize() is called on the respective datasets
            syncManager.RefreshDatasetMetadataAsync(RefreshDatasetMetadataCallback, null);
        }
        else if (GUILayout.Button("Delete all local player data", GUILayout.MinHeight(20), GUILayout.Width(Screen.width * 0.6f)))
        {
            statusMessage = "Deleting all local data..";
            syncManager.WipeData();
            playerName = "Enter ur full name";
            alias = "Enter ur alias";
            selectedDifficultyLevel = 0;
            statusMessage = "Deleting all local data complete. ";
        }
        GUILayout.EndArea();

    }


    private void RefreshDatasetMetadataCallback(AmazonCognitoResult result)
    {
        if (result.Exception == null)
        {
            Debug.Log("RefreshDatasetMetadataAsync complete");
            statusMessage = "Refreshing metadata complete";
            playerInfo.Synchronize();
            playerSettings.Synchronize();
        }
        else
        {
            Debug.Log("RefreshDatasetMetadataAsync failed");
            statusMessage = "Refreshing metadata failed";
            Debug.LogException(result.Exception);
            disableButton = false;
        }
    }

    #region Public Authentication Providers
#if USE_FACEBOOK_LOGIN
    private void FacebookLoginCallback(FBResult result)
    {
        Debug.Log("FB.Login completed");
        
        if (result.Error != null || !FB.IsLoggedIn)
        {
            Debug.LogError(result.Error);
            statusMessage = result.Error;
            disableButton = false;
        }
        else
        {
            Debug.Log (result.Text);

            // please make a note of the FB.AccessTokenExpiresAt and refresh it before it expires
            // cognito auth will fail if the FB.AccessToken is expired
            CognitoSyncClientManager.CognitoAWSCredentialsInstance.IdentityProvider.RefreshAsync(AuthenticationCallback, "Facebook Login");
        }
        
    }
    private void AuthenticationCallback(AmazonServiceResult refreshResult) 
    {
        var loginRequestState = (refreshResult.State != null) ?  refreshResult.State as string : "Refresh Identity";
        if (refreshResult.Exception != null) 
        {
            Debug.LogException(refreshResult.Exception);
            
            statusMessage = loginRequestState + " failed ! ";    
        }
        else
        {
            statusMessage = loginRequestState + " successful ! ";    
        }
        disableButton = false;
        
    }
#endif
    #endregion

    #region Sync events
    private bool HandleDatasetDeleted(Dataset dataset)
    {
        
        statusMessage = dataset.Metadata.DatasetName + "Dataset has been deleted..";
        Debug.Log(dataset.Metadata.DatasetName + " Dataset has been deleted");

        // TODO do clean up if necessary 

        // returning true informs the corresponding dataset can be purged in the local storage and return false retains the local dataset
        return true;
    }

    public bool HandleDatasetMerged(Dataset dataset, List<string> datasetNames)
    {
        statusMessage = "Merging datasets between different identities..";
        Debug.Log(dataset + " Dataset has been merged");
        // returning true allows the Synchronize to resume and false cancels it
        return true;
    }

    private bool HandleSyncConflict(Amazon.CognitoSync.SyncManager.Dataset dataset, List<SyncConflict> conflicts)
    {
        
        statusMessage = "Handling Sync Conflicts..";
        Debug.Log("OnSyncConflict");
        List<Amazon.CognitoSync.SyncManager.Record> resolvedRecords = new List<Amazon.CognitoSync.SyncManager.Record>();

        foreach (SyncConflict conflictRecord in conflicts)
        {
            // TODO : implement your logic for handling conflicts
            // This example resolves all the conflicts using ResolveWithRemoteRecord 
            // SyncManager provides the following default conflict resolution methods:
            //      ResolveWithRemoteRecord - overwrites the local with remote records
            //      ResolveWithLocalRecord - overwrites the remote with local records
            //      ResolveWithValue - for developer logic  
            resolvedRecords.Add(conflictRecord.ResolveWithRemoteRecord());
        }

        // resolves the conflicts in local storage
        dataset.Resolve(resolvedRecords);

        // on return true the synchronize operation continues where it left,
        //      returning false cancels the synchronize operation
        return true;
    }

    private void HandleSyncSuccess(object sender, SyncSuccessEvent e)
    {

        var dataset = sender as Dataset;
        //Amazon.CognitoSync.SyncManager.Dataset dataset, List<Amazon.CognitoSync.SyncManager.Record> updatedRecords
        Debug.Log("Successfully synced for dataset : " + dataset.Metadata.DatasetName);

        if (dataset == playerInfo)
        {
            alias = string.IsNullOrEmpty(playerInfo.Get("alias")) ? "Enter ur alias" : dataset.Get("alias");
            playerName = string.IsNullOrEmpty(playerInfo.Get("playerName")) ? "Enter ur fullName" : dataset.Get("playerName");
        }
        statusMessage = "Syncing to CognitoSync Cloud succeeded !";
        disableButton = false;
    }

    private void HandleSyncFailure(object sender, SyncFailureEvent e)
    {
        var dataset = sender as Dataset;
	    Debug.Log("Sync failed for dataset : " + dataset.Metadata.DatasetName);
		Debug.LogException(e.Exception);

        statusMessage = "Syncing to CognitoSync Cloud failed !";
        disableButton = false;
    }
    #endregion
}