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
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Amazon.CognitoIdentity
{
    internal class CognitoIdentityAsyncExecutor
    {

        private static Logger Logger = Logger.GetLogger(typeof(CognitoIdentityAsyncExecutor));


        public static void ExecuteAsync<T>(Func<T> function, AsyncOptions options, AmazonCognitoIdentityCallback<T> callback)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                T result = default(T);
                Exception exception = null;
                try
                {
                    result = function();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                if (callback != null)
                {
                    if (options.ExecuteCallbackOnMainThread)
                    {
                        UnityRequestQueue.Instance.ExecuteOnMainThread(() =>
                        {
                            callback(new AmazonCognitoIdentityResult<T>(result, exception, options.State));
                        });
                    }
                    else
                    {
                        try
                        {
                            callback(new AmazonCognitoIdentityResult<T>(result, exception, options.State));
                        }
                        catch (Exception callbackException)
                        {
                            // Catch any unhandled exceptions from the user callback 
                            // and log it. 
                            Logger.Error(callbackException,
                                "An unhandled exception was thrown from the callback method {0}.",
                                callback.Method.Name);
                        }
                    }
                }
            });
        }
    }
}
