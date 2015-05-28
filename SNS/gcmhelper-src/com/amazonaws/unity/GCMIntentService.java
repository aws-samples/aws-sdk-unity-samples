package com.amazonaws.unity;

import android.app.IntentService;
import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;

public class GCMIntentService extends IntentService {

	public static final String TAG = "GCMIntentService";

	private static final String MESSAGE = "message";
	private static final String DEFAULT = "default";

	public GCMIntentService() {
		super("GcmIntentService");
	}

	@Override
	protected void onHandleIntent(Intent intent) {
		try {
			String action = intent.getAction();
			if (action
					.equalsIgnoreCase("com.google.android.c2dm.intent.RECEIVE")) {
				Bundle extras = intent.getExtras();
				if (TextUtils.isEmpty(extras.getString(MESSAGE))) {
					HandleMessage(intent, extras.getString(DEFAULT));
					// to handle RAW format of data
				} else {
					HandleMessage(intent, extras.getString(MESSAGE));
					// to handle data of json format
				}
			}
		} catch (Exception e) {
			Log.e(TAG, e.getMessage());
			Log.e(TAG, e.getStackTrace().toString());
		} finally {
			// Release the wake lock provided by the WakefulBroadcastReceiver.
			GCMBroadcastReceiver.completeWakefulIntent(intent);
		}
	}

	private void HandleMessage(Intent intent, String message) {
		Utils.showNotification(this, message);
	}

}
