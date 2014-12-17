package com.facebook.unity;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;
import android.util.Log;
import com.unity3d.player.UnityPlayer;


public class UnityMessage {
    private String methodName;
    private Map<String, Serializable> params = new HashMap<String, Serializable>();

    public UnityMessage(String methodName) {
        this.methodName = methodName;
    }

    public UnityMessage put(String name, Serializable value) {
        params.put(name, value);
        return this;
    }

    public UnityMessage putCancelled() {
        put("cancelled", true);
        return this;
    }

    public UnityMessage putID(String id) {
        put("id", id);
        return this;
    }

    public void sendNotLoggedInError() {
        sendError("not logged in");
    }

    public void sendError(String errorMsg) {
        this.put("error", errorMsg);
        send();
    }

    public void send() {
        assert methodName != null : "no method specified";
        String message = new UnityParams(this.params).toString();
        Log.v(FB.TAG,"sending to Unity "+this.methodName+"("+message+")");
        try {
            UnityPlayer.UnitySendMessage(FB.FB_UNITY_OBJECT, this.methodName, message);
        } catch (UnsatisfiedLinkError e) {
            Log.v(FB.TAG, "message not send, Unity not initialized");
        }
    }
}
