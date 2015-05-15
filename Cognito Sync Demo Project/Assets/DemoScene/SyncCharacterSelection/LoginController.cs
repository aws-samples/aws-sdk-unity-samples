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
using System.Collections;
using UnityEngine.SocialPlatforms;

/// <summary>
/// Handles the login of FB-authenticated or unauthenticated users
/// and then activates and initializes the SaveManager component.
/// </summary>
public class LoginController : MonoBehaviour {

	[HideInInspector]
    public bool loggingIn = false;

#if !UNITY_WEBPLAYER
    private void Start () {
		loggingIn = true;
		FB.Init(FacebookInitCallback);
    }

	private void FacebookInitCallback()
	{
		Debug.Log("FB Init completed" + FB.IsLoggedIn + "-" + FB.AccessToken);
		if (FB.IsLoggedIn)
		{
			GetComponent<SaveManager> ().InitWithFacebook (FB.AccessToken);
		}
		else
		{
			loggingIn = false;
		}
	}

    private void FacebookLoginCallback(FBResult result)
    {
		Debug.Log("FB Login completed");
		if (FB.IsLoggedIn)
		{
			GetComponent<SaveManager> ().InitWithFacebook (FB.AccessToken);
		}
		else
		{
			loggingIn = false;
		}
    }
#endif

    private void OnGUI()
    {
        if (!loggingIn)
        {
            float ratio = Screen.width/600.0f;
            GUI.skin.button.fontSize = (int)(15*ratio);
            if (GUI.Button (new Rect (Screen.width / 2 - 80*ratio, Screen.height / 2 - 20*ratio, 160*ratio, 30*ratio), "Login with Facebook"))
            {
                loggingIn = true;
#if !UNITY_WEBPLAYER
                FB.Login ("email", FacebookLoginCallback);
#else
				Debug.LogError("Facebook is disabled when running in webplayer because it can only be used inside a Facebook app canvas.");
#endif
            }
            if (GUI.Button (new Rect (Screen.width / 2 - 80*ratio, Screen.height / 2 + 20*ratio, 160*ratio, 30*ratio), "Skip authentication"))
            {
                loggingIn = true;
                GetComponent<SaveManager> ().InitUnauth ();
            }
        }
    }

}
