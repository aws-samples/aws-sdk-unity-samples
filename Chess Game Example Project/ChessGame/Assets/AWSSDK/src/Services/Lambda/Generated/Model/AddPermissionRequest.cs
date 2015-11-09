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
 * Do not modify this file. This file is generated from the lambda-2015-03-31.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.Lambda.Model
{
    /// <summary>
    /// Container for the parameters to the AddPermission operation.
    /// Adds a permission to the access policy associated with the specified AWS Lambda function.
    /// In a "push event" model, the access policy attached to the Lambda function grants
    /// Amazon S3 or a user application permission for the Lambda <code>lambda:Invoke</code>
    /// action. For information about the push model, see <a href="http://docs.aws.amazon.com/lambda/latest/dg/lambda-introduction.html">AWS
    /// Lambda: How it Works</a>. Each Lambda function has one access policy associated with
    /// it. You can use the <code>AddPermission</code> API to add a permission to the policy.
    /// You have one access policy but it can have multiple permission statements.
    /// 
    ///  
    /// <para>
    /// This operation requires permission for the <code>lambda:AddPermission</code> action.
    /// </para>
    /// </summary>
    public partial class AddPermissionRequest : AmazonLambdaRequest
    {
        private string _action;
        private string _functionName;
        private string _principal;
        private string _sourceAccount;
        private string _sourceArn;
        private string _statementId;

        /// <summary>
        /// Gets and sets the property Action. 
        /// <para>
        /// The AWS Lambda action you want to allow in this statement. Each Lambda action is a
        /// string starting with "lambda:" followed by the API name (see <a>Operations</a>). For
        /// example, "lambda:CreateFunction". You can use wildcard ("lambda:*") to grant permission
        /// for all AWS Lambda actions. 
        /// </para>
        /// </summary>
        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }

        // Check to see if Action property is set
        internal bool IsSetAction()
        {
            return this._action != null;
        }

        /// <summary>
        /// Gets and sets the property FunctionName. 
        /// <para>
        /// Name of the Lambda function whose access policy you are updating by adding a new permission.
        /// </para>
        ///  
        /// <para>
        ///  You can specify an unqualified function name (for example, "Thumbnail") or you can
        /// specify Amazon Resource Name (ARN) of the function (for example, "arn:aws:lambda:us-west-2:account-id:function:ThumbNail").
        /// AWS Lambda also allows you to specify only the account ID qualifier (for example,
        /// "account-id:Thumbnail"). Note that the length constraint applies only to the ARN.
        /// If you specify only the function name, it is limited to 64 character in length. 
        /// </para>
        /// </summary>
        public string FunctionName
        {
            get { return this._functionName; }
            set { this._functionName = value; }
        }

        // Check to see if FunctionName property is set
        internal bool IsSetFunctionName()
        {
            return this._functionName != null;
        }

        /// <summary>
        /// Gets and sets the property Principal. 
        /// <para>
        /// The principal who is getting this permission. It can be Amazon S3 service Principal
        /// ("s3.amazonaws.com") if you want Amazon S3 to invoke the function, an AWS account
        /// ID if you are granting cross-account permission, or any valid AWS service principal
        /// such as "sns.amazonaws.com". For example, you might want to allow a custom application
        /// in another AWS account to push events to AWS Lambda by invoking your function. 
        /// </para>
        /// </summary>
        public string Principal
        {
            get { return this._principal; }
            set { this._principal = value; }
        }

        // Check to see if Principal property is set
        internal bool IsSetPrincipal()
        {
            return this._principal != null;
        }

        /// <summary>
        /// Gets and sets the property SourceAccount. 
        /// <para>
        /// The AWS account ID (without a hyphen) of the source owner. For example, if the <code>SourceArn</code>
        /// identifies a bucket, then this is the bucket owner's account ID. You can use this
        /// additional condition to ensure the bucket you specify is owned by a specific account
        /// (it is possible the bucket owner deleted the bucket and some other AWS account created
        /// the bucket). You can also use this condition to specify all sources (that is, you
        /// don't specify the <code>SourceArn</code>) owned by a specific account. 
        /// </para>
        /// </summary>
        public string SourceAccount
        {
            get { return this._sourceAccount; }
            set { this._sourceAccount = value; }
        }

        // Check to see if SourceAccount property is set
        internal bool IsSetSourceAccount()
        {
            return this._sourceAccount != null;
        }

        /// <summary>
        /// Gets and sets the property SourceArn. 
        /// <para>
        /// This is optional; however, when granting Amazon S3 permission to invoke your function,
        /// you should specify this field with the bucket Amazon Resource Name (ARN) as its value.
        /// This ensures that only events generated from the specified bucket can invoke the function.
        /// 
        /// </para>
        ///  <important>If you add a permission for the Amazon S3 principal without providing
        /// the source ARN, any AWS account that creates a mapping to your function ARN can send
        /// events to invoke your Lambda function from Amazon S3.</important>
        /// </summary>
        public string SourceArn
        {
            get { return this._sourceArn; }
            set { this._sourceArn = value; }
        }

        // Check to see if SourceArn property is set
        internal bool IsSetSourceArn()
        {
            return this._sourceArn != null;
        }

        /// <summary>
        /// Gets and sets the property StatementId. 
        /// <para>
        /// A unique statement identifier.
        /// </para>
        /// </summary>
        public string StatementId
        {
            get { return this._statementId; }
            set { this._statementId = value; }
        }

        // Check to see if StatementId property is set
        internal bool IsSetStatementId()
        {
            return this._statementId != null;
        }

    }
}