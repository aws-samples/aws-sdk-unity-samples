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

using Amazon.Runtime;

namespace Amazon.Util
{
    public static partial class CryptoUtilFactory
    {
        static CryptoUtil util = new CryptoUtil();

        public static ICryptoUtil CryptoInstance
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
