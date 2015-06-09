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
using System.Linq;
using System.Text;

namespace Amazon.Runtime
{
    public delegate void AmazonServiceCallback<TRequest, TResponse>(AmazonServiceResult<TRequest, TResponse> responseObject)
        where TRequest : AmazonWebServiceRequest
        where TResponse : AmazonWebServiceResponse;

    public class AmazonServiceResult<TRequest, TResponse>
        where TRequest : AmazonWebServiceRequest
        where TResponse : AmazonWebServiceResponse
    {
        public TRequest Request { get; internal set; }
        public TResponse Response { get; internal set; }
        public Exception Exception { get; internal set; }
        public object state { get; internal set; }

	public AmazonServiceResult(TRequest request, TResponse response, Exception exception, object state)
        {
            this.Request = request;
            this.Response = response;
            this.Exception = exception;
            this.state = state;
        }
    }

}
