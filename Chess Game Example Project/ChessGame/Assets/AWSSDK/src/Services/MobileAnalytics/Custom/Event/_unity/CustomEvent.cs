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
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Collections.ObjectModel;
using Amazon.MobileAnalytics.MobileAnalyticsManager;
using Amazon.Util;
using Amazon.MobileAnalytics.MobileAnalyticsManager.Internal;

namespace Amazon.MobileAnalytics.MobileAnalyticsManager
{
    /// <summary>
    /// Represents any useful event you wish to record in your application.
    /// <example>
    /// The example below shows how to use CustomEvent
    /// <code>
    ///     CustomEvent customEvent = new CustomEvent("level_complete");   
    ///     
    ///     customEvent.AddAttribute("LevelName","Level1");
    ///     customEvent.AddAttribute("Successful","True");
    ///     customEvent.AddMetric("Score",12345);
    ///     customEvent.AddMetric("TimeInLevel",64);   
    ///     
    ///     analyticsManager.RecordEvent(customEvent);
    /// </code>  
    /// </example> 
    /// </summary>
    public class CustomEvent : IEvent, IEventConverter
    {
        protected string _eventType;
        protected static Dictionary<string, Dictionary<string,string>> _eventTypeGlobalAttributes = new Dictionary<string,Dictionary<string,string>>();
        protected static Dictionary<string, Dictionary<string,double>> _eventTypeGlobalMetrics = new Dictionary<string, Dictionary<string,double>>();

        protected static Dictionary<string,string> _globalAttributes = new Dictionary<string,string>();
        protected static Dictionary<string,double> _globalMetrics = new Dictionary<string,double>();
        
        protected Dictionary<string,string> _attributes = new Dictionary<string,string>();
        protected Dictionary<string,double> _metrics = new Dictionary<string,double>();

        public string SessionId {get;set;}
        public string StartTimestamp {get;set;}
        public string StopTimestamp {get;set;}
        public long Duration {get;set;}
        public string Timestamp {get;set;}
        
        protected static Object _globalLock = new Object();
        protected Object _lock = new Object();
        
        private const int MAX_KEY_SIZE = 50;
        private const int MAX_ATTRIB_VALUE_SIZE = 255;
        
        #region constructor
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.CustomEvent"/> class.
        /// </summary>
        public CustomEvent() : this("defaultEventType")
        {
        } 
        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.CustomEvent"/> class.
        /// </summary>
        /// <param name="eventType">Event type.</param>
        public CustomEvent(string eventType)
        {
            this._eventType = eventType;
        } 
        #endregion
        
        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public string EventType 
        {
            get
            {
                return _eventType;
            }
            
            set
            {
                _eventType = value;
            }
        }
        
        /// <summary>
        /// Converts to mobile analytics model event. <see cref="Amazon.MobileAnalytics.Model.Event"/>
        /// </summary>
        /// <returns>The to mobile analytics model event.</returns>
        /// <param name="session">Session.</param>
        public virtual Amazon.MobileAnalytics.Model.Event ConvertToMobileAnalyticsModelEvent(Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.Session session)
        {

            Amazon.MobileAnalytics.Model.Event modelEvent = new Amazon.MobileAnalytics.Model.Event();
            
            // get session info
            string startTimestamp=null,stopTimestamp=null,sessionId=null; long duration=0;
            session.GetSessionInfo(out startTimestamp,out stopTimestamp,out sessionId,out duration);
            
            if(string.IsNullOrEmpty(this.StartTimestamp))
                this.StartTimestamp = startTimestamp;
            if(string.IsNullOrEmpty(this.SessionId))
                this.SessionId = sessionId;
            
            
            
            // assign session info from manager event to model event
            modelEvent.EventType = this._eventType;
            modelEvent.Session = new Amazon.MobileAnalytics.Model.Session();
            modelEvent.Session.Id = this.SessionId;
            modelEvent.Session.StartTimestamp = DateTime.ParseExact(this.StartTimestamp,AWSSDKUtils.ISO8601DateFormat,CultureInfo.InvariantCulture);

            if (_eventType == "_session.stop") 
            {
                modelEvent.Session.StopTimestamp = DateTime.ParseExact(stopTimestamp, AWSSDKUtils.ISO8601DateFormat, CultureInfo.InvariantCulture);
                modelEvent.Session.Duration = this.Duration;
            }
           
            lock(_globalLock)
            {
                AddDict(_globalAttributes,modelEvent.Attributes);
                if(_eventTypeGlobalAttributes.ContainsKey(_eventType))
                {
                    AddDict(_eventTypeGlobalAttributes[_eventType],modelEvent.Attributes);
                }
                
                AddDict(_globalMetrics,modelEvent.Metrics);
                if(_eventTypeGlobalMetrics.ContainsKey(_eventType))
                {
                    AddDict(_eventTypeGlobalMetrics[_eventType],modelEvent.Metrics);
                }
               
            }

            lock(_lock)
            {
                AddDict(_attributes,modelEvent.Attributes);
                AddDict(_metrics,modelEvent.Metrics);
            }

            modelEvent.Timestamp = DateTime.ParseExact(this.Timestamp, AWSSDKUtils.ISO8601DateFormat, CultureInfo.InvariantCulture);
            modelEvent.Version = "v2.0";
            
            return modelEvent;
        }



        #region attribute
        /// <summary>
        /// Adds the attribute.
        /// </summary>
        /// <param name="attributeName">Attribute name. Max name length is 50.</param>
        /// <param name="attributeValue">Attribute value. Max value length is 255.</param>
        public void AddAttribute(string attributeName, string attributeValue)
        {
            if(string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName");
            }
            
            if(attributeName.Length > MAX_KEY_SIZE)
            {
                throw new ArgumentException("length of attributeName " + attributeName+" is more than " + MAX_KEY_SIZE);
            }
            
            if(string.IsNullOrEmpty(attributeValue))
            {
                throw new ArgumentNullException("attributeValue");
            }
            
            if(attributeValue.Length > MAX_ATTRIB_VALUE_SIZE)
            {
                throw new ArgumentException("length of attributeValue is more than " + MAX_ATTRIB_VALUE_SIZE);
            }
            
            lock(_lock)
            {
                _attributes[attributeName] = attributeValue;
            }
        }
        
        /// <summary>
        /// Determines whether this instance has attribute the specified attributeName.
        /// </summary>
        /// <returns>true</returns>
        /// <c>false</c>
        /// <param name="attributeName">Attribute name.</param>
        public bool HasAttribute(string attributeName)
        {
            if(string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName");
            }

            bool ret = false;
            lock(_lock)
            {
                ret = _attributes.ContainsKey("attributeName");
            }
            
            return ret;
        }
        
        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <returns>The attribute. Return null of attribute doesn't exist.</returns>
        /// <param name="attributeName">Attribute name.</param>
        public string GetAttribute(string attributeName)
        {
            if(string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName");
            }
            string ret = null;
            lock(_lock)
            {
                if(attributeName.Contains(attributeName))
                    ret = _attributes[attributeName];
            }
            
            return ret;
        }
        
        /// <summary>
        /// Gets all attributes.
        /// </summary>
        /// <returns>All the attributes.</returns>
        public IDictionary<string, string> GetAllAttributes()
        {
            IDictionary<string,string> ret;
            lock(_lock)
            {
                ret = CopyDict(_attributes);
            }
            return ret;
        }
        #endregion
        
        #region metric
        /// <summary>
        /// Adds the metric.
        /// </summary>
        /// <param name="metricName">Metric name. Max length is 50.</param>
        /// <param name="metricValue">Metric value.</param>
        public void AddMetric(string metricName, double metricValue)
        {
            if(string.IsNullOrEmpty(metricName))
            {
                throw new ArgumentNullException("metricName");
            }
            
            if(metricName.Length > MAX_KEY_SIZE)
            {
                throw new ArgumentException("length of the metricName " + metricName+" is more than " + MAX_KEY_SIZE);
            }

            lock(_lock)
            {
                _metrics[metricName] = metricValue;
            }
            
        }
        
        /// <summary>
        /// Determines whether this instance has metric the specified metricName.
        /// </summary>
        /// <returns>true</returns>
        /// <c>false</c>
        /// <param name="metricName">Metric name.</param>
        public bool HasMetric(string metricName)
        {
            if(string.IsNullOrEmpty(metricName))
            {
                throw new ArgumentNullException("metricName");
            }
            
            bool ret = false;
            
            lock(_lock)
            {
                ret = _metrics.ContainsKey(metricName);
            }
            return ret;
        }
        
        /// <summary>
        /// Gets the metric.
        /// </summary>
        /// <returns>The metric. Return null of metric doesn't exist.</returns>
        /// <param name="metricName">Metric name.</param>
        public double? GetMetric(string metricName)
        {
            if(string.IsNullOrEmpty(metricName))
            {
                throw new ArgumentNullException("metricName");
            }
            
            double? ret = null;
            
            
            lock(_lock)
            {
                if(_metrics.ContainsKey(metricName))
                    ret = _metrics[metricName];
            }
            
            return ret;
        }
        
        
        /// <summary>
        /// Gets all metrics.
        /// </summary>
        /// <returns>All the metrics.</returns>
        public IDictionary<string, double> GetAllMetrics()
        {
            IDictionary<string,double> ret;
            lock(_lock)
            {
                ret = CopyDict(_metrics);
            }
            return ret;
        }
        #endregion
        
        #region gloablattribute
        /// <summary>
        /// Adds the global attribute, which is valid for all events.
        /// </summary>
        /// <param name="attributeName">Attribute name. Max length is 50.</param>
        /// <param name="attributeValue">Attribute value. Max length is 255.</param>
        public void AddGlobalAttribute(string attributeName, string attributeValue)
        {
            if(string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName");
            }
            
            if(string.IsNullOrEmpty(attributeValue))
            {
                throw new ArgumentNullException("attributeValue");
            }
            
            if(attributeName.Length > MAX_KEY_SIZE)
            {
                throw new ArgumentException("length of attributeName " + attributeName+" is more than " + MAX_KEY_SIZE);
            }
            
            if(attributeValue.Length > MAX_ATTRIB_VALUE_SIZE)
            {
                throw new ArgumentException("length of attributeValue is more than " + MAX_ATTRIB_VALUE_SIZE);
            }
            
            lock(_globalLock)
            {
                
                if(!_globalAttributes.ContainsKey(attributeName))
                {
                    _globalAttributes.Add(attributeName,attributeValue);
                }
                    
            }
        }
        
        
        /// <summary>
        /// Adds the global attribute, which is valid to some specific event type.
        /// </summary>
        /// <param name="eventType">Event type.</param>
        /// <param name="attributeName">Attribute name. Max length is 50.</param>
        /// <param name="attributeValue">Attribute value. Max length is 255.</param>
        public void AddGlobalAttribute(string eventType, string attributeName, string attributeValue)
        {
            if(string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException("eventType");
            }
            
            if(string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName");
            }
            
            if(string.IsNullOrEmpty(attributeValue))
            {
                throw new ArgumentNullException("attributeValue");
            }
            
            if(attributeName.Length > MAX_KEY_SIZE)
            {
                throw new ArgumentException("length of attributeName " + attributeName+" is more than " + MAX_KEY_SIZE);
            }
            
            if(attributeValue.Length > MAX_ATTRIB_VALUE_SIZE)
            {
                throw new ArgumentException("length of attributeValue is more than " + MAX_ATTRIB_VALUE_SIZE);
            }
            
            lock(_globalLock)
            {
                if(!_eventTypeGlobalAttributes.ContainsKey(eventType))
                {
                    _eventTypeGlobalAttributes.Add(eventType,new Dictionary<string, string>());
                    _eventTypeGlobalAttributes[eventType].Add(attributeName, attributeValue);
                }
                else if(_eventTypeGlobalAttributes.ContainsKey(eventType) && !_eventTypeGlobalAttributes[eventType].ContainsKey(attributeName))
                {
                    _eventTypeGlobalAttributes[eventType].Add(attributeName, attributeValue);
                }
                
            }
        }
        
        /// <summary>
        /// Removes the global attribute, which is valid for all events.
        /// </summary>
        /// <param name="attributeName">Attribute name.</param>
        public void RemoveGlobalAttribute(string attributeName)
        {
            if(string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName");
            }
            
            lock(_globalLock)
            {
                if(_globalAttributes.ContainsKey(attributeName))
                {
                    _globalAttributes.Remove(attributeName);
                }
            }

        }
        
        /// <summary>
        /// Removes the global attribute, which is valid to some specific event type.
        /// </summary>
        /// <param name="eventType">Event type.</param>
        /// <param name="attributeName">Attribute name.</param>
        public void RemoveGlobalAttribute(string eventType, string attributeName)
        {
            if(string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException("eventType");
            }
            
            if(string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName");
            }
            
            lock(_globalLock)
            {
                if(_eventTypeGlobalAttributes.ContainsKey(eventType) && _eventTypeGlobalAttributes[eventType].ContainsKey(attributeName))
                {
                    _eventTypeGlobalAttributes[eventType].Remove(attributeName);
                }
            }
        }
        
        /// <summary>
        /// Gets the global attribute, which is valid for all events.
        /// </summary>
        /// <returns>The global attribute. Return null if attribute doesn't exist.</returns>
        /// <param name="attributeName">Attribute name.</param>
        public string GetGlobalAttribute(string attributeName)
        {
            if(string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName");
            }
            
            string ret = null;
            lock(_globalLock)
            {
                if(_globalAttributes.ContainsKey(attributeName))
                {
                    ret = _globalAttributes[attributeName];
                }
            }
            return ret;
        }
        
        /// <summary>
        /// Gets the global attribute, which is valid for some specific event type.
        /// </summary>
        /// <returns>The global attribute. Return null if attribute doesn't exist. </returns>
        /// <param name="eventType">Event type.</param>
        /// <param name="attributeName">Attribute name.</param>
        public string GetGlobalAttribute(string eventType, string attributeName)
        {
            if(string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException(eventType);
            }
            if(string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException(attributeName);
            }
            
            string ret = null;
            lock(_globalLock)
            {
                if(_eventTypeGlobalAttributes.ContainsKey(eventType) && _eventTypeGlobalAttributes[eventType].ContainsKey(attributeName))
                {
                    ret = _eventTypeGlobalAttributes[eventType][attributeName];
                }
            }
            return ret;
        }
        #endregion
        
        #region globalmetric
        /// <summary>
        /// Adds the global metric, which is valid for all events.
        /// </summary>
        /// <param name="metricName">Metric name. Max length is 50.</param>
        /// <param name="metricValue">Metric value.</param>
        public void AddGlobalMetric(string metricName, double metricValue)
        {
            if(string.IsNullOrEmpty(metricName))
            {
                throw new ArgumentNullException("metricName");
            }
            
            if(metricName.Length > MAX_KEY_SIZE)
            {
                throw new ArgumentException("length of the metricName " + metricName+" is more than " + MAX_KEY_SIZE);
            }

            lock(_globalLock)
            {
                _globalMetrics[metricName] = metricValue;
            }

        }
        
        /// <summary>
        /// Adds the global metric, which is valid for some specific event type.
        /// </summary>
        /// <param name="eventType">Event type.</param>
        /// <param name="metricName">Metric name. Max length is 50.</param>
        /// <param name="metricValue">Metric value.</param>
        public void AddGlobalMetric(string eventType, string metricName, double metricValue)
        {
            if(string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException("eventType");
            }
            
            if(string.IsNullOrEmpty(metricName))
            {
                throw new ArgumentNullException("metricName");
            }
            
            if(metricName.Length > MAX_KEY_SIZE)
            {
                throw new ArgumentException("length of the metricName " + metricName+" is more than " + MAX_KEY_SIZE);
            }
            
            lock(_globalLock)
            {
                if(!_eventTypeGlobalMetrics.ContainsKey(eventType))
                {
                    _eventTypeGlobalMetrics.Add(eventType,new Dictionary<string, double>());
                    _eventTypeGlobalMetrics[eventType][metricName] = metricValue;
                    return;
                }
                else if(_eventTypeGlobalMetrics.ContainsKey(eventType))
                {
                    _eventTypeGlobalMetrics[eventType][metricName] = metricValue;
                    return;
                }
            }
            
        }
        
        /// <summary>
        /// Removes the global metric, which is valid for all events.
        /// </summary>
        /// <param name="metricName">Metric name.</param>
        public void RemoveGlobalMetric(string metricName)
        {
            if(string.IsNullOrEmpty(metricName))
            {
                throw new ArgumentNullException("metricName");
            }
            
            lock(_globalLock)
            {
                if(_globalMetrics.ContainsKey(metricName))
                {
                    _globalMetrics.Remove(metricName);
                }
            }

        }
        
        /// <summary>
        /// Removes the global metric, which is valid for some specific event type.
        /// </summary>
        /// <param name="eventType">Event type.</param>
        /// <param name="metricName">Metric name.</param>
        public void RemoveGlobalMetric(string eventType,string metricName)
        {
            if(string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException("eventType");
            }
            
            if(string.IsNullOrEmpty(metricName))
            {
                throw new ArgumentNullException("metricName");
            }
            
            lock(_globalLock)
            {
                if(_eventTypeGlobalMetrics.ContainsKey(eventType) && _eventTypeGlobalMetrics[eventType].ContainsKey(metricName))
                {
                    _eventTypeGlobalMetrics[eventType].Remove(metricName);
                }
            }
        }
        
        /// <summary>
        /// Gets the global metric, which is valid for all events.
        /// </summary>
        /// <returns>The global metric. Return null if metric doesn't exist.</returns>
        /// <param name="metricName">Metric name.</param>
        public double? GetGlobalMetric(string metricName)
        {
            if(string.IsNullOrEmpty(metricName))
            {
                throw new ArgumentNullException("metricName");
            }
            
            double? ret = null;
            lock(_globalLock)
            {
                if(_globalMetrics.ContainsKey(metricName))
                    ret = _globalMetrics[metricName];
            }
            return ret;
        }
        
        /// <summary>
        /// Gets the global metric, which is valid for some specific event type.
        /// </summary>
        /// <returns>The global metric. Return null if metric doesn't exist. </returns>
        /// <param name="eventType">Event type.</param>
        /// <param name="metricName">Metric name.</param>
        public double? GetGlobalMetric(string eventType, string metricName)
        {
            if(string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException(eventType);
            }
            
            if(string.IsNullOrEmpty(metricName))
            {
                throw new ArgumentNullException(metricName);
            }
            
            double? ret = null;
            lock(_globalLock)
            {
                if(_eventTypeGlobalMetrics.ContainsKey(eventType) && _eventTypeGlobalMetrics[eventType].ContainsKey(metricName))
                {
                    ret = _eventTypeGlobalMetrics[eventType][metricName];
                }
            }
            return ret;
        }
        #endregion
        
        
        #region util
        private void AddDict<T, S>(Dictionary<T, S> srcDict, Dictionary<T, S> dstDict)
        {
            if(srcDict == null)
            {
                throw new ArgumentNullException("srcDict");
            }
            
            if (dstDict == null)
            {
                throw new ArgumentNullException("dstDict");
            }
            
            foreach (var item in srcDict)
            {
                dstDict[item.Key] = item.Value;
            } 
        }
        
        
        private Dictionary<T, S> CopyDict<T, S>(Dictionary<T, S> srcDict)
        {
            if(srcDict == null)
            {
                throw new ArgumentNullException("srcDict");
            }
            
            Dictionary<T, S> dstDict = new Dictionary<T, S>();
            
            foreach (var item in srcDict)
            {
                dstDict.Add(item.Key, item.Value);
            } 
            return dstDict;
        }

        #endregion
        
        
    }


}



