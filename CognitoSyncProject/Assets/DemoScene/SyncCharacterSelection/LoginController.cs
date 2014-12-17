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

/// <summary>
/// Handles the login of FB-authenticated or unauthenticated users
/// and then activates and initializes the SaveManager component.
/// </summary>
public class LoginController : MonoBehaviour {

    private bool loggingIn = true;

    private void Start () {
        FB.Init(InitCallback);
    }

    private void InitCallback()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log ("User was already logged in with FB");
            OnLoggedIn ();
        }
        else
        {
            loggingIn = false;
        }
    }

    private void LoginCallback(FBResult result)
    {
        Debug.Log("FB.Login completed");

        if (FB.IsLoggedIn)
        {
            OnLoggedIn ();
        }
        else
        {
            loggingIn = false;
        }
    }

    private void OnLoggedIn()
    {
        GetComponent<SaveManager> ().InitWithFacebook (FB.AccessToken);
    }

    private void OnGUI()
    {
        if (!loggingIn)
        {
            float ratio = Screen.width/600.0f;
            GUI.skin.button.fontSize = (int)(15*ratio);
            if (GUI.Button (new Rect (Screen.width / 2 - 80*ratio, Screen.height / 2 - 20*ratio, 160*ratio, 30*ratio), "Login with Facebook"))
            {
                loggingIn = true;
                FB.Login ("email", LoginCallback);
            }
            if (GUI.Button (new Rect (Screen.width / 2 - 80*ratio, Screen.height / 2 + 20*ratio, 160*ratio, 30*ratio), "Skip authentication"))
            {
                loggingIn = true;
                GetComponent<SaveManager> ().InitWithoutFacebook ();
            }
        }
    }

}
