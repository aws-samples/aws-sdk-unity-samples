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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Amazon.Runtime;
using Amazon.Util;
using Amazon.Runtime.Internal;
using Amazon.MobileAnalytics.MobileAnalyticsManager.Internal;


namespace Amazon.MobileAnalytics.MobileAnalyticsManager
{

    /// <summary>
    /// MobileAnalyticsManager in the entry point to recording analytic events for your application
    /// </summary>
    public class MobileAnalyticsManager
    {
        private string _appId = null;
        private static object _lock = new object();
        private static IDictionary<string, MobileAnalyticsManager> _instanceDictionary = new Dictionary<string, MobileAnalyticsManager>();

        #region constructor

        /// <summary>
        /// Gets the or creates Mobile Analytics Manager instance. If the instance already exists, returns the instance; otherwise
        /// creates new instance and returns it.
        /// </summary>
        /// <returns>Mobile Analytics Manager instance.</returns>
        /// <param name="credentials">AWS Credentials.</param>
        /// <param name="regionEndpoint">Region endpoint.</param>
        /// <param name="appId">Amazon Mobile Analytics Application ID.</param>
        public static MobileAnalyticsManager GetOrCreateInstance(AWSCredentials credential,
                                                                 RegionEndpoint regionEndpoint,
                                                                 string appId)
        {
            if (credential == null)
                throw new ArgumentNullException("credential");
            if (regionEndpoint == null)
                throw new ArgumentNullException("regionEndpoint");
            if (string.IsNullOrEmpty(appId))
                throw new ArgumentNullException("appId");

            return MobileAnalyticsManager.GetOrCreateInstanceHelper(appId, credential, regionEndpoint);
        }

        /// <summary>
        /// This API is deprecated, use GetOrCreateInstance(AWSCredentials credential,RegionEndpoint regionEndpoint,string appId)
        /// Gets the or creates Mobile Analytics Manager instance. If the instance already exists, returns the instance; otherwise
        /// creates new instance and returns it.
        /// </summary>
        /// <returns>Mobile Analytics Manager instance.</returns>
        /// <param name="credentials">AWS Credentials.</param>
        /// <param name="appId">Amazon Mobile Analytics Application ID.</param>
        [Obsolete(@"This API is deprecated, use GetOrCreateInstance(AWSCredentials credential,RegionEndpoint regionEndpoint,string appId)")]
        public static MobileAnalyticsManager GetOrCreateInstance(AWSCredentials credential,
                                                                 string appId)
        {
            if (credential == null)
                throw new ArgumentNullException("credential");
            if (string.IsNullOrEmpty(appId))
                throw new ArgumentNullException("appId");

            return MobileAnalyticsManager.GetOrCreateInstanceHelper(appId, credential, null);
        }

        /// <summary>
        /// Gets Mobile Analytics Manager instance by Application ID. Returns Mobile Analytics Manager instance if it's found.
        /// Throws InvalidOperationException if the instance has not been instantiated.
        /// </summary>
        /// <returns>The Mobile Analytics Manager instance.</returns>
        /// <param name="appId">Amazon Mobile Analytics Application ID.</param>
        public static MobileAnalyticsManager GetInstance(string appId)
        {
            if (string.IsNullOrEmpty(appId))
                throw new ArgumentNullException("appId");

            lock (_lock)
            {
                if (_instanceDictionary.ContainsKey(appId))
                {
                    return _instanceDictionary[appId];
                }
                else
                {
                    throw new InvalidOperationException("We cannot find MobileAnalyticsManager instance for appId " + appId + ". Please call GetOrCreateInstance() first.");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.MobileAnalyticsManager"/> class.
        /// </summary>
        /// <param name="clientContextConfig">Client context config.</param>
        /// <param name="regionEndpoint">Region endpoint.</param>
        /// <param name="credentials">AWS Credentials.</param>
        private MobileAnalyticsManager(string appId, AWSCredentials credentials, RegionEndpoint regionEndpoint)
        {
            _appId = appId;

            ClientContext = new Amazon.Runtime.Internal.ClientContext(appId);

            if (credentials == null)
                throw new ArgumentNullException("credentials");

            BackgroundDeliveryClient = new DeliveryClient(AWSConfigsMobileAnalytics.AllowUseDataNetwork, ClientContext, credentials, regionEndpoint);

            Session = new Session(_appId);
        }

        /// <summary>
        /// Gets the or create instance.
        /// </summary>
        /// <returns>The or create instance.</returns>
        /// <param name="clientContextConfig">Client context config.</param>
        /// <param name="regionEndpoint">Region endpoint.</param>
        /// <param name="credentials">Credentials.</param>
        private static MobileAnalyticsManager GetOrCreateInstanceHelper(string appId,
                                                                        AWSCredentials credential,
                                                                        RegionEndpoint regionEndpoint
                                                                       )
        {
            MobileAnalyticsManager managerInstance = null;
            bool isNewInstance = false;
            lock (_lock)
            {
                if (_instanceDictionary.ContainsKey(appId))
                {
                    managerInstance = _instanceDictionary[appId];
                }
                else
                {
                    managerInstance = new MobileAnalyticsManager(appId, credential, regionEndpoint);
                    _instanceDictionary[appId] = managerInstance;
                    isNewInstance = true;
                }
            }

            if (isNewInstance)
                managerInstance.Session.Start();

            BackgroundRunner.StartWork();

            return managerInstance;
        }
        #endregion

        #region public
        /// <summary>
        /// Pauses the current session.
        /// PauseSession() is the entry point into the Amazon Mobile Analytics SDK where sessions can be resumed. Session is created and started immediately 
        /// after instantiating the MobileAnalyticsManager object. The session remains active until it is paused. When in a paused state, the session time will 
        /// not accumulate. When resuming a session, if enough time has elapsed from when the session is paused to when it's resumed, the session is ended and 
        /// a new session is created and started. Otherwise, the paused session is resumed and the session time continues to accumulate. Currently session 
        /// time out default value is 5 seconds.
        /// 
        /// For example, when MobileAnalyticsManager is first instantiated, it creates Session 1. As the user transitions from scene to scene, the old 
        /// scene will pause the current session, and the new scene will immediately resume the current session. In this case, Session 1 remains active 
        /// and accumulates session time. The user continues to use the App for a total of 3 minutes, at which point, the user receives a phone call. 
        /// When transitioning to the phone call, the current scene will pause the session and then transition to the phone app. In this case Session 1 
        /// remains paused while the phone call is in progress and session time does not accumulate. After completing the phone call a few minutes later, 
        /// the user returns to the App and the scene will resume Session 1. Since enough time has elapsed since resuming Session 1, Session 1 will be ended 
        /// with a play time of 3 minutes. Session 2 will then be immediately created and started.
        /// 
        /// In order for MobileAnalyticsManager to track sessions, you must call the PauseSession() and ResumeSession() in each scene of your Unity app
        /// and you must call them from main thread.
        /// <example>
        /// The example below shows how to pause and resume session
        /// <code>
        /// public class AmazonMobileAnalyticsSample : MonoBehaviour
        /// {
        ///     void Start()
        ///     {
        ///         analyticsManager = MobileAnalyticsManager.GetOrCreateInstance(credential,region,appId);
        ///     }
        /// 
        ///     void OnApplicationFocus(bool focus) 
        ///     {
        ///         if(focus)
        ///         {
        ///             analyticsManager.ResumeSession();
        ///         }
        ///         else
        ///         {
        ///             analyticsManager.PauseSession();
        ///         }
        ///     }
        /// }
        /// </code>  
        /// </example> 
        /// </summary>
        public void PauseSession()
        {
            Session.Pause();
        }

        /// <summary>
        /// Resume the current session.
        /// ResumeSession() is the entry point into the Amazon Mobile Analytics SDK where sessions can be resumed. Session is created and started immediately 
        /// after instantiating the MobileAnalyticsManager object. The session remains active until it is paused. When in a paused state, the session time will 
        /// not accumulate. When resuming a session, if enough time has elapsed from when the session is paused to when it's resumed, the session is ended and 
        /// a new session is created and started. Otherwise, the paused session is resumed and the session time continues to accumulate. Currently session 
        /// time out default value is 5 seconds.
        /// 
        /// For example, when MobileAnalyticsManager is first instantiated, it creates Session 1. As the user transitions from scene to scene, the old 
        /// scene will pause the current session, and the new scene will immediately resume the current session. In this case, Session 1 remains active 
        /// and accumulates session time. The user continues to use the App for a total of 3 minutes, at which point, the user receives a phone call. 
        /// When transitioning to the phone call, the current scene will pause the session and then transition to the phone app. In this case Session 1 
        /// remains paused while the phone call is in progress and session time does not accumulate. After completing the phone call a few minutes later, 
        /// the user returns to the App and the scene will resume Session 1. Since enough time has elapsed since resuming Session 1, Session 1 will be ended 
        /// with a play time of 3 minutes. Session 2 will then be immediately created and started.
        /// 
        /// In order for MobileAnalyticsManager to track sessions, you must call the PauseSession() and ResumeSession() in each scene of your Unity app
        /// and you must call them from main thread.
        /// <example>
        /// The example below shows how to pause and resume session
        /// <code>
        /// public class AmazonMobileAnalyticsSample : MonoBehaviour
        /// {
        ///     void Start()
        ///     {
        ///         analyticsManager = MobileAnalyticsManager.GetOrCreateInstance(credential,region,appId);
        ///     }
        /// 
        ///     void OnApplicationFocus(bool focus) 
        ///     {
        ///         if(focus)
        ///         {
        ///             analyticsManager.ResumeSession();
        ///         }
        ///         else
        ///         {
        ///             analyticsManager.PauseSession();
        ///         }
        ///     }
        /// }
        /// </code>  
        /// </example> 
        /// </summary>
        public void ResumeSession()
        {
            Session.Resume();
        }

        /// <summary>
        /// Records the custom event to the local persistent storage. Background thread will deliver the event later.
        /// </summary>
        /// <param name="customEvent">The event.</param>
        public void RecordEvent(CustomEvent customEvent)
        {
            customEvent.Timestamp = DateTime.UtcNow.ToString(AWSSDKUtils.ISO8601DateFormat);

            Amazon.MobileAnalytics.Model.Event modelEvent = customEvent.ConvertToMobileAnalyticsModelEvent(this.Session);
            BackgroundDeliveryClient.EnqueueEventsForDelivery(modelEvent);
        }

        /// <summary>
        /// Adds client context custom attribute
        /// Refer <a href="http://docs.aws.amazon.com/mobileanalytics/latest/ug/PutEvents.html" >Rest API</a> for more information
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public void AddClientContextCustomAttribute(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            ClientContext.AddCustomAttributes(key, value);
        }


        #endregion


        #region internal
        internal Session Session { get; private set; }

        internal Amazon.Runtime.Internal.ClientContext ClientContext { get; private set; }

        internal IDeliveryClient BackgroundDeliveryClient { get; private set; }

        internal static IDictionary<string, MobileAnalyticsManager> InstanceDictionary
        {
            get
            {
                lock (_lock)
                {
                    return new Dictionary<string, MobileAnalyticsManager>(_instanceDictionary);
                }
            }
        }

        internal void ForceDelivery()
        {
            BackgroundDeliveryClient.AttemptDelivery();
        }

        #endregion

    }
}