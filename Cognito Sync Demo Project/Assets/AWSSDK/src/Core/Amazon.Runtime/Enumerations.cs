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

namespace Amazon.Runtime
{
    /// <summary>
    /// The valid hashing algorithm supported by the sdk for request signing.
    /// </summary>
    public enum SigningAlgorithm 
    { 
        HmacSHA1, 
        HmacSHA256 
    };

    /// <summary>
    /// Which end of a request was responsible for a service error response.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// The sender was responsible for the error, i.e. the client
        /// request failed validation or was improperly formatted.
        /// </summary>
        Sender,
        /// <summary>
        /// The error occured within the service.
        /// </summary>
        Receiver,
        /// <summary>
        /// An unrecognized error type was returned.
        /// </summary>
        Unknown
    }
}
