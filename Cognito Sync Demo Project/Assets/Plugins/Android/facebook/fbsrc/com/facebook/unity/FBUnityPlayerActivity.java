package com.facebook.unity;

import android.content.Intent;
import android.os.Bundle;

import com.unity3d.player.UnityPlayerActivity;

/*
 * @deprecated
 * Since 5.0 Facebook Unity SDK does no longer require to use main activity
 * To properly upgrade to 5.0, use UnityPlayerActivity as your main activity or extend it with your own custom class in manifest
 *  and set com.facebook.unity.FBUnityDeepLinkingActivity as your deep-linking class in the facebook developer dashboard.
 */
@Deprecated
public class FBUnityPlayerActivity extends UnityPlayerActivity {

    @Override
    protected void onCreate(Bundle arg0) {
        super.onCreate(arg0);
        FB.SetIntent(this.getIntent());
    }

    @Override
    public void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        FB.SetIntent(intent);
    }
}
