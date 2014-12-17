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
using Amazon.CognitoIdentity.Model;

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
    public partial interface IAmazonCognitoIdentity : IDisposable
    {
#if CONTROLPANEL_API_SUPPORT
		
        #region  CreateIdentityPool
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the CreateIdentityPool operation.
		/// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the CreateIdentityPool operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void CreateIdentityPoolAsync(CreateIdentityPoolRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
		
        #region  DeleteIdentityPool
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the DeleteIdentityPool operation.
		/// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the DeleteIdentityPool operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void DeleteIdentityPoolAsync(DeleteIdentityPoolRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
		
        #region  DescribeIdentityPool
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the DescribeIdentityPool operation.
		/// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the DescribeIdentityPool operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void DescribeIdentityPoolAsync(DescribeIdentityPoolRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
#endif
        #region  GetId


        /// <summary>
        /// Initiates the asynchronous execution of the GetId operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetId operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <returns>void</returns>
        void GetIdAsync(GetIdRequest request, AmazonServiceCallback callback, object state);

        #endregion

        #region  GetOpenIdToken


        /// <summary>
        /// Initiates the asynchronous execution of the GetOpenIdToken operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the GetOpenIdToken operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <returns>void</returns>
        void GetOpenIdTokenAsync(GetOpenIdTokenRequest request, AmazonServiceCallback callback, object state);

        #endregion
#if CONTROLPANEL_API_SUPPORT
        #region  GetOpenIdTokenForDeveloperIdentity
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the GetOpenIdTokenForDeveloperIdentity operation.
		/// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the GetOpenIdTokenForDeveloperIdentity operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void GetOpenIdTokenForDeveloperIdentityAsync(GetOpenIdTokenForDeveloperIdentityRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
		
        #region  ListIdentities
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the ListIdentities operation.
		/// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the ListIdentities operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void ListIdentitiesAsync(ListIdentitiesRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
		
        #region  ListIdentityPools
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the ListIdentityPools operation.
		/// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the ListIdentityPools operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void ListIdentityPoolsAsync(ListIdentityPoolsRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
		
        #region  LookupDeveloperIdentity
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the LookupDeveloperIdentity operation.
		/// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the LookupDeveloperIdentity operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void LookupDeveloperIdentityAsync(LookupDeveloperIdentityRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
		
        #region  MergeDeveloperIdentities
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the MergeDeveloperIdentities operation.
		/// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the MergeDeveloperIdentities operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void MergeDeveloperIdentitiesAsync(MergeDeveloperIdentitiesRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
		
        #region  UnlinkDeveloperIdentity
		
		/// NOT SUPPORTED - Since it is Control Panel API
		/// <summary>
		/// Initiates the asynchronous execution of the UnlinkDeveloperIdentity operation.
		/// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
		/// </summary>
		/// <param name="request">Container for the necessary parameters to execute the UnlinkDeveloperIdentity operation.</param>
		/// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
		/// <returns>void</returns>
		void UnlinkDeveloperIdentityAsync(UnlinkDeveloperIdentityRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
#endif
        #region  UnlinkIdentity


        /// <summary>
        /// Initiates the asynchronous execution of the UnlinkIdentity operation.
        /// <seealso cref="Amazon.CognitoIdentity.IAmazonCognitoIdentity"/>
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the UnlinkIdentity operation.</param>
        /// <param name="callback">An AsyncCallback delegate that is invoked when the operation completes</param>
        /// <returns>void</returns>
        void UnlinkIdentityAsync(UnlinkIdentityRequest request, AmazonServiceCallback callback, object state);

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
		/// <returns>void</returns>
		void UpdateIdentityPoolAsync(UpdateIdentityPoolRequest request, AmazonServiceCallback callback, object state);
		
        #endregion
#endif
    }
}