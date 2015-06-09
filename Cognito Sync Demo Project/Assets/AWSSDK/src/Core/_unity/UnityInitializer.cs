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
using System.Collections.Specialized;
using System.Diagnostics;

using Amazon;
using Amazon.Runtime.Internal.Util;
using Amazon.Runtime.Internal;
using Amazon.Util;
using System.Threading;
using Amazon.Util.Internal;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Amazon
{ 
    /// <summary>
    /// A singleton instance of which will initialize the Main thread dispatcher & logger.
    /// This class is linked to the AWSPrefab so that user can make any configuration 
    /// changes and instance will be included in every scene via the prefab.
    /// </summary>
    public class UnityInitializer : MonoBehaviour
    {

        private static UnityInitializer _instance = null;
        private static object _lock = new object();
        private Logger _logger; 
        
        #region constructor
        private UnityInitializer(){}
        #endregion

        #region public api
        public static UnityInitializer Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Awake()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    // singleton instance
                    _instance = this;

                    if (_mainThread == null || !_mainThread.Equals(Thread.CurrentThread))
                        _mainThread = Thread.CurrentThread;


                    // preventing the instance from getting destroyed between scenes
                    DontDestroyOnLoad(this);

                    //initialize the logger
                    TraceListener tracelistener = new UnityDebugTraceListener("UnityDebug");
                    AWSConfigs.AddTraceListener("Amazon", tracelistener);

                    // Associate the main thread dispatcher
                    _instance.gameObject.AddComponent<UnityMainThreadDispatcher>();

                    // add callback for Editor Mode change
#if UNITY_EDITOR
                    IsEditorPlaying = EditorApplication.isPlaying;
                    EditorApplication.playmodeStateChanged += this.HandleEditorPlayModeChange;
#endif
                    AmazonHookedPlatformInfo.Instance.Init();

                }
                else
                {
                    if (this != _instance)
                    {
#if UNITY_EDITOR
                        DestroyImmediate(this.gameObject);
#else
                            Destroy (this.gameObject);
#endif
                    }
                }

            }
        }
        #endregion

        private static Thread _mainThread;
        /// <summary>
        /// Checks if the thread is a game/main/unity thread
        /// </summary>
        /// <returns>true if the thread is the game/main/unity thread, else false</returns>
        public static bool IsMainThread()
        {
            return Thread.CurrentThread.Equals(_mainThread);
        }

#if UNITY_EDITOR
        public static bool IsEditorPlaying
        {
            get;
            private set;
        }

        public static bool IsEditorPaused
        {
            get;
            private set;
        }

        private void HandleEditorPlayModeChange()
        {
            IsEditorPlaying = EditorApplication.isPlaying;
            IsEditorPaused = EditorApplication.isPaused;
        }
#endif
    }
}
