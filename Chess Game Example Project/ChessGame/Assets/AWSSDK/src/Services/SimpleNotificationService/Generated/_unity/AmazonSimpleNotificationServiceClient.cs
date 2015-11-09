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
 * Do not modify this file. This file is generated from the sns-2010-03-31.normal.json service model.
 */


using System;
using System.Collections.Generic;

using Amazon.SimpleNotificationService.Model;
using Amazon.SimpleNotificationService.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.SimpleNotificationService
{
    /// <summary>
    /// Implementation for accessing SimpleNotificationService
    ///
    /// Amazon Simple Notification Service        
    /// <para>
    /// Amazon Simple Notification Service (Amazon SNS) is a web service that enables you
    /// to build distributed web-enabled applications.         Applications can use Amazon SNS
    /// to easily push real-time notification messages         to interested subscribers over
    /// multiple delivery protocols. For more information about this product        see <a href="http://aws.amazon.com/sns/">http://aws.amazon.com/sns</a>.
    /// For detailed information about Amazon SNS features and their associated API calls,        see
    /// the <a href="http://docs.aws.amazon.com/sns/latest/dg/">Amazon SNS Developer Guide</a>.    
    /// </para>
    ///     
    /// <para>
    /// We also provide SDKs that enable you to access Amazon SNS from your preferred programming
    /// language.       The SDKs contain functionality that automatically takes care of tasks
    /// such as: cryptographically signing your service requests,       retrying requests,
    /// and handling error responses. For a list of available SDKs, go to <a href="http://aws.amazon.com/tools/">Tools
    /// for Amazon Web Services</a>.      
    /// </para>
    /// </summary>
    public partial class AmazonSimpleNotificationServiceClient : AmazonUnityServiceClient, IAmazonSimpleNotificationService
    {
        #region Constructors

        /// <summary>
        /// Constructs AmazonSimpleNotificationServiceClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        public AmazonSimpleNotificationServiceClient(AWSCredentials credentials)
            : this(credentials, new AmazonSimpleNotificationServiceConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonSimpleNotificationServiceClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="region">The region to connect.</param>
        public AmazonSimpleNotificationServiceClient(AWSCredentials credentials, RegionEndpoint region)
            : this(credentials, new AmazonSimpleNotificationServiceConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonSimpleNotificationServiceClient with AWS Credentials and an
        /// AmazonSimpleNotificationServiceClient Configuration object.
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="clientConfig">The AmazonSimpleNotificationServiceClient Configuration Object</param>
        public AmazonSimpleNotificationServiceClient(AWSCredentials credentials, AmazonSimpleNotificationServiceConfig clientConfig)
            : base(credentials, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonSimpleNotificationServiceClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        public AmazonSimpleNotificationServiceClient(string awsAccessKeyId, string awsSecretAccessKey)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonSimpleNotificationServiceConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonSimpleNotificationServiceClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="region">The region to connect.</param>
        public AmazonSimpleNotificationServiceClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonSimpleNotificationServiceConfig() {RegionEndpoint=region})
        {
        }

        /// <summary>
        /// Constructs AmazonSimpleNotificationServiceClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonSimpleNotificationServiceClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="clientConfig">The AmazonSimpleNotificationServiceClient Configuration Object</param>
        public AmazonSimpleNotificationServiceClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonSimpleNotificationServiceConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonSimpleNotificationServiceClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        public AmazonSimpleNotificationServiceClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonSimpleNotificationServiceConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonSimpleNotificationServiceClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="region">The region to connect.</param>
        public AmazonSimpleNotificationServiceClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonSimpleNotificationServiceConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonSimpleNotificationServiceClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonSimpleNotificationServiceClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="clientConfig">The AmazonSimpleNotificationServiceClient Configuration Object</param>
        public AmazonSimpleNotificationServiceClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, AmazonSimpleNotificationServiceConfig clientConfig)
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
            pipeline.ReplaceHandler<Amazon.Runtime.Internal.ErrorHandler>(new Amazon.Runtime.Internal.ErrorHandler<AmazonSimpleNotificationServiceException>(this.Logger));
        }    

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

        
        #region  AddPermission

        /// <summary>
        /// Adds a statement to a topic's access control policy, granting access for the specified
        /// AWS accounts to the specified actions.
        /// </summary>
        /// <param name="topicArn">The ARN of the topic whose access control policy you wish to modify.</param>
        /// <param name="label">A unique identifier for the new policy statement.</param>
        /// <param name="awsAccountId">The AWS account IDs of the users (principals) who will be given access to the specified actions. The users must have AWS accounts, but do not need to be signed up  for this service. </param>
        /// <param name="actionName">The action you want to allow for the specified principal(s). Valid values: any Amazon SNS action name.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the AddPermission service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        public void AddPermissionAsync(string topicArn, string label, List<string> awsAccountId, List<string> actionName,  AmazonServiceCallback<AddPermissionRequest, AddPermissionResponse> callback, AsyncOptions options = null)
        {
            var request = new AddPermissionRequest();
            request.TopicArn = topicArn;
            request.Label = label;
            request.AWSAccountId = awsAccountId;
            request.ActionName = actionName;
            AddPermissionAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the AddPermission operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the AddPermission operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void AddPermissionAsync(AddPermissionRequest request, AmazonServiceCallback<AddPermissionRequest, AddPermissionResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new AddPermissionRequestMarshaller();
            var unmarshaller = AddPermissionResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<AddPermissionRequest,AddPermissionResponse> responseObject 
                            = new AmazonServiceResult<AddPermissionRequest,AddPermissionResponse>((AddPermissionRequest)req, (AddPermissionResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<AddPermissionRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ConfirmSubscription

        /// <summary>
        /// Verifies an endpoint owner's intent to receive messages by validating      the token
        /// sent to the endpoint by an earlier <code>Subscribe</code> action. If the      token
        /// is valid, the action creates a new subscription and returns its      Amazon Resource
        /// Name (ARN). This call requires an AWS signature only when the <code>AuthenticateOnUnsubscribe</code>
        /// flag is set to "true".
        /// </summary>
        /// <param name="topicArn">The ARN of the topic for which you wish to confirm a subscription.</param>
        /// <param name="token">Short-lived token sent to an endpoint during the <code>Subscribe</code> action.</param>
        /// <param name="authenticateOnUnsubscribe">Disallows unauthenticated unsubscribes of the subscription.  If the value of this parameter is <code>true</code> and the request has an AWS signature, then only the topic owner and the subscription owner can unsubscribe the endpoint.  The unsubscribe action requires AWS authentication. </param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ConfirmSubscription service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.SubscriptionLimitExceededException">
        /// Indicates that the customer already owns the maximum allowed number of subscriptions.
        /// </exception>
        public void ConfirmSubscriptionAsync(string topicArn, string token, string authenticateOnUnsubscribe,  AmazonServiceCallback<ConfirmSubscriptionRequest, ConfirmSubscriptionResponse> callback, AsyncOptions options = null)
        {
            var request = new ConfirmSubscriptionRequest();
            request.TopicArn = topicArn;
            request.Token = token;
            request.AuthenticateOnUnsubscribe = authenticateOnUnsubscribe;
            ConfirmSubscriptionAsync(request, callback, options);
        }


        /// <summary>
        /// Verifies an endpoint owner's intent to receive messages by validating      the token
        /// sent to the endpoint by an earlier <code>Subscribe</code> action. If the      token
        /// is valid, the action creates a new subscription and returns its      Amazon Resource
        /// Name (ARN). This call requires an AWS signature only when the <code>AuthenticateOnUnsubscribe</code>
        /// flag is set to "true".
        /// </summary>
        /// <param name="topicArn">The ARN of the topic for which you wish to confirm a subscription.</param>
        /// <param name="token">Short-lived token sent to an endpoint during the <code>Subscribe</code> action.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ConfirmSubscription service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.SubscriptionLimitExceededException">
        /// Indicates that the customer already owns the maximum allowed number of subscriptions.
        /// </exception>
        public void ConfirmSubscriptionAsync(string topicArn, string token,  AmazonServiceCallback<ConfirmSubscriptionRequest, ConfirmSubscriptionResponse> callback, AsyncOptions options = null)
        {
            var request = new ConfirmSubscriptionRequest();
            request.TopicArn = topicArn;
            request.Token = token;
            ConfirmSubscriptionAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the ConfirmSubscription operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ConfirmSubscription operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ConfirmSubscriptionAsync(ConfirmSubscriptionRequest request, AmazonServiceCallback<ConfirmSubscriptionRequest, ConfirmSubscriptionResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ConfirmSubscriptionRequestMarshaller();
            var unmarshaller = ConfirmSubscriptionResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ConfirmSubscriptionRequest,ConfirmSubscriptionResponse> responseObject 
                            = new AmazonServiceResult<ConfirmSubscriptionRequest,ConfirmSubscriptionResponse>((ConfirmSubscriptionRequest)req, (ConfirmSubscriptionResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ConfirmSubscriptionRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  CreatePlatformApplication

        /// <summary>
        /// Initiates the asynchronous execution of the CreatePlatformApplication operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CreatePlatformApplication operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void CreatePlatformApplicationAsync(CreatePlatformApplicationRequest request, AmazonServiceCallback<CreatePlatformApplicationRequest, CreatePlatformApplicationResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new CreatePlatformApplicationRequestMarshaller();
            var unmarshaller = CreatePlatformApplicationResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<CreatePlatformApplicationRequest,CreatePlatformApplicationResponse> responseObject 
                            = new AmazonServiceResult<CreatePlatformApplicationRequest,CreatePlatformApplicationResponse>((CreatePlatformApplicationRequest)req, (CreatePlatformApplicationResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<CreatePlatformApplicationRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  CreatePlatformEndpoint

        /// <summary>
        /// Initiates the asynchronous execution of the CreatePlatformEndpoint operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CreatePlatformEndpoint operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void CreatePlatformEndpointAsync(CreatePlatformEndpointRequest request, AmazonServiceCallback<CreatePlatformEndpointRequest, CreatePlatformEndpointResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new CreatePlatformEndpointRequestMarshaller();
            var unmarshaller = CreatePlatformEndpointResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<CreatePlatformEndpointRequest,CreatePlatformEndpointResponse> responseObject 
                            = new AmazonServiceResult<CreatePlatformEndpointRequest,CreatePlatformEndpointResponse>((CreatePlatformEndpointRequest)req, (CreatePlatformEndpointResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<CreatePlatformEndpointRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  CreateTopic

        /// <summary>
        /// Creates a topic to which notifications can be published. Users can create     at most
        /// 3000 topics. For more information, see <a href="http://aws.amazon.com/sns/">http://aws.amazon.com/sns</a>.
        ///      This action is idempotent, so if the requester already owns a topic with the
        /// specified name, that topic's ARN is      returned without creating a new topic.
        /// </summary>
        /// <param name="name">The name of the topic you want to create. Constraints: Topic names must be made up of  only uppercase and lowercase ASCII letters, numbers, underscores, and hyphens, and must be  between 1 and 256 characters long. </param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the CreateTopic service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.TopicLimitExceededException">
        /// Indicates that the customer already owns the maximum allowed number of topics.
        /// </exception>
        public void CreateTopicAsync(string name,  AmazonServiceCallback<CreateTopicRequest, CreateTopicResponse> callback, AsyncOptions options = null)
        {
            var request = new CreateTopicRequest();
            request.Name = name;
            CreateTopicAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the CreateTopic operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CreateTopic operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void CreateTopicAsync(CreateTopicRequest request, AmazonServiceCallback<CreateTopicRequest, CreateTopicResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new CreateTopicRequestMarshaller();
            var unmarshaller = CreateTopicResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<CreateTopicRequest,CreateTopicResponse> responseObject 
                            = new AmazonServiceResult<CreateTopicRequest,CreateTopicResponse>((CreateTopicRequest)req, (CreateTopicResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<CreateTopicRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  DeleteEndpoint

        /// <summary>
        /// Initiates the asynchronous execution of the DeleteEndpoint operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteEndpoint operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DeleteEndpointAsync(DeleteEndpointRequest request, AmazonServiceCallback<DeleteEndpointRequest, DeleteEndpointResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DeleteEndpointRequestMarshaller();
            var unmarshaller = DeleteEndpointResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DeleteEndpointRequest,DeleteEndpointResponse> responseObject 
                            = new AmazonServiceResult<DeleteEndpointRequest,DeleteEndpointResponse>((DeleteEndpointRequest)req, (DeleteEndpointResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DeleteEndpointRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  DeletePlatformApplication

        /// <summary>
        /// Initiates the asynchronous execution of the DeletePlatformApplication operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeletePlatformApplication operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DeletePlatformApplicationAsync(DeletePlatformApplicationRequest request, AmazonServiceCallback<DeletePlatformApplicationRequest, DeletePlatformApplicationResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DeletePlatformApplicationRequestMarshaller();
            var unmarshaller = DeletePlatformApplicationResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DeletePlatformApplicationRequest,DeletePlatformApplicationResponse> responseObject 
                            = new AmazonServiceResult<DeletePlatformApplicationRequest,DeletePlatformApplicationResponse>((DeletePlatformApplicationRequest)req, (DeletePlatformApplicationResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DeletePlatformApplicationRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  DeleteTopic

        /// <summary>
        /// Deletes a topic and all its subscriptions. Deleting a topic might      prevent some
        /// messages previously sent to the topic from being delivered to      subscribers. This
        /// action is idempotent, so deleting a topic that does not      exist does not result
        /// in an error.
        /// </summary>
        /// <param name="topicArn">The ARN of the topic you want to delete.<examples> <queryrequest>  http://sns.us-east-1.amazonaws.com/  ?TopicArn=arn%3Aaws%3Asns%3Aus-east-1%3A123456789012%3AMy-Topic  &amp;Action=DeleteTopic  &amp;SignatureVersion=2  &amp;SignatureMethod=HmacSHA256  &amp;Timestamp=2010-03-31T12%3A00%3A00.000Z  &amp;AWSAccessKeyId=(AWS Access Key ID)  &amp;Signature=DjHBa%2BbYCKQAzctOPnLP7MbHnrHT3%2FK3kFEZjwcf9%2FU%3D </queryrequest> <queryresponse>  &lt;DeleteTopicResponse xmlns="http://sns.amazonaws.com/doc/2010-03-31/"&gt;  &lt;ResponseMetadata&gt; &lt;RequestId&gt;fba800b9-3765-11df-8cf3-c58c53254dfb&lt;/RequestId&gt;  &lt;/ResponseMetadata&gt;&lt;/DeleteTopicResponse&gt;   </queryresponse></examples></param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the DeleteTopic service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        public void DeleteTopicAsync(string topicArn,  AmazonServiceCallback<DeleteTopicRequest, DeleteTopicResponse> callback, AsyncOptions options = null)
        {
            var request = new DeleteTopicRequest();
            request.TopicArn = topicArn;
            DeleteTopicAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the DeleteTopic operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteTopic operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DeleteTopicAsync(DeleteTopicRequest request, AmazonServiceCallback<DeleteTopicRequest, DeleteTopicResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DeleteTopicRequestMarshaller();
            var unmarshaller = DeleteTopicResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DeleteTopicRequest,DeleteTopicResponse> responseObject 
                            = new AmazonServiceResult<DeleteTopicRequest,DeleteTopicResponse>((DeleteTopicRequest)req, (DeleteTopicResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DeleteTopicRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetEndpointAttributes

        /// <summary>
        /// Initiates the asynchronous execution of the GetEndpointAttributes operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetEndpointAttributes operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetEndpointAttributesAsync(GetEndpointAttributesRequest request, AmazonServiceCallback<GetEndpointAttributesRequest, GetEndpointAttributesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetEndpointAttributesRequestMarshaller();
            var unmarshaller = GetEndpointAttributesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetEndpointAttributesRequest,GetEndpointAttributesResponse> responseObject 
                            = new AmazonServiceResult<GetEndpointAttributesRequest,GetEndpointAttributesResponse>((GetEndpointAttributesRequest)req, (GetEndpointAttributesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetEndpointAttributesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetPlatformApplicationAttributes

        /// <summary>
        /// Initiates the asynchronous execution of the GetPlatformApplicationAttributes operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetPlatformApplicationAttributes operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetPlatformApplicationAttributesAsync(GetPlatformApplicationAttributesRequest request, AmazonServiceCallback<GetPlatformApplicationAttributesRequest, GetPlatformApplicationAttributesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetPlatformApplicationAttributesRequestMarshaller();
            var unmarshaller = GetPlatformApplicationAttributesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetPlatformApplicationAttributesRequest,GetPlatformApplicationAttributesResponse> responseObject 
                            = new AmazonServiceResult<GetPlatformApplicationAttributesRequest,GetPlatformApplicationAttributesResponse>((GetPlatformApplicationAttributesRequest)req, (GetPlatformApplicationAttributesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetPlatformApplicationAttributesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetSubscriptionAttributes

        /// <summary>
        /// Returns all of the properties of a subscription.
        /// </summary>
        /// <param name="subscriptionArn">The ARN of the subscription whose properties you want to get.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the GetSubscriptionAttributes service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        public void GetSubscriptionAttributesAsync(string subscriptionArn,  AmazonServiceCallback<GetSubscriptionAttributesRequest, GetSubscriptionAttributesResponse> callback, AsyncOptions options = null)
        {
            var request = new GetSubscriptionAttributesRequest();
            request.SubscriptionArn = subscriptionArn;
            GetSubscriptionAttributesAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the GetSubscriptionAttributes operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetSubscriptionAttributes operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetSubscriptionAttributesAsync(GetSubscriptionAttributesRequest request, AmazonServiceCallback<GetSubscriptionAttributesRequest, GetSubscriptionAttributesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetSubscriptionAttributesRequestMarshaller();
            var unmarshaller = GetSubscriptionAttributesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetSubscriptionAttributesRequest,GetSubscriptionAttributesResponse> responseObject 
                            = new AmazonServiceResult<GetSubscriptionAttributesRequest,GetSubscriptionAttributesResponse>((GetSubscriptionAttributesRequest)req, (GetSubscriptionAttributesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetSubscriptionAttributesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetTopicAttributes

        /// <summary>
        /// Returns all of the properties of a topic.     Topic properties returned might differ
        /// based on the authorization of the user.
        /// </summary>
        /// <param name="topicArn">The ARN of the topic whose properties you want to get.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the GetTopicAttributes service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        public void GetTopicAttributesAsync(string topicArn,  AmazonServiceCallback<GetTopicAttributesRequest, GetTopicAttributesResponse> callback, AsyncOptions options = null)
        {
            var request = new GetTopicAttributesRequest();
            request.TopicArn = topicArn;
            GetTopicAttributesAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the GetTopicAttributes operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetTopicAttributes operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetTopicAttributesAsync(GetTopicAttributesRequest request, AmazonServiceCallback<GetTopicAttributesRequest, GetTopicAttributesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetTopicAttributesRequestMarshaller();
            var unmarshaller = GetTopicAttributesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetTopicAttributesRequest,GetTopicAttributesResponse> responseObject 
                            = new AmazonServiceResult<GetTopicAttributesRequest,GetTopicAttributesResponse>((GetTopicAttributesRequest)req, (GetTopicAttributesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetTopicAttributesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListEndpointsByPlatformApplication

        /// <summary>
        /// Initiates the asynchronous execution of the ListEndpointsByPlatformApplication operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListEndpointsByPlatformApplication operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListEndpointsByPlatformApplicationAsync(ListEndpointsByPlatformApplicationRequest request, AmazonServiceCallback<ListEndpointsByPlatformApplicationRequest, ListEndpointsByPlatformApplicationResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListEndpointsByPlatformApplicationRequestMarshaller();
            var unmarshaller = ListEndpointsByPlatformApplicationResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListEndpointsByPlatformApplicationRequest,ListEndpointsByPlatformApplicationResponse> responseObject 
                            = new AmazonServiceResult<ListEndpointsByPlatformApplicationRequest,ListEndpointsByPlatformApplicationResponse>((ListEndpointsByPlatformApplicationRequest)req, (ListEndpointsByPlatformApplicationResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListEndpointsByPlatformApplicationRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListPlatformApplications

        /// <summary>
        /// Lists the platform application objects for the supported push notification services,
        ///       such as APNS and GCM. The results for <code>ListPlatformApplications</code>
        /// are paginated and return a limited list of applications, up to 100.      If additional
        /// records are available after the first page results, then a NextToken string will be
        /// returned.       To receive the next page, you call <code>ListPlatformApplications</code>
        /// using the NextToken string received from the previous call.       When there are no
        /// more records to return, NextToken will be null.            For more information, see
        /// <a href="http://docs.aws.amazon.com/sns/latest/dg/SNSMobilePush.html">Using Amazon
        /// SNS Mobile Push Notifications</a>.
        /// </summary>
         /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListPlatformApplications service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        public void ListPlatformApplicationsAsync(AmazonServiceCallback<ListPlatformApplicationsRequest, ListPlatformApplicationsResponse> callback, AsyncOptions options = null)
        {
            ListPlatformApplicationsAsync(new ListPlatformApplicationsRequest(), callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the ListPlatformApplications operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListPlatformApplications operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListPlatformApplicationsAsync(ListPlatformApplicationsRequest request, AmazonServiceCallback<ListPlatformApplicationsRequest, ListPlatformApplicationsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListPlatformApplicationsRequestMarshaller();
            var unmarshaller = ListPlatformApplicationsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListPlatformApplicationsRequest,ListPlatformApplicationsResponse> responseObject 
                            = new AmazonServiceResult<ListPlatformApplicationsRequest,ListPlatformApplicationsResponse>((ListPlatformApplicationsRequest)req, (ListPlatformApplicationsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListPlatformApplicationsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListSubscriptions

        /// <summary>
        /// Returns a list of the requester's subscriptions. Each call returns a limited list
        ///      of subscriptions, up to 100. If there are more subscriptions, a <code>NextToken</code>
        /// is also returned. Use the <code>NextToken</code> parameter in a       new <code>ListSubscriptions</code>
        /// call to get further results.
        /// </summary>
         /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListSubscriptions service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        public void ListSubscriptionsAsync(AmazonServiceCallback<ListSubscriptionsRequest, ListSubscriptionsResponse> callback, AsyncOptions options = null)
        {
            ListSubscriptionsAsync(new ListSubscriptionsRequest(), callback, options);
        }


        /// <summary>
        /// Returns a list of the requester's subscriptions. Each call returns a limited list
        ///      of subscriptions, up to 100. If there are more subscriptions, a <code>NextToken</code>
        /// is also returned. Use the <code>NextToken</code> parameter in a       new <code>ListSubscriptions</code>
        /// call to get further results.
        /// </summary>
        /// <param name="nextToken">Token returned by the previous <code>ListSubscriptions</code> request.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListSubscriptions service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        public void ListSubscriptionsAsync(string nextToken,  AmazonServiceCallback<ListSubscriptionsRequest, ListSubscriptionsResponse> callback, AsyncOptions options = null)
        {
            var request = new ListSubscriptionsRequest();
            request.NextToken = nextToken;
            ListSubscriptionsAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the ListSubscriptions operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListSubscriptions operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListSubscriptionsAsync(ListSubscriptionsRequest request, AmazonServiceCallback<ListSubscriptionsRequest, ListSubscriptionsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListSubscriptionsRequestMarshaller();
            var unmarshaller = ListSubscriptionsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListSubscriptionsRequest,ListSubscriptionsResponse> responseObject 
                            = new AmazonServiceResult<ListSubscriptionsRequest,ListSubscriptionsResponse>((ListSubscriptionsRequest)req, (ListSubscriptionsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListSubscriptionsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListSubscriptionsByTopic

        /// <summary>
        /// Returns a list of the subscriptions to a specific topic. Each call returns       a
        /// limited list of subscriptions, up to 100. If there are more subscriptions, a <code>NextToken</code>
        /// is also returned. Use the <code>NextToken</code>       parameter in a new <code>ListSubscriptionsByTopic</code>
        /// call to get further results.
        /// </summary>
        /// <param name="topicArn">The ARN of the topic for which you wish to find subscriptions.</param>
        /// <param name="nextToken">Token returned by the previous <code>ListSubscriptionsByTopic</code> request.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListSubscriptionsByTopic service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        public void ListSubscriptionsByTopicAsync(string topicArn, string nextToken,  AmazonServiceCallback<ListSubscriptionsByTopicRequest, ListSubscriptionsByTopicResponse> callback, AsyncOptions options = null)
        {
            var request = new ListSubscriptionsByTopicRequest();
            request.TopicArn = topicArn;
            request.NextToken = nextToken;
            ListSubscriptionsByTopicAsync(request, callback, options);
        }


        /// <summary>
        /// Returns a list of the subscriptions to a specific topic. Each call returns       a
        /// limited list of subscriptions, up to 100. If there are more subscriptions, a <code>NextToken</code>
        /// is also returned. Use the <code>NextToken</code>       parameter in a new <code>ListSubscriptionsByTopic</code>
        /// call to get further results.
        /// </summary>
        /// <param name="topicArn">The ARN of the topic for which you wish to find subscriptions.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListSubscriptionsByTopic service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        public void ListSubscriptionsByTopicAsync(string topicArn,  AmazonServiceCallback<ListSubscriptionsByTopicRequest, ListSubscriptionsByTopicResponse> callback, AsyncOptions options = null)
        {
            var request = new ListSubscriptionsByTopicRequest();
            request.TopicArn = topicArn;
            ListSubscriptionsByTopicAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the ListSubscriptionsByTopic operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListSubscriptionsByTopic operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListSubscriptionsByTopicAsync(ListSubscriptionsByTopicRequest request, AmazonServiceCallback<ListSubscriptionsByTopicRequest, ListSubscriptionsByTopicResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListSubscriptionsByTopicRequestMarshaller();
            var unmarshaller = ListSubscriptionsByTopicResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListSubscriptionsByTopicRequest,ListSubscriptionsByTopicResponse> responseObject 
                            = new AmazonServiceResult<ListSubscriptionsByTopicRequest,ListSubscriptionsByTopicResponse>((ListSubscriptionsByTopicRequest)req, (ListSubscriptionsByTopicResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListSubscriptionsByTopicRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListTopics

        /// <summary>
        /// Returns a list of the requester's topics. Each call returns a limited list of topics,
        /// up to 100. If      there are more topics, a <code>NextToken</code> is also returned.
        /// Use the <code>NextToken</code> parameter in a new <code>ListTopics</code> call to
        /// get      further results.
        /// </summary>
         /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListTopics service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        public void ListTopicsAsync(AmazonServiceCallback<ListTopicsRequest, ListTopicsResponse> callback, AsyncOptions options = null)
        {
            ListTopicsAsync(new ListTopicsRequest(), callback, options);
        }


        /// <summary>
        /// Returns a list of the requester's topics. Each call returns a limited list of topics,
        /// up to 100. If      there are more topics, a <code>NextToken</code> is also returned.
        /// Use the <code>NextToken</code> parameter in a new <code>ListTopics</code> call to
        /// get      further results.
        /// </summary>
        /// <param name="nextToken">Token returned by the previous <code>ListTopics</code> request.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListTopics service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        public void ListTopicsAsync(string nextToken,  AmazonServiceCallback<ListTopicsRequest, ListTopicsResponse> callback, AsyncOptions options = null)
        {
            var request = new ListTopicsRequest();
            request.NextToken = nextToken;
            ListTopicsAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the ListTopics operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListTopics operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListTopicsAsync(ListTopicsRequest request, AmazonServiceCallback<ListTopicsRequest, ListTopicsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListTopicsRequestMarshaller();
            var unmarshaller = ListTopicsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListTopicsRequest,ListTopicsResponse> responseObject 
                            = new AmazonServiceResult<ListTopicsRequest,ListTopicsResponse>((ListTopicsRequest)req, (ListTopicsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListTopicsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  Publish

        /// <summary>
        /// Sends a message to all of a topic's subscribed endpoints. When a    <code>messageId</code>
        /// is returned, the message has been saved and Amazon SNS will attempt to deliver it
        ///       to the topic's subscribers shortly. The format of the outgoing message to each
        ///      subscribed endpoint depends on the notification protocol selected.
        /// 
        ///     
        /// <para>
        /// To use the <code>Publish</code> action for sending a message to a mobile endpoint,
        /// such as an app on a Kindle device or mobile phone,       you must specify the EndpointArn.
        /// The EndpointArn is returned when making a call with the <code>CreatePlatformEndpoint</code>
        /// action.       The second example below shows a request and response for publishing
        /// to a mobile endpoint.    
        /// </para>
        /// </summary>
        /// <param name="topicArn">The topic you want to publish to.</param>
        /// <param name="message">The message you want to send to the topic. If you want to send the same message to all transport protocols,    include the text of the message as a String value. If you want to send different messages for each transport protocol,    set the value of the <code>MessageStructure</code> parameter to <code>json</code>    and use a JSON object for the <code>Message</code> parameter.    See the Examples section for the format of the JSON object.  Constraints: Messages must be UTF-8 encoded   strings at most 256 KB in size (262144 bytes, not 262144 characters). JSON-specific constraints:   <ul>  <li>Keys in the JSON object that correspond to supported transport   protocols must have simple JSON string values. </li>  <li>The values will be parsed (unescaped)  before they are used in outgoing messages.</li>  <li>Outbound notifications are JSON  encoded (meaning that the characters will be reescaped for sending).</li>  <li>Values have a minimum length of 0 (the empty string, "", is allowed).</li>  <li>Values have a maximum length bounded by the overall message size (so, including  multiple protocols may limit message sizes).</li>  <li>Non-string values will cause the key  to be ignored.</li>  <li>Keys that do not correspond to supported transport protocols are ignored.</li>  <li>Duplicate keys are not allowed.</li>  <li>Failure to parse or validate any key or    value in the message will cause the <code>Publish</code> call to return an error (no partial   delivery).</li>   </ul>  </param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the Publish service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.EndpointDisabledException">
        /// Exception error indicating endpoint disabled.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterValueException">
        /// 
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.PlatformApplicationDisabledException">
        /// Exception error indicating platform application disabled.
        /// </exception>
        public void PublishAsync(string topicArn, string message,  AmazonServiceCallback<PublishRequest, PublishResponse> callback, AsyncOptions options = null)
        {
            var request = new PublishRequest();
            request.TopicArn = topicArn;
            request.Message = message;
            PublishAsync(request, callback, options);
        }


        /// <summary>
        /// Sends a message to all of a topic's subscribed endpoints. When a    <code>messageId</code>
        /// is returned, the message has been saved and Amazon SNS will attempt to deliver it
        ///       to the topic's subscribers shortly. The format of the outgoing message to each
        ///      subscribed endpoint depends on the notification protocol selected.
        /// 
        ///     
        /// <para>
        /// To use the <code>Publish</code> action for sending a message to a mobile endpoint,
        /// such as an app on a Kindle device or mobile phone,       you must specify the EndpointArn.
        /// The EndpointArn is returned when making a call with the <code>CreatePlatformEndpoint</code>
        /// action.       The second example below shows a request and response for publishing
        /// to a mobile endpoint.    
        /// </para>
        /// </summary>
        /// <param name="topicArn">The topic you want to publish to.</param>
        /// <param name="message">The message you want to send to the topic. If you want to send the same message to all transport protocols,    include the text of the message as a String value. If you want to send different messages for each transport protocol,    set the value of the <code>MessageStructure</code> parameter to <code>json</code>    and use a JSON object for the <code>Message</code> parameter.    See the Examples section for the format of the JSON object.  Constraints: Messages must be UTF-8 encoded   strings at most 256 KB in size (262144 bytes, not 262144 characters). JSON-specific constraints:   <ul>  <li>Keys in the JSON object that correspond to supported transport   protocols must have simple JSON string values. </li>  <li>The values will be parsed (unescaped)  before they are used in outgoing messages.</li>  <li>Outbound notifications are JSON  encoded (meaning that the characters will be reescaped for sending).</li>  <li>Values have a minimum length of 0 (the empty string, "", is allowed).</li>  <li>Values have a maximum length bounded by the overall message size (so, including  multiple protocols may limit message sizes).</li>  <li>Non-string values will cause the key  to be ignored.</li>  <li>Keys that do not correspond to supported transport protocols are ignored.</li>  <li>Duplicate keys are not allowed.</li>  <li>Failure to parse or validate any key or    value in the message will cause the <code>Publish</code> call to return an error (no partial   delivery).</li>   </ul>  </param>
        /// <param name="subject">Optional parameter to be used as the "Subject" line when the message is   delivered to email endpoints. This field will also be included, if present,    in the standard JSON messages delivered to other endpoints. Constraints: Subjects must be ASCII text that begins with a letter, number,    or punctuation mark; must not include line breaks or control characters; and    must be less than 100 characters long.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the Publish service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.EndpointDisabledException">
        /// Exception error indicating endpoint disabled.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterValueException">
        /// 
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.PlatformApplicationDisabledException">
        /// Exception error indicating platform application disabled.
        /// </exception>
        public void PublishAsync(string topicArn, string message, string subject,  AmazonServiceCallback<PublishRequest, PublishResponse> callback, AsyncOptions options = null)
        {
            var request = new PublishRequest();
            request.TopicArn = topicArn;
            request.Message = message;
            request.Subject = subject;
            PublishAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the Publish operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the Publish operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void PublishAsync(PublishRequest request, AmazonServiceCallback<PublishRequest, PublishResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new PublishRequestMarshaller();
            var unmarshaller = PublishResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<PublishRequest,PublishResponse> responseObject 
                            = new AmazonServiceResult<PublishRequest,PublishResponse>((PublishRequest)req, (PublishResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<PublishRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  RemovePermission

        /// <summary>
        /// Removes a statement from a topic's access control policy.
        /// </summary>
        /// <param name="topicArn">The ARN of the topic whose access control policy you wish to modify.</param>
        /// <param name="label">The unique label of the statement you want to remove.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the RemovePermission service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        public void RemovePermissionAsync(string topicArn, string label,  AmazonServiceCallback<RemovePermissionRequest, RemovePermissionResponse> callback, AsyncOptions options = null)
        {
            var request = new RemovePermissionRequest();
            request.TopicArn = topicArn;
            request.Label = label;
            RemovePermissionAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the RemovePermission operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the RemovePermission operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void RemovePermissionAsync(RemovePermissionRequest request, AmazonServiceCallback<RemovePermissionRequest, RemovePermissionResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new RemovePermissionRequestMarshaller();
            var unmarshaller = RemovePermissionResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<RemovePermissionRequest,RemovePermissionResponse> responseObject 
                            = new AmazonServiceResult<RemovePermissionRequest,RemovePermissionResponse>((RemovePermissionRequest)req, (RemovePermissionResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<RemovePermissionRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  SetEndpointAttributes

        /// <summary>
        /// Initiates the asynchronous execution of the SetEndpointAttributes operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the SetEndpointAttributes operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void SetEndpointAttributesAsync(SetEndpointAttributesRequest request, AmazonServiceCallback<SetEndpointAttributesRequest, SetEndpointAttributesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new SetEndpointAttributesRequestMarshaller();
            var unmarshaller = SetEndpointAttributesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<SetEndpointAttributesRequest,SetEndpointAttributesResponse> responseObject 
                            = new AmazonServiceResult<SetEndpointAttributesRequest,SetEndpointAttributesResponse>((SetEndpointAttributesRequest)req, (SetEndpointAttributesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<SetEndpointAttributesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  SetPlatformApplicationAttributes

        /// <summary>
        /// Initiates the asynchronous execution of the SetPlatformApplicationAttributes operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the SetPlatformApplicationAttributes operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void SetPlatformApplicationAttributesAsync(SetPlatformApplicationAttributesRequest request, AmazonServiceCallback<SetPlatformApplicationAttributesRequest, SetPlatformApplicationAttributesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new SetPlatformApplicationAttributesRequestMarshaller();
            var unmarshaller = SetPlatformApplicationAttributesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<SetPlatformApplicationAttributesRequest,SetPlatformApplicationAttributesResponse> responseObject 
                            = new AmazonServiceResult<SetPlatformApplicationAttributesRequest,SetPlatformApplicationAttributesResponse>((SetPlatformApplicationAttributesRequest)req, (SetPlatformApplicationAttributesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<SetPlatformApplicationAttributesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  SetSubscriptionAttributes

        /// <summary>
        /// Allows a subscription owner to set an attribute of the topic to a new value.
        /// </summary>
        /// <param name="subscriptionArn">The ARN of the subscription to modify.</param>
        /// <param name="attributeName">The name of the attribute you want to set. Only a subset of the subscriptions attributes are mutable. Valid values: <code>DeliveryPolicy</code> | <code>RawMessageDelivery</code></param>
        /// <param name="attributeValue">The new value for the attribute in JSON format.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the SetSubscriptionAttributes service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        public void SetSubscriptionAttributesAsync(string subscriptionArn, string attributeName, string attributeValue,  AmazonServiceCallback<SetSubscriptionAttributesRequest, SetSubscriptionAttributesResponse> callback, AsyncOptions options = null)
        {
            var request = new SetSubscriptionAttributesRequest();
            request.SubscriptionArn = subscriptionArn;
            request.AttributeName = attributeName;
            request.AttributeValue = attributeValue;
            SetSubscriptionAttributesAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the SetSubscriptionAttributes operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the SetSubscriptionAttributes operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void SetSubscriptionAttributesAsync(SetSubscriptionAttributesRequest request, AmazonServiceCallback<SetSubscriptionAttributesRequest, SetSubscriptionAttributesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new SetSubscriptionAttributesRequestMarshaller();
            var unmarshaller = SetSubscriptionAttributesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<SetSubscriptionAttributesRequest,SetSubscriptionAttributesResponse> responseObject 
                            = new AmazonServiceResult<SetSubscriptionAttributesRequest,SetSubscriptionAttributesResponse>((SetSubscriptionAttributesRequest)req, (SetSubscriptionAttributesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<SetSubscriptionAttributesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  SetTopicAttributes

        /// <summary>
        /// Allows a topic owner to set an attribute of the topic to a new value.
        /// </summary>
        /// <param name="topicArn">The ARN of the topic to modify.</param>
        /// <param name="attributeName">The name of the attribute you want to set. Only a subset of the topic's attributes are mutable. Valid values: <code>Policy</code> | <code>DisplayName</code> | <code>DeliveryPolicy</code></param>
        /// <param name="attributeValue">The new value for the attribute.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the SetTopicAttributes service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        public void SetTopicAttributesAsync(string topicArn, string attributeName, string attributeValue,  AmazonServiceCallback<SetTopicAttributesRequest, SetTopicAttributesResponse> callback, AsyncOptions options = null)
        {
            var request = new SetTopicAttributesRequest();
            request.TopicArn = topicArn;
            request.AttributeName = attributeName;
            request.AttributeValue = attributeValue;
            SetTopicAttributesAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the SetTopicAttributes operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the SetTopicAttributes operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void SetTopicAttributesAsync(SetTopicAttributesRequest request, AmazonServiceCallback<SetTopicAttributesRequest, SetTopicAttributesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new SetTopicAttributesRequestMarshaller();
            var unmarshaller = SetTopicAttributesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<SetTopicAttributesRequest,SetTopicAttributesResponse> responseObject 
                            = new AmazonServiceResult<SetTopicAttributesRequest,SetTopicAttributesResponse>((SetTopicAttributesRequest)req, (SetTopicAttributesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<SetTopicAttributesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  Subscribe

        /// <summary>
        /// Prepares to subscribe an endpoint by sending the endpoint a confirmation message.
        /// To actually create a      subscription, the endpoint owner must call the <code>ConfirmSubscription</code>
        ///      action with the token from the confirmation message. Confirmation tokens are
        ///      valid for three days.
        /// </summary>
        /// <param name="topicArn">The ARN of the topic you want to subscribe to.</param>
        /// <param name="protocol">The protocol you want to use. Supported protocols include: <ul>   <li><code>http</code> -- delivery of JSON-encoded message via HTTP POST</li>   <li><code>https</code> -- delivery of JSON-encoded message via HTTPS POST</li>   <li><code>email</code> -- delivery of message via SMTP</li>   <li><code>email-json</code> -- delivery of JSON-encoded message via SMTP</li>   <li><code>sms</code> -- delivery of message via SMS</li>   <li><code>sqs</code> -- delivery of JSON-encoded message to an Amazon SQS queue</li>   <li><code>application</code> -- delivery of JSON-encoded message to an EndpointArn for a mobile app and device.</li> </ul></param>
        /// <param name="endpoint">The endpoint that you want to receive notifications. Endpoints vary by protocol: <ul>   <li>For the <code>http</code> protocol, the endpoint is an URL beginning with "http://"</li>   <li>For the <code>https</code> protocol, the endpoint is a URL beginning with "https://"</li>   <li>For the <code>email</code> protocol, the endpoint is an email address</li>   <li>For the <code>email-json</code> protocol, the endpoint is an email address</li>   <li>For the <code>sms</code> protocol, the endpoint is a phone number of an SMS-enabled device</li>   <li>For the <code>sqs</code> protocol, the endpoint is the ARN of an Amazon SQS queue</li>   <li>For the <code>application</code> protocol, the endpoint is the EndpointArn of a mobile app and device.</li> </ul></param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the Subscribe service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.SubscriptionLimitExceededException">
        /// Indicates that the customer already owns the maximum allowed number of subscriptions.
        /// </exception>
        public void SubscribeAsync(string topicArn, string protocol, string endpoint,  AmazonServiceCallback<SubscribeRequest, SubscribeResponse> callback, AsyncOptions options = null)
        {
            var request = new SubscribeRequest();
            request.TopicArn = topicArn;
            request.Protocol = protocol;
            request.Endpoint = endpoint;
            SubscribeAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the Subscribe operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the Subscribe operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void SubscribeAsync(SubscribeRequest request, AmazonServiceCallback<SubscribeRequest, SubscribeResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new SubscribeRequestMarshaller();
            var unmarshaller = SubscribeResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<SubscribeRequest,SubscribeResponse> responseObject 
                            = new AmazonServiceResult<SubscribeRequest,SubscribeResponse>((SubscribeRequest)req, (SubscribeResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<SubscribeRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  Unsubscribe

        /// <summary>
        /// Deletes a subscription. If the subscription requires authentication for       deletion,
        /// only the owner of the subscription or the topic's owner       can unsubscribe, and
        /// an AWS signature is required. If the       <code>Unsubscribe</code> call does not
        /// require authentication and the requester is not       the subscription owner, a final
        /// cancellation message is delivered to the       endpoint, so that the endpoint owner
        /// can easily resubscribe to the topic if       the <code>Unsubscribe</code> request
        /// was unintended.
        /// </summary>
        /// <param name="subscriptionArn">The ARN of the subscription to be deleted.</param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the Unsubscribe service method, as returned by SimpleNotificationService.</returns>
        /// <exception cref="Amazon.SimpleNotificationService.Model.AuthorizationErrorException">
        /// Indicates that the user has been denied access to the requested resource.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InternalErrorException">
        /// Indicates an internal service error.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.InvalidParameterException">
        /// Indicates that a request parameter does not comply with the associated constraints.
        /// </exception>
        /// <exception cref="Amazon.SimpleNotificationService.Model.NotFoundException">
        /// Indicates that the requested resource does not exist.
        /// </exception>
        public void UnsubscribeAsync(string subscriptionArn,  AmazonServiceCallback<UnsubscribeRequest, UnsubscribeResponse> callback, AsyncOptions options = null)
        {
            var request = new UnsubscribeRequest();
            request.SubscriptionArn = subscriptionArn;
            UnsubscribeAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the Unsubscribe operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the Unsubscribe operation on AmazonSimpleNotificationServiceClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void UnsubscribeAsync(UnsubscribeRequest request, AmazonServiceCallback<UnsubscribeRequest, UnsubscribeResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new UnsubscribeRequestMarshaller();
            var unmarshaller = UnsubscribeResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<UnsubscribeRequest,UnsubscribeResponse> responseObject 
                            = new AmazonServiceResult<UnsubscribeRequest,UnsubscribeResponse>((UnsubscribeRequest)req, (UnsubscribeResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<UnsubscribeRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
    }
}