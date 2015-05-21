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


public class SaveManager : MonoBehaviour
{
	
	public  string IDENTITY_POOL_ID = "";
	public RegionEndpoint ENDPOINT = RegionEndpoint.USEast1;

	private CognitoAWSCredentials credentials;
    private CognitoSyncManager syncManager;

	private Dataset characters;

    bool mergeInCourse = false;

	/// <summary>
	/// Inits Cognito Sync with FB-authenticated user credentials.
	/// </summary>
	/// <param name="fbAccessToken">The FB access token returned by the FB SDK.</param>
	public void InitWithFacebook(string fbAccessToken) 
	{
		try {
			Init("graph.facebook.com", fbAccessToken);
		} catch (Exception ex) {
			Debug.LogException (ex);
			return;
		}
	}
		
	/// <summary>
	/// Inits Cognito Sync with unauthenticated user credentials.
	/// </summary>
	public void InitUnauth()
	{
		try {
			Init(null, null);
		} catch (Exception ex) {
			Debug.LogException (ex);
			return;
		}
	}
	
    private void Init(string authProvider, string accessToken)
    {
		enabled = true;

		if (string.IsNullOrEmpty(IDENTITY_POOL_ID))
		{
			throw new NotSupportedException ("Please setup your the identity pool id in SceneController");
		}

		//Create a Credentials provider that uses Cognito Identity
		credentials = new CognitoAWSCredentials(IDENTITY_POOL_ID, ENDPOINT);

		//If fbAccesToken is set, we can use Cognito's authenticated identities
		if (accessToken != null) {
			credentials.AddLogin(authProvider, accessToken);
		}

		syncManager = new CognitoSyncManager (credentials, new AmazonCognitoSyncConfig { RegionEndpoint = ENDPOINT });

		InitializeCharactersDataset();
    }
	
	void InitializeCharactersDataset()
	{
		// Loading the datasets
		characters = syncManager.OpenOrCreateDataset ("characters");

		// when .Synchronize() is called the localDataset is merged with the remoteDataset
		// OnDatasetDeleted, OnDatasetMerged, OnDatasetSuccess,  the corresponding callback is fired.
		// The developer has the freedom of handling these events needed for the Dataset
		characters.OnSyncSuccess += this.HandleSyncSuccess; // OnSyncSucess uses events/delegates pattern
		characters.OnSyncFailure += this.HandleSyncFailure; // OnSyncFailure uses events/delegates pattern
		characters.OnSyncConflict = this.HandleSyncConflict;
		characters.OnDatasetMerged = this.HandleDatasetMerged;
		characters.OnDatasetDeleted = this.HandleDatasetDeleted;

		LoadFromDataset ();
	}
		
    void OnGUI()
    {
		float ratio = Screen.width/600.0f;
		GUI.skin.label.fontSize = (int)(15*ratio);
		GUI.skin.button.fontSize = (int)(15*ratio);

        if (syncManager == null)
        {
            GUILayout.Space(20);
            GUILayout.Label("Please setup the Cognito credentials");
            return;
        }

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
				if (credentials.CurrentLoginProviders.Length > 0) { //Auth identity
					if (FB.IsLoggedIn) {
						FB.Logout();
					}
					credentials.Clear();
					syncManager.WipeData();
				}
                Application.LoadLevel(Application.loadedLevel);
            }
			if (credentials.CurrentLoginProviders.Length == 0) { //Unauth
				if (GUI.Button (new Rect (Screen.width-150*ratio, 70*ratio, 120*ratio, 30*ratio), "Link with FB"))
				{	
					GetComponent<CharacterList> ().enabled = false; //Disable GUI
					FB.Login("email", FacebookLoginCallback);
				}
			}
        }
        else
        {
            GUI.Label(new Rect(30*ratio, 30*ratio, 120*ratio, 30*ratio), "Please wait...");
        }

		GUI.Label(new Rect(20*ratio, Screen.height-50*ratio, 600*ratio, 30*ratio), "Identity: " + credentials.GetCachedIdentityId());

    }

	private void FacebookLoginCallback(FBResult result)
	{
		GetComponent<CharacterList> ().enabled = true; //Enable GUI
		if (FB.IsLoggedIn) {
			credentials.AddLogin("graph.facebook.com", FB.AccessToken);
			LoadFromDataset();
		}
	}

	private void LoadFromDataset()
    {
		GetComponent<CharacterList> ().enabled = false; //Disable GUI

		characters.Synchronize();
    }

	private String indexToKey(int index) {
		return index.ToString("00000");
	}

    private void SaveToDataset()
	{
		GetComponent<CharacterList> ().enabled = false; //Disable GUI

		//Store data from the scene into the dataset
        CharacterList charList = GetComponent<CharacterList> ();
        string[] characterStrings = charList.SerializeCharacters ();
        int i = 0;
        foreach (string s in characterStrings)
        {
			characters.Put (indexToKey(i++), s);
        }
		while (characters.Get(indexToKey(i)) != null)
        {
			characters.Remove(indexToKey(i++));
        }

        characters.Synchronize();
    }

    private void HandleSyncSuccess(object sender, SyncSuccessEvent e)
    {
        if (mergeInCourse) {
            Debug.Log ("Waiting for merge to complete to sync again");
            return;
        }

        var dataset = sender as Dataset;
		if (dataset != null && dataset.Metadata != null) {
        	Debug.Log("Successfully synced for dataset : " + dataset.Metadata.DatasetName);
		} else {
			//In case we called synchronize after deleting the dataset, we can not access it anymore
			Debug.Log("Successfully synced dataset");
		}


        IDictionary<string, string> dic = dataset.GetAll ();
        string[] characterStrings = new string[dic.Count];
        dic.Values.CopyTo(characterStrings, 0);

		CharacterList charList = GetComponent<CharacterList> ();
		charList.DeserializeCharacters (characterStrings);

		GetComponent<CharacterList> ().enabled = true; //Enable GUI
    }

    private bool HandleDatasetDeleted(Dataset dataset)
    {
        Debug.Log(dataset.Metadata.DatasetName + " Dataset has been deleted");

        CharacterList charList = GetComponent<CharacterList> ();
        charList.DeleteAllCharacters ();

        // returning true informs the corresponding dataset can be purged in the local storage and return false retains the local dataset
        return true;
    }

    private bool HandleDatasetMerged(Dataset localDataset, List<string> mergedDatasetNames)
    {
		Debug.LogWarning("Sync merge");

        if (mergeInCourse) {
			Debug.LogWarning ("Already in a merge");
			return false;            
        }

		//This variable can be used to hold the game from actually starting
        //and show a loading indicator to the user meanwhile
        mergeInCourse = true;
        
        //Delete the merged datasets and just keep the local data
        foreach (string name in mergedDatasetNames) {

			Dataset mergedDataset = syncManager.OpenOrCreateDataset(name);
			//mergedDataset.Delete(); //Remove any data we could have from a previous execution of this handler
            //Lambda function to delete the dataset after fetching it
            EventHandler<SyncSuccessEvent> lambda;
            Debug.Log ("fetching dataset to merge: " + name);
            lambda = (object sender, SyncSuccessEvent e) => { 
                //Actual merge code: We join the local characters and the remote characters into a new single dataset.
                List<string> allCharacters = new List<string>();
				ICollection<string> existingCharacters = localDataset.GetAll().Values;
				ICollection<string> newCharacters = mergedDataset.GetAll().Values;
				Debug.LogWarning(localDataset.Metadata.DatasetName + ": " + existingCharacters.Count);
				Debug.LogWarning(mergedDataset.Metadata.DatasetName + ": " + newCharacters.Count);
                allCharacters.AddRange(existingCharacters);
                allCharacters.AddRange(newCharacters);
                int i = 0;
                foreach (string characterString in allCharacters) {
					localDataset.Put (indexToKey(i++), characterString);
                }

                Debug.Log ("deleting merged dataset: " + name);
				mergedDataset.Delete(); //Delete the dataset locally
				mergedDataset.OnSyncSuccess -= lambda; //We don't want this callback to be fired again
				mergedDataset.OnSyncSuccess += (object s2, SyncSuccessEvent e2) => {
                    Debug.Log ("merge comleted (dataset merged and deleted)");
                    mergeInCourse = false;
					localDataset.Synchronize(); //Continue the sync operation that was interrupted by the merge
                };
				mergedDataset.Synchronize(); //Synchronize it as deleted, failing to do so will leave us in an inconsistent state

            };
            
			mergedDataset.OnSyncSuccess += lambda;
			mergedDataset.Synchronize(); //Asnchronously fetch the dataset
        }

        // returning true allows the Synchronize to continue and false cancels it
		return true;
    }

    private bool HandleSyncConflict(Amazon.CognitoSync.SyncManager.Dataset dataset, List<SyncConflict> conflicts)
    {
		if (dataset.Metadata != null) {
			Debug.LogWarning("Sync conflict " + dataset.Metadata.DatasetName);
		} else {
			//In case we called synchronize after deleting the dataset, we can not access it anymore
			Debug.LogWarning("Sync conflict");
		}

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