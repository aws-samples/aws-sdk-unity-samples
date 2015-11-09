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

using Amazon.MobileAnalytics.Model;

namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    internal interface IDeliveryClient
    {
        /// <summary>
        /// Attempts the delivery of all the events from local store to service
        /// </summary>
        void AttemptDelivery();
        
        /// <summary>
        /// Enqueues the events for delivery. The event is stored in an <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.IEventStore"/>.
        /// </summary>
        /// <param name="eventObject">Event object. <see cref="Amazon.MobileAnalytics.Model.Event"/></param>
        void EnqueueEventsForDelivery(Amazon.MobileAnalytics.Model.Event E);
        
        /// <summary>
        /// Set custom policies to the delivery client. This will allow you to fine grain control on when an attempt should be made to deliver the events on the service.
        /// </summary>
        /// <param name="policy">An instance of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.IDeliveryPolicy"/></param>
        void AddDeliveryPolicies(IDeliveryPolicy policy);
    }
}

