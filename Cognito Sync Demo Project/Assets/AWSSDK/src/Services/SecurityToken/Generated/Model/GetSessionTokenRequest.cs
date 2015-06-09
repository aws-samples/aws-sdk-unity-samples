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
    /// Container for the parameters to the GetSessionToken operation.
    /// Returns a set of temporary credentials for an AWS account or IAM user. The credentials
    /// consist of an access key ID, a secret access key, and a security token. Typically,
    /// you use <code>GetSessionToken</code> if you want to use MFA to protect programmatic
    /// calls to specific AWS APIs like Amazon EC2 <code>StopInstances</code>. MFA-enabled
    /// IAM users would need to call <code>GetSessionToken</code> and submit an MFA code that
    /// is associated with their MFA device. Using the temporary security credentials that
    /// are returned from the call, IAM users can then make programmatic calls to APIs that
    /// require MFA authentication. 
    /// 
    ///  
    /// <para>
    /// The <code>GetSessionToken</code> action must be called by using the long-term AWS
    /// security credentials of the AWS account or an IAM user. Credentials that are created
    /// by IAM users are valid for the duration that you specify, between 900 seconds (15
    /// minutes) and 129600 seconds (36 hours); credentials that are created by using account
    /// credentials have a maximum duration of 3600 seconds (1 hour). 
    /// </para>
    ///  
    /// <para>
    /// The permissions associated with the temporary security credentials returned by <code>GetSessionToken</code>
    /// are based on the permissions associated with account or IAM user whose credentials
    /// are used to call the action. If <code>GetSessionToken</code> is called using root
    /// account credentials, the temporary credentials have root account permissions. Similarly,
    /// if <code>GetSessionToken</code> is called using the credentials of an IAM user, the
    /// temporary credentials have the same permissions as the IAM user. 
    /// </para>
    ///  
    /// <para>
    /// For more information about using <code>GetSessionToken</code> to create temporary
    /// credentials, go to <a href="http://docs.aws.amazon.com/STS/latest/UsingSTS/CreatingSessionTokens.html"
    /// target="_blank">Creating Temporary Credentials to Enable Access for IAM Users</a>
    /// in <i>Using Temporary Security Credentials</i>. 
    /// </para>
    /// </summary>
    public partial class GetSessionTokenRequest : AmazonSecurityTokenServiceRequest
    {
        private int? _durationSeconds;
        private string _serialNumber;
        private string _tokenCode;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public GetSessionTokenRequest() { }

        /// <summary>
        /// Gets and sets the property DurationSeconds. 
        /// <para>
        /// The duration, in seconds, that the credentials should remain valid. Acceptable durations
        /// for IAM user sessions range from 900 seconds (15 minutes) to 129600 seconds (36 hours),
        /// with 43200 seconds (12 hours) as the default. Sessions for AWS account owners are
        /// restricted to a maximum of 3600 seconds (one hour). If the duration is longer than
        /// one hour, the session for AWS account owners defaults to one hour. 
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
        /// Gets and sets the property SerialNumber. 
        /// <para>
        /// The identification number of the MFA device that is associated with the IAM user who
        /// is making the <code>GetSessionToken</code> call. Specify this value if the IAM user
        /// has a policy that requires MFA authentication. The value is either the serial number
        /// for a hardware device (such as <code>GAHT12345678</code>) or an Amazon Resource Name
        /// (ARN) for a virtual device (such as <code>arn:aws:iam::123456789012:mfa/user</code>).
        /// You can find the device for an IAM user by going to the AWS Management Console and
        /// viewing the user's security credentials. 
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
        /// The value provided by the MFA device, if MFA is required. If any policy requires the
        /// IAM user to submit an MFA code, specify this value. If MFA authentication is required,
        /// and the user does not provide a code when requesting a set of temporary security credentials,
        /// the user will receive an "access denied" response when requesting resources that require
        /// MFA authentication.
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
