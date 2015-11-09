#define AWS_APM_API
#define AWSSDK_UNITY
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
#if AWS_ASYNC_API
using System.Threading.Tasks;
#endif

namespace Amazon.Runtime.SharedInterfaces
{
    /// <summary>
    /// ICoreAmazonS3 is not meant to use directly. It defines S3 with basic .NET types
    /// and allows other services to be able to use S3 as a runtime dependency. This interface
    /// is implemented by the AmazonS3Client defined in the S3 assembly.
    /// </summary>
    public interface ICoreAmazonS3
    {
        /// <summary>
        /// Generate a presigned URL.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="expiration"></param>
        /// <param name="additionalProperties"></param>
        /// <returns></returns>
        string GeneratePreSignedURL(string bucketName, string objectKey, DateTime expiration, IDictionary<string, object> additionalProperties);

#if BCL||AWSSDK_UNITY
        /// <summary>
        /// Get all the object keys for the particular bucket and key prefix.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="prefix"></param>
        /// <param name="additionalProperties"></param>
        /// <returns></returns>
        IList<string> GetAllObjectKeys(string bucketName, string prefix, IDictionary<string, object> additionalProperties);

        /// <summary>
        /// Delete the object.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="additionalProperties"></param>
        void Delete(string bucketName, string objectKey, IDictionary<string, object> additionalProperties);

        /// <summary>
        /// Deletes the ojects.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKeys"></param>
        /// <param name="additionalProperties"></param>
        void Deletes(string bucketName, IEnumerable<string> objectKeys, IDictionary<string, object> additionalProperties);


        /// <summary>
        /// Upload an object from a stream.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="stream"></param>
        /// <param name="additionalProperties"></param>
        void UploadObjectFromStream(string bucketName, string objectKey, Stream stream, IDictionary<string, object> additionalProperties);

        /// <summary>
        /// Upload an object from a file path.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="filepath"></param>
        /// <param name="additionalProperties"></param>
        void UploadObjectFromFilePath(string bucketName, string objectKey, string filepath, IDictionary<string, object> additionalProperties);

        /// <summary>
        /// Download object to a file path.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="filepath"></param>
        /// <param name="additionalProperties"></param>
        void DownloadToFilePath(string bucketName, string objectKey, string filepath, IDictionary<string, object> additionalProperties);

        /// <summary>
        /// Get stream for an object.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="additionalProperties"></param>
        /// <returns></returns>
        Stream GetObjectStream(string bucketName, string objectKey, IDictionary<string, object> additionalProperties);

        /// <summary>
        /// Set the ACL on the object to public readable.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="enable"></param>
        void MakeObjectPublic(string bucketName, string objectKey, bool enable);

        /// <summary>
        /// Check to see if the bucket exists and if it doesn't create the bucket.
        /// </summary>
        /// <param name="bucketName"></param>
        void EnsureBucketExists(string bucketName);

        /// <summary>
        /// Check to see if the bucket exists.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        bool DoesS3BucketExist(string bucketName);
#endif

#if AWS_APM_API

        /// <summary>
        /// Start a delete object.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        IAsyncResult BeginDelete(string bucketName, string objectKey, IDictionary<string, object> additionalProperties, AsyncCallback callback, object state);

        /// <summary>
        /// Get the results of a delete object.
        /// </summary>
        /// <param name="result"></param>
        void EndDelete(IAsyncResult result);

        /// <summary>
        /// Start an upload object from stream.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="stream"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        IAsyncResult BeginUploadObjectFromStream(string bucketName, string objectKey, Stream stream, IDictionary<string, object> additionalProperties, AsyncCallback callback, object state);

        /// <summary>
        /// Get the results of an upload from stream.
        /// </summary>
        /// <param name="result"></param>
        void EndUploadObjectFromStream(IAsyncResult result);

        /// <summary>
        /// Start an upload object from file path.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="filepath"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        IAsyncResult BeginUploadObjectFromFilePath(string bucketName, string objectKey, string filepath, IDictionary<string, object> additionalProperties, AsyncCallback callback, object state);

        /// <summary>
        /// Get the results of an upload from file path.
        /// </summary>
        /// <param name="result"></param>
        void EndUploadObjectFromFilePath(IAsyncResult result);

        /// <summary>
        /// Start a download to a file path.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="filepath"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        IAsyncResult BeginDownloadToFilePath(string bucketName, string objectKey, string filepath, IDictionary<string, object> additionalProperties, AsyncCallback callback, object state);

        /// <summary>
        /// Get results of downloading an object to a file path.
        /// </summary>
        /// <param name="result"></param>
        void EndDownloadToFilePath(IAsyncResult result);

        /// <summary>
        /// Start opening a stream to an object in S3.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        IAsyncResult BeginGetObjectStream(string bucketName, string objectKey, IDictionary<string, object> additionalProperties, AsyncCallback callback, object state);

        /// <summary>
        /// Get results of opening a stream to an object in S3.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        Stream EndGetObjectStream(IAsyncResult result);
#endif
#if AWS_ASYNC_API

        /// <summary>
        /// Upload an object from a stream.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="stream"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UploadObjectFromStreamAsync(string bucketName, string objectKey, Stream stream, IDictionary<string, object> additionalProperties, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete an object.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(string bucketName, string objectKey, IDictionary<string, object> additionalProperties, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Open a stream to an object in S3.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Stream> GetObjectStreamAsync(string bucketName, string objectKey, IDictionary<string, object> additionalProperties, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Upload an object from a file path.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="filepath"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UploadObjectFromFilePathAsync(string bucketName, string objectKey, string filepath, IDictionary<string, object> additionalProperties, CancellationToken cancellationToken = default(CancellationToken));

#endif

#if BCL45

        /// <summary>
        /// Download an object in S3 to a file path.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="filepath"></param>
        /// <param name="additionalProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DownloadToFilePathAsync(string bucketName, string objectKey, string filepath, IDictionary<string, object> additionalProperties, CancellationToken cancellationToken = default(CancellationToken));

#endif

    }
}
