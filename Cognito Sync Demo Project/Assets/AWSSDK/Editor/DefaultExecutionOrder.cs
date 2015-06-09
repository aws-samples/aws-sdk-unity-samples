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
using System.Collections;
using UnityEditor;
using System;

[InitializeOnLoad]
public class DefaultExecutionOrder : MonoBehaviour {
    
    /// <summary>
    /// script to initialize the unity initializer as the top level execution so that the main thread is set
    /// </summary>
    static DefaultExecutionOrder()
    {
        MonoScript[] scripts = MonoImporter.GetAllRuntimeMonoScripts();
        foreach(var ms in scripts)
        {
            if(ms == null || ms.GetClass() == null)
                continue;
            
            if(ms.GetClass().ToString().Equals("Amazon.UnityInitializer"))
            {
                int currentExecutionOrder = MonoImporter.GetExecutionOrder(ms);
                
                if(currentExecutionOrder>=0)
                    MonoImporter.SetExecutionOrder(ms,-100);
            }
        }
    }
    
}
