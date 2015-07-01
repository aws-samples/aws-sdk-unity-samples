package com.facebook.unity;

import java.io.Serializable;
import java.util.Iterator;
import java.util.Map;

import org.json.JSONException;
import org.json.JSONObject;
import android.os.Bundle;
import android.util.Log;

/**
 * Wrapping JSONObject object used to communicate with Unity, catch errors, share code in one place
 */
public class UnityParams {
    JSONObject json;

    public UnityParams(String s) throws JSONException {
        json = new JSONObject(s);
    }

    public UnityParams(JSONObject obj) {
        json = obj;
    }

    public UnityParams(Map<String, Serializable> map) {
        json = new JSONObject(map);
    }

    /*
     * Parse params, log error
     */
    public static UnityParams parse(String data, String msg) {
        try {
            return new UnityParams(data);
        } catch (JSONException e) {
            Log.e(FB.TAG, msg);
        }
        return null;
    }

    public static UnityParams parse(String data) {
        return parse(data, "couldn't parse params: " + data);
    }

    public String getString(String key) {
        try {
            return json.getString(key);
        } catch (JSONException e) {
            Log.e(FB.TAG, "cannot get string " + key + " from " + this.toString());
            return "";
        }
    }

    public double getDouble(String key) {
        try {
            return json.getDouble(key);
        } catch (JSONException e) {
            Log.e(FB.TAG, "cannot get double " + key + " from " + this.toString());
            return 0;
        }
    }

    public UnityParams getParamsObject(String key) {
        try {
            return new UnityParams(json.getJSONObject(key));
        } catch (JSONException e) {
            Log.e(FB.TAG, "cannot get object " + key + " from " + this.toString());
            return null;
        }
    }

    public void put(String name, Object value) {
        try {
            json.put(name, value);
        } catch (JSONException e) {
            Log.e(FB.TAG, "couldn't add key " + name + " to " + this.toString());
        }
    }

    public boolean has(String key) {
        return json.has(key) && !json.isNull(key);
    }

    public Boolean hasString(String key) {
        return this.has(key) && this.getString(key) != "";
    }

    /*
     * Gets all the string keys from JSON object
     */
    public Bundle getStringParams() {
        Bundle result = new Bundle();
        Iterator<?> keys = json.keys();
        while(keys.hasNext()) {
            String key = (String)keys.next();
            try {
                String value = json.getString(key);
                if (value != null) {
                    result.putString(key, value);
                }
            } catch (JSONException e) {
            }
        }
        return result;
    }

    public String toString() {
        return json.toString();
    }

}
