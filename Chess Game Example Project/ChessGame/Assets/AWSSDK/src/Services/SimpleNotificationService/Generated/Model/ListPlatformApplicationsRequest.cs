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
    /// Container for the parameters to the ListPlatformApplications operation.
    /// Lists the platform application objects for the supported push notification services,
    ///       such as APNS and GCM. The results for <code>ListPlatformApplications</code>
    /// are paginated and return a limited list of applications, up to 100.      If additional
    /// records are available after the first page results, then a NextToken string will be
    /// returned.       To receive the next page, you call <code>ListPlatformApplications</code>
    /// using the NextToken string received from the previous call.       When there are no
    /// more records to return, NextToken will be null.            For more information, see
    /// <a href="http://docs.aws.amazon.com/sns/latest/dg/SNSMobilePush.html">Using Amazon
    /// SNS Mobile Push Notifications</a>.
    /// </summary>
    public partial class ListPlatformApplicationsRequest : AmazonSimpleNotificationServiceRequest
    {
        private string _nextToken;

        /// <summary>
        /// Gets and sets the property NextToken. 
        /// <para>
        /// NextToken string is used when calling ListPlatformApplications action to retrieve
        /// additional records that are available after the first page results.
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

    }
}