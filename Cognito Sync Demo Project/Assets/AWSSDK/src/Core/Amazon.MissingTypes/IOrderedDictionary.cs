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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amazon.MissingTypes
{
    public interface IOrderedDictionary : System.Collections.IDictionary, ICollection, IEnumerable
    {
        // Summary:
        //     Gets or sets the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to get or set.
        //
        // Returns:
        //     The element at the specified index.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than 0.-or- index is equal to or greater than System.Collections.ICollection.Count.
        object this[int index] { get; set; }

        // Summary:
        //     Returns an enumerator that iterates through the System.Collections.Specialized.IOrderedDictionary
        //     collection.
        //
        // Returns:
        //     An System.Collections.IDictionaryEnumerator for the entire System.Collections.Specialized.IOrderedDictionary
        //     collection.
        new IDictionaryEnumerator GetEnumerator();
        //
        // Summary:
        //     Inserts a key/value pair into the collection at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index at which the key/value pair should be inserted.
        //
        //   key:
        //     The object to use as the key of the element to add.
        //
        //   value:
        //     The object to use as the value of the element to add. The value can be null.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than 0.-or-index is greater than System.Collections.ICollection.Count.
        //
        //   System.ArgumentNullException:
        //     key is null.
        //
        //   System.ArgumentException:
        //     An element with the same key already exists in the System.Collections.Specialized.IOrderedDictionary
        //     collection.
        //
        //   System.NotSupportedException:
        //     The System.Collections.Specialized.IOrderedDictionary collection is read-only.-or-The
        //     System.Collections.Specialized.IOrderedDictionary collection has a fixed
        //     size.
        void Insert(int index, object key, object value);
        //
        // Summary:
        //     Removes the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to remove.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than 0.-or- index is equal to or greater than System.Collections.ICollection.Count.
        //
        //   System.NotSupportedException:
        //     The System.Collections.Specialized.IOrderedDictionary collection is read-only.-or-
        //     The System.Collections.Specialized.IOrderedDictionary collection has a fixed
        //     size.
        void RemoveAt(int index);
    }
}
