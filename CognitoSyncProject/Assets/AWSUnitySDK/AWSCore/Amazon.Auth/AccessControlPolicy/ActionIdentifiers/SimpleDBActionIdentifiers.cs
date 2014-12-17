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

namespace Amazon.Auth.AccessControlPolicy.ActionIdentifiers
{
    /// <summary>
    /// The available AWS access control policy actions for Amazon SimpleDB.
    /// </summary>
    /// <see cref="Amazon.Auth.AccessControlPolicy.Statement.Actions"/>
    public static class SimpleDBActionIdentifiers
    {
        public static readonly ActionIdentifier AllSimpleDBActions = new ActionIdentifier("sdb:*");

        public static readonly ActionIdentifier BatchDeleteAttributes = new ActionIdentifier("sdb:BatchDeleteAttributes");
        public static readonly ActionIdentifier BatchPutAttributes = new ActionIdentifier("sdb:BatchPutAttributes");
        public static readonly ActionIdentifier CreateDomain = new ActionIdentifier("sdb:CreateDomain");
        public static readonly ActionIdentifier DeleteAttributes = new ActionIdentifier("sdb:DeleteAttributes");
        public static readonly ActionIdentifier DeleteDomain = new ActionIdentifier("sdb:DeleteDomain");
        public static readonly ActionIdentifier DomainMetadata = new ActionIdentifier("sdb:DomainMetadata");
        public static readonly ActionIdentifier GetAttributes = new ActionIdentifier("sdb:GetAttributes");
        public static readonly ActionIdentifier ListDomains = new ActionIdentifier("sdb:ListDomains");
        public static readonly ActionIdentifier PutAttributes = new ActionIdentifier("sdb:PutAttributes");
        public static readonly ActionIdentifier Select = new ActionIdentifier("sdb:Select");
    }
}
