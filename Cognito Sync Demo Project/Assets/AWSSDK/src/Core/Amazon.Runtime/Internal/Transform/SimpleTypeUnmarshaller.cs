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
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.Runtime.Internal.Transform
{
    static class SimpleTypeUnmarshaller<T>
    {
        public static T Unmarshall(XmlUnmarshallerContext context)
        {
            string text = context.ReadText();
            return (T)Convert.ChangeType(text, typeof(T), CultureInfo.InvariantCulture);
        }

        public static T Unmarshall(JsonUnmarshallerContext context)
        {
            context.Read();
            string text = context.ReadText();
            if (text == null)
                return default(T);

            return (T)Convert.ChangeType(text, typeof(T), CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    /// Unmarshaller for int fields
    /// </summary>
    public class IntUnmarshaller : IUnmarshaller<int, XmlUnmarshallerContext>, IUnmarshaller<int, JsonUnmarshallerContext>
    {        
        private IntUnmarshaller() { }

        private static IntUnmarshaller _instance = new IntUnmarshaller();

        public static IntUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

        public static IntUnmarshaller GetInstance()
        {
            return IntUnmarshaller.Instance;
        }

        public int Unmarshall(XmlUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<int>.Unmarshall(context);
        }
        public int Unmarshall(JsonUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<int>.Unmarshall(context);
        }
    }

    /// <summary>
    /// Unmarshaller for long fields
    /// </summary>
    public class LongUnmarshaller : IUnmarshaller<long, XmlUnmarshallerContext>, IUnmarshaller<long, JsonUnmarshallerContext>
    {        
        private LongUnmarshaller() { }

        private static LongUnmarshaller _instance = new LongUnmarshaller();

        public static LongUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

        public static LongUnmarshaller GetInstance()
        {
            return LongUnmarshaller.Instance;
        }

        public long Unmarshall(XmlUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<long>.Unmarshall(context);
        }
        public long Unmarshall(JsonUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<long>.Unmarshall(context);
        }
    }

    /// <summary>
    /// Unmarshaller for float fields
    /// </summary>
    public class FloatUnmarshaller : IUnmarshaller<float, XmlUnmarshallerContext>, IUnmarshaller<float, JsonUnmarshallerContext>
    {        
        private FloatUnmarshaller() { }

        private static FloatUnmarshaller _instance = new FloatUnmarshaller();

        public static FloatUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

        public static FloatUnmarshaller GetInstance()
        {
            return FloatUnmarshaller.Instance;
        }

        public float Unmarshall(XmlUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<float>.Unmarshall(context);
        }
        public float Unmarshall(JsonUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<float>.Unmarshall(context);
        }
    }

    /// <summary>
    /// Unmarshaller for double fields
    /// </summary>
    public class DoubleUnmarshaller : IUnmarshaller<double, XmlUnmarshallerContext>, IUnmarshaller<double, JsonUnmarshallerContext>
    {        
        private DoubleUnmarshaller() { }

        private static DoubleUnmarshaller _instance = new DoubleUnmarshaller();

        public static DoubleUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }


        public static DoubleUnmarshaller GetInstance()
        {
            return DoubleUnmarshaller.Instance;
        }

        public double Unmarshall(XmlUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<double>.Unmarshall(context);
        }
        public double Unmarshall(JsonUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<double>.Unmarshall(context);
        }
    }

    /// <summary>
    /// Unmarshaller for bool fields
    /// </summary>
    public class BoolUnmarshaller : IUnmarshaller<bool, XmlUnmarshallerContext>, IUnmarshaller<bool, JsonUnmarshallerContext>
    {        
        private BoolUnmarshaller() { }

        private static BoolUnmarshaller _instance = new BoolUnmarshaller();

        public static BoolUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

        public static BoolUnmarshaller GetInstance()
        {
            return BoolUnmarshaller.Instance;
        }

        public bool Unmarshall(XmlUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<bool>.Unmarshall(context);
        }
        public bool Unmarshall(JsonUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<bool>.Unmarshall(context);
        }
    }

    /// <summary>
    /// Unmarshaller for string fields
    /// </summary>
    public class StringUnmarshaller : IUnmarshaller<string, XmlUnmarshallerContext>, IUnmarshaller<string, JsonUnmarshallerContext>
    {        
        private StringUnmarshaller() { }

        private static StringUnmarshaller _instance = new StringUnmarshaller();

        public static StringUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

        public static StringUnmarshaller GetInstance()
        {
            return StringUnmarshaller.Instance;
        }

        public string Unmarshall(XmlUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<string>.Unmarshall(context);
        }
        public string Unmarshall(JsonUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<string>.Unmarshall(context);
        }
    }

    /// <summary>
    /// Unmarshaller for byte fields
    /// </summary>
    public class ByteUnmarshaller : IUnmarshaller<byte, XmlUnmarshallerContext>, IUnmarshaller<byte, JsonUnmarshallerContext>
    {        
        private ByteUnmarshaller() { }

        private static ByteUnmarshaller _instance = new ByteUnmarshaller();

        public static ByteUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

        public static ByteUnmarshaller GetInstance()
        {
            return ByteUnmarshaller.Instance;
        }

        public byte Unmarshall(XmlUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<byte>.Unmarshall(context);
        }
        public byte Unmarshall(JsonUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<byte>.Unmarshall(context);
        }
    }

    /// <summary>
    /// Unmarshaller for DateTime fields
    /// </summary>
    public class DateTimeUnmarshaller : IUnmarshaller<DateTime, XmlUnmarshallerContext>, IUnmarshaller<DateTime, JsonUnmarshallerContext>
    {        
        private DateTimeUnmarshaller() { }

        private static DateTimeUnmarshaller _instance = new DateTimeUnmarshaller();

        public static DateTimeUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

        public static DateTimeUnmarshaller GetInstance()
        {
            return DateTimeUnmarshaller.Instance;
        }

        public DateTime Unmarshall(XmlUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<DateTime>.Unmarshall(context);
        }
        public DateTime Unmarshall(JsonUnmarshallerContext context)
        {
            DateTime ret;
            Double seconds;
            context.Read();
            string text = context.ReadText();
            if (Double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out seconds))
            {
                ret = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                ret = ret.AddSeconds(seconds);
            }
            else
            {
                if (text == null)
                    return default(DateTime);

                return (DateTime)Convert.ChangeType(text, typeof(DateTime), CultureInfo.InvariantCulture);
            }
            return ret;
        }
    }

    public class DateTimeEpochLongMillisecondsUnmarshaller : IUnmarshaller<DateTime, XmlUnmarshallerContext>, IUnmarshaller<DateTime, JsonUnmarshallerContext>
    {
        private DateTimeEpochLongMillisecondsUnmarshaller() { }

        private static DateTimeEpochLongMillisecondsUnmarshaller _instance = new DateTimeEpochLongMillisecondsUnmarshaller();

        public static DateTimeEpochLongMillisecondsUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

        public static DateTimeEpochLongMillisecondsUnmarshaller GetInstance()
        {
            return DateTimeEpochLongMillisecondsUnmarshaller.Instance;
        }

        public DateTime Unmarshall(XmlUnmarshallerContext context)
        {
            return SimpleTypeUnmarshaller<DateTime>.Unmarshall(context);
        }
        public DateTime Unmarshall(JsonUnmarshallerContext context)
        {
            long millseconds = LongUnmarshaller.Instance.Unmarshall(context);
            var ret = Amazon.Util.AWSSDKUtils.EPOCH_START.AddMilliseconds(millseconds);
            return ret;
        }
    }

    /// <summary>
    /// Unmarshaller for MemoryStream fields
    /// </summary>
    public class MemoryStreamUnmarshaller : IUnmarshaller<MemoryStream, XmlUnmarshallerContext>, IUnmarshaller<MemoryStream, JsonUnmarshallerContext>
    {        
        private MemoryStreamUnmarshaller() { }

        private static MemoryStreamUnmarshaller _instance = new MemoryStreamUnmarshaller();

        public static MemoryStreamUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

        public static MemoryStreamUnmarshaller GetInstance()
        {
            return MemoryStreamUnmarshaller.Instance;
        }

        public MemoryStream Unmarshall(XmlUnmarshallerContext context)
        {
            byte[] bytes = Convert.FromBase64String(context.ReadText());
            MemoryStream stream = new MemoryStream(bytes);
            return stream;
        }

        public MemoryStream Unmarshall(JsonUnmarshallerContext context)
        {
            context.Read();
            byte[] bytes = Convert.FromBase64String(context.ReadText());
            MemoryStream stream = new MemoryStream(bytes);
            return stream;
        }
    }

    /// <summary>
    /// Unmarshaller for ResponseMetadata
    /// </summary>
    public class ResponseMetadataUnmarshaller : IUnmarshaller<ResponseMetadata, XmlUnmarshallerContext>, IUnmarshaller<ResponseMetadata, JsonUnmarshallerContext>
    {        
        private ResponseMetadataUnmarshaller() { }

        private static ResponseMetadataUnmarshaller _instance = new ResponseMetadataUnmarshaller();

        public static ResponseMetadataUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

        public static ResponseMetadataUnmarshaller GetInstance()
        {
            return ResponseMetadataUnmarshaller.Instance;
        }

        public ResponseMetadata Unmarshall(XmlUnmarshallerContext context)
        {
            ResponseMetadata metadata = new ResponseMetadata();

            int depth = context.CurrentDepth;

            while (depth <= context.CurrentDepth)
            {
                context.Read();
                if (context.IsStartElement)
                {
                    if (context.TestExpression("ResponseMetadata/RequestId"))
                        metadata.RequestId = StringUnmarshaller.GetInstance().Unmarshall(context);
                    else
                        metadata.Metadata.Add(context.CurrentPath.Substring(context.CurrentPath.LastIndexOf('/') + 1), StringUnmarshaller.GetInstance().Unmarshall(context));
                }
            }

            return metadata;
        }
        public ResponseMetadata Unmarshall(JsonUnmarshallerContext context)
        {
            ResponseMetadata metadata = new ResponseMetadata();
            int depth = context.CurrentDepth;

            while (context.CurrentDepth >= depth)
            {
                context.Read();
                if (context.TestExpression("ResponseMetadata/RequestId"))
                {
                    metadata.RequestId = StringUnmarshaller.GetInstance().Unmarshall(context);
                }
            }

            return metadata;
        }
    }

    public class KeyValueUnmarshaller<K, V, KUnmarshaller, VUnmarshaller> :
    IUnmarshaller<KeyValuePair<K, V>, XmlUnmarshallerContext>, 
    IUnmarshaller<KeyValuePair<K, V>, JsonUnmarshallerContext>
        where KUnmarshaller : IUnmarshaller<K, XmlUnmarshallerContext>, IUnmarshaller<K, JsonUnmarshallerContext>
        where VUnmarshaller : IUnmarshaller<V, XmlUnmarshallerContext>, IUnmarshaller<V, JsonUnmarshallerContext>
    {
        private KUnmarshaller keyUnmarshaller;
        private VUnmarshaller valueUnmarshaller;

        public KeyValueUnmarshaller(KUnmarshaller keyUnmarshaller, VUnmarshaller valueUnmarshaller)
        {
            this.keyUnmarshaller = keyUnmarshaller;
            this.valueUnmarshaller = valueUnmarshaller;
        }

        public KeyValuePair<K, V> Unmarshall(XmlUnmarshallerContext context)
        {
            K key = default(K);
            V value = default(V);

            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            while (context.Read())
            {
                if (context.TestExpression("key", targetDepth))
                {
                    key = this.keyUnmarshaller.Unmarshall(context);
                }
                else if (context.TestExpression("name", targetDepth))
                {
                    key = this.keyUnmarshaller.Unmarshall(context);
                }
                else if (context.TestExpression("value", targetDepth))
                {
                    value = this.valueUnmarshaller.Unmarshall(context);
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    break;
                }
            }

            return new KeyValuePair<K, V>(key, value);
        }

        public KeyValuePair<K, V> Unmarshall(JsonUnmarshallerContext context)
        {
            K key = this.keyUnmarshaller.Unmarshall(context);
            V value = this.valueUnmarshaller.Unmarshall(context);

            return new KeyValuePair<K, V>(key, value);
        }
    }

    public class ListUnmarshaller<I, IUnmarshaller> : IUnmarshaller<List<I>, XmlUnmarshallerContext>, IUnmarshaller<List<I>, JsonUnmarshallerContext>
        where IUnmarshaller : IUnmarshaller<I, XmlUnmarshallerContext>, IUnmarshaller<I, JsonUnmarshallerContext>
    {
        private IUnmarshaller iUnmarshaller;

        public ListUnmarshaller(IUnmarshaller iUnmarshaller)
        {
            this.iUnmarshaller = iUnmarshaller;
        }

        public List<I> Unmarshall(XmlUnmarshallerContext context)
        {
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            var list = new List<I>();
            while (context.Read())
            {
                if (context.IsStartElement)
                {
                    if (context.TestExpression("member", targetDepth))
                    {
                        var item = iUnmarshaller.Unmarshall(context);
                        list.Add(item);
                    }
                }
            }
            return list;
        }
        public List<I> Unmarshall(JsonUnmarshallerContext context)
        {
            context.Read(); // Read [ or null
            if (context.CurrentTokenType == JsonToken.Null)
                return new List<I>();

            // If a list is present in the response, use AlwaysSendList,
            // so if the response was empty, reusing the object in the request we will
            // end up sending the same empty collection back.
            List<I> list = new AlwaysSendList<I>();
            while (!context.Peek(JsonToken.ArrayEnd)) // Peek for ]
            {
                list.Add(iUnmarshaller.Unmarshall(context));
            }
            context.Read(); // Read ]
            return list;
        }
    }

    public class DictionaryUnmarshaller<K, V, KUnmarshaller, VUnmarshaller> : IUnmarshaller<Dictionary<K, V>, XmlUnmarshallerContext>, IUnmarshaller<Dictionary<K, V>, JsonUnmarshallerContext>
        where KUnmarshaller : IUnmarshaller<K, XmlUnmarshallerContext>, IUnmarshaller<K, JsonUnmarshallerContext>
        where VUnmarshaller : IUnmarshaller<V, XmlUnmarshallerContext>, IUnmarshaller<V, JsonUnmarshallerContext>
    {
        private KeyValueUnmarshaller<K, V, KUnmarshaller, VUnmarshaller> KVUnmarshaller;

        public DictionaryUnmarshaller(KUnmarshaller kUnmarshaller, VUnmarshaller vUnmarshaller)
        {
            KVUnmarshaller = new KeyValueUnmarshaller<K, V, KUnmarshaller, VUnmarshaller>(kUnmarshaller, vUnmarshaller);
        }

        Dictionary<K, V> IUnmarshaller<Dictionary<K, V>, XmlUnmarshallerContext>.Unmarshall(XmlUnmarshallerContext context)
        {
            throw new NotImplementedException();
        }
        public Dictionary<K, V> Unmarshall(JsonUnmarshallerContext context)
        {
            context.Read(); // Read { or null
            if (context.CurrentTokenType == JsonToken.Null)
                return new Dictionary<K,V>();

            // If a dictionary is present in the response, use AlwaysSendDictionary,
            // so if the response was empty, reusing the object in the request we will
            // end up sending the same empty collection back.
            Dictionary<K, V> dictionary = new AlwaysSendDictionary<K, V>();
            while (!context.Peek(JsonToken.ObjectEnd)) // Peek }
            {
                KeyValuePair<K, V> item = KVUnmarshaller.Unmarshall(context);
                dictionary.Add(item.Key, item.Value);
            }
            context.Read(); // Read }
            return dictionary;

        }
    }

    public static class UnmarshallerExtensions
    {
        public static void Add<K, V>(this Dictionary<K, V> dict, KeyValuePair<K, V> item)
        {
            dict.Add(item.Key, item.Value);
        }
        //public static void Add<T>(this List<T> list, List<T> items)
        //{
        //    list.AddRange(items);
        //}
    }
}
