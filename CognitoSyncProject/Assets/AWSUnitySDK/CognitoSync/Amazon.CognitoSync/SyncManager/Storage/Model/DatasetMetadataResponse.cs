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

using Amazon.Runtime;
using System;
namespace Amazon.CognitoSync.SyncManager
{
    public class DatasetMetadataResponse : AmazonCognitoResponse
    {
        private DatasetMetadata _metadata;

        public DatasetMetadata Metadata
        {
            get { return _metadata; }
            set
            {
                this._metadata = value;
            }
        }
    }
}
