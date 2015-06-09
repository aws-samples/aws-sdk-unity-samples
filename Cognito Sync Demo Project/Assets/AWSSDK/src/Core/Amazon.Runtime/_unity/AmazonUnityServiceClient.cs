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
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using Amazon.Util;
using System.Collections.Generic;
using System.Threading;

namespace Amazon.Runtime
{
    public abstract class AmazonUnityServiceClient : AmazonServiceClient
    {
        #region Constructors
        protected AmazonUnityServiceClient(AWSCredentials credentials, ClientConfig config) :
            base(credentials, config)
        {
        }

        protected AmazonUnityServiceClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, ClientConfig config)
            : base(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, config)
        {
        }

        protected AmazonUnityServiceClient(string awsAccessKeyId, string awsSecretAccessKey, ClientConfig config)
            : base(awsAccessKeyId, awsSecretAccessKey, config)
        {
        }

        protected override void CustomizeRuntimePipeline(RuntimePipeline pipeline)
        {
            base.CustomizeRuntimePipeline(pipeline);
        }
        #endregion

        #region Invoke methods

        protected IAsyncResult BeginInvoke<TRequest>(TRequest request,
           IMarshaller<IRequest, AmazonWebServiceRequest> marshaller, ResponseUnmarshaller unmarshaller, AsyncOptions asyncOptions,
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper)
           where TRequest : AmazonWebServiceRequest
        {
            ThrowIfDisposed();

            asyncOptions = asyncOptions ?? new AsyncOptions();
            var executionContext = new AsyncExecutionContext(
                new AsyncRequestContext(this.Config.LogMetrics)
                {
                    ClientConfig = this.Config,
                    Marshaller = marshaller,
                    OriginalRequest = request,
                    Signer = Signer,
                    Unmarshaller = unmarshaller,
                    Action = callbackHelper,
                    AsyncOptions = asyncOptions,
                    IsAsync = true
                },
                new AsyncResponseContext()
            );

            return this.RuntimePipeline.InvokeAsync(executionContext);
        }

        #endregion

        #region Overrides

        protected override void BuildRuntimePipeline()
        {
            var httpRequestFactory = new UnityWebRequestFactory();
            var httpHandler = new HttpHandler<string>(httpRequestFactory, this);

            var preMarshallHandler = new CallbackHandler();
            preMarshallHandler.OnPreInvoke = this.ProcessPreRequestHandlers;

            var postMarshallHandler = new CallbackHandler();
            postMarshallHandler.OnPreInvoke = this.ProcessRequestHandlers;

            var postUnmarshallHandler = new CallbackHandler();
            postUnmarshallHandler.OnPostInvoke = this.ProcessResponseHandlers;

            var errorCallbackHandler = new ErrorCallbackHandler();
            errorCallbackHandler.OnError = this.ProcessExceptionHandlers;

            // Build default runtime pipeline.
            this.RuntimePipeline = new RuntimePipeline(new List<IPipelineHandler>
                {
                    httpHandler,                    
                    new Unmarshaller(this.SupportResponseLogging),
                    new ErrorHandler(this.Logger),
                    postUnmarshallHandler,
                    new Signer(),
                    new CredentialsRetriever(this.Credentials),
                    postMarshallHandler,
                    new EndpointResolver(),                    
                    new Marshaller(),
                    preMarshallHandler,
                    errorCallbackHandler,
                    new MetricsHandler(),
                    new ThreadPoolExecutionHandler(10)//remove the hardcoded to unity config
                },
                this.Logger
            );
            CustomizeRuntimePipeline(this.RuntimePipeline);
        }

        #endregion

    }
}
