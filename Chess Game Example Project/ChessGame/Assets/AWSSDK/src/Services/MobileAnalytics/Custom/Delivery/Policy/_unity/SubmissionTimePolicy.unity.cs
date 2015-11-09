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
using System.Globalization;


using Amazon.MobileAnalytics;
using Amazon.Util.Storage;
using Amazon.Util.Storage.Internal;

namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    internal class SubmissionTimePolicy:IDeliveryPolicy
    {
        
        private const String LAST_SUCCESSFUL_DELIVERY_TIME_STAMP_KEY = "ma_last_successful_delivery";
        
        private long WaitInterval;
        
        private long LastSubmittedTime;
        
        private KVStore _persistStore;
        
        readonly DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.SubmissionTimePolicy"/> class.
        /// </summary>
        /// <param name="WaitInterval">Wait interval.</param>
        public SubmissionTimePolicy (long WaitInterval)
        {
            this.WaitInterval = WaitInterval;
        
            //retrieve the last submitted timestamp from the cache
            _persistStore = new PlayerPreferenceKVStore();

            String TimeStamp = _persistStore.Get(LAST_SUCCESSFUL_DELIVERY_TIME_STAMP_KEY);
            if(TimeStamp != null && TimeStamp.Length > 0)
            {
                this.LastSubmittedTime = long.Parse(TimeStamp);
            }
            else
            {
                this.LastSubmittedTime = 0;
            }
        }
        
        /// <summary>
        /// Determines whether this policy allows the delivery of the events or not
        /// </summary>
        /// <returns>true, if the wait time and last submitted time is greater than current timestamp</returns>
        /// <c>false</c>
        public bool IsAllowed()
        {
            long now = DateTime.Now.Subtract(start).Ticks;
            
            //return true if the current time is greater than than the wait time + last submitted time.
            if(now > (this.WaitInterval + this.LastSubmittedTime)){
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Call back to policy once the delivery has been completed
        /// On Successful delivery the timestamp is recorded on the local storage
        /// </summary>
        /// <param name="isSuccessful">If set to <c>true</c> successful.</param>
        public void HandleDeliveryAttempt(bool isSuccessful)
        {
            //persist the timestamp
            if(isSuccessful)
            {
                _persistStore.Put(LAST_SUCCESSFUL_DELIVERY_TIME_STAMP_KEY,DateTime.Now.Subtract(start).Ticks.ToString());
            }
        }
    }
}

