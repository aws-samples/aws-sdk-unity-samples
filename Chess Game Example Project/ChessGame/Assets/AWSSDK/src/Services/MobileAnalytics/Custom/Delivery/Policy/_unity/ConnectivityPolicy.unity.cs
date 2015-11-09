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
using System.Net;

using UnityEngine;

using Amazon.MobileAnalytics;
using Amazon.Util.Storage.Internal;


namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    public partial class ConnectivityPolicy:IDeliveryPolicy
    {
        /// <summary>
        /// Determines whether this instance has network connectivity.
        /// </summary>
        /// <returns><c>true</c> if this instance has network connectivity; otherwise, <c>false</c>.</returns>
        private bool HasNetworkConnectivity ()
        {
            NetworkReachability networkReachability = NetworkInfo.GetReachability();
            bool networkFlag = false;
            switch (networkReachability) {
                case NetworkReachability.NotReachable:
                    networkFlag = false;
                    break;
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    networkFlag = true;
                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    networkFlag = IsDataAllowed;
                    break;
            }
            return networkFlag;
        }
		
		/// <summary>
		/// Call back to policy once the delivery has been completed
		/// On Successful delivery the timestamp is recorded on the local storage
		/// </summary>
		/// <param name="isSuccessful">If set to <c>true</c> successful.</param>
		public void HandleDeliveryAttempt(bool isSuccessful)
		{
			
		}
    }
	
}