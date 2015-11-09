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
    public enum NetworkStatus
    {
        NotReachable,
        ReachableViaCarrierDataNetwork,
        ReachableViaWiFiNetwork
    }

    public class NetworkStatusEventArgs : EventArgs
    {
        public NetworkStatus Status { get; private set; }

        public NetworkStatusEventArgs(NetworkStatus status)
        {
            this.Status = status;
        }
    }

    public interface INetworkReachability
    {
        NetworkStatus NetworkStatus { get; }

        event EventHandler<NetworkStatusEventArgs> NetworkReachabilityChanged;
    }
}
