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
using System.Text.RegularExpressions;

using Amazon.CognitoIdentity;
using Amazon.Runtime.Internal.Util;

namespace Amazon.CognitoSync.SyncManager.Internal
{
    public class DatasetUtils
    {
        /// <summary>
        /// Valid dataset name pattern
        /// </summary>
        public static readonly string DATASET_NAME_PATTERN = @"^[a-zA-Z0-9_.:-]{1,128}$";

        /// <summary>
        /// Unknown identity id when the identity id is null
        /// </summary>
        public static readonly string UNKNOWN_IDENTITY_ID = "unknown";

        /// <summary>
        /// Validates the dataset name.
        /// </summary>
        /// <returns>The dataset name.</returns>
        /// <param name="datasetName">Dataset name.</param>
        public static string ValidateDatasetName(string datasetName)
        {
            if (!Regex.IsMatch(datasetName, DATASET_NAME_PATTERN))
            {
                throw new ArgumentException("Invalid dataset name");
            }
            return datasetName;
        }

        /// <summary>
        /// Validates the record key. It must be non empty and its length must be no
        /// greater than 128. Otherwise {@link IllegalArgumentException} will be
        /// thrown.
        /// </summary>
        /// <returns>The record key.</returns>
        /// <param name="key">Key.</param>
        public static string ValidateRecordKey(string key)
        {
            if (string.IsNullOrEmpty(key) || StringUtils.Utf8ByteLength(key) > 128)
            {
                throw new ArgumentException("Invalid record key");
            }
            return key;
        }

        /// <summary>
        /// A helper function to compute record size which equals the sum of the
        /// UTF-8 string length of record key and value. 0 if record is null.
        /// </summary>
        /// <returns>The record size.</returns>
        /// <param name="record">Record.</param>
        public static long ComputeRecordSize(Record record)
        {
            if (record == null)
            {
                return 0;
            }
            return StringUtils.Utf8ByteLength(record.Key)
                + StringUtils.Utf8ByteLength(record.Value);
        }

        /// <summary>
        /// A helper function to get the identity id of the dataset from credentials
        /// provider. If the identity id is null, UNKNOWN_IDENTITY_ID will be
        /// returned.
        /// </summary>
        /// <returns>The identity identifier.</returns>
        /// <param name="provider">Provider.</param>
        public static string GetIdentityId(CognitoAWSCredentials credentials)
        {
            return string.IsNullOrEmpty(credentials.GetCachedIdentityId())
                ? UNKNOWN_IDENTITY_ID
                    : credentials.GetCachedIdentityId();
        }
    }
}

