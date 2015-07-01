using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.FacebookEditor;
using UnityEditor.XCodeEditor;

namespace UnityEditor.FacebookEditor
{
    public static class XCodePostProcess
    {
        [PostProcessBuild(100)]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            // If integrating with facebook on any platform, throw a warning if the app id is invalid
            if (!FBSettings.IsValidAppId)
            {
                Debug.LogWarning("You didn't specify a Facebook app ID.  Please add one using the Facebook menu in the main Unity editor.");
            }
			// Unity renamed build target from iPhone to iOS in Unity 5, this keeps both versions happy
			if (target.ToString() == "iOS" || target.ToString() == "iPhone")
            {
                UnityEditor.XCodeEditor.XCProject project = new UnityEditor.XCodeEditor.XCProject(path);

                // Find and run through all projmods files to patch the project

                string projModPath = System.IO.Path.Combine(Application.dataPath, "Facebook/Editor/iOS");
                var files = System.IO.Directory.GetFiles(projModPath, "*.projmods", System.IO.SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    project.ApplyMod(Application.dataPath, file);
                }
                project.Save();

                UpdatePlist(path);
                FixupFiles.FixSimulator(path);
                FixupFiles.AddVersionDefine(path);
                FixupFiles.FixColdStart(path);
            }

            if (target == BuildTarget.Android)
            {
                // The default Bundle Identifier for Unity does magical things that causes bad stuff to happen
                if (PlayerSettings.bundleIdentifier == "com.Company.ProductName")
                {
                    Debug.LogError("The default Unity Bundle Identifier (com.Company.ProductName) will not work correctly.");
                }
                if (!FacebookAndroidUtil.IsSetupProperly())
                {
                    Debug.LogError("Your Android setup is not correct. See Settings in Facebook menu.");
                }

                if (!ManifestMod.CheckManifest())
                {
                    // If something is wrong with the Android Manifest, try to regenerate it to fix it for the next build.
                    ManifestMod.GenerateManifest();
                }
            }
        }

        public static void UpdatePlist(string path)
        {
            const string fileName = "Info.plist";
            string appId = FBSettings.AppId; 
            string fullPath = Path.Combine(path, fileName);
            
            if (string.IsNullOrEmpty(appId) || appId.Equals("0"))
            {
                Debug.LogError("You didn't specify a Facebook app ID.  Please add one using the Facebook menu in the main Unity editor.");
                return;
            }
            
            var fbParser = new FBPListParser(fullPath);
            fbParser.UpdateFBSettings(appId, FBSettings.AllAppIds);
            fbParser.WriteToFile();
        }
    }
}
