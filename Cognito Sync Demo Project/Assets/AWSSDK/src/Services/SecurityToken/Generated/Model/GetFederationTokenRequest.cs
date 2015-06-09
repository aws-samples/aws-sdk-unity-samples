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
    /// Container for the parameters to the GetFederationToken operation.
    /// Returns a set of temporary security credentials (consisting of an access key ID, a
    /// secret access key, and a security token) for a federated user. A typical use is in
    /// a proxy application that gets temporary security credentials on behalf of distributed
    /// applications inside a corporate network. Because you must call the <code>GetFederationToken</code>
    /// action using the long-term security credentials of an IAM user, this call is appropriate
    /// in contexts where those credentials can be safely stored, usually in a server-based
    /// application.
    /// 
    ///  
    /// <para>
    ///  <b>Note:</b> Do not use this call in mobile applications or client-based web applications
    /// that directly get temporary security credentials. For those types of applications,
    /// use <code>AssumeRoleWithWebIdentity</code>.
    /// </para>
    ///  
    /// <para>
    /// The <code>GetFederationToken</code> action must be called by using the long-term AWS
    /// security credentials of an IAM user. You can also call <code>GetFederationToken</code>
    /// using the security credentials of an AWS account (root), but this is not recommended.
    /// Instead, we recommend that you create an IAM user for the purpose of the proxy application
    /// and then attach a policy to the IAM user that limits federated users to only the actions
    /// and resources they need access to. For more information, see <a href="http://docs.aws.amazon.com/IAM/latest/UserGuide/IAMBestPractices.html">IAM
    /// Best Practices</a> in <i>Using IAM</i>. 
    /// </para>
    ///  
    /// <para>
    /// The temporary security credentials that are obtained by using the long-term credentials
    /// of an IAM user are valid for the specified duration, between 900 seconds (15 minutes)
    /// and 129600 seconds (36 hours). Temporary credentials that are obtained by using AWS
    /// account (root) credentials have a maximum duration of 3600 seconds (1 hour)
    /// </para>
    ///  
    /// <para>
    ///  <b>Permissions</b> 
    /// </para>
    ///  
    /// <para>
    /// The permissions for the temporary security credentials returned by <code>GetFederationToken</code>
    /// are determined by a combination of the following: 
    /// </para>
    ///  <ul> <li>The policy or policies that are attached to the IAM user whose credentials
    /// are used to call <code>GetFederationToken</code>.</li> <li>The policy that is passed
    /// as a parameter in the call.</li> </ul> 
    /// <para>
    /// The passed policy is attached to the temporary security credentials that result from
    /// the <code>GetFederationToken</code> API call--that is, to the <i>federated user</i>.
    /// When the federated user makes an AWS request, AWS evaluates the policy attached to
    /// the federated user in combination with the policy or policies attached to the IAM
    /// user whose credentials were used to call <code>GetFederationToken</code>. AWS allows
    /// the federated user's request only when both the federated user <i><b>and</b></i> the
    /// IAM user are explicitly allowed to perform the requested action. The passed policy
    /// cannot grant more permissions than those that are defined in the IAM user policy.
    /// </para>
    ///  
    /// <para>
    /// A typical use case is that the permissions of the IAM user whose credentials are used
    /// to call <code>GetFederationToken</code> are designed to allow access to all the actions
    /// and resources that any federated user will need. Then, for individual users, you pass
    /// a policy to the operation that scopes down the permissions to a level that's appropriate
    /// to that individual user, using a policy that allows only a subset of permissions that
    /// are granted to the IAM user. 
    /// </para>
    ///  
    /// <para>
    /// If you do not pass a policy, the resulting temporary security credentials have no
    /// effective permissions. The only exception is when the temporary security credentials
    /// are used to access a resource that has a resource-based policy that specifically allows
    /// the federated user to access the resource. 
    /// </para>
    ///  
    /// <para>
    /// For more information about how permissions work, see <a href="http://docs.aws.amazon.com/STS/latest/UsingSTS/permissions-get-federation-token.html">Permissions
    /// for GetFederationToken</a> in <i>Using Temporary Security Credentials</i>. For information
    /// about using <code>GetFederationToken</code> to create temporary security credentials,
    /// see <a href="http://docs.aws.amazon.com/STS/latest/UsingSTS/CreatingFedTokens.html">Creating
    /// Temporary Credentials to Enable Access for Federated Users</a> in <i>Using Temporary
    /// Security Credentials</i>. 
    /// </para>
    /// </summary>
    public partial class GetFederationTokenRequest : AmazonSecurityTokenServiceRequest
    {
        private int? _durationSeconds;
        private string _name;
        private string _policy;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public GetFederationTokenRequest() { }

        /// <summary>
        /// Instantiates GetFederationTokenRequest with the parameterized properties
        /// </summary>
        /// <param name="name">The name of the federated user. The name is used as an identifier for the temporary security credentials (such as <code>Bob</code>). For example, you can reference the federated user name in a resource-based policy, such as in an Amazon S3 bucket policy. </param>
        public GetFederationTokenRequest(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Gets and sets the property DurationSeconds. 
        /// <para>
        /// The duration, in seconds, that the session should last. Acceptable durations for federation
        /// sessions range from 900 seconds (15 minutes) to 129600 seconds (36 hours), with 43200
        /// seconds (12 hours) as the default. Sessions obtained using AWS account (root) credentials
        /// are restricted to a maximum of 3600 seconds (one hour). If the specified duration
        /// is longer than one hour, the session obtained by using AWS account (root) credentials
        /// defaults to one hour. 
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
        /// Gets and sets the property Name. 
        /// <para>
        /// The name of the federated user. The name is used as an identifier for the temporary
        /// security credentials (such as <code>Bob</code>). For example, you can reference the
        /// federated user name in a resource-based policy, such as in an Amazon S3 bucket policy.
        /// 
        /// </para>
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        // Check to see if Name property is set
        internal bool IsSetName()
        {
            return this._name != null;
        }

        /// <summary>
        /// Gets and sets the property Policy. 
        /// <para>
        /// An IAM policy in JSON format that is passed with the <code>GetFederationToken</code>
        /// call and evaluated along with the policy or policies that are attached to the IAM
        /// user whose credentials are used to call <code>GetFederationToken</code>. The passed
        /// policy is used to scope down the permissions that are available to the IAM user, by
        /// allowing only a subset of the permissions that are granted to the IAM user. The passed
        /// policy cannot grant more permissions than those granted to the IAM user. The final
        /// permissions for the federated user are the most restrictive set based on the intersection
        /// of the passed policy and the IAM user policy.
        /// </para>
        ///  
        /// <para>
        /// If you do not pass a policy, the resulting temporary security credentials have no
        /// effective permissions. The only exception is when the temporary security credentials
        /// are used to access a resource that has a resource-based policy that specifically allows
        /// the federated user to access the resource. 
        /// </para>
        ///  
        /// <para>
        /// For more information about how permissions work, see <a href="http://docs.aws.amazon.com/STS/latest/UsingSTS/permissions-get-federation-token.html">Permissions
        /// for GetFederationToken</a> in <i>Using Temporary Security Credentials</i>.
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

    }
}
