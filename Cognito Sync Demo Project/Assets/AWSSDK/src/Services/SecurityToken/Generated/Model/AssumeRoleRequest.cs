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
    /// Container for the parameters to the AssumeRole operation.
    /// Returns a set of temporary security credentials (consisting of an access key ID, a
    /// secret access key, and a security token) that you can use to access AWS resources
    /// that you might not normally have access to. Typically, you use <code>AssumeRole</code>
    /// for cross-account access or federation. 
    /// 
    ///  
    /// <para>
    /// <b>Important:</b> You cannot call <code>AssumeRole</code> by using AWS account credentials;
    /// access will be denied. You must use IAM user credentials or temporary security credentials
    /// to call <code>AssumeRole</code>. 
    /// </para>
    ///  
    /// <para>
    /// For cross-account access, imagine that you own multiple accounts and need to access
    /// resources in each account. You could create long-term credentials in each account
    /// to access those resources. However, managing all those credentials and remembering
    /// which one can access which account can be time consuming. Instead, you can create
    /// one set of long-term credentials in one account and then use temporary security credentials
    /// to access all the other accounts by assuming roles in those accounts. For more information
    /// about roles, see <a href="http://docs.aws.amazon.com/IAM/latest/UserGuide/WorkingWithRoles.html">Roles</a>
    /// in <i>Using IAM</i>. 
    /// </para>
    ///  
    /// <para>
    /// For federation, you can, for example, grant single sign-on access to the AWS Management
    /// Console. If you already have an identity and authentication system in your corporate
    /// network, you don't have to recreate user identities in AWS in order to grant those
    /// user identities access to AWS. Instead, after a user has been authenticated, you call
    /// <code>AssumeRole</code> (and specify the role with the appropriate permissions) to
    /// get temporary security credentials for that user. With those temporary security credentials,
    /// you construct a sign-in URL that users can use to access the console. For more information,
    /// see <a href="http://docs.aws.amazon.com/STS/latest/UsingSTS/STSUseCases.html">Scenarios
    /// for Granting Temporary Access</a> in <i>Using Temporary Security Credentials</i>.
    /// 
    /// </para>
    ///  
    /// <para>
    /// The temporary security credentials are valid for the duration that you specified when
    /// calling <code>AssumeRole</code>, which can be from 900 seconds (15 minutes) to 3600
    /// seconds (1 hour). The default is 1 hour. 
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
    /// for AssumeRole</a> in <i>Using Temporary Security Credentials</i>.
    /// </para>
    ///  
    /// <para>
    /// To assume a role, your AWS account must be trusted by the role. The trust relationship
    /// is defined in the role's trust policy when the role is created. You must also have
    /// a policy that allows you to call <code>sts:AssumeRole</code>. 
    /// </para>
    ///  
    /// <para>
    ///  <b>Using MFA with AssumeRole</b> 
    /// </para>
    ///  
    /// <para>
    /// You can optionally include multi-factor authentication (MFA) information when you
    /// call <code>AssumeRole</code>. This is useful for cross-account scenarios in which
    /// you want to make sure that the user who is assuming the role has been authenticated
    /// using an AWS MFA device. In that scenario, the trust policy of the role being assumed
    /// includes a condition that tests for MFA authentication; if the caller does not include
    /// valid MFA information, the request to assume the role is denied. The condition in
    /// a trust policy that tests for MFA authentication might look like the following example.
    /// </para>
    ///  
    /// <para>
    ///  <code>"Condition": {"Null": {"aws:MultiFactorAuthAge": false}}</code> 
    /// </para>
    ///  
    /// <para>
    /// For more information, see <a href="http://docs.aws.amazon.com/IAM/latest/UserGuide/MFAProtectedAPI.html">Configuring
    /// MFA-Protected API Access</a> in the <i>Using IAM</i> guide. 
    /// </para>
    ///  
    /// <para>
    /// To use MFA with <code>AssumeRole</code>, you pass values for the <code>SerialNumber</code>
    /// and <code>TokenCode</code> parameters. The <code>SerialNumber</code> value identifies
    /// the user's hardware or virtual MFA device. The <code>TokenCode</code> is the time-based
    /// one-time password (TOTP) that the MFA devices produces. 
    /// </para>
    ///  <member name="RoleArn" target="arnType"></member> <member name="RoleSessionName"
    /// target="userNameType"></member> <member name="Policy" target="sessionPolicyDocumentType"></member>
    /// <member name="DurationSeconds" target="roleDurationSecondsType"></member> <member
    /// name="ExternalId" target="externalIdType"></member>
    /// </summary>
    public partial class AssumeRoleRequest : AmazonSecurityTokenServiceRequest
    {
        private int? _durationSeconds;
        private string _externalId;
        private string _policy;
        private string _roleArn;
        private string _roleSessionName;
        private string _serialNumber;
        private string _tokenCode;

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
        /// Gets and sets the property ExternalId. 
        /// <para>
        /// A unique identifier that is used by third parties to assume a role in their customers'
        /// accounts. For each role that the third party can assume, they should instruct their
        /// customers to create a role with the external ID that the third party generated. Each
        /// time the third party assumes the role, they must pass the customer's external ID.
        /// The external ID is useful in order to help third parties bind a role to the customer
        /// who created it. For more information about the external ID, see <a href="http://docs.aws.amazon.com/STS/latest/UsingSTS/sts-delegating-externalid.html"
        /// target="_blank">About the External ID</a> in <i>Using Temporary Security Credentials</i>.
        /// 
        /// </para>
        /// </summary>
        public string ExternalId
        {
            get { return this._externalId; }
            set { this._externalId = value; }
        }

        // Check to see if ExternalId property is set
        internal bool IsSetExternalId()
        {
            return this._externalId != null;
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
        /// for AssumeRole</a> in <i>Using Temporary Security Credentials</i>. 
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
        /// An identifier for the assumed role session. The session name is included as part of
        /// the <code>AssumedRoleUser</code>. 
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
        /// Gets and sets the property SerialNumber. 
        /// <para>
        /// The identification number of the MFA device that is associated with the user who is
        /// making the <code>AssumeRole</code> call. Specify this value if the trust policy of
        /// the role being assumed includes a condition that requires MFA authentication. The
        /// value is either the serial number for a hardware device (such as <code>GAHT12345678</code>)
        /// or an Amazon Resource Name (ARN) for a virtual device (such as <code>arn:aws:iam::123456789012:mfa/user</code>).
        /// 
        /// </para>
        /// </summary>
        public string SerialNumber
        {
            get { return this._serialNumber; }
            set { this._serialNumber = value; }
        }

        // Check to see if SerialNumber property is set
        internal bool IsSetSerialNumber()
        {
            return this._serialNumber != null;
        }

        /// <summary>
        /// Gets and sets the property TokenCode. 
        /// <para>
        /// The value provided by the MFA device, if the trust policy of the role being assumed
        /// requires MFA (that is, if the policy includes a condition that tests for MFA). If
        /// the role being assumed requires MFA and if the <code>TokenCode</code> value is missing
        /// or expired, the <code>AssumeRole</code> call returns an "access denied" error. 
        /// </para>
        /// </summary>
        public string TokenCode
        {
            get { return this._tokenCode; }
            set { this._tokenCode = value; }
        }

        // Check to see if TokenCode property is set
        internal bool IsSetTokenCode()
        {
            return this._tokenCode != null;
        }

    }
}
