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

/*
 * Do not modify this file. This file is generated from the cognito-sync-2014-06-30.normal.json service model.
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
    /// Response to a successful DescribeDataset request.
    /// </summary>
    public partial class DescribeDatasetResponse : AmazonWebServiceResponse
    {
        private Dataset _dataset;

        /// <summary>
        /// Gets and sets the property Dataset. Meta data for a collection of data for an identity.
        /// An identity can have multiple datasets. A dataset can be general or associated with
        /// a particular entity in an application (like a saved game). Datasets are automatically
        /// created if they don't exist. Data is synced by dataset, and a dataset can hold up
        /// to 1MB of key-value pairs.
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
