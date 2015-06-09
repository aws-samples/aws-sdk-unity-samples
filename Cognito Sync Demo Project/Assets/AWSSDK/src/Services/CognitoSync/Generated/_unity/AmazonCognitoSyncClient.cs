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

/*
 * Do not modify this file. This file is generated from the cognito-sync-2014-06-30.normal.json service model.
 */


using System;
using System.Collections.Generic;

using Amazon.CognitoSync.Model;
using Amazon.CognitoSync.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;

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
    ///  
    /// <para>
    /// With Amazon Cognito Sync, the data stored for each identity is accessible only to
    /// credentials assigned to that identity. In order to use the Cognito Sync service, you
    /// need to make API calls using credentials retrieved with <a href="http://docs.aws.amazon.com/cognitoidentity/latest/APIReference/Welcome.html">Amazon
    /// Cognito Identity service</a>.
    /// </para>
    ///  
    /// <para>
    /// If you want to use Cognito Sync in an Android or iOS application, you will probably
    /// want to make API calls via the AWS Mobile SDK. To learn more, see the <a href="http://docs.aws.amazon.com/mobile/sdkforandroid/developerguide/cognito-sync.html">Developer
    /// Guide for Android</a> and the <a href="http://docs.aws.amazon.com/mobile/sdkforios/developerguide/cognito-sync.html">Developer
    /// Guide for iOS</a>.
    /// </para>
    /// </summary>
    public partial class AmazonCognitoSyncClient : AmazonUnityServiceClient, IAmazonCognitoSync
    {
        #region Constructors

        /// <summary>
        /// Constructs AmazonCognitoSyncClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        public AmazonCognitoSyncClient(AWSCredentials credentials)
            : this(credentials, new AmazonCognitoSyncConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoSyncClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="region">The region to connect.</param>
        public AmazonCognitoSyncClient(AWSCredentials credentials, RegionEndpoint region)
            : this(credentials, new AmazonCognitoSyncConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoSyncClient with AWS Credentials and an
        /// AmazonCognitoSyncClient Configuration object.
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="clientConfig">The AmazonCognitoSyncClient Configuration Object</param>
        public AmazonCognitoSyncClient(AWSCredentials credentials, AmazonCognitoSyncConfig clientConfig)
            : base(credentials, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoSyncClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        public AmazonCognitoSyncClient(string awsAccessKeyId, string awsSecretAccessKey)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonCognitoSyncConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoSyncClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="region">The region to connect.</param>
        public AmazonCognitoSyncClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonCognitoSyncConfig() {RegionEndpoint=region})
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoSyncClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonCognitoSyncClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="clientConfig">The AmazonCognitoSyncClient Configuration Object</param>
        public AmazonCognitoSyncClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonCognitoSyncConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoSyncClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        public AmazonCognitoSyncClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonCognitoSyncConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoSyncClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="region">The region to connect.</param>
        public AmazonCognitoSyncClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonCognitoSyncConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoSyncClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonCognitoSyncClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="clientConfig">The AmazonCognitoSyncClient Configuration Object</param>
        public AmazonCognitoSyncClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, AmazonCognitoSyncConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, clientConfig)
        {
        }

        #endregion

        #region Overrides

        protected override AbstractAWSSigner CreateSigner()
        {
            return new AWS4Signer();
        }

        protected override void CustomizeRuntimePipeline(RuntimePipeline pipeline)
        {
            pipeline.ReplaceHandler<Amazon.Runtime.Internal.ErrorHandler>(new Amazon.Runtime.Internal.ErrorHandler<AmazonCognitoSyncException>(this.Logger));
            pipeline.RemoveHandler<Amazon.Runtime.Internal.CredentialsRetriever>();
            pipeline.AddHandlerBefore<Amazon.Runtime.Internal.Marshaller>(new Amazon.CognitoSync.Internal.CognitoCredentialsRetriever(this.Credentials));
            pipeline.AddHandlerAfter<Amazon.Runtime.Internal.Marshaller>(new Amazon.CognitoSync.Internal.AmazonCognitoSyncPostMarshallHandler());
            pipeline.AddHandlerAfter<Amazon.Runtime.Internal.Signer>(new Amazon.CognitoSync.Internal.AmazonCognitoSyncPostSignHandler());
        }    

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

        
        #region  BulkPublish


        /// <summary>
        /// Initiates the asynchronous execution of the BulkPublish operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the BulkPublish operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void BulkPublishAsync(BulkPublishRequest request, AmazonServiceCallback<BulkPublishRequest, BulkPublishResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new BulkPublishRequestMarshaller();
            var unmarshaller = BulkPublishResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<BulkPublishRequest,BulkPublishResponse> responseObject 
                            = new AmazonServiceResult<BulkPublishRequest,BulkPublishResponse>((BulkPublishRequest)req, (BulkPublishResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<BulkPublishRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  DeleteDataset


        /// <summary>
        /// Initiates the asynchronous execution of the DeleteDataset operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteDataset operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DeleteDatasetAsync(DeleteDatasetRequest request, AmazonServiceCallback<DeleteDatasetRequest, DeleteDatasetResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DeleteDatasetRequestMarshaller();
            var unmarshaller = DeleteDatasetResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DeleteDatasetRequest,DeleteDatasetResponse> responseObject 
                            = new AmazonServiceResult<DeleteDatasetRequest,DeleteDatasetResponse>((DeleteDatasetRequest)req, (DeleteDatasetResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DeleteDatasetRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  DescribeDataset


        /// <summary>
        /// Initiates the asynchronous execution of the DescribeDataset operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeDataset operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DescribeDatasetAsync(DescribeDatasetRequest request, AmazonServiceCallback<DescribeDatasetRequest, DescribeDatasetResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DescribeDatasetRequestMarshaller();
            var unmarshaller = DescribeDatasetResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DescribeDatasetRequest,DescribeDatasetResponse> responseObject 
                            = new AmazonServiceResult<DescribeDatasetRequest,DescribeDatasetResponse>((DescribeDatasetRequest)req, (DescribeDatasetResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DescribeDatasetRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  DescribeIdentityPoolUsage

        /// <summary>
        /// Gets usage details (for example, data storage) about a particular identity pool.
        /// 
        ///  
        /// <para>
        /// DescribeIdentityPoolUsage can only be called with developer credentials. You cannot
        /// make this API call with the temporary user credentials provided by Cognito Identity.
        /// </para>
        /// </summary>
        /// <param name="identityPoolId">A name-spaced GUID (for example, us-east-1:23EC4050-6AEA-7089-A2DD-08002EXAMPLE) created by Amazon Cognito. GUID generation is unique within a region.</param>
        /// 
        /// <returns>The response from the DescribeIdentityPoolUsage service method, as returned by CognitoSync.</returns>
        /// <exception cref="Amazon.CognitoSync.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.CognitoSync.Model.InvalidParameterException">
        /// Thrown when a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.CognitoSync.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoSync.Model.ResourceNotFoundException">
        /// Thrown if the resource doesn't exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoSync.Model.TooManyRequestsException">
        /// Thrown if the request is throttled.
        /// </exception>
        public void DescribeIdentityPoolUsageAsync(string identityPoolId, AmazonServiceCallback<DescribeIdentityPoolUsageRequest, DescribeIdentityPoolUsageResponse> callback, AsyncOptions options = null)
        {
            var request = new DescribeIdentityPoolUsageRequest();
            request.IdentityPoolId = identityPoolId;
            DescribeIdentityPoolUsageAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the DescribeIdentityPoolUsage operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeIdentityPoolUsage operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DescribeIdentityPoolUsageAsync(DescribeIdentityPoolUsageRequest request, AmazonServiceCallback<DescribeIdentityPoolUsageRequest, DescribeIdentityPoolUsageResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DescribeIdentityPoolUsageRequestMarshaller();
            var unmarshaller = DescribeIdentityPoolUsageResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DescribeIdentityPoolUsageRequest,DescribeIdentityPoolUsageResponse> responseObject 
                            = new AmazonServiceResult<DescribeIdentityPoolUsageRequest,DescribeIdentityPoolUsageResponse>((DescribeIdentityPoolUsageRequest)req, (DescribeIdentityPoolUsageResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DescribeIdentityPoolUsageRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  DescribeIdentityUsage


        /// <summary>
        /// Initiates the asynchronous execution of the DescribeIdentityUsage operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeIdentityUsage operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DescribeIdentityUsageAsync(DescribeIdentityUsageRequest request, AmazonServiceCallback<DescribeIdentityUsageRequest, DescribeIdentityUsageResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DescribeIdentityUsageRequestMarshaller();
            var unmarshaller = DescribeIdentityUsageResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DescribeIdentityUsageRequest,DescribeIdentityUsageResponse> responseObject 
                            = new AmazonServiceResult<DescribeIdentityUsageRequest,DescribeIdentityUsageResponse>((DescribeIdentityUsageRequest)req, (DescribeIdentityUsageResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DescribeIdentityUsageRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetBulkPublishDetails


        /// <summary>
        /// Initiates the asynchronous execution of the GetBulkPublishDetails operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetBulkPublishDetails operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetBulkPublishDetailsAsync(GetBulkPublishDetailsRequest request, AmazonServiceCallback<GetBulkPublishDetailsRequest, GetBulkPublishDetailsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetBulkPublishDetailsRequestMarshaller();
            var unmarshaller = GetBulkPublishDetailsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetBulkPublishDetailsRequest,GetBulkPublishDetailsResponse> responseObject 
                            = new AmazonServiceResult<GetBulkPublishDetailsRequest,GetBulkPublishDetailsResponse>((GetBulkPublishDetailsRequest)req, (GetBulkPublishDetailsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetBulkPublishDetailsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetCognitoEvents


        /// <summary>
        /// Initiates the asynchronous execution of the GetCognitoEvents operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetCognitoEvents operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetCognitoEventsAsync(GetCognitoEventsRequest request, AmazonServiceCallback<GetCognitoEventsRequest, GetCognitoEventsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetCognitoEventsRequestMarshaller();
            var unmarshaller = GetCognitoEventsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetCognitoEventsRequest,GetCognitoEventsResponse> responseObject 
                            = new AmazonServiceResult<GetCognitoEventsRequest,GetCognitoEventsResponse>((GetCognitoEventsRequest)req, (GetCognitoEventsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetCognitoEventsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetIdentityPoolConfiguration

        /// <summary>
        /// Gets the configuration settings of an identity pool.
        /// </summary>
        /// <param name="identityPoolId">A name-spaced GUID (for example, us-east-1:23EC4050-6AEA-7089-A2DD-08002EXAMPLE) created by Amazon Cognito. This is the ID of the pool for which to return a configuration.</param>
        /// 
        /// <returns>The response from the GetIdentityPoolConfiguration service method, as returned by CognitoSync.</returns>
        /// <exception cref="Amazon.CognitoSync.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.CognitoSync.Model.InvalidParameterException">
        /// Thrown when a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.CognitoSync.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoSync.Model.ResourceNotFoundException">
        /// Thrown if the resource doesn't exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoSync.Model.TooManyRequestsException">
        /// Thrown if the request is throttled.
        /// </exception>
        public void GetIdentityPoolConfigurationAsync(string identityPoolId, AmazonServiceCallback<GetIdentityPoolConfigurationRequest, GetIdentityPoolConfigurationResponse> callback, AsyncOptions options = null)
        {
            var request = new GetIdentityPoolConfigurationRequest();
            request.IdentityPoolId = identityPoolId;
            GetIdentityPoolConfigurationAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the GetIdentityPoolConfiguration operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetIdentityPoolConfiguration operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetIdentityPoolConfigurationAsync(GetIdentityPoolConfigurationRequest request, AmazonServiceCallback<GetIdentityPoolConfigurationRequest, GetIdentityPoolConfigurationResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetIdentityPoolConfigurationRequestMarshaller();
            var unmarshaller = GetIdentityPoolConfigurationResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetIdentityPoolConfigurationRequest,GetIdentityPoolConfigurationResponse> responseObject 
                            = new AmazonServiceResult<GetIdentityPoolConfigurationRequest,GetIdentityPoolConfigurationResponse>((GetIdentityPoolConfigurationRequest)req, (GetIdentityPoolConfigurationResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetIdentityPoolConfigurationRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListDatasets


        /// <summary>
        /// Initiates the asynchronous execution of the ListDatasets operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListDatasets operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListDatasetsAsync(ListDatasetsRequest request, AmazonServiceCallback<ListDatasetsRequest, ListDatasetsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListDatasetsRequestMarshaller();
            var unmarshaller = ListDatasetsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListDatasetsRequest,ListDatasetsResponse> responseObject 
                            = new AmazonServiceResult<ListDatasetsRequest,ListDatasetsResponse>((ListDatasetsRequest)req, (ListDatasetsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListDatasetsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListIdentityPoolUsage


        /// <summary>
        /// Initiates the asynchronous execution of the ListIdentityPoolUsage operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListIdentityPoolUsage operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListIdentityPoolUsageAsync(ListIdentityPoolUsageRequest request, AmazonServiceCallback<ListIdentityPoolUsageRequest, ListIdentityPoolUsageResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListIdentityPoolUsageRequestMarshaller();
            var unmarshaller = ListIdentityPoolUsageResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListIdentityPoolUsageRequest,ListIdentityPoolUsageResponse> responseObject 
                            = new AmazonServiceResult<ListIdentityPoolUsageRequest,ListIdentityPoolUsageResponse>((ListIdentityPoolUsageRequest)req, (ListIdentityPoolUsageResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListIdentityPoolUsageRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListRecords


        /// <summary>
        /// Initiates the asynchronous execution of the ListRecords operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListRecords operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListRecordsAsync(ListRecordsRequest request, AmazonServiceCallback<ListRecordsRequest, ListRecordsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListRecordsRequestMarshaller();
            var unmarshaller = ListRecordsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListRecordsRequest,ListRecordsResponse> responseObject 
                            = new AmazonServiceResult<ListRecordsRequest,ListRecordsResponse>((ListRecordsRequest)req, (ListRecordsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListRecordsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  RegisterDevice


        /// <summary>
        /// Initiates the asynchronous execution of the RegisterDevice operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the RegisterDevice operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void RegisterDeviceAsync(RegisterDeviceRequest request, AmazonServiceCallback<RegisterDeviceRequest, RegisterDeviceResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new RegisterDeviceRequestMarshaller();
            var unmarshaller = RegisterDeviceResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<RegisterDeviceRequest,RegisterDeviceResponse> responseObject 
                            = new AmazonServiceResult<RegisterDeviceRequest,RegisterDeviceResponse>((RegisterDeviceRequest)req, (RegisterDeviceResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<RegisterDeviceRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  SetCognitoEvents


        /// <summary>
        /// Initiates the asynchronous execution of the SetCognitoEvents operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the SetCognitoEvents operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void SetCognitoEventsAsync(SetCognitoEventsRequest request, AmazonServiceCallback<SetCognitoEventsRequest, SetCognitoEventsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new SetCognitoEventsRequestMarshaller();
            var unmarshaller = SetCognitoEventsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<SetCognitoEventsRequest,SetCognitoEventsResponse> responseObject 
                            = new AmazonServiceResult<SetCognitoEventsRequest,SetCognitoEventsResponse>((SetCognitoEventsRequest)req, (SetCognitoEventsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<SetCognitoEventsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  SetIdentityPoolConfiguration


        /// <summary>
        /// Initiates the asynchronous execution of the SetIdentityPoolConfiguration operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the SetIdentityPoolConfiguration operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void SetIdentityPoolConfigurationAsync(SetIdentityPoolConfigurationRequest request, AmazonServiceCallback<SetIdentityPoolConfigurationRequest, SetIdentityPoolConfigurationResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new SetIdentityPoolConfigurationRequestMarshaller();
            var unmarshaller = SetIdentityPoolConfigurationResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<SetIdentityPoolConfigurationRequest,SetIdentityPoolConfigurationResponse> responseObject 
                            = new AmazonServiceResult<SetIdentityPoolConfigurationRequest,SetIdentityPoolConfigurationResponse>((SetIdentityPoolConfigurationRequest)req, (SetIdentityPoolConfigurationResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<SetIdentityPoolConfigurationRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  SubscribeToDataset


        /// <summary>
        /// Initiates the asynchronous execution of the SubscribeToDataset operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the SubscribeToDataset operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void SubscribeToDatasetAsync(SubscribeToDatasetRequest request, AmazonServiceCallback<SubscribeToDatasetRequest, SubscribeToDatasetResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new SubscribeToDatasetRequestMarshaller();
            var unmarshaller = SubscribeToDatasetResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<SubscribeToDatasetRequest,SubscribeToDatasetResponse> responseObject 
                            = new AmazonServiceResult<SubscribeToDatasetRequest,SubscribeToDatasetResponse>((SubscribeToDatasetRequest)req, (SubscribeToDatasetResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<SubscribeToDatasetRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  UnsubscribeFromDataset


        /// <summary>
        /// Initiates the asynchronous execution of the UnsubscribeFromDataset operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the UnsubscribeFromDataset operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void UnsubscribeFromDatasetAsync(UnsubscribeFromDatasetRequest request, AmazonServiceCallback<UnsubscribeFromDatasetRequest, UnsubscribeFromDatasetResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new UnsubscribeFromDatasetRequestMarshaller();
            var unmarshaller = UnsubscribeFromDatasetResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<UnsubscribeFromDatasetRequest,UnsubscribeFromDatasetResponse> responseObject 
                            = new AmazonServiceResult<UnsubscribeFromDatasetRequest,UnsubscribeFromDatasetResponse>((UnsubscribeFromDatasetRequest)req, (UnsubscribeFromDatasetResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<UnsubscribeFromDatasetRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  UpdateRecords


        /// <summary>
        /// Initiates the asynchronous execution of the UpdateRecords operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the UpdateRecords operation on AmazonCognitoSyncClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void UpdateRecordsAsync(UpdateRecordsRequest request, AmazonServiceCallback<UpdateRecordsRequest, UpdateRecordsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new UpdateRecordsRequestMarshaller();
            var unmarshaller = UpdateRecordsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<UpdateRecordsRequest,UpdateRecordsResponse> responseObject 
                            = new AmazonServiceResult<UpdateRecordsRequest,UpdateRecordsResponse>((UpdateRecordsRequest)req, (UpdateRecordsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<UpdateRecordsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
    }
}
