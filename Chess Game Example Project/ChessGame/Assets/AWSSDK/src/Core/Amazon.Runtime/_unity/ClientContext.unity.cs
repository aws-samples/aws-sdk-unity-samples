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
using Amazon.Util.Internal.PlatformServices;
using System.Collections.Generic;
using System;
using Amazon.Util.Internal;
using System.IO;

namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// This class composes Client Context header for Amazon Web Service client.
    /// It contains information like app title, version code, version name, client id, OS platform etc.
    /// </summary>
    public partial class ClientContext
    {
        private const string APP_CLIENT_ID_KEY = "mobile_analytics_client_id";

        private static IApplicationSettings _appSetting = ServiceFactory.Instance.GetService<IApplicationSettings>();
        private static IApplicationInfo _appInfo = ServiceFactory.Instance.GetService<IApplicationInfo>();
        private static IEnvironmentInfo _envInfo = ServiceFactory.Instance.GetService<IEnvironmentInfo>();

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Amazon.Runtime.Internal.ClientContext"/> class.
        /// </summary>
        /// <param name="config">Config <see cref="Amazon.Runtime.Internal.ClientContextConfig"/></param>
        public ClientContext(string appId)
        {
            this.AppID = appId;
            this._custom = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(_clientID))
            {
                _clientID = _appSetting.GetValue(APP_CLIENT_ID_KEY, ApplicationSettingsMode.Local);
                if (string.IsNullOrEmpty(_clientID))
                {
                    _clientID = Guid.NewGuid().ToString();
                    _appSetting.SetValue(APP_CLIENT_ID_KEY, _clientID, ApplicationSettingsMode.Local);
                }
            }
        }

    }

}