using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;

namespace UnityEditor.FacebookEditor
{
    public class ManifestMod
    {
        public const string DeepLinkingActivityName = "com.facebook.unity.FBUnityDeepLinkingActivity";

        public const string LoginActivityName = "com.facebook.LoginActivity";

        public const string UnityLoginActivityName = "com.facebook.unity.FBUnityLoginActivity";

        public const string ApplicationIdMetaDataName = "com.facebook.sdk.ApplicationId";

        public static void GenerateManifest()
        {
            var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");

            // only copy over a fresh copy of the AndroidManifest if one does not exist
            if (!File.Exists(outputFile))
            {
                var inputFile = Path.Combine(EditorApplication.applicationContentsPath, "PlaybackEngines/androidplayer/AndroidManifest.xml");
                File.Copy(inputFile, outputFile);
            }
            UpdateManifest(outputFile);
        }

        public static bool CheckManifest()
        {
            bool result = true;
            var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
            if (!File.Exists(outputFile))
            {
                Debug.LogError("An android manifest must be generated for the Facebook SDK to work.  Go to Facebook->Edit Settings and press \"Regenerate Android Manifest\"");
                return false;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(outputFile);

            if (doc == null)
            {
                Debug.LogError("Couldn't load " + outputFile);
                return false;
            }

            XmlNode manNode = FindChildNode(doc, "manifest");
            XmlNode dict = FindChildNode(manNode, "application");

            if (dict == null)
            {
                Debug.LogError("Error parsing " + outputFile);
                return false;
            }

            string ns = dict.GetNamespaceOfPrefix("android");

            XmlElement loginElement = FindElementWithAndroidName("activity", "name", ns, UnityLoginActivityName, dict);
            if (loginElement == null)
            {
                Debug.LogError(string.Format("{0} is missing from your android manifest.  Go to Facebook->Edit Settings and press \"Regenerate Android Manifest\"", LoginActivityName));
                result = false;
            }

            var deprecatedMainActivityName = "com.facebook.unity.FBUnityPlayerActivity";
            XmlElement deprecatedElement = FindElementWithAndroidName("activity", "name", ns, deprecatedMainActivityName, dict);
            if (deprecatedElement != null)
            {
                Debug.LogWarning(string.Format("{0} is deprecated and no longer needed for the Facebook SDK.  Feel free to use your own main activity or use the default \"com.unity3d.player.UnityPlayerNativeActivity\"", deprecatedMainActivityName));
            }

            return result;
        }

        private static XmlNode FindChildNode(XmlNode parent, string name)
        {
            XmlNode curr = parent.FirstChild;
            while (curr != null)
            {
                if (curr.Name.Equals(name))
                {
                    return curr;
                }
                curr = curr.NextSibling;
            }
            return null;
        }

        private static XmlElement FindElementWithAndroidName(string name, string androidName, string ns, string value, XmlNode parent)
        {
            var curr = parent.FirstChild;
            while (curr != null)
            {
                if (curr.Name.Equals(name) && curr is XmlElement && ((XmlElement)curr).GetAttribute(androidName, ns) == value)
                {
                    return curr as XmlElement;
                }
                curr = curr.NextSibling;
            }
            return null;
        }

        public static void UpdateManifest(string fullPath)
        {
            string appId = FBSettings.AppId;

            if (!FBSettings.IsValidAppId)
            {
                Debug.LogError("You didn't specify a Facebook app ID.  Please add one using the Facebook menu in the main Unity editor.");
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fullPath);

            if (doc == null)
            {
                Debug.LogError("Couldn't load " + fullPath);
                return;
            }

            XmlNode manNode = FindChildNode(doc, "manifest");
            XmlNode dict = FindChildNode(manNode, "application");

            if (dict == null)
            {
                Debug.LogError("Error parsing " + fullPath);
                return;
            }

            string ns = dict.GetNamespaceOfPrefix("android");

            //add the unity login activity
            XmlElement unityLoginElement = FindElementWithAndroidName("activity", "name", ns, UnityLoginActivityName, dict);
            if (unityLoginElement == null)
            {
                unityLoginElement = CreateUnityLoginElement(doc, ns);
                dict.AppendChild(unityLoginElement);
            }


            //add the login activity
            XmlElement loginElement = FindElementWithAndroidName("activity", "name", ns, LoginActivityName, dict);
            if (loginElement == null)
            {
                loginElement = CreateLoginElement(doc, ns);
                dict.AppendChild(loginElement);
            }

            //add deep linking activity
            XmlElement deepLinkingElement = FindElementWithAndroidName("activity", "name", ns, DeepLinkingActivityName, dict);
            if (deepLinkingElement == null)
            {
                deepLinkingElement = CreateDeepLinkingElement(doc, ns);
                dict.AppendChild(deepLinkingElement);
            }

            //add the app id
            //<meta-data android:name="com.facebook.sdk.ApplicationId" android:value="\ 409682555812308" />
            XmlElement appIdElement = FindElementWithAndroidName("meta-data", "name", ns, ApplicationIdMetaDataName, dict);
            if (appIdElement == null)
            {
                appIdElement = doc.CreateElement("meta-data");
                appIdElement.SetAttribute("name", ns, ApplicationIdMetaDataName);
                dict.AppendChild(appIdElement);
            }
            appIdElement.SetAttribute("value", ns, "\\ " + appId); //stupid hack so that the id comes out as a string

            doc.Save(fullPath);
        }

        private static XmlElement CreateLoginElement(XmlDocument doc, string ns)
        {
            //<activity android:name="com.facebook.LoginActivity" android:configChanges="keyboardHidden|orientation" android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen">
            //</activity>
            XmlElement activityElement = doc.CreateElement("activity");
            activityElement.SetAttribute("name", ns, LoginActivityName);
            activityElement.SetAttribute("configChanges", ns, "keyboardHidden|orientation");
            activityElement.SetAttribute("theme", ns, "@android:style/Theme.Translucent.NoTitleBar.Fullscreen");
            activityElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
            return activityElement;
        }

        private static XmlElement CreateDeepLinkingElement(XmlDocument doc, string ns)
        {
            //<activity android:name="com.facebook.unity.FBDeepLinkingActivity" android:exported="true">
            //</activity>
            XmlElement activityElement = doc.CreateElement("activity");
            activityElement.SetAttribute("name", ns, DeepLinkingActivityName);
            activityElement.SetAttribute("exported", ns, "true");
            activityElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
            return activityElement;
        }

        private static XmlElement CreateUnityLoginElement(XmlDocument doc, string ns)
        {
            //<activity android:name="com.facebook.unity.FBUnityLoginActivity" android:configChanges="all|of|them" android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen">
            //</activity>
            XmlElement activityElement = doc.CreateElement("activity");
            activityElement.SetAttribute("name", ns, UnityLoginActivityName);
            activityElement.SetAttribute("configChanges", ns, "fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen");
            activityElement.SetAttribute("theme", ns, "@android:style/Theme.Translucent.NoTitleBar.Fullscreen");
            activityElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
            return activityElement;
        }
    }
}