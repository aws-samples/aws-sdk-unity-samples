using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

namespace UnityEditor.FacebookEditor
{
    public class FixupFiles
    {
        protected static string Load(string fullPath)
        {
            string data;
            FileInfo projectFileInfo = new FileInfo( fullPath );
            StreamReader fs = projectFileInfo.OpenText();
            data = fs.ReadToEnd();
            fs.Close();

            return data;
        }

        protected static void Save(string fullPath, string data)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(fullPath, false);
            writer.Write(data);
            writer.Close();
        }

        public static void FixSimulator(string path)
        {
            string fullPath = Path.Combine(path, Path.Combine("Libraries", "RegisterMonoModules.cpp"));
            string data = Load (fullPath);


            data = Regex.Replace(data, @"\s+void\s+mono_dl_register_symbol\s+\(const\s+char\*\s+name,\s+void\s+\*addr\);", "");
            data = Regex.Replace(data, "typedef int gboolean;", "typedef int gboolean;\n\tvoid mono_dl_register_symbol (const char* name, void *addr);");

            data = Regex.Replace(data, @"#endif\s+//\s*!\s*\(\s*TARGET_IPHONE_SIMULATOR\s*\)\s*}\s*void RegisterAllStrippedInternalCalls\s*\(\s*\)", "}\n\nvoid RegisterAllStrippedInternalCalls()");
            data = Regex.Replace(data, @"mono_aot_register_module\(mono_aot_module_mscorlib_info\);",
                                 "mono_aot_register_module(mono_aot_module_mscorlib_info);\n#endif // !(TARGET_IPHONE_SIMULATOR)");

            Save (fullPath, data);
        }

        public static void AddVersionDefine(string path)
        {
            int versionNumber = GetUnityVersionNumber ();

            string fullPath = Path.Combine(path, Path.Combine("Libraries", "RegisterMonoModules.h"));
            string data = Load(fullPath);

            if (versionNumber >= 430)
            {
                data += "\n#define HAS_UNITY_VERSION_DEF 1\n";
            } else {
                data += "\n#define UNITY_VERSION ";
                data += versionNumber;
                data += "\n";
            }

            Save(fullPath, data);
        }

        private static int GetUnityVersionNumber()
        {
            string version = Application.unityVersion;
            string[] versionComponents = version.Split('.');

            int majorVersion = 0;
            int minorVersion = 0;

            try
            {
                if (versionComponents != null && versionComponents.Length > 0 && versionComponents[0] != null)
                    majorVersion = Convert.ToInt32(versionComponents[0]);
                if (versionComponents != null && versionComponents.Length > 1 && versionComponents[1] != null)
                    minorVersion = Convert.ToInt32(versionComponents[1]);
            }
            catch (System.Exception e)
            {
                FbDebug.Error("Error parsing Unity version number: " + e);
            }

            return ((majorVersion * 100) + (minorVersion * 10));
        }

        public static void FixColdStart(string path)
        {
            string fullPath = Path.Combine(path, Path.Combine("Classes", "UnityAppController.mm"));
            string data = Load(fullPath);

            data = Regex.Replace(data,
                @"(?x)                                  # Verbose mode
                  (didFinishLaunchingWithOptions.+      # Find this function...
                    (?:.*\n)+?                          # Match as few lines as possible until...
                    \s*return\ )NO(\;\n                 #   return NO;
                  \})                                   # }",
                "$1YES$2");

            Save(fullPath, data);
        }
    }
}
