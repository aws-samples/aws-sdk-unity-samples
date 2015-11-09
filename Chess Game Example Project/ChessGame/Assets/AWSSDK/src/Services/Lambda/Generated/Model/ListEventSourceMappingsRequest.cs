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
 * Do not modify this file. This file is generated from the lambda-2015-03-31.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.Lambda.Model
{
    /// <summary>
    /// Container for the parameters to the ListEventSourceMappings operation.
    /// Returns a list of event source mappings you created using the <code>CreateEventSourceMapping</code>
    /// (see <a>CreateEventSourceMapping</a>), where you identify a stream as an event source.
    /// This list does not include Amazon S3 event sources. 
    /// 
    ///  
    /// <para>
    /// For each mapping, the API returns configuration information. You can optionally specify
    /// filters to retrieve specific event source mappings.
    /// </para>
    ///  
    /// <para>
    /// This operation requires permission for the <code>lambda:ListEventSourceMappings</code>
    /// action.
    /// </para>
    /// </summary>
    public partial class ListEventSourceMappingsRequest : AmazonLambdaRequest
    {
        private string _eventSourceArn;
        private string _functionName;
        private string _marker;
        private int? _maxItems;

        /// <summary>
        /// Gets and sets the property EventSourceArn. 
        /// <para>
        /// The Amazon Resource Name (ARN) of the Amazon Kinesis stream.
        /// </para>
        /// </summary>
        public string EventSourceArn
        {
            get { return this._eventSourceArn; }
            set { this._eventSourceArn = value; }
        }

        // Check to see if EventSourceArn property is set
        internal bool IsSetEventSourceArn()
        {
            return this._eventSourceArn != null;
        }

        /// <summary>
        /// Gets and sets the property FunctionName. 
        /// <para>
        /// The name of the Lambda function.
        /// </para>
        ///  
        /// <para>
        ///  You can specify an unqualified function name (for example, "Thumbnail") or you can
        /// specify Amazon Resource Name (ARN) of the function (for example, "arn:aws:lambda:us-west-2:account-id:function:ThumbNail").
        /// AWS Lambda also allows you to specify only the account ID qualifier (for example,
        /// "account-id:Thumbnail"). Note that the length constraint applies only to the ARN.
        /// If you specify only the function name, it is limited to 64 character in length. 
        /// </para>
        /// </summary>
        public string FunctionName
        {
            get { return this._functionName; }
            set { this._functionName = value; }
        }

        // Check to see if FunctionName property is set
        internal bool IsSetFunctionName()
        {
            return this._functionName != null;
        }

        /// <summary>
        /// Gets and sets the property Marker. 
        /// <para>
        /// Optional string. An opaque pagination token returned from a previous <code>ListEventSourceMappings</code>
        /// operation. If present, specifies to continue the list from where the returning call
        /// left off. 
        /// </para>
        /// </summary>
        public string Marker
        {
            get { return this._marker; }
            set { this._marker = value; }
        }

        // Check to see if Marker property is set
        internal bool IsSetMarker()
        {
            return this._marker != null;
        }

        /// <summary>
        /// Gets and sets the property MaxItems. 
        /// <para>
        /// Optional integer. Specifies the maximum number of event sources to return in response.
        /// This value must be greater than 0.
        /// </para>
        /// </summary>
        public int MaxItems
        {
            get { return this._maxItems.GetValueOrDefault(); }
            set { this._maxItems = value; }
        }

        // Check to see if MaxItems property is set
        internal bool IsSetMaxItems()
        {
            return this._maxItems.HasValue; 
        }

    }
}