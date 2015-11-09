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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

using System.Globalization;
using UnityEngine;


namespace Amazon.Util.Internal
{
    public static partial class InternalSDKUtils
    {
        static string _userAgentBaseName = "aws-sdk-unity";
		
        private const string UnknownMonoVersion = "Mono/Unknown";
		
        public static string GetMonoRuntimeVersion()
        {
            Type type = Type.GetType("Mono.Runtime");
            if (type != null)
            {
                MethodInfo displayName = type.GetMethod("GetDisplayName");
                if (displayName != null)
                {
                    var version = (string)displayName.Invoke(null, null);
                    // Replace "/" from the version string as it's a
                    // seperator in the user agent format
                    version = version.Replace("/", ":").Replace(" ",string.Empty);
                    return "Mono/" + version;
                }
            }
            return UnknownMonoVersion;
        }

        public static bool IsAndroid
        {
            get
            {
                return Application.platform == RuntimePlatform.Android;
            }
        }

        public static bool IsiOS
        {
            get
            {
                return Application.platform == RuntimePlatform.IPhonePlayer;
            }
        }


        internal static Type GetTypeFromUnityEngine(string typeName)
        {
            return Type.GetType(string.Format("UnityEngine.{0}, UnityEngine", typeName));
        }



    }
}
