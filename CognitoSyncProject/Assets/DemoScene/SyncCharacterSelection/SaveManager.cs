/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Sample Application License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
 */
 
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

/// <summary>
/// Contains the Cognito Sync related classes.
/// Reads and stores a CharacterList to a Cognito Sync dataset.
/// </summary>
public class SaveManager : MonoBehaviour
{

    private CognitoAWSCredentials credentials;
    private CognitoSyncManager syncManager;
    private Dataset characters;
    string[] characterStrings = null;

    bool initializationPending = false;

    /// <summary>
    /// Inits Cognito Sync with FB-authenticated user credentials.
    /// </summary>
    /// <param name="fbAccessToken">The FB access token returned by the FB SDK.</param>
    public void InitWithFacebook(string fbAccessToken)
    {
        enabled = true;

        InitWithoutFacebook();

        if (fbAccessToken != null) {
            credentials.IdentityProvider.Logins.Add ("graph.facebook.com", fbAccessToken);
            credentials.IdentityProvider.RefreshAsync (RefreshCallback, null);
            initializationPending = false;
        } else {
            initializationPending = true;
        }
    }

    /// <summary>
    /// Inits Cognito Sync with unauthenticated user credentials.
    /// </summary>
    public void InitWithoutFacebook()
    {
        enabled = true;

        // Enabling Logs
        //AmazonLogging.EnableSDKLogging = true;

        // CognitoAWSCredentials is recommended in the place of Developer credentials{accessKey,secretKey} for reasons of security best practices
        // CognitoAWSCredentials uses the CognitoIdentityProvider & provides temporary scoped AWS credentials from AssumeRoleWithWebIdentity
        // ref: http://mobile.awsblog.com/post/TxR1UCU80YEJJZ/Using-the-Amazon-Cognito-Credentials-Provider

        // Ref: http://docs.aws.amazon.com/mobile/sdkforandroid/developerguide/cognito-auth.html#create-an-identity-pool
        // for setting up Cognito Identity Pools, can you use the sample code for .NET SDK
        try {
            credentials = new CachingCognitoAWSCredentials ();
        } catch (Exception ex) {
            Debug.LogException (ex);
            return;
        }
        // DefaultCognitoSyncManager is a high level CognitoSync Client which handles all Sync operations at a Dataset level.
        // Additionally, it also provides local storage of the Datasets which can be later Synchronized with the cloud(CognitoSync service)
        // This feature allows the user to continue working w/o internet access and sync with CognitoSync whenever possible
        syncManager = new DefaultCognitoSyncManager (credentials, new AmazonCognitoSyncConfig { RegionEndpoint = RegionEndpoint.USEast1 });

        initializationPending = true;
    }

    void RefreshCallback (AmazonServiceResult result)
    {
        initializationPending = true;
    }

    void OnGUI()
    {
        if (syncManager == null)
        {
            GUILayout.Space(20);
            GUILayout.Label("Please setup the Cognito credentials for the AWSPrefab in the scene");
            return;
        }

        float ratio = Screen.width/600.0f;
        GUI.skin.label.fontSize = (int)(15*ratio);
        GUI.skin.button.fontSize = (int)(15*ratio);
        if (GetComponent<CharacterList> ().enabled)
        {
            if (GUI.Button (new Rect (30*ratio, 30*ratio, 120*ratio, 30*ratio), "Save"))
            {
                SaveToDataset ();
            }
            else if (GUI.Button (new Rect (30*ratio, 70*ratio, 120*ratio, 30*ratio), "Load"))
            {
                LoadFromDataset ();
            }
            else if (GUI.Button (new Rect (Screen.width-150*ratio, 30*ratio, 120*ratio, 30*ratio), "Logout"))
            {
                FB.Logout();
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else
        {
            GUI.Label(new Rect(30*ratio, 30*ratio, 120*ratio, 30*ratio), "Please wait...");
        }

    }

    private void LoadFromDataset()
    {
        GetComponent<CharacterList> ().enabled = false;
        // Remote DatasetMetadata is fetched and stored in SQLiteStorage
        // Records are not fetched until ds.Synchronize() is called on the respective datasets
        syncManager.RefreshDatasetMetadataAsync(RefreshDatasetMetadataCallback, null);

    }

    private void SaveToDataset()
    {
        CharacterList charList = GetComponent<CharacterList> ();
        string[] characterStrings = charList.SerializeCharacters ();
        int i = 0;
        foreach (string s in characterStrings)
        {
            characters.Put ((i++).ToString(), s);
        }
        while (characters.Get(i.ToString()) != null)
        {
            characters.Remove((i++).ToString());
        }
        GetComponent<CharacterList> ().enabled = false;
        characters.Synchronize();
    }

    private void RefreshDatasetMetadataCallback(AmazonCognitoResult result)
    {
        if (result.Exception == null)
        {
            characters.Synchronize();
            Debug.Log("RefreshDatasetMetadataAsync complete");
        }
        else
        {
            Debug.LogException(result.Exception);
        }
    }

    private void HandleSyncSuccess(object sender, SyncSuccessEvent e)
    {
        var dataset = sender as Dataset;
        //Amazon.CognitoSync.SyncManager.Dataset dataset, List<Amazon.CognitoSync.SyncManager.Record> updatedRecords
        Debug.Log("Successfully synced for dataset : " + dataset.Metadata.DatasetName);

        Dictionary<string, string> dic = dataset.GetAll ();
        characterStrings = new string[dic.Count];
        dic.Values.CopyTo(characterStrings,0);
    }

    void Update()
    {
        if (initializationPending)
        {
            // Loading the datasets
            characters = syncManager.OpenOrCreateDataset ("characters");

            // when ds.Synchronize() is called the localDataset is merged with the remoteDataset
            // OnDatasetDeleted, OnDatasetMerged, OnDatasetSuccess,  the corresponding callback is fired.
            // The developer has the freedom of handling these events needed for the Dataset
            characters.OnSyncSuccess += this.HandleSyncSuccess; // OnSyncSucess uses events/delegates pattern
            characters.OnSyncFailure += this.HandleSyncFailure; // OnSyncFailure uses events/delegates pattern
            characters.OnSyncConflict = this.HandleSyncConflict;
            characters.OnDatasetMerged = this.HandleDatasetMerged;
            characters.OnDatasetDeleted = this.HandleDatasetDeleted;

            LoadFromDataset ();

            initializationPending = false;
        }

        if (characterStrings != null)
        {
            CharacterList charList = GetComponent<CharacterList> ();
            charList.DeserializeCharacters (characterStrings);
            characterStrings = null;
            charList.enabled = true;
        }
    }

    private bool HandleDatasetDeleted(Dataset dataset)
    {
        Debug.Log(dataset.Metadata.DatasetName + " Dataset has been deleted");

        CharacterList charList = GetComponent<CharacterList> ();
        charList.DeleteAllCharacters ();

        // returning true informs the corresponding dataset can be purged in the local storage and return false retains the local dataset
        return true;
    }

    private bool HandleDatasetMerged(Dataset dataset, List<string> datasetNames)
    {
        Debug.Log(dataset + " Dataset has been merged");

        // returning true allows the Synchronize to resume and false cancels it
        return true;
    }

    private bool HandleSyncConflict(Amazon.CognitoSync.SyncManager.Dataset dataset, List<SyncConflict> conflicts)
    {
        List<Amazon.CognitoSync.SyncManager.Record> resolvedRecords = new List<Amazon.CognitoSync.SyncManager.Record>();
        foreach (SyncConflict conflictRecord in conflicts)
        {
            resolvedRecords.Add(conflictRecord.ResolveWithRemoteRecord());
        }
        // resolves the conflicts in local storage
        dataset.Resolve(resolvedRecords);
        // on return true the synchronize operation continues where it left,
        //      returning false cancels the synchronize operation
        return true;
    }

    private void HandleSyncFailure(object sender, SyncFailureEvent e)
    {
        var dataset = sender as Dataset;
        Debug.Log("Sync failed for dataset : " + dataset.Metadata.DatasetName);
    }


}