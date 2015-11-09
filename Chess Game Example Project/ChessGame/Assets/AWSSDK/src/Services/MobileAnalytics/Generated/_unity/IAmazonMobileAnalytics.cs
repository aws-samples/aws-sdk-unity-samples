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

/*
 * Do not modify this file. This file is generated from the mobileanalytics-2014-06-05.normal.json service model.
 */

using System;
using System.Collections.Generic;
using Amazon.Runtime;
using Amazon.MobileAnalytics.Model;

namespace Amazon.MobileAnalytics
{
    /// <summary>
    /// Interface for accessing MobileAnalytics
    ///
    /// Amazon Mobile Analytics is a service for collecting, visualizing, and understanding
    /// app usage data at scale.
    /// </summary>
    public partial interface IAmazonMobileAnalytics : IDisposable
    {

        
        #region  PutEvents


        /// <summary>
        /// Initiates the asynchronous execution of the PutEvents operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutEvents operation on AmazonMobileAnalyticsClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        void PutEventsAsync(PutEventsRequest request, AmazonServiceCallback<PutEventsRequest, PutEventsResponse> callback, AsyncOptions options = null);


        #endregion
        
    }
}