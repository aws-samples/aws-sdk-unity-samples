/**
 * Copyright 2013-2014 Amazon.com, 
 * Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Amazon Software License (the "License"). 
 * You may not use this file except in compliance with the 
 * License. A copy of the License is located at
 * 
 *     http://aws.amazon.com/asl/
 * 
 * or in the "license" file accompanying this file. This file is 
 * distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 * CONDITIONS OF ANY KIND, express or implied. See the License 
 * for the specific language governing permissions and 
 * limitations under the License.
 */
using System;
using System.Text;

namespace Amazon.CognitoSync.SyncManager.Util
{
    public class StringUtils
    {

        private static readonly Encoding UTF_8 = Encoding.UTF8;

        /// <summary>
        /// Calculates the byte length of a UTF-8 encoded string. 0 if the string is null.
        /// </summary>
        /// <returns>byte length of a UTF-8 string in bytes, 0 if null.</returns>
        /// <param name="theString">tring to be computed</param>
        public static long Utf8ByteLength(string theString)
        {
            if (theString == null)
            {
                return 0;
            }
            return UTF_8.GetByteCount(theString);
        }

        /// <summary>
        /// Checks whether a String is empty.
        /// </summary>
        /// <returns><c>true</c> if the string is null or empty, <c>false</c>  otherwise</returns>
        /// <param name="theString">a string to check</param>
        public static bool IsEmpty(string theString)
        {
            return theString == null || theString.Trim().Length == 0;
        }

        /// <summary>
        /// Compares two Strings. Returns true if both are null or have the same
        /// string value.
        /// </summary>
        /// <param name="s1">first string, can be null</param>
        /// <param name="s2">second string, can be null</param>
        public static bool Equals(string s1, string s2)
        {
            if (s1 == null)
            {
                return s2 == null;
            }
            else
            {
                return s1.Equals(s2);
            }
        }
    }
}

