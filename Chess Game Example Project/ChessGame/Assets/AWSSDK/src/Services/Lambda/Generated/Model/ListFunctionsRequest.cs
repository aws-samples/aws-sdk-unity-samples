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
    /// Container for the parameters to the ListFunctions operation.
    /// Returns a list of your Lambda functions. For each function, the response includes
    /// the function configuration information. You must use <a>GetFunction</a> to retrieve
    /// the code for your function.
    /// 
    ///  
    /// <para>
    /// This operation requires permission for the <code>lambda:ListFunctions</code> action.
    /// </para>
    /// </summary>
    public partial class ListFunctionsRequest : AmazonLambdaRequest
    {
        private string _marker;
        private int? _maxItems;

        /// <summary>
        /// Gets and sets the property Marker. 
        /// <para>
        /// Optional string. An opaque pagination token returned from a previous <code>ListFunctions</code>
        /// operation. If present, indicates where to continue the listing. 
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
        /// Optional integer. Specifies the maximum number of AWS Lambda functions to return in
        /// response. This parameter value must be greater than 0.
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