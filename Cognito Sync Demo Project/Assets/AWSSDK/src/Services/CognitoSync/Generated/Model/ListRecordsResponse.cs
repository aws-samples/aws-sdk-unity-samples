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
    /// Returned for a successful ListRecordsRequest.
    /// </summary>
    public partial class ListRecordsResponse : AmazonWebServiceResponse
    {
        private int? _count;
        private bool? _datasetDeletedAfterRequestedSyncCount;
        private bool? _datasetExists;
        private long? _datasetSyncCount;
        private string _lastModifiedBy;
        private List<string> _mergedDatasetNames = new List<string>();
        private string _nextToken;
        private List<Record> _records = new List<Record>();
        private string _syncSessionToken;

        /// <summary>
        /// Gets and sets the property Count. Total number of records.
        /// </summary>
        public int Count
        {
            get { return this._count.GetValueOrDefault(); }
            set { this._count = value; }
        }

        // Check to see if Count property is set
        internal bool IsSetCount()
        {
            return this._count.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property DatasetDeletedAfterRequestedSyncCount. A boolean value
        /// specifying whether to delete the dataset locally.
        /// </summary>
        public bool DatasetDeletedAfterRequestedSyncCount
        {
            get { return this._datasetDeletedAfterRequestedSyncCount.GetValueOrDefault(); }
            set { this._datasetDeletedAfterRequestedSyncCount = value; }
        }

        // Check to see if DatasetDeletedAfterRequestedSyncCount property is set
        internal bool IsSetDatasetDeletedAfterRequestedSyncCount()
        {
            return this._datasetDeletedAfterRequestedSyncCount.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property DatasetExists. Indicates whether the dataset exists.
        /// </summary>
        public bool DatasetExists
        {
            get { return this._datasetExists.GetValueOrDefault(); }
            set { this._datasetExists = value; }
        }

        // Check to see if DatasetExists property is set
        internal bool IsSetDatasetExists()
        {
            return this._datasetExists.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property DatasetSyncCount. Server sync count for this dataset.
        /// </summary>
        public long DatasetSyncCount
        {
            get { return this._datasetSyncCount.GetValueOrDefault(); }
            set { this._datasetSyncCount = value; }
        }

        // Check to see if DatasetSyncCount property is set
        internal bool IsSetDatasetSyncCount()
        {
            return this._datasetSyncCount.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property LastModifiedBy. The user/device that made the last change
        /// to this record.
        /// </summary>
        public string LastModifiedBy
        {
            get { return this._lastModifiedBy; }
            set { this._lastModifiedBy = value; }
        }

        // Check to see if LastModifiedBy property is set
        internal bool IsSetLastModifiedBy()
        {
            return this._lastModifiedBy != null;
        }

        /// <summary>
        /// Gets and sets the property MergedDatasetNames. Names of merged datasets.
        /// </summary>
        public List<string> MergedDatasetNames
        {
            get { return this._mergedDatasetNames; }
            set { this._mergedDatasetNames = value; }
        }

        // Check to see if MergedDatasetNames property is set
        internal bool IsSetMergedDatasetNames()
        {
            return this._mergedDatasetNames != null && this._mergedDatasetNames.Count > 0; 
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
        /// Gets and sets the property Records. A list of all records.
        /// </summary>
        public List<Record> Records
        {
            get { return this._records; }
            set { this._records = value; }
        }

        // Check to see if Records property is set
        internal bool IsSetRecords()
        {
            return this._records != null && this._records.Count > 0; 
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
