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
using Amazon.Util.Internal.PlatformServices;
using Amazon.Runtime.Internal.Util;

namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    /// <summary>
    /// An object for determining whether the delivery client should send events
    /// to mobile analytics service by checking the network status.
    /// </summary>
    public partial class ConnectivityPolicy : IDeliveryPolicy
    {
        private readonly bool IsDataAllowed;
        private Logger _logger = Logger.GetLogger(typeof(ConnectivityPolicy));
		
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.ConnectivityPolicy"/> class.
        /// </summary>
        /// <param name="IsDataAllowed">If set to <c>true</c> polciy will allow the delivery on data network.</param>
        public ConnectivityPolicy(bool IsDataAllowed)
        {
            this.IsDataAllowed = IsDataAllowed;
        }

        /// <summary>
        /// Determines whether this policy allows the delivery of the events or not.
        /// </summary>
        /// <returns>true</returns>
        /// <c>false</c>
        public bool IsAllowed()
        {
            return this.HasNetworkConnectivity();
        }
    }
}
