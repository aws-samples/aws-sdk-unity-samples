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
using System.Threading;

using UnityEngine;

namespace Amazon.Unity
{
    public class AmazonInitializer : MonoBehaviour
    {
        
        private static AmazonInitializer _instance = null;
        private static bool _initialized = false;
        private static object _lock = new object();

        #region Inspector variables
        public int MaxConnectionPoolSize = 10;
        public string AMAZON_ACCOUNT_ID = "";
        public string IDENTITY_POOL_ID = "";
        public string DEFAULT_UNAUTH_ROLE = "";
        public string DEFAULT_AUTH_ROLE = "";
        public enum RegionEndpointEnum
        {
            USEast1, EUWest1
        }
        ;
        public RegionEndpointEnum COGNITO_REGIONENDPOINT; // This will create the dropdown in the inspector
        #endregion
        

        #region Internal statics
        /// <summary>
        /// Determines if its initialized & running.
        /// </summary>
        /// <returns><c>true</c> if is running; otherwise, <c>false</c>.</returns>
        internal static bool IsInitialized
        {
            get
            {
                return _initialized;
            }
        }

        internal static string persistentDataPath = null;

        #endregion
        
        public void Awake ()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    // singleton instance
                    _instance = this;
                    
                    // preventing the instance from getting destroyed between scenes
                    DontDestroyOnLoad (_instance.gameObject);
                    Amazon.RegionEndpoint.LoadEndpointDefinitions ();
                    persistentDataPath = Application.persistentDataPath;

                    _instance.gameObject.AddComponent<AmazonMainThreadDispatcher>();
                    _initialized = true;
                }
                else
                {
                    if (this != _instance)
                        Destroy (this.gameObject);
                }
                
            }
        }
        
        #region internal properties
        
        internal static string AmazonAccountId
        {
            get
            {
                if (!AmazonInitializer.IsInitialized || string.IsNullOrEmpty(_instance.AMAZON_ACCOUNT_ID))
                {
                    throw new NotSupportedException ("Please setup the inspector variables in AWSPrefab for the scene");
                }
                return _instance.AMAZON_ACCOUNT_ID;
            }
        }
        
        internal static string IdentityPoolId
        {
            get
            {
                if (!AmazonInitializer.IsInitialized || string.IsNullOrEmpty(_instance.IDENTITY_POOL_ID))
                {
                    throw new NotSupportedException ("Please setup the inspector variables in AWSPrefab for the scene");
                }
                return _instance.IDENTITY_POOL_ID;
            }
        }
        
        internal static string DefaultAuthRole
        {
            get
            {
                if (!AmazonInitializer.IsInitialized)
                {
                    throw new NotSupportedException ("Please setup the inspector variables in AWSPrefab for the scene");
                }
                return _instance.DEFAULT_AUTH_ROLE;
            }
        }
        
        internal static string DefaultUnAuthRole
        {
            get
            {
                if (!AmazonInitializer.IsInitialized)
                {
                    throw new NotSupportedException ("Please setup the inspector variables in AWSPrefab for the scene");
                }
                return _instance.DEFAULT_UNAUTH_ROLE;
            }
        }
        
        internal static RegionEndpoint CognitoRegionEndpoint
        {
            get
            {
                return _instance.COGNITO_REGIONENDPOINT == RegionEndpointEnum.USEast1 ? RegionEndpoint.USEast1: RegionEndpoint.EUWest1;
            }
        }
        
        #endregion
        
        private AmazonInitializer ()
        {
        }

    }
}

