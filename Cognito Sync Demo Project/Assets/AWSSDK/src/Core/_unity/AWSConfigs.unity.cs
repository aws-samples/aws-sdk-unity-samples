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
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Amazon.Runtime.Internal.Util;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Amazon.Util.Internal;
using System.Reflection;
using System.Threading;
using Amazon.Runtime.Internal;


namespace Amazon
{
    public static partial class AWSConfigs
    {
        private static List<string> standardConfigs = new List<string>() { "region", "logging", "correctForClockSkew" };

        public static string GetConfig(string name)
        {
            return null;
        }

        internal static T GetSection<T>(string sectionName)
            where T : AWSSection, new()
        {
            if (xmlDoc == null)
            {
                lock (_lock)
                {
                    if (xmlDoc == null)
                    {
                        LoadConfigFromResource();
                    }
                }
            }

            XElement sectionElement = xmlDoc.Element(sectionName);

            T t = new T
            {
                Logging = GetObject<LoggingSection>(sectionElement, "logging"),
                Region = sectionElement.Attribute("region")==null?string.Empty:sectionElement.Attribute("region").Value,
                CorrectForClockSkew = bool.Parse(sectionElement.Attribute("correctForClockSkew").Value),
                ServiceSections = GetUnresolvedElements(sectionElement)
            };

            return t;
        }

        internal static bool XmlSectionExists(string sectionName)
        {
            return xmlDoc.Element(sectionName) != null;
        }


        #region tracelistener

        private static Dictionary<string, List<TraceListener>> _traceListeners
           = new Dictionary<string, List<TraceListener>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Add a listener for SDK logging. 
        /// </summary>
        /// <remarks>If the listener does not have a name, you will not be able to remove it later.</remarks>
        /// <param name="source">The source to log for, e.g. "Amazon", or "Amazon.DynamoDB".</param>
        /// <param name="listener">The listener to add.</param>
        public static void AddTraceListener(string source, TraceListener listener)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentException("Source cannot be null or empty", "source");
            if (null == listener)
                throw new ArgumentException("Listener cannot be null", "listener");

            lock (_traceListeners)
            {
                if (!_traceListeners.ContainsKey(source))
                    _traceListeners.Add(source, new List<TraceListener>());
                _traceListeners[source].Add(listener);
            }

        }

        /// <summary>
        /// Remove a trace listener from SDK logging.
        /// </summary>
        /// <param name="source">The source the listener was added to.</param>
        /// <param name="name">The name of the listener.</param>
        public static void RemoveTraceListener(string source, string name)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentException("Source cannot be null or empty", "source");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty", "name");

            lock (_traceListeners)
            {
                if (_traceListeners.ContainsKey(source))
                {
                    foreach (var l in _traceListeners[source])
                    {
                        if (l.Name.Equals(name, StringComparison.Ordinal))
                        {
                            _traceListeners[source].Remove(l);
                            break;
                        }
                    }
                }
            }

            Logger.ClearLoggerCache();
        }

        // Used by Logger.Diagnostic to add listeners to TraceSources when loggers 
        // are created.
        internal static TraceListener[] TraceListeners(string source)
        {
            lock (_traceListeners)
            {
                List<TraceListener> temp;

                if (_traceListeners.TryGetValue(source, out temp))
                {
                    return temp.ToArray();
                }

                return new TraceListener[0];
            }
        }

        #endregion

        #region private methods
        const string CONFIG_FILE = "awsconfig";
        static XDocument xmlDoc = LoadConfigFromResource();
        private static object _lock = new object();

        private static XDocument LoadConfigFromResource()
        {
            XDocument xDoc = null;
            TextAsset awsEndpoints = null;
            
            Action action = () =>
            {
                awsEndpoints = Resources.Load(CONFIG_FILE) as TextAsset;
                
                using (Stream stream = new MemoryStream(awsEndpoints.bytes))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        xDoc = XDocument.Load(reader);
                    }
                }
            };
            
            if(UnityInitializer.IsMainThread())
            {
                action();
            }
            else
            {
                ManualResetEvent e = new ManualResetEvent(false);
                UnityRequestQueue.Instance.ExecuteOnMainThread(() =>
                {
                    action();
                    e.Set();
                });
                
                e.WaitOne();
            }
            return xDoc;
        }

        public static T GetObject<T>(XElement rootElement, string propertyName) where T : class, new()
        {
            return (T)GetObject(rootElement, propertyName, typeof(T));
        }

        private static object GetObject(XElement rootElement, string propertyName, Type type)
        {
            object t = Activator.CreateInstance(type);

            var propertyInfos = t.GetType().GetProperties();

            rootElement = rootElement.Elements(propertyName).Count() == 0 ? 
                rootElement : rootElement.Elements(propertyName).First();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanWrite)
                {
                    var propertyType = propertyInfo.PropertyType;

                    // Try to find a matching attribute
                    var attrib = rootElement.Attributes().SingleOrDefault(
                        a => a.Name.ToString().Equals(propertyInfo.Name, StringComparison.OrdinalIgnoreCase));
                    if (attrib != null)
                    {
                        if (propertyType.BaseType.Equals(typeof(Enum)))
                        {
                            propertyInfo.SetValue(t, Enum.Parse(propertyType, attrib.Value, true), null);
                        }
                        else
                        {
                            // Check if the type is nullable
                            var underlyingType = Nullable.GetUnderlyingType(propertyType);
                            object result;

                            if (underlyingType != null)
                            {
                                result = Convert.ChangeType(attrib.Value, underlyingType);
                            }
                            else
                            {
                                // Converters for types which Convert.ChangeType
                                // can't handle.
                                if (propertyType == typeof(Type))
                                {
                                    result = Type.GetType(attrib.Value, true);
                                }
                                else
                                {
                                    result = Convert.ChangeType(attrib.Value, propertyType);
                                }
                            }
                            propertyInfo.SetValue(t, result, null);
                        }
                        continue;
                    }

                    // Try to find a matching child element if a matching attribute was not found
                    var element = rootElement.Elements().SingleOrDefault(
                        e => e.Name.ToString().Equals(propertyInfo.Name, StringComparison.OrdinalIgnoreCase));

                    // Process lists
                    if (typeof(IList).IsAssignableFrom(propertyType))
                    {
                        // Pass rootElement instead of element so that
                        // we can support XML lists items where the enclosing tag
                        // is not present
                        var listPropertyName = element == null ? null : element.Name.ToString();
                        var result = GetList(rootElement, propertyType, listPropertyName);
                        propertyInfo.SetValue(t, result, null);

                        continue;
                    }

                    // Process complex child elements
                    if (element != null)
                    {
                        var result = GetObject(element, element.Name.ToString(), propertyType);
                        propertyInfo.SetValue(t, result, null);
                    }
                }
            }

            return t;
        }

        private static IEnumerable GetList(XElement rootElement, Type listType, string propertyName)
        {
            var list = (IList)Activator.CreateInstance(listType);
            
            var itemNamePropInfo = listType.GetProperty("ItemPropertyName");
            var itemName = (string)itemNamePropInfo.GetValue(list, null);
            var itemType = listType.GetProperty("Item").PropertyType;

            if (!string.IsNullOrEmpty(propertyName))
            {
                rootElement = rootElement.Elements(propertyName).Count() == 0 ?
                rootElement : rootElement.Elements(propertyName).First();
            }        

            // Process list items
            foreach (var childElement in rootElement.Elements())
            {
                var item = GetObject(childElement, itemName, itemType);
                list.Add(item);
            }

            return list;
        }

        private static IDictionary<string, XElement> GetUnresolvedElements(XElement parent)
        {
            IDictionary<string, XElement> unresolvedElemets = new Dictionary<string, XElement>();

            foreach (XElement element in parent.Elements())
            {
                if (!standardConfigs.Contains(element.Name.ToString()))
                    unresolvedElemets.Add(element.Name.ToString(), element);
            }

            return unresolvedElemets;
        }

        #endregion

    }
}

namespace Amazon.Util.Internal
{
    public abstract class ConfigurationElement
    {
        public ConfigurationElement()
        {
            this.ElementInformation = new ElementInformation(true);
        }

        public ElementInformation ElementInformation { get; set; }
    }

    public class ElementInformation
    {
        public ElementInformation(bool isPresent)
        {
            this.IsPresent = isPresent;
        }

        public bool IsPresent { get; private set; }
    }

    public abstract class ConfigurationList<T> : List<T>
    {
        public IEnumerable<T> Items { get { return this; } }
    }
}
