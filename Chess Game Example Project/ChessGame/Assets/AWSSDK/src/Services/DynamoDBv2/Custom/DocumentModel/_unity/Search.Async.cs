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

using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace Amazon.DynamoDBv2.DocumentModel
{
    /// <summary>
    /// Search response object
    /// </summary>
    public partial class Search
    {
        #region Public async methods

        /// <summary>
        /// Initiates the asynchronous execution of the GetNextSet operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Search.GetNextSet"/>
        /// </summary>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void GetNextSetAsync(AmazonDynamoDBCallback<List<Document>> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<List<Document>>(
                () => { return GetNextSetHelper(true); },
                asyncOptions,
                callback);
        }
        
        /// <summary>
        /// Initiates the asynchronous execution of the GetRemaining operation.
        /// <seealso cref="Amazon.DynamoDBv2.DocumentModel.Search.GetRemaining"/>
        /// </summary>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param> 
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void GetRemainingAsync(AmazonDynamoDBCallback<List<Document>> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<List<Document>>(
                () => { return GetRemainingHelper(true); },
                asyncOptions,
                callback);
        }

        #endregion
    }
}
