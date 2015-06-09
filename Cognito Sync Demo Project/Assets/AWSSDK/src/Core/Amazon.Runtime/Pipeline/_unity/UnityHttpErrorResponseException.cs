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
/*
 * Copyright 2010-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// Unity specific exception for an HTTP error response.
    /// </summary>
    public class UnityHttpErrorResponseException : Exception
    {
        public IWebResponseData Response { get; set; }
        public UnityWebRequest Request { get; private set; }

        public UnityHttpErrorResponseException(UnityWebRequest request)
        {
            this.Request = request;
            this.Response = request.Response;
        }

        public UnityHttpErrorResponseException(string message, UnityWebRequest request) :
            base(message)
        {
            this.Request = request;
            this.Response = request.Response;
        }

        public UnityHttpErrorResponseException(string message, Exception innerException, UnityWebRequest request) :
            base(message,innerException)
        {
            this.Request = request;
            this.Response = request.Response;
        }

    }
}
