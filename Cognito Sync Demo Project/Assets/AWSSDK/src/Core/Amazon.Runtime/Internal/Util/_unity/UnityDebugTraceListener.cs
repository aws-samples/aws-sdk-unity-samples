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
using System.Diagnostics;

namespace Amazon.Runtime.Internal.Util
{
    public class UnityDebugTraceListener : TraceListener
    {
        public UnityDebugTraceListener() : base() { }

        public UnityDebugTraceListener(string name)
            : base(name) { }

        public override void Write(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public override void WriteLine(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            LogMessage(string.Format(format, args), eventType);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            LogMessage(data.ToString(),eventType);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            LogMessage(message,eventType);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            foreach (object o in data)
            {
                if(o!=null)
                    LogMessage(o.ToString(),eventType);
            }
        }

        public override void Fail(string message)
        {
            UnityEngine.Debug.LogError(message);
        }

        public override void Fail(string message, string detailMessage)
        {
            UnityEngine.Debug.LogError(message + " " + detailMessage);
        }

        public override void Write(object o)
        {
            UnityEngine.Debug.Log(o.ToString());
        }

        public override void WriteLine(object o)
        {
            UnityEngine.Debug.Log(o.ToString());
        }

        public override void WriteLine(object o, string category)
        {
            UnityEngine.Debug.Log(o.ToString());
        }

        public override bool IsThreadSafe
        {
            get
            {
                return true ;
            }
        }

        private void LogMessage(string message, TraceEventType eventType)
        {
            if (eventType.Equals(TraceEventType.Critical) || eventType.Equals(TraceEventType.Error))
            {
                UnityEngine.Debug.LogError(message);
            }
            else if (eventType.Equals(TraceEventType.Warning))
            {
                UnityEngine.Debug.LogWarning(message);
            }
            else
            {
                UnityEngine.Debug.Log(message);
            }
        }

    }

}
