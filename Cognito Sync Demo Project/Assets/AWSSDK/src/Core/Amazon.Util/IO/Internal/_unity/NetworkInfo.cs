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
using Amazon.Runtime.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Amazon.Util.Storage.Internal
{
    public class NetworkInfo
    {
        public static NetworkReachability GetReachability()
        {
            //if the thread is main thread. Then return the rechability status directly
            if (UnityInitializer.IsMainThread())
            {
                return Application.internetReachability;
            }
            else
            {
                NetworkReachability _networkReachability = NetworkReachability.NotReachable;
                AutoResetEvent asyncEvent = new AutoResetEvent(false);
                UnityRequestQueue.Instance.ExecuteOnMainThread(() =>
                {
                    _networkReachability = Application.internetReachability;
                    asyncEvent.Set();
                });
                asyncEvent.WaitOne();

                return _networkReachability;
            }
        }
    }
}
