using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif
public class FBSettings : ScriptableObject
{

    const string facebookSettingsAssetName = "FacebookSettings";
    const string facebookSettingsPath = "Facebook/Resources";
    const string facebookSettingsAssetExtension = ".asset";

    private static FBSettings instance;

    static FBSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load(facebookSettingsAssetName) as FBSettings;
                if (instance == null)
                {
                    // If not found, autocreate the asset object.
                    instance = CreateInstance<FBSettings>();
#if UNITY_EDITOR
                    string properPath = Path.Combine(Application.dataPath, facebookSettingsPath);
                    if (!Directory.Exists(properPath))
                    {
                        AssetDatabase.CreateFolder("Assets/Facebook", "Resources");
                    }

                    string fullPath = Path.Combine(Path.Combine("Assets", facebookSettingsPath),
                                                   facebookSettingsAssetName + facebookSettingsAssetExtension
                                                  );
                    AssetDatabase.CreateAsset(instance, fullPath);
#endif
                }
            }
            return instance;
        }
    }

#if UNITY_EDITOR
    [MenuItem("Facebook/Edit Settings")]
    public static void Edit()
    {
        Selection.activeObject = Instance;
    }

    [MenuItem("Facebook/Developers Page")]
    public static void OpenAppPage()
    {
        string url = "https://developers.facebook.com/apps/";
        if (Instance.AppIds[Instance.SelectedAppIndex] != "0")
            url += Instance.AppIds[Instance.SelectedAppIndex];
        Application.OpenURL(url);
    }

    [MenuItem("Facebook/SDK Documentation")]
    public static void OpenDocumentation()
    {
        string url = "https://developers.facebook.com/games/unity/";
        Application.OpenURL(url);
    }

    [MenuItem("Facebook/SDK Facebook Group")]
    public static void OpenFacebookGroup()
    {
        string url = "https://www.facebook.com/groups/491736600899443/";
        Application.OpenURL(url);
    }

    [MenuItem("Facebook/Report a SDK Bug")]
    public static void ReportABug()
    {
        string url = "https://developers.facebook.com/bugs/create";
        Application.OpenURL(url);
    }
#endif

    #region App Settings

    [SerializeField]
    private int selectedAppIndex = 0;
    [SerializeField]
    private string[] appIds = new[] { "0" };
    [SerializeField]
    private string[] appLabels = new[] { "App Name" };
    [SerializeField]
    private bool cookie = true;
    [SerializeField]
    private bool logging = true;
    [SerializeField]
    private bool status = true;
    [SerializeField]
    private bool xfbml = false;
    [SerializeField]
    private bool frictionlessRequests = true;
    [SerializeField]
    private string iosURLSuffix = "";

    public void SetAppIndex(int index)
    {
        if (selectedAppIndex != index)
        {
            selectedAppIndex = index;
            DirtyEditor();
        }
    }

    public int SelectedAppIndex
    {
        get { return selectedAppIndex; }
    }

    public void SetAppId(int index, string value)
    {
        if (appIds[index] != value)
        {
            appIds[index] = value;
            DirtyEditor();
        }
    }

    public string[] AppIds
    {
        get { return appIds; }
        set
        {
            if (appIds != value)
            {
                appIds = value;
                DirtyEditor();
            }
        }
    }

    public void SetAppLabel(int index, string value)
    {
        if (appLabels[index] != value)
        {
            AppLabels[index] = value;
            DirtyEditor();
        }
    }

    public string[] AppLabels
    {
        get { return appLabels; }
        set
        {
            if (appLabels != value)
            {
                appLabels = value;
                DirtyEditor();
            }
        }
    }

    public static string[] AllAppIds
    {
        get
        {
            return Instance.AppIds;
        }
    }

    public static string AppId
    {
        get
        {
            return Instance.AppIds[Instance.SelectedAppIndex];
        }
    }

    public static bool IsValidAppId
    {
        get
        {
            return FBSettings.AppId != null 
                && FBSettings.AppId.Length > 0 
                && !FBSettings.AppId.Equals("0");
        }
    }

    public static bool Cookie
    {
        get { return Instance.cookie; }
        set
        {
            if (Instance.cookie != value)
            {
                Instance.cookie = value;
                DirtyEditor();
            }
        }
    }

    public static bool Logging
    {
        get { return Instance.logging; }
        set
        {
            if (Instance.logging != value)
            {
                Instance.logging = value;
                DirtyEditor();
            }
        }
    }

    public static bool Status
    {
        get { return Instance.status; }
        set
        {
            if (Instance.status != value)
            {
                Instance.status = value;
                DirtyEditor();
            }
        }
    }

    public static bool Xfbml
    {
        get { return Instance.xfbml; }
        set
        {
            if (Instance.xfbml != value)
            {
                Instance.xfbml = value;
                DirtyEditor();
            }
        }
    }

    public static string IosURLSuffix
    {
        get { return Instance.iosURLSuffix; }
        set 
        {
            if (Instance.iosURLSuffix != value)
            {
                Instance.iosURLSuffix = value;
                DirtyEditor ();
            }
        }
    }

    public static string ChannelUrl
    {
        get { return "/channel.html"; }
    }

    public static bool FrictionlessRequests
    {
        get { return Instance.frictionlessRequests; }
        set
        {
            if (Instance.frictionlessRequests != value)
            {
                Instance.frictionlessRequests = value;
                DirtyEditor();
            }
        }
    }

#if UNITY_EDITOR

    private string testFacebookId = "";
    private string testAccessToken = "";

    public static string TestFacebookId
    {
        get { return Instance.testFacebookId; }
        set
        {
            if (Instance.testFacebookId != value)
            {
                Instance.testFacebookId = value;
                DirtyEditor();
            }
        }
    }

    public static string TestAccessToken
    {
        get { return Instance.testAccessToken; }
        set
        {
            if (Instance.testAccessToken != value)
            {
                Instance.testAccessToken = value;
                DirtyEditor();
            }
        }
    }
#endif

    private static void DirtyEditor()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(Instance);
#endif
    }

    #endregion
}
