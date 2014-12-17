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
using System.Linq;
using System.Text;

using Amazon.Util;

namespace Amazon.Runtime.Internal
{
    internal class StreamReadTracker
    {
        AmazonWebServiceClient client;
        EventHandler<StreamTransferProgressArgs> callback;
        long contentLength;
        long totalBytesRead;
        long totalIncrementTransferred;

        internal StreamReadTracker(AmazonWebServiceClient client, EventHandler<StreamTransferProgressArgs> callback, long contentLength)
        {
            this.client = client;
            this.callback = callback;
            this.contentLength = contentLength;
        }

        public void ReadProgress(int bytesRead)
        {
            if (callback == null)
                return;

            // Invoke the progress callback only if bytes read > 0
            if (bytesRead > 0)
            {
                totalBytesRead += bytesRead;
                totalIncrementTransferred += bytesRead;

                if (totalIncrementTransferred >= this.client.Config.ProgressUpdateInterval ||
                    totalBytesRead == contentLength)
                {

                    AWSSDKUtils.InvokeInBackground(
                                        callback,
                                        new StreamTransferProgressArgs(totalIncrementTransferred, totalBytesRead, contentLength),
                                        client);
                    totalIncrementTransferred = 0;
                }
            }
        }
    }
}
