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
    /// Represents the data for an attribute. You can set one, and only one, of the elements.
    /// 
    ///  
    /// <para>
    /// Each attribute in an item is a name-value pair. An attribute can be single-valued
    /// or multi-valued set. For example, a book item can have title and authors attributes.
    /// Each book has one title but can have many authors. The multi-valued attribute is a
    /// set; duplicate values are not allowed. 
    /// </para>
    /// </summary>
    public partial class AttributeValue
    {
        private MemoryStream _b;
        private bool? _bool;
        private List<MemoryStream> _bs = new List<MemoryStream>();
        private List<AttributeValue> _l = new List<AttributeValue>();
        private Dictionary<string, AttributeValue> _m = new Dictionary<string, AttributeValue>();
        private string _n;
        private List<string> _ns = new List<string>();
        private bool? _null;
        private string _s;
        private List<string> _ss = new List<string>();

        /// <summary>
        /// Empty constructor used to set  properties independently even when a simple constructor is available
        /// </summary>
        public AttributeValue() { }

        /// <summary>
        /// Instantiates AttributeValue with the parameterized properties
        /// </summary>
        /// <param name="s">A String data type.</param>
        public AttributeValue(string s)
        {
            _s = s;
        }

        /// <summary>
        /// Instantiates AttributeValue with the parameterized properties
        /// </summary>
        /// <param name="ss">A String Set data type.</param>
        public AttributeValue(List<string> ss)
        {
            _ss = ss;
        }

        /// <summary>
        /// Gets and sets the property B. 
        /// <para>
        /// A Binary data type.
        /// </para>
        /// </summary>
        public MemoryStream B
        {
            get { return this._b; }
            set { this._b = value; }
        }

        // Check to see if B property is set
        internal bool IsSetB()
        {
            return this._b != null;
        }

        /// <summary>
        /// Gets and sets the property BOOL. 
        /// <para>
        /// A Boolean data type.
        /// </para>
        /// </summary>
        public bool BOOL
        {
            get { return this._bool.GetValueOrDefault(); }
            set { this._bool = value; }
        }

        /// <summary>
        /// This property is set to true if the property <seealso cref="BOOL"/>
        /// is set; false otherwise.
        /// This property can be used to determine if the related property
        /// was returned by a service response or if the related property
        /// should be sent to the service during a service call.
        /// </summary>
        /// <returns>
        /// True if the related property was set or will be sent to a service; false otherwise.
        /// </returns>
        public bool IsBOOLSet
        {
            get
            {
                return Amazon.Util.Internal.InternalSDKUtils.GetIsSet(this._bool);
            }
            set
            {
                Amazon.Util.Internal.InternalSDKUtils.SetIsSet(value, ref this._bool);
            }
        }

        // Check to see if BOOL property is set
        internal bool IsSetBOOL()
        {
            return this.IsBOOLSet; 
        }

        /// <summary>
        /// Gets and sets the property BS. 
        /// <para>
        /// A Binary Set data type.
        /// </para>
        /// </summary>
        public List<MemoryStream> BS
        {
            get { return this._bs; }
            set { this._bs = value; }
        }

        // Check to see if BS property is set
        internal bool IsSetBS()
        {
            return this._bs != null && this._bs.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property L. 
        /// <para>
        /// A List of attribute values.
        /// </para>
        /// </summary>
        public List<AttributeValue> L
        {
            get { return this._l; }
            set { this._l = value; }
        }

        /// <summary>
        /// This property is set to true if the property <seealso cref="L"/>
        /// is set; false otherwise.
        /// This property can be used to determine if the related property
        /// was returned by a service response or if the related property
        /// should be sent to the service during a service call.
        /// </summary>
        /// <returns>
        /// True if the related property was set or will be sent to a service; false otherwise.
        /// </returns>
        public bool IsLSet
        {
            get
            {
                return Amazon.Util.Internal.InternalSDKUtils.GetIsSet(this._l);
            }
            set
            {
                Amazon.Util.Internal.InternalSDKUtils.SetIsSet(value, ref this._l);
            }
        }

        // Check to see if L property is set
        internal bool IsSetL()
        {
            return this.IsLSet; 
        }

        /// <summary>
        /// Gets and sets the property M. 
        /// <para>
        /// A Map of attribute values.
        /// </para>
        /// </summary>
        public Dictionary<string, AttributeValue> M
        {
            get { return this._m; }
            set { this._m = value; }
        }

        /// <summary>
        /// This property is set to true if the property <seealso cref="M"/>
        /// is set; false otherwise.
        /// This property can be used to determine if the related property
        /// was returned by a service response or if the related property
        /// should be sent to the service during a service call.
        /// </summary>
        /// <returns>
        /// True if the related property was set or will be sent to a service; false otherwise.
        /// </returns>
        public bool IsMSet
        {
            get
            {
                return Amazon.Util.Internal.InternalSDKUtils.GetIsSet(this._m);
            }
            set
            {
                Amazon.Util.Internal.InternalSDKUtils.SetIsSet(value, ref this._m);
            }
        }

        // Check to see if M property is set
        internal bool IsSetM()
        {
            return this.IsMSet; 
        }

        /// <summary>
        /// Gets and sets the property N. 
        /// <para>
        /// A Number data type.
        /// </para>
        /// </summary>
        public string N
        {
            get { return this._n; }
            set { this._n = value; }
        }

        // Check to see if N property is set
        internal bool IsSetN()
        {
            return this._n != null;
        }

        /// <summary>
        /// Gets and sets the property NS. 
        /// <para>
        /// A Number Set data type.
        /// </para>
        /// </summary>
        public List<string> NS
        {
            get { return this._ns; }
            set { this._ns = value; }
        }

        // Check to see if NS property is set
        internal bool IsSetNS()
        {
            return this._ns != null && this._ns.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property NULL. 
        /// <para>
        /// A Null data type.
        /// </para>
        /// </summary>
        public bool NULL
        {
            get { return this._null.GetValueOrDefault(); }
            set { this._null = value; }
        }

        // Check to see if NULL property is set
        internal bool IsSetNULL()
        {
            return this._null.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property S. 
        /// <para>
        /// A String data type.
        /// </para>
        /// </summary>
        public string S
        {
            get { return this._s; }
            set { this._s = value; }
        }

        // Check to see if S property is set
        internal bool IsSetS()
        {
            return this._s != null;
        }

        /// <summary>
        /// Gets and sets the property SS. 
        /// <para>
        /// A String Set data type.
        /// </para>
        /// </summary>
        public List<string> SS
        {
            get { return this._ss; }
            set { this._ss = value; }
        }

        // Check to see if SS property is set
        internal bool IsSetSS()
        {
            return this._ss != null && this._ss.Count > 0; 
        }

    }
}