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
#pragma warning disable 1587
#region Header
///
/// IJsonWrapper.cs
///   Interface that represents a type capable of handling all kinds of JSON
///   data. This is mainly used when mapping objects through JsonMapper, and
///   it's implemented by JsonData.
///
/// The authors disclaim copyright to this source code. For more details, see
/// the COPYING file included with this distribution.
///
#endregion


using System.Collections;
using System.Collections.Specialized;

#if (WIN_RT || WINDOWS_PHONE)
using Amazon.MissingTypes;
#endif

namespace ThirdParty.Json.LitJson
{
    public enum JsonType
    {
        None,

        Object,
        Array,
        String,
        Int,
        Long,
        Double,
        Boolean
    }

    public interface IJsonWrapper : IList, IOrderedDictionary
    {
        bool IsArray   { get; }
        bool IsBoolean { get; }
        bool IsDouble  { get; }
        bool IsInt     { get; }
        bool IsLong    { get; }
        bool IsObject  { get; }
        bool IsString  { get; }

        bool     GetBoolean ();
        double   GetDouble ();
        int      GetInt ();
        JsonType GetJsonType ();
        long     GetLong ();
        string   GetString ();

        void SetBoolean  (bool val);
        void SetDouble   (double val);
        void SetInt      (int val);
        void SetJsonType (JsonType type);
        void SetLong     (long val);
        void SetString   (string val);

        string ToJson ();
        void   ToJson (JsonWriter writer);
    }
}
