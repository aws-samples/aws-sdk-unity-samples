package com.facebook.unity;

import java.util.Arrays;
import java.util.List;

import com.facebook.widget.FacebookDialog;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;

public class FBDialogUtils {
    private static final List<String> SUPPORTED_SHARE_DIALOG_PARAMS =
    	Arrays.asList("callback_id", "name", "caption", "description", "link", "picture",
    				  "place", "friends", "ref");    		
        
    public static enum DialogType {
        SHARE_DIALOG
    };
        
    public static FacebookDialog.ShareDialogBuilder createShareDialogBuilder(Activity activity, Bundle params) {
        FacebookDialog.ShareDialogBuilder builder = new FacebookDialog.ShareDialogBuilder(activity);
        
        if (params.containsKey("name")) {
            builder.setName(params.getString("name"));
        }

        if (params.containsKey("caption")) {
            builder.setCaption(params.getString("caption"));
        }

        if (params.containsKey("description")) {
            builder.setDescription(params.getString("description"));
        }

        if (params.containsKey("link")) {
            builder.setLink(params.getString("link"));
        }

        if (params.containsKey("picture")) {
            builder.setPicture(params.getString("picture"));
        }

        if (params.containsKey("place")) {
            builder.setPlace(params.getString("place"));
        }

        if (params.containsKey("ref")) {
            builder.setRef(params.getString("ref"));
        }
               
        return builder;
    }
        
    public static boolean hasUnsupportedParams(DialogType dialogType, Bundle params) {
        switch (dialogType) {
            case SHARE_DIALOG:
                return !SUPPORTED_SHARE_DIALOG_PARAMS.containsAll(params.keySet());
            default:
                Log.e(FB.TAG, "Unrecognized Dialog Type");
                return false;
        }
    }

}
