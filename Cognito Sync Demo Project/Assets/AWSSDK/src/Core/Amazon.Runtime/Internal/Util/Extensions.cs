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
using Amazon.Util;
/*
 * Copyright 2010-2013 Amazon.com, Inc. or its affiliates. All Rights Reserved.
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
using System.Diagnostics;
using System.Text;

#if WIN_RT
using Amazon.MissingTypes;
#endif

namespace Amazon.Runtime.Internal.Util
{
    public static partial class Extensions
    {
        private static readonly long ticksPerSecond = TimeSpan.FromSeconds(1).Ticks;
        private static readonly double tickFrequency = ticksPerSecond / (double)Stopwatch.Frequency;
        public static long GetElapsedDateTimeTicks(this Stopwatch self)
        {
            double stopwatchTicks = self.ElapsedTicks;
            long ticks = (long)(stopwatchTicks * tickFrequency);
            return ticks;
        }

        /// <summary>
        /// Returns true if the Content is set or there are
        /// query parameters.
        /// </summary>
        /// <param name="request">This request</param>
        /// <returns>True if data is present; false otherwise.</returns>
        public static bool HasRequestData(this IRequest request)
        {
            if (request == null)
                return false;

            if (request.ContentStream != null || request.Content != null)
                return true;

            if (request.Parameters.Count > 0)
                return true;

            return false;
        }
    }
}
