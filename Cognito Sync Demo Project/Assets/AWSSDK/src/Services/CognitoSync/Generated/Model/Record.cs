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
    /// The basic data structure of a dataset.
    /// </summary>
    public partial class Record
    {
        private DateTime? _deviceLastModifiedDate;
        private string _key;
        private string _lastModifiedBy;
        private DateTime? _lastModifiedDate;
        private long? _syncCount;
        private string _value;

        /// <summary>
        /// Gets and sets the property DeviceLastModifiedDate. The last modified date of the client
        /// device.
        /// </summary>
        public DateTime DeviceLastModifiedDate
        {
            get { return this._deviceLastModifiedDate.GetValueOrDefault(); }
            set { this._deviceLastModifiedDate = value; }
        }

        // Check to see if DeviceLastModifiedDate property is set
        internal bool IsSetDeviceLastModifiedDate()
        {
            return this._deviceLastModifiedDate.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property Key. The key for the record.
        /// </summary>
        public string Key
        {
            get { return this._key; }
            set { this._key = value; }
        }

        // Check to see if Key property is set
        internal bool IsSetKey()
        {
            return this._key != null;
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
        /// Gets and sets the property LastModifiedDate. The date on which the record was last
        /// modified.
        /// </summary>
        public DateTime LastModifiedDate
        {
            get { return this._lastModifiedDate.GetValueOrDefault(); }
            set { this._lastModifiedDate = value; }
        }

        // Check to see if LastModifiedDate property is set
        internal bool IsSetLastModifiedDate()
        {
            return this._lastModifiedDate.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property SyncCount. The server sync count for this record.
        /// </summary>
        public long SyncCount
        {
            get { return this._syncCount.GetValueOrDefault(); }
            set { this._syncCount = value; }
        }

        // Check to see if SyncCount property is set
        internal bool IsSetSyncCount()
        {
            return this._syncCount.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property Value. The value for the record.
        /// </summary>
        public string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        // Check to see if Value property is set
        internal bool IsSetValue()
        {
            return this._value != null;
        }

    }
}
