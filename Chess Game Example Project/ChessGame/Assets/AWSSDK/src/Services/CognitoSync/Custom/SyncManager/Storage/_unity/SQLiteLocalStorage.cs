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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Amazon.Runtime;
using Amazon.CognitoSync.SyncManager;
using Amazon.CognitoSync.SyncManager.Internal;

using System.Text;
using UnityEngine;

using Amazon.Util.Storage;
using Amazon.Runtime.Internal.Util;
using Amazon.Util.Storage.Internal;

namespace Amazon.CognitoSync.SyncManager.Internal
{
    public class SQLiteLocalStorage : ILocalStorage
    {
        private static object sqlite_lock = new object();
        
        internal static readonly string TABLE_DATASETS = "datasets";
        internal static readonly string TABLE_RECORDS = "records";
        internal readonly string dataPath;
        private Logger _logger;

        static class DatasetColumns
        {
            internal static readonly string IDENTITY_ID = "identity_id";
            internal static readonly string DATASET_NAME = "dataset_name";
            internal static readonly string CREATION_TIMESTAMP = "creation_timestamp";
            internal static readonly string LAST_MODIFIED_TIMESTAMP = "last_modified_timestamp";
            internal static readonly string LAST_MODIFIED_BY = "last_modified_by";
            internal static readonly string STORAGE_SIZE_BYTES = "storage_size_bytes";
            internal static readonly string RECORD_COUNT = "record_count";
            internal static readonly string LAST_SYNC_COUNT = "last_sync_count";
            internal static readonly string LAST_SYNC_TIMESTAMP = "last_sync_timestamp";
            internal static readonly string LAST_SYNC_RESULT = "last_sync_result";
            internal static readonly string[] ALL = new string[] {
                IDENTITY_ID, DATASET_NAME,
                CREATION_TIMESTAMP, LAST_MODIFIED_TIMESTAMP, LAST_MODIFIED_BY,
                STORAGE_SIZE_BYTES, RECORD_COUNT,
                LAST_SYNC_COUNT, LAST_SYNC_TIMESTAMP, LAST_SYNC_RESULT,
            };

            public static string BuildQuery(string conditions)
            {
                string query = "SELECT " + string.Join(",", ALL) + " FROM " + SQLiteLocalStorage.TABLE_DATASETS;

                if (conditions != null && conditions.Trim().Length > 0)
                {
                    query += " WHERE " + conditions;
                }

                return query;
            }

            public static string BuildInsert()
            {
                return DatasetColumns.BuildInsert(ALL);
            }

            public static string BuildInsert(string[] fieldList)
            {
                string insert = "INSERT INTO " + SQLiteLocalStorage.TABLE_DATASETS + " (" + string.Join(",", fieldList) + ") " +
                    " VALUES (";

                for (int i = 0; i < fieldList.Length; i++)
                {
                    insert += "@" + fieldList[i] + (i < fieldList.Length - 1 ? "," : "");
                }

                insert += " )";

                return insert;
            }

            public static string BuildUpdate(string[] fieldList, string conditions)
            {
                string update = "UPDATE " + SQLiteLocalStorage.TABLE_DATASETS + " SET ";

                for (int i = 0; i < fieldList.Length; i++)
                {
                    update += fieldList[i] + " = @" + fieldList[i] + (i < fieldList.Length - 1 ? "," : "");
                }

                if (conditions != null && conditions.Trim().Length > 0)
                {
                    update += " WHERE " + conditions;
                }

                return update;
            }

            public static string BuildDelete(string conditions)
            {
                string delete = "DELETE FROM " + SQLiteLocalStorage.TABLE_DATASETS;

                if (conditions != null && conditions.Trim().Length > 0)
                {
                    delete += " WHERE " + conditions;
                }

                return delete;
            }
        }

        static class RecordColumns
        {
            internal static readonly string IDENTITY_ID = "identity_id";
            internal static readonly string DATASET_NAME = "dataset_name";
            internal static readonly string KEY = "key";
            internal static readonly string VALUE = "value";
            internal static readonly string SYNC_COUNT = "sync_count";
            internal static readonly string LAST_MODIFIED_TIMESTAMP = "last_modified_timestamp";
            internal static readonly string LAST_MODIFIED_BY = "last_modified_by";
            internal static readonly string DEVICE_LAST_MODIFIED_TIMESTAMP = "device_last_modified_timestamp";
            internal static readonly string MODIFIED = "modified";
            internal static readonly string[] ALL = new string[] {
                IDENTITY_ID, DATASET_NAME, KEY, VALUE, SYNC_COUNT, LAST_MODIFIED_TIMESTAMP,
                LAST_MODIFIED_BY, DEVICE_LAST_MODIFIED_TIMESTAMP, MODIFIED
            };

            public static string BuildQuery(string conditions)
            {
                string query = "SELECT " + string.Join(",", ALL) + " FROM " + SQLiteLocalStorage.TABLE_RECORDS;

                if (conditions != null && conditions.Trim().Length > 0)
                {
                    query += " WHERE " + conditions;
                }
                return query;
            }

            public static string BuildInsert()
            {
                return RecordColumns.BuildInsert(ALL);
            }

            public static string BuildInsert(string[] fieldList)
            {
                string insert = "INSERT INTO " + SQLiteLocalStorage.TABLE_RECORDS + " (" + string.Join(",", fieldList) + ") " +
                    " VALUES (";

                for (int i = 0; i < fieldList.Length; i++)
                {
                    insert += "@" + fieldList[i] + (i < fieldList.Length - 1 ? "," : "");
                }

                insert += " )";

                return insert;
            }

            public static string BuildUpdate(string[] fieldList, string conditions)
            {
                string update = "UPDATE " + SQLiteLocalStorage.TABLE_RECORDS + " SET ";

                for (int i = 0; i < fieldList.Length; i++)
                {
                    update += fieldList[i] + " = @" + fieldList[i] + (i < fieldList.Length - 1 ? "," : "");
                }

                if (conditions != null && conditions.Trim().Length > 0)
                {
                    update += " WHERE " + conditions;
                }

                return update;
            }

            public static string BuildDelete(string conditions)
            {
                string delete = "DELETE FROM " + SQLiteLocalStorage.TABLE_RECORDS;

                if (conditions != null && conditions.Trim().Length > 0)
                {
                    delete += " WHERE " + conditions;
                }

                return delete;
            }
        }

        public SQLiteLocalStorage(string path)
        {
            _logger = Logger.GetLogger(this.GetType());
            this.dataPath = path;
            this.SetupDatabase();
        }

        ~SQLiteLocalStorage()
        {
            if (db != null)
                db.CloseDatabase();
        }


        public static byte[] ToUtf8(string sText)
        {
            byte[] byteArray;
            int nLen = Encoding.UTF8.GetByteCount(sText) + 1;
            byteArray = new byte[nLen];
            nLen = Encoding.UTF8.GetBytes(sText, 0, sText.Length, byteArray, 0);
            byteArray[nLen] = 0;
            return byteArray;
        }

        public  void CreateDataset(string identityId, string datasetName)
        {
            lock (sqlite_lock)
            {
                DatasetMetadata metadata = this.GetMetadataInternal(identityId, datasetName);

                if (metadata == null)
                {
                    SQLiteStatement stmt = null;
                    try
                    {


                        string query = DatasetColumns.BuildInsert(
                        new string[] {
                        DatasetColumns.IDENTITY_ID,
                        DatasetColumns.DATASET_NAME,
                        DatasetColumns.CREATION_TIMESTAMP,
                        DatasetColumns.LAST_MODIFIED_TIMESTAMP
                       });

                        stmt = db.Prepare(query);
                        stmt.BindText(1, identityId);
                        stmt.BindText(2, datasetName);
                        stmt.BindDateTime(3, DateTime.Now);
                        stmt.BindDateTime(4, DateTime.Now);

                        stmt.Step();

                    }
                    finally
                    {
                        stmt.FinalizeStm();
                    }
                }
            }
        }

        public  string GetValue(string identityId, string datasetName, string key)
        {
            lock (sqlite_lock)
            {
                Record record = this.GetRecord(identityId, datasetName, key);

                if (record == null)
                {
                    return null;
                }
                else
                {
                    return record.Value;
                }
            }
        }

        public  void PutValue(string identityId, string datasetName, string key, string value)
        {
            lock (sqlite_lock)
            {
                bool result = this.PutValueInternal(identityId, datasetName, key, value);

                if (!result)
                {
                    _logger.DebugFormat("{0}", @"Cognito Sync - SQLiteStorage - Put Value Failed");
                }
                else
                {
                    this.UpdateLastModifiedTimestamp(identityId, datasetName);
                }
            }
        }

        public  Dictionary<string, string> GetValueMap(string identityId, string datasetName)
        {
            lock (sqlite_lock)
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                List<Record> records = this.GetRecords(identityId, datasetName);

                foreach (Record record in records)
                {
                    if (!record.IsDeleted)
                    {
                        values.Add(record.Key, record.Value);
                    }
                }

                return values;
            }
        }

        public  void PutAllValues(string identityId, string datasetName, IDictionary<string, string> values)
        {
            lock (sqlite_lock)
            {
                foreach (KeyValuePair<string, string> entry in values)
                {
                    this.PutValueInternal(identityId, datasetName, entry.Key, entry.Value);
                }
                this.UpdateLastModifiedTimestamp(identityId, datasetName);
            }
        }

        public  List<DatasetMetadata> GetDatasetMetadata(string identityId)
        {
            lock (sqlite_lock)
            {
                List<DatasetMetadata> datasets = new List<DatasetMetadata>();
                SQLiteStatement stmt = null;
                try
                {


                    stmt = db.Prepare(DatasetColumns.BuildQuery(
                    DatasetColumns.IDENTITY_ID + " = @whereIdentityId"
                    ));

                    stmt.BindText(1, identityId);

                    while (stmt.Read())
                    {
                        datasets.Add(this.SqliteStmtToDatasetMetadata(stmt));
                    }

                }
                finally
                {
                    stmt.FinalizeStm();
                }
                return datasets;
            }
        }

        public  DatasetMetadata GetDatasetMetadata(string identityId, string datasetName)
        {
            lock (sqlite_lock)
            {
                return this.GetMetadataInternal(identityId, datasetName);
            }
        }

        public  Record GetRecord(string identityId, string datasetName, string key)
        {
            lock (sqlite_lock)
            {

                Record record = null;
                SQLiteStatement stmt = null;
                try
                {


                    stmt = db.Prepare(RecordColumns.BuildQuery(
                    RecordColumns.IDENTITY_ID + " = @identityId AND " +
                        RecordColumns.DATASET_NAME + " = @datasetName AND " +
                        RecordColumns.KEY + " = @key"
                    ));

                    stmt.BindText(1, identityId);
                    stmt.BindText(2, datasetName);
                    stmt.BindText(3, key);

                    if (stmt.Read())
                    {
                        record = this.SqliteStmtToRecord(stmt);
                    }
                }
                finally
                {
                    stmt.FinalizeStm();
                }
                return record;
            }
        }

        public  List<Record> GetRecords(string identityId, string datasetName)
        {
            lock (sqlite_lock)
            {

                List<Record> records = new List<Record>();

                SQLiteStatement stmt = null;
                try
                {

                    stmt = db.Prepare(
                    RecordColumns.BuildQuery(
                        RecordColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                        RecordColumns.DATASET_NAME + " = @whereDatasetName"
                    ));

                    stmt.BindText(1, identityId);
                    stmt.BindText(2, datasetName);

                    while (stmt.Read())
                    {
                        records.Add(this.SqliteStmtToRecord(stmt));
                    }
                }
                finally
                {
                    stmt.FinalizeStm();
                }
                return records;
            }
        }

        public  void PutRecords(string identityId, string datasetName, List<Record> records)
        {
            lock (sqlite_lock)
            {
                foreach (Record record in records)
                {
                    this.UpdateOrInsertRecord(identityId, datasetName, record);
                }
            }
        }

        public  void DeleteDataset(string identityId, string datasetName)
        {
            lock (sqlite_lock)
            {
                SQLiteStatement stmt = null;
                try
                {

                    stmt = db.Prepare(
                    RecordColumns.BuildDelete(
                        RecordColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                        RecordColumns.DATASET_NAME + " = @whereDatasetName"
                    ));

                    stmt.BindText(1, identityId);
                    stmt.BindText(2, datasetName);
                    stmt.Step();

                    stmt.FinalizeStm();


                    stmt = db.Prepare(
                    DatasetColumns.BuildUpdate(
                        new string[] {
                    DatasetColumns.LAST_MODIFIED_TIMESTAMP,
                    DatasetColumns.LAST_SYNC_COUNT
                },
                        DatasetColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                        DatasetColumns.DATASET_NAME + " = @whereDatasetName"
                    ));
                    stmt.BindDateTime(1, DateTime.Now);
                    stmt.BindInt(2, -1);
                    stmt.BindText(3, identityId);
                    stmt.BindText(4, datasetName);

                    stmt.Step();

                }
                finally
                {
                    stmt.FinalizeStm();
                }

            }
        }

        public  void PurgeDataset(string identityId, string datasetName)
        {
            lock (sqlite_lock)
            {
                this.DeleteDataset(identityId, datasetName);
                SQLiteStatement stmt = null;
                try
                {

                    stmt = db.Prepare(
                    DatasetColumns.BuildDelete(
                        DatasetColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                        DatasetColumns.DATASET_NAME + " = @whereDatasetName"
                    ));

                    stmt.BindText(1, identityId);
                    stmt.BindText(2, datasetName);


                    stmt.Step();

                }
                finally
                {
                    stmt.FinalizeStm();
                }
            }
        }

        public  long GetLastSyncCount(string identityId, string datasetName)
        {
            long lastSyncCount = 0;
            lock (sqlite_lock)
            {
                SQLiteStatement stmt = null;
                try
                {

                    stmt = db.Prepare(
                DatasetColumns.BuildQuery(
                    DatasetColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                        DatasetColumns.DATASET_NAME + " = @whereDatasetName"
                    ));

                    stmt.BindText(1, identityId);
                    stmt.BindText(2, datasetName);



                    if (stmt.Read())
                    {
                        lastSyncCount = stmt.Fields[DatasetColumns.LAST_SYNC_COUNT].INTEGER;
                    }

                }
                finally
                {
                    stmt.FinalizeStm();
                }
                return lastSyncCount;
            }
        }

        public  List<Record> GetModifiedRecords(string identityId, string datasetName)
        {
            lock (sqlite_lock)
            {

                List<Record> records = new List<Record>();

                SQLiteStatement stmt = null;
                try
                {
                    stmt = db.Prepare(
                        RecordColumns.BuildQuery(
                            RecordColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                            RecordColumns.DATASET_NAME + " = @whereDatasetName AND " +
                            RecordColumns.MODIFIED + " = @whereModified"
                    ));

                    stmt.BindText(1, identityId);
                    stmt.BindText(2, datasetName);
                    stmt.BindInt(3, 1);


                    while (stmt.Read())
                    {
                        records.Add(this.SqliteStmtToRecord(stmt));
                    }

                }
                finally
                {
                    stmt.FinalizeStm();
                }
                return records;
            }
        }

        public  void UpdateLastSyncCount(string identityId, string datasetName, long lastSyncCount)
        {
            lock (sqlite_lock)
            {

                SQLiteStatement stmt = null;
                try
                {

                    stmt = db.Prepare(
                DatasetColumns.BuildUpdate(
                    new string[] {
                    DatasetColumns.LAST_SYNC_COUNT,
                    DatasetColumns.LAST_SYNC_TIMESTAMP
                },
                    RecordColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                        RecordColumns.DATASET_NAME + " = @whereDatasetName"
                    ));

                    stmt.BindInt(1, lastSyncCount);
                    stmt.BindDateTime(2, DateTime.Now);
                    stmt.BindText(3, identityId);
                    stmt.BindText(4, datasetName);

                    stmt.Step();
                }
                finally
                {
                    stmt.FinalizeStm();
                }
            }
        }

        public  void WipeData()
        {
            lock (sqlite_lock)
            {
                db.Exec(DatasetColumns.BuildDelete(null));
                db.Exec(RecordColumns.BuildDelete(null));
            }
        }

        HashSet<string> GetCommonDatasetNames(string oldIdentityId, string newIdentityId)
        {
            HashSet<string> newNameSet = new HashSet<string>();
            HashSet<string> oldNameSet = new HashSet<string>();
            if (oldIdentityId != null && newIdentityId != null)
            {
                List<DatasetMetadata> newDatasets = GetDatasetMetadata(newIdentityId);
                List<DatasetMetadata> oldDatasets = GetDatasetMetadata(oldIdentityId);
                foreach (DatasetMetadata oldMetaData in oldDatasets)
                {
                    oldNameSet.Add(oldMetaData.DatasetName);
                }
                foreach (DatasetMetadata newMetaData in newDatasets)
                {
                    newNameSet.Add(newMetaData.DatasetName);
                }
                oldNameSet.IntersectWith(newNameSet);
            }
            return oldNameSet;
        }

        public  void ChangeIdentityId(string oldIdentityId, string newIdentityId)
        {
            Debug.Log("Reparenting datasets from " + oldIdentityId + " to " + newIdentityId);
            GetCommonDatasetNames(oldIdentityId, newIdentityId);
            lock (sqlite_lock)
            {
                SQLiteStatement stmt = null;
                try
                {
                    // if oldIdentityId is unknown, aka the dataset is created prior to
                    // having a cognito id, just reparent datasets from unknown to
                    // newIdentityId
                    if (DatasetUtils.UNKNOWN_IDENTITY_ID == oldIdentityId)
                    {

                        HashSet<string> commonDatasetNames = GetCommonDatasetNames(oldIdentityId, newIdentityId);

                        // append UNKNOWN to the name of all non unique datasets
                        foreach (String oldDatasetName in commonDatasetNames)
                        {

                            stmt = db.Prepare("UPDATE " + TABLE_DATASETS
                                  + " SET " + DatasetColumns.DATASET_NAME + " = ?"
                                  + " WHERE " + DatasetColumns.IDENTITY_ID + " = ?"
                                  + " AND " + DatasetColumns.DATASET_NAME + " = ?"
                            );

                            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                            string timestamp = ((DateTime.UtcNow - epoch).TotalSeconds).ToString();

                            stmt.BindText(1, oldDatasetName + "." + oldIdentityId + "-" + timestamp);
                            stmt.BindText(2, oldIdentityId);
                            stmt.BindText(3, oldDatasetName);

                            stmt.Step();

                            stmt.FinalizeStm();

                            stmt = db.Prepare("UPDATE " + TABLE_RECORDS
                                + " SET " + RecordColumns.DATASET_NAME + " = ?"
                                + " WHERE " + RecordColumns.IDENTITY_ID + " = ?"
                                + " AND " + RecordColumns.DATASET_NAME + " = ?"
                            );

                            stmt.BindText(1, oldDatasetName + "." + oldIdentityId + "-" + timestamp);
                            stmt.BindText(2, oldIdentityId);
                            stmt.BindText(3, oldDatasetName);

                            stmt.Step();

                            stmt.FinalizeStm();
                        }

                        stmt = db.Prepare(
                            DatasetColumns.BuildUpdate(
                                new string[] { DatasetColumns.IDENTITY_ID },
                                DatasetColumns.IDENTITY_ID + " = ?"
                            )
                        );

                        stmt.BindText(1, newIdentityId);
                        stmt.BindText(2, oldIdentityId);

                        stmt.Step();

                        stmt.FinalizeStm();


                        stmt = db.Prepare(
                            RecordColumns.BuildUpdate(
                                new string[] { RecordColumns.IDENTITY_ID },
                                RecordColumns.IDENTITY_ID + " = ?"
                            )
                        );


                        stmt.BindText(1, newIdentityId);
                        stmt.BindText(2, oldIdentityId);

                        stmt.Step();

                    }
                    else
                    {
                        // 1. copy oldIdentityId/dataset to newIdentityId/dataset
                        // datasets table
                        stmt = db.Prepare("INSERT INTO " + TABLE_DATASETS + "("
                            + DatasetColumns.IDENTITY_ID + ","
                            + DatasetColumns.DATASET_NAME + ","
                            + DatasetColumns.CREATION_TIMESTAMP + ","
                            + DatasetColumns.STORAGE_SIZE_BYTES + ","
                            + DatasetColumns.RECORD_COUNT
                            // last sync count is reset to default 0
                            + ")"
                            + " SELECT "
                            + "'" + newIdentityId + "'," // assign new owner
                            + DatasetColumns.DATASET_NAME + ","
                            + DatasetColumns.CREATION_TIMESTAMP + ","
                            + DatasetColumns.STORAGE_SIZE_BYTES + ","
                            + DatasetColumns.RECORD_COUNT
                            + " FROM " + TABLE_DATASETS
                            + " WHERE " + DatasetColumns.IDENTITY_ID + " = ?"
                        );

                        stmt.BindText(1, oldIdentityId);

                        stmt.Step();
                        stmt.FinalizeStm();

                        // records table
                        stmt = db.Prepare("INSERT INTO " + TABLE_RECORDS + "("
                            + RecordColumns.IDENTITY_ID + ","
                            + RecordColumns.DATASET_NAME + ","
                            + RecordColumns.KEY + ","
                            + RecordColumns.VALUE + ","
                            // sync count is resset to default 0
                            + RecordColumns.LAST_MODIFIED_TIMESTAMP + ","
                            + RecordColumns.LAST_MODIFIED_BY + ","
                            + RecordColumns.DEVICE_LAST_MODIFIED_TIMESTAMP
                            // modified is reset to default 1 (dirty)
                            + ")"
                            + " SELECT "
                            + "'" + newIdentityId + "'," // assign new owner
                            + RecordColumns.DATASET_NAME + ","
                            + RecordColumns.KEY + ","
                            + RecordColumns.VALUE + ","
                            + RecordColumns.LAST_MODIFIED_TIMESTAMP + ","
                            + RecordColumns.LAST_MODIFIED_BY + ","
                            + RecordColumns.DEVICE_LAST_MODIFIED_TIMESTAMP
                            + " FROM " + TABLE_RECORDS
                            + " WHERE " + RecordColumns.IDENTITY_ID + " = ?"
                        );

                        stmt.BindText(1, oldIdentityId);

                        stmt.Step();
                        stmt.FinalizeStm();

                        // 2. rename oldIdentityId/dataset to
                        // newIdentityId/dataset.oldIdentityId
                        // datasets table
                        stmt = db.Prepare("UPDATE " + TABLE_DATASETS
                            + " SET "
                            + DatasetColumns.IDENTITY_ID + " = '" + newIdentityId + "', "
                            + DatasetColumns.DATASET_NAME + " = "
                            + DatasetColumns.DATASET_NAME + " || '." + oldIdentityId + "'"
                            + " WHERE " + DatasetColumns.IDENTITY_ID + " = ?"
                        );

                        stmt.BindText(1, oldIdentityId);

                        stmt.Step();
                        stmt.FinalizeStm();

                        // records table
                        stmt = db.Prepare("UPDATE " + TABLE_RECORDS
                            + " SET "
                            + RecordColumns.IDENTITY_ID + " = '" + newIdentityId + "', "
                            + RecordColumns.DATASET_NAME + " = "
                            + RecordColumns.DATASET_NAME + " || '." + oldIdentityId + "'"
                            + " WHERE " + RecordColumns.IDENTITY_ID + " = ?"
                        );

                        stmt.BindText(1, oldIdentityId);

                        stmt.Step();
                    }
                }
                finally
                {
                    stmt.FinalizeStm();
                }
            }

        }

        public  void UpdateDatasetMetadata(string identityId, List<DatasetMetadata> datasetMetadata)
        {
            lock (sqlite_lock)
            {
                foreach (DatasetMetadata metadata in datasetMetadata)
                {
                    if (!this.UpdateDatasetMetadataInternal(identityId, metadata))
                    {
                        // TODO: Log error while updating metadata
                    }
                }
            }
        }

        public void UpdateLastModifiedTimestamp(string identityId, string datasetName)
        {
            lock (sqlite_lock)
            {
                SQLiteStatement stmt = null;
                try
                {
                    stmt = db.Prepare(
                        DatasetColumns.BuildUpdate(
                        new string[] { DatasetColumns.LAST_MODIFIED_TIMESTAMP },
                        DatasetColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                        DatasetColumns.DATASET_NAME + " = @whereDatasetName"
                    ));

                    stmt.BindDateTime(1, DateTime.Now);
                    stmt.BindText(2, identityId);
                    stmt.BindText(3, datasetName);
                    stmt.Step();
                }
                finally
                {
                    stmt.FinalizeStm();
                }

            }
        }

        private bool UpdateDatasetMetadataInternal(string identityId, DatasetMetadata metadata)
        {
            lock (sqlite_lock)
            {
                DatasetMetadata local = this.GetMetadataInternal(identityId, metadata.DatasetName);
                SQLiteStatement stmt = null;
                try
                {

                    if (local == null)
                    {
                        stmt = db.Prepare(DatasetColumns.BuildInsert());
                        stmt.BindText(1, identityId);
                        stmt.BindText(2, metadata.DatasetName);
                        stmt.BindDateTime(3, metadata.CreationDate);
                        stmt.BindDateTime(4, metadata.LastModifiedDate);
                        stmt.BindInt(5, metadata.RecordCount);
                        stmt.BindInt(6, metadata.StorageSizeBytes);
                        stmt.BindInt(7, 0);
                        stmt.BindInt(8, 0);
                        stmt.BindText(9, null);
                        stmt.Step();
                    }
                    else
                    {
                        stmt = db.Prepare(
                        DatasetColumns.BuildUpdate(
                            new string[] {
                                DatasetColumns.DATASET_NAME,
                                DatasetColumns.CREATION_TIMESTAMP,
                                DatasetColumns.LAST_MODIFIED_TIMESTAMP,
                                DatasetColumns.LAST_MODIFIED_BY,
                                DatasetColumns.RECORD_COUNT,
                                DatasetColumns.STORAGE_SIZE_BYTES
                            },
                            DatasetColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                            DatasetColumns.DATASET_NAME + " = @whereDatasetName"
                        ));

                        stmt.BindText(1, metadata.DatasetName);
                        stmt.BindDateTime(2, metadata.CreationDate);
                        stmt.BindDateTime(3, metadata.LastModifiedDate);
                        stmt.BindText(4, metadata.LastModifiedBy);

                        stmt.BindInt(5, metadata.RecordCount);
                        stmt.BindInt(6, metadata.StorageSizeBytes);

                        stmt.BindText(7, identityId);
                        stmt.BindText(8, metadata.DatasetName);
                        stmt.Step();
                    }
                    return true;
                }
                finally
                {
                    stmt.FinalizeStm();
                }
            }
        }

        private void UpdateOrInsertRecord(string identityId, string datasetName, Record record)
        {
            lock (sqlite_lock)
            {
                SQLiteStatement stmt = null;
                try
                {
                    stmt = db.Prepare(
                    RecordColumns.BuildQuery(
                        RecordColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                        RecordColumns.DATASET_NAME + " = @whereDatasetName AND " +
                        RecordColumns.KEY + " = @whereKey "
                    ));

                    stmt.BindText(1, identityId);
                    stmt.BindText(2, datasetName);
                    stmt.BindText(3, record.Key);
                    bool recordsFound = false;

                    while (stmt.Read())
                    {
                        recordsFound = true;
                    }
                    stmt.FinalizeStm();

                    if (recordsFound)
                    {
                        stmt = db.Prepare(
                        RecordColumns.BuildUpdate(
                            new string[] {
                            RecordColumns.VALUE,
                            RecordColumns.SYNC_COUNT,
                            RecordColumns.MODIFIED,
                            RecordColumns.LAST_MODIFIED_TIMESTAMP,
                            RecordColumns.LAST_MODIFIED_BY,
                            RecordColumns.DEVICE_LAST_MODIFIED_TIMESTAMP
                        },
                        RecordColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                            RecordColumns.DATASET_NAME + " = @whereDatasetName AND " +
                            RecordColumns.KEY + " = @whereKey "
                        ));
                        stmt.BindText(1, record.Value);
                        stmt.BindInt(2, record.SyncCount);
                        stmt.BindInt(3, record.IsModified ? 1 : 0);
                        stmt.BindDateTime(4, record.LastModifiedDate);
                        stmt.BindText(5, record.LastModifiedBy);
                        stmt.BindDateTime(6, record.DeviceLastModifiedDate);
                        stmt.BindText(7, identityId);
                        stmt.BindText(8, datasetName);
                        stmt.BindText(9, record.Key);
                        stmt.Step();
                    }
                    else
                    {
                        stmt = db.Prepare(RecordColumns.BuildInsert());
                        stmt.BindText(1, identityId);
                        stmt.BindText(2, datasetName);
                        stmt.BindText(3, record.Key);
                        stmt.BindText(4, record.Value);
                        stmt.BindInt(5, record.SyncCount);
                        stmt.BindDateTime(6, record.LastModifiedDate);
                        stmt.BindText(7, record.LastModifiedBy);
                        stmt.BindDateTime(8, record.DeviceLastModifiedDate);
                        stmt.BindInt(9, record.IsModified ? 1 : 0);
                        stmt.Step();
                    }
                }
                finally
                {
                    stmt.FinalizeStm();
                }
            }
        }

        private Record SqliteStmtToRecord(SQLiteStatement stmt)
        {
            return new Record(stmt.Fields[RecordColumns.KEY].TEXT, stmt.Fields[RecordColumns.VALUE].TEXT,
                               stmt.Fields[RecordColumns.SYNC_COUNT].INTEGER, stmt.Fields[RecordColumns.LAST_MODIFIED_TIMESTAMP].DATETIME,
                               stmt.Fields[RecordColumns.LAST_MODIFIED_BY].TEXT, stmt.Fields[RecordColumns.DEVICE_LAST_MODIFIED_TIMESTAMP].DATETIME,
                               (stmt.Fields[RecordColumns.MODIFIED].INTEGER == 1));
        }

        private bool PutValueInternal(string identityId, string datasetName, string key, string value)
        {
            lock (sqlite_lock)
            {
                Record record = this.GetRecord(identityId, datasetName, key);

                if (record != null && string.Equals(record.Value, value))
                {
                    return true;
                }
                SQLiteStatement stmt = null;
                try
                {
                    if (record == null)
                    {
                        stmt = db.Prepare(RecordColumns.BuildInsert());
                        stmt.BindText(1, identityId);
                        stmt.BindText(2, datasetName);
                        stmt.BindText(3, key);
                        stmt.BindText(4, value);
                        stmt.BindInt(5, 0);
                        stmt.BindDateTime(6, DateTime.Now);
                        stmt.BindText(7, "");
                        stmt.BindInt(8, 0);
                        stmt.BindInt(9, 1);
                        stmt.Step();
                        return true;
                    }
                    else
                    {
                        stmt = db.Prepare(
                        RecordColumns.BuildUpdate(
                            new string[] { 
                                RecordColumns.IDENTITY_ID, RecordColumns.DATASET_NAME, RecordColumns.KEY, 
                                RecordColumns.VALUE, RecordColumns.MODIFIED, RecordColumns.SYNC_COUNT, 
                                RecordColumns.DEVICE_LAST_MODIFIED_TIMESTAMP 
                            },
                            RecordColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                            RecordColumns.DATASET_NAME + " = @whereDatasetName AND " +
                            RecordColumns.KEY + " = @whereKey "
                        ));

                        stmt.BindText(1, identityId);
                        stmt.BindText(2, datasetName);
                        stmt.BindText(3, key);
                        stmt.BindText(4, value);
                        stmt.BindInt(5, 1);
                        stmt.BindInt(6, record.SyncCount);
                        stmt.BindDateTime(7, DateTime.Now);
                        stmt.BindText(8, identityId);
                        stmt.BindText(9, datasetName);
                        stmt.BindText(10, key);
                        stmt.Step();
                        return true;
                    }
                }
                finally
                {
                    stmt.FinalizeStm();
                }
            }
        }

        private DatasetMetadata GetMetadataInternal(string identityId, string datasetName)
        {
            lock (sqlite_lock)
            {
                SQLiteStatement stmt = null;
                try
                {
                    stmt = db.Prepare(DatasetColumns.BuildQuery(
                    DatasetColumns.IDENTITY_ID + " = '" + identityId + "' AND " +
                        DatasetColumns.DATASET_NAME + " = '" + datasetName + "'"
                    ));
                    DatasetMetadata metadata = null;
                    while (stmt.Read())
                    {
                        metadata = this.SqliteStmtToDatasetMetadata(stmt);
                    }
                    return metadata;
                }
                finally
                {
                    stmt.FinalizeStm();
                }
            }
        }


        private DatasetMetadata SqliteStmtToDatasetMetadata(SQLiteStatement stmt)
        {
            return new DatasetMetadata(
                stmt.Fields[DatasetColumns.DATASET_NAME].TEXT,
                stmt.Fields[DatasetColumns.CREATION_TIMESTAMP].DATETIME,
                stmt.Fields[DatasetColumns.LAST_MODIFIED_TIMESTAMP].DATETIME,
                stmt.Fields[DatasetColumns.LAST_MODIFIED_BY].TEXT,
                stmt.Fields[DatasetColumns.STORAGE_SIZE_BYTES].INTEGER,
                stmt.Fields[DatasetColumns.RECORD_COUNT].INTEGER
            );
        }

        private SQLiteDatabase db = null;
        private void SetupDatabase()
        {
            lock (sqlite_lock)
            {

                SQLiteStatement stmt = null;
                try
                {

                    db = new SQLiteDatabase(this.dataPath);

                    string query = "SELECT count(*) as count FROM sqlite_master WHERE type='table' AND name='" + TABLE_DATASETS + "'";
                    stmt = db.Prepare(query);


                    if (stmt.Read() && stmt.Fields["count"].INTEGER == 0)
                    {
                        _logger.InfoFormat("{0}", @"Cognito Sync - SQLiteStorage - running create dataset");
                        db.Exec(
                    "CREATE TABLE " + TABLE_DATASETS + "("
                            + DatasetColumns.IDENTITY_ID + " TEXT NOT NULL,"
                            + DatasetColumns.DATASET_NAME + " TEXT NOT NULL,"
                            + DatasetColumns.CREATION_TIMESTAMP + " TEXT DEFAULT '0',"
                            + DatasetColumns.LAST_MODIFIED_TIMESTAMP + " TEXT DEFAULT '0',"
                            + DatasetColumns.LAST_MODIFIED_BY + " TEXT,"
                            + DatasetColumns.STORAGE_SIZE_BYTES + " INTEGER DEFAULT 0,"
                            + DatasetColumns.RECORD_COUNT + " INTEGER DEFAULT 0,"
                            + DatasetColumns.LAST_SYNC_COUNT + " INTEGER NOT NULL DEFAULT 0,"
                            + DatasetColumns.LAST_SYNC_TIMESTAMP + " INTEGER DEFAULT '0',"
                            + DatasetColumns.LAST_SYNC_RESULT + " TEXT,"
                            + "UNIQUE (" + DatasetColumns.IDENTITY_ID + ", "
                            + DatasetColumns.DATASET_NAME + ")"
                            + ")");
                    }

                    stmt.FinalizeStm();
                    query = "SELECT count(*) as count FROM sqlite_master WHERE type='table' AND name='" + TABLE_RECORDS + "'";

                    stmt = db.Prepare(query);


                    if (stmt.Read() && stmt.Fields["count"].INTEGER == 0)
                    {
                        _logger.InfoFormat("{0}", @"Cognito Sync - SQLiteStorage - running create dataset");
                        db.Exec(
                        "CREATE TABLE " + TABLE_RECORDS + "("
                            + RecordColumns.IDENTITY_ID + " TEXT NOT NULL,"
                            + RecordColumns.DATASET_NAME + " TEXT NOT NULL,"
                            + RecordColumns.KEY + " TEXT NOT NULL,"
                            + RecordColumns.VALUE + " TEXT,"
                            + RecordColumns.SYNC_COUNT + " INTEGER NOT NULL DEFAULT 0,"
                            + RecordColumns.LAST_MODIFIED_TIMESTAMP + " TEXT DEFAULT '0',"
                            + RecordColumns.LAST_MODIFIED_BY + " TEXT,"
                            + RecordColumns.DEVICE_LAST_MODIFIED_TIMESTAMP + " TEXT DEFAULT '0',"
                            + RecordColumns.MODIFIED + " INTEGER NOT NULL DEFAULT 1,"
                            + "UNIQUE (" + RecordColumns.IDENTITY_ID + ", " + RecordColumns.DATASET_NAME
                            + ", " + RecordColumns.KEY + ")"
                            + ")");
                    }

                }
                finally
                {
                    if (stmt != null)
                        stmt.FinalizeStm();
                }
                _logger.InfoFormat("{0}", @"Cognito Sync - SQLiteStorage - completed setupdatabase");
            }
        }

        public void ConditionallyPutRecords(String identityId, String datasetName, List<Record> records,
                List<Record> localRecords)
        {
            /*
             * Grab an instance of the record from the local store with the remote change's 
             * key and the snapshot version.
             * 1) If both are null the remote change is new and we should save. 
             * 2) If both exist but the values and sync counts have changed, 
             *    it has changed locally and we shouldn't overwrite with the remote changes, 
             *    which will still exist in remote. 
             * 3) If both exist and the values have not changed, we should save the remote change.
             * 4) If the current check exists but it wasn't in the snapshot, we should save.
             */
            Dictionary<string, Record> localRecordMap = new Dictionary<string, Record>();
            foreach (Record record in localRecords)
            {
                localRecordMap[record.Key] = record;
            }
            foreach (Record record in records)
            {
                Record databaseRecord = this.GetRecord(identityId, datasetName, record.Key);
                Record oldDatabaseRecord = localRecordMap[record.Key];
                if (databaseRecord != null && oldDatabaseRecord != null
                        && (!StringUtils.Equals(databaseRecord.Value, oldDatabaseRecord.Value)
                        || databaseRecord.SyncCount != oldDatabaseRecord.SyncCount
                        || !StringUtils.Equals(databaseRecord.LastModifiedBy, oldDatabaseRecord.LastModifiedBy)))
                {
                    continue;
                }
                UpdateOrInsertRecord(identityId, datasetName, record);
            }

        }

    }
}
