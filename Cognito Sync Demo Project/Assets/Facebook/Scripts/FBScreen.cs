using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/**
 * Basic wrapper around UnityEngine.Screen
 * Allows for window resizing within Facebook Canvas
 */
public class FBScreen {

    private static bool resizable = false;

    public static bool FullScreen {
        get { return Screen.fullScreen; }
        set { Screen.fullScreen = value; }
    }

    // Is the game resizable by the user?
    // (ex. By growing or shrinking the browser window)
    public static bool Resizable
    {
        get { return resizable; }
    }

    public static int Width
    {
        get { return Screen.width; }
    }

    public static int Height
    {
        get { return Screen.height; }
    }

    public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params Layout[] layoutParams)
    {
#if !UNITY_WEBPLAYER
        Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
#else
        if (fullscreen)
        {
            Screen.SetResolution(width, height, true, preferredRefreshRate);
        }
        else
        {
            resizable = false;
            Application.ExternalCall("IntegratedPluginCanvas.setResolution", width, height);
            SetLayout(layoutParams);
        }
#endif
    }

    public static void SetAspectRatio(int width, int height, params Layout[] layoutParams)
    {
#if !UNITY_WEBPLAYER
        var newWidth = Screen.height / height * width;
        Screen.SetResolution(newWidth, Screen.height, Screen.fullScreen);
#else
        resizable = true;
        Application.ExternalCall("IntegratedPluginCanvas.setAspectRatio", width, height);
        SetLayout(layoutParams);
#endif
    }

    public static void SetUnityPlayerEmbedCSS(string key, string value)
    {
#if UNITY_WEBPLAYER
        Application.ExternalEval(string.Format("$(\"#unityPlayerEmbed\").css(\"{0}\",\"{1}\")", key, value));
#endif
    }

    #region Layout Params

    public static Layout.OptionLeft Left(float amount)
    {
        return new Layout.OptionLeft
        {
            Amount = amount,
        };
    }

    public static Layout.OptionTop Top(float amount)
    {
        return new Layout.OptionTop
        {
            Amount = amount,
        };
    }

    public static Layout.OptionCenterHorizontal CenterHorizontal()
    {
        return new Layout.OptionCenterHorizontal();
    }

    public static Layout.OptionCenterVertical CenterVertical()
    {
        return new Layout.OptionCenterVertical();
    }

    #endregion

    private static void SetLayout(IEnumerable<Layout> parameters)
    {
#if UNITY_WEBPLAYER
        foreach (Layout parameter in parameters)
        {
            var layoutLeft = parameter as Layout.OptionLeft;
            if (layoutLeft != null)
            {
                SetUnityPlayerEmbedCSS("margin-left", layoutLeft.Amount + "px");
                SetUnityPlayerEmbedCSS("padding-left", "0px");
                // remove the horizontal centering align function listener if it's there
                Application.ExternalEval(@"
                    if (typeof fbCenterWebPlayerHorizontally == ""function"") 
                    {
                        $(window).off(""resize"", fbCenterWebPlayerHorizontally);
                    }
                ");
                continue;
            }

            var layoutTop = parameter as Layout.OptionTop;
            if (layoutTop != null)
            {
                SetUnityPlayerEmbedCSS("margin-top", layoutTop.Amount + "px");
                SetUnityPlayerEmbedCSS("padding-top", "0px");
                // remove the vertical centering align function listener if it's there
                Application.ExternalEval(@"
                    if (typeof fbCenterWebPlayerVertically == ""function"") 
                    {
                        $(window).off(""resize"", fbCenterWebPlayerVertically);
                    }
                ");
                continue;
            }

            var layoutCenterHorizontal = parameter as Layout.OptionCenterHorizontal;
            if (layoutCenterHorizontal != null)
            {
                Application.ExternalEval(@"
                    function fbCenterWebPlayerHorizontally(){
                        $(""#unityPlayerEmbed"").css(
                            ""margin-left"", 
                            ($(window).innerWidth()/2 - $(""#unityPlayerEmbed"").children(""object, embed"").width()/2) + ""px"")
                    }; 
                    fbCenterWebPlayerHorizontally(); 
                    $(window).resize(fbCenterWebPlayerHorizontally)
                ");
                continue;
            }

            var layoutCenterVertical = parameter as Layout.OptionCenterVertical;
            if (layoutCenterVertical != null)
            {
                Application.ExternalEval(@"
                    function fbCenterWebPlayerVertically(){
                        $(""#unityPlayerEmbed"").css(
                            ""margin-top"", 
                            ($(window).innerHeight()/2 - $(""#unityPlayerEmbed"").children(""object, embed"").height()/2) + ""px"")
                    }; 
                    fbCenterWebPlayerVertically(); 
                    $(window).resize(fbCenterWebPlayerVertically)
                ");
                continue;
            }

            FbDebug.Error("Unknown Layout type: " + parameter.GetType());
        }
#endif
    }

    public class Layout
    {
        public class OptionLeft : Layout
        {
            public float Amount = 0;
        }

        public class OptionTop : Layout
        {
            public float Amount = 0;
        }

        public class OptionCenterHorizontal : Layout
        {
        }

        public class OptionCenterVertical : Layout
        {
        }
    }
}
