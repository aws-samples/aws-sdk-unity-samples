#define AWSSDK_UNITY
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
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;

using Amazon.Runtime.Internal.Util;
using Amazon.Runtime.Internal;
using Amazon.Runtime;
using System.Threading;

namespace Amazon.Util.Internal
{
    public static class InternalSDKUtils
    {
        public static void ApplyValues(object target, IDictionary<string, object> propertyValues)
        {
            if (propertyValues == null || propertyValues.Count == 0)
                return;

            var targetTypeInfo = TypeFactory.GetTypeInfo(target.GetType());

            foreach (var kvp in propertyValues)
            {
                var property = targetTypeInfo.GetProperty(kvp.Key);
                if (property == null)
                    throw new ArgumentException(string.Format("Unable to find property {0} on type {1}.", kvp.Key, targetTypeInfo.FullName));

                try
                {
                    var propertyTypeInfo = TypeFactory.GetTypeInfo(property.PropertyType);
                    if (propertyTypeInfo.IsEnum)
                    {
                        var enumValue = Enum.Parse(property.PropertyType, kvp.Value.ToString(), true);
                        property.SetValue(target, enumValue, null);
                    }
                    else
                    {
                        property.SetValue(target, kvp.Value, null);
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException(string.Format("Unable to set property {0} on type {1}: {2}", kvp.Key, targetTypeInfo.FullName, e.Message));
                }
            }
        }

        public static void AddToDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                    "Dictionary already contains item with key {0}", key));
            dictionary[key] = value;
        }

        public static void FillDictionary<T, TKey, TValue>(IEnumerable<T> items, Func<T, TKey> keyGenerator, Func<T, TValue> valueGenerator, Dictionary<TKey, TValue> targetDictionary)
        {
            foreach (var item in items)
            {
                var key = keyGenerator(item);
                var value = valueGenerator(item);
                AddToDictionary(targetDictionary, key, value);
            }
        }

        public static Dictionary<TKey, TValue> ToDictionary<T, TKey, TValue>(IEnumerable<T> items, Func<T, TKey> keyGenerator, Func<T, TValue> valueGenerator, IEqualityComparer<TKey> comparer = null)
        {
            Dictionary<TKey, TValue> dictionary;
            if (comparer == null)
                dictionary = new Dictionary<TKey, TValue>();
            else
                dictionary = new Dictionary<TKey, TValue>(comparer);

            FillDictionary(items, keyGenerator, valueGenerator, dictionary);

            return dictionary;
        }

        public static bool TryFindByValue<TKey, TValue>(
            IDictionary<TKey, TValue> dictionary, TValue value, IEqualityComparer<TValue> valueComparer,
            out TKey key)
        {
            foreach (var kvp in dictionary)
            {
                var candidateValue = kvp.Value;
                if (valueComparer.Equals(value, candidateValue))
                {
                    key = kvp.Key;
                    return true;
                }
            }

            key = default(TKey);
            return false;
        }

#if AWSSDK_UNITY
        private static Logger Logger = Logger.GetLogger(typeof(InternalSDKUtils));

        public static void AsyncExecutor(Action action, AsyncOptions options)
        {
            if (options.ExecuteCallbackOnMainThread)
            {
                if (UnityInitializer.IsMainThread())
                {
                    SafeExecute(action);
                }
                else
                {
                    UnityRequestQueue.Instance.ExecuteOnMainThread(action);
                }
            }
            else
            {
                if (!UnityInitializer.IsMainThread())
                {
                    SafeExecute(action);
                }
                else
                {
                    ThreadPool.QueueUserWorkItem((state) =>
                    {
                        SafeExecute(action);
                    });
                }
            }
        }

        public static void SafeExecute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                // Catch any unhandled exceptions from the user callback 
                // and log it. 
                Logger.Error(exception,
                    "An unhandled exception was thrown from the callback method {0}.",
                    action.Method.Name);
            }
        }
#endif

        #region IsSet methods

        /*
            Set
              Collection
                True -> set to empty AlwaysSend*
                False -> set to empty collection type
              Value type
                True -> set to default(T)
                False -> set to null

            Get
              Collection
                Field is AlwaysSend* OR has items -> True
                Otherwise -> False
              Value type
                Field is any value -> True
                Null -> False
         */

        public static void SetIsSet<T>(bool isSet, ref Nullable<T> field)
            where T : struct
        {
            if (isSet)
                field = default(T);
            else
                field = null;
        }
        public static void SetIsSet<T>(bool isSet, ref List<T> field)
        {
            if (isSet)
                field = new AlwaysSendList<T>(field);
            else
                field = new List<T>();
        }
        public static void SetIsSet<TKey, TValue>(bool isSet, ref Dictionary<TKey, TValue> field)
        {
            if (isSet)
                field = new AlwaysSendDictionary<TKey, TValue>(field);
            else
                field = new Dictionary<TKey, TValue>();
        }

        public static bool GetIsSet<T>(Nullable<T> field)
            where T : struct
        {
            return (field.HasValue);
        }
        public static bool GetIsSet<T>(List<T> field)
        {
            if (field == null)
                return false;

            if (field.Count > 0)
                return true;

            var sl = field as AlwaysSendList<T>;
            if (sl != null)
                return true;

            return false;
        }
        public static bool GetIsSet<TKey, TVvalue>(Dictionary<TKey, TVvalue> field)
        {
            if (field == null)
                return false;

            if (field.Count > 0)
                return true;

            var sd = field as AlwaysSendDictionary<TKey, TVvalue>;
            if (sd != null)
                return true;

            return false;
        }

        #endregion
    }
}
