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
using System.Runtime.InteropServices;

using Amazon.Common;
using Amazon.Runtime;
using Amazon.CognitoSync.SyncManager;
using Amazon.CognitoSync.SyncManager.Util;

using System.Text;
using UnityEngine;

using Amazon.Unity.Storage;

namespace Amazon.CognitoSync.SyncManager.Storage
{
    public class SQLiteLocalStorage : LocalStorage
    {

        internal static readonly string TABLE_DATASETS = "datasets";
        internal static readonly string TABLE_RECORDS = "records";
        internal readonly string dataPath;


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

        public override void CreateDataset(string identityId, string datasetName)
        {
            lock (SQLiteDatabase.SQLiteLock)
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

        public override string GetValue(string identityId, string datasetName, string key)
        {
            lock (SQLiteDatabase.SQLiteLock)
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

        public override void PutValue(string identityId, string datasetName, string key, string value)
        {
            lock (SQLiteDatabase.SQLiteLock)
            {
                bool result = this.PutValueInternal(identityId, datasetName, key, value);

                if (!result)
                {
                    AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Errors, "SQLite", "PutValue failed");

                }
                else
                {
                    this.UpdateLastModifiedTimestamp(identityId, datasetName);
                }
            }
        }

        public override Dictionary<string, string> GetValueMap(string identityId, string datasetName)
        {
            lock (SQLiteDatabase.SQLiteLock)
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                List<Record> records = this.GetRecords(identityId, datasetName);

                foreach (Record record in records)
                {
                    if (!record.IsDeleted())
                    {
                        values.Add(record.Key, record.Value);
                    }
                }

                return values;
            }
        }

        public override void PutAllValues(string identityId, string datasetName, Dictionary<string, string> values)
        {
            lock (SQLiteDatabase.SQLiteLock)
            {
                foreach (KeyValuePair<string, string> entry in values)
                {
                    this.PutValueInternal(identityId, datasetName, entry.Key, entry.Value);
                }
                this.UpdateLastModifiedTimestamp(identityId, datasetName);
            }
        }

        public override List<DatasetMetadata> GetDatasets(string identityId)
        {
            lock (SQLiteDatabase.SQLiteLock)
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

        public override DatasetMetadata GetDatasetMetadata(string identityId, string datasetName)
        {
            lock (SQLiteDatabase.SQLiteLock)
            {
                return this.GetMetadataInternal(identityId, datasetName);
            }
        }

        public override Record GetRecord(string identityId, string datasetName, string key)
        {
            lock (SQLiteDatabase.SQLiteLock)
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

        public override List<Record> GetRecords(string identityId, string datasetName)
        {
            lock (SQLiteDatabase.SQLiteLock)
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

        public override void PutRecords(string identityId, string datasetName, List<Record> records)
        {
            lock (SQLiteDatabase.SQLiteLock)
            {
                foreach (Record record in records)
                {
                    this.UpdateAndClearRecord(identityId, datasetName, record);
                }
            }
        }

        public override void DeleteDataset(string identityId, string datasetName)
        {
            lock (SQLiteDatabase.SQLiteLock)
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

        public override void PurgeDataset(string identityId, string datasetName)
        {
            lock (SQLiteDatabase.SQLiteLock)
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

        public override long GetLastSyncCount(string identityId, string datasetName)
        {
            long lastSyncCount = 0;
            lock (SQLiteDatabase.SQLiteLock)
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

        public override List<Record> GetModifiedRecords(string identityId, string datasetName)
        {
            lock (SQLiteDatabase.SQLiteLock)
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

        public override void UpdateLastSyncCount(string identityId, string datasetName, long lastSyncCount)
        {
            lock (SQLiteDatabase.SQLiteLock)
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

        public override void WipeData()
        {
            lock (SQLiteDatabase.SQLiteLock)
            {
                db.Exec(DatasetColumns.BuildDelete(null));
                db.Exec(RecordColumns.BuildDelete(null));
            }
        }

        public override void ChangeIdentityId(string oldIdentityId, string newIdentityId)
        {
            lock (SQLiteDatabase.SQLiteLock)
            {
                SQLiteStatement stmt = null;
                try
                {

                    stmt = db.Prepare(
                    DatasetColumns.BuildUpdate(
                    new string[] { DatasetColumns.IDENTITY_ID },
                DatasetColumns.IDENTITY_ID + " = @whereIdentityId"
                    ));

                    stmt.BindText(1, newIdentityId);
                    stmt.BindText(2, oldIdentityId);


                    stmt.Step();

                    stmt.FinalizeStm();


                    stmt = db.Prepare(
                RecordColumns.BuildUpdate(
                    new string[] { RecordColumns.IDENTITY_ID },
                    RecordColumns.IDENTITY_ID + " = @whereIdentityId"
                    ));


                    stmt.BindText(1, newIdentityId);
                    stmt.BindText(2, oldIdentityId);


                    stmt.Step();
                }
                finally
                {
                    stmt.FinalizeStm();
                }
            }
        }

        public override void UpdateDatasetMetadata(string identityId, List<DatasetMetadata> datasetMetadata)
        {
            lock (SQLiteDatabase.SQLiteLock)
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
            lock (SQLiteDatabase.SQLiteLock)
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

            lock (SQLiteDatabase.SQLiteLock)
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

                        //LAST_SYNC_COUNT, LAST_SYNC_TIMESTAMP, LAST_SYNC_RESULT,

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

        private void UpdateAndClearRecord(string identityId, string datasetName, Record record)
        {
            lock (SQLiteDatabase.SQLiteLock)
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
                        RecordColumns.MODIFIED
                    },
                        RecordColumns.IDENTITY_ID + " = @whereIdentityId AND " +
                            RecordColumns.DATASET_NAME + " = @whereDatasetName AND " +
                            RecordColumns.KEY + " = @whereKey "
                        ));
                        stmt.BindText(1, record.Value);
                        stmt.BindInt(2, record.SyncCount);
                        stmt.BindInt(3, record.Modified ? 1 : 0);
                        stmt.BindText(4, identityId);
                        stmt.BindText(5, datasetName);
                        stmt.BindText(6, record.Key);

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
                        stmt.BindInt(9, record.Modified? 1 : 0);

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

            lock (SQLiteDatabase.SQLiteLock)
            {
                Record record = this.GetRecord(identityId, datasetName, key);

                if (record != null && StringUtils.Equals(record.Value, value))
                {
                    return true;
                }
                SQLiteStatement stmt = null;
                try
                {


                    if (record == null)
                    {
                        stmt = db.Prepare(RecordColumns.BuildInsert());
                        //IDENTITY_ID1, DATASET_NAME2, KEY3, VALUE 4 , SYNC_COUNT 5 , LAST_MODIFIED_TIMESTAMP 6,
                        //LAST_MODIFIED_BY 7 , DEVICE_LAST_MODIFIED_TIMESTAMP 8 , MODIFIED 9

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
            lock (SQLiteDatabase.SQLiteLock)
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
            /*  Column Order
                 * IDENTITY_ID, DATASET_NAME,
                    CREATION_TIMESTAMP, LAST_MODIFIED_TIMESTAMP, LAST_MODIFIED_BY,
                    STORAGE_SIZE_BYTES, RECORD_COUNT,
                    LAST_SYNC_COUNT, LAST_SYNC_TIMESTAMP, LAST_SYNC_RESULT,
                 */

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
            lock (SQLiteDatabase.SQLiteLock)
            {

                SQLiteStatement stmt = null;
                try
                {

                    db = new SQLiteDatabase(this.dataPath);

                    string query = "SELECT count(*) as count FROM sqlite_master WHERE type='table' AND name='" + TABLE_DATASETS + "'";
                    //sqliteStm stmHandle = db.
                    stmt = db.Prepare(query);


                    if (stmt.Read() && stmt.Fields["count"].INTEGER == 0)
                    {
                        AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "SQLiteLocalStorage", "running create dataset");
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
                        AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "SQLiteLocalStorage", "running create records");
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
                AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose, "SQLiteLocalStorage", "completed setupdatabase");
            }
        }
    }
}

