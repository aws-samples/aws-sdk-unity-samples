package com.facebook.unity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Window;

public class FBUnityDeepLinkingActivity extends Activity{
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		
		Log.v(FB.TAG, "Saving deep link from deep linking activity");
		FB.SetIntent(this.getIntent());
		
		Log.v(FB.TAG, "Returning to main activity");
		//start main activity
		Intent newIntent = new Intent(this, getMainActivityClass());
		this.startActivity(newIntent);
        finish();
	}

	private Class<?> getMainActivityClass() {
        String packageName = this.getPackageName();
        Intent launchIntent = this.getPackageManager().getLaunchIntentForPackage(packageName);
        try {
            return Class.forName(launchIntent.getComponent().getClassName());
        } catch (Exception e) {
            Log.e(FB.TAG, "Unable to find Main Activity Class");
            return null;
        }
	}
}
