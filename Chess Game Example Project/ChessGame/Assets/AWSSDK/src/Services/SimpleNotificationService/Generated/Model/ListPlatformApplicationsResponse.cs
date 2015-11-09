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
 * Do not modify this file. This file is generated from the sns-2010-03-31.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.SimpleNotificationService.Model
{
    /// <summary>
    /// Response for ListPlatformApplications action.
    /// </summary>
    public partial class ListPlatformApplicationsResponse : AmazonWebServiceResponse
    {
        private string _nextToken;
        private List<PlatformApplication> _platformApplications = new List<PlatformApplication>();

        /// <summary>
        /// Gets and sets the property NextToken. 
        /// <para>
        /// NextToken string is returned when calling ListPlatformApplications action if additional
        /// records are available after the first page results.
        /// </para>
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

        /// <summary>
        /// Gets and sets the property PlatformApplications. 
        /// <para>
        /// Platform applications returned when calling ListPlatformApplications action.
        /// </para>
        /// </summary>
        public List<PlatformApplication> PlatformApplications
        {
            get { return this._platformApplications; }
            set { this._platformApplications = value; }
        }

        // Check to see if PlatformApplications property is set
        internal bool IsSetPlatformApplications()
        {
            return this._platformApplications != null && this._platformApplications.Count > 0; 
        }

    }
}