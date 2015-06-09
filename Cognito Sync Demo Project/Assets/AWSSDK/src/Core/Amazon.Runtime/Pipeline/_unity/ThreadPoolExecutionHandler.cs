#define AWS_APM_API
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
using Amazon.Runtime.Internal.Util;

namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// This handler executes the subsequent handlers on a ThreadPool thread.
    /// This feature is only implemented for APM based async methods.
    /// </summary>
    public class ThreadPoolExecutionHandler : PipelineHandler
    {
        private static ThreadPoolThrottler<IAsyncExecutionContext> _throttler;
        private static object _lock = new object();

        public ThreadPoolExecutionHandler(int concurrentRequests)
        {
            lock (_lock)
            {
                if (_throttler == null)
                    _throttler = new ThreadPoolThrottler<IAsyncExecutionContext>(concurrentRequests);
            }
        }

#if AWS_APM_API
        public override IAsyncResult InvokeAsync(IAsyncExecutionContext executionContext)
        {
            if (UnityInitializer.IsMainThread())
            {
                _throttler.Enqueue(executionContext, InvokeAsyncHelper, ErrorCallback);
                return null;
            }
            else
            {
                return base.InvokeAsync(executionContext);
            }
        }
#endif
        void InvokeAsyncHelper(IAsyncExecutionContext executionContext)
        {
            base.InvokeAsync(executionContext);
        }

        void ErrorCallback(Exception exception, IAsyncExecutionContext executionContext)
        {
            // Handle the exception by logging it and setting the exception on the context,
            // so that the exception is visible to the caller
            this.Logger.Error(exception, "An exception of type {0} was thrown from InvokeAsyncCallback().",
                    exception.GetType().Name);
            executionContext.RequestContext.Metrics.AddProperty(Metric.Exception, exception);

            // An unhandled exception occured in the callback implementation.
            // Capture the exception and end the callback processing by signalling the
            // wait handle.
            executionContext.RequestContext.Metrics.StopEvent(Metric.ClientExecuteTime);
            LogMetrics(ExecutionContext.CreateFromAsyncContext(executionContext));

            executionContext.ResponseContext.AsyncResult =
                        new RuntimeAsyncResult(executionContext.RequestContext.Callback,
                            executionContext.RequestContext.State);

            executionContext.ResponseContext.AsyncResult.Exception = exception;
            executionContext.ResponseContext.AsyncResult.AsyncOptions = executionContext.RequestContext.AsyncOptions;
            executionContext.ResponseContext.AsyncResult.Action = executionContext.RequestContext.Action;
            executionContext.ResponseContext.AsyncResult.Request = executionContext.RequestContext.OriginalRequest;
            executionContext.ResponseContext.AsyncResult.InvokeCallback();

        }

    }

}
