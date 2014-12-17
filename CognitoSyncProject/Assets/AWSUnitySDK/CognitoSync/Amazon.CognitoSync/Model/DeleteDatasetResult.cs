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
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.CognitoSync.Model
{
    /// <summary>
    /// Response to a successful DeleteDataset request.
    /// </summary>
    public partial class DeleteDatasetResult : AmazonWebServiceResponse
    {
        private Dataset _dataset;


        /// <summary>
        /// Gets and sets the property Dataset. A collection of data for an identity pool. An
        /// identity pool can have multiple datasets. A dataset is per identity and can be general
        /// or associated with a particular entity in an application (like a saved game). Datasets
        /// are automatically created if they don't exist. Data is synced by dataset, and a dataset
        /// can hold up to 1MB of key-value pairs.
        /// </summary>
        public Dataset Dataset
        {
            get { return this._dataset; }
            set { this._dataset = value; }
        }

        // Check to see if Dataset property is set
        internal bool IsSetDataset()
        {
            return this._dataset != null;
        }

    }
}