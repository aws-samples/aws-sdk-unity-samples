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


namespace Amazon.Runtime
{
    /// <summary>
    /// Base class for request used by some of the services.
    /// </summary>
    public abstract partial class AmazonWebServiceRequest
    {
        internal event RequestEventHandler BeforeRequestEvent;
        internal EventHandler<StreamTransferProgressArgs> StreamUploadProgressCallback;

        private Dictionary<string, object> requestState = null;
        internal Dictionary<string, object> RequestState
        {
            get
            {
                if (requestState == null)
                {
                    requestState = new Dictionary<string, object>();
                }
                return requestState;
            }
        }

        protected AmazonWebServiceRequest()
        {
        }

        internal AmazonWebServiceRequest WithBeforeRequestHandler(RequestEventHandler handler)
        {
            BeforeRequestEvent += handler;
            return this;
        }

        internal void FireBeforeRequestEvent(object sender, RequestEventArgs args)
        {
            if (BeforeRequestEvent != null)
                BeforeRequestEvent(sender, args);
        }
    }
}
