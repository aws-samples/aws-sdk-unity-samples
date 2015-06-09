#define AWSSDK_UNITY
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

using Amazon.Runtime.Internal.Util;
using System;
using System.Threading;

namespace Amazon.Runtime.Internal
{
    public class RuntimeAsyncResult : IAsyncResult, IDisposable
    {
        private object _lockObj;
        private ManualResetEvent _waitHandle;
        private bool _disposed = false;
        private bool _callbackInvoked = false;

        private Logger _logger;

        public RuntimeAsyncResult(AsyncCallback asyncCallback, object asyncState)
        {
            _lockObj = new object();
            _callbackInvoked = false;

            this.AsyncState = asyncState;
            this.IsCompleted = false;
            this.AsyncCallback = asyncCallback;
            this.CompletedSynchronously = false;

            this._logger = Logger.GetLogger(this.GetType());
        }

        public AsyncCallback AsyncCallback { get; private set; }

        public object AsyncState { get; private set; }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get
            {
                if (this._waitHandle != null)
                {
                    return this._waitHandle;
                }

                lock (this._lockObj)
                {
                    if (this._waitHandle == null)
                    {
                        this._waitHandle = new ManualResetEvent(this.IsCompleted);
                    }
                }
                return this._waitHandle;
            }
        }

        public bool CompletedSynchronously { get; private set; }

        public bool IsCompleted { get; private set; }

        public Exception Exception { get; set; }

        public AmazonWebServiceResponse Response { get; set; }

#if AWSSDK_UNITY

        public AmazonWebServiceRequest Request { get; set; }

        public Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> Action { get; set; }

        public AsyncOptions AsyncOptions { get; set; }

#endif

        internal void SignalWaitHandle()
        {
            this.IsCompleted = true;
            if (this._waitHandle != null)
            {
                this._waitHandle.Set();
            }
        }

        internal void HandleException(Exception exception)
        {
            this.Exception = exception;
            InvokeCallback();
        }

        public void InvokeCallback()
        {
            this.SignalWaitHandle();
            if (!_callbackInvoked)
            {
                _callbackInvoked = true;
                try {
#if AWSSDK_UNITY
                    if (this.AsyncOptions.ExecuteCallbackOnMainThread)
                    {
                        // Enqueue the callback so that the Unity main thread dispatcher 
                        // can invoke the callback on the main thread.
                        UnityRequestQueue.Instance.EnqueueCallback(this);
                    }
                    else
                    {
                        // Invoke the callback on current (background) thread
                        if (this.Action != null)
                        {
                            this.Action(this.Request, this.Response,
                                this.Exception, this.AsyncOptions);
                        }
                    }
#else
                    if(this.AsyncCallback != null)
                        this.AsyncCallback(this);
#endif
                } catch (Exception e)
                {
                    _logger.Error(e, "Exception in user callback");
                }
            }
        }

        #region Dispose Pattern Implementation

        /// <summary>
        /// Implements the Dispose pattern
        /// </summary>
        /// <param name="disposing">Whether this object is being disposed via a call to Dispose
        /// or garbage collected.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing && _waitHandle != null)
                {
#if WIN_RT
                    _waitHandle.Dispose();
#else
                    _waitHandle.Close();
#endif
                    _waitHandle = null;
                }
                this._disposed = true;
            }
        }

        /// <summary>
        /// Disposes of all managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
