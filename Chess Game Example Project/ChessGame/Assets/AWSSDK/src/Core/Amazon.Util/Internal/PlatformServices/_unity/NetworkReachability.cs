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
    public class NetworkReachability : INetworkReachability
    {
        public NetworkStatus NetworkStatus
        {
            get
            {
                var networkReachability = NetworkInfo.GetReachability();
                if (networkReachability == UnityEngine.NetworkReachability.ReachableViaCarrierDataNetwork)
                    return NetworkStatus.ReachableViaCarrierDataNetwork;
                else if (networkReachability == UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork)
                    return NetworkStatus.ReachableViaWiFiNetwork;
                else
                    return NetworkStatus.NotReachable;
            }
        }

        public event EventHandler<NetworkStatusEventArgs> NetworkReachabilityChanged
        {
            add
            {
                throw new NotImplementedException(ServiceFactory.NotImplementedErrorMessage);
            }
            remove
            {
                throw new NotImplementedException(ServiceFactory.NotImplementedErrorMessage);
            }
        }
    }
}
