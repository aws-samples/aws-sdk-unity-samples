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
    /// Describes the session. Session information is required on ALL events.
    /// </summary>
    public partial class Session
    {
        private long? _duration;
        private string _id;
        private DateTime? _startTimestamp;
        private DateTime? _stopTimestamp;

        /// <summary>
        /// Gets and sets the property Duration. 
        /// <para>
        /// The duration of the session.
        /// </para>
        /// </summary>
        public long Duration
        {
            get { return this._duration.GetValueOrDefault(); }
            set { this._duration = value; }
        }

        // Check to see if Duration property is set
        internal bool IsSetDuration()
        {
            return this._duration.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property Id. 
        /// <para>
        /// A unique identifier for the session
        /// </para>
        /// </summary>
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        // Check to see if Id property is set
        internal bool IsSetId()
        {
            return this._id != null;
        }

        /// <summary>
        /// Gets and sets the property StartTimestamp. 
        /// <para>
        /// The time the event started in ISO 8601 standard date time format. For example, 2014-06-30T19:07:47.885Z
        /// </para>
        /// </summary>
        public DateTime StartTimestamp
        {
            get { return this._startTimestamp.GetValueOrDefault(); }
            set { this._startTimestamp = value; }
        }

        // Check to see if StartTimestamp property is set
        internal bool IsSetStartTimestamp()
        {
            return this._startTimestamp.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property StopTimestamp. 
        /// <para>
        /// The time the event terminated in ISO 8601 standard date time format. For example,
        /// 2014-06-30T19:07:47.885Z
        /// </para>
        /// </summary>
        public DateTime StopTimestamp
        {
            get { return this._stopTimestamp.GetValueOrDefault(); }
            set { this._stopTimestamp = value; }
        }

        // Check to see if StopTimestamp property is set
        internal bool IsSetStopTimestamp()
        {
            return this._stopTimestamp.HasValue; 
        }

    }
}