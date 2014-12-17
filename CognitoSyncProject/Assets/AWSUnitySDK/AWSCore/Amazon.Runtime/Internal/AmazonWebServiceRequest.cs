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

using Amazon.Runtime.Internal;

namespace Amazon.Runtime
{
    public abstract partial class AmazonWebServiceRequest : IRequestEvents
    {
        /// <summary>
        /// Gest or Sets a value indicating if "Expect: 100-continue" HTTP header will be 
        /// sent by the client for this request. The default value is false.
        /// </summary>
        internal virtual bool Expect100Continue
        {
            get { return false; }            
        }

        internal virtual bool IncludeSHA256Header
        {
            get { return true; }
        }

        void IRequestEvents.AddBeforeRequestHandler(RequestEventHandler handler)
        {
            ((AmazonWebServiceRequest)this).WithBeforeRequestHandler(handler);
        }

        void IRequestEvents.FireBeforeRequestEvent(object sender, RequestEventArgs args)
        {
            ((AmazonWebServiceRequest)this).FireBeforeRequestEvent(sender, args);
        }
    }
}
