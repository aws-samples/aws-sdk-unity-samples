//
// Copyright 2014-2015 Amazon.com, 
// Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the AWS Mobile SDK For Unity 
// Sample Application License Agreement (the "License"). 
// You may not use this file except in compliance with the 
// License. A copy of the License is located 
// in the "license" file accompanying this file. This file is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, express or implied. See the License 
// for the specific language governing permissions and 
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AWSSDK.Examples
{
    public class GCM
    {
        //namepsaced java class name which will be invoked
        private const string CLASS_NAME = "com.amazonaws.unity.AWSUnityGCMWrapper";

        public static void Register(Action<string> OnRegisterCallback,params string[] senderId)
        {
#if UNITY_ANDROID
            using(AndroidJavaClass cls = new AndroidJavaClass(CLASS_NAME))
            {
                string senderIds = string.Join(",",senderId);
                string regId = cls.CallStatic<string>("register",senderIds);
                Debug.Log("regId = " + regId);
                if (OnRegisterCallback != null)
                    OnRegisterCallback(regId);
            }
#endif
        }
        
        public static void Unregister()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                using (AndroidJavaClass cls = new AndroidJavaClass(CLASS_NAME))
                {
                    cls.CallStatic("unregister");
                }
            }
#endif
        }
    }
}
