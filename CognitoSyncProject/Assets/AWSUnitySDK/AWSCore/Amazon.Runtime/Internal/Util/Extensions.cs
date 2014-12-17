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

#if WIN_RT
using Amazon.MissingTypes;
#endif

namespace Amazon.Runtime.Internal.Util
{
    internal static partial class Extensions
    {
        private static readonly long ticksPerSecond = TimeSpan.FromSeconds(1).Ticks;
        private static readonly double tickFrequency = ticksPerSecond / (double)Stopwatch.Frequency;
        public static long GetElapsedDateTimeTicks(this Stopwatch self)
        {
            double stopwatchTicks = self.ElapsedTicks;
            long ticks = (long)(stopwatchTicks * tickFrequency);
            return ticks;
        }
    }
}
