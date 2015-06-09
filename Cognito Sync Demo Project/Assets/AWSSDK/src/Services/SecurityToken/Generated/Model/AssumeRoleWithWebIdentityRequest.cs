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
 * Do not modify this file. This file is generated from the sts-2011-06-15.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.SecurityToken.Model
{
    /// <summary>
    /// Container for the parameters to the AssumeRoleWithWebIdentity operation.
    /// Returns a set of temporary security credentials for users who have been authenticated
    /// in a mobile or web application with a web identity provider, such as Login with Amazon,
    /// Amazon Cognito, Facebook, or Google. 
    /// 
    ///  
    /// <para>
    /// Calling <code>AssumeRoleWithWebIdentity</code> does not require the use of AWS security
    /// credentials. Therefore, you can distribute an application (for example, on mobile
    /// devices) that requests temporary security credentials without including long-term
    /// AWS credentials in the application, and without deploying server-based proxy services
    /// that use long-term AWS credentials. Instead, the identity of the caller is validated
    /// by using a token from the web identity provider. 
    /// </para>
    ///  
    /// <para>
    /// The temporary security credentials returned by this API consist of an access key ID,
    /// a secret access key, and a security token. Applications can use these temporary security
    /// credentials to sign calls to AWS service APIs. The credentials are valid for the duration
    /// that you specified when calling <code>AssumeRoleWithWebIdentity</code>, which can
    /// be from 900 seconds (15 minutes) to 3600 seconds (1 hour). By default, the temporary
    /// security credentials are valid for 1 hour. 
    /// </para>
    ///  
    /// <para>
    /// Optionally, you can pass an IAM access policy to this operation. If you choose not
    /// to pass a policy, the temporary security credentials that are returned by the operation
    /// have the permissions that are defined in the access policy of the role that is being
    /// assumed. If you pass a policy to this operation, the temporary security credentials
    /// that are returned by the operation have the permissions that are allowed by both the
    /// access policy of the role that is being assumed, <i><b>and</b></i> the policy that
    /// you pass. This gives you a way to further restrict the permissions for the resulting
    /// temporary security credentials. You cannot use the passed policy to grant permissions
    /// that are in excess of those allowed by the access policy of the role that is being
    /// assumed. For more information, see <a href="http://docs.aws.amazon.com/STS/latest/UsingSTS/permissions-assume-role.html">Permissions
    /// for AssumeRoleWithWebIdentity</a> in <i>Using Temporary Security Credentials</i>.
    /// </para>
    ///  
    /// <para>
    /// Before your application can call <code>AssumeRoleWithWebIdentity</code>, you must
    /// have an identity token from a supported identity provider and create a role that the
    /// application can assume. The role that your application assumes must trust the identity
    /// provider that is associated with the identity token. In other words, the identity
    /// provider must be specified in the role's trust policy. 
    /// </para>
    ///  
    /// <para>
    /// For more information about how to use web identity federation and the <code>AssumeRoleWithWebIdentity</code>,
    /// see the following resources: 
    /// </para>
    ///  <ul> <li> <a href="http://docs.aws.amazon.com/STS/latest/UsingSTS/STSUseCases.html#MobileApplication-KnownProvider"
    /// target="_blank"> Creating a Mobile Application with Third-Party Sign-In</a> and <a
    /// href="http://docs.aws.amazon.com/STS/latest/UsingSTS/CreatingWIF.html" target="_blank">
    /// Creating Temporary Security Credentials for Mobile Apps Using Third-Party Identity
    /// Providers</a> in <i>Using Temporary Security Credentials</i>. </li> <li> <a href="https://web-identity-federation-playground.s3.amazonaws.com/index.html"
    /// target="_blank"> Web Identity Federation Playground</a>. This interactive website
    /// lets you walk through the process of authenticating via Login with Amazon, Facebook,
    /// or Google, getting temporary security credentials, and then using those credentials
    /// to make a request to AWS. </li> <li> <a href="http://aws.amazon.com/sdkforios/">AWS
    /// SDK for iOS</a> and <a href="http://aws.amazon.com/sdkforandroid/" target="_blank">AWS
    /// SDK for Android</a>. These toolkits contain sample apps that show how to invoke the
    /// identity providers, and then how to use the information from these providers to get
    /// and use temporary security credentials. </li> <li> <a href="http://aws.amazon.com/articles/4617974389850313"
    /// target="_blank">Web Identity Federation with Mobile Applications</a>. This article
    /// discusses web identity federation and shows an example of how to use web identity
    /// federation to get access to content in Amazon S3. </li> </ul> <member name="RoleArn"
    /// target="arnType"></member> <member name="RoleSessionName" target="userNameType"></member>
    /// <member name="WebIdentityToken" target="clientTokenType"></member> <member name="ProviderId"
    /// target="urlType"></member> <member name="Policy" target="sessionPolicyDocumentType"></member>
    /// <member name="DurationSeconds" target="roleDurationSecondsType"></member>
    /// </summary>
    public partial class AssumeRoleWithWebIdentityRequest : AmazonSecurityTokenServiceRequest
    {
        private int? _durationSeconds;
        private string _policy;
        private string _providerId;
        private string _roleArn;
        private string _roleSessionName;
        private string _webIdentityToken;

        /// <summary>
        /// Gets and sets the property DurationSeconds. 
        /// <para>
        /// The duration, in seconds, of the role session. The value can range from 900 seconds
        /// (15 minutes) to 3600 seconds (1 hour). By default, the value is set to 3600 seconds.
        /// 
        /// </para>
        /// </summary>
        public int DurationSeconds
        {
            get { return this._durationSeconds.GetValueOrDefault(); }
            set { this._durationSeconds = value; }
        }

        // Check to see if DurationSeconds property is set
        internal bool IsSetDurationSeconds()
        {
            return this._durationSeconds.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property Policy. 
        /// <para>
        /// An IAM policy in JSON format.
        /// </para>
        ///  
        /// <para>
        /// The policy parameter is optional. If you pass a policy, the temporary security credentials
        /// that are returned by the operation have the permissions that are allowed by both the
        /// access policy of the role that is being assumed, <i><b>and</b></i> the policy that
        /// you pass. This gives you a way to further restrict the permissions for the resulting
        /// temporary security credentials. You cannot use the passed policy to grant permissions
        /// that are in excess of those allowed by the access policy of the role that is being
        /// assumed. For more information, see <a href="http://docs.aws.amazon.com/STS/latest/UsingSTS/permissions-assume-role.html">Permissions
        /// for AssumeRoleWithWebIdentity</a> in <i>Using Temporary Security Credentials</i>.
        /// 
        /// </para>
        /// </summary>
        public string Policy
        {
            get { return this._policy; }
            set { this._policy = value; }
        }

        // Check to see if Policy property is set
        internal bool IsSetPolicy()
        {
            return this._policy != null;
        }

        /// <summary>
        /// Gets and sets the property ProviderId. 
        /// <para>
        /// The fully-qualified host component of the domain name of the identity provider. Specify
        /// this value only for OAuth access tokens. Do not specify this value for OpenID Connect
        /// ID tokens, such as <code>accounts.google.com</code>. Do not include URL schemes and
        /// port numbers. Currently, <code>www.amazon.com</code> and <code>graph.facebook.com</code>
        /// are supported. 
        /// </para>
        /// </summary>
        public string ProviderId
        {
            get { return this._providerId; }
            set { this._providerId = value; }
        }

        // Check to see if ProviderId property is set
        internal bool IsSetProviderId()
        {
            return this._providerId != null;
        }

        /// <summary>
        /// Gets and sets the property RoleArn. 
        /// <para>
        /// The Amazon Resource Name (ARN) of the role that the caller is assuming.
        /// </para>
        /// </summary>
        public string RoleArn
        {
            get { return this._roleArn; }
            set { this._roleArn = value; }
        }

        // Check to see if RoleArn property is set
        internal bool IsSetRoleArn()
        {
            return this._roleArn != null;
        }

        /// <summary>
        /// Gets and sets the property RoleSessionName. 
        /// <para>
        /// An identifier for the assumed role session. Typically, you pass the name or identifier
        /// that is associated with the user who is using your application. That way, the temporary
        /// security credentials that your application will use are associated with that user.
        /// This session name is included as part of the ARN and assumed role ID in the <code>AssumedRoleUser</code>
        /// response element. 
        /// </para>
        /// </summary>
        public string RoleSessionName
        {
            get { return this._roleSessionName; }
            set { this._roleSessionName = value; }
        }

        // Check to see if RoleSessionName property is set
        internal bool IsSetRoleSessionName()
        {
            return this._roleSessionName != null;
        }

        /// <summary>
        /// Gets and sets the property WebIdentityToken. 
        /// <para>
        /// The OAuth 2.0 access token or OpenID Connect ID token that is provided by the identity
        /// provider. Your application must get this token by authenticating the user who is using
        /// your application with a web identity provider before the application makes an <code>AssumeRoleWithWebIdentity</code>
        /// call. 
        /// </para>
        /// </summary>
        public string WebIdentityToken
        {
            get { return this._webIdentityToken; }
            set { this._webIdentityToken = value; }
        }

        // Check to see if WebIdentityToken property is set
        internal bool IsSetWebIdentityToken()
        {
            return this._webIdentityToken != null;
        }

    }
}
