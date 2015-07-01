package com.facebook.unity;

import com.facebook.Session;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

public class FBUnityLoginActivity extends Activity {
	public static final String LOGIN_PARAMS = "login_params";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        FBLogin.login(getIntent().getStringExtra(LOGIN_PARAMS), this);
	}

	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data) {
		super.onActivityResult(requestCode, resultCode, data);
		// Unity might get killed in the background, in that case just finish
		if (Session.getActiveSession() != null) {
			FBLogin.onActivityResult(this, requestCode, resultCode, data);
		} else {
			finish();
		}
	}

}
