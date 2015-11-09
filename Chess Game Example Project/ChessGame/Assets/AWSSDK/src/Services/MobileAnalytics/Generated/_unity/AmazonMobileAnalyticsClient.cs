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
 * Do not modify this file. This file is generated from the mobileanalytics-2014-06-05.normal.json service model.
 */


using System;
using System.Collections.Generic;

using Amazon.MobileAnalytics.Model;
using Amazon.MobileAnalytics.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.MobileAnalytics
{
    /// <summary>
    /// Implementation for accessing MobileAnalytics
    ///
    /// Amazon Mobile Analytics is a service for collecting, visualizing, and understanding
    /// app usage data at scale.
    /// </summary>
    public partial class AmazonMobileAnalyticsClient : AmazonUnityServiceClient, IAmazonMobileAnalytics
    {
        #region Constructors

        /// <summary>
        /// Constructs AmazonMobileAnalyticsClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        public AmazonMobileAnalyticsClient(AWSCredentials credentials)
            : this(credentials, new AmazonMobileAnalyticsConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonMobileAnalyticsClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="region">The region to connect.</param>
        public AmazonMobileAnalyticsClient(AWSCredentials credentials, RegionEndpoint region)
            : this(credentials, new AmazonMobileAnalyticsConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonMobileAnalyticsClient with AWS Credentials and an
        /// AmazonMobileAnalyticsClient Configuration object.
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="clientConfig">The AmazonMobileAnalyticsClient Configuration Object</param>
        public AmazonMobileAnalyticsClient(AWSCredentials credentials, AmazonMobileAnalyticsConfig clientConfig)
            : base(credentials, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonMobileAnalyticsClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        public AmazonMobileAnalyticsClient(string awsAccessKeyId, string awsSecretAccessKey)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonMobileAnalyticsConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonMobileAnalyticsClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="region">The region to connect.</param>
        public AmazonMobileAnalyticsClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonMobileAnalyticsConfig() {RegionEndpoint=region})
        {
        }

        /// <summary>
        /// Constructs AmazonMobileAnalyticsClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonMobileAnalyticsClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="clientConfig">The AmazonMobileAnalyticsClient Configuration Object</param>
        public AmazonMobileAnalyticsClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonMobileAnalyticsConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonMobileAnalyticsClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        public AmazonMobileAnalyticsClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonMobileAnalyticsConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonMobileAnalyticsClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="region">The region to connect.</param>
        public AmazonMobileAnalyticsClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonMobileAnalyticsConfig{RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonMobileAnalyticsClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonMobileAnalyticsClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="clientConfig">The AmazonMobileAnalyticsClient Configuration Object</param>
        public AmazonMobileAnalyticsClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, AmazonMobileAnalyticsConfig clientConfig)
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
            pipeline.ReplaceHandler<Amazon.Runtime.Internal.ErrorHandler>(new Amazon.Runtime.Internal.ErrorHandler<AmazonMobileAnalyticsException>(this.Logger));
        }    

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

        
        #region  PutEvents

        /// <summary>
        /// Initiates the asynchronous execution of the PutEvents operation.
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the PutEvents operation on AmazonMobileAnalyticsClient.</param>
        /// <param name="callback">An Action delegate that is invoked when the operation completes.</param>
        /// <param name="options">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        public void PutEventsAsync(PutEventsRequest request, AmazonServiceCallback<PutEventsRequest, PutEventsResponse> callback, AsyncOptions options = null)
        {
            options = options == null?new AsyncOptions():options;
            var marshaller = new PutEventsRequestMarshaller();
            var unmarshaller = PutEventsResponseUnmarshaller.Instance;
            Action<AmazonWebServiceRequest, AmazonWebServiceResponse, Exception, AsyncOptions> callbackHelper = null;
            if(callback !=null )
                callbackHelper = (AmazonWebServiceRequest req, AmazonWebServiceResponse res, Exception ex, AsyncOptions ao) => { 
                    AmazonServiceResult<PutEventsRequest,PutEventsResponse> responseObject 
                            = new AmazonServiceResult<PutEventsRequest,PutEventsResponse>((PutEventsRequest)req, (PutEventsResponse)res, ex , ao.State);    
                        callback(responseObject); 
                };
            BeginInvoke<PutEventsRequest>(request, marshaller, unmarshaller, options, callbackHelper);
        }

        #endregion
        
    }
}