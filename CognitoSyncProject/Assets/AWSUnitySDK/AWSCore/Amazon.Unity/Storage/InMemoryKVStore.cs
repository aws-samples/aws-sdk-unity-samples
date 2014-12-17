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
using System.Collections.Generic;


namespace Amazon.Unity.Storage
{
    public class InMemoryKVStore : KVStore
    {
        private static object _lock = new  object();

        public InMemoryKVStore()
        {
        }

        private static Dictionary<string, string> Store;
        static InMemoryKVStore()
        {
            lock(_lock)
            {
                if (Store == null)  Store = new Dictionary<string, string>();
            }
        }

        #region implemented abstract members of KVStore

        public override void Clear(string key)
        {
            lock (_lock)
            {
                InMemoryKVStore.Store.Remove(key);
            }
        }

        public override void Put(string key, string value)
        {
            lock(_lock)
            {
                InMemoryKVStore.Store[key] = value;
            }
        }

        public override string Get(string key)
        {
            lock(_lock)
            {
                string get = InMemoryKVStore.Store.ContainsKey(key) ? InMemoryKVStore.Store[key] : null;
                return get;
            }
        }

        #endregion
    }
}

