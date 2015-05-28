package com.amazonaws.unity;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.text.TextUtils;
import android.util.Log;

import com.google.android.gms.gcm.GoogleCloudMessaging;
import com.unity3d.player.UnityPlayer;

import java.io.IOException;

public class AWSUnityGCMWrapper {

	private static final String TAG = "AWSUnityGCMWrapper";

	public static String register(final String senderIds) {
		try {
			if (senderIds == null) {
				return "";
			}
			Activity activity = UnityPlayer.currentActivity;
			GoogleCloudMessaging gcm = GoogleCloudMessaging
					.getInstance(activity);
			String regId = gcm.register(senderIds);
			storeRegistrationId(activity, regId);
			return regId;
		} catch (IOException e) {
			Log.e(TAG, "failed to register the to gcm");
			Log.e(TAG, "exception = " + e.getMessage());
		}
		return "";
	}

	public static void unregister() {
		try {
			Activity activity = UnityPlayer.currentActivity;
			GoogleCloudMessaging gcm = GoogleCloudMessaging
					.getInstance(activity);
			gcm.unregister();
		} catch (IOException e) {
			Log.e(TAG, "failed to unregister from gcm");
			Log.e(TAG, "exception = " + e.getMessage());
		}
	}

	public static boolean isRegistered(Context context) {
		String regId = getRegistrationId(context);
		if (TextUtils.isEmpty(regId)) {
			return false;
		} else {
			return true;
		}
	}

	public static final String PROPERTY_REG_ID = "registration_id";
	private static final String PROPERTY_APP_VERSION = "appVersion";

	public static String getRegistrationId(Context context) {
		final SharedPreferences prefs = getGCMPreferences(context);
		String registrationId = prefs.getString(PROPERTY_REG_ID, "");
		if (TextUtils.isEmpty(registrationId)) {
			Log.i(TAG, "Registration not found.");
			return "";
		}
		// Check if app was updated; if so, it must clear the registration ID
		// since the existing registration ID is not guaranteed to work with
		// the new app version.
		int registeredVersion = prefs.getInt(PROPERTY_APP_VERSION,
				Integer.MIN_VALUE);
		int currentVersion = getAppVersion(context);
		if (registeredVersion != currentVersion) {
			Log.i(TAG, "App version changed.");
			return "";
		}
		return registrationId;
	}

	private static void storeRegistrationId(Context context, String regId) {
		final SharedPreferences prefs = getGCMPreferences(context);
		int appVersion = getAppVersion(context);
		Log.i(TAG, "Saving regId on app version " + appVersion);
		SharedPreferences.Editor editor = prefs.edit();
		editor.putString(PROPERTY_REG_ID, regId);
		editor.putInt(PROPERTY_APP_VERSION, appVersion);
		editor.commit();
	}

	private static SharedPreferences getGCMPreferences(Context context) {
		// This sample app persists the registration ID in shared preferences,
		// but
		// how you store the registration ID in your app is up to you.
		return context.getSharedPreferences(UnityPlayer.class.getSimpleName(),
				Context.MODE_PRIVATE);
	}

	private static int getAppVersion(Context context) {
		try {
			PackageInfo packageInfo = context.getPackageManager()
					.getPackageInfo(context.getPackageName(), 0);
			return packageInfo.versionCode;
		} catch (PackageManager.NameNotFoundException e) {
			// should never happen
			throw new RuntimeException("Could not get package name: " + e);
		}
	}
}
