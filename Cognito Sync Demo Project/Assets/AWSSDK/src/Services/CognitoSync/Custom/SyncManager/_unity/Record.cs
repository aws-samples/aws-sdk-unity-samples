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

using System;

using Amazon.CognitoSync.SyncManager.Internal;

namespace Amazon.CognitoSync.SyncManager
{
    /// <summary>
    /// A Record is the element stored in a <see cref="Dataset"/>. 
    /// There can be up to 1k records or up to 1 MB in size in a <see cref="Dataset"/>.
    /// </summary>
    public sealed class Record
    {
        private readonly string _key;
        private readonly string _value;
        private readonly long _syncCount;
        private readonly DateTime? _lastModifiedDate;
        private readonly string _lastModifiedBy;
        private readonly DateTime? _deviceLastModifiedDate;
        private readonly bool _modified;

        /// <summary>
        /// The key of this record. It's a non empty string.
        /// </summary>
        /// <value>The key associated with the Record</value>
        public string Key
        {
            get { return this._key; }
        }

        /// <summary>
        /// The value of this record. If it's null, then the record is considered as deleted.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return this._value; }
        }

        /// <summary>
        /// Equivalent to the version of this record. It's a increasing non negative
        /// long number. The value is returned from server. Do not set this value
        /// arbitrarily, or else update may fail.
        /// </summary>
        /// <value>The sync count.</value>
        public long SyncCount
        {
            get { return this._syncCount; }
        }

        /// <summary>
        /// The server timestamp of this record when it was last modified.
        /// </summary>
        /// <value>The last modified date.</value>
        public DateTime? LastModifiedDate
        {
            get { return this._lastModifiedDate; }
        }

        /// <summary>
        /// An identifier of the user or device which modified this record last.
        /// </summary>
        /// <value>The last modified by.</value>
        public string LastModifiedBy
        {
            get { return this._lastModifiedBy; }
        }

        /// <summary>
        /// The local timestamp of this record when it was last modified.
        /// </summary>
        /// <value>The device last modified date.</value>
        public DateTime? DeviceLastModifiedDate
        {
            get { return this._deviceLastModifiedDate; }
        }

        /// <summary>
        /// A flag that indicates whether this record is modified locally but hasn't
        /// been synced.
        /// </summary>
        /// <value><c>true</c> if modified; otherwise, <c>false</c>.</value>
        public bool IsModified
        {
            get { return this._modified; }
        }

        public Record(string key, string value, long syncCount, DateTime? lastModifiedDate, string lastModifiedBy, DateTime? deviceLastModifiedDate, bool modified)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            this._key = key;
            this._value = value;
            this._syncCount = syncCount;
            this._lastModifiedDate = lastModifiedDate != null? lastModifiedDate.Value.ToUniversalTime() : lastModifiedDate;
            this._lastModifiedBy = lastModifiedBy;
            this._deviceLastModifiedDate = deviceLastModifiedDate != null? deviceLastModifiedDate.Value.ToUniversalTime() : deviceLastModifiedDate;
            this._modified = modified;
        }

        /// <summary>
        /// Gets whether the record is marked as deleted. True if value is null,
        /// false otherwise.
        /// </summary>
        /// <returns><c>true</c> if the record is marked as deleted; otherwise, <c>false</c>.</returns>
        public bool IsDeleted
        {
            get
            {
                return Value == null;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "Key:[{0}], Value:[{1}], SyncCount:[{2}], LastModifiedDate:[{3}], LastModifiedBy:[{4}], DeviceLastModifiedDate:[{5}], IsModified:[{6}]",
                Key, Value, SyncCount, LastModifiedDate, LastModifiedBy, DeviceLastModifiedDate, IsModified);
        }
    }
}

