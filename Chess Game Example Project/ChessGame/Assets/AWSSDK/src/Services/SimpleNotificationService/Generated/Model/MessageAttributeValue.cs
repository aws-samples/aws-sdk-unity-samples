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
    /// The user-specified message attribute value. For string data types, the value attribute
    /// has the same restrictions on the content as the message body.       For more information,
    /// see <a href="http://docs.aws.amazon.com/sns/latest/api/API_Publish.html">Publish</a>.
    /// 
    ///         
    /// <para>
    /// Name, type, and value must not be empty or null. In addition, the message body should
    /// not be empty or null.       All parts of the message attribute, including name, type,
    /// and value, are included in the message size restriction, which is currently 256 KB
    /// (262,144 bytes).
    /// </para>
    /// </summary>
    public partial class MessageAttributeValue
    {
        private MemoryStream _binaryValue;
        private string _dataType;
        private string _stringValue;

        /// <summary>
        /// Gets and sets the property BinaryValue. 
        /// <para>
        /// Binary type attributes can store any binary data, for example, compressed data, encrypted
        /// data, or images.
        /// </para>
        /// </summary>
        public MemoryStream BinaryValue
        {
            get { return this._binaryValue; }
            set { this._binaryValue = value; }
        }

        // Check to see if BinaryValue property is set
        internal bool IsSetBinaryValue()
        {
            return this._binaryValue != null;
        }

        /// <summary>
        /// Gets and sets the property DataType. 
        /// <para>
        /// Amazon SNS supports the following logical data types: String, Number, and Binary.
        /// For more information, see <a href="http://docs.aws.amazon.com/sns/latest/dg/SNSMessageAttributes.html#SNSMessageAttributes.DataTypes">Message
        /// Attribute Data Types</a>.
        /// </para>
        /// </summary>
        public string DataType
        {
            get { return this._dataType; }
            set { this._dataType = value; }
        }

        // Check to see if DataType property is set
        internal bool IsSetDataType()
        {
            return this._dataType != null;
        }

        /// <summary>
        /// Gets and sets the property StringValue. 
        /// <para>
        /// Strings are Unicode with UTF8 binary encoding.       For a list of code values, see
        /// <a href="http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters">http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters</a>.
        /// </para>
        /// </summary>
        public string StringValue
        {
            get { return this._stringValue; }
            set { this._stringValue = value; }
        }

        // Check to see if StringValue property is set
        internal bool IsSetStringValue()
        {
            return this._stringValue != null;
        }

    }
}