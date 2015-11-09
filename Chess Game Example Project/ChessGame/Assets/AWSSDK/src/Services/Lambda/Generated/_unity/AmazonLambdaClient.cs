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
 * Do not modify this file. This file is generated from the lambda-2015-03-31.normal.json service model.
 */


using System;
using System.Collections.Generic;

using Amazon.Lambda.Model;
using Amazon.Lambda.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.Lambda
{
    /// <summary>
    /// Implementation for accessing Lambda
    ///
    /// AWS Lambda 
    /// <para>
    /// <b>Overview</b>
    /// </para>
    ///  
    /// <para>
    /// This is the <i>AWS Lambda API Reference</i>. The AWS Lambda Developer Guide provides
    /// additional information. For the service overview, go to <a href="http://docs.aws.amazon.com/lambda/latest/dg/welcome.html">What
    /// is AWS Lambda</a>, and for information about how the service works, go to <a href="http://docs.aws.amazon.com/lambda/latest/dg/lambda-introduction.html">AWS
    /// Lambda: How it Works</a> in the <i>AWS Lambda Developer Guide</i>.
    /// </para>
    /// </summary>
    public partial class AmazonLambdaClient : AmazonUnityServiceClient, IAmazonLambda
    {
        #region Constructors

        /// <summary>
        /// Constructs AmazonLambdaClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        public AmazonLambdaClient(AWSCredentials credentials)
            : this(credentials, new AmazonLambdaConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonLambdaClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="region">The region to connect.</param>
        public AmazonLambdaClient(AWSCredentials credentials, RegionEndpoint region)
            : this(credentials, new AmazonLambdaConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonLambdaClient with AWS Credentials and an
        /// AmazonLambdaClient Configuration object.
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="clientConfig">The AmazonLambdaClient Configuration Object</param>
        public AmazonLambdaClient(AWSCredentials credentials, AmazonLambdaConfig clientConfig)
            : base(credentials, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonLambdaClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        public AmazonLambdaClient(string awsAccessKeyId, string awsSecretAccessKey)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonLambdaConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonLambdaClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="region">The region to connect.</param>
        public AmazonLambdaClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonLambdaConfig() {RegionEndpoint=region})
        {
        }

        /// <summary>
        /// Constructs AmazonLambdaClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonLambdaClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="clientConfig">The AmazonLambdaClient Configuration Object</param>
        public AmazonLambdaClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonLambdaConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonLambdaClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        public AmazonLambdaClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonLambdaConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonLambdaClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="region">The region to connect.</param>
        public AmazonLambdaClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonLambdaConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonLambdaClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonLambdaClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="clientConfig">The AmazonLambdaClient Configuration Object</param>
        public AmazonLambdaClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, AmazonLambdaConfig clientConfig)
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
            pipeline.ReplaceHandler<Amazon.Runtime.Internal.ErrorHandler>(new Amazon.Runtime.Internal.ErrorHandler<AmazonLambdaException>(this.Logger));
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
        /// Initiates the asynchronous execution of the AddPermission operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the AddPermission operation on AmazonLambdaClient.</param>
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
        
        #region  CreateEventSourceMapping

        /// <summary>
        /// Initiates the asynchronous execution of the CreateEventSourceMapping operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CreateEventSourceMapping operation on AmazonLambdaClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void CreateEventSourceMappingAsync(CreateEventSourceMappingRequest request, AmazonServiceCallback<CreateEventSourceMappingRequest, CreateEventSourceMappingResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new CreateEventSourceMappingRequestMarshaller();
            var unmarshaller = CreateEventSourceMappingResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<CreateEventSourceMappingRequest,CreateEventSourceMappingResponse> responseObject 
                            = new AmazonServiceResult<CreateEventSourceMappingRequest,CreateEventSourceMappingResponse>((CreateEventSourceMappingRequest)req, (CreateEventSourceMappingResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<CreateEventSourceMappingRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  CreateFunction

        /// <summary>
        /// Initiates the asynchronous execution of the CreateFunction operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CreateFunction operation on AmazonLambdaClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void CreateFunctionAsync(CreateFunctionRequest request, AmazonServiceCallback<CreateFunctionRequest, CreateFunctionResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new CreateFunctionRequestMarshaller();
            var unmarshaller = CreateFunctionResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<CreateFunctionRequest,CreateFunctionResponse> responseObject 
                            = new AmazonServiceResult<CreateFunctionRequest,CreateFunctionResponse>((CreateFunctionRequest)req, (CreateFunctionResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<CreateFunctionRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetEventSourceMapping

        /// <summary>
        /// Initiates the asynchronous execution of the GetEventSourceMapping operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetEventSourceMapping operation on AmazonLambdaClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetEventSourceMappingAsync(GetEventSourceMappingRequest request, AmazonServiceCallback<GetEventSourceMappingRequest, GetEventSourceMappingResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetEventSourceMappingRequestMarshaller();
            var unmarshaller = GetEventSourceMappingResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetEventSourceMappingRequest,GetEventSourceMappingResponse> responseObject 
                            = new AmazonServiceResult<GetEventSourceMappingRequest,GetEventSourceMappingResponse>((GetEventSourceMappingRequest)req, (GetEventSourceMappingResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetEventSourceMappingRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetFunction

        /// <summary>
        /// Returns the configuration information of the Lambda function and a presigned URL link
        /// to the .zip file you uploaded with <a>CreateFunction</a> so you can download the .zip
        /// file. Note that the URL is valid for up to 10 minutes. The configuration information
        /// is the same information you provided as parameters when uploading the function.
        /// 
        ///  
        /// <para>
        /// This operation requires permission for the <code>lambda:GetFunction</code> action.
        /// </para>
        /// </summary>
        /// <param name="functionName">The Lambda function name.   You can specify an unqualified function name (for example, "Thumbnail") or you can specify Amazon Resource Name (ARN) of the function (for example, "arn:aws:lambda:us-west-2:account-id:function:ThumbNail"). AWS Lambda also allows you to specify only the account ID qualifier (for example, "account-id:Thumbnail"). Note that the length constraint applies only to the ARN. If you specify only the function name, it is limited to 64 character in length. </param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the GetFunction service method, as returned by Lambda.</returns>
        /// <exception cref="Amazon.Lambda.Model.ResourceNotFoundException">
        /// The resource (for example, a Lambda function or access policy statement) specified
        /// in the request does not exist.
        /// </exception>
        /// <exception cref="Amazon.Lambda.Model.ServiceException">
        /// The AWS Lambda service encountered an internal error.
        /// </exception>
        /// <exception cref="Amazon.Lambda.Model.TooManyRequestsException">
        /// 
        /// </exception>
        public void GetFunctionAsync(string functionName,  AmazonServiceCallback<GetFunctionRequest, GetFunctionResponse> callback, AsyncOptions options = null)
        {
            var request = new GetFunctionRequest();
            request.FunctionName = functionName;
            GetFunctionAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the GetFunction operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetFunction operation on AmazonLambdaClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetFunctionAsync(GetFunctionRequest request, AmazonServiceCallback<GetFunctionRequest, GetFunctionResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetFunctionRequestMarshaller();
            var unmarshaller = GetFunctionResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetFunctionRequest,GetFunctionResponse> responseObject 
                            = new AmazonServiceResult<GetFunctionRequest,GetFunctionResponse>((GetFunctionRequest)req, (GetFunctionResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetFunctionRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetFunctionConfiguration

        /// <summary>
        /// Returns the configuration information of the Lambda function. This the same information
        /// you provided as parameters when uploading the function by using <a>CreateFunction</a>.
        /// 
        ///  
        /// <para>
        /// This operation requires permission for the <code>lambda:GetFunctionConfiguration</code>
        /// operation.
        /// </para>
        /// </summary>
        /// <param name="functionName">The name of the Lambda function for which you want to retrieve the configuration information.  You can specify an unqualified function name (for example, "Thumbnail") or you can specify Amazon Resource Name (ARN) of the function (for example, "arn:aws:lambda:us-west-2:account-id:function:ThumbNail"). AWS Lambda also allows you to specify only the account ID qualifier (for example, "account-id:Thumbnail"). Note that the length constraint applies only to the ARN. If you specify only the function name, it is limited to 64 character in length. </param>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the GetFunctionConfiguration service method, as returned by Lambda.</returns>
        /// <exception cref="Amazon.Lambda.Model.ResourceNotFoundException">
        /// The resource (for example, a Lambda function or access policy statement) specified
        /// in the request does not exist.
        /// </exception>
        /// <exception cref="Amazon.Lambda.Model.ServiceException">
        /// The AWS Lambda service encountered an internal error.
        /// </exception>
        /// <exception cref="Amazon.Lambda.Model.TooManyRequestsException">
        /// 
        /// </exception>
        public void GetFunctionConfigurationAsync(string functionName,  AmazonServiceCallback<GetFunctionConfigurationRequest, GetFunctionConfigurationResponse> callback, AsyncOptions options = null)
        {
            var request = new GetFunctionConfigurationRequest();
            request.FunctionName = functionName;
            GetFunctionConfigurationAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the GetFunctionConfiguration operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetFunctionConfiguration operation on AmazonLambdaClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetFunctionConfigurationAsync(GetFunctionConfigurationRequest request, AmazonServiceCallback<GetFunctionConfigurationRequest, GetFunctionConfigurationResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetFunctionConfigurationRequestMarshaller();
            var unmarshaller = GetFunctionConfigurationResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetFunctionConfigurationRequest,GetFunctionConfigurationResponse> responseObject 
                            = new AmazonServiceResult<GetFunctionConfigurationRequest,GetFunctionConfigurationResponse>((GetFunctionConfigurationRequest)req, (GetFunctionConfigurationResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetFunctionConfigurationRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  GetPolicy

        /// <summary>
        /// Initiates the asynchronous execution of the GetPolicy operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the GetPolicy operation on AmazonLambdaClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void GetPolicyAsync(GetPolicyRequest request, AmazonServiceCallback<GetPolicyRequest, GetPolicyResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new GetPolicyRequestMarshaller();
            var unmarshaller = GetPolicyResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<GetPolicyRequest,GetPolicyResponse> responseObject 
                            = new AmazonServiceResult<GetPolicyRequest,GetPolicyResponse>((GetPolicyRequest)req, (GetPolicyResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<GetPolicyRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  Invoke

        /// <summary>
        /// Initiates the asynchronous execution of the Invoke operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the Invoke operation on AmazonLambdaClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void InvokeAsync(InvokeRequest request, AmazonServiceCallback<InvokeRequest, InvokeResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new InvokeRequestMarshaller();
            var unmarshaller = InvokeResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<InvokeRequest,InvokeResponse> responseObject 
                            = new AmazonServiceResult<InvokeRequest,InvokeResponse>((InvokeRequest)req, (InvokeResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<InvokeRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  InvokeAsync

        /// <summary>
        /// Initiates the asynchronous execution of the InvokeAsync operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the InvokeAsync operation on AmazonLambdaClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        [Obsolete("This API is deprecated. We recommend that you use Invoke API instead.")]
        public void InvokeAsyncAsync(InvokeAsyncRequest request, AmazonServiceCallback<InvokeAsyncRequest, InvokeAsyncResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new InvokeAsyncRequestMarshaller();
            var unmarshaller = InvokeAsyncResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<InvokeAsyncRequest,InvokeAsyncResponse> responseObject 
                            = new AmazonServiceResult<InvokeAsyncRequest,InvokeAsyncResponse>((InvokeAsyncRequest)req, (InvokeAsyncResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<InvokeAsyncRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListEventSourceMappings

        /// <summary>
        /// Initiates the asynchronous execution of the ListEventSourceMappings operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListEventSourceMappings operation on AmazonLambdaClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListEventSourceMappingsAsync(ListEventSourceMappingsRequest request, AmazonServiceCallback<ListEventSourceMappingsRequest, ListEventSourceMappingsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListEventSourceMappingsRequestMarshaller();
            var unmarshaller = ListEventSourceMappingsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListEventSourceMappingsRequest,ListEventSourceMappingsResponse> responseObject 
                            = new AmazonServiceResult<ListEventSourceMappingsRequest,ListEventSourceMappingsResponse>((ListEventSourceMappingsRequest)req, (ListEventSourceMappingsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListEventSourceMappingsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
        #region  ListFunctions

        /// <summary>
        /// Returns a list of your Lambda functions. For each function, the response includes
        /// the function configuration information. You must use <a>GetFunction</a> to retrieve
        /// the code for your function.
        /// 
        ///  
        /// <para>
        /// This operation requires permission for the <code>lambda:ListFunctions</code> action.
        /// </para>
        /// </summary>
        /// <param name="options">
         ///     A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
         ///     procedure using the AsyncState property.
         /// </param>
        /// 
        /// <returns>The response from the ListFunctions service method, as returned by Lambda.</returns>
        /// <exception cref="Amazon.Lambda.Model.ServiceException">
        /// The AWS Lambda service encountered an internal error.
        /// </exception>
        /// <exception cref="Amazon.Lambda.Model.TooManyRequestsException">
        /// 
        /// </exception>
        public void ListFunctionsAsync( AmazonServiceCallback<ListFunctionsRequest, ListFunctionsResponse> callback, AsyncOptions options = null)
        {
            var request = new ListFunctionsRequest();
            ListFunctionsAsync(request, callback, options);
        }


        /// <summary>
        /// Initiates the asynchronous execution of the ListFunctions operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ListFunctions operation on AmazonLambdaClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void ListFunctionsAsync(ListFunctionsRequest request, AmazonServiceCallback<ListFunctionsRequest, ListFunctionsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new ListFunctionsRequestMarshaller();
            var unmarshaller = ListFunctionsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<ListFunctionsRequest,ListFunctionsResponse> responseObject 
                            = new AmazonServiceResult<ListFunctionsRequest,ListFunctionsResponse>((ListFunctionsRequest)req, (ListFunctionsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<ListFunctionsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
    }
}