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
using System.IO;

using UnityEngine;

using Amazon.Util.Storage;
using Amazon.MobileAnalytics.Model;

using ThirdParty.Json.LitJson;
using Amazon.MobileAnalytics.MobileAnalyticsManager;
using Amazon.Util.Storage.Internal;
using Amazon.Util;
using Amazon.Runtime.Internal.Util;
using Amazon.Util.Internal;

namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    internal class SQLiteEventStore:IEventStore
    {
        private static Logger _logger = Logger.GetLogger(typeof(SQLiteEventStore));
        private const String TABLE_NAME = "ma_events";
        private const String EVENT_COLUMN_NAME = "ma_event";
        private const String EVENT_ID_COLUMN_NAME = "ma_event_id";
        private const String EVENT_DELIVERY_ATTEMPT_COUNT_COLUMN_NAME = "ma_delivery_attempt_count";
        private const String MA_APP_ID_COLUMN_NAME = "ma_app_id";
        private const String TABLE_ROWID = "ROWID";
        private const String DB_SIZE_KEY = "MAX_DB_SIZE";
        private const String DB_WARNING_THRESHOLD_KEY  ="DB_WARNING_THRESHOLD";
        private const String dbPath = "mobile_analytic_event.db";
        
        private const int MAX_ALLOWED_SELECTS = 200;
        
        private readonly long _maxDbSize;
        private readonly double _dbWarningThreshold;
        
        //private static SQLiteDatabase db;
        private static object _lock = new object();
        private static SQLiteDatabase db;

        public SQLiteEventStore(long maxDbSize,double dbWarningThreshold){
            this._maxDbSize = maxDbSize;
            this._dbWarningThreshold = dbWarningThreshold;
        }
        
        /// <summary>
        /// Initializes the <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.SQLiteEventStore"/> class.
        /// </summary>
        static SQLiteEventStore ()
        {
            SetUpDatabase(dbPath);
        }
        
        /// <summary>
        /// Sets up database.
        /// </summary>
        /// <param name="dbPath">Db path.</param>
        private static void SetUpDatabase(String dbPath)
        {
            lock (_lock)
            {
                SQLiteStatement stmt = null;
                try
                {
                    db = new SQLiteDatabase(System.IO.Path.Combine(AmazonHookedPlatformInfo.Instance.PersistentDataPath, dbPath));
                    
                    //turn on auto vacuuming so that when events are deleted, then we can recover the table space.
                    string query = "PRAGMA auto_vacuum = 1";
                    db.Exec(query);
                    
                    query = "SELECT count(*) as count FROM sqlite_master WHERE type='table' AND name='" + TABLE_NAME + "'";
                    
                    stmt = db.Prepare(query);
                    if (stmt.Read() && stmt.Fields["count"].INTEGER == 0)
                    {
                        query = "CREATE TABLE " + TABLE_NAME + " ("
                                + EVENT_COLUMN_NAME + " TEXT NOT NULL,"+EVENT_ID_COLUMN_NAME + " TEXT NOT NULL UNIQUE,"
                                + MA_APP_ID_COLUMN_NAME + " TEXT NOT NULL," 
                                + EVENT_DELIVERY_ATTEMPT_COUNT_COLUMN_NAME + " INTEGER NOT NULL DEFAULT 0)";
                        db.Exec(query);
                    }
                }
                catch(Exception e)
                {
                    _logger.Error(e,"");
                }
                finally
                {
                    if (stmt != null)
                        stmt.FinalizeStm();
                }
            }
        }
        
        /// <summary>
        /// Add an event to the store.
        /// </summary>
        /// <returns><c>true</c>, if event was put, <c>false</c> otherwise.</returns>
        public bool PutEvent(string eventString, string appId)
        {
            bool success = false;
            bool proceedToInsert = false;
            long currentDatabaseSize = GetDatabaseSize();
            
            if(string.IsNullOrEmpty(appId))
                throw new ArgumentNullException("AppId");
            
            if(currentDatabaseSize>=_maxDbSize)
            {
                proceedToInsert = false;

                InvalidOperationException e = new InvalidOperationException();
                _logger.Error(e, "The database size has exceeded the threshold limit. Unable to insert any new events");
            }
            else if(currentDatabaseSize/_maxDbSize >= _dbWarningThreshold)
            {
                proceedToInsert = true;
                _logger.InfoFormat("The database size is almost full");
            }
            else
            {
                proceedToInsert = true;
            }
            
            
            //keep the lock as short as possible
            if(proceedToInsert)
            {
                lock (_lock)
                {
                    SQLiteStatement stmt = null;
                    try
                    {
#if SQL_DEBUG
                        DateTime _dbExecutionStartTime = DateTime.Now;
#endif
                        string query = "INSERT INTO " + TABLE_NAME + " (" + EVENT_COLUMN_NAME + "," + EVENT_ID_COLUMN_NAME + "," + MA_APP_ID_COLUMN_NAME + ") values(?,?,?)";
                        stmt = db.Prepare(query);
                        stmt.BindText(1, eventString);
                        stmt.BindText(2, Guid.NewGuid().ToString());
                        stmt.BindText(3, appId);
                        stmt.Step();
                        success =  true;
#if SQL_DEBUG
                        DateTime _dbExecutionEndTime = DateTime.Now;
                        double totalSeconds = _dbExecutionEndTime.Subtract(_dbExecutionStartTime).TotalSeconds;
                        AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose,"SQLiteEventStore","Put Operation completed on local store in " + totalSeconds + " seconds");
#endif
                    }
                    finally
                    {
                        if(stmt != null)
                        stmt.FinalizeStm();
                    }
                }
            }
            
            return success;
        }
        
        /// <summary>
        /// Deletes a list of events.
        /// </summary>
        /// <returns><c>true</c>, if events was deleted, <c>false</c> otherwise.</returns>
        /// <param name="rowIds">Row identifiers.</param>
        public bool DeleteEvent(List<string> rowIds)
        {
            bool success = false;
            lock (_lock)
            {
                SQLiteStatement stmt = null;
                try
                {
#if SQL_DEBUG
                    DateTime _dbExecutionStartTime = DateTime.Now;
#endif
                    string ids = "'" + String.Join("', '",rowIds.ToArray()) +"'";
                    string query = String.Format("DELETE FROM " + TABLE_NAME + " WHERE " +  EVENT_ID_COLUMN_NAME +" IN ({0})",ids);
                    stmt = db.Prepare(query);
                    stmt.Step();
                    success = true;
#if SQL_DEBUG
                    DateTime _dbExecutionEndTime = DateTime.Now;
                    double totalSeconds = _dbExecutionEndTime.Subtract(_dbExecutionStartTime).TotalSeconds;
                    AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose,"SQLiteEventStore","Delete Operation completed on local store in " + totalSeconds + " seconds");
#endif
                }
                finally
                {
                    if(stmt != null)
                        stmt.FinalizeStm();
                }
            }
            return success;
        }
        
        /// <summary>
        /// Get All event from the Event Store
        /// </summary>
        /// <param name="appid">Appid.</param>
        /// <returns>All the events as a List of <see cref="ThirdParty.Json.LitJson.JsonData"/>.</returns>
        public List<JsonData> GetAllEvents(string appId)
        {
            List<JsonData> eventList = new List<JsonData>();
            lock (_lock)
            {
                SQLiteStatement stmt = null;
                try
                {
#if SQL_DEBUG
                    DateTime _dbExecutionStartTime = DateTime.Now;
#endif
                    string query = "SELECT * FROM " + TABLE_NAME + " WHERE " + MA_APP_ID_COLUMN_NAME + " = ?  ORDER BY " + EVENT_DELIVERY_ATTEMPT_COUNT_COLUMN_NAME + ",ROWID LIMIT " + MAX_ALLOWED_SELECTS;
                    stmt = db.Prepare(query);
                    stmt.BindText(1, appId);
                    while(stmt.Read()){
                        JsonData data = new JsonData();
                        data["id"] = stmt.Fields[EVENT_ID_COLUMN_NAME].TEXT;
                        data["event"] = stmt.Fields[EVENT_COLUMN_NAME.ToLower()].TEXT;
                        data["appId"] = stmt.Fields[MA_APP_ID_COLUMN_NAME].TEXT;
                        eventList.Add(data);
                    }
#if SQL_DEBUG
                    DateTime _dbExecutionEndTime = DateTime.Now;
                    double totalSeconds = _dbExecutionEndTime.Subtract(_dbExecutionStartTime).TotalSeconds;
                    AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose,"SQLiteEventStore","Select All Operation completed from local store in " + totalSeconds + " seconds");
#endif
                }
                catch(Exception e)
                {
                    _logger.Error(e, "Exception happens when getting events.");
                }
                finally
                {
                    if(stmt !=null)
                        stmt.FinalizeStm();
                }
            }
            
            return eventList;
        }
        
        /// <summary>
        /// Gets Numbers the of events.
        /// </summary>
        /// <returns>The number of events.</returns>
        public long NumberOfEvents(string appid)
        {
            long count = 0;
            lock (_lock)
            {
                SQLiteStatement stmt = null;
                try
                {
                    string query = "SELECT COUNT(*) C FROM " + TABLE_NAME + " where " + MA_APP_ID_COLUMN_NAME +  " = ?";
                    stmt = db.Prepare(query);
                    stmt.BindText(1,appid);
                    while(stmt.Read()){
                        count = stmt.Fields["C"].INTEGER;
                    }
                }
                finally
                {
                    if(stmt!=null)
                    stmt.FinalizeStm();
                }
                
            }
            
            return count;
        }
        
        /// <summary>
        /// Increments the delivery attempt.
        /// </summary>
        /// <returns>true</returns>
        /// <c>false</c>
        /// <param name="rowIds">Row identifiers.</param>
        public bool IncrementDeliveryAttempt(List<string> rowIds)
        {
            bool success = false;
            lock (_lock)
            {
                SQLiteStatement stmt = null;
                try
                {
#if SQL_DEBUG
                    DateTime _dbExecutionStartTime = DateTime.Now;
#endif
                    string ids = "'" + String.Join("', '",rowIds.ToArray()) +"'";
                    string query = String.Format("UPDATE " + TABLE_NAME + " SET " + EVENT_DELIVERY_ATTEMPT_COUNT_COLUMN_NAME + "= " +EVENT_DELIVERY_ATTEMPT_COUNT_COLUMN_NAME + "+1 WHERE " + EVENT_ID_COLUMN_NAME +" IN ({0})",ids);
                    stmt = db.Prepare(query);
                    stmt.Step();
                    success = true;
#if SQL_DEBUG
                    DateTime _dbExecutionEndTime = DateTime.Now;
                    double totalSeconds = _dbExecutionEndTime.Subtract(_dbExecutionStartTime).TotalSeconds;
                    AmazonLogging.Log(AmazonLogging.AmazonLoggingLevel.Verbose,"SQLiteEventStore","Increment Operation completed on local store in " + totalSeconds + " seconds");
#endif
                }
                finally
                {
                    if(stmt != null)
                        stmt.FinalizeStm();
                }
            }
            return success;
        }
        
        /// <summary>
        /// Gets the size of the database.
        /// </summary>
        /// <returns>The database size.</returns>
        public long GetDatabaseSize()
        {
            FileInfo fileInfo = new FileInfo(System.IO.Path.Combine(AmazonHookedPlatformInfo.Instance.PersistentDataPath, dbPath));
            return fileInfo.Length;
        }
        
    }
}

