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
    /// Contains the result of a successful call to the <a>AssumeRoleWithSAML</a> action,
    /// including temporary AWS credentials that can be used to make AWS requests.
    /// </summary>
    public partial class AssumeRoleWithSAMLResponse : AmazonWebServiceResponse
    {
        private AssumedRoleUser _assumedRoleUser;
        private string _audience;
        private Credentials _credentials;
        private string _issuer;
        private string _nameQualifier;
        private int? _packedPolicySize;
        private string _subject;
        private string _subjectType;

        /// <summary>
        /// Gets and sets the property AssumedRoleUser.
        /// </summary>
        public AssumedRoleUser AssumedRoleUser
        {
            get { return this._assumedRoleUser; }
            set { this._assumedRoleUser = value; }
        }

        // Check to see if AssumedRoleUser property is set
        internal bool IsSetAssumedRoleUser()
        {
            return this._assumedRoleUser != null;
        }

        /// <summary>
        /// Gets and sets the property Audience. 
        /// <para>
        ///  The value of the <code>Recipient</code> attribute of the <code>SubjectConfirmationData</code>
        /// element of the SAML assertion. 
        /// </para>
        /// </summary>
        public string Audience
        {
            get { return this._audience; }
            set { this._audience = value; }
        }

        // Check to see if Audience property is set
        internal bool IsSetAudience()
        {
            return this._audience != null;
        }

        /// <summary>
        /// Gets and sets the property Credentials.
        /// </summary>
        public Credentials Credentials
        {
            get { return this._credentials; }
            set { this._credentials = value; }
        }

        // Check to see if Credentials property is set
        internal bool IsSetCredentials()
        {
            return this._credentials != null;
        }

        /// <summary>
        /// Gets and sets the property Issuer. 
        /// <para>
        /// The value of the <code>Issuer</code> element of the SAML assertion.
        /// </para>
        /// </summary>
        public string Issuer
        {
            get { return this._issuer; }
            set { this._issuer = value; }
        }

        // Check to see if Issuer property is set
        internal bool IsSetIssuer()
        {
            return this._issuer != null;
        }

        /// <summary>
        /// Gets and sets the property NameQualifier. 
        /// <para>
        /// A hash value based on the concatenation of the <code>Issuer</code> response value,
        /// the AWS account ID, and the friendly name (the last part of the ARN) of the SAML provider
        /// in IAM. The combination of <code>NameQualifier</code> and <code>Subject</code> can
        /// be used to uniquely identify a federated user. 
        /// </para>
        ///  
        /// <para>
        /// The following pseudocode shows how the hash value is calculated:
        /// </para>
        ///  
        /// <para>
        ///  <code>BASE64 ( SHA1 ( "https://example.com/saml" + "123456789012" + "/MySAMLIdP"
        /// ) )</code> 
        /// </para>
        /// </summary>
        public string NameQualifier
        {
            get { return this._nameQualifier; }
            set { this._nameQualifier = value; }
        }

        // Check to see if NameQualifier property is set
        internal bool IsSetNameQualifier()
        {
            return this._nameQualifier != null;
        }

        /// <summary>
        /// Gets and sets the property PackedPolicySize. 
        /// <para>
        /// A percentage value that indicates the size of the policy in packed form. The service
        /// rejects any policy with a packed size greater than 100 percent, which means the policy
        /// exceeded the allowed space. 
        /// </para>
        /// </summary>
        public int PackedPolicySize
        {
            get { return this._packedPolicySize.GetValueOrDefault(); }
            set { this._packedPolicySize = value; }
        }

        // Check to see if PackedPolicySize property is set
        internal bool IsSetPackedPolicySize()
        {
            return this._packedPolicySize.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property Subject. 
        /// <para>
        /// The value of the <code>NameID</code> element in the <code>Subject</code> element of
        /// the SAML assertion.
        /// </para>
        /// </summary>
        public string Subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }

        // Check to see if Subject property is set
        internal bool IsSetSubject()
        {
            return this._subject != null;
        }

        /// <summary>
        /// Gets and sets the property SubjectType. 
        /// <para>
        ///  The format of the name ID, as defined by the <code>Format</code> attribute in the
        /// <code>NameID</code> element of the SAML assertion. Typical examples of the format
        /// are <code>transient</code> or <code>persistent</code>. 
        /// </para>
        ///  
        /// <para>
        ///  If the format includes the prefix <code>urn:oasis:names:tc:SAML:2.0:nameid-format</code>,
        /// that prefix is removed. For example, <code>urn:oasis:names:tc:SAML:2.0:nameid-format:transient</code>
        /// is returned as <code>transient</code>. If the format includes any other prefix, the
        /// format is returned with no modifications.
        /// </para>
        /// </summary>
        public string SubjectType
        {
            get { return this._subjectType; }
            set { this._subjectType = value; }
        }

        // Check to see if SubjectType property is set
        internal bool IsSetSubjectType()
        {
            return this._subjectType != null;
        }

    }
}
