using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Facebook
{
    class EditorFacebook : AbstractFacebook, IFacebook
    {
        private AbstractFacebook fb;
        private FacebookDelegate loginCallback;

        public override int DialogMode
        {
            get { return 0; }
            set { ; }
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
            }
        }

        #region Init
        protected override void OnAwake()
        {
            // bootstrap the canvas facebook for native dialogs
            StartCoroutine(FB.RemoteFacebookLoader.LoadFacebookClass("CanvasFacebook", OnDllLoaded));
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
            StartCoroutine(OnInit(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate));
        }

        private IEnumerator OnInit(
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
            // wait until the native dialogs are loaded
            while (fb == null)
            {
                yield return null;
            }
            fb.Init(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate);

            this.isInitialized = true;
            if (onInitComplete != null)
            {
                onInitComplete();
            }
        }

        private void OnDllLoaded(IFacebook fb)
        {
            this.fb = (AbstractFacebook) fb;
        }
        #endregion

        public override void Login(string scope = "", FacebookDelegate callback = null)
        {
            AddAuthDelegate(callback);
            FBComponentFactory.GetComponent<EditorFacebookAccessToken>();
        }

        public override void Logout()
        {
            isLoggedIn = false;
            userId = "";
            accessToken = "";
            fb.UserId = "";
            fb.AccessToken = "";
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
            fb.AppRequest(message, actionType, objectId, to, filters, excludeIds, maxRecipients, data, title, callback);
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
            fb.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
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
            FbDebug.Info("Pay method only works with Facebook Canvas.  Does nothing in the Unity Editor, iOS or Android");
        }

        public override void GameGroupCreate(
            string name,
            string description,
            string privacy = "CLOSED",
            FacebookDelegate callback = null)
        {
            throw new PlatformNotSupportedException("There is no Facebook GameGroupCreate Dialog on Editor");
        }

        public override void GameGroupJoin(
            string id,
            FacebookDelegate callback = null)
        {
            throw new PlatformNotSupportedException("There is no Facebook GameGroupJoin Dialog on Editor");
        }

        public override void GetAuthResponse(FacebookDelegate callback = null)
        {
            fb.GetAuthResponse(callback);
        }

        public override void PublishInstall(string appId, FacebookDelegate callback = null) {}
        public override void ActivateApp(string appId = null)
        {
            FbDebug.Info("This only needs to be called for iOS or Android.");
        }

        public override void GetDeepLink(FacebookDelegate callback)
        {
            FbDebug.Info("No Deep Linking in the Editor");
            if (callback != null)
            {
                callback(new FBResult("<platform dependent>"));
            }
        }

        public override void AppEventsLogEvent(
            string logEvent,
            float? valueToSum = null,
            Dictionary<string, object> parameters = null)
        {
            FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
        }

        public override void AppEventsLogPurchase(
            float logPurchase,
            string currency = "USD",
            Dictionary<string, object> parameters = null)
        {
            FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
        }

        #region Editor Mock Login

        public void MockLoginCallback(FBResult result)
        {
            Destroy(FBComponentFactory.GetComponent<EditorFacebookAccessToken>());
            if (result.Error != null)
            {
                BadAccessToken(result.Error);
                return;
            }

            try
            {
                var json = (List<object>) MiniJSON.Json.Deserialize(result.Text);
                var responses = new List<string>();
                foreach (object obj in json)
                {
                    if (!(obj is Dictionary<string, object>))
                    {
                        continue;
                    }

                    var response = (Dictionary<string, object>) obj;

                    if (!response.ContainsKey("body"))
                    {
                        continue;
                    }

                    responses.Add((string) response["body"]);
                }

                var userData = (Dictionary<string, object>) MiniJSON.Json.Deserialize(responses[0]);
                var appData = (Dictionary<string, object>) MiniJSON.Json.Deserialize(responses[1]);

                if (FB.AppId != (string) appData["id"])
                {
                    BadAccessToken("Access token is not for current app id: " + FB.AppId);
                    return;
                }

                userId = (string)userData["id"];
                fb.UserId = userId;
                fb.AccessToken = accessToken;
                isLoggedIn = true;

                OnAuthResponse(new FBResult(""));

            }
            catch (Exception e)
            {
                BadAccessToken("Could not get data from access token: " + e.Message);
            }
        }

        public void MockCancelledLoginCallback()
        {
            OnAuthResponse(new FBResult(""));
        }

        private void BadAccessToken(string error)
        {
            FbDebug.Error(error);
            userId = "";
            fb.UserId = "";
            accessToken = "";
            fb.AccessToken = "";
            FBComponentFactory.GetComponent<EditorFacebookAccessToken>();
        }

        #endregion
    }
}
