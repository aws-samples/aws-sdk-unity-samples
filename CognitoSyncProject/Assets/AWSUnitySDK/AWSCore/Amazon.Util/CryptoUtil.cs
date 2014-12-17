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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Amazon.Runtime;

namespace Amazon.Util
{
    internal static partial class CryptoUtilFactory
    {
        static CryptoUtil util = new CryptoUtil();

        internal static ICryptoUtil CryptoInstance
        {
            get { return util; }
        }


        partial class CryptoUtil : ICryptoUtil
        {
            internal CryptoUtil()
            {
            }
        }
    }
}
