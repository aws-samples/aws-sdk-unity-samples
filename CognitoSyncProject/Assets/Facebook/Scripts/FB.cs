using Facebook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FB : ScriptableObject
{
    public static InitDelegate OnInitComplete;
    public static HideUnityDelegate OnHideUnity;

    private static IFacebook facebook;
    private static string authResponse;
    private static bool isInitCalled = false;
    private static string appId;
    private static bool cookie;
    private static bool logging;
    private static bool status;
    private static bool xfbml;
    private static bool frictionlessRequests;

    static IFacebook FacebookImpl
    {
        get
        {
            if (facebook == null)
            {
                throw new NullReferenceException("Facebook object is not yet loaded.  Did you call FB.Init()?");
            }
            return facebook;
        }
    }

    public static string AppId
    {
        get
        {
            // appId might be different from FBSettings.AppId
            // if using the programmatic version of FB.Init()
            return appId;
        }
    }
    public static string UserId
    {
        get
        {
            return (facebook != null) ? facebook.UserId : "";
        }
    }
    public static string AccessToken
    {
        get
        {
            return (facebook != null) ? facebook.AccessToken : "";
        }
    }

    public static DateTime AccessTokenExpiresAt
    {
        get
        {
            return (facebook != null) ? facebook.AccessTokenExpiresAt : DateTime.MinValue;
        }
    }

    public static bool IsLoggedIn
    {
        get
        {
            return (facebook != null) && facebook.IsLoggedIn;
        }
    }

    public static bool IsInitialized
    {
        get
        {
            return (facebook != null) && facebook.IsInitialized;
        }
    }

    #region Init
    /**
     * This is the preferred way to call FB.Init().  It will take the facebook app id specified in your
     * "Facebook" => "Edit Settings" menu when it is called.
     *
     * onInitComplete - Delegate is called when FB.Init() finished initializing everything.
     *                  By passing in a delegate you can find out when you can safely call the other methods.
     */
    public static void Init(InitDelegate onInitComplete, HideUnityDelegate onHideUnity = null, string authResponse = null)
    {
        Init(
            onInitComplete,
            FBSettings.AppId,
            FBSettings.Cookie,
            FBSettings.Logging,
            FBSettings.Status,
            FBSettings.Xfbml,
            FBSettings.FrictionlessRequests,
            onHideUnity,
            authResponse);
    }

    /**
     * If you need a more programmatic way to set the facebook app id and other setting call this function.
     * Useful for a build pipeline that requires no human input.
     */
    public static void Init(
        InitDelegate onInitComplete,
        string appId,
        bool cookie = true,
        bool logging = true,
        bool status = true,
        bool xfbml = false,
        bool frictionlessRequests = true,
        HideUnityDelegate onHideUnity = null,
        string authResponse = null)
    {
        FB.appId = appId;
        FB.cookie = cookie;
        FB.logging = logging;
        FB.status = status;
        FB.xfbml = xfbml;
        FB.frictionlessRequests = frictionlessRequests;
        FB.authResponse = authResponse;
        FB.OnInitComplete = onInitComplete;
        FB.OnHideUnity = onHideUnity;

        if (!isInitCalled)
        {
            var versionInfo = FBBuildVersionAttribute.GetVersionAttributeOfType(typeof (IFacebook));

            if (versionInfo == null)
            {
                FbDebug.Warn("Cannot find Facebook SDK Version");
            }
            else
            {
                FbDebug.Info(String.Format("Using SDK {0}, Build {1}", versionInfo.SdkVersion, versionInfo.BuildVersion));
            }

#if UNITY_EDITOR
            FBComponentFactory.GetComponent<EditorFacebookLoader>();
#elif UNITY_WEBPLAYER
            FBComponentFactory.GetComponent<CanvasFacebookLoader>();
#elif UNITY_IOS
            FBComponentFactory.GetComponent<IOSFacebookLoader>();
#elif UNITY_ANDROID
            FBComponentFactory.GetComponent<AndroidFacebookLoader>();
#else
            throw new NotImplementedException("Facebook API does not yet support this platform");
#endif
            isInitCalled = true;
            return;
        }

        FbDebug.Warn("FB.Init() has already been called.  You only need to call this once and only once.");

        // Init again if possible just in case something bad actually happened.
        if (FacebookImpl != null)
        {
            OnDllLoaded();
        }
    }

    private static void OnDllLoaded()
    {
        var versionInfo = FBBuildVersionAttribute.GetVersionAttributeOfType(FacebookImpl.GetType());
        if (versionInfo != null)
        {
            FbDebug.Log(string.Format("Finished loading Facebook dll. Version {0} Build {1}", versionInfo.SdkVersion, versionInfo.BuildVersion));
        }
        FacebookImpl.Init(
            OnInitComplete,
            appId,
            cookie,
            logging,
            status,
            xfbml,
            FBSettings.ChannelUrl,
            authResponse,
            frictionlessRequests,
            OnHideUnity
        );
    }
    #endregion

    public static void Login(string scope = "", FacebookDelegate callback = null)
    {
        FacebookImpl.Login(scope, callback);
    }

    public static void Logout()
    {
        FacebookImpl.Logout();
    }

    public static void AppRequest(
            string message,
            OGActionType actionType,
            string objectId,
            string[] to,
            string data = "",
            string title = "",
            FacebookDelegate callback = null)
    {
        FacebookImpl.AppRequest(message, actionType, objectId, to, null, null, null, data, title, callback);
    }

    public static void AppRequest(
            string message,
            OGActionType actionType,
            string objectId,
            List<object> filters = null,
            string[] excludeIds = null,
            int? maxRecipients = null,
            string data = "",
            string title = "",
            FacebookDelegate callback = null)
    {
        FacebookImpl.AppRequest(message, actionType, objectId, null, filters, excludeIds, maxRecipients, data, title, callback);
    }

    public static void AppRequest(
            string message,
            string[] to = null,
            List<object> filters = null,
            string[] excludeIds = null,
            int? maxRecipients = null,
            string data = "",
            string title = "",
            FacebookDelegate callback = null)
    {
        FacebookImpl.AppRequest(message, null, null, to, filters, excludeIds, maxRecipients, data, title, callback);
    }

    public static void Feed(
            string toId = "",
            string link = "",
            string linkName = "",
            string linkCaption = "",
            string linkDescription = "",
            string picture = "",
            string mediaSource = "",
            string actionName = "",
            string actionLink = "",
            string reference = "",
            Dictionary<string, string[]> properties = null,
            FacebookDelegate callback = null)
    {
        FacebookImpl.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
    }

    public static void API(string query, HttpMethod method, FacebookDelegate callback = null, Dictionary<string, string> formData = null)
    {
        FacebookImpl.API(query, method, formData, callback);
    }

    public static void API(string query, HttpMethod method, FacebookDelegate callback, WWWForm formData)
    {
        FacebookImpl.API(query, method, formData, callback);
    }

    [Obsolete("use FB.ActivateApp()")]
    public static void PublishInstall(FacebookDelegate callback = null)
    {
        FacebookImpl.PublishInstall(AppId, callback);
    }

    public static void ActivateApp()
    {
        FacebookImpl.ActivateApp(AppId);
    }

    public static void GetDeepLink(FacebookDelegate callback)
    {
        FacebookImpl.GetDeepLink(callback);
    }

    public static void GameGroupCreate(
        string name,
        string description,
        string privacy = "CLOSED",
        FacebookDelegate callback = null)
    {
        FacebookImpl.GameGroupCreate(name, description, privacy, callback);
    }

    public static void GameGroupJoin(
        string id,
        FacebookDelegate callback = null)
    {
        FacebookImpl.GameGroupJoin(id, callback);
    }

    #region App Events
    public sealed class AppEvents
    {

        // If the player has set the limitEventUsage flag to YES, your app will continue
        // to send this data to Facebook, but Facebook will not use the data to serve
        // targeted ads. Facebook may continue to use the information for other purposes,
        // including frequency capping, conversion events, estimating the number of unique
        // users, security and fraud detection, and debugging.

        public static bool LimitEventUsage
        {
            get
            {
                return (facebook != null) && facebook.LimitEventUsage;
            }
            set
            {
                facebook.LimitEventUsage = value;
            }
        }

        public static void LogEvent(
            string logEvent,
            float? valueToSum = null,
            Dictionary<string, object> parameters = null)
        {
            FacebookImpl.AppEventsLogEvent(logEvent, valueToSum, parameters);
        }

        public static void LogPurchase(
            float logPurchase,
            string currency = "USD",
            Dictionary<string, object> parameters = null)
        {
            FacebookImpl.AppEventsLogPurchase(logPurchase, currency, parameters);
        }
    }
    #endregion

    #region Canvas-Only Implemented Methods
    public sealed class Canvas
    {
        public static void Pay(
            string product,
            string action = "purchaseitem",
            int quantity = 1,
            int? quantityMin = null,
            int? quantityMax = null,
            string requestId = null,
            string pricepointId = null,
            string testCurrency = null,
            FacebookDelegate callback = null)
        {
            FacebookImpl.Pay(product, action, quantity, quantityMin, quantityMax, requestId, pricepointId, testCurrency, callback);
        }

        public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
        {
            FBScreen.SetResolution(width, height, fullscreen, preferredRefreshRate, layoutParams);
        }

        public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
        {
            FBScreen.SetAspectRatio(width, height, layoutParams);
        }
    }
    #endregion

    #region Android-Only Implemented Methods
    public sealed class Android
    {
         public static string KeyHash
         {
             get
             {
                var androidFacebook = facebook as AndroidFacebook;
                return (androidFacebook != null) ? androidFacebook.KeyHash : "";
             }
         }
    }
    #endregion

    #region Facebook Loader Class
    public abstract class RemoteFacebookLoader : MonoBehaviour
    {
        public delegate void LoadedDllCallback(IFacebook fb);

        private const string facebookNamespace = "Facebook.";

        private const int maxRetryLoadCount = 3;
        private static int retryLoadCount = 0;

        public static IEnumerator LoadFacebookClass(string className, LoadedDllCallback callback)
        {
            var url = string.Format(IntegratedPluginCanvasLocation.DllUrl, className);
            var www = new WWW(url);
            FbDebug.Log("loading dll: " + url);
            yield return www;

            if (www.error != null)
            {
                FbDebug.Error(www.error);
                if (retryLoadCount < maxRetryLoadCount)
                {
                    ++retryLoadCount;
#if UNITY_WEBPLAYER
                    FBComponentFactory.AddComponent<CanvasFacebookLoader>();
#endif
                }
                www.Dispose();
                yield break;
            }

#if !UNITY_WINRT
#if UNITY_4_5 || UNITY_4_6 || UNITY_5_0
            var authTokenWww = new WWW(IntegratedPluginCanvasLocation.KeyUrl);
            yield return authTokenWww;
            if (authTokenWww.error != null)
            {
                FbDebug.Error("Cannot load from " + IntegratedPluginCanvasLocation.KeyUrl + ": " + authTokenWww.error);
                authTokenWww.Dispose();
                yield break;
            }
            var assembly = Security.LoadAndVerifyAssembly(www.bytes, authTokenWww.text);
#else
            var assembly = Security.LoadAndVerifyAssembly(www.bytes);
#endif
            if (assembly == null)
            {
                FbDebug.Error("Could not securely load assembly from " + url);
                www.Dispose();
                yield break;
            }

            var facebookClass = assembly.GetType(facebookNamespace + className);
            if (facebookClass == null)
            {
                FbDebug.Error(className + " not found in assembly!");
                www.Dispose();
                yield break;
            }

            // load the Facebook component into the gameobject
            // using the "as" cast so it'll null if it fails to cast, instead of exception
            var fb = typeof(FBComponentFactory)
                    .GetMethod("GetComponent")
                    .MakeGenericMethod(facebookClass)
                    .Invoke(null, new object[] { IfNotExist.AddNew }) as IFacebook;

            if (fb == null)
            {
                FbDebug.Error(className + " couldn't be created.");
                www.Dispose();
                yield break;
            }

            callback(fb);
#endif
            www.Dispose();
        }

        protected abstract string className { get; }

        IEnumerator Start()
        {
            var loader = LoadFacebookClass(className, OnDllLoaded);
            while (loader.MoveNext())
            {
                yield return loader.Current;
            }
            Destroy(this);
        }

        private void OnDllLoaded(IFacebook fb)
        {
            FB.facebook = fb;
            FB.OnDllLoaded();
        }
    }

    public abstract class CompiledFacebookLoader : MonoBehaviour
    {
        protected abstract IFacebook fb { get; }

        void Start()
        {
            FB.facebook = fb;
            FB.OnDllLoaded();
            Destroy(this);
        }
    }
    #endregion
}
