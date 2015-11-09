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
using System.Threading;
using System;
using Amazon.Runtime.Internal.Util;


namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    /// <summary>
    /// Amazon mobile analytics background runner.
    /// Background runner periodically sends events to server.
    /// </summary>
    public class BackgroundRunner
    {
        private static System.Threading.Thread _thread = null;
        private static Logger _logger = Logger.GetLogger(typeof(BackgroundRunner));
        private static object _lock = new object();

        /// <summary>
        /// Determines if background thread is alive.
        /// </summary>
        /// <returns><c>true</c> If is alive; otherwise, <c>false</c>.</returns>
        private static bool IsAlive()
        {
            return _thread != null && _thread.ThreadState != ThreadState.Stopped
                                   && _thread.ThreadState != ThreadState.Aborted
                                   && _thread.ThreadState != ThreadState.AbortRequested;
        }

        /// <summary>
        /// Starts the Mobile Analytics Manager background thread.
        /// </summary>
        public static void StartWork()
        {
            lock (_lock)
            {
                if (!IsAlive())
                {
                    _thread = new System.Threading.Thread(DoWork);
                    _thread.Start();
                }
            }
        }

        /// <summary>
        /// Sends Mobile Analytics events to server on background thread.
        /// </summary>
        private static void DoWork()
        {
            while (true)
            {
#if UNITY_EDITOR
                if (UnityInitializer.IsEditorPlaying && !UnityInitializer.IsEditorPaused)
                {
#endif
                    try
                    {
                        _logger.InfoFormat("Mobile Analytics Manager is trying to deliver events in background thread.");

                        IDictionary<string, MobileAnalyticsManager> instanceDictionary = MobileAnalyticsManager.InstanceDictionary;
                        foreach (string appId in instanceDictionary.Keys)
                        {
                            try
                            {
                                MobileAnalyticsManager manager = MobileAnalyticsManager.GetInstance(appId);
                                manager.BackgroundDeliveryClient.AttemptDelivery();
                            }
                            catch (System.Exception e)
                            {
                                _logger.Error(e, "An exception occurred in Mobile Analytics Delivery Client.");
                            }
                        }
                        Thread.Sleep(Convert.ToInt32(AWSConfigsMobileAnalytics.BackgroundSubmissionWaitTime) * 1000);
                    }
                    catch (System.Exception e)
                    {
                        _logger.Error(e, "An exception occurred in Mobile Analytics Manager.");
                    }
#if UNITY_EDITOR
                }

                else if (!UnityInitializer.IsEditorPlaying)
                {
                    _thread.Abort();
                }
#endif
            }
        }
    }
}

