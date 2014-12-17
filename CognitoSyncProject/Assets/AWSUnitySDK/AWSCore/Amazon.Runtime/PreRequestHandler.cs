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
using System.Net;
using System.Collections.Specialized;

namespace Amazon.Runtime
{
    public class PreRequestEventArgs : EventArgs
    {
        #region Constructor

        protected PreRequestEventArgs() { }
        
        #endregion

        #region Properties

        public AmazonWebServiceRequest Request { get; protected set; }

        #endregion

        #region Creator method

        internal static PreRequestEventArgs Create(AmazonWebServiceRequest request)
        {
            PreRequestEventArgs args = new PreRequestEventArgs
            {
                Request = request
            };
            return args;
        }

        #endregion
    }

    public delegate void PreRequestEventHandler(object sender, PreRequestEventArgs args);
}
