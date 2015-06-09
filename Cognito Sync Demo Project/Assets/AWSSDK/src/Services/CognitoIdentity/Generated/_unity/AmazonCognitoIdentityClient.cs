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
 * Do not modify this file. This file is generated from the cognito-identity-2014-06-30.normal.json service model.
 */


using System;
using System.Collections.Generic;

using Amazon.CognitoIdentity.Model;
using Amazon.CognitoIdentity.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.CognitoIdentity
{
    /// <summary>
    /// Implementation for accessing CognitoIdentity
    ///
    /// Amazon Cognito 
    /// <para>
    /// Amazon Cognito is a web service that delivers scoped temporary credentials to mobile
    /// devices and other untrusted environments. Amazon Cognito uniquely identifies a device
    /// and supplies the user with a consistent identity over the lifetime of an application.
    /// </para>
    ///  
    /// <para>
    /// Using Amazon Cognito, you can enable authentication with one or more third-party identity
    /// providers (Facebook, Google, or Login with Amazon), and you can also choose to support
    /// unauthenticated access from your app. Cognito delivers a unique identifier for each
    /// user and acts as an OpenID token provider trusted by AWS Security Token Service (STS)
    /// to access temporary, limited-privilege AWS credentials.
    /// </para>
    ///  
    /// <para>
    /// To provide end-user credentials, first make an unsigned call to <a>GetId</a>. If the
    /// end user is authenticated with one of the supported identity providers, set the <code>Logins</code>
    /// map with the identity provider token. <code>GetId</code> returns a unique identifier
    /// for the user.
    /// </para>
    ///  
    /// <para>
    /// Next, make an unsigned call to <a>GetCredentialsForIdentity</a>. This call expects
    /// the same <code>Logins</code> map as the <code>GetId</code> call, as well as the <code>IdentityID</code>
    /// originally returned by <code>GetId</code>. Assuming your identity pool has been configured
    /// via the <a>SetIdentityPoolRoles</a> operation, <code>GetCredentialsForIdentity</code>
    /// will return AWS credentials for your use. If your pool has not been configured with
    /// <code>SetIdentityPoolRoles</code>, or if you want to follow legacy flow, make an unsigned
    /// call to <a>GetOpenIdToken</a>, which returns the OpenID token necessary to call STS
    /// and retrieve AWS credentials. This call expects the same <code>Logins</code> map as
    /// the <code>GetId</code> call, as well as the <code>IdentityID</code> originally returned
    /// by <code>GetId</code>. The token returned by <code>GetOpenIdToken</code> can be passed
    /// to the STS operation <a href="http://docs.aws.amazon.com/STS/latest/APIReference/API_AssumeRoleWithWebIdentity.html">AssumeRoleWithWebIdentity</a>
    /// to retrieve AWS credentials.
    /// </para>
    ///  
    /// <para>
    /// If you want to use Amazon Cognito in an Android, iOS, or Unity application, you will
    /// probably want to make API calls via the AWS Mobile SDK. To learn more, see the <a
    /// href="http://docs.aws.amazon.com/mobile/index.html">AWS Mobile SDK Developer Guide</a>.
    /// </para>
    /// </summary>
    public partial class AmazonCognitoIdentityClient : AmazonUnityServiceClient, IAmazonCognitoIdentity
    {
        #region Constructors

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        public AmazonCognitoIdentityClient(AWSCredentials credentials)
            : this(credentials, new AmazonCognitoIdentityConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="region">The region to connect.</param>
        public AmazonCognitoIdentityClient(AWSCredentials credentials, RegionEndpoint region)
            : this(credentials, new AmazonCognitoIdentityConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with AWS Credentials and an
        /// AmazonCognitoIdentityClient Configuration object.
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="clientConfig">The AmazonCognitoIdentityClient Configuration Object</param>
        public AmazonCognitoIdentityClient(AWSCredentials credentials, AmazonCognitoIdentityConfig clientConfig)
            : base(credentials, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        public AmazonCognitoIdentityClient(string awsAccessKeyId, string awsSecretAccessKey)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonCognitoIdentityConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="region">The region to connect.</param>
        public AmazonCognitoIdentityClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonCognitoIdentityConfig() {RegionEndpoint=region})
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonCognitoIdentityClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="clientConfig">The AmazonCognitoIdentityClient Configuration Object</param>
        public AmazonCognitoIdentityClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonCognitoIdentityConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        public AmazonCognitoIdentityClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonCognitoIdentityConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="region">The region to connect.</param>
        public AmazonCognitoIdentityClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonCognitoIdentityConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonCognitoIdentityClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="clientConfig">The AmazonCognitoIdentityClient Configuration Object</param>
        public AmazonCognitoIdentityClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, AmazonCognitoIdentityConfig clientConfig)
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
            pipeline.ReplaceHandler<Amazon.Runtime.Internal.ErrorHandler>(new Amazon.Runtime.Internal.ErrorHandler<AmazonCognitoIdentityException>(this.Logger));
        }    

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

        
        #region  CreateIdentityPool


        /// <summary>
        /// Initiates the asynchronous execution of the CreateIdentityPool operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CreateIdentityPool operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void CreateIdentityPoolAsync(CreateIdentityPoolRequest request, AmazonServiceCallback<CreateIdentityPoolRequest, CreateIdentityPoolResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new CreateIdentityPoolRequestMarshaller();
            var unmarshaller = CreateIdentityPoolResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<CreateIdentityPoolRequest,CreateIdentityPoolResponse> responseObject 
                            = new AmazonServiceResult<CreateIdentityPoolRequest,CreateIdentityPoolResponse>((CreateIdentityPoolRequest)req, (CreateIdentityPoolResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<CreateIdentityPoolRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Creates a new identity pool. The identity pool is a store of user identity information
        /// that is specific to your AWS account. The limit on identity pools is 60 per account.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the CreateIdentityPool service method.</param>
        /// 
        /// <returns>The response from the CreateIdentityPool service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.LimitExceededException">
        /// Thrown when the total number of user pools has exceeded a preset limit.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal CreateIdentityPoolResponse CreateIdentityPool(CreateIdentityPoolRequest request)
        {
            var marshaller = new CreateIdentityPoolRequestMarshaller();
            var unmarshaller = CreateIdentityPoolResponseUnmarshaller.Instance;

            return Invoke<CreateIdentityPoolRequest,CreateIdentityPoolResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  DeleteIdentityPool

        /// <summary>
        /// Deletes a user pool. Once a pool is deleted, users will not be able to authenticate
        /// with the pool.
        /// </summary>
        /// <param name="identityPoolId">An identity pool ID in the format REGION:GUID.</param>
        /// 
        /// <returns>The response from the DeleteIdentityPool service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        public void DeleteIdentityPoolAsync(string identityPoolId, AmazonServiceCallback<DeleteIdentityPoolRequest, DeleteIdentityPoolResponse> callback, AsyncOptions options = null)
        {
            var request = new DeleteIdentityPoolRequest();
            request.IdentityPoolId = identityPoolId;
            DeleteIdentityPoolAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the DeleteIdentityPool operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DeleteIdentityPool operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DeleteIdentityPoolAsync(DeleteIdentityPoolRequest request, AmazonServiceCallback<DeleteIdentityPoolRequest, DeleteIdentityPoolResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DeleteIdentityPoolRequestMarshaller();
            var unmarshaller = DeleteIdentityPoolResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DeleteIdentityPoolRequest,DeleteIdentityPoolResponse> responseObject 
                            = new AmazonServiceResult<DeleteIdentityPoolRequest,DeleteIdentityPoolResponse>((DeleteIdentityPoolRequest)req, (DeleteIdentityPoolResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DeleteIdentityPoolRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Deletes a user pool. Once a pool is deleted, users will not be able to authenticate
        /// with the pool.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteIdentityPool service method.</param>
        /// 
        /// <returns>The response from the DeleteIdentityPool service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal DeleteIdentityPoolResponse DeleteIdentityPool(DeleteIdentityPoolRequest request)
        {
            var marshaller = new DeleteIdentityPoolRequestMarshaller();
            var unmarshaller = DeleteIdentityPoolResponseUnmarshaller.Instance;

            return Invoke<DeleteIdentityPoolRequest,DeleteIdentityPoolResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  DescribeIdentity

        /// <summary>
        /// Returns metadata related to the given identity, including when the identity was created
        /// and any associated linked logins.
        /// </summary>
        /// <param name="identityId">A unique identifier in the format REGION:GUID.</param>
        /// 
        /// <returns>The response from the DescribeIdentity service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        public void DescribeIdentityAsync(string identityId, AmazonServiceCallback<DescribeIdentityRequest, DescribeIdentityResponse> callback, AsyncOptions options = null)
        {
            var request = new DescribeIdentityRequest();
            request.IdentityId = identityId;
            DescribeIdentityAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the DescribeIdentity operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeIdentity operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DescribeIdentityAsync(DescribeIdentityRequest request, AmazonServiceCallback<DescribeIdentityRequest, DescribeIdentityResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DescribeIdentityRequestMarshaller();
            var unmarshaller = DescribeIdentityResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DescribeIdentityRequest,DescribeIdentityResponse> responseObject 
                            = new AmazonServiceResult<DescribeIdentityRequest,DescribeIdentityResponse>((DescribeIdentityRequest)req, (DescribeIdentityResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DescribeIdentityRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Returns metadata related to the given identity, including when the identity was created
        /// and any associated linked logins.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DescribeIdentity service method.</param>
        /// 
        /// <returns>The response from the DescribeIdentity service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal DescribeIdentityResponse DescribeIdentity(DescribeIdentityRequest request)
        {
            var marshaller = new DescribeIdentityRequestMarshaller();
            var unmarshaller = DescribeIdentityResponseUnmarshaller.Instance;

            return Invoke<DescribeIdentityRequest,DescribeIdentityResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  DescribeIdentityPool

        /// <summary>
        /// Gets details about a particular identity pool, including the pool name, ID description,
        /// creation date, and current number of users.
        /// </summary>
        /// <param name="identityPoolId">An identity pool ID in the format REGION:GUID.</param>
        /// 
        /// <returns>The response from the DescribeIdentityPool service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        public void DescribeIdentityPoolAsync(string identityPoolId, AmazonServiceCallback<DescribeIdentityPoolRequest, DescribeIdentityPoolResponse> callback, AsyncOptions options = null)
        {
            var request = new DescribeIdentityPoolRequest();
            request.IdentityPoolId = identityPoolId;
            DescribeIdentityPoolAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the DescribeIdentityPool operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeIdentityPool operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void DescribeIdentityPoolAsync(DescribeIdentityPoolRequest request, AmazonServiceCallback<DescribeIdentityPoolRequest, DescribeIdentityPoolResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new DescribeIdentityPoolRequestMarshaller();
            var unmarshaller = DescribeIdentityPoolResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<DescribeIdentityPoolRequest,DescribeIdentityPoolResponse> responseObject 
                            = new AmazonServiceResult<DescribeIdentityPoolRequest,DescribeIdentityPoolResponse>((DescribeIdentityPoolRequest)req, (DescribeIdentityPoolResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<DescribeIdentityPoolRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Gets details about a particular identity pool, including the pool name, ID description,
        /// creation date, and current number of users.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DescribeIdentityPool service method.</param>
        /// 
        /// <returns>The response from the DescribeIdentityPool service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal DescribeIdentityPoolResponse DescribeIdentityPool(DescribeIdentityPoolRequest request)
        {
            var marshaller = new DescribeIdentityPoolRequestMarshaller();
            var unmarshaller = DescribeIdentityPoolResponseUnmarshaller.Instance;

            return Invoke<DescribeIdentityPoolRequest,DescribeIdentityPoolResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  GetCredentialsForIdentity

        /// <summary>
        /// Returns credentials for the the provided identity ID. Any provided logins will be
        /// validated against supported login providers. If the token is for cognito-identity.amazonaws.com,
        /// it will be passed through to AWS Security Token Service with the appropriate role
        /// for the token.
        /// </summary>
        /// <param name="identityId">A unique identifier in the format REGION:GUID.</param>
        /// 
        /// <returns>The response from the GetCredentialsForIdentity service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidIdentityPoolConfigurationException">
        /// Thrown if the identity pool has no role associated for the given auth type (auth/unauth)
        /// or if the AssumeRole fails.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        public void GetCredentialsForIdentityAsync(string identityId, AmazonServiceCallback<GetCredentialsForIdentityRequest, GetCredentialsForIdentityResponse> callback, AsyncOptions options = null)
        {
            var request = new GetCredentialsForIdentityRequest();
            request.IdentityId = identityId;
            GetCredentialsForIdentityAsync(request, callback, options);
        }

        /// <summary>
        /// Returns credentials for the the provided identity ID. Any provided logins will be
        /// validated against supported login providers. If the token is for cognito-identity.amazonaws.com,
        /// it will be passed through to AWS Security Token Service with the appropriate role
        /// for the token.
        /// </summary>
        /// <param name="identityId">A unique identifier in the format REGION:GUID.</param>
        /// <param name="logins">A set of optional name-value pairs that map provider names to provider tokens.</param>
        /// 
        /// <returns>The response from the GetCredentialsForIdentity service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidIdentityPoolConfigurationException">
        /// Thrown if the identity pool has no role associated for the given auth type (auth/unauth)
        /// or if the AssumeRole fails.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        public void GetCredentialsForIdentityAsync(string identityId, Dictionary<string, string> logins, AmazonServiceCallback<GetCredentialsForIdentityRequest, GetCredentialsForIdentityResponse> callback, AsyncOptions options = null)
        {
            var request = new GetCredentialsForIdentityRequest();
            request.IdentityId = identityId;
            request.Logins = logins;
            GetCredentialsForIdentityAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the GetCredentialsForIdentity operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetCredentialsForIdentity operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetCredentialsForIdentityAsync(GetCredentialsForIdentityRequest request, AmazonServiceCallback<GetCredentialsForIdentityRequest, GetCredentialsForIdentityResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetCredentialsForIdentityRequestMarshaller();
            var unmarshaller = GetCredentialsForIdentityResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetCredentialsForIdentityRequest,GetCredentialsForIdentityResponse> responseObject 
                            = new AmazonServiceResult<GetCredentialsForIdentityRequest,GetCredentialsForIdentityResponse>((GetCredentialsForIdentityRequest)req, (GetCredentialsForIdentityResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetCredentialsForIdentityRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Returns credentials for the the provided identity ID. Any provided logins will be
        /// validated against supported login providers. If the token is for cognito-identity.amazonaws.com,
        /// it will be passed through to AWS Security Token Service with the appropriate role
        /// for the token.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetCredentialsForIdentity service method.</param>
        /// 
        /// <returns>The response from the GetCredentialsForIdentity service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidIdentityPoolConfigurationException">
        /// Thrown if the identity pool has no role associated for the given auth type (auth/unauth)
        /// or if the AssumeRole fails.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal GetCredentialsForIdentityResponse GetCredentialsForIdentity(GetCredentialsForIdentityRequest request)
        {
            var marshaller = new GetCredentialsForIdentityRequestMarshaller();
            var unmarshaller = GetCredentialsForIdentityResponseUnmarshaller.Instance;

            return Invoke<GetCredentialsForIdentityRequest,GetCredentialsForIdentityResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  GetId


        /// <summary>
        /// Initiates the asynchronous execution of the GetId operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetId operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetIdAsync(GetIdRequest request, AmazonServiceCallback<GetIdRequest, GetIdResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetIdRequestMarshaller();
            var unmarshaller = GetIdResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetIdRequest,GetIdResponse> responseObject 
                            = new AmazonServiceResult<GetIdRequest,GetIdResponse>((GetIdRequest)req, (GetIdResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetIdRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Generates (or retrieves) a Cognito ID. Supplying multiple logins will create an implicit
        /// linked account.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetId service method.</param>
        /// 
        /// <returns>The response from the GetId service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.LimitExceededException">
        /// Thrown when the total number of user pools has exceeded a preset limit.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal GetIdResponse GetId(GetIdRequest request)
        {
            var marshaller = new GetIdRequestMarshaller();
            var unmarshaller = GetIdResponseUnmarshaller.Instance;

            return Invoke<GetIdRequest,GetIdResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  GetIdentityPoolRoles

        /// <summary>
        /// Gets the roles for an identity pool.
        /// </summary>
        /// <param name="identityPoolId">An identity pool ID in the format REGION:GUID.</param>
        /// 
        /// <returns>The response from the GetIdentityPoolRoles service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        public void GetIdentityPoolRolesAsync(string identityPoolId, AmazonServiceCallback<GetIdentityPoolRolesRequest, GetIdentityPoolRolesResponse> callback, AsyncOptions options = null)
        {
            var request = new GetIdentityPoolRolesRequest();
            request.IdentityPoolId = identityPoolId;
            GetIdentityPoolRolesAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the GetIdentityPoolRoles operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetIdentityPoolRoles operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetIdentityPoolRolesAsync(GetIdentityPoolRolesRequest request, AmazonServiceCallback<GetIdentityPoolRolesRequest, GetIdentityPoolRolesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetIdentityPoolRolesRequestMarshaller();
            var unmarshaller = GetIdentityPoolRolesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetIdentityPoolRolesRequest,GetIdentityPoolRolesResponse> responseObject 
                            = new AmazonServiceResult<GetIdentityPoolRolesRequest,GetIdentityPoolRolesResponse>((GetIdentityPoolRolesRequest)req, (GetIdentityPoolRolesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetIdentityPoolRolesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Gets the roles for an identity pool.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetIdentityPoolRoles service method.</param>
        /// 
        /// <returns>The response from the GetIdentityPoolRoles service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal GetIdentityPoolRolesResponse GetIdentityPoolRoles(GetIdentityPoolRolesRequest request)
        {
            var marshaller = new GetIdentityPoolRolesRequestMarshaller();
            var unmarshaller = GetIdentityPoolRolesResponseUnmarshaller.Instance;

            return Invoke<GetIdentityPoolRolesRequest,GetIdentityPoolRolesResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  GetOpenIdToken

        /// <summary>
        /// Gets an OpenID token, using a known Cognito ID. This known Cognito ID is returned
        /// by <a>GetId</a>. You can optionally add additional logins for the identity. Supplying
        /// multiple logins creates an implicit link.
        /// 
        ///  
        /// <para>
        /// The OpenId token is valid for 15 minutes.
        /// </para>
        /// </summary>
        /// <param name="identityId">A unique identifier in the format REGION:GUID.</param>
        /// 
        /// <returns>The response from the GetOpenIdToken service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        public void GetOpenIdTokenAsync(string identityId, AmazonServiceCallback<GetOpenIdTokenRequest, GetOpenIdTokenResponse> callback, AsyncOptions options = null)
        {
            var request = new GetOpenIdTokenRequest();
            request.IdentityId = identityId;
            GetOpenIdTokenAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the GetOpenIdToken operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetOpenIdToken operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetOpenIdTokenAsync(GetOpenIdTokenRequest request, AmazonServiceCallback<GetOpenIdTokenRequest, GetOpenIdTokenResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetOpenIdTokenRequestMarshaller();
            var unmarshaller = GetOpenIdTokenResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetOpenIdTokenRequest,GetOpenIdTokenResponse> responseObject 
                            = new AmazonServiceResult<GetOpenIdTokenRequest,GetOpenIdTokenResponse>((GetOpenIdTokenRequest)req, (GetOpenIdTokenResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetOpenIdTokenRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Gets an OpenID token, using a known Cognito ID. This known Cognito ID is returned
        /// by <a>GetId</a>. You can optionally add additional logins for the identity. Supplying
        /// multiple logins creates an implicit link.
        /// 
        ///  
        /// <para>
        /// The OpenId token is valid for 15 minutes.
        /// </para>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetOpenIdToken service method.</param>
        /// 
        /// <returns>The response from the GetOpenIdToken service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal GetOpenIdTokenResponse GetOpenIdToken(GetOpenIdTokenRequest request)
        {
            var marshaller = new GetOpenIdTokenRequestMarshaller();
            var unmarshaller = GetOpenIdTokenResponseUnmarshaller.Instance;

            return Invoke<GetOpenIdTokenRequest,GetOpenIdTokenResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  GetOpenIdTokenForDeveloperIdentity


        /// <summary>
        /// Initiates the asynchronous execution of the GetOpenIdTokenForDeveloperIdentity operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetOpenIdTokenForDeveloperIdentity operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetOpenIdTokenForDeveloperIdentityAsync(GetOpenIdTokenForDeveloperIdentityRequest request, AmazonServiceCallback<GetOpenIdTokenForDeveloperIdentityRequest, GetOpenIdTokenForDeveloperIdentityResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetOpenIdTokenForDeveloperIdentityRequestMarshaller();
            var unmarshaller = GetOpenIdTokenForDeveloperIdentityResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetOpenIdTokenForDeveloperIdentityRequest,GetOpenIdTokenForDeveloperIdentityResponse> responseObject 
                            = new AmazonServiceResult<GetOpenIdTokenForDeveloperIdentityRequest,GetOpenIdTokenForDeveloperIdentityResponse>((GetOpenIdTokenForDeveloperIdentityRequest)req, (GetOpenIdTokenForDeveloperIdentityResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetOpenIdTokenForDeveloperIdentityRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Registers (or retrieves) a Cognito <code>IdentityId</code> and an OpenID Connect token
        /// for a user authenticated by your backend authentication process. Supplying multiple
        /// logins will create an implicit linked account. You can only specify one developer
        /// provider as part of the <code>Logins</code> map, which is linked to the identity pool.
        /// The developer provider is the "domain" by which Cognito will refer to your users.
        /// 
        ///  
        /// <para>
        /// You can use <code>GetOpenIdTokenForDeveloperIdentity</code> to create a new identity
        /// and to link new logins (that is, user credentials issued by a public provider or developer
        /// provider) to an existing identity. When you want to create a new identity, the <code>IdentityId</code>
        /// should be null. When you want to associate a new login with an existing authenticated/unauthenticated
        /// identity, you can do so by providing the existing <code>IdentityId</code>. This API
        /// will create the identity in the specified <code>IdentityPoolId</code>.
        /// </para>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetOpenIdTokenForDeveloperIdentity service method.</param>
        /// 
        /// <returns>The response from the GetOpenIdTokenForDeveloperIdentity service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.DeveloperUserAlreadyRegisteredException">
        /// The provided developer user identifier is already registered with Cognito under a
        /// different identity ID.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal GetOpenIdTokenForDeveloperIdentityResponse GetOpenIdTokenForDeveloperIdentity(GetOpenIdTokenForDeveloperIdentityRequest request)
        {
            var marshaller = new GetOpenIdTokenForDeveloperIdentityRequestMarshaller();
            var unmarshaller = GetOpenIdTokenForDeveloperIdentityResponseUnmarshaller.Instance;

            return Invoke<GetOpenIdTokenForDeveloperIdentityRequest,GetOpenIdTokenForDeveloperIdentityResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  ListIdentities


        /// <summary>
        /// Initiates the asynchronous execution of the ListIdentities operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListIdentities operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListIdentitiesAsync(ListIdentitiesRequest request, AmazonServiceCallback<ListIdentitiesRequest, ListIdentitiesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListIdentitiesRequestMarshaller();
            var unmarshaller = ListIdentitiesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListIdentitiesRequest,ListIdentitiesResponse> responseObject 
                            = new AmazonServiceResult<ListIdentitiesRequest,ListIdentitiesResponse>((ListIdentitiesRequest)req, (ListIdentitiesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListIdentitiesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Lists the identities in a pool.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListIdentities service method.</param>
        /// 
        /// <returns>The response from the ListIdentities service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal ListIdentitiesResponse ListIdentities(ListIdentitiesRequest request)
        {
            var marshaller = new ListIdentitiesRequestMarshaller();
            var unmarshaller = ListIdentitiesResponseUnmarshaller.Instance;

            return Invoke<ListIdentitiesRequest,ListIdentitiesResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  ListIdentityPools


        /// <summary>
        /// Initiates the asynchronous execution of the ListIdentityPools operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListIdentityPools operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListIdentityPoolsAsync(ListIdentityPoolsRequest request, AmazonServiceCallback<ListIdentityPoolsRequest, ListIdentityPoolsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListIdentityPoolsRequestMarshaller();
            var unmarshaller = ListIdentityPoolsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListIdentityPoolsRequest,ListIdentityPoolsResponse> responseObject 
                            = new AmazonServiceResult<ListIdentityPoolsRequest,ListIdentityPoolsResponse>((ListIdentityPoolsRequest)req, (ListIdentityPoolsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListIdentityPoolsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Lists all of the Cognito identity pools registered for your account.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListIdentityPools service method.</param>
        /// 
        /// <returns>The response from the ListIdentityPools service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal ListIdentityPoolsResponse ListIdentityPools(ListIdentityPoolsRequest request)
        {
            var marshaller = new ListIdentityPoolsRequestMarshaller();
            var unmarshaller = ListIdentityPoolsResponseUnmarshaller.Instance;

            return Invoke<ListIdentityPoolsRequest,ListIdentityPoolsResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  LookupDeveloperIdentity


        /// <summary>
        /// Initiates the asynchronous execution of the LookupDeveloperIdentity operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the LookupDeveloperIdentity operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void LookupDeveloperIdentityAsync(LookupDeveloperIdentityRequest request, AmazonServiceCallback<LookupDeveloperIdentityRequest, LookupDeveloperIdentityResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new LookupDeveloperIdentityRequestMarshaller();
            var unmarshaller = LookupDeveloperIdentityResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<LookupDeveloperIdentityRequest,LookupDeveloperIdentityResponse> responseObject 
                            = new AmazonServiceResult<LookupDeveloperIdentityRequest,LookupDeveloperIdentityResponse>((LookupDeveloperIdentityRequest)req, (LookupDeveloperIdentityResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<LookupDeveloperIdentityRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Retrieves the <code>IdentityID</code> associated with a <code>DeveloperUserIdentifier</code>
        /// or the list of <code>DeveloperUserIdentifier</code>s associated with an <code>IdentityId</code>
        /// for an existing identity. Either <code>IdentityID</code> or <code>DeveloperUserIdentifier</code>
        /// must not be null. If you supply only one of these values, the other value will be
        /// searched in the database and returned as a part of the response. If you supply both,
        /// <code>DeveloperUserIdentifier</code> will be matched against <code>IdentityID</code>.
        /// If the values are verified against the database, the response returns both values
        /// and is the same as the request. Otherwise a <code>ResourceConflictException</code>
        /// is thrown.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the LookupDeveloperIdentity service method.</param>
        /// 
        /// <returns>The response from the LookupDeveloperIdentity service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal LookupDeveloperIdentityResponse LookupDeveloperIdentity(LookupDeveloperIdentityRequest request)
        {
            var marshaller = new LookupDeveloperIdentityRequestMarshaller();
            var unmarshaller = LookupDeveloperIdentityResponseUnmarshaller.Instance;

            return Invoke<LookupDeveloperIdentityRequest,LookupDeveloperIdentityResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  MergeDeveloperIdentities


        /// <summary>
        /// Initiates the asynchronous execution of the MergeDeveloperIdentities operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the MergeDeveloperIdentities operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void MergeDeveloperIdentitiesAsync(MergeDeveloperIdentitiesRequest request, AmazonServiceCallback<MergeDeveloperIdentitiesRequest, MergeDeveloperIdentitiesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new MergeDeveloperIdentitiesRequestMarshaller();
            var unmarshaller = MergeDeveloperIdentitiesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<MergeDeveloperIdentitiesRequest,MergeDeveloperIdentitiesResponse> responseObject 
                            = new AmazonServiceResult<MergeDeveloperIdentitiesRequest,MergeDeveloperIdentitiesResponse>((MergeDeveloperIdentitiesRequest)req, (MergeDeveloperIdentitiesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<MergeDeveloperIdentitiesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Merges two users having different <code>IdentityId</code>s, existing in the same identity
        /// pool, and identified by the same developer provider. You can use this action to request
        /// that discrete users be merged and identified as a single user in the Cognito environment.
        /// Cognito associates the given source user (<code>SourceUserIdentifier</code>) with
        /// the <code>IdentityId</code> of the <code>DestinationUserIdentifier</code>. Only developer-authenticated
        /// users can be merged. If the users to be merged are associated with the same public
        /// provider, but as two different users, an exception will be thrown.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the MergeDeveloperIdentities service method.</param>
        /// 
        /// <returns>The response from the MergeDeveloperIdentities service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal MergeDeveloperIdentitiesResponse MergeDeveloperIdentities(MergeDeveloperIdentitiesRequest request)
        {
            var marshaller = new MergeDeveloperIdentitiesRequestMarshaller();
            var unmarshaller = MergeDeveloperIdentitiesResponseUnmarshaller.Instance;

            return Invoke<MergeDeveloperIdentitiesRequest,MergeDeveloperIdentitiesResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  SetIdentityPoolRoles

        /// <summary>
        /// Sets the roles for an identity pool. These roles are used when making calls to <code>GetCredentialsForIdentity</code>
        /// action.
        /// </summary>
        /// <param name="identityPoolId">An identity pool ID in the format REGION:GUID.</param>
        /// <param name="roles">The map of roles associated with this pool. Currently only authenticated and unauthenticated roles are supported.</param>
        /// 
        /// <returns>The response from the SetIdentityPoolRoles service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        public void SetIdentityPoolRolesAsync(string identityPoolId, Dictionary<string, string> roles, AmazonServiceCallback<SetIdentityPoolRolesRequest, SetIdentityPoolRolesResponse> callback, AsyncOptions options = null)
        {
            var request = new SetIdentityPoolRolesRequest();
            request.IdentityPoolId = identityPoolId;
            request.Roles = roles;
            SetIdentityPoolRolesAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the SetIdentityPoolRoles operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the SetIdentityPoolRoles operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void SetIdentityPoolRolesAsync(SetIdentityPoolRolesRequest request, AmazonServiceCallback<SetIdentityPoolRolesRequest, SetIdentityPoolRolesResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new SetIdentityPoolRolesRequestMarshaller();
            var unmarshaller = SetIdentityPoolRolesResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<SetIdentityPoolRolesRequest,SetIdentityPoolRolesResponse> responseObject 
                            = new AmazonServiceResult<SetIdentityPoolRolesRequest,SetIdentityPoolRolesResponse>((SetIdentityPoolRolesRequest)req, (SetIdentityPoolRolesResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<SetIdentityPoolRolesRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Sets the roles for an identity pool. These roles are used when making calls to <code>GetCredentialsForIdentity</code>
        /// action.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the SetIdentityPoolRoles service method.</param>
        /// 
        /// <returns>The response from the SetIdentityPoolRoles service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal SetIdentityPoolRolesResponse SetIdentityPoolRoles(SetIdentityPoolRolesRequest request)
        {
            var marshaller = new SetIdentityPoolRolesRequestMarshaller();
            var unmarshaller = SetIdentityPoolRolesResponseUnmarshaller.Instance;

            return Invoke<SetIdentityPoolRolesRequest,SetIdentityPoolRolesResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  UnlinkDeveloperIdentity


        /// <summary>
        /// Initiates the asynchronous execution of the UnlinkDeveloperIdentity operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the UnlinkDeveloperIdentity operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void UnlinkDeveloperIdentityAsync(UnlinkDeveloperIdentityRequest request, AmazonServiceCallback<UnlinkDeveloperIdentityRequest, UnlinkDeveloperIdentityResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new UnlinkDeveloperIdentityRequestMarshaller();
            var unmarshaller = UnlinkDeveloperIdentityResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<UnlinkDeveloperIdentityRequest,UnlinkDeveloperIdentityResponse> responseObject 
                            = new AmazonServiceResult<UnlinkDeveloperIdentityRequest,UnlinkDeveloperIdentityResponse>((UnlinkDeveloperIdentityRequest)req, (UnlinkDeveloperIdentityResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<UnlinkDeveloperIdentityRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Unlinks a <code>DeveloperUserIdentifier</code> from an existing identity. Unlinked
        /// developer users will be considered new identities next time they are seen. If, for
        /// a given Cognito identity, you remove all federated identities as well as the developer
        /// user identifier, the Cognito identity becomes inaccessible.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UnlinkDeveloperIdentity service method.</param>
        /// 
        /// <returns>The response from the UnlinkDeveloperIdentity service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal UnlinkDeveloperIdentityResponse UnlinkDeveloperIdentity(UnlinkDeveloperIdentityRequest request)
        {
            var marshaller = new UnlinkDeveloperIdentityRequestMarshaller();
            var unmarshaller = UnlinkDeveloperIdentityResponseUnmarshaller.Instance;

            return Invoke<UnlinkDeveloperIdentityRequest,UnlinkDeveloperIdentityResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  UnlinkIdentity


        /// <summary>
        /// Initiates the asynchronous execution of the UnlinkIdentity operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the UnlinkIdentity operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void UnlinkIdentityAsync(UnlinkIdentityRequest request, AmazonServiceCallback<UnlinkIdentityRequest, UnlinkIdentityResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new UnlinkIdentityRequestMarshaller();
            var unmarshaller = UnlinkIdentityResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<UnlinkIdentityRequest,UnlinkIdentityResponse> responseObject 
                            = new AmazonServiceResult<UnlinkIdentityRequest,UnlinkIdentityResponse>((UnlinkIdentityRequest)req, (UnlinkIdentityResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<UnlinkIdentityRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Unlinks a federated identity from an existing account. Unlinked logins will be considered
        /// new identities next time they are seen. Removing the last linked login will make this
        /// identity inaccessible.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UnlinkIdentity service method.</param>
        /// 
        /// <returns>The response from the UnlinkIdentity service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal UnlinkIdentityResponse UnlinkIdentity(UnlinkIdentityRequest request)
        {
            var marshaller = new UnlinkIdentityRequestMarshaller();
            var unmarshaller = UnlinkIdentityResponseUnmarshaller.Instance;

            return Invoke<UnlinkIdentityRequest,UnlinkIdentityResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
        #region  UpdateIdentityPool


        /// <summary>
        /// Initiates the asynchronous execution of the UpdateIdentityPool operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the UpdateIdentityPool operation on AmazonCognitoIdentityClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void UpdateIdentityPoolAsync(UpdateIdentityPoolRequest request, AmazonServiceCallback<UpdateIdentityPoolRequest, UpdateIdentityPoolResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new UpdateIdentityPoolRequestMarshaller();
            var unmarshaller = UpdateIdentityPoolResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<UpdateIdentityPoolRequest,UpdateIdentityPoolResponse> responseObject 
                            = new AmazonServiceResult<UpdateIdentityPoolRequest,UpdateIdentityPoolResponse>((UpdateIdentityPoolRequest)req, (UpdateIdentityPoolResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<UpdateIdentityPoolRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        /// <summary>
        /// Updates a user pool.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UpdateIdentityPool service method.</param>
        /// 
        /// <returns>The response from the UpdateIdentityPool service method, as returned by CognitoIdentity.</returns>
        /// <exception cref="Amazon.CognitoIdentity.Model.InternalErrorException">
        /// Thrown when the service encounters an error during processing the request.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.InvalidParameterException">
        /// Thrown for missing or bad input parameter(s).
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.NotAuthorizedException">
        /// Thrown when a user is not authorized to access the requested resource.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceConflictException">
        /// Thrown when a user tries to use a login which is already linked to another account.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.ResourceNotFoundException">
        /// Thrown when the requested resource (for example, a dataset or record) does not exist.
        /// </exception>
        /// <exception cref="Amazon.CognitoIdentity.Model.TooManyRequestsException">
        /// Thrown when a request is throttled.
        /// </exception>
        internal UpdateIdentityPoolResponse UpdateIdentityPool(UpdateIdentityPoolRequest request)
        {
            var marshaller = new UpdateIdentityPoolRequestMarshaller();
            var unmarshaller = UpdateIdentityPoolResponseUnmarshaller.Instance;

            return Invoke<UpdateIdentityPoolRequest,UpdateIdentityPoolResponse>(request, marshaller, unmarshaller);
        }
        #endregion
        
    }
}
