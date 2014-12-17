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
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.SecurityToken.Model
{
    /// <summary>
    /// Identifiers for the federated user that is associated with the credentials.
    /// </summary>
    public partial class FederatedUser
    {
        private string _arn;
        private string _federatedUserId;


        /// <summary>
        /// Gets and sets the property Arn. 
        /// <para>
        /// The ARN that specifies the federated user that is associated with the credentials.
        /// For more information about ARNs and how to use them in policies, see <a href="http://docs.aws.amazon.com/IAM/latest/UserGuide/Using_Identifiers.html"
        /// target="_blank">Identifiers for IAM Entities</a> in <i>Using IAM</i>. 
        /// </para>
        /// </summary>
        public string Arn
        {
            get { return this._arn; }
            set { this._arn = value; }
        }

        // Check to see if Arn property is set
        internal bool IsSetArn()
        {
            return this._arn != null;
        }


        /// <summary>
        /// Gets and sets the property FederatedUserId. 
        /// <para>
        /// The string that identifies the federated user associated with the credentials, similar
        /// to the unique ID of an IAM user.
        /// </para>
        /// </summary>
        public string FederatedUserId
        {
            get { return this._federatedUserId; }
            set { this._federatedUserId = value; }
        }

        // Check to see if FederatedUserId property is set
        internal bool IsSetFederatedUserId()
        {
            return this._federatedUserId != null;
        }

    }
}