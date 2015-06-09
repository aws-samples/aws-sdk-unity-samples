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
using System.Text;
using System.Xml.Linq;


namespace Amazon.Util.Internal
{
    /// <summary>
    /// Root AWS config
    /// </summary>
    public partial class RootConfig
    {
        public LoggingConfig Logging { get; private set; }
        public ProxyConfig Proxy { get; private set; }
        public string EndpointDefinition { get; set; }
        public string Region { get; set; }
        public string ProfileName { get; set; }
        public string ProfilesLocation { get; set; }
        public RegionEndpoint RegionEndpoint
        {
            get
            {
                if (string.IsNullOrEmpty(Region))
                    return null;
                return RegionEndpoint.GetBySystemName(Region);
            }
            set
            {
                if (value == null)
                    Region = null;
                else
                    Region = value.SystemName;
            }
        }
        public bool UseSdkCache { get; set; }
        public bool CorrectForClockSkew { get; set; }

        private const string _rootAwsSectionName = "aws";
        public RootConfig()
        {
            Logging = new LoggingConfig();
            Proxy = new ProxyConfig();

            EndpointDefinition = AWSConfigs._endpointDefinition;
            Region = AWSConfigs._awsRegion;
            ProfileName = AWSConfigs._awsProfileName;
            ProfilesLocation = AWSConfigs._awsAccountsLocation;
            UseSdkCache = AWSConfigs._useSdkCache;
            CorrectForClockSkew = true;

#if !WIN_RT && !WINDOWS_PHONE
            var root = AWSConfigs.GetSection<AWSSection>(_rootAwsSectionName);

            Logging.Configure(root.Logging);
            Proxy.Configure(root.Proxy);

            ServiceSections = root.ServiceSections;
            if (root.UseSdkCache.HasValue)
                UseSdkCache = root.UseSdkCache.Value;

            EndpointDefinition = Choose(EndpointDefinition, root.EndpointDefinition);
            Region = Choose(Region, root.Region);
            ProfileName = Choose(ProfileName, root.ProfileName);
            ProfilesLocation = Choose(ProfilesLocation, root.ProfilesLocation);
            if (root.CorrectForClockSkew.HasValue)
                CorrectForClockSkew = root.CorrectForClockSkew.Value;
#endif
        }

        // If a is not null-or-empty, returns a; otherwise, returns b.
        private static string Choose(string a, string b)
        {
            return (string.IsNullOrEmpty(a) ? b : a);
        }

        IDictionary<string, XElement> ServiceSections { get; set; }
        public XElement GetServiceSection(string service)
        {
            XElement section;
            if (ServiceSections.TryGetValue(service, out section))
                return section;

            return null;
        }
    }

}
