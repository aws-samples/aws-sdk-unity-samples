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
namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    /// <summary>
    /// Objects that represnets constants used in Mobile Analytics
    /// </summary>
    internal class Constants
    {
        // Event Type Constants ---------------------------
        public const string SESSION_START_EVENT_TYPE = "_session.start";
        public const string SESSION_STOP_EVENT_TYPE = "_session.stop";
        public const string SESSION_PAUSE_EVENT_TYPE = "_session.pause";
        public const string SESSION_RESUME_EVENT_TYPE = "_session.resume";
    }
}