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
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amazon.DynamoDBv2.DataModel
{
    public partial class S3Link
    {
        #region Upload/PutObject

        /// <summary>
        /// Initiates the asynchronous execution of the UploadFrom operation.
        /// </summary>
        /// <param name="sourcePath">Path of the file to be uploaded.</param>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void UploadFromAsync(string sourcePath, AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () =>
                {
                    this.s3ClientCache.GetClient(this.RegionAsEndpoint).UploadObjectFromFilePath(
                        this.linker.s3.bucket, this.linker.s3.key, sourcePath, null);
                },
                asyncOptions,
                callback);
        }
        
        #endregion

        #region Download/GetObject

        /// <summary>
        /// Initiates the asynchronous execution of the DownloadTo operation.
        /// </summary>
        /// <param name="downloadPath">Path to save the file.</param>
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void DownloadToAsync(string downloadPath, AmazonDynamoDBCallback callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync(
                () =>
                {
                    this.s3ClientCache.GetClient(this.RegionAsEndpoint).DownloadToFilePath(
                this.linker.s3.bucket, this.linker.s3.key, downloadPath, null);
                },
                asyncOptions,
                callback);
        }

        #endregion
    }
}
