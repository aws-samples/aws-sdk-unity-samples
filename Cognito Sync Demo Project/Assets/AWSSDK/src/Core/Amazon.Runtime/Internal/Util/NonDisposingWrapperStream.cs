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
using System.IO;
using System.Linq;
using System.Text;

namespace Amazon.Runtime.Internal.Util
{
    /// <summary>
    /// A wrapper stream which supresses disposal of the underlying stream.
    /// </summary>
    public class NonDisposingWrapperStream : WrapperStream
    {
        /// <summary>
        /// Constructor for NonDisposingWrapperStream.
        /// </summary>
        /// <param name="baseStream">The base stream to wrap.</param>
        public NonDisposingWrapperStream(Stream baseStream) : base (baseStream)
        {
        }
#if !WIN_RT
        /// <summary>
        /// The Close implementation for this wrapper stream
        /// does not close the underlying stream.
        /// </summary>
        public override void Close()
        {
            // Suppress disposing the stream by not calling Close() on the base stream.            
        }
#endif
        /// <summary>
        /// The Dispose implementation for this wrapper stream
        /// does not close the underlying stream.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            // Suppress disposing the stream by not calling Dispose() on the base stream.            
        }
    }
}
