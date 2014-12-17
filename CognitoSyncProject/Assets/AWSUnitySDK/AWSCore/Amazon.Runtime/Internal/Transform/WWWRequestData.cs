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
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

namespace Amazon.Runtime.Internal.Transform
{
    /// <summary>
    /// Its a container for Url, Data and Headers which are used in the AmazonMainThreadDispatcher 
    /// for firing WWW request. The purpose of having container with complete final requests parameters
    /// is to minimize the work performed by the Main thread 
    /// </summary>
    internal class WWWRequestData
    {
        public string Url { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public byte[] Data { get; set; }
        private WWW _request;

        public WWWRequestData()
        {
            Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Can be run only from the Main thread because of Unity's restriction on WWW API usage
        /// Fires WWW request using available parameters
        /// </summary>
        public WWW FireRequest()
        {
            // checking for main thread
            if (!Amazon.Unity.AmazonMainThreadDispatcher.IsMainThread)
            {
                throw new InvalidOperationException("Supported only on main(game) thread");
            }

            // Unity 4.3 or later use Dictionary<string, string> for string whereas
            //  the older versions use Hashtables. Instead of maintaining of list of 
            //  supported/unsupported versions, reflection is used 
            Type[] constructorType = new Type[3]
            {
                typeof (string),
                typeof (byte[]),
                typeof (Dictionary<string, string>)
            };
            ConstructorInfo constructor = typeof(WWW).GetConstructor(constructorType);
            if (constructor != null)
            {
                _request = (WWW) constructor.Invoke(new object[3]
                {
                    (object) Url,
                    (object) Data,
                    (object) Headers
                });
            }
            else
            {
                Hashtable headerTable = new Hashtable();
                foreach(string headerkey in Headers.Keys)
                    headerTable.Add(headerkey, Headers[headerkey]);
                
                constructorType = new Type[3]
                {
                    typeof (string),
                    typeof (byte[]),
                    typeof (Hashtable)
                };
                _request = (WWW) typeof(WWW).GetConstructor(constructorType).Invoke(new object[3]
                {
                    (object) Url,
                    (object) Data,
                    (object) headerTable
                });
            }
            return _request;
        }

        /// <summary>
        /// Determines whether this instance is done.
        /// </summary>
        /// <returns><c>true</c> if this instance is done; otherwise, <c>false</c>.</returns>
        public bool IsDone()
        {
            return _request != null && _request.isDone;
        }

        /// <summary>
        /// Moves the WWW.response fields to WWWResponseData
        /// Again, this method can be invoked only from main thread
        /// </summary>
        /// <returns>The response data</returns>
        public WWWResponseData GetResponseData()
        {
            // checking for main thread
            if (!Amazon.Unity.AmazonMainThreadDispatcher.IsMainThread)
            {
                throw new InvalidOperationException("Supported only on main(game) thread");
            }
            if (!_request.isDone)
                throw new InvalidOperationException("Check IsDone() before calling GetResponseData()");

            return new WWWResponseData(_request);
        }
    }
}