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
using System.Text;

namespace Amazon.Auth.AccessControlPolicy.Internal
{
    internal static class JsonDocumentFields
    {
        internal const string VERSION = "Version";
        internal const string POLICY_ID = "Id";
        internal const string STATEMENT = "Statement";
        internal const string STATEMENT_EFFECT = "Effect";
        internal const string EFFECT_VALUE_ALLOW = "Allow";
        internal const string STATEMENT_ID = "Sid";
        internal const string PRINCIPAL = "Principal";
        internal const string ACTION = "Action";
        internal const string RESOURCE = "Resource";
        internal const string CONDITION = "Condition";

    }
}
