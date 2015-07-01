using Facebook;
using UnityEngine;
using UnityEditor;
using UnityEditor.FacebookEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(FBSettings))]
public class FacebookSettingsEditor : Editor
{
    bool showFacebookInitSettings = false;
    bool showAndroidUtils = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android);
	// Unity renamed build target from iPhone to iOS in Unity 5, this keeps both versions happy
    bool showIOSSettings = (EditorUserBuildSettings.activeBuildTarget.ToString() == "iPhone"
	                        || EditorUserBuildSettings.activeBuildTarget.ToString() == "iOS");

    GUIContent appNameLabel = new GUIContent("App Name [?]:", "For your own use and organization.\n(ex. 'dev', 'qa', 'prod')");
    GUIContent appIdLabel = new GUIContent("App Id [?]:", "Facebook App Ids can be found at https://developers.facebook.com/apps");

    GUIContent urlSuffixLabel = new GUIContent ("URL Scheme Suffix [?]", "Use this to share Facebook APP ID's across multiple iOS apps.  https://developers.facebook.com/docs/ios/share-appid-across-multiple-apps-ios-sdk/");
    
    GUIContent cookieLabel = new GUIContent("Cookie [?]", "Sets a cookie which your server-side code can use to validate a user's Facebook session");
    GUIContent loggingLabel = new GUIContent("Logging [?]", "(Web Player only) If true, outputs a verbose log to the Javascript console to facilitate debugging.");
    GUIContent statusLabel = new GUIContent("Status [?]", "If 'true', attempts to initialize the Facebook object with valid session data.");
    GUIContent xfbmlLabel = new GUIContent("Xfbml [?]", "(Web Player only If true) Facebook will immediately parse any XFBML elements on the Facebook Canvas page hosting the app");
    GUIContent frictionlessLabel = new GUIContent("Frictionless Requests [?]", "Use frictionless app requests, as described in their own documentation.");

    GUIContent packageNameLabel = new GUIContent("Package Name [?]", "aka: the bundle identifier");
    GUIContent classNameLabel = new GUIContent("Class Name [?]", "aka: the activity name");
    GUIContent debugAndroidKeyLabel = new GUIContent("Debug Android Key Hash [?]", "Copy this key to the Facebook Settings in order to test a Facebook Android app");

    GUIContent sdkVersion = new GUIContent("SDK Version [?]", "This Unity Facebook SDK version.  If you have problems or compliments please include this so we know exactly what version to look out for.");
    GUIContent buildVersion = new GUIContent("Build Version [?]", "This Unity Facebook SDK version.  If you have problems or compliments please include this so we know exactly what version to look out for.");

    private FBSettings instance;

    public override void OnInspectorGUI()
    {
        instance = (FBSettings)target;

        AppIdGUI();
        FBParamsInitGUI();
        AndroidUtilGUI();
        IOSUtilGUI();
        AboutGUI();
    }

    private void AppIdGUI()
    {
        EditorGUILayout.HelpBox("1) Add the Facebook App Id(s) associated with this game", MessageType.None);
        if (instance.AppIds.Length == 0 || instance.AppIds[instance.SelectedAppIndex] == "0")
        {
            EditorGUILayout.HelpBox("Invalid App Id", MessageType.Error);
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(appNameLabel);
        EditorGUILayout.LabelField(appIdLabel);
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < instance.AppIds.Length; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            instance.SetAppLabel(i, EditorGUILayout.TextField(instance.AppLabels[i]));
            GUI.changed = false;
            instance.SetAppId(i, EditorGUILayout.TextField(instance.AppIds[i]));
            if (GUI.changed)
                ManifestMod.GenerateManifest();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Another App Id"))
        {
            var appLabels = new List<string>(instance.AppLabels);
            appLabels.Add("New App");
            instance.AppLabels = appLabels.ToArray();

            var appIds = new List<string>(instance.AppIds);
            appIds.Add("0");
            instance.AppIds = appIds.ToArray();
        }
        if (instance.AppLabels.Length > 1)
        {
            if (GUILayout.Button("Remove Last App Id"))
            {
                var appLabels = new List<string>(instance.AppLabels);
                appLabels.RemoveAt(appLabels.Count - 1);
                instance.AppLabels = appLabels.ToArray();

                var appIds = new List<string>(instance.AppIds);
                appIds.RemoveAt(appIds.Count - 1);
                instance.AppIds = appIds.ToArray();
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        if (instance.AppIds.Length > 1)
        {
            EditorGUILayout.HelpBox("2) Select Facebook App Id to be compiled with this game", MessageType.None);
            GUI.changed = false;
            instance.SetAppIndex(EditorGUILayout.Popup("Selected App Id", instance.SelectedAppIndex, instance.AppLabels));
            if (GUI.changed)
                ManifestMod.GenerateManifest();
            EditorGUILayout.Space();
        }
        else
        {
            instance.SetAppIndex(0);
        }
    }

    private void FBParamsInitGUI()
    {
        EditorGUILayout.HelpBox("(Optional) Edit the FB.Init() parameters", MessageType.None);
        showFacebookInitSettings = EditorGUILayout.Foldout(showFacebookInitSettings, "FB.Init() Parameters");
        if (showFacebookInitSettings)
        {
            FBSettings.Cookie = EditorGUILayout.Toggle(cookieLabel, FBSettings.Cookie);
            FBSettings.Logging = EditorGUILayout.Toggle(loggingLabel, FBSettings.Logging);
            FBSettings.Status = EditorGUILayout.Toggle(statusLabel, FBSettings.Status);
            FBSettings.Xfbml = EditorGUILayout.Toggle(xfbmlLabel, FBSettings.Xfbml);
            FBSettings.FrictionlessRequests = EditorGUILayout.Toggle(frictionlessLabel, FBSettings.FrictionlessRequests);
        }
        EditorGUILayout.Space();
    }

    private void IOSUtilGUI()
    {
        showIOSSettings = EditorGUILayout.Foldout(showIOSSettings, "iOS Build Settings");
        if (showIOSSettings)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(urlSuffixLabel, GUILayout.Width(135), GUILayout.Height(16));
            FBSettings.IosURLSuffix = EditorGUILayout.TextField(FBSettings.IosURLSuffix);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
    }

    private void AndroidUtilGUI()
    {
        showAndroidUtils = EditorGUILayout.Foldout(showAndroidUtils, "Android Build Facebook Settings");
        if (showAndroidUtils)
        {
            if (!FacebookAndroidUtil.IsSetupProperly())
            {
                var msg = "Your Android setup is not right. Check the documentation.";
                switch (FacebookAndroidUtil.SetupError)
                {
                    case FacebookAndroidUtil.ERROR_NO_SDK:
                        msg = "You don't have the Android SDK setup!  Go to " + (Application.platform == RuntimePlatform.OSXEditor ? "Unity" : "Edit") + "->Preferences... and set your Android SDK Location under External Tools";
                        break;
                    case FacebookAndroidUtil.ERROR_NO_KEYSTORE:
                        msg = "Your android debug keystore file is missing! You can create new one by creating and building empty Android project in Ecplise.";
                        break;
                    case FacebookAndroidUtil.ERROR_NO_KEYTOOL:
                        msg = "Keytool not found. Make sure that Java is installed, and that Java tools are in your path.";
                        break;
                    case FacebookAndroidUtil.ERROR_NO_OPENSSL:
                        msg = "OpenSSL not found. Make sure that OpenSSL is installed, and that it is in your path.";
                        break;
                    case FacebookAndroidUtil.ERROR_KEYTOOL_ERROR:
                        msg = "Unkown error while getting Debug Android Key Hash.";
                        break;
                }
                EditorGUILayout.HelpBox(msg, MessageType.Warning);
            }
            EditorGUILayout.HelpBox("Copy and Paste these into your \"Native Android App\" Settings on developers.facebook.com/apps", MessageType.None);
            SelectableLabelField(packageNameLabel, PlayerSettings.bundleIdentifier);
            SelectableLabelField(classNameLabel, ManifestMod.DeepLinkingActivityName);
            SelectableLabelField(debugAndroidKeyLabel, FacebookAndroidUtil.DebugKeyHash);
            if (GUILayout.Button("Regenerate Android Manifest"))
            {
                ManifestMod.GenerateManifest();
            }
        }
        EditorGUILayout.Space();
    }

    private void AboutGUI()
    {
        var versionInfo = FBBuildVersionAttribute.GetVersionAttributeOfType(typeof(IFacebook));
        if (versionInfo == null)
        {
            EditorGUILayout.HelpBox("Cannot find version info on the Facebook SDK!", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.HelpBox("About the Facebook SDK", MessageType.None);
            SelectableLabelField(sdkVersion, versionInfo.SdkVersion);
            SelectableLabelField(buildVersion, versionInfo.BuildVersion);
        }
        EditorGUILayout.Space();
    }

    private void SelectableLabelField(GUIContent label, string value)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label, GUILayout.Width(180), GUILayout.Height(16));
        EditorGUILayout.SelectableLabel(value, GUILayout.Height(16));
        EditorGUILayout.EndHorizontal();
    }
}
