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

using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// This class is used to dispatch web requests using the WWW API and
    /// to call user callbacks on the main thread.
    /// </summary>
    public class UnityMainThreadDispatcher : MonoBehaviour
    {
        private Logger _logger;

        /// <summary>
        /// This method is called called when the script instance is
        /// being loaded.
        /// </summary>
        public void Awake()
        {
            _logger = Logger.GetLogger(this.GetType());
            // Call the method to process requests at a regular interval.
            // TODO : Perf testing to figure out appropriate value for interval.
            InvokeRepeating("ProcessRequests", 0.1f, 0.1f);
        }

        /// <summary>
        /// This method processes queued web requests and user callbacks.
        /// </summary>
        void ProcessRequests()
        {
            // TODO : The current implementation dequeues a single request and callback per cycle
            // to reduce the CPU utilization of the main thread by the UnityMainThreadDispatcher.
            // Need to do perf testing to to verify if this is the best approach.

            // Make a network call for queued requests on the main thread
            var request = UnityRequestQueue.Instance.DequeueRequest();
            if (request != null)
            {
                StartCoroutine(InvokeRequest(request));
            }

            // Invoke queued callbacks on the main thread
            var asyncResult = UnityRequestQueue.Instance.DequeueCallback();
            if (asyncResult != null && asyncResult.Action!=null)
            {
                try
                {
                    asyncResult.Action(asyncResult.Request, asyncResult.Response,
                        asyncResult.Exception, asyncResult.AsyncOptions);
                }
                catch (Exception exception)
                {
                    // Catch any unhandled exceptions from the user callback 
                    // and log it. 
                    _logger.Error(exception,
                        "An unhandled exception was thrown from the callback method {0}.",
                        asyncResult.Request.ToString());
                }
            }

            //Invoke queued main thread executions
            var mainThreadCallback = UnityRequestQueue.Instance.DequeueMainThreadOperation();
            if (mainThreadCallback != null)
            {
                try
                {
                    mainThreadCallback();
                }
                catch (Exception exception)
                {
                    // Catch any unhandled exceptions from the user callback 
                    // and log it. 
                    _logger.Error(exception,
                        "An unhandled exception was thrown from the callback method");
                }
            }

            //network updates
            //TODO: need to check for performance
            NetworkReachability _networkReachability = Application.internetReachability;
            if (OnRefresh != null)
            {
                OnRefresh(this, new NetworkStatusRefreshed(_networkReachability));
            }

        }

        /// <summary>
        /// Makes a single web request using the WWW API.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>IEnumerator which indicated if the operation is pending.</returns>
        IEnumerator InvokeRequest(UnityWebRequest request)
        {

#if UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
            // Versions before Unity 4.3 use a WWW constructor
            // where the headers parameter is a HashTable.

            var headerTable = new Hashtable();
            foreach (string headerkey in request.Headers.Keys)
                headerTable.Add(headerkey, request.Headers[headerkey]);

            // Fire the request            
            request.WwwRequest = new WWW(request.RequestUri.AbsoluteUri,
                request.RequestContent, headerTable);
#else
            // Fire the request            
            request.WwwRequest = new WWW(request.RequestUri.AbsoluteUri,
                request.RequestContent, request.Headers);
#endif
            yield return request.WwwRequest;
            request.Response = new UnityWebResponseData(request.WwwRequest);
            
            if (request.IsSync)
            {
                // For synchronous calls, signal the wait handle 
                // so that the calling thread which waits on the wait handle
                // is unblocked.
                if(!request.Response.IsSuccessStatusCode)
                    request.Exception = new UnityHttpErrorResponseException(request);
                
                request.WaitHandle.Set();
            }
            else
            {
		    
                if (!request.Response.IsSuccessStatusCode)
            	{
                	var executionContext = request.AsyncResult.AsyncState as IAsyncExecutionContext;
                	executionContext.ResponseContext.AsyncResult.Exception = new UnityHttpErrorResponseException(request);
            	}
                // For asychronous calls invoke the callback method with the
                // state object that was originally passed in.

                // Invoke the callback method for the request on the thread pool
                // after the web request is executed. This callback triggers the 
                // post processing of the response from the server.
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    try
                    {
                        request.Callback(request.AsyncResult);
                    }
                    catch (Exception exception)
                    {
                        // The callback method (HttpHandler.GetResponseCallback) and 
                        // subsequent calls to handler callbacks capture any exceptions
                        // thrown from the runtime pipeline during post processing.

                        // Log the exception, in case we get an unhandled exception 
                        // from the callback.
                        _logger.Error(exception,
                            "An exception was thrown from the callback method executed from" +
                            "UnityMainThreadDispatcher.InvokeRequest method.");

                    }
                });
            }
        }

        /// Public callback API to send the status of network.
        /// </summary>
        public class NetworkStatusRefreshed : EventArgs
        {
            public NetworkReachability NetworkReachability;

            public NetworkStatusRefreshed(NetworkReachability reachability)
            {
                NetworkReachability = reachability;
            }

        }
        public static event EventHandler<NetworkStatusRefreshed> OnRefresh;


    }
}
