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
 * Do not modify this file. This file is generated from the dynamodb-2012-08-10.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.DynamoDBv2.Model
{
    /// <summary>
    /// Represents <i>a single element</i> of a key schema. A key schema specifies the attributes
    /// that make up the primary key of a table, or the key attributes of an index.
    /// 
    ///  
    /// <para>
    /// A <i>KeySchemaElement</i> represents exactly one attribute of the primary key. For
    /// example, a hash type primary key would be represented by one <i>KeySchemaElement</i>.
    /// A hash-and-range type primary key would require one <i>KeySchemaElement</i> for the
    /// hash attribute, and another <i>KeySchemaElement</i> for the range attribute.
    /// </para>
    /// </summary>
    public partial class KeySchemaElement
    {
        private string _attributeName;
        private KeyType _keyType;

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public KeySchemaElement() { }

        /// <summary>
        /// Instantiates KeySchemaElement with the parameterized properties
        /// </summary>
        /// <param name="attributeName">The name of a key attribute.</param>
        /// <param name="keyType">The attribute data, consisting of the data type and the attribute value itself.</param>
        public KeySchemaElement(string attributeName, KeyType keyType)
        {
            _attributeName = attributeName;
            _keyType = keyType;
        }

        /// <summary>
        /// Gets and sets the property AttributeName. 
        /// <para>
        /// The name of a key attribute.
        /// </para>
        /// </summary>
        public string AttributeName
        {
            get { return this._attributeName; }
            set { this._attributeName = value; }
        }

        // Check to see if AttributeName property is set
        internal bool IsSetAttributeName()
        {
            return this._attributeName != null;
        }

        /// <summary>
        /// Gets and sets the property KeyType. 
        /// <para>
        /// The attribute data, consisting of the data type and the attribute value itself.
        /// </para>
        /// </summary>
        public KeyType KeyType
        {
            get { return this._keyType; }
            set { this._keyType = value; }
        }

        // Check to see if KeyType property is set
        internal bool IsSetKeyType()
        {
            return this._keyType != null;
        }

    }
}