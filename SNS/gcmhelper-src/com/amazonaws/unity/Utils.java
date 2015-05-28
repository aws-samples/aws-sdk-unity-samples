package com.amazonaws.unity;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.res.Resources;
import android.support.v4.app.NotificationCompat;

import com.unity3d.player.UnityPlayerProxyActivity;

public class Utils {

	private static final int REQUEST_CODE = 1001;
	private static final int NOTIFICATION_ID = 1;

	public static void showNotification(Context context, String contentTitle,
			String contentText) {
		// Intent
		Intent intent = new Intent(context, UnityPlayerProxyActivity.class);
		PendingIntent contentIntent = PendingIntent.getActivity(context,
				REQUEST_CODE, intent, PendingIntent.FLAG_UPDATE_CURRENT);

		NotificationCompat.Builder builder = new NotificationCompat.Builder(
				context.getApplicationContext());
		builder.setContentIntent(contentIntent);
		builder.setContentText(contentText);
		builder.setContentTitle(contentTitle);
		builder.setWhen(System.currentTimeMillis());
		builder.setAutoCancel(true);

		Resources res = context.getResources();
		builder.setSmallIcon(res.getIdentifier("app_icon", "drawable",
				context.getPackageName()));

		builder.setDefaults(Notification.DEFAULT_SOUND
				| Notification.DEFAULT_VIBRATE | Notification.DEFAULT_LIGHTS);

		NotificationManager nm = (NotificationManager) context
				.getSystemService(Context.NOTIFICATION_SERVICE);
		nm.notify(NOTIFICATION_ID, builder.build());
	}

	public static void showNotification(Context context, String contentText) {
		showNotification(context, "", contentText);
	}
}
