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
    /// Container for the parameters to the ListRecords operation.
    /// Gets paginated records, optionally changed after a particular sync count for a dataset
    /// and identity. With Amazon Cognito Sync, each identity has access only to its own data.
    /// Thus, the credentials used to make this API call need to have access to the identity
    /// data.
    /// 
    ///  
    /// <para>
    /// ListRecords can be called with temporary user credentials provided by Cognito Identity
    /// or with developer credentials. You should use Cognito Identity credentials to make
    /// this API call.
    /// </para>
    /// </summary>
    public partial class ListRecordsRequest : AmazonCognitoSyncRequest
    {
        private string _datasetName;
        private string _identityId;
        private string _identityPoolId;
        private long? _lastSyncCount;
        private int? _maxResults;
        private string _nextToken;
        private string _syncSessionToken;

        /// <summary>
        /// Gets and sets the property DatasetName. A string of up to 128 characters. Allowed
        /// characters are a-z, A-Z, 0-9, '_' (underscore), '-' (dash), and '.' (dot).
        /// </summary>
        public string DatasetName
        {
            get { return this._datasetName; }
            set { this._datasetName = value; }
        }

        // Check to see if DatasetName property is set
        internal bool IsSetDatasetName()
        {
            return this._datasetName != null;
        }

        /// <summary>
        /// Gets and sets the property IdentityId. A name-spaced GUID (for example, us-east-1:23EC4050-6AEA-7089-A2DD-08002EXAMPLE)
        /// created by Amazon Cognito. GUID generation is unique within a region.
        /// </summary>
        public string IdentityId
        {
            get { return this._identityId; }
            set { this._identityId = value; }
        }

        // Check to see if IdentityId property is set
        internal bool IsSetIdentityId()
        {
            return this._identityId != null;
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

        /// <summary>
        /// Gets and sets the property LastSyncCount. The last server sync count for this record.
        /// </summary>
        public long LastSyncCount
        {
            get { return this._lastSyncCount.GetValueOrDefault(); }
            set { this._lastSyncCount = value; }
        }

        // Check to see if LastSyncCount property is set
        internal bool IsSetLastSyncCount()
        {
            return this._lastSyncCount.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property MaxResults. The maximum number of results to be returned.
        /// </summary>
        public int MaxResults
        {
            get { return this._maxResults.GetValueOrDefault(); }
            set { this._maxResults = value; }
        }

        // Check to see if MaxResults property is set
        internal bool IsSetMaxResults()
        {
            return this._maxResults.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property NextToken. A pagination token for obtaining the next page
        /// of results.
        /// </summary>
        public string NextToken
        {
            get { return this._nextToken; }
            set { this._nextToken = value; }
        }

        // Check to see if NextToken property is set
        internal bool IsSetNextToken()
        {
            return this._nextToken != null;
        }

        /// <summary>
        /// Gets and sets the property SyncSessionToken. A token containing a session ID, identity
        /// ID, and expiration.
        /// </summary>
        public string SyncSessionToken
        {
            get { return this._syncSessionToken; }
            set { this._syncSessionToken = value; }
        }

        // Check to see if SyncSessionToken property is set
        internal bool IsSetSyncSessionToken()
        {
            return this._syncSessionToken != null;
        }

    }
}
