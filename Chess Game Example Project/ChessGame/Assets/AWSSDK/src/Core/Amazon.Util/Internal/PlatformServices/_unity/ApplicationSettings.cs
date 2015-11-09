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
using Amazon.Util.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amazon.Util.Internal.PlatformServices
{
    public class ApplicationSettings : IApplicationSettings
    {

        public void SetValue(string key, string value, ApplicationSettingsMode mode)
        {
            var kvStore = new PlayerPreferenceKVStore();
            kvStore.Put(key, value);
        }

        public string GetValue(string key, ApplicationSettingsMode mode)
        {
            var kvStore = new PlayerPreferenceKVStore();
            return kvStore.Get(key);
        }

        public void RemoveValue(string key, ApplicationSettingsMode mode)
        {
            var kvStore = new PlayerPreferenceKVStore();
            kvStore.Clear(key);
        }
    }
}
