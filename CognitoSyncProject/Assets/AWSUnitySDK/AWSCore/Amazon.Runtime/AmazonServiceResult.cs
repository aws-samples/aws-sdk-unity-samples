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

namespace Amazon.Runtime
{
    /// <summary>
    /// API callback delegate passes AmazonServiceResult to the callback delegate
    /// </summary>
    public delegate void AmazonServiceCallback (AmazonServiceResult result);
    
    /// <summary>
    /// Container for callback response, contains Request, Response, Exception & state objects
    /// </summary>
    public class AmazonServiceResult
    {
        /// <summary>
        /// Original unchanged request object used while invoking the requestz`
        /// </summary>
        /// <value>The request.</value>
        public AmazonWebServiceRequest Request { get; internal set; }
        
        public AmazonWebServiceResponse Response { get; internal set; }
        
        public Exception Exception { get; internal set; }
        
        public object State { get; internal set; }
        
        internal AmazonServiceResult (AmazonWebServiceRequest request, object state)
        {
            this.Request = request;
            this.State = state;
        }
        
        internal AmazonServiceResult (AmazonWebServiceRequest request, AmazonWebServiceResponse response, 
                                     Exception exception, object state)
        {
            this.Request = request;
            this.Response = response;
            this.Exception = exception;
            this.State = state;
        }
    }
}
