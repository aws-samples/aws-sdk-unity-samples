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
    /// This interface is for delivery policy that determines whether delivery client should send events 
    /// to Mobile Analytics service.
    /// </summary>
    public partial interface IDeliveryPolicy
    {
        /// <summary>
        /// Determines whether this policy allows the delivery of the events or not.
        /// </summary>
        /// <returns><c>true</c> if this policy allows the delivery of events; otherwise, <c>false</c>.</returns>
        bool IsAllowed();
    }
}

