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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amazon.DynamoDBv2.DataModel
{
    public partial class S3Link
    {

        /// <summary>
        /// If enable is set to true the object will have its permission set to PublicRead otherwise the permissions will be set to Private.
        /// </summary>
        /// <param name="enable">If true the object will have its permission set to PublicRead otherwise the permissions will be set to Private.</param>
        public void MakeS3ObjectPublic(bool enable)
        {
            this.s3ClientCache.GetClient(this.RegionAsEndpoint).MakeObjectPublic(this.linker.s3.bucket, this.linker.s3.key, enable);
        }


        #region Upload/PutObject

        /// <summary>
        /// Uploads the specified file and stores it in the specified bucket with the provided key from construction.
        /// </summary>
        /// <param name="sourcePath">Path of the file to be uploaded.</param>
        public void UploadFrom(string sourcePath)
        {
            this.s3ClientCache.GetClient(this.RegionAsEndpoint).UploadObjectFromFilePath(this.linker.s3.bucket, this.linker.s3.key, sourcePath, null);
        }

        #endregion

        #region Download/GetObject

        /// <summary>
        /// Downloads the file from the S3Link's specified bucket and key then saves it in the given path. 
        /// Creates directories and the file if they do not already exist.
        /// </summary>
        /// <param name="downloadPath">Path to save the file.</param>
        public void DownloadTo(string downloadPath)
        {
            this.s3ClientCache.GetClient(this.RegionAsEndpoint).DownloadToFilePath(this.linker.s3.bucket, this.linker.s3.key, downloadPath, null);
        }

        #endregion

    }
}
