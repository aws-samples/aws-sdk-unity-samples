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

namespace Amazon.CognitoSync.Model
{
    /// <summary>
    /// Configuration for accessing Amazon ListRecords service
    /// </summary>
    public partial class ListRecordsResponse : ListRecordsResult
    {
        /// <summary>
        /// Gets and sets the ListRecordsResult property.
        /// Represents the output of a ListRecords operation.
        /// </summary>
        [Obsolete(@"This property has been deprecated. All properties of the ListRecordsResult class are now available on the ListRecordsResponse class. You should use the properties on ListRecordsResponse instead of accessing them through ListRecordsResult.")]
        public ListRecordsResult ListRecordsResult
        {
            get
            {
                return this;
            }
        }
    }
}