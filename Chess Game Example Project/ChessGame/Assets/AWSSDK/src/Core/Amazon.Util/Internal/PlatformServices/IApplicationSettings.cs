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

namespace Amazon.Util.Internal.PlatformServices
{
    public enum ApplicationSettingsMode
    {
        None,
        Local,
        Roaming
    }

    public interface IApplicationSettings
    {
        void SetValue(string key, string value, ApplicationSettingsMode mode);

        string GetValue(string key, ApplicationSettingsMode mode);

        void RemoveValue(string key, ApplicationSettingsMode mode);
    }
}
