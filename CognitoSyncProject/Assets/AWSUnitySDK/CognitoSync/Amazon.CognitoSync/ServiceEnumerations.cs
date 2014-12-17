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

using Amazon.Runtime;

namespace Amazon.CognitoSync
{

    /// <summary>
    /// Constants used for properties of type Operation.
    /// </summary>
    public class Operation : ConstantClass
    {

        /// <summary>
        /// Constant Remove for Operation
        /// </summary>
        public static readonly Operation Remove = new Operation("remove");
        /// <summary>
        /// Constant Replace for Operation
        /// </summary>
        public static readonly Operation Replace = new Operation("replace");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Operation(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static Operation FindValue(string value)
        {
            return FindValue<Operation>(value);
        }

        public static implicit operator Operation(string value)
        {
            return FindValue(value);
        }
    }

}