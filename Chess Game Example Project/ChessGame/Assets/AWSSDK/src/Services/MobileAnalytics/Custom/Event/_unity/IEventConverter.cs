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
using System.Collections.Generic;
using Amazon.MobileAnalytics.MobileAnalyticsManager.Internal;

namespace Amazon.MobileAnalytics.MobileAnalyticsManager
{
    public interface IEventConverter
    {
	    /// <summary>
        /// Converts to mobile analytics model event. <see cref="Amazon.MobileAnalytics.Model.Event"/>
        /// </summary>
        /// <returns>The to mobile analytics model event.</returns>
        /// <param name="session">Session.</param>
        Amazon.MobileAnalytics.Model.Event ConvertToMobileAnalyticsModelEvent(Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.Session session);
	}
	
}