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
    /// Returned for a successful ListDatasets request.
    /// </summary>
    public partial class ListDatasetsResult : AmazonWebServiceResponse
    {
        private int? _count;
        private List<Dataset> _datasets = new List<Dataset>();
        private string _nextToken;

        /// <summary>
        /// Gets and sets the property Count. Number of datasets returned.
        /// </summary>
        public int Count
        {
            get { return this._count.GetValueOrDefault(); }
            set { this._count = value; }
        }

        // Check to see if Count property is set
        internal bool IsSetCount()
        {
            return this._count.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property Datasets. A set of datasets.
        /// </summary>
        public List<Dataset> Datasets
        {
            get { return this._datasets; }
            set { this._datasets = value; }
        }

        // Check to see if Datasets property is set
        internal bool IsSetDatasets()
        {
            return this._datasets != null && this._datasets.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property NextToken. A pagination token for obtaining the next page
        /// of results.
        /// </summary>
        public string NextToken
        {
            get { return this._nextToken; }
            set { this._nextToken = value; }
        }

        // Check to see if NextToken property is set
        internal bool IsSetNextToken()
        {
            return this._nextToken != null;
        }

    }
}
