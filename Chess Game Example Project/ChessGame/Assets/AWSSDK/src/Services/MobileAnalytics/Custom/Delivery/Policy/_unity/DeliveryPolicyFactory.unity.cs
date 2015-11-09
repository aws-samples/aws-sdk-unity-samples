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
using Amazon.MobileAnalytics;
using Amazon.MobileAnalytics.MobileAnalyticsManager;

namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    public partial class DeliveryPolicyFactory:IDeliveryPolicyFactory
    {
        /// <summary>
        /// returns a new force submission time policy
        /// </summary>
        /// <returns>instance of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.IDeliveryPolicy"/></returns>
        public IDeliveryPolicy NewForceSubmissionPolicy()
        {
            return new SubmissionTimePolicy(AWSConfigsMobileAnalytics.ForceSubmissionWaitTime);
        }
        
        
        /// <summary>
        /// returns a new force submission time policy
        /// </summary>
        /// <returns>instance of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.IDeliveryPolicy"/></returns>
        public IDeliveryPolicy NewBackgroundSubmissionPolicy()
        {
            return new SubmissionTimePolicy(AWSConfigsMobileAnalytics.BackgroundSubmissionWaitTime); ;
        }
    
    }
}

