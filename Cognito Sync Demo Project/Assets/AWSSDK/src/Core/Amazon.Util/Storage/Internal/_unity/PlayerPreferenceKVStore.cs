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
using Amazon.Runtime.Internal;
using System.Threading;

namespace Amazon.Util.Storage.Internal
{
    public class PlayerPreferenceKVStore : KVStore
    {

        public override void Clear(string key)
        {
            if (UnityInitializer.IsMainThread())
            {
                ClearHelper(key);
            }
            else
            {
                UnityRequestQueue.Instance.ExecuteOnMainThread(() =>
                {
                    ClearHelper(key);
                });
            }
        }

        public override void Put(string key, string value)
        {
            if (UnityInitializer.IsMainThread())
            {
                PutHelper(key, value);
            }
            else
            {
                UnityRequestQueue.Instance.ExecuteOnMainThread(() =>
                {
                    PutHelper(key, value);
                });
            }
        }

        public override string Get(string key)
        {
            if (UnityInitializer.IsMainThread())
            {
                return GetHelper(key);
            }
            else
            {
                string value = string.Empty;
                AutoResetEvent asyncEvent = new AutoResetEvent(false);
                UnityRequestQueue.Instance.ExecuteOnMainThread(() =>
                {
                    value = GetHelper(key);
                    asyncEvent.Set();
                });
                asyncEvent.WaitOne();
                return value;
            }
            
        }

        #region private methods

        private void PutHelper(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        private void ClearHelper(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }

        private string GetHelper(string key)
        {
            string value = string.Empty;
            if (PlayerPrefs.HasKey(key))
                value = PlayerPrefs.GetString(key);

            return value;
        }
        #endregion
    }
}
