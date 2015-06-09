#define AWSSDK_UNITY
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
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.ComponentModel;

namespace Amazon.Runtime.Internal.Util
{
    /// <summary>
    /// A single logged message
    /// </summary>
    public class LogMessage
    {
        public object[] Args { get; private set; }
        public IFormatProvider Provider { get; private set; }
        public string Format { get; private set; }

        public LogMessage(string message) : this(CultureInfo.InvariantCulture, message) { }
        public LogMessage(string format, params object[] args) : this(CultureInfo.InvariantCulture, format, args) { }
        public LogMessage(IFormatProvider provider, string format, params object[] args)
        {
            Args = args;
            Format = format;
            Provider = provider;
        }

        public override string ToString()
        {
            return string.Format(Provider, Format, Args);
        }
    }

    /// <summary>
    /// This is a dynamic wrapper around log4net so we can avoid log4net being required
    /// to be distributed with the SDK.
    /// </summary>
    public class Logger : ILogger
    {
        private static IDictionary<Type, Logger> cachedLoggers = new Dictionary<Type, Logger>();
        private List<InternalLogger> loggers;
        private static Logger emptyLogger = new Logger();

        private Logger()
        {
            loggers = new List<InternalLogger>();
        }
        private Logger(Type type)
        {
            loggers = new List<InternalLogger>();

            InternalLog4netLogger log4netLogger = new InternalLog4netLogger(type);
            loggers.Add(log4netLogger);
#if BCL
            InternalSystemDiagnosticsLogger sdLogger = new InternalSystemDiagnosticsLogger(type);
            loggers.Add(sdLogger);
#endif
#if AWSSDK_UNITY
            UnityDebugLogger debugLogger = new UnityDebugLogger(type);
            loggers.Add(debugLogger);
#endif
            ConfigureLoggers();
            AWSConfigs.PropertyChanged += ConfigsChanged;
        }
        private void ConfigsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e != null && string.Equals(e.PropertyName, AWSConfigs.LoggingDestinationProperty, StringComparison.Ordinal))
            {
                ConfigureLoggers();
            }
        }
        private void ConfigureLoggers()
        {
            LoggingOptions logging = AWSConfigs.LoggingConfig.LogTo;
            foreach (InternalLogger il in loggers)
            {
                if (il is InternalLog4netLogger)
                    il.IsEnabled = (logging & LoggingOptions.Log4Net) == LoggingOptions.Log4Net;

#if BCL
                if (il is InternalSystemDiagnosticsLogger)
                    il.IsEnabled = (logging & LoggingOptions.SystemDiagnostics) == LoggingOptions.SystemDiagnostics;
#endif
#if AWSSDK_UNITY
                if (il is UnityDebugLogger)
                    il.IsEnabled = (logging & LoggingOptions.UnityLogger) == LoggingOptions.UnityLogger;
#endif
            }
        }

        #region Static accessor

        public static Logger GetLogger(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            Logger l;
            lock (cachedLoggers)
            {
                if (!cachedLoggers.TryGetValue(type, out l))
                {
                    l = new Logger(type);
                    cachedLoggers[type] = l;
                }
            }
            return l;
        }

        public static void ClearLoggerCache()
        {
            lock (cachedLoggers)
            {
                cachedLoggers = new Dictionary<Type, Logger>();
            }
        }

        public static Logger EmptyLogger { get { return emptyLogger; } }

        #endregion

        #region Logging methods

        public void Flush()
        {
            foreach (InternalLogger logger in loggers)
            {
                logger.Flush();
            }
        }

        public void Error(Exception exception, string messageFormat, params object[] args)
        {
            foreach (InternalLogger logger in loggers)
            {
                if (logger.IsEnabled && logger.IsErrorEnabled)
                    logger.Error(exception, messageFormat, args);
            }
        }

        public void Debug(Exception exception, string messageFormat, params object[] args)
        {
            foreach (InternalLogger logger in loggers)
            {
                if (logger.IsEnabled && logger.IsDebugEnabled)
                    logger.Debug(exception, messageFormat, args);
            }
        }

        public void DebugFormat(string messageFormat, params object[] args)
        {
            foreach (InternalLogger logger in loggers)
            {
                if (logger.IsEnabled && logger.IsDebugEnabled)
                    logger.DebugFormat(messageFormat, args);
            }
        }

        public void InfoFormat(string messageFormat, params object[] args)
        {
            foreach (InternalLogger logger in loggers)
            {
                if (logger.IsEnabled && logger.IsInfoEnabled)
                    logger.InfoFormat(messageFormat, args);
            }
        }

        #endregion

    }

    /// <summary>
    /// Abstract logger class, base for any custom/specific loggers.
    /// </summary>
    internal abstract class InternalLogger
    {
        public Type DeclaringType { get; private set; }

        public bool IsEnabled { get; set; }

        public InternalLogger(Type declaringType)
        {
            DeclaringType = declaringType;
            IsEnabled = true;
        }

        #region Logging methods

        /// <summary>
        /// Flushes the logger contents.
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// Simple wrapper around the log4net IsErrorEnabled property.
        /// </summary>
        public virtual bool IsErrorEnabled { get { return true; } }

        /// <summary>
        /// Simple wrapper around the log4net IsDebugEnabled property.
        /// </summary>
        public virtual bool IsDebugEnabled { get { return true; } }

        /// <summary>
        /// Simple wrapper around the log4net IsInfoEnabled property.
        /// </summary>
        public virtual bool IsInfoEnabled { get { return true; } }

        /// <summary>
        /// Simple wrapper around the log4net Error method.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="messageFormat"></param>
        /// <param name="args"></param>
        public abstract void Error(Exception exception, string messageFormat, params object[] args);

        /// <summary>
        /// Simple wrapper around the log4net Debug method.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="messageFormat"></param>
        /// <param name="args"></param>
        public abstract void Debug(Exception exception, string messageFormat, params object[] args);

        /// <summary>
        /// Simple wrapper around the log4net DebugFormat method.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="arguments"></param>
        public abstract void DebugFormat(string message, params object[] arguments);

        /// <summary>
        /// Simple wrapper around the log4net InfoFormat method.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="arguments"></param>
        public abstract void InfoFormat(string message, params object[] arguments);

        #endregion
    }

}
