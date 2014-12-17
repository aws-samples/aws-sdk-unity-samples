/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
 */
using System;
using Amazon.Unity;
using Amazon.Common;

namespace Amazon.Unity.Storage
{
    /// <summary>
    /// This classes exposes methods used by CachingCognitoAWSCredentials to cache the 
    /// retrieved IdentityId and temporary STS credentials using SQLiteStorage
    /// </summary>
    public class SQLiteKVStore : KVStore
    {
        private static string TABLE_RECORDS = "kv_records";
        private static string COLUMN_KEY = "col_keys";
        private static string COLUMN_VALUE = "col_values";

        static SQLiteKVStore()
        {
            SetupDatabase("aws_cognito_cache.db");
        }


        private static SQLiteDatabase db = null;

        private static void SetupDatabase(string dataPath)
        {
            lock (SQLiteDatabase.SQLiteLock)
            {

                SQLiteStatement stmt = null;
                try
                {

                    db = new SQLiteDatabase(System.IO.Path.Combine(AmazonInitializer.persistentDataPath, dataPath));

                    string query = "SELECT count(*) as count FROM sqlite_master WHERE type='table' AND name='" + TABLE_RECORDS + "'";

                    stmt = db.Prepare(query);

                    if (stmt.Read() && stmt.Fields["count"].INTEGER == 0)
                    {
                        query = "CREATE TABLE " + TABLE_RECORDS + " ("
                                + COLUMN_KEY + " TEXT NOT NULL,"
                                + COLUMN_VALUE + " TEXT NOT NULL,"
                                + "UNIQUE (" + COLUMN_KEY + ")"
                                + ")";
                        db.Exec(query);
                    }
                }
                finally
                {
                    if (stmt != null)
                        stmt.FinalizeStm();
                }
            }
        }

        public override void Clear(string key)
        {
            lock (SQLiteDatabase.SQLiteLock)
            {
                string query = "DELETE FROM " + TABLE_RECORDS + " where " + COLUMN_KEY + " = ?";
                SQLiteStatement stmt = null;
                try
                {
                    stmt = db.Prepare(query);
                    stmt.BindText(1, key);
                    stmt.Step();
                    stmt.FinalizeStm();
                }
                finally
                {
                    if (stmt != null)
                        stmt.FinalizeStm();
                }
            }
        }

        public override void Put(string key, string value)
        {
            lock (SQLiteDatabase.SQLiteLock)
            {
                string query = "SELECT count(*) as count FROM " + TABLE_RECORDS + " where " + COLUMN_KEY + " = ?";
                SQLiteStatement stmt = null;
                try
                {
                    stmt = db.Prepare(query);
                    stmt.BindText(1, key);
                    if (stmt.Read())
                    {
                        long count = stmt.Fields["count"].INTEGER;
                        stmt.FinalizeStm();

                        if (count == 0)
                        {
                            // insert record
                            query = "INSERT INTO " + TABLE_RECORDS + " (" + COLUMN_KEY + ", " + COLUMN_VALUE + ") VALUES(?,?)";
                            stmt = db.Prepare(query);
                            stmt.BindText(1, key);
                            stmt.BindText(2, value);
                            stmt.Step();
                            return;
                        }
                        else
                        {
                            // update record
                            query = "UPDATE " + TABLE_RECORDS + " SET " + COLUMN_VALUE + " = ? where " + COLUMN_KEY + " = ?";
                            stmt = db.Prepare(query);
                            stmt.BindText(2, key);
                            stmt.BindText(1, value);
                            stmt.Step();
                            return;
                        }
                    }
                    else
                    {
                        AmazonLogging.LogError(AmazonLogging.AmazonLoggingLevel.Errors, "SQLiteKVStore", "Save failed");
                        throw new Exception("Save failed");
                    }
                }
                finally
                {
                    if (stmt != null)
                        stmt.FinalizeStm();
                }
            }
        }


        public override string Get(string key)
        {
            lock (SQLiteDatabase.SQLiteLock)
            {
                string query = "SELECT * FROM " + TABLE_RECORDS + " where " + COLUMN_KEY + " = ?";
                SQLiteStatement stmt = null;
                try
                {
                    stmt = db.Prepare(query);
                    stmt.BindText(1, key);
                    if (stmt.Read())
                    {
                        return stmt.Fields[COLUMN_VALUE].TEXT;
                    }
                    else
                    {
                        return null;
                    }
                }
                finally
                {
                    if (stmt != null)
                        stmt.FinalizeStm();
                }
            }
        }
    }
}

