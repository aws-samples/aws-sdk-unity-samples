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

using Amazon.Runtime;
using Amazon.CognitoSync.Model;

namespace Amazon.CognitoSync
{
    /// <summary>
    /// Implementation for accessing CognitoSync
    ///
    /// Amazon Cognito Sync 
    /// <para>
    /// Amazon Cognito Sync provides an AWS service and client library that enable cross-device
    /// syncing of application-related user data. High-level client libraries are available
    /// for both iOS and Android. You can use these libraries to persist data locally so that
    /// it's available even if the device is offline. Developer credentials don't need to
    /// be stored on the mobile device to access the service. You can use Amazon Cognito to
    /// obtain a normalized user ID and credentials. User data is persisted in a dataset that
    /// can store up to 1 MB of key-value pairs, and you can have up to 20 datasets per user
    /// identity.
    /// </para>
    /// </summary>
    public partial interface IAmazonCognitoSync : IDisposable
    {

#if DELETEMETHOD_SUPPORT
        #region  DeleteDataset
		
		/// NOT SUPPORTED - Since it is uses DELETE Method
		/// <summary>
		/// Initiates the asynchronous execution of the DeleteDataset operation.
		/// <seealso cref="Amazon.CognitoSync.IAmazonCognitoSync"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the DeleteDataset operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void DeleteDatasetAsync(DeleteDatasetRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
#endif

        #region  DescribeDataset


        /// <summary>
        /// Initiates the asynchronous execution of the DescribeDataset operation.
        /// <seealso cref="Amazon.CognitoSync.IAmazonCognitoSync"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DescribeDataset operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <returns>void</returns>
        void DescribeDatasetAsync(DescribeDatasetRequest request, AmazonServiceCallback callback, object state);

        #endregion

#if CONTROLPANEL_API_SUPPORT
        #region  DescribeIdentityPoolUsage
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the DescribeIdentityPoolUsage operation.
		/// <seealso cref="Amazon.CognitoSync.IAmazonCognitoSync"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the DescribeIdentityPoolUsage operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void DescribeIdentityPoolUsageAsync(DescribeIdentityPoolUsageRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
		
        #region  DescribeIdentityUsage
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the DescribeIdentityUsage operation.
		/// <seealso cref="Amazon.CognitoSync.IAmazonCognitoSync"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the DescribeIdentityUsage operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void DescribeIdentityUsageAsync(DescribeIdentityUsageRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
#endif

        #region  ListDatasets


        /// <summary>
        /// Initiates the asynchronous execution of the ListDatasets operation.
        /// <seealso cref="Amazon.CognitoSync.IAmazonCognitoSync"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListDatasets operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <returns>void</returns>
        void ListDatasetsAsync(ListDatasetsRequest request, AmazonServiceCallback callback, object state);

        #endregion

#if CONTROLPANEL_API_SUPPORT
        #region  ListIdentityPoolUsage
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the ListIdentityPoolUsage operation.
		/// <seealso cref="Amazon.CognitoSync.IAmazonCognitoSync"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the ListIdentityPoolUsage operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void ListIdentityPoolUsageAsync(ListIdentityPoolUsageRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
#endif
        #region  ListRecords


        /// <summary>
        /// Initiates the asynchronous execution of the ListRecords operation.
        /// <seealso cref="Amazon.CognitoSync.IAmazonCognitoSync"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListRecords operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <returns>void</returns>
        void ListRecordsAsync(ListRecordsRequest request, AmazonServiceCallback callback, object state);

        #endregion

        #region  UpdateRecords


        /// <summary>
        /// Initiates the asynchronous execution of the UpdateRecords operation.
        /// <seealso cref="Amazon.CognitoSync.IAmazonCognitoSync"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UpdateRecords operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <returns>void</returns>
        void UpdateRecordsAsync(UpdateRecordsRequest request, AmazonServiceCallback callback, object state);

        #endregion

    }
}