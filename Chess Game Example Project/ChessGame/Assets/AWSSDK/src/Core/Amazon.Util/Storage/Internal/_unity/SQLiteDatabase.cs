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
using System.Text;
using System.Collections.Generic;

using Amazon.Runtime;
using System.Runtime.InteropServices;

namespace Amazon.Util.Storage.Internal
{
    public class SQLiteDatabase
    {
        public enum Result : int
        {
            SQLITE_OK = 0, /* Successful result */
            SQLITE_ERROR = 1, /* SQL error or missing database */
            SQLITE_INTERNAL = 2, /* An internal logic error in SQLite */
            SQLITE_PERM = 3, /* Access permission denied */
            SQLITE_ABORT = 4, /* Callback routine requested an abort */
            SQLITE_BUSY = 5, /* The database file is locked */
            SQLITE_LOCKED = 6, /* A table in the database is locked */
            SQLITE_NOMEM = 7, /* A malloc() failed */
            SQLITE_READONLY = 8, /* Attempt to write a readonly database */
            SQLITE_INTERRUPT = 9, /* Operation terminated by sqlite_interrupt() */
            SQLITE_IOERR = 10, /* Some kind of disk I/O error occurred */
            SQLITE_CORRUPT = 11, /* The database disk image is malformed */
            SQLITE_NOTFOUND = 12, /* (Internal Only) Table or record not found */
            SQLITE_FULL = 13, /* Insertion failed because database is full */
            SQLITE_CANTOPEN = 14, /* Unable to open the database file */
            SQLITE_PROTOCOL = 15, /* Database lock protocol error */
            SQLITE_EMPTY = 16, /* (Internal Only) Database table is empty */
            SQLITE_SCHEMA = 17, /* The database schema changed */
            SQLITE_TOOBIG = 18, /* Too much data for one row of a table */
            SQLITE_CONSTRAINT = 19, /* Abort due to constraint violation */
            SQLITE_MISMATCH = 20, /* Data type mismatch */
            SQLITE_MISUSE = 21, /* Library used incorrectly */
            SQLITE_NOLFS = 22, /* Uses OS features not supported on host */
            SQLITE_AUTH = 23, /* Authorization denied */
            SQLITE_FORMAT = 24,  /* Auxiliary database format error */
            SQLITE_RANGE = 25,   /* 2nd parameter to sqlite3_bind out of range */
            SQLITE_NOTADB = 26,   /* File opened that is not a database file */
            SQLITE_NOTICE = 27,   /* Notifications from sqlite3_log() */
            SQLITE_WARNING = 28,   /* Warnings from sqlite3_log() */
            SQLITE_ROW = 100, /* sqlite_step() has another row ready */
            SQLITE_DONE = 101  /* sqlite_step() has finished executing */
        }

        public enum DataType : int
        {
            INTEGER = 1,
            FLOAT = 2,
            TEXT = 3,
            BLOB = 4,
            NULL = 5
        }



        [DllImport("sqlite3", EntryPoint = "sqlite3_open")]
        public static extern Result sqlite3_open(string filename, out IntPtr db);

        [DllImport("sqlite3", EntryPoint = "sqlite3_close")]
        public static extern Result sqlite3_close(IntPtr db);

        [DllImport("sqlite3", EntryPoint = "sqlite3_prepare_v2")]
        public static extern Result sqlite3_prepare_v2(IntPtr db, string zSql, int nByte, out IntPtr ppStmpt, IntPtr pzTail);

        [DllImport("sqlite3.dll", EntryPoint = "sqlite3_exec")]
        public static extern Result exec(IntPtr hDb, string sql, IntPtr callback, IntPtr args, out IntPtr errorMessage);

        [DllImport("sqlite3", EntryPoint = "sqlite3_step")]
        public static extern Result sqlite3_step(IntPtr hStmt);

        [DllImport("sqlite3", EntryPoint = "sqlite3_finalize")]
        public static extern Result sqlite3_finalize(IntPtr hStmt);

        [DllImport("sqlite3", EntryPoint = "sqlite3_errmsg")]
        public static extern IntPtr sqlite3_errmsg(IntPtr db);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_count")]
        public static extern int sqlite3_column_count(IntPtr hStmt);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_name")]
        public static extern IntPtr sqlite3_column_name(IntPtr hStmt, int iCol);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_type")]
        public static extern DataType sqlite3_column_type(IntPtr hStmt, int iCol);

        [DllImport("sqlite3.dll", EntryPoint = "sqlite3_column_int64")]
        public static extern long sqlite3_column_int64(IntPtr hStmt, int nCol);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_text")]
        public static extern IntPtr sqlite3_column_text(IntPtr hStmt, int iCol);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_blob")]
        public static extern IntPtr sqlite3_column_blob(IntPtr hStmt, int iCol);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_bytes")]
        public static extern Result sqlite3_column_bytes(IntPtr hStmt, int iCol);

        [DllImport("sqlite3.dll", EntryPoint = "sqlite3_bind_int64")]
        public static extern Result sqlite3_bind_int64(IntPtr hStmt, int n, long value);

        [DllImport("sqlite3.dll", EntryPoint = "sqlite3_bind_null")]
        public static extern Result sqlite3_bind_null(IntPtr hStmt, int n);

        [DllImport("sqlite3.dll", EntryPoint = "sqlite3_bind_text")]
        public static extern Result sqlite3_bind_text(IntPtr hStmt, int n, byte[] value, int length, IntPtr freetype);

        [DllImport("sqlite3.dll", EntryPoint = "sqlite3_last_insert_rowid")]
        public static extern long sqlite3_last_insert_rowid(IntPtr hDb);

        [DllImport("sqlite3.dll", EntryPoint = "sqlite3_reset")]
        public static extern Result sqlite3_reset(IntPtr hStmt);

        [DllImport("sqlite3.dll", EntryPoint = "sqlite3_clear_bindings")] //
        public static extern Result sqlite3_clear_bindings(IntPtr hStmt);


        public static byte[] StringToUTF8ByteArray(string sText)
        {
            byte[] byteArray;
            int nLen = Encoding.UTF8.GetByteCount(sText) + 1;
            byteArray = new byte[nLen];
            nLen = Encoding.UTF8.GetBytes(sText, 0, sText.Length, byteArray, 0);
            byteArray[nLen] = 0;
            return byteArray;
        }

        public static string PtrToString(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }



        private string dbPath;
        internal IntPtr hDb = IntPtr.Zero;

        public SQLiteDatabase(string dbPath)
        {
            if (dbPath == null)
                throw new ArgumentNullException("dataPath");

            this.dbPath = dbPath;

            if (SQLiteDatabase.sqlite3_open(dbPath, out hDb) != SQLiteDatabase.Result.SQLITE_OK)
                throw new AmazonClientException("Could not open database file: " + dbPath);
        }


        public void CloseDatabase()
        {
            if (hDb != IntPtr.Zero)
            {
                SQLiteDatabase.sqlite3_close(hDb);
                hDb = IntPtr.Zero;
            }
        }


        public SQLiteStatement Prepare(string SQL)
        {
            if (hDb == IntPtr.Zero)
                throw new AmazonClientException("Database closed: " + dbPath);
            return new SQLiteStatement(this, SQL);
        }

        public void OpenDatabase()
        {
            if (SQLiteDatabase.sqlite3_open(dbPath, out hDb) != SQLiteDatabase.Result.SQLITE_OK)
                throw new AmazonClientException("Could not open database file: " + dbPath);
        }

        public long LastInsertRowid()
        {
            return SQLiteDatabase.sqlite3_last_insert_rowid(hDb);
        }

        public void Exec(string SQL)
        {
            IntPtr hError;
            Result exitCode;

            if (hDb == IntPtr.Zero)
                throw new AmazonClientException("SQLite database is not open.");

            exitCode = SQLiteDatabase.exec(hDb, SQL, IntPtr.Zero, IntPtr.Zero, out hError);

            if (exitCode != SQLiteDatabase.Result.SQLITE_OK)
                throw new AmazonClientException("Could not execute SQL statement." + ErrorMsg());
        }

        public String ErrorMsg()
        {
            return PtrToString(sqlite3_errmsg(this.hDb));
        }
    }

    public class SQLiteStatement
    {
        private IntPtr hStmt = IntPtr.Zero;
        private SQLiteDatabase db;

        internal SQLiteStatement(SQLiteDatabase db, string query)
        {
            this.db = db;
            SQLiteDatabase.Result exitCode = SQLiteDatabase.sqlite3_prepare_v2(db.hDb, query, -1, out hStmt, IntPtr.Zero);
            if (exitCode != SQLiteDatabase.Result.SQLITE_OK)
                throw new AmazonClientException(exitCode + " - " + db.ErrorMsg());
        }

        public SQLiteStatement Step()
        {
            SQLiteDatabase.Result eResult = SQLiteDatabase.sqlite3_step(this.hStmt);
            if (eResult != SQLiteDatabase.Result.SQLITE_DONE)
                throw new AmazonClientException(db.ErrorMsg());
            return this;
        }

        public SQLiteStatement BindText(int column, string inputString)
        {
            SQLiteDatabase.Result exitCode;
            if (inputString == null)
                exitCode = SQLiteDatabase.sqlite3_bind_null(hStmt, column);
            else
                exitCode = SQLiteDatabase.sqlite3_bind_text(hStmt, column, SQLiteDatabase.StringToUTF8ByteArray(inputString), -1, new IntPtr(-1));

            if (exitCode != SQLiteDatabase.Result.SQLITE_OK)
                throw new AmazonServiceException(exitCode + " - " + db.ErrorMsg());
            return this;
        }

        public SQLiteStatement BindInt(int column, long? inputInt)
        {
            SQLiteDatabase.Result exitCode;
            if (inputInt == null)
                exitCode = SQLiteDatabase.sqlite3_bind_null(hStmt, column);
            else
                exitCode = SQLiteDatabase.sqlite3_bind_int64(hStmt, column, (long)inputInt);

            if (exitCode != SQLiteDatabase.Result.SQLITE_OK)
                throw new AmazonClientException(exitCode + " - " + db.ErrorMsg());
            return this;
        }

        public SQLiteStatement BindDateTime(int column, DateTime? inputDateTime)
        {
            return BindText(column, (inputDateTime == null ? null : inputDateTime.Value.ToUniversalTime().Ticks.ToString()));
        }

        public Dictionary<string, SQLiteField> Fields = new Dictionary<string, SQLiteField>();

        public bool Read()
        {
            bool bContinue = false;
            SQLiteField field;
            SQLiteDatabase.Result exitCode = SQLiteDatabase.sqlite3_step(this.hStmt);
            if (exitCode == SQLiteDatabase.Result.SQLITE_DONE)
            {
                //return false;
            }
            else if (exitCode == SQLiteDatabase.Result.SQLITE_ROW)
            {
                int columns = SQLiteDatabase.sqlite3_column_count(hStmt);
                Fields.Clear();
                for (int column = 0; column < columns; column++)
                {
                    field = new SQLiteField(hStmt);
                    field.column = column;
                    field.name = Marshal.PtrToStringAnsi(SQLiteDatabase.sqlite3_column_name(hStmt, column));
                    field.Read();
                    Fields.Add(field.name, field);
                }

                bContinue = true;
            }
            else
            {
                throw new AmazonClientException(exitCode + "\n" + SQLiteDatabase.sqlite3_errmsg(db.hDb));
            }
            return bContinue;
        }


        public SQLiteStatement Reset()
        {
            if (SQLiteDatabase.sqlite3_reset(hStmt) != SQLiteDatabase.Result.SQLITE_OK)
                throw new AmazonClientException(db.ErrorMsg());
            return this;
        }

        public SQLiteStatement ClearBindings()
        {

            if (SQLiteDatabase.sqlite3_clear_bindings(hStmt) != SQLiteDatabase.Result.SQLITE_OK)
                throw new AmazonClientException(db.ErrorMsg());

            return this;
        }

        public SQLiteStatement FinalizeStm()
        {
            if (SQLiteDatabase.sqlite3_finalize(hStmt) != SQLiteDatabase.Result.SQLITE_OK)
                throw new AmazonClientException(db.ErrorMsg());
            this.hStmt = IntPtr.Zero;
            return this;
        }

    }


    public class SQLiteField
    {
        public string name;
        public int column;
        public SQLiteDatabase.DataType eType;
        public long nValue;
        public string sValue;
        public double fValue;
        public bool bNull;
        private IntPtr hStm = IntPtr.Zero;

        public SQLiteField(IntPtr hStm)
        {
            this.hStm = hStm;
        }

        public void Read()
        {
            bNull = false;
            nValue = 0;
            sValue = "";
            fValue = 0d;
            eType = SQLiteDatabase.sqlite3_column_type(hStm, column);
            switch (eType)
            {
                case SQLiteDatabase.DataType.NULL:
                    bNull = true;
                    break;
                case SQLiteDatabase.DataType.INTEGER:
                    nValue = SQLiteDatabase.sqlite3_column_int64(hStm, column);
                    break;
                case SQLiteDatabase.DataType.TEXT:
                    sValue = SQLiteDatabase.PtrToString(SQLiteDatabase.sqlite3_column_text(hStm, column));
                    break;
                default:
                    throw new AmazonClientException("Unknown datatype");
            }
        }

        public bool IsNull()
        {
            return bNull;
        }

        public bool BOOL
        {
            get
            {
                if (eType != SQLiteDatabase.DataType.INTEGER)
                    throw new AmazonClientException("Datatype: " + eType.ToString());
                return (nValue != 0);
            }
        }

        public string TEXT
        {
            get
            {
                if (IsNull())
                    return null;

                if (eType != SQLiteDatabase.DataType.TEXT)
                    throw new AmazonClientException("Datatype: " + eType.ToString());
                return sValue;
            }
        }

        public long INTEGER
        {
            get
            {
                if (eType != SQLiteDatabase.DataType.INTEGER)
                    throw new AmazonClientException("Datatype: " + eType.ToString());
                return nValue;
            }
        }

        public DateTime? DATETIME
        {
            get
            {
                if (IsNull())
                    return null;

                if (eType != SQLiteDatabase.DataType.TEXT)
                    throw new AmazonClientException("Datatype: " + eType.ToString());

                return sValue == null ? (DateTime?)null : new DateTime(long.Parse(sValue)).ToLocalTime();
            }
        }
    }
}

