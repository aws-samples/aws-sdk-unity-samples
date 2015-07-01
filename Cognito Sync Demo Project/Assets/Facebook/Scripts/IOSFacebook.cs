using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Facebook
{
    class IOSFacebook : AbstractFacebook, IFacebook
    {
        private const string CancelledResponse = "{\"cancelled\":true}";
#if UNITY_IOS
        [DllImport ("__Internal")] private static extern void iosInit(string appId, bool cookie, bool logging, bool status, bool frictionlessRequests, string urlSuffix);
        [DllImport ("__Internal")] private static extern void iosLogin(string scope);
        [DllImport ("__Internal")] private static extern void iosLogout();

        [DllImport ("__Internal")] private static extern void iosSetShareDialogMode(int mode);

        [DllImport ("__Internal")]
        private static extern void iosFeedRequest(
            int requestId,
            string toId,
            string link,
            string linkName,
            string linkCaption,
            string linkDescription,
            string picture,
            string mediaSource,
            string actionName,
            string actionLink,
            string reference);

        [DllImport ("__Internal")]
        private static extern void iosAppRequest(
            int requestId,
            string message,
            string actionType,
            string objectId,
            string[] to = null,
            int toLength = 0,
            string filters = "",
            string[] excludeIds = null,
            int excludeIdsLength = 0,
            bool hasMaxRecipients = false,
            int maxRecipients = 0,
            string data = "",
            string title = "");

        [DllImport ("__Internal")]
        private static extern void iosCreateGameGroup(
            int requestId,
            string name,
            string description,
            string privacy);

        [DllImport ("__Internal")]
        private static extern void iosJoinGameGroup(int requestId, string id);

        [DllImport ("__Internal")]
        private static extern void iosFBSettingsPublishInstall(int requestId, string appId);

        [DllImport ("__Internal")]
        private static extern void iosFBSettingsActivateApp(string appId);

        [DllImport ("__Internal")]
        private static extern void iosFBAppEventsLogEvent(
            string logEvent,
            double valueToSum,
            int numParams,
            string[] paramKeys,
            string[] paramVals);

        [DllImport ("__Internal")]
        private static extern void iosFBAppEventsLogPurchase(
            double logPurchase,
            string currency,
            int numParams,
            string[] paramKeys,
            string[] paramVals);

        [DllImport ("__Internal")]
        private static extern void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage);

        [DllImport ("__Internal")]
        private static extern void iosGetDeepLink();
#else
        void iosInit(string appId, bool cookie, bool logging, bool status, bool frictionlessRequests, string urlSuffix) { }
        void iosLogin(string scope) { }
        void iosLogout() { }

        void iosSetShareDialogMode(int mode) { }

        void iosFeedRequest(
            int requestId,
            string toId,
            string link,
            string linkName,
            string linkCaption,
            string linkDescription,
            string picture,
            string mediaSource,
            string actionName,
            string actionLink,
            string reference) { }

        void iosAppRequest(
            int requestId,
            string message,
            string actionType,
            string objectId,
            string[] to = null,
            int toLength = 0,
            string filters = "",
            string[] excludeIds = null,
            int excludeIdsLength = 0,
            bool hasMaxRecipients = false,
            int maxRecipients = 0,
            string data = "",
            string title = "") { }

        void iosCreateGameGroup(
            int requestId,
            string name,
            string description,
            string privacy) { }

        void iosJoinGameGroup(int requestId, string id) {}

        void iosFBSettingsPublishInstall(int requestId, string appId) { }

        void iosFBSettingsActivateApp(string appId) { }

        void iosFBAppEventsLogEvent(
            string logEvent,
            double valueToSum,
            int numParams,
            string[] paramKeys,
            string[] paramVals) { }

        void iosFBAppEventsLogPurchase(
            double logPurchase,
            string currency,
            int numParams,
            string[] paramKeys,
            string[] paramVals) { }

        void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage) { }

        void iosGetDeepLink() { }
#endif

        private class NativeDict
        {
            public NativeDict()
            {
                numEntries = 0;
                keys = null;
                vals = null;
            }

            public int numEntries;
            public string[] keys;
            public string[] vals;
        };

        public enum FBInsightsFlushBehavior
        {
            FBInsightsFlushBehaviorAuto,
            FBInsightsFlushBehaviorExplicitOnly,
        };

        private int dialogMode = (int)NativeDialogModes.eModes.FAST_APP_SWITCH_SHARE_DIALOG;

        private InitDelegate externalInitDelegate;

        public override int DialogMode
        {
            get { return dialogMode; }
            set { dialogMode = value; iosSetShareDialogMode(dialogMode); }
        }

        public override bool LimitEventUsage
        {
            get
            {
                return limitEventUsage;
            }
            set
            {
                limitEventUsage = value;
                iosFBAppEventsSetLimitEventUsage(value);
            }
        }

        private FacebookDelegate deepLinkDelegate;

        #region Init
        protected override void OnAwake()
        {
            accessToken = "NOT_USED_ON_IOS_FACEBOOK";
        }

        public override void Init(
            InitDelegate onInitComplete,
            string appId,
            bool cookie = false,
            bool logging = true,
            bool status = true,
            bool xfbml = false,
            string channelUrl = "",
            string authResponse = null,
            bool frictionlessRequests = false,
            Facebook.HideUnityDelegate hideUnityDelegate = null)
        {
            iosInit(appId, cookie, logging, status, frictionlessRequests, FBSettings.IosURLSuffix);
            externalInitDelegate = onInitComplete;
        }
        #endregion

        #region FB public interface
        public override void Login(string scope = "", FacebookDelegate callback = null)
        {
            AddAuthDelegate(callback);
            iosLogin(scope);
        }

        public override void Logout()
        {
            iosLogout();
            isLoggedIn = false;
        }

        public override void AppRequest(
            string message,
            OGActionType actionType,
            string objectId,
            string[] to = null,
            List<object> filters = null,
            string[] excludeIds = null,
            int? maxRecipients = null,
            string data = "",
            string title = "",
            FacebookDelegate callback = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message", "message cannot be null or empty!");
            }

            if (actionType != null && string.IsNullOrEmpty(objectId))
            {
                throw new ArgumentNullException("objectId", "You cannot provide an actionType without an objectId");
            }

            if (actionType == null && !string.IsNullOrEmpty(objectId))
            {
                throw new ArgumentNullException("actionType", "You cannot provide an objectId without an actionType");
            }

            string mobileFilter = null;
            if(filters != null && filters.Count > 0) {
                mobileFilter = filters[0] as string;
            }

            iosAppRequest(
                Convert.ToInt32(AddFacebookDelegate(callback)),
                message,
                (actionType != null) ? actionType.ToString() : null,
                objectId,
                to,
                to != null ? to.Length : 0,
                mobileFilter != null ? mobileFilter : "",
                excludeIds,
                excludeIds != null ? excludeIds.Length : 0,
                maxRecipients.HasValue,
                maxRecipients.HasValue ? maxRecipients.Value : 0,
                data,
                title);
        }

        public override void FeedRequest(
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
            iosFeedRequest(System.Convert.ToInt32(AddFacebookDelegate(callback)), toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference);
        }

        public override void Pay(
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
            throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on iOS");
        }

        public override void GameGroupCreate(
            string name,
            string description,
            string privacy = "CLOSED",
            FacebookDelegate callback = null)
        {
            iosCreateGameGroup(System.Convert.ToInt32(AddFacebookDelegate(callback)), name, description, privacy);
        }

        public override void GameGroupJoin(
            string id,
            FacebookDelegate callback = null)
        {
            iosJoinGameGroup(System.Convert.ToInt32(AddFacebookDelegate(callback)), id);
        }

        public override void GetDeepLink(FacebookDelegate callback)
        {
            if (callback == null)
            {
                return;
            }
            deepLinkDelegate = callback;
            iosGetDeepLink();
        }

        public void OnGetDeepLinkComplete(string message)
        {
            var rawResult = (Dictionary<string, object>)MiniJSON.Json.Deserialize(message);
            if (deepLinkDelegate == null)
            {
                return;
            }
            object deepLink = "";
            rawResult.TryGetValue("deep_link", out deepLink);
            deepLinkDelegate(new FBResult(deepLink.ToString()));
        }

        public override void AppEventsLogEvent(
            string logEvent,
            float? valueToSum = null,
            Dictionary<string, object> parameters = null)
        {
            NativeDict dict = MarshallDict(parameters);
            if (valueToSum.HasValue)
            {
                iosFBAppEventsLogEvent(logEvent, valueToSum.Value, dict.numEntries, dict.keys, dict.vals);
            }
            else
            {
                iosFBAppEventsLogEvent(logEvent, 0.0, dict.numEntries, dict.keys, dict.vals);
            }
        }

        public override void AppEventsLogPurchase(
            float logPurchase,
            string currency = "USD",
            Dictionary<string, object> parameters = null)
        {
            NativeDict dict = MarshallDict(parameters);
            if (string.IsNullOrEmpty(currency))
            {
                currency = "USD";
            }
            iosFBAppEventsLogPurchase(logPurchase, currency, dict.numEntries, dict.keys, dict.vals);
        }

        public override void PublishInstall(string appId, FacebookDelegate callback = null)
        {
            iosFBSettingsPublishInstall(System.Convert.ToInt32(AddFacebookDelegate(callback)), appId);
        }

        public override void ActivateApp(string appId = null)
        {
            iosFBSettingsActivateApp(appId);
        }
        #endregion

        #region Interal stuff
        private NativeDict MarshallDict(Dictionary<string, object> dict)
        {
            NativeDict res = new NativeDict();

            if (dict != null && dict.Count > 0)
            {
                res.keys = new string[dict.Count];
                res.vals = new string[dict.Count];
                res.numEntries = 0;
                foreach (KeyValuePair<string, object> kvp in dict)
                {
                    res.keys[res.numEntries] = kvp.Key;
                    res.vals[res.numEntries] = kvp.Value.ToString();
                    res.numEntries++;
                }
            }
            return res;
        }
        private NativeDict MarshallDict(Dictionary<string, string> dict)
        {
            NativeDict res = new NativeDict();

            if (dict != null && dict.Count > 0)
            {
                res.keys = new string[dict.Count];
                res.vals = new string[dict.Count];
                res.numEntries = 0;
                foreach (KeyValuePair<string, string> kvp in dict)
                {
                    res.keys[res.numEntries] = kvp.Key;
                    res.vals[res.numEntries] = kvp.Value;
                    res.numEntries++;
                }
            }
            return res;
        }

        private void OnInitComplete(string msg)
        {
            this.isInitialized = true;
            if (!string.IsNullOrEmpty(msg))
            {
                OnLogin(msg);
            }
            externalInitDelegate();
        }

        public void OnLogin(string msg)
        {
            // MiniJSON doesn't parse empty strings well it seems.
            if (string.IsNullOrEmpty(msg))
            {
                OnAuthResponse(new FBResult(CancelledResponse));
                return;
            }
            var parameters = (Dictionary<string, object>)MiniJSON.Json.Deserialize(msg);
            if (parameters.ContainsKey ("user_id"))
            {
                isLoggedIn = true;
            }

            //pull userId, access token and expiration time out of the response
            ParseLoginDict (parameters);

            OnAuthResponse(new FBResult(msg));
        }

        public void ParseLoginDict(Dictionary<string, object>parameters)
        {
            if (parameters.ContainsKey ("user_id"))
            {
                userId = (string)parameters ["user_id"];
            }

            if (parameters.ContainsKey ("access_token"))
            {
                accessToken = (string)parameters ["access_token"];
            }

            if(parameters.ContainsKey ("expiration_timestamp"))
            {
                accessTokenExpiresAt = FromTimestamp(int.Parse((string)parameters["expiration_timestamp"]));
            }
        }

        //TODO: move into AbstractFacebook
        public void OnAccessTokenRefresh(string message)
        {
            var parameters = (Dictionary<string, object>)MiniJSON.Json.Deserialize(message);
            ParseLoginDict (parameters);
			// make sure that login callback gets called
            OnAuthResponse(new FBResult(message));
        }

        //TODO: move into AbstractFacebook
        private DateTime FromTimestamp(int timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
        }

        public void OnLogout(string msg)
        {
            isLoggedIn = false;
        }

        public void OnRequestComplete(string msg)
        {
            int delimIdx = msg.IndexOf(":");
            if (delimIdx <= 0)
            {
                FbDebug.Error("Malformed callback from ios.  I expected the form id:message but couldn't find either the ':' character or the id.");
                FbDebug.Error("Here's the message that errored: " + msg);
                return;
            }

            string idStr = msg.Substring(0, delimIdx);
            string payload = msg.Substring(delimIdx + 1);

            FbDebug.Info("id:" + idStr + " msg:" + payload);

            OnFacebookResponse(idStr, new FBResult(payload));
        }
        #endregion
    }
}
