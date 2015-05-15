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

using UnityEngine.SocialPlatforms;

public class IdentityManager : MonoBehaviour
{

	public string IDENTITY_POOL_ID = "";
	public RegionEndpoint ENDPOINT = RegionEndpoint.USEast1;
	
	private CognitoAWSCredentials credentials;
    
	string myIdentity = "";

	bool loading = false;
	bool loggedIn = false;

    private void Start () {
		loading = true;
		FB.Init(FacebookInitCallback);
    }

	private void FacebookInitCallback()
	{
		/*if (FB.IsLoggedIn)
		{
			InitWithFacebook (FB.AccessToken);
		}
		else
		{
			loading = false;
		}*/
		loading = false;
	}

    private void FacebookLoginCallback(FBResult result)
    {
		Debug.Log("FB Login completed");
		if (FB.IsLoggedIn)
		{
			InitWithFacebook (FB.AccessToken);
		}
		else
		{
			loading = false;
		}
    }

    private void OnGUI()
    {
		float ratio = Screen.width/600.0f;
		GUI.skin.label.fontSize = (int)(15*ratio);
		GUI.skin.button.fontSize = (int)(15*ratio);

		GUI.Label(new Rect(30*ratio, 30*ratio, 800*ratio, 30*ratio), "Identity: " + myIdentity);
        
        if (loading)
		{
			return;
		}

        if (loggedIn) {

			if (GUI.Button (new Rect (Screen.width / 2 - 80*ratio, Screen.height / 2 - 40*ratio, 160*ratio, 30*ratio), "GetCognitoId"))
			{
				UpdateIdentity ();
			}

			if (GUI.Button (new Rect (Screen.width / 2 - 80*ratio, Screen.height / 2 , 160*ratio, 30*ratio), "Clear identity"))
			{
				myIdentity = "";
				credentials.Clear();
			}
			
			if (GUI.Button (new Rect (Screen.width / 2 - 80*ratio, Screen.height / 2 + 40*ratio, 160*ratio, 30*ratio), "Logout"))
			{
				Application.LoadLevel(Application.loadedLevel);
				loggedIn = false;
			}

		} else {
			
			GUI.skin.button.fontSize = (int)(15*ratio);
			if (GUI.Button (new Rect (Screen.width / 2 - 80*ratio, Screen.height / 2 - 20*ratio, 160*ratio, 30*ratio), "Login with Facebook"))
			{
				loading = true;
				FB.Login ("email", FacebookLoginCallback);
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 80*ratio, Screen.height / 2 + 20*ratio, 160*ratio, 30*ratio), "Skip authentication"))
			{
				loading = true;
				InitUnauth ();
			}
		}
			
    }

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
	/// Inits Cognito Sync with FB-authenticated user credentials.
	/// </summary>
	/// <param name="fbAccessToken">The FB access token returned by the FB SDK.</param>
	public void InitWithGoogle(string gpAccessToken) 
	{
		try {
			Init("accounts.google.com", gpAccessToken);
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
		if (string.IsNullOrEmpty(IDENTITY_POOL_ID))
		{
			throw new NotSupportedException ("Identity Pool ID is not set");
		}
		
		//Enable Logs
		//AmazonLogging.Level = AmazonLogging.LoggingLevel.DEBUG;

		//Create a Credentials provider that uses Cognito Identity
		credentials = new CognitoAWSCredentials(IDENTITY_POOL_ID, ENDPOINT);

		myIdentity = credentials.GetIdentityId();
		/*
		credentials.IdentityChangedEvent += (object sender, Amazon.CognitoIdentity.CognitoAWSCredentials.IdentityChangedArgs e) => {
			Debug.Log("Identity Changed (old: '"+e.OldIdentityId+"', new: '"+e.NewIdentityId+"')");
			myIdentity = e.NewIdentityId;
		};

		//If fbAccesToken is set, we can use Cognito's authenticated identities
		if (accessToken != null) {
			credentials.AddLogin(authProvider, accessToken);
		}

		loggedIn = true;

		UpdateIdentity();*/

	}
		

	/// <summary>
	/// Retrieve an identity string from Cognito, using Facebook for
	/// authentication if possible.
	/// </summary>
	/// <param name="callback">Callback function, callback(error, identity).</param>
	public void UpdateIdentity()
	{
		loading = true;
		myIdentity = credentials.GetIdentityId();
	}
	


}