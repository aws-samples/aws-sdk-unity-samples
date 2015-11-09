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
    /// The object for the Lambda function location.
    /// </summary>
    public partial class FunctionCodeLocation
    {
        private string _location;
        private string _repositoryType;

        /// <summary>
        /// Gets and sets the property Location. 
        /// <para>
        /// The presigned URL you can use to download the function's .zip file that you previously
        /// uploaded. The URL is valid for up to 10 minutes.
        /// </para>
        /// </summary>
        public string Location
        {
            get { return this._location; }
            set { this._location = value; }
        }

        // Check to see if Location property is set
        internal bool IsSetLocation()
        {
            return this._location != null;
        }

        /// <summary>
        /// Gets and sets the property RepositoryType. 
        /// <para>
        /// The repository from which you can download the function.
        /// </para>
        /// </summary>
        public string RepositoryType
        {
            get { return this._repositoryType; }
            set { this._repositoryType = value; }
        }

        // Check to see if RepositoryType property is set
        internal bool IsSetRepositoryType()
        {
            return this._repositoryType != null;
        }

    }
}