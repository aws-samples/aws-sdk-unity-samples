/**
 * Copyright 2013-2014 Amazon.com, 
 * Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Amazon Software License (the "License"). 
 * You may not use this file except in compliance with the 
 * License. A copy of the License is located at
 * 
 *     http://aws.amazon.com/asl/
 * 
 * or in the "license" file accompanying this file. This file is 
 * distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 * CONDITIONS OF ANY KIND, express or implied. See the License 
 * for the specific language governing permissions and 
 * limitations under the License.
 */
using System;
using System.Collections;
using System.Collections.Generic;

using Amazon.CognitoSync.SyncManager.Exceptions;
using Amazon.Common;

namespace Amazon.CognitoSync.SyncManager
{

    /// <summary>
    /// Information about an SyncSuccess Event.
    /// </summary>
    public class SyncSuccessEvent : EventArgs
    {
        public List<Record> UpdatedRecords { get; private set; }

        internal SyncSuccessEvent(List<Record> updatedRecords)
        {
            this.UpdatedRecords = updatedRecords;
        }
    }

    /// <summary>
    /// Information about an SyncFailure Event.
    /// </summary>
    public class SyncFailureEvent : EventArgs
    {
        public DataStorageException Exception { get; private set; }

        internal SyncFailureEvent(DataStorageException exception)
        {
            this.Exception = exception;
        }
    }

    /// <summary>
    /// Dataset is the container of <see cref="Amazon.CognitoSync.SyncManager.Record"/>s. It can have up to 1k
    /// <see cref="Amazon.CognitoSync.SyncManager.Record"/> or 1 MB in size. A typical use of {@link Dataset} is the
    /// following.
    /// 
    /// <code>
    /// // open or create dataset
    /// Dataset dataset = cognitoClient.openOrCreate(&quot;new dataset&quot;);
    /// // synchronize. It pulls down latest changes from remote storage
    /// // and push local changes to remote storage
    /// dataset.Synchronize();
    /// // reads value
    /// String highScore = dataset.getValue(&quot;high_score&quot;);
    /// String name = dataset.getValue(&quot;name&quot;);
    /// // sets value
    /// dataset.put(&quot;high_score&quot;, &quot;90&quot;);
    /// dataset.put(&quot;name&quot;, &quot;John&quot;);
    /// // push changes to remote if needed
    /// dataset.synchronize();
    /// </code>
    /// </summary>
    public abstract class Dataset
    {

        /// <summary>
        /// Retrieves the associated {@link DatasetMetadata} from local storage.
        /// </summary>
        /// <value>The metadata for the Dataset.</value>
        public abstract DatasetMetadata Metadata
        {
            get;
        }

        /// Synchronize <see cref="Dataset"/> between local storage and remote storage.
        /// </summary>
        public abstract void Synchronize();

        /// <summary>
        /// Attempt to synchronize <see cref="Dataset"/> when connectivity is available. If
        /// the connectivity is available right away, it behaves the same as
        /// <see cref="Dataset.Synchronize"/>. Otherwise it listens to connectivity
        /// changes, and will do a sync once the connectivity is back. Note that if
        /// this method is called multiple times, only the last synchronize request
        /// is kept and only the last callback will fire. If either the dataset or
        /// the callback is garbage collected, this method will not perform a sync
        /// and the callback won't fire.
        /// </summary>
        public abstract void SynchronizeOnConnectivity();

        /// <summary>
        /// Gets the value of a <see cref="Dataset.Record"/> with the given key. If the
        /// <see cref="Amazon.CognitoSync.SyncManager.Record"/> doesn't exist or is marked deleted, null will be returned.
        /// </summary>
        /// <param name="key">key of the record in the dataset.</param>
        public abstract string Get(string key);

        /// <summary>
        /// Puts a <see cref="Amazon.CognitoSync.SyncManager.Record"/> with the given key and value into the
        /// {@link Dataset}. If a <see cref="Amazon.CognitoSync.SyncManager.Record"/> with the same key exists, its value
        /// will be overwritten. If a <see cref="Amazon.CognitoSync.SyncManager.Record"/> is marked as deleted previously,
        /// then it will be resurrected with new value while the sync count continues
        /// with previous value. No matter whether the value changes or not, the
        /// record is considered as updated, and it will be written to Cognito Sync
        /// service on next synchronize operation. If value is null, a
        /// {@link NullPointerException} will be thrown.
        /// </summary>
        /// <param name="key">key of the record</param>
        /// <param name="value">string value of a <see cref="Amazon.CognitoSync.SyncManager.Record"/> to be put into the
        /// <see cref="Dataset"/></param>
        public abstract void Put(string key, string value);

        /// <summary>
        /// Populates a dataset with a dictionary of key/value pairs
        /// </summary>
        /// <param name="values">Values.</param>
        public abstract void PutAll(Dictionary<string, string> values);

        /// <summary>
        /// Marks a <see cref="Amazon.CognitoSync.SyncManager.Record"/> with the given key as deleted. Nothing happens if
        /// the <see cref="Amazon.CognitoSync.SyncManager.Record"/> doesn't exist or is deleted already.
        /// </summary>
        /// <param name="key">Key.</param>
        public abstract void Remove(string key);

        /// <summary>
        /// Saves resolved conflicting <see cref="Amazon.CognitoSync.SyncManager.Record">s into local storage. This is
        /// used inside {@link SyncCallback#onConflict(Dataset, List)} after you
        /// resolve all conflicts.
        /// </summary>
        /// <param name="resolvedConflicts">a list of records to save into local storage</param>
        public abstract void Resolve(List<Record> resolvedConflicts);

        /// <summary>
        /// Retrieves all raw records, marked deleted or not, from local storage.
        /// </summary>
        /// <returns>List of all raw records.</returns>
        public abstract List<Record> GetAllRecords();

        /// <summary>
        /// Gets the key-value representation of all records of this dataset. Marked
        /// as deleted records are excluded.
        /// </summary>
        /// <returns>key-value representation of all records, excluding deleted ones</returns>
        public abstract Dictionary<string, string> GetAll();

        /// <summary>
        /// Gets the total size in bytes of this dataset. Records that are marked as
        /// deleted don't contribute to the total size.
        /// </summary>
        /// <returns>The total size in bytes.</returns>
        public abstract long GetTotalSizeInBytes();

        /// <summary>
        /// Gets the size of a record with the given key. If the key is deleted, -1
        /// will be returned.
        /// </summary>
        /// <returns>The size in bytes.</returns>
        /// <param name="key">the key of a record</param>
        public abstract long GetSizeInBytes(string key);

        /// <summary>
        /// Retrieves the status of a record.
        /// </summary>
        /// <returns><c>true</c> if it is modified locally; otherwise, <c>false</c>.</returns>
        /// <param name="key">key the key of a record</param>
        public abstract bool IsChanged(string key);

        /// <summary>
        /// Delete this {@link Dataset}. No more following operations on this
        /// dataset, or else {@link IllegalStateException} will be thrown.
        /// </summary>
        public abstract void Delete();

        #region SynchronizeEvents

        /// <summary>
        /// This is called after remote changes are downloaded to local storage
        /// and local changes are uploaded to remote storage. Updated records
        /// from remote storage are passed in the callback. If conflicts occur
        /// during synchronize and are resolved in {@link #onConflict} after
        /// several retries, then updatedRecords will be what are pulled down
        /// from remote in the last retry.
        /// 
        /// <param name="dataset">the dataset that performed sync</param> 
        /// <param name="updatedRecords">new records from remote storage that are downloaded</param>            
        /// </summary>
        public event EventHandler<SyncSuccessEvent> OnSyncSuccess;

        /// <summary>
        /// This is called when an exception occurs during sync.
        /// 
        /// <param name="dse">exception</param> 
        /// </summary>
        public event EventHandler<SyncFailureEvent> OnSyncFailure;

        protected void FireSyncSuccessEvent(List<Record> records)
        {
            if (OnSyncSuccess != null)
            {
                OnSyncSuccess(this, new SyncSuccessEvent(records));
            }
        }

        protected void FireSyncFailureEvent(Exception exception)
        {
            if (OnSyncFailure != null)
            {
                var dse = exception as DataStorageException;

                if (dse == null)
                    dse = new DataStorageException(exception);

                OnSyncFailure(this, new SyncFailureEvent(dse));
            }
        }
        #endregion

        #region SynchronizeDelegates
        public delegate bool SyncConflictDelegate(Dataset dataset, List<SyncConflict> conflicts);

        public delegate bool DatasetDeletedDelegate(Dataset dataset);

        public delegate bool DatasetMergedDelegate(Dataset dataset, List<string> datasetNames);


        /// <summary>
        /// This can be triggered during two phases. One is when the remote
        /// changes are about to be written to local storage. The other is when
        /// local changes are uploaded to remote storage and got rejected. Here
        /// is an example:
        /// 
        /// <code>
        /// List&lt;Record&gt; resolved = new List&lt;Record&gt;();
        /// for (SyncConflict conflict in conflicts) {
        ///     resolved.add(conflicts.resolveWithRemoteRecord());
        /// }
        /// dataset.save(resolved);
        /// return true; // so that synchronize() can retry
        /// </code>
        /// If you prefer to add additional logic when resolving conflict, you
        /// can use <see cref="SyncConflict#resolveWithValue(String)"/>
        /// <code>
        /// int remoteMoney = Integer.valueOf(conflicts.getRemote().getValue());
        /// int localMoney = Integer.valueOf(conflicts.getLocal().getValue());
        /// int total = remoteMoney + localMoney;
        /// Record resolve = conflicts.resolveWithValue(String.valueOf(total));
        /// </code>
        /// </summary>
        /// 
        /// <param name="dataset">the dataset that performed sync
        /// <param name="conflicts">conflicting records</param> 
        /// <return> true if conflicts are resolved so that synchronize will
        ///         retry, false otherwise.</return>
        public SyncConflictDelegate OnSyncConflict;

        /// <summary>
        /// This is triggered when the given dataset is deleted remotely. Return
        /// true if you want to remove local dataset, or false if you want to
        /// keep it.
        /// 
        /// <param name="dataset"> dataset handler</param> 
        /// <param name="datasetName"> the name of the dataset that is deleted remotely</param> 
        /// <return> true to remove local dataset, or false to keep it</return>
        /// </summary>
        public DatasetDeletedDelegate OnDatasetDeleted;

        /// <summary>
        /// If two or more datasets are merged as a result of identity merge,
        /// this will be triggered. A list of names of merged datasets' is passed
        /// in. The merged dataset name will be appended with its old identity
        /// id. One can open the merged dataset, synchronize the content,
        /// reconcile with the current dataset, and remove it. This callback will
        /// fire off until the merged dataset is removed.
        /// 
        /// <param name="dataset"></para> dataset handler</param> 
        /// <param name="datasetNames"> a list of names of merged datasets'</param> 
        /// <return></return>
        /// </summary>
        public DatasetMergedDelegate OnDatasetMerged;

        #endregion

        #region Default conflict resolution
        protected bool DefaultConflictResolution(List<SyncConflict> conflicts)
        {
            AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Info, this.GetType().ToString(), "Last writer wins conflict resolution for dataset ");

            List<Record> resolvedRecords = new List<Record>();

            foreach (SyncConflict conflict in conflicts)
            {
                if (conflict.RemoteRecord == null || conflict.LocalRecord.LastModifiedDate.Value.CompareTo(conflict.RemoteRecord.LastModifiedDate) > 0)
                {
                    resolvedRecords.Add(conflict.ResolveWithLocalRecord());
                }
                else
                {
                    resolvedRecords.Add(conflict.ResolveWithRemoteRecord());
                }
            }
            this.Resolve(resolvedRecords);
            return true;
        }
        #endregion
    }
}

