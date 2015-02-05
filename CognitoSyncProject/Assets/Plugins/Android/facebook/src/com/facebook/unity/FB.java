package com.facebook.unity;

import java.math.BigDecimal;
import java.util.*;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

import android.annotation.TargetApi;
import android.content.Intent;
import android.app.Activity;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.util.Base64;
import android.content.pm.*;
import android.content.pm.PackageManager.NameNotFoundException;

import com.facebook.*;
import com.facebook.internal.Utility;
import com.facebook.widget.FacebookDialog;
import com.facebook.widget.WebDialog;
import com.facebook.widget.WebDialog.OnCompleteListener;
import com.unity3d.player.UnityPlayer;

public class FB {
    static final String TAG = "FBUnitySDK";
    // i.e. the game object that receives this message
    static final String FB_UNITY_OBJECT = "UnityFacebookSDKPlugin";
    private static Intent intent;
    private static AppEventsLogger appEventsLogger;

    private static Boolean frictionlessRequests = false;

    private static AppEventsLogger getAppEventsLogger() {
        if (appEventsLogger == null) {
            appEventsLogger = AppEventsLogger.newLogger(getUnityActivity().getApplicationContext());
        }
        return appEventsLogger;
    }

	public static boolean isLoggedIn() {
        return Session.getActiveSession() != null && Session.getActiveSession().isOpened();
    }


	public static Activity getUnityActivity() {
        return UnityPlayer.currentActivity;
    }

    @UnityCallable
    public static void Init(String params) {
        UnityParams unity_params = UnityParams.parse(params, "couldn't parse init params: "+params);
        if (unity_params.hasString("frictionlessRequests")) {
            frictionlessRequests = Boolean.valueOf(unity_params.getString("frictionlessRequests"));
        }
	    String appID;
        if (unity_params.hasString("appId")) {
            appID = unity_params.getString("appId");
        } else {
            appID = Utility.getMetadataApplicationId(getUnityActivity());
        }

	    // tries to log the user in if they've already TOS'd the app
		FBLogin.init(appID);
    }

    @UnityCallable
    public static void Login(String params) {
        Intent intent = new Intent(getUnityActivity(), FBUnityLoginActivity.class);
        intent.putExtra(FBUnityLoginActivity.LOGIN_PARAMS, params);
        getUnityActivity().startActivity(intent);
    }

    @UnityCallable
    public static void Logout(String params) {
        Session.getActiveSession().closeAndClearTokenInformation();
        new UnityMessage("OnLogoutComplete").send();
    }

    @UnityCallable
    public static void AppRequest(String params_str) {
        Log.v(TAG, "sendRequestDialog(" + params_str + ")");
        final UnityMessage response = new UnityMessage("OnAppRequestsComplete");

        if (!isLoggedIn()) {
            response.sendNotLoggedInError();
            return;
        }

        UnityParams unity_params = UnityParams.parse(params_str);
        if (unity_params.hasString("callback_id")) {
            response.put("callback_id", unity_params.getString("callback_id"));
        }

        final Bundle params = unity_params.getStringParams();
        if (params.containsKey("callback_id")) {
            params.remove("callback_id");
        }

        if (frictionlessRequests) {
            params.putString("frictionless", "true");
        }

        getUnityActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                // TODO Auto-generated method stub
                WebDialog requestsDialog = (
                        new WebDialog.RequestsDialogBuilder(getUnityActivity(),
                                Session.getActiveSession(),
                                params))
                                .setOnCompleteListener(new OnCompleteListener() {

                                    @Override
                                    public void onComplete(Bundle values,
                                            FacebookException error) {

                                        if (error != null) {
                                            if(error.toString().equals("com.facebook.FacebookOperationCanceledException")) {
                                                response.putCancelled();
                                                response.send();
                                            } else {
                                                response.sendError(error.toString());
                                            }
                                        } else {
                                            if(values != null) {
                                                final String requestId = values.getString("request");
                                                if(requestId == null) {
                                                    response.putCancelled();
                                                }
                                            }

                                            for (String key : values.keySet()) {
                                                response.put(key, values.getString(key));
                                            }
                                            response.send();
                                        }

                                    }

                                })
                                .build();
                requestsDialog.show();

            }
        });
    }
    
    @UnityCallable
    public static void GameGroupCreate(String params_str) {
        final UnityParams unity_params = UnityParams.parse(params_str);
        
        final UnityMessage response = new UnityMessage("OnGroupCreateComplete");
        if (unity_params.hasString("callback_id")) {
            response.put("callback_id", unity_params.getString("callback_id"));
        }
        
        if (!isLoggedIn()) {
            response.sendNotLoggedInError();
            return;
        }
        
        final Bundle params = unity_params.getStringParams();
        if(params.containsKey("callback_id")) {
            params.remove("callback_id");
        }
        
        getUnityActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                WebDialog feedDialog = (
                    new WebDialog.Builder(getUnityActivity(),
                            Session.getActiveSession(),
                            "game_group_create",
                            params))
                            .setOnCompleteListener(new OnCompleteListener() {
                                @Override
                                public void onComplete(Bundle values,
                                                       FacebookException error) {
                                    // response
                                    if (error == null) {
                                        final String postID = values.getString("id");
                                        if (postID != null) {
                                            response.putID(postID);
                                        } else {
                                            response.putCancelled();
                                        }
                                        response.send();
                                    } else if (error instanceof FacebookOperationCanceledException) {
                                        // User clicked the "x" button
                                        response.putCancelled();
                                        response.send();
                                    } else {
                                        // Generic, ex: network error
                                        response.sendError(error.toString());
                                    }
                                }
                            })
                            .build();
                            feedDialog.show();
            }
        });
    }
    
    @UnityCallable
    public static void GameGroupJoin(String params_str) {
        final UnityParams unity_params = UnityParams.parse(params_str);
        
        final UnityMessage response = new UnityMessage("OnGroupCreateComplete");
        if (unity_params.hasString("callback_id")) {
            response.put("callback_id", unity_params.getString("callback_id"));
        }
        
        if (!isLoggedIn()) {
            response.sendNotLoggedInError();
            return;
        }
        
        final Bundle params = unity_params.getStringParams();
        if(params.containsKey("callback_id")) {
            params.remove("callback_id");
        }
        
        getUnityActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                WebDialog feedDialog = (
                    new WebDialog.Builder(getUnityActivity(),
                            Session.getActiveSession(),
                            "game_group_join",
                            params))
                            .setOnCompleteListener(new OnCompleteListener() {
                                @Override
                                public void onComplete(Bundle values,
                                                       FacebookException error) {
                                    // response
                                    if (error == null) {
                                        final String postID = values.getString("id");
                                        if (postID != null) {
                                            response.putID(postID);
                                        } else {
                                            response.putCancelled();
                                        }
                                        response.send();
                                    } else if (error instanceof FacebookOperationCanceledException) {
                                        // User clicked the "x" button
                                        response.putCancelled();
                                        response.send();
                                    } else {
                                        // Generic, ex: network error
                                        response.sendError(error.toString());
                                    }
                                }
                            })
                            .build();
                            feedDialog.show();
            }
        });
    }

    @UnityCallable
    public static void FeedRequest(String params_str) {
        Log.v(TAG, "FeedRequest(" + params_str + ")");
        final UnityMessage response = new UnityMessage("OnFeedRequestComplete");

        UnityParams unity_params = UnityParams.parse(params_str);
        if (unity_params.hasString("callback_id")){
            response.put("callback_id", unity_params.getString("callback_id"));
        }

        if (!isLoggedIn()) {
            response.sendNotLoggedInError();
            return;
        }

        final Bundle params = unity_params.getStringParams();
                
        if (!FacebookDialog.canPresentShareDialog(getUnityActivity()) || 
            FBDialogUtils.hasUnsupportedParams(FBDialogUtils.DialogType.SHARE_DIALOG, params)) {
            if (params.containsKey("callback_id")) {
                params.remove("callback_id");
            }
                
            getUnityActivity().runOnUiThread(new Runnable() {

                @Override
                public void run() {
                    WebDialog feedDialog = (
                            new WebDialog.FeedDialogBuilder(getUnityActivity(),
                                    Session.getActiveSession(),
                                    params))
                            .setOnCompleteListener(new OnCompleteListener() {
        
                                @Override
                                public void onComplete(Bundle values,
                                                       FacebookException error) {

                                    // response
                                    if (error == null) {
                                        final String postID = values.getString("post_id");
                                            if (postID != null) {
                                                response.putID(postID);
                                            } else {
                                                response.putCancelled();
                                            }
                                            response.send();
                                        } else if (error instanceof FacebookOperationCanceledException) {
                                            // User clicked the "x" button
                                            response.putCancelled();
                                            response.send();
                                        } else {
                                            // Generic, ex: network error
                                            response.sendError(error.toString());
                                        }
                                    }
        
                                })
                                .build();
                        feedDialog.show();
                }
            });
        } else {
	        Intent intent = new Intent(getUnityActivity(), FBUnityDialogsActivity.class);
	        intent.putExtra(FBUnityDialogsActivity.DIALOG_TYPE, FBDialogUtils.DialogType.SHARE_DIALOG);
	        intent.putExtra(FBUnityDialogsActivity.DIALOG_PARAMS, params);
	        getUnityActivity().startActivity(intent);
        }
    }

    @UnityCallable
    public static void PublishInstall(String params_str) {
        final UnityMessage unityMessage = new UnityMessage("OnPublishInstallComplete");
        final UnityParams unity_params = UnityParams.parse(params_str);
        if (unity_params.hasString("callback_id")) {
            unityMessage.put("callback_id", unity_params.getString("callback_id"));
        }
        AppEventsLogger.activateApp(getUnityActivity().getApplicationContext());
        unityMessage.send();
    }

    @UnityCallable
    public static void ActivateApp(String params_str) {
        final UnityParams unity_params = UnityParams.parse(params_str);
        if (unity_params.hasString("app_id")) {
            AppEventsLogger.activateApp(getUnityActivity().getApplicationContext(), unity_params.getString("app_id"));
        } else {
            AppEventsLogger.activateApp(getUnityActivity().getApplicationContext());
        }
    }

    @UnityCallable
    public static void GetDeepLink(String params_str) {
        final UnityMessage unityMessage = new UnityMessage("OnGetDeepLinkComplete");
        if (intent != null && intent.getData() != null) {
            unityMessage.put("deep_link", intent.getData().toString());
        } else {
            unityMessage.put("deep_link", "");
        }
        unityMessage.send();
    }

    public static void SetIntent(Intent intent) {
        FB.intent = intent;
        GetDeepLink("");
    }

    public static void SetLimitEventUsage(String params) {
        Settings.setLimitEventAndDataUsage(getUnityActivity().getApplicationContext(), Boolean.valueOf(params));
    }

    @UnityCallable
    public static void AppEvents(String params) {
        Log.v(TAG, "AppEvents(" + params + ")");
        UnityParams unity_params = UnityParams.parse(params);

        Bundle parameters = new Bundle();
        if (unity_params.has("parameters")) {
            UnityParams unity_params_parameter = unity_params.getParamsObject("parameters");
            parameters = unity_params_parameter.getStringParams();
        }

        if (unity_params.has("logPurchase")) {
            FB.getAppEventsLogger().logPurchase(
                    new BigDecimal(unity_params.getDouble("logPurchase")),
                    Currency.getInstance(unity_params.getString("currency")),
                    parameters
            );
        } else if (unity_params.hasString("logEvent")) {
            if (unity_params.has("valueToSum")) {
                FB.getAppEventsLogger().logEvent(
                        unity_params.getString("logEvent"),
                        unity_params.getDouble("valueToSum"),
                        parameters
                );
            } else {
                FB.getAppEventsLogger().logEvent(
                        unity_params.getString("logEvent"),
                        parameters
                );
            }
        } else {
            Log.e(TAG, "couldn't logPurchase or logEvent params: "+params);
        }
    }

    /**
     * Provides the key hash to solve the openSSL issue with Amazon
     * @return key hash
     */
    @TargetApi(Build.VERSION_CODES.FROYO)
    public static String getKeyHash() {
        try {
            PackageInfo info = getUnityActivity().getPackageManager().getPackageInfo(
                getUnityActivity().getPackageName(), PackageManager.GET_SIGNATURES);
            for (Signature signature : info.signatures){
                MessageDigest md = MessageDigest.getInstance("SHA");
                md.update(signature.toByteArray());
                String keyHash = Base64.encodeToString(md.digest(), Base64.DEFAULT);
                Log.d(TAG, "KeyHash: " + keyHash);
                return keyHash;
            }
        } catch (NameNotFoundException e) {
        } catch (NoSuchAlgorithmException e) {
        }
        return "";
    }
}
