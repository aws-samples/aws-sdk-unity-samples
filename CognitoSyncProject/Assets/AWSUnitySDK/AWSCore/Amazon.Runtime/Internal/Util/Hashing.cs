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
using System.Collections.Generic;


namespace Amazon.Runtime.Internal.Util
{
    public static class Hashing
    {
        /// <summary>
        /// Hashes a set of objects.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Hash(params object[] value)
        {
            int result = 0;
            foreach (object item in value)
            {
                int hash = (item == null ? 0 : item.GetHashCode());
                result = CombineHashesInternal(result, hash);
            }
            return result;
        }

        /// <summary>
        /// Combines a set of hashses.
        /// </summary>
        /// <param name="hashes"></param>
        /// <returns></returns>
        public static int CombineHashes(params int[] hashes)
        {
            int result = 0;
            foreach (int hash in hashes)
            {
                result = CombineHashesInternal(result, hash);
            }
            return result;
        }

        /// <summary>
        /// Combines two hashes.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int CombineHashesInternal(int a, int b)
        {
            return unchecked(((a << 5) + a) ^ b);
        }
    }
}
