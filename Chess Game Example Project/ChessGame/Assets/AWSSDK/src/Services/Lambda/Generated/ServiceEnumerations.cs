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

/*
 * Do not modify this file. This file is generated from the lambda-2015-03-31.normal.json service model.
 */

using System;

using Amazon.Runtime;

namespace Amazon.Lambda
{

    /// <summary>
    /// Constants used for properties of type EventSourcePosition.
    /// </summary>
    public class EventSourcePosition : ConstantClass
    {

        /// <summary>
        /// Constant LATEST for EventSourcePosition
        /// </summary>
        public static readonly EventSourcePosition LATEST = new EventSourcePosition("LATEST");
        /// <summary>
        /// Constant TRIM_HORIZON for EventSourcePosition
        /// </summary>
        public static readonly EventSourcePosition TRIM_HORIZON = new EventSourcePosition("TRIM_HORIZON");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventSourcePosition(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static EventSourcePosition FindValue(string value)
        {
            return FindValue<EventSourcePosition>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator EventSourcePosition(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type InvocationType.
    /// </summary>
    public class InvocationType : ConstantClass
    {

        /// <summary>
        /// Constant DryRun for InvocationType
        /// </summary>
        public static readonly InvocationType DryRun = new InvocationType("DryRun");
        /// <summary>
        /// Constant Event for InvocationType
        /// </summary>
        public static readonly InvocationType Event = new InvocationType("Event");
        /// <summary>
        /// Constant RequestResponse for InvocationType
        /// </summary>
        public static readonly InvocationType RequestResponse = new InvocationType("RequestResponse");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public InvocationType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static InvocationType FindValue(string value)
        {
            return FindValue<InvocationType>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator InvocationType(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type LogType.
    /// </summary>
    public class LogType : ConstantClass
    {

        /// <summary>
        /// Constant None for LogType
        /// </summary>
        public static readonly LogType None = new LogType("None");
        /// <summary>
        /// Constant Tail for LogType
        /// </summary>
        public static readonly LogType Tail = new LogType("Tail");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public LogType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static LogType FindValue(string value)
        {
            return FindValue<LogType>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator LogType(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type Runtime.
    /// </summary>
    public class Runtime : ConstantClass
    {

        /// <summary>
        /// Constant Java8 for Runtime
        /// </summary>
        public static readonly Runtime Java8 = new Runtime("java8");
        /// <summary>
        /// Constant Nodejs for Runtime
        /// </summary>
        public static readonly Runtime Nodejs = new Runtime("nodejs");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Runtime(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static Runtime FindValue(string value)
        {
            return FindValue<Runtime>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator Runtime(string value)
        {
            return FindValue(value);
        }
    }

}