package com.facebook.unity;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.util.Log;

import com.facebook.Request;
import com.facebook.Response;
import com.facebook.Session;
import com.facebook.SessionDefaultAudience;
import com.facebook.SessionState;
import com.facebook.Session.Builder;
import com.facebook.Session.OpenRequest;
import com.facebook.Session.StatusCallback;
import com.facebook.model.GraphUser;

public class FBLogin {
    public static void init(String appID) {
        Session session;
        if (FB.isLoggedIn()) {
            session = Session.getActiveSession();
            // this shouldn't be an issue for most people: the app id in the session not matching the one provided
            // instead it can probably happen if a developer wants to switch app ids at run time.
            if (appID != session.getApplicationId()) {
                Log.w(FB.TAG, "App Id in active session ("+ session.getApplicationId() +") doesn't match App Id passed in: " + appID);
                session = new Builder(FB.getUnityActivity()).setApplicationId(appID).build();
            }
        } else {
            session = new Builder(FB.getUnityActivity()).setApplicationId(appID).build();
        }
        Session.setActiveSession(session);

        final UnityMessage unityMessage = new UnityMessage("OnInitComplete");
        unityMessage.put("key_hash", FB.getKeyHash());

        // if there is an existing session, reopen it
        if (SessionState.CREATED_TOKEN_LOADED.equals(session.getState())) {
            Session.StatusCallback finalCallback = getFinalCallback(unityMessage, null);
            sessionOpenRequest(session, finalCallback, FB.getUnityActivity(), null, false);
        } else {
            unityMessage.send();
        }
    }

    public static void login(String params, final Activity activity) {
        Session session = Session.getActiveSession();
        if (session == null) {
            Log.w(FB.TAG, "Session not found. Call init() before calling login()");
            return;
        }
        // if the old session is closed (or login was cancelled), create new one
        if (session.isClosed()) {
            session = new Builder(FB.getUnityActivity()).setApplicationId(session.getApplicationId()).build();
            Session.setActiveSession(session);
        }
        final UnityMessage unityMessage = new UnityMessage("OnLoginComplete");

        unityMessage.put("key_hash", FB.getKeyHash());

        // parse and separate the permissions into read and publish permissions
        List<String> permissions = new ArrayList<String>();
        UnityParams unity_params = UnityParams.parse(params, "couldn't parse login params: " + params);
        if (unity_params.hasString("scope")) {
            permissions = new ArrayList<String>(Arrays.asList(unity_params.getString("scope").split(",")));
        }
        List<String> publishPermissions = new ArrayList<String>();
        List<String> readPermissions = new ArrayList<String>();
        if(permissions.size() > 0) {
            for(String s:permissions) {
                if(s.length() == 0) {
                    continue;
                }
                if(Session.isPublishPermission(s)) {
                    publishPermissions.add(s);
                } else {
                    readPermissions.add((s));
                }
            }
        }
        boolean hasPublishPermissions = !publishPermissions.isEmpty();

        // check to see if the readPermissions have been TOSed already
        // we don't need to show the readPermissions dialog if they have all been TOSed even though it's a mix
        // of permissions
        boolean showMixedPermissionsFlow = hasPublishPermissions && !session.getPermissions().containsAll(readPermissions);

        // if we're logging in and showing a mix of publish and read permission, we need to split up the dialogs
        // first just show the read permissions, after they are accepted show publish permissions
        if (showMixedPermissionsFlow) {
            Session.StatusCallback afterReadPermissionCallback = getAfterReadPermissionLoginCallback(unityMessage, publishPermissions, activity);
            sessionOpenRequest(session, afterReadPermissionCallback, activity, readPermissions, false);
        } else {
            Session.StatusCallback finalCallback = getFinalCallback(unityMessage, activity);
            sessionOpenRequest(session, finalCallback, activity, permissions, hasPublishPermissions);
        }
    }

    static void sessionOpenRequest(Session session, Session.StatusCallback callback, Activity activity, List<String> permissions, boolean publish) {
        if (session.isOpened()) {
            Session.NewPermissionsRequest req = getNewPermissionsRequest(session, callback, permissions, activity);
            if (publish) {
                session.requestNewPublishPermissions(req);
            } else {
                session.requestNewReadPermissions(req);
            }
        } else {
            OpenRequest req = getOpenRequest(callback, permissions, activity);
            if (publish) {
                session.openForPublish(req);
            } else {
                session.openForRead(req);
            }
        }
    }

    private static Session.StatusCallback getAfterReadPermissionLoginCallback(final UnityMessage unityMessage, final List<String> publishPermissions, final Activity activity) {
        return new Session.StatusCallback() {
            // callback when session changes state
            @Override
            public void call(Session session, SessionState state, Exception exception) {
                if (session.getState().equals(SessionState.OPENING)){
                    return;
                }
                session.removeCallback(this);

                if (!session.isOpened() && state != SessionState.CLOSED_LOGIN_FAILED) {
                    unityMessage.sendError("Unknown error while opening session. Check logcat for details.");
                    activity.finish();
                    return;
                }

                // if someone cancels on the read permissions and we don't even have the most basic access_token
                // for basic info, we shouldn't be asking for publish permissions.  It doesn't make sense
                // and it simply won't work anyways.
                if (session.getAccessToken() == null || session.getAccessToken().equals("")) {
                    unityMessage.putCancelled();
                    unityMessage.send();
                    activity.finish();
                    return;
                }

                //ask for publish permissions, if necessary.
                if(session.getPermissions().containsAll(publishPermissions)) {
                    finalizeLogin(session, state, exception, unityMessage, activity);
                } else {
                    Session.StatusCallback finalCallback = getFinalCallback(unityMessage, activity);
                    sessionOpenRequest(session, finalCallback, activity, publishPermissions, true);
                }
            }
        };
    }

    private static Session.StatusCallback getFinalCallback(final UnityMessage unityMessage, final Activity activityToClose) {
        return new Session.StatusCallback() {
            // callback when session changes state
            @Override
            public void call(Session session, SessionState state, Exception exception) {
                if (session.getState().equals(SessionState.OPENING)){
                    return;
                }
                session.removeCallback(this);

                finalizeLogin(session, state, exception, unityMessage, activityToClose);
            }
        };
    }

    private static void finalizeLogin(Session session, SessionState state, Exception exception, final UnityMessage unityMessage, final Activity activityToClose) {
        if (activityToClose != null) {
            activityToClose.finish();
        }

        if (!session.isOpened() && state != SessionState.CLOSED_LOGIN_FAILED) {
            unityMessage.sendError("Unknown error while opening session. Check logcat for details.");
            return;
        }

        if (session.isOpened()) {
            unityMessage.put("opened", true);
        } else if (state == SessionState.CLOSED_LOGIN_FAILED) {
            unityMessage.putCancelled();
        }

        if (session.getAccessToken() == null || session.getAccessToken().equals("")) {
            unityMessage.send();
            return;
        }

        // there's a chance a subset of the permissions were allowed even if the login was cancelled
        // if the access token is there, try to get it anyways

        // add a callback to update the access token when it changes
        session.addCallback(new StatusCallback(){
            @Override
            public void call(Session session,
               SessionState state, Exception exception) {
                if (session == null || session.getAccessToken() == null) {
                    return;
                }
                final UnityMessage unityMessage = new UnityMessage("OnAccessTokenUpdate");
                unityMessage.put("access_token", session.getAccessToken());
                unityMessage.put("expiration_timestamp", "" + session.getExpirationDate().getTime() / 1000);
                unityMessage.send();
            }
        });
        unityMessage.put("access_token", session.getAccessToken());
        unityMessage.put("expiration_timestamp", "" + session.getExpirationDate().getTime() / 1000);
        Request.newMeRequest(session, new Request.GraphUserCallback() {
            @Override
            public void onCompleted(GraphUser user, Response response) {
                if (user != null) {
                    unityMessage.put("user_id", user.getId());
                }
                unityMessage.send();
            }
        }).executeAsync();
    }

    private static OpenRequest getOpenRequest(StatusCallback callback, List<String> permissions, Activity activity) {
        OpenRequest req = new OpenRequest(activity);
        req.setCallback(callback);
        req.setPermissions(permissions);
        req.setDefaultAudience(SessionDefaultAudience.FRIENDS);

        return req;
    }

    private static Session.NewPermissionsRequest getNewPermissionsRequest(Session session, StatusCallback callback, List<String> permissions, Activity activity) {
        Session.NewPermissionsRequest req = new Session.NewPermissionsRequest(activity, permissions);
        req.setCallback(callback);
        // This should really be "req.setCallback(callback);"
        // Unfortunately the current underlying SDK won't add the callback when you do it that way
        // TODO: when upgrading to the latest see if this can be "req.setCallback(callback);"
        // if it still doesn't have it, file a bug!
        session.addCallback(callback);
        req.setDefaultAudience(SessionDefaultAudience.FRIENDS);
        return req;
    }

    public static void onActivityResult(Activity activity, int requestCode, int resultCode, Intent data) {
        Session.getActiveSession().onActivityResult(activity, requestCode, resultCode, data);
    }

}
