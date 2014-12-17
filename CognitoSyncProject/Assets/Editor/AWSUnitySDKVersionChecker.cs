/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
 */
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AWSUnitySDKVersionChecker : AssetPostprocessor
{
    private static string SDK_VERSION_PREFIX = "aws-unity-sdk-version" ;
    
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
    {
        foreach (string assetPath in importedAssets)
        {
            string ext = Path.GetExtension(assetPath);
            string importedVersion = null;
            string existingVersion = null;
            
            // Only operate on txt files
            if (!string.IsNullOrEmpty(ext) && ext.ToLower() == ".txt" && assetPath.Contains(SDK_VERSION_PREFIX) && !assetPath.Contains(".old"))
            {
                // parse imported version
                importedVersion = assetPath.Substring(assetPath.LastIndexOf('-') + 1, assetPath.LastIndexOf(".txt") - assetPath.LastIndexOf('-') - 1);
                
                // find out all Resource folder and search for aws-unity-sdk version file
                List<string> resourceDirs = GetAllResourcesDirs();

                foreach (string dir in resourceDirs)
                {
                    string [] fileEntries = Directory.GetFiles(dir);
                    foreach (string fileName in fileEntries)
                    {
                        ext = Path.GetExtension(fileName);
                        if (!string.IsNullOrEmpty(ext) && ext.ToLower() == ".txt" && fileName.Contains(SDK_VERSION_PREFIX) && !fileName.Contains(".old"))
                        {
                            string version = fileName.Substring(fileName.LastIndexOf('-') + 1, fileName.LastIndexOf(".txt") - fileName.LastIndexOf('-') - 1);
                            
                            if (version != importedVersion)
                            {
                                existingVersion = version;
                                
                                // add old in existing Version file
                                string newName = Path.GetFileName(fileName).Substring(0, Path.GetFileName(fileName).Length - 4) + ".old";
                                string pathName = fileName.Substring(fileName.IndexOf("Assets"));
                                string ret = AssetDatabase.RenameAsset(pathName, newName);
                                if (ret != null)
                                    Debug.LogError(ret);
                            }
                            
                        }
                    
                    }
                }
                
                if (existingVersion != null)
                {
                    //"The existing version of the AWSCore (version x.x.x) has been overwritten by version y.y.y. Please make sure all AWS plugins are of the same version in your project."
                    EditorUtility.DisplayDialog("Warning", 
                                                "The existing version of the AWSCore (" + "version " + existingVersion + ") has been overwritten by version " + importedVersion + ". Please make sure all AWS SDK are of the same version in your project. To avoid any potential conflicts, please remove all AWS SDK and import the latest version. ",
                                                "OK", "");
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
}

