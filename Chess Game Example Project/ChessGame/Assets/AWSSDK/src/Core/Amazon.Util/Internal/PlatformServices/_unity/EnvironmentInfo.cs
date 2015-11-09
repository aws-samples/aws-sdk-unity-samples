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
using System.Globalization;
using System.Linq;
using System.Text;

namespace Amazon.Util.Internal.PlatformServices
{
    public class EnvironmentInfo : IEnvironmentInfo
    {
        public EnvironmentInfo()
        {
            this.Platform = AmazonHookedPlatformInfo.Instance.Platform;
            this.PlatformVersion = AmazonHookedPlatformInfo.Instance.PlatformVersion;
            this.Model = AmazonHookedPlatformInfo.Instance.Model;
            this.Make = AmazonHookedPlatformInfo.Instance.Make;
            this.Locale = AmazonHookedPlatformInfo.Instance.Locale;
            this.FrameworkUserAgent =
                string.Format(CultureInfo.InvariantCulture,
                ".NET_Runtime/{0}.{1} UnityVersion/{2}",
                 Environment.Version.Major,
                 Environment.Version.MajorRevision,
                 DetermineFramework());
            this.PclPlatform = "Unity";
            this.PlatformUserAgent = string.Format(@"unity_{0}_{1}", this.Platform, this.PlatformVersion);
        }

        public string Platform { get; private set; }

        public string Model { get; private set; }

        public string Make { get; private set; }

        public string PlatformVersion { get; private set; }

        public string Locale { get; private set; }

        public string FrameworkUserAgent { get; private set; }

        public string PclPlatform { get; private set; }

        public string PlatformUserAgent { get; private set; }

        private static string DetermineFramework()
        {
            return UnityEngine.Application.unityVersion;
        }



    }
}
