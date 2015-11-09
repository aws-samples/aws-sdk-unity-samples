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

namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    /// <summary>
    /// Interface for creating delivery policy.
    /// </summary>
    public partial interface IDeliveryPolicyFactory
    {
        /// <summary>
        /// Returns a new connectivity policy.
        /// </summary>
        /// <returns>instance of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.IDeliveryPolicy"/>, which checks network connectivity</returns>
        IDeliveryPolicy NewConnectivityPolicy();
        
    }
}

