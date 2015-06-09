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
using System.Collections.Generic;

namespace Amazon.Runtime.Internal
{
	   /// <summary>
    /// A singleton which stores the list of pending web requests and pending callbacks.
    /// A singleton is used to maintain this state globally across multiple Unity scenes.
    /// </summary>
    public sealed class UnityRequestQueue
    {
        private static readonly UnityRequestQueue _instance = new UnityRequestQueue();

        private static readonly object _requestsLock = new object();
        private static readonly object _callbacksLock = new object();
        private static readonly object _mainThreadCallbackLock = new object();

        private Queue<UnityWebRequest> _requests = new Queue<UnityWebRequest>();
        private Queue<RuntimeAsyncResult> _callbacks = new Queue<RuntimeAsyncResult>();
        private Queue<Action> _mainThreadCallbacks = new Queue<Action>();
        /// <summary>
        /// The private contructor for the singleton class.
        /// </summary>
        private UnityRequestQueue() { }

        /// <summary>
        /// Returns a singleton instance of UnityRequestQueue.
        /// </summary>
        public static UnityRequestQueue Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Enqueues a request to be processed by the UnityMainThreadDispatcher.
        /// Unity 
        /// </summary>
        /// <param name="request">An instance of UnityWebRequest.</param>
        public void EnqueueRequest(UnityWebRequest request)
        {
            lock (_requestsLock)
            {
                _requests.Enqueue(request);
            }            
        }

        /// <summary>
        /// Dequeues a request from the queue of pending requests.
        /// </summary>
        /// <returns>An instance of UnityWebRequest.</returns>
        public UnityWebRequest DequeueRequest()
        {
            UnityWebRequest request = null;
            lock (_requestsLock)
            {
                if (_requests.Count > 0)
                {
                    request = _requests.Dequeue();
                }
            }
            return request;
        }

        /// <summary>
        /// Enqueues a callback method that should be invoked on the
        /// main thread by the UnityMainThreadDispatcher.
        /// </summary>
        /// <param name="asyncResult">Instance of RuntimeAsyncResult that contains
        /// all the information about the callback.</param>
        public void EnqueueCallback(RuntimeAsyncResult asyncResult)
        {
            lock (_callbacksLock)
            {
                _callbacks.Enqueue(asyncResult);
            }
        }

        /// <summary>
        /// Dequeues an callback from the queue of pending callbacks that
        /// need to be invoked.
        /// </summary>
        /// <returns></returns>
        public RuntimeAsyncResult DequeueCallback()
        {
            RuntimeAsyncResult asyncResult = null;
            lock (_callbacksLock)
            {
                if (_callbacks.Count > 0)
                {
                    asyncResult = _callbacks.Dequeue();
                }
            }
            return asyncResult;
        }

        /// <summary>
        /// Enqueue an anonymous method on main thread
        /// </summary>
        /// <param name="action"></param>
        public void ExecuteOnMainThread(Action action) 
        { 
            lock(_mainThreadCallbackLock)
            {
                _mainThreadCallbacks.Enqueue(action);
            }
        }

        public Action DequeueMainThreadOperation()
        {
            Action action = null;
            lock(_mainThreadCallbackLock)
            {
                if (_mainThreadCallbacks.Count > 0)
                {
                    action = _mainThreadCallbacks.Dequeue();
                }
            }
            return action;
        }

    }

}
