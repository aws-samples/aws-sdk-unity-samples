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
using System.Linq;
using System.Text;

namespace Amazon.Runtime.Internal.Util
{
    internal class UnityDebugLogger : InternalLogger
    {

        public UnityDebugLogger(Type declaringType)
            : base(declaringType)
        {
        }

        public override void Flush()
        {
            //flush not supported on unity debug logger
        }

        public override void Error(Exception exception, string messageFormat, params object[] args)
        {
            try
            {
                if (exception != null)
                    UnityEngine.Debug.LogError(exception.Message);

                if (!string.IsNullOrEmpty(messageFormat))
                    UnityEngine.Debug.LogError(string.Format(messageFormat, args));
            }
            catch
            {
                //suppress exception for now
            }
        }

        public override void Debug(Exception exception, string messageFormat, params object[] args)
        {
            try
            {
                if (exception != null)
                    UnityEngine.Debug.Log(exception.Message);

                if (!string.IsNullOrEmpty(messageFormat))
                    UnityEngine.Debug.Log(string.Format(messageFormat, args));
            }
            catch
            {
                //suppress exceptions
            }
        }

        public override void DebugFormat(string messageFormat, params object[] args)
        {
            try
            {
                if (!string.IsNullOrEmpty(messageFormat))
                    UnityEngine.Debug.Log(string.Format(messageFormat, args));
            }
            catch
            {
                //suppress exceptions
            }
        }

        public override void InfoFormat(string message, params object[] arguments)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                    UnityEngine.Debug.Log(string.Format(message, arguments));
            }
            catch
            {
                //suppress exceptions
            }
        }

        public override bool IsDebugEnabled { get { return true; } }
        //always enable error
        public override bool IsErrorEnabled { get { return true; } }

        public override bool IsInfoEnabled { get { return true; } }


    }
}
