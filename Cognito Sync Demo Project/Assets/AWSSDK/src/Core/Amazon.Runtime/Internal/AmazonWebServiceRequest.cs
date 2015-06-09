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
using System.Text;

using Amazon.Runtime.Internal;

namespace Amazon.Runtime
{
    public abstract partial class AmazonWebServiceRequest : IAmazonWebServiceRequest
    {
        /// <summary>
        /// This flag specifies if SigV4 will be used for the current request.
        /// </summary>
        bool IAmazonWebServiceRequest.UseSigV4
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets a value indicating if "Expect: 100-continue" HTTP header will be 
        /// sent by the client for this request. The default value is false.
        /// </summary>
        protected virtual bool Expect100Continue
        {
            get { return false; }            
        }

        internal bool GetExpect100Continue()
        {
            return this.Expect100Continue;
        }

        protected virtual bool IncludeSHA256Header
        {
            get { return true; }
        }

        internal bool GetIncludeSHA256Header()
        {
            return this.IncludeSHA256Header;
        }
    }
}
