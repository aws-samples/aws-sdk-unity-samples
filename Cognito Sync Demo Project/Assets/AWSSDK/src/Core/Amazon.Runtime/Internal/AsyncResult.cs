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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;

#if (WIN_RT || WINDOWS_PHONE)
using Amazon.MissingTypes;
#endif

namespace Amazon.Runtime.Internal
{
    public class AsyncResult : IAsyncResult, IRequestData, IDisposable
    {
        // Private members

        private ManualResetEvent _waitHandle;
        private object _lockObj;
        private Stopwatch _stopWatch;
        private bool _callbackInvoked = false;
        private bool _disposed = false;


        // Constructor
        internal AsyncResult(IRequest request, AsyncCallback callback, object state, bool completeSynchronized, AbstractAWSSigner signer, ResponseUnmarshaller unmarshaller)
        {
            this.Request = request;
            this.Callback = callback;
            this.State = state;
            this.CompletedSynchronously = completeSynchronized;
            this.Signer = signer;
            this.Unmarshaller = unmarshaller;
            this.RequestName = request.OriginalRequest.GetType().Name;
            this.Metrics = new RequestMetrics();

            this._lockObj = new object();

            this._stopWatch = Stopwatch.StartNew();
            this._stopWatch.Start();
        }


        // Properties

        internal Exception Exception { get; set; }

        public int RetriesAttempt { get; set; }

        internal AsyncRequestState RequestState { get; set; }

        internal object FinalResponse { get; set; }

        // Read-only properties

        public ResponseUnmarshaller Unmarshaller { get; private set; }

        public IRequest Request { get; private set; }

        internal AsyncCallback Callback { get; private set; }

        public AbstractAWSSigner Signer { get; private set; }

        internal object State { get; private set; }

        internal string RequestName { get; private set; }

        public RequestMetrics Metrics { get; private set; }

        public bool CompletedSynchronously { get; private set; }

        public bool IsCompleted { get; private set; }

        public object AsyncState
        {
            get { return this.State; }
        }

        public WaitHandle AsyncWaitHandle
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


        // Methods
        internal void SetCompletedSynchronously(bool completedSynchronously)
        {
            this.CompletedSynchronously = completedSynchronously;
        }

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
            this.Metrics.AddProperty(Metric.Exception, exception);
            InvokeCallback();            
        }

        internal void InvokeCallback()
        {
            this.SignalWaitHandle();
            if (!_callbackInvoked && this.Callback!=null)
            {
                _callbackInvoked = true;
                this.Callback(this);   
            }
        }

        #region Metric properties

        private Guid id = Guid.NewGuid();
        internal Guid Id { get { return this.id; } }

        #endregion

        public class AsyncRequestState
        {
            public AsyncRequestState(HttpWebRequest webRequest, byte[] requestData, Stream requestStream)
            {
                this.WebRequest = webRequest;
                this.RequestData = requestData;
                this.RequestStream = requestStream;
            }

            internal HttpWebRequest WebRequest { get; private set; }

            internal byte[] RequestData { get; private set; }
            internal Stream RequestStream { get; private set; }


            internal bool GetRequestStreamCallbackCalled { get; set; }

            internal bool GetResponseCallbackCalled { get; set; }
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
