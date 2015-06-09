//
// Copyright 2014-2015 Amazon.com, 
// Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the Amazon Software License (the "License"). 
// You may not use this file except in compliance with the 
// License. A copy of the License is located at
// 
//     http://aws.amazon.com/asl/
// 
// or in the "license" file accompanying this file. This file is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, express or implied. See the License 
// for the specific language governing permissions and 
// limitations under the License.
//
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class AWSUnitySDKVersionChecker : AssetPostprocessor
{
    private static string SDK_VERSION_PREFIX = "aws-unity-sdk-version-" ;
    
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
    {
        foreach (string assetPath in importedAssets)
        {
            string ext = Path.GetExtension(assetPath);
            string importedVersion = null;
            string existingVersion = null;
            VersionCode importedVersionCode = null;
            VersionCode existingVersionCode = null;
            string importedComponent = null;
            string existingComponent = null;

            // Only operate on txt files
            if (!string.IsNullOrEmpty(ext) && ext.ToLower() == ".txt" && assetPath.Contains(SDK_VERSION_PREFIX))
            {                     
                try
                {
                    // read imported version
                    System.IO.StreamReader reader = new System.IO.StreamReader(assetPath);
                    importedVersion = reader.ReadToEnd();
                    reader.Close();
                    // parse version code
                    importedVersionCode = ParseVersionCode(importedVersion);
                    // get component name
                    importedComponent = GetComponentName(assetPath);

                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }

                // find out all Resource folder and search for aws-unity-sdk version file
                List<string> resourceDirs = GetAllResourcesDirs();

                foreach (string dir in resourceDirs)
                {
                    string [] fileEntries = Directory.GetFiles(dir);
                    foreach (string fileName in fileEntries)
                    {
                        ext = Path.GetExtension(fileName);
                        if (!string.IsNullOrEmpty(ext) && ext.ToLower() == ".txt" && fileName.Contains(SDK_VERSION_PREFIX))
                        {
                            try
                            {
                                // get version string
                                System.IO.StreamReader reader = new System.IO.StreamReader(fileName);
                                existingVersion = reader.ReadToEnd();
                                reader.Close();

                                // parse version code
                                existingVersionCode = ParseVersionCode(existingVersion);

                                // get component name
                                existingComponent = GetComponentName(fileName);
                            }
                            catch (Exception e)
                            {
                                Debug.LogException(e);
                            }
                            

                            // check compatibility
                            if (existingVersionCode.W != importedVersionCode.W || existingVersionCode.X != importedVersionCode.X)
                            {    
                                EditorUtility.DisplayDialog("Warning",
                                                            "The existing " + existingComponent  + " (" + "version " + existingVersion + ") may not be compatible with " + importedComponent + "(version " + importedVersion + "). To avoid any potential conflicts, please remove all AWS SDK and import the latest version. ",
                                                            "OK", "");

                                break;
                            }

                        }
                    
                    }
                }
            }
            
        }
    }
    
    private static List<string> GetAllResourcesDirs()
    {
        List<string> resourceDirs = new List<string>();
        Stack<string> dirStack = new Stack<string>();
        dirStack.Push(Application.dataPath);
        while (dirStack.Count > 0)
        {
            string currentDir = dirStack.Pop();
            try
            {
                string [] subDirs = Directory.GetDirectories(currentDir);
                foreach (string dir in subDirs)
                {
                    if (Path.GetFileName(dir) == "Resources")
                    {
                        resourceDirs.Add(dir);
                    }
                    dirStack.Push(dir);
                }
            } 
            catch
            {
                Debug.LogError(currentDir + " cannot be read ");
            }
        }
        return resourceDirs;
    }

    private static VersionCode ParseVersionCode(string versionText)
    {
        if (string.IsNullOrEmpty(versionText))
        {
            throw new ArgumentNullException("versionText");
        }
        
        char[] delimiter = {'.'};
        string[] words = versionText.Split(delimiter);
        if (words.Length != 4)
        {
            throw new Exception("Parsing AWS Unity SDK version code fail.");
        }
        VersionCode versionCode = new VersionCode(words[0], words[1], words[2], words[3]);
        return versionCode;
    }

    private static string GetComponentName(string filename)
    {
        if (string.IsNullOrEmpty(filename))
        {
            throw new ArgumentException("filename");
        }


        // parse component name
        int pos1 = filename.ToLower().LastIndexOf(SDK_VERSION_PREFIX);
        int pos2 = filename.ToLower().LastIndexOf(".txt");

        string name = filename.Substring(pos1 + SDK_VERSION_PREFIX.Length, pos2 - pos1 - SDK_VERSION_PREFIX.Length);

        return name;
    }



    private class VersionCode
    {
        public VersionCode(string W, string X, string Y, string Z)
        {
            if (string.IsNullOrEmpty(W))
                throw new ArgumentNullException("W");
            if (string.IsNullOrEmpty(X))
                throw new ArgumentNullException("X");
            if (string.IsNullOrEmpty(Y))
                throw new ArgumentNullException("Y");
            if (string.IsNullOrEmpty(Z))
                throw new ArgumentNullException("Z");
            this.W = Convert.ToInt32(W);
            this.X = Convert.ToInt32(X);
            this.Y = Convert.ToInt32(Y);
            this.Z = Convert.ToInt32(Z);
        }
        public int W;
        public int X;
        public int Y;
        public int Z;
    };
}

