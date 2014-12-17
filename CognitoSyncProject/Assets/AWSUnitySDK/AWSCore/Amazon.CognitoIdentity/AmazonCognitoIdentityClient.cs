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
using System.Threading;

using Amazon.CognitoIdentity.Model;
using Amazon.CognitoIdentity.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;
using Amazon.Unity;

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
    /// Next, make an unsigned call to <a>GetOpenIdToken</a>, which returns the OpenID token
    /// necessary to call STS and retrieve AWS credentials. This call expects the same <code>Logins</code>
    /// map as the <code>GetId</code> call, as well as the <code>IdentityID</code> originally
    /// returned by <code>GetId</code>. The token returned by <code>GetOpenIdToken</code>
    /// can be passed to the STS operation <a href="http://docs.aws.amazon.com/STS/latest/APIReference/API_AssumeRoleWithWebIdentity.html">AssumeRoleWithWebIdentity</a>
    /// to retrieve AWS credentials.
    /// </para>
    /// </summary>
    public partial class AmazonCognitoIdentityClient : AmazonWebServiceClient, IAmazonCognitoIdentity
    {
        AWS4Signer signer = new AWS4Signer();

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with the credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// 
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfileName" value="AWS Default"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        ///
        /// </summary>
        public AmazonCognitoIdentityClient()
            : base(FallbackCredentialsFactory.GetCredentials(), new AmazonCognitoIdentityConfig(), AuthenticationTypes.User | AuthenticationTypes.Session)
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with the credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// 
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfileName" value="AWS Default"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        ///
        /// </summary>
        /// <param name="region">The region to connect.</param>
        public AmazonCognitoIdentityClient(RegionEndpoint region)
            : base(FallbackCredentialsFactory.GetCredentials(), new AmazonCognitoIdentityConfig { RegionEndpoint = region }, AuthenticationTypes.User | AuthenticationTypes.Session)
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with the credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// 
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfileName" value="AWS Default"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        ///
        /// </summary>
        /// <param name="config">The AmazonCognitoIdentityClient Configuration Object</param>
        public AmazonCognitoIdentityClient(AmazonCognitoIdentityConfig config)
            : base(FallbackCredentialsFactory.GetCredentials(), config, AuthenticationTypes.User | AuthenticationTypes.Session)
        {
        }

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
            : this(credentials, new AmazonCognitoIdentityConfig { RegionEndpoint = region })
        {
        }

        /// <summary>
        /// Constructs AmazonCognitoIdentityClient with AWS Credentials and an
        /// AmazonCognitoIdentityClient Configuration object.
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="clientConfig">The AmazonCognitoIdentityClient Configuration Object</param>
        public AmazonCognitoIdentityClient(AWSCredentials credentials, AmazonCognitoIdentityConfig clientConfig)
            : base(credentials, clientConfig, AuthenticationTypes.User | AuthenticationTypes.Session)
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
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonCognitoIdentityConfig() { RegionEndpoint = region })
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
            : base(awsAccessKeyId, awsSecretAccessKey, clientConfig, AuthenticationTypes.User | AuthenticationTypes.Session)
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
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonCognitoIdentityConfig { RegionEndpoint = region })
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
            : base(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, clientConfig, AuthenticationTypes.User | AuthenticationTypes.Session)
        {
        }

        #endregion

#if CONTROLPANEL_API_SUPPORT
        #region  CreateIdentityPool
        
        /// NOT SUPPORTED - Since it is Control Panel API
        /// <summary>
        /// Initiates the asynchronous execution of the CreateIdentityPool operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the CreateIdentityPool operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void CreateIdentityPoolAsync(CreateIdentityPoolRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                                                          {
                var marshaller = new CreateIdentityPoolRequestMarshaller();
                var unmarshaller = CreateIdentityPoolResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }
        
        
        #endregion
        
        #region  DeleteIdentityPool
        
        /// NOT SUPPORTED - Since it is Control Panel API
        /// <summary>
        /// Initiates the asynchronous execution of the DeleteIdentityPool operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DeleteIdentityPool operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void DeleteIdentityPoolAsync(DeleteIdentityPoolRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                                                          {
                var marshaller = new DeleteIdentityPoolRequestMarshaller();
                var unmarshaller = DeleteIdentityPoolResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }
        
        
        #endregion
        
        #region  DescribeIdentityPool
        
        /// NOT SUPPORTED - Since it is Control Panel API
        /// <summary>
        /// Initiates the asynchronous execution of the DescribeIdentityPool operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the DescribeIdentityPool operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void DescribeIdentityPoolAsync(DescribeIdentityPoolRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                                                          {
                var marshaller = new DescribeIdentityPoolRequestMarshaller();
                var unmarshaller = DescribeIdentityPoolResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }
        
        
        #endregion
#endif

        #region  GetId


        /// <summary>
        /// Initiates the asynchronous execution of the GetId operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetId operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void GetIdAsync(GetIdRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                var marshaller = new GetIdRequestMarshaller();
                var unmarshaller = GetIdResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }


        #endregion

        #region  GetOpenIdToken


        /// <summary>
        /// Initiates the asynchronous execution of the GetOpenIdToken operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetOpenIdToken operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void GetOpenIdTokenAsync(GetOpenIdTokenRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                var marshaller = new GetOpenIdTokenRequestMarshaller();
                var unmarshaller = GetOpenIdTokenResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }


        #endregion

#if CONTROLPANEL_API_SUPPORT
        #region  GetOpenIdTokenForDeveloperIdentity
        
        
        /// <summary>
        /// Initiates the asynchronous execution of the GetOpenIdTokenForDeveloperIdentity operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetOpenIdTokenForDeveloperIdentity operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void GetOpenIdTokenForDeveloperIdentityAsync (GetOpenIdTokenForDeveloperIdentityRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception ("AWSPrefab is not added to the scene");
            
            ThreadPool.QueueUserWorkItem (new WaitCallback (delegate
            {
                var marshaller = new GetOpenIdTokenForDeveloperIdentityRequestMarshaller ();
                var unmarshaller = GetOpenIdTokenForDeveloperIdentityResponseUnmarshaller.Instance;
                Invoke (request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }
        
        
        #endregion

        #region  ListIdentities
        
        /// NOT SUPPORTED - Since it is Control Panel API
        /// <summary>
        /// Initiates the asynchronous execution of the ListIdentities operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListIdentities operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void ListIdentitiesAsync(ListIdentitiesRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                                                          {
                var marshaller = new ListIdentitiesRequestMarshaller();
                var unmarshaller = ListIdentitiesResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }
        
        
        #endregion
        
        #region  ListIdentityPools
        
        /// NOT SUPPORTED - Since it is Control Panel API
        /// <summary>
        /// Initiates the asynchronous execution of the ListIdentityPools operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListIdentityPools operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void ListIdentityPoolsAsync(ListIdentityPoolsRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                                                          {
                var marshaller = new ListIdentityPoolsRequestMarshaller();
                var unmarshaller = ListIdentityPoolsResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }
        
        
        #endregion
        
        #region  LookupDeveloperIdentity
        
        /// NOT SUPPORTED - Since it is Control Panel API
        /// <summary>
        /// Initiates the asynchronous execution of the LookupDeveloperIdentity operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the LookupDeveloperIdentity operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void LookupDeveloperIdentityAsync(LookupDeveloperIdentityRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                                                          {
                var marshaller = new LookupDeveloperIdentityRequestMarshaller();
                var unmarshaller = LookupDeveloperIdentityResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }
        
        
        #endregion
        
        #region  MergeDeveloperIdentities
        
        /// NOT SUPPORTED - Since it is Control Panel API
        /// <summary>
        /// Initiates the asynchronous execution of the MergeDeveloperIdentities operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the MergeDeveloperIdentities operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void MergeDeveloperIdentitiesAsync(MergeDeveloperIdentitiesRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                                                          {
                var marshaller = new MergeDeveloperIdentitiesRequestMarshaller();
                var unmarshaller = MergeDeveloperIdentitiesResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }
        
        
        #endregion

        #region  UnlinkDeveloperIdentity

        /// <summary>
        /// Initiates the asynchronous execution of the UnlinkDeveloperIdentity operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UnlinkDeveloperIdentity operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void UnlinkDeveloperIdentityAsync (UnlinkDeveloperIdentityRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception ("AWSPrefab is not added to the scene");
            
            ThreadPool.QueueUserWorkItem (new WaitCallback (delegate
            {
                var marshaller = new UnlinkDeveloperIdentityRequestMarshaller ();
                var unmarshaller = UnlinkDeveloperIdentityResponseUnmarshaller.Instance;
                Invoke (request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }
        
        
        #endregion
#endif
        #region  UnlinkIdentity


        /// <summary>
        /// Initiates the asynchronous execution of the UnlinkIdentity operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UnlinkIdentity operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void UnlinkIdentityAsync(UnlinkIdentityRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                var marshaller = new UnlinkIdentityRequestMarshaller();
                var unmarshaller = UnlinkIdentityResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }


        #endregion

#if CONTROLPANEL_API_SUPPORT
        #region  UpdateIdentityPool
        
        /// NOT SUPPORTED - Since it is Control Panel API
        /// <summary>
        /// Initiates the asynchronous execution of the UpdateIdentityPool operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UpdateIdentityPool operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <param name="state">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback
        ///          procedure using the AsyncState property.</param>
        /// <returns>void</returns>
        public void UpdateIdentityPoolAsync(UpdateIdentityPoolRequest request, AmazonServiceCallback callback, object state)
        {
            if (!AmazonInitializer.IsInitialized)
                throw new Exception("AWSPrefab is not added to the scene");
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                                                          {
                var marshaller = new UpdateIdentityPoolRequestMarshaller();
                var unmarshaller = UpdateIdentityPoolResponseUnmarshaller.Instance;
                Invoke(request, callback, state, marshaller, unmarshaller, signer);
            }));
            return;
        }
        
        
        #endregion
#endif
    }
}