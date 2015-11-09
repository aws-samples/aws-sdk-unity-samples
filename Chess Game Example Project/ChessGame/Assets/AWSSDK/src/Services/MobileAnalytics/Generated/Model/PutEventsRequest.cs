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
 * Do not modify this file. This file is generated from the mobileanalytics-2014-06-05.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.MobileAnalytics.Model
{
    /// <summary>
    /// Container for the parameters to the PutEvents operation.
    /// The PutEvents operation records one or more events. You can have up to 1,500 unique
    /// custom events per app, any combination of up to 40 attributes and metrics per custom
    /// event, and any number of attribute or metric values.
    /// </summary>
    public partial class PutEventsRequest : AmazonMobileAnalyticsRequest
    {
        private string _clientContext;
        private string _clientContextEncoding;
        private List<Event> _events = new List<Event>();

        /// <summary>
        /// Gets and sets the property ClientContext. 
        /// <para>
        /// The client context including the client ID, app title, app version and package name.
        /// </para>
        /// </summary>
        public string ClientContext
        {
            get { return this._clientContext; }
            set { this._clientContext = value; }
        }

        // Check to see if ClientContext property is set
        internal bool IsSetClientContext()
        {
            return this._clientContext != null;
        }

        /// <summary>
        /// Gets and sets the property ClientContextEncoding. 
        /// <para>
        /// The encoding used for the client context.
        /// </para>
        /// </summary>
        public string ClientContextEncoding
        {
            get { return this._clientContextEncoding; }
            set { this._clientContextEncoding = value; }
        }

        // Check to see if ClientContextEncoding property is set
        internal bool IsSetClientContextEncoding()
        {
            return this._clientContextEncoding != null;
        }

        /// <summary>
        /// Gets and sets the property Events. 
        /// <para>
        /// An array of Event JSON objects
        /// </para>
        /// </summary>
        public List<Event> Events
        {
            get { return this._events; }
            set { this._events = value; }
        }

        // Check to see if Events property is set
        internal bool IsSetEvents()
        {
            return this._events != null && this._events.Count > 0; 
        }

    }
}