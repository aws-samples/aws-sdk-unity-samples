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
using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace Amazon.DynamoDBv2.DataModel
{
    /// <summary>
    /// A strongly-typed object for retrieving search results (Query or Scan)
    /// from DynamoDB.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class AsyncSearch<T>
    {
        #region Public async methods

        /// <summary>
        /// Initiates the asynchronous execution to get the next set of results from DynamoDB.
        /// </summary>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void GetNextSetAsync(AmazonDynamoDBCallback<List<T>> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<List<T>>(
                () =>
                {
                    var documents = DocumentSearch.GetNextSetHelper(true);
                    List<T> items = SourceContext.FromDocumentsHelper<T>(documents, Config).ToList();
                    return items;
                },
                asyncOptions,
                callback);
        }

        /// <summary>
        /// Initiates the asynchronous execution to get all the remaining results from DynamoDB.
        /// </summary>        
        /// <param name="callback">The callback that will be invoked when the asynchronous operation completes.</param>
        /// <param name="asyncOptions">An instance of AsyncOptions that specifies how the async method should be executed.</param>
        public void GetRemainingAsync(AmazonDynamoDBCallback<List<T>> callback, AsyncOptions asyncOptions = null)
        {
            asyncOptions = asyncOptions ?? new AsyncOptions();
            DynamoDBAsyncExecutor.ExecuteAsync<List<T>>(
                () =>
                {
                    var documents = DocumentSearch.GetRemainingHelper(true);
                    List<T> items = SourceContext.FromDocumentsHelper<T>(documents, Config).ToList();
                    return items;
                },
                asyncOptions,
                callback);
        }
        
        #endregion
    }
}
