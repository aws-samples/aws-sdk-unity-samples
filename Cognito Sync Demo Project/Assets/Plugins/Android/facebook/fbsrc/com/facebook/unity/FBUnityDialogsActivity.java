package com.facebook.unity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.facebook.FacebookOperationCanceledException;
import com.facebook.UiLifecycleHelper;
import com.facebook.widget.FacebookDialog;
import com.facebook.widget.FacebookDialog.PendingCall;

public class FBUnityDialogsActivity extends Activity {
    public static final String DIALOG_TYPE = "dialog_type";
    public static final String DIALOG_PARAMS = "dialog_params";
    private UiLifecycleHelper uiHelper;
    private FBDialogUtils.DialogType dialogType;
    private Bundle params;
    
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        uiHelper = new UiLifecycleHelper(this, null);
        uiHelper.onCreate(savedInstanceState);                
        params = getIntent().getBundleExtra(DIALOG_PARAMS);
        dialogType = (FBDialogUtils.DialogType) getIntent().getSerializableExtra(DIALOG_TYPE);
        
        switch (dialogType) {
            case SHARE_DIALOG:
                final FacebookDialog.ShareDialogBuilder builder = 
                    FBDialogUtils.createShareDialogBuilder(this, params);
                uiHelper.trackPendingDialogCall(builder.build().present());
                break;
            default:
                Log.e(FB.TAG, "Unrecognized Dialog Type");
        }
    }
        
    @Override
    protected void onResume() {
        super.onResume();
        uiHelper.onResume();
    }

    @Override
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        uiHelper.onSaveInstanceState(outState);
    }

    @Override
    public void onPause() {
        super.onPause();
        uiHelper.onPause();
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        uiHelper.onDestroy();
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
                
        final UnityMessage response;
        
        switch (dialogType) {
            case SHARE_DIALOG:
                response = new UnityMessage("OnFeedRequestComplete");
                String callbackID = params.getString("callback_id");
                if (callbackID != null) {
                	response.put("callback_id", callbackID);
                }        
                uiHelper.onActivityResult(requestCode, resultCode, data, new FacebookDialog.Callback() {
                    
                    @Override
                    public void onError(PendingCall pendingCall, Exception error, Bundle data) {
                        if (error instanceof FacebookOperationCanceledException) {
                            response.putCancelled();
                            response.send();
                        } else {
                            response.sendError(error.toString());
                        }
                    }
                    
                    @Override
                    public void onComplete(PendingCall pendingCall, Bundle data) {
                        // Verifying that the dialog did actually complete with a post gesture.
                        if (FacebookDialog.getNativeDialogDidComplete(data)
                                && FacebookDialog.getNativeDialogCompletionGesture(data).equals(
                                        FacebookDialog.COMPLETION_GESTURE_POST)) {
                            final String postID = FacebookDialog.getNativeDialogPostId(data);
                            if (postID != null) {
                                response.putID(postID);
                            }
                            // Unity SDK requires to have at least one key beside callback_id.
                            response.put("posted", true);
                        } else {
                            response.putCancelled();
                        }
                        response.send();
                    }
                });
                break;
            default:
                Log.e(FB.TAG, "Unrecognized Dialog Type");
                return;
        }
        this.finish();
    }
}
