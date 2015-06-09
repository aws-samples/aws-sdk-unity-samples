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
using System.Globalization;
using System.Text;
using System.IO;

using Amazon.Util;

namespace Amazon.Runtime.Internal.Util
{
    /// <summary>
    /// Utilities for converting objects to strings. Used by the marshaller classes.
    /// </summary>
    public static class StringUtils
    {
        private static readonly Encoding UTF_8 = Encoding.UTF8;

        public static string FromString(String value) 
        {
            return value;
        }

        public static string FromString(ConstantClass value)
        {
            return value == null ? "" : value.Intern().Value;
        }

        public static string FromMemoryStream(MemoryStream value)
        {
            return Convert.ToBase64String(value.ToArray());
        }

        public static string FromInt(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
        
        public static string FromInt(int? value)
        {
            if (value != null)
                return value.Value.ToString(CultureInfo.InvariantCulture);

            return null;
        }

        public static string FromLong(long value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string FromBool(bool value)
        {
            return value.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
        }

        public static string FromDateTime(DateTime value)
        {
            return value.ToString(AWSSDKUtils.ISO8601DateFormat, CultureInfo.InvariantCulture);
        }

        public static string FromDouble(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static long Utf8ByteLength(string theString)
        {
            if (theString == null)
            {
                return 0;
            }
            return UTF_8.GetByteCount(theString);
        }
    }
}
