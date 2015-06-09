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

namespace Amazon.CognitoSync.SyncManager
{
    /// <summary>
    /// A model which stores conflicting record from the remote storage and the local storage.
    /// </summary>
    public class SyncConflict
    {
        private readonly string _key;
        private readonly Record _remoteRecord;
        private readonly Record _localRecord;

        public string Key
        {
            get { return this._key; }
        }

        public Record RemoteRecord
        {
            get { return this._remoteRecord; }
        }

        public Record LocalRecord
        {
            get { return this._localRecord; }
        }

        /// <summary>
        /// Constructs a SyncConflict object.
        /// </summary>
        /// <param name="remoteRecord">record from remote storage</param>
        /// <param name="localRecord">record from local storage</param>
        public SyncConflict(Record remoteRecord, Record localRecord)
        {
            if (remoteRecord == null || localRecord == null)
            {
                throw new ArgumentException("Record can't be null");
            }
            if (!remoteRecord.Key.Equals(localRecord.Key))
            {
                throw new ArgumentException("The keys of remote record and local record don't match");
            }
            this._key = remoteRecord.Key;
            this._remoteRecord = remoteRecord;
            this._localRecord = localRecord;
        }

        /// <summary>
        /// Resolves conflict with remote record
        /// </summary>
        /// <returns>Resolved record</returns>
        public Record ResolveWithRemoteRecord()
        {
            return new Record(
                this.Key,
                this.RemoteRecord.Value,
                this.RemoteRecord.SyncCount,
                this.RemoteRecord.LastModifiedDate,
                this.RemoteRecord.LastModifiedBy,
                this.RemoteRecord.DeviceLastModifiedDate,
                false
                );
        }

        /// <summary>
        /// Resolves conflict with local record
        /// </summary>
        /// <returns>Resolved record</returns>
        public Record ResolveWithLocalRecord()
        {
            return new Record(
                this.Key,
                this.LocalRecord.Value,
                this.RemoteRecord.SyncCount,
                this.LocalRecord.LastModifiedDate,
                this.LocalRecord.LastModifiedBy,
                this.LocalRecord.DeviceLastModifiedDate,
                true
                );
        }

        /// <summary>
        /// Resolves conflict with a new value.
        /// </summary>
        /// <returns>Resolved record.</returns>
        /// <param name="newValue">new value of the record</param>
        public Record ResolveWithValue(string newValue)
        {
            return new Record(
                this.Key,
                newValue,
                this.RemoteRecord.SyncCount,
                this.LocalRecord.LastModifiedDate,
                this.LocalRecord.LastModifiedBy,
                this.LocalRecord.DeviceLastModifiedDate,
                true
                );
        }
    }
}

