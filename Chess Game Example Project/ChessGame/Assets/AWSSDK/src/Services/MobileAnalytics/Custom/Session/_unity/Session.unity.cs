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
using System.Collections;
using System;

using Amazon.Util;
using Amazon.Util.Storage;
using ThirdParty.Json.LitJson;
using System.IO;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Util;
using System.Threading;
using Amazon.Util.Internal;

namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    public class Session
    {
        private Logger _logger = Logger.GetLogger(typeof(Session));
        
        // Event Type Constants ---------------------------
        public static string SESSION_START_EVENT_TYPE            = "_session.start";
        public static string SESSION_STOP_EVENT_TYPE             = "_session.stop";
        public static string SESSION_PAUSE_EVENT_TYPE            = "_session.pause";
        public static string SESSION_RESUME_EVENT_TYPE           = "_session.resume";
        // Event Attribute/Metric Key Constants -----------
        private const string SESSION_ID_ATTRIBUTE_KEY            = "_session.id";
        private const string SESSION_DURATION_METRIC_KEY         = "_session.duration";
        private const string SESSION_START_TIME_ATTRIBUTE_KEY    = "_session.startTime";
        private const string SESSION_STOP_TIME_ATTRIBUTE_KEY     = "_session.stopTime";
        
        
        // session info
        private DateTime _startTime;
        private DateTime? _stopTime;
        private DateTime _preStartTime;
        private string _sessionId;
        private long _duration;
        
        // lock to guard session info
        private Object _lock = new Object();
       


        private class SessionStorage
        {
            public SessionStorage()
            {
                _sessionId = null; _duration = 0;
            }
            
            public DateTime _startTime;
            public DateTime? _stopTime;
            public DateTime _preStartTime;
            public string _sessionId;
            public long _duration;
        }
        private SessionStorage _sessionStorage = new SessionStorage();
   

        private string _appid;
        
        private string _sessionStorageFilePath;
        
        #region public
        public Session(string appId)
        {
            if(string.IsNullOrEmpty(appId))
                throw new ArgumentNullException("appId");

            _appid = appId;
            _sessionStorageFilePath = AmazonHookedPlatformInfo.Instance.PersistentDataPath + "/"+_appid +"_session_storage.json";
        }
        
        
        
        /// <summary>
        /// Start this session.
        /// </summary>
        internal void Start()
        {
            // read session info from persistent storage, in case app is killed
            RetrieveSessionStorage();
            
            // if session storage is valid, restore session and resume session
            if(_sessionStorage != null && !string.IsNullOrEmpty(_sessionStorage._sessionId))
            {
                _startTime = _sessionStorage._startTime;
                _stopTime = _sessionStorage._stopTime;
                _sessionId = _sessionStorage._sessionId;
                _duration = _sessionStorage._duration;
                
                Resume();
            }
            else
            {
                NewSession();
            }
            
        }
        
        /// <summary>
        /// Pause this session.
        /// </summary>
        public void Pause()
        {
            PauseSession();
            SaveSessionStorage();
        }
        
        /// <summary>
        /// Resume this session.
        /// </summary>
        public void Resume()
        {
            if(_stopTime == null)
            {
                //this may sometimes be a valid scenario e.g when the applciation starts
                InvalidOperationException e = new InvalidOperationException();
                _logger.Error(e,"call Resume() without calling Pause() first");
                return;
            }
            
            DateTime currentTime = DateTime.UtcNow;
            
            if(_stopTime.Value < currentTime)
            {
                
                long resumeTimeStamp =  Convert.ToInt64((currentTime - AWSSDKUtils.EPOCH_START).TotalSeconds) ;
                long stopTimeStamp =  Convert.ToInt64((_stopTime.Value - AWSSDKUtils.EPOCH_START).TotalSeconds);
                
                // new session 
                if (resumeTimeStamp - stopTimeStamp > AWSConfigsMobileAnalytics.SessionTimeout)
                {
                    StopSession();
                    NewSession();
                    
                }
                // resume old session
                else
                {
                    ResumeSession();
                }
            
            }
            else
            {
                InvalidOperationException e = new InvalidOperationException();
                _logger.Error(e, "session stop time is earlier than start time !");
            }

        }

        /// <summary>
        /// Gets the session info.
        /// </summary>
        /// <param name="startTimestamp">Start timestamp.</param>
        /// <param name="stopTimestamp">Stop timestamp.</param>
        /// <param name="sessionId">Session identifier.</param>
        /// <param name="duration">Duration.</param>
        public void GetSessionInfo(out string startTimestamp, out string stopTimestamp, out string sessionId, out long duration)
        {
            if(string.IsNullOrEmpty(_sessionId))
            {
                InvalidOperationException e = new InvalidOperationException();
                _logger.Error(e, "session id is empty");
                NewSession();
            }
            
            lock(_lock)
            {
                startTimestamp = _startTime.ToString(AWSSDKUtils.ISO8601DateFormat);
                if(_stopTime != null)
                {
                    stopTimestamp = ((DateTime)_stopTime).ToString(AWSSDKUtils.ISO8601DateFormat);
                }
                else
                    stopTimestamp = null;
                sessionId = string.Copy(_sessionId);
                duration = _duration;
            }
        }
        #endregion
        
        
        #region private
        private void NewSession()
        {
            lock(_lock)
            {
                _startTime = DateTime.UtcNow;
                _preStartTime = DateTime.UtcNow;
                _stopTime = null;
                _sessionId = Guid.NewGuid().ToString();
                _duration = 0;
            }

            CustomEvent customEvent = new CustomEvent(SESSION_START_EVENT_TYPE);
            MobileAnalyticsManager.GetInstance(_appid).RecordEvent(customEvent);
        }
        
        private void StopSession()
        {
            DateTime currentTime = DateTime.UtcNow;
            
            // record session stop event
            CustomEvent managerEvent = new CustomEvent(SESSION_STOP_EVENT_TYPE);
            lock(_lock)
            {
                if(_stopTime != null)
                    managerEvent.StopTimestamp = ((DateTime)_stopTime).ToString(AWSSDKUtils.ISO8601DateFormat);
                
                managerEvent.Duration = _duration;
            }
            MobileAnalyticsManager.GetInstance(_appid).RecordEvent(managerEvent);
        }
        
        private void PauseSession()
        {
            DateTime currentTime = DateTime.UtcNow;
           
            // update session info
            lock(_lock)
            {
                _stopTime = currentTime;
                _duration += Convert.ToInt64((currentTime-_preStartTime).TotalMilliseconds);
            }
            
            // record session pause event
            CustomEvent customEvent = new CustomEvent(SESSION_PAUSE_EVENT_TYPE);
            MobileAnalyticsManager.GetInstance(_appid).RecordEvent(customEvent);
        }
        
        private void ResumeSession()
        {
            DateTime currentTime = DateTime.UtcNow;
            
            // update session info
            lock(_lock)
            {
                _preStartTime = currentTime;
            }
            
            // record session resume event
            CustomEvent customEvent = new CustomEvent(SESSION_RESUME_EVENT_TYPE);
            lock(_lock)
            {
                if(_stopTime != null)
                    customEvent.StopTimestamp = ((DateTime)_stopTime).ToString(AWSSDKUtils.ISO8601DateFormat);
                    
                customEvent.Duration = _duration;
            }
            MobileAnalyticsManager.GetInstance(_appid).RecordEvent(customEvent);
        }
        
        private void SaveSessionStorage()
        {

            lock (_lock)
            {
                _sessionStorage._startTime = _startTime;
                _sessionStorage._stopTime = _stopTime;
                _sessionStorage._preStartTime = _preStartTime;
                _sessionStorage._sessionId = _sessionId;
                _sessionStorage._duration = _duration;
            }

            // store session into file
            _logger.DebugFormat("Mobile Analytics is about to store session info: {0} ", JsonMapper.ToJson(_sessionStorage));

            Action action = () =>
            {
                // create a file to store session info
                if (!File.Exists(_sessionStorageFilePath))
                {
                    FileStream fs = File.Create(_sessionStorageFilePath);
                    fs.Close();

                    File.WriteAllText(_sessionStorageFilePath, JsonMapper.ToJson(_sessionStorage));
                }
                else
                {
                    File.WriteAllText(_sessionStorageFilePath, String.Empty);
                    File.WriteAllText(_sessionStorageFilePath, JsonMapper.ToJson(_sessionStorage));
                }
            };


            if (UnityInitializer.IsMainThread())
            {
                action();
            }
            else
            {
                ManualResetEvent e = new ManualResetEvent(false);
                UnityRequestQueue.Instance.ExecuteOnMainThread(() =>
                {
                    action();
                    e.Set();
                });
                e.WaitOne();
            }
        }
        
        private void RetrieveSessionStorage()
        {
            string sessionString = null;

            Action action = () =>
            {
                if (File.Exists(_sessionStorageFilePath))
                {
                    System.IO.StreamReader sessionFile = new System.IO.StreamReader(_sessionStorageFilePath);
                    sessionString = sessionFile.ReadToEnd();
                    sessionFile.Close();
                    _logger.DebugFormat("Mobile Analytics retrieves session info: {0}", sessionString);
                }
                else
                {
                    _logger.DebugFormat("Mobile Analytics session file does not exist.");
                }
            };


            if (UnityInitializer.IsMainThread())
            {
                action();
            }
            else
            {
                ManualResetEvent e = new ManualResetEvent(false);
                UnityRequestQueue.Instance.ExecuteOnMainThread(() =>
                {
                    action();
                    e.Set();
                });

                e.WaitOne();
            }


            if (!string.IsNullOrEmpty(sessionString))
                _sessionStorage = JsonMapper.ToObject<SessionStorage>(sessionString);
        }
        
        #endregion
    }
}
