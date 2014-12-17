/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using Amazon.Unity;

#if (WIN_RT || WINDOWS_PHONE)
using Amazon.MissingTypes;
#endif

namespace Amazon.Runtime.Internal
{
	public class AsyncResult : IAsyncResult, IRequestData, IDisposable
	{
		// Private members
		
		//private ManualResetEvent _waitHandle;
		//private object _lockObj;
		private Stopwatch _stopWatch;
		private bool _callbackInvoked = false;
		private bool _disposed = false;
		
		// Constructor
		internal AsyncResult(IRequest request, AmazonWebServiceRequest originalRequest, AmazonServiceCallback callback, object state, AbstractAWSSigner signer, ResponseUnmarshaller unmarshaller)
		{
			this.Request = request;
			this.Callback = callback;
			//this.State = state;
			//this.CompletedSynchronously = completeSynchronized;
			this.Signer = signer;
			this.Unmarshaller = unmarshaller;
			
			if (request != null)
				this.RequestName = request.OriginalRequest.GetType().Name;
			
			this.Metrics = new RequestMetrics();
			
			//this._lockObj = new object();
			
			this._stopWatch = Stopwatch.StartNew();
			this._stopWatch.Start();
			this.ServiceResult = new AmazonServiceResult (originalRequest, state);
		}
		
		// Properties
		
		public Exception Exception { get; internal set; }
		
		public int RetriesAttempt { get; set; }
		
		//internal AsyncRequestState RequestState { get; set; }
		
		internal AmazonServiceResult ServiceResult { get; private set; }
		
		internal WWWRequestData RequestData { get; set; }
		
		internal WWWResponseData ResponseData { get; set; }
		
		internal WaitCallback WaitCallback { get; set; }
		
		// Read-only properties
		
		public ResponseUnmarshaller Unmarshaller { get; private set; }
		
		public IRequest Request { get; private set; }
		
		internal AmazonServiceCallback Callback { get; private set; }
		
		public AbstractAWSSigner Signer { get; private set; }
		
		internal string RequestName { get; private set; }
		
		public RequestMetrics Metrics { get; private set; }
		
		public bool CompletedSynchronously { get{ return false;} }
		
		public bool IsCompleted { get; set; }
		
		public object AsyncState
		{
			get { throw new NotSupportedException("AsyncState"); }
		}
		
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				throw new NotSupportedException("AsyncWaitHandle");
				/*
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
                */
			}
		}
		
		
		// Methods
		/*
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
        */
		internal void HandleException(Exception exception)
		{
			this.ServiceResult.Exception = (exception.GetType () == typeof(System.Net.WebException)
                                            && exception.GetType () != typeof(AmazonServiceResult)) ? 
                                new AmazonServiceException (exception) : exception;
			this.Metrics.AddProperty(Metric.Exception, exception);
			InvokeCallback();
		}
		
		internal void InvokeCallback()
		{
			//this.SignalWaitHandle();
			if (!_callbackInvoked && this.Callback != null)
			{
				_callbackInvoked = true;
				AmazonMainThreadDispatcher.ExecCallback(this.Callback, this.ServiceResult);
			}
		}
		
		#region Metric properties
		
		private Guid id = Guid.NewGuid();
		internal Guid Id { get { return this.id; } }
		
		#endregion
		/*
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
        */
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
				/*
                if (disposing && _waitHandle != null)
                {
#if WIN_RT
                    _waitHandle.Dispose();
#else
                    _waitHandle.Close();
#endif
                    _waitHandle = null;
                }
                */
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
