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
 * Do not modify this file. This file is generated from the cognito-sync-2014-06-30.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.CognitoSync.Model
{
    /// <summary>
    /// The output for the GetBulkPublishDetails operation.
    /// </summary>
    public partial class GetBulkPublishDetailsResult : AmazonWebServiceResponse
    {
        private DateTime? _bulkPublishCompleteTime;
        private DateTime? _bulkPublishStartTime;
        private BulkPublishStatus _bulkPublishStatus;
        private string _failureMessage;
        private string _identityPoolId;

        /// <summary>
        /// Gets and sets the property BulkPublishCompleteTime. If BulkPublishStatus is SUCCEEDED,
        /// the time the last bulk publish operation completed.
        /// </summary>
        public DateTime BulkPublishCompleteTime
        {
            get { return this._bulkPublishCompleteTime.GetValueOrDefault(); }
            set { this._bulkPublishCompleteTime = value; }
        }

        // Check to see if BulkPublishCompleteTime property is set
        internal bool IsSetBulkPublishCompleteTime()
        {
            return this._bulkPublishCompleteTime.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property BulkPublishStartTime. The date/time at which the last bulk
        /// publish was initiated.
        /// </summary>
        public DateTime BulkPublishStartTime
        {
            get { return this._bulkPublishStartTime.GetValueOrDefault(); }
            set { this._bulkPublishStartTime = value; }
        }

        // Check to see if BulkPublishStartTime property is set
        internal bool IsSetBulkPublishStartTime()
        {
            return this._bulkPublishStartTime.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property BulkPublishStatus. Status of the last bulk publish operation,
        /// valid values are: 
        /// <para>
        /// NOT_STARTED - No bulk publish has been requested for this identity pool
        /// </para>
        ///  
        /// <para>
        /// IN_PROGRESS - Data is being published to the configured stream
        /// </para>
        ///  
        /// <para>
        /// SUCCEEDED - All data for the identity pool has been published to the configured stream
        /// </para>
        ///  
        /// <para>
        /// FAILED - Some portion of the data has failed to publish, check FailureMessage for
        /// the cause.
        /// </para>
        /// </summary>
        public BulkPublishStatus BulkPublishStatus
        {
            get { return this._bulkPublishStatus; }
            set { this._bulkPublishStatus = value; }
        }

        // Check to see if BulkPublishStatus property is set
        internal bool IsSetBulkPublishStatus()
        {
            return this._bulkPublishStatus != null;
        }

        /// <summary>
        /// Gets and sets the property FailureMessage. If BulkPublishStatus is FAILED this field
        /// will contain the error message that caused the bulk publish to fail.
        /// </summary>
        public string FailureMessage
        {
            get { return this._failureMessage; }
            set { this._failureMessage = value; }
        }

        // Check to see if FailureMessage property is set
        internal bool IsSetFailureMessage()
        {
            return this._failureMessage != null;
        }

        /// <summary>
        /// Gets and sets the property IdentityPoolId. A name-spaced GUID (for example, us-east-1:23EC4050-6AEA-7089-A2DD-08002EXAMPLE)
        /// created by Amazon Cognito. GUID generation is unique within a region.
        /// </summary>
        public string IdentityPoolId
        {
            get { return this._identityPoolId; }
            set { this._identityPoolId = value; }
        }

        // Check to see if IdentityPoolId property is set
        internal bool IsSetIdentityPoolId()
        {
            return this._identityPoolId != null;
        }

    }
}
