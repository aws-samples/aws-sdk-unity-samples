/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

#if !WIN_RT
using System.Configuration;
#endif

using Amazon.Util;
using Amazon.Runtime.Internal.Util;

namespace Amazon
{
	    internal partial class RootConfig
	    {
	        public LoggingConfig Logging { get; private set; }
	        public DynamoDBConfig DynamoDB { get; private set; }
	        public S3Config S3 { get; private set; }
	        public EC2Config EC2 { get; private set; }
	        public ProxyConfig Proxy { get; private set; }
	        public string EndpointDefinition { get; set; }
	        public string Region { get; set; }
	        public string ProfileName { get; set; }
	        public string ProfilesLocation { get; set; }
	        public RegionEndpoint RegionEndpoint
	        {
	            get
	            {
	                if (string.IsNullOrEmpty(Region))
	                    return null;
	                return RegionEndpoint.GetBySystemName(Region);
	            }
	            set
	            {
	                if (value == null)
	                    Region = null;
	                else 
	                    Region = value.SystemName;
	            }
	        }
	
	        private const string _rootAwsSectionName = "aws";
	        internal RootConfig()
	        {
	            Logging = new LoggingConfig();
	            DynamoDB = new DynamoDBConfig();
	            S3 = new S3Config();
	            EC2 = new EC2Config();
	            Proxy = new ProxyConfig();
	
	            EndpointDefinition = AWSConfigs._endpointDefinition;
	            Region = AWSConfigs._awsRegion;
	            ProfileName = AWSConfigs._awsProfileName;
	            ProfilesLocation = AWSConfigs._awsAccountsLocation;
	
	#if !WIN_RT && !WINDOWS_PHONE
	            //var root = AWSConfigs.GetSection<AWSSection>(_rootAwsSectionName);
	
//	            Logging.Configure(root.Logging);
//	            DynamoDB.Configure(root.DynamoDB);
//	            S3.Configure(root.S3);
//	            EC2.Configure(root.EC2);
//	            Proxy.Configure(root.Proxy);
//	
//	            EndpointDefinition = Choose(EndpointDefinition, root.EndpointDefinition);
//	            Region = Choose(Region, root.Region);
//	            ProfileName = Choose(ProfileName, root.ProfileName);
//	            ProfilesLocation = Choose(ProfilesLocation, root.ProfilesLocation);
	#endif
	        }
	
	        // If a is not null-or-empty, returns a; otherwise, returns b.
	        private static string Choose(string a, string b)
	        {
	            return (string.IsNullOrEmpty(a) ? b : a);
	        }
	    }



	/// <summary>
    /// Configuration options that apply to the entire SDK.
    /// 
    /// These settings can be configured through app.config or web.config.
    /// Below is a full sample configuration that illustrates all the possible options.
    /// <code>
    /// &lt;configSections&gt;
    ///   &lt;section name="aws" type="Amazon.AWSSection, AWSSDK"/&gt;
    /// &lt;/configSections&gt;
    /// &lt;aws region="us-west-2"&gt;
    ///   &lt;logging logTo="Log4Net, SystemDiagnostics" logResponses="Always" logMetrics="true" /&gt;
    ///   &lt;s3 useSignatureVersion4="true" /&gt;
    ///   &lt;ec2 useSignatureVersion4="false" /&gt;
    ///   &lt;proxy host="localhost" port="8888" username="1" password="1" /&gt;
    ///   
    ///   &lt;dynamoDB&gt;
    ///     &lt;dynamoDBContext tableNamePrefix="Prod-"&gt;
    /// 
    ///       &lt;tableAliases&gt;
    ///         &lt;alias fromTable="FakeTable" toTable="People" /&gt;
    ///         &lt;alias fromTable="Persons" toTable="People" /&gt;
    ///       &lt;/tableAliases&gt;
    /// 
    ///       &lt;mappings&gt;
    ///         &lt;map type="Sample.Tests.Author, SampleDLL" targetTable="People" /&gt;
    ///         &lt;map type="Sample.Tests.Editor, SampleDLL" targetTable="People"&gt;
    ///           &lt;property name="FullName" attribute="Name" /&gt;
    ///           &lt;property name="EmployeeId" attribute="Id" /&gt;
    ///           &lt;property name="ComplexData" converter="Sample.Tests.ComplexDataConverter, SampleDLL" /&gt;
    ///           &lt;property name="Version" version="true" /&gt;
    ///           &lt;property name="Password" ignore="true" /&gt;
    ///         &lt;/map&gt;
    ///       &lt;/mappings&gt;
    /// 
    ///     &lt;/dynamoDBContext&gt;
    ///   &lt;/dynamoDB&gt;
    /// &lt;/aws&gt;
    /// </code>
    /// </summary>
    public static partial class AWSConfigs
    {
        #region Private static members

        private static char[] validSeparators = new char[] { ' ', ',' };

        // Deprecated configs
        internal static string _awsRegion = GetConfig(AWSRegionKey);
        internal static LoggingOptions _logging = GetLoggingSetting();
        internal static ResponseLoggingOption _responseLogging = GetConfigEnum<ResponseLoggingOption>(ResponseLoggingKey);
        internal static bool _logMetrics = GetConfigBool(LogMetricsKey);
        internal static string _endpointDefinition = GetConfig(EndpointDefinitionKey);
        internal static string _dynamoDBContextTableNamePrefix = GetConfig(DynamoDBContextTableNamePrefixKey);
        internal static bool _s3UseSignatureVersion4 = GetConfigBool(S3UseSignatureVersion4Key);
        internal static bool _ec2UseSignatureVersion4 = GetConfigBool(EC2UseSignatureVersion4Key);
        internal static string _awsProfileName = GetConfig(AWSProfileNameKey);
        internal static string _awsAccountsLocation = GetConfig(AWSProfilesLocationKey);

        // New config section
        private static RootConfig _rootConfig = new RootConfig();

        #endregion

        #region Region

        /// <summary>
        /// Key for the AWSRegion property.
        /// <seealso cref="Amazon.AWSConfigs.AWSRegion"/>
        /// </summary>
        public const string AWSRegionKey = "AWSRegion";

        /// <summary>
        /// Configures the default AWS region for clients which have not explicitly specified a region.
        /// Changes to this setting will only take effect for newly constructed instances of AWS clients.
        /// 
        /// This setting can be configured through the App.config. For example:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="aws" type="Amazon.AWSSection, AWSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;aws region="us-west-2" /&gt;
        /// </code>
        /// </summary>
        public static string AWSRegion
        {
            get { return _rootConfig.Region; }
            set { _rootConfig.Region = value; }
        }

        #endregion

        #region Account Name

        /// <summary>
        /// Key for the AWSProfileName property.
        /// <seealso cref="Amazon.AWSConfigs.AWSProfileName"/>
        /// </summary>
        public const string AWSProfileNameKey = "AWSProfileName";

        /// <summary>
        /// Profile name for stored AWS credentials that will be used to make service calls.
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// <para>
        /// To reference the account from an application's App.config or Web.config use the AWSProfileName setting.
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfileName" value="development"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </para>
        /// </summary>
        public static string AWSProfileName
        {
            get { return _rootConfig.ProfileName; }
            set { _rootConfig.ProfileName = value; }
        }

        #endregion

        #region Accounts Location

        /// <summary>
        /// Key for the AWSProfilesLocation property.
        /// <seealso cref="Amazon.AWSConfigs.LogMetrics"/>
        /// </summary>
        public const string AWSProfilesLocationKey = "AWSProfilesLocation";

        /// <summary>
        /// Location of the credentials file shared with other AWS SDKs.
        /// By default, the credentials file is stored in the .aws directory in the current user's home directory.
        /// 
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// <para>
        /// To reference the profile from an application's App.config or Web.config use the AWSProfileName setting.
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfilesLocation" value="c:\config"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </para>
        /// </summary>
        public static string AWSProfilesLocation
        {
            get { return _rootConfig.ProfilesLocation; }
            set { _rootConfig.ProfilesLocation = value; }
        }

        #endregion

        #region Logging

        /// <summary>
        /// Key for the Logging property.
        /// <seealso cref="Amazon.AWSConfigs.Logging"/>
        /// </summary>
        public const string LoggingKey = "AWSLogging";

        /// <summary>
        /// Configures how the SDK should log events, if at all.
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// 
        /// The setting can be configured through App.config, for example:
        /// <code>
        /// &lt;appSettings&gt;
        ///   &lt;add key="AWSLogging" value="log4net"/&gt;
        /// &lt;/appSettings&gt;
        /// </code>
        /// </summary>
        [Obsolete("This property is obsolete. Use LoggingConfig.LogTo instead.")]
        public static LoggingOptions Logging
        {
            get { return _rootConfig.Logging.LogTo; }
            set { _rootConfig.Logging.LogTo = value; }
        }

        private static LoggingOptions GetLoggingSetting()
        {
            string value = GetConfig(LoggingKey);
            if (string.IsNullOrEmpty(value))
                return LoggingOptions.None;

            string[] settings = value.Split(validSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (settings == null || settings.Length == 0)
                return LoggingOptions.None;

            LoggingOptions totalSetting = LoggingOptions.None;
            foreach (string setting in settings)
            {
                LoggingOptions l = ParseEnum<LoggingOptions>(setting);
                totalSetting |= l;
            }
            return totalSetting;
        }

        #endregion

        #region Response Logging

        /// <summary>
        /// Key for the ResponseLogging property.
        /// 
        /// <seealso cref="Amazon.AWSConfigs.ResponseLogging"/>
        /// </summary>
        public const string ResponseLoggingKey = "AWSResponseLogging";

        /// <summary>
        /// Configures when the SDK should log service responses.
        /// Changes to this setting will take effect immediately.
        /// 
        /// The setting can be configured through App.config, for example:
        /// <code>
        /// &lt;appSettings&gt;
        ///   &lt;add key="AWSResponseLogging" value="OnError"/&gt;
        /// &lt;/appSettings&gt;
        /// </code>
        /// </summary>
        [Obsolete("This property is obsolete. Use LoggingConfig.LogResponses instead.")]
        public static ResponseLoggingOption ResponseLogging
        {
            get { return _rootConfig.Logging.LogResponses; }
            set { _rootConfig.Logging.LogResponses = value; }
        }

        #endregion

        #region Log Metrics

        /// <summary>
        /// Key for the LogMetrics property.
        /// <seealso cref="Amazon.AWSConfigs.LogMetrics"/>
        /// </summary>
        public const string LogMetricsKey = "AWSLogMetrics";

        /// <summary>
        /// Configures if the SDK should log performance metrics.
        /// This setting configures the default LogMetrics property for all clients/configs.
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// 
        /// The setting can be configured through App.config, for example:
        /// <code>
        /// &lt;appSettings&gt;
        ///   &lt;add key="AWSLogMetrics" value="true"/&gt;
        /// &lt;/appSettings&gt;
        /// </code>
        /// </summary>
        [Obsolete("This property is obsolete. Use LoggingConfig.LogMetrics instead.")]
        public static bool LogMetrics
        {
            get { return _rootConfig.Logging.LogMetrics; }
            set { _rootConfig.Logging.LogMetrics = value; }
        }

        #endregion

        #region Endpoint Configuration

        /// <summary>
        /// Key for the EndpointDefinition property.
        /// <seealso cref="Amazon.AWSConfigs.LogMetrics"/>
        /// </summary>
        public const string EndpointDefinitionKey = "AWSEndpointDefinition";

        /// <summary>
        /// Configures if the SDK should use a custom configuration file that defines the regions and endpoints.
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="aws" type="Amazon.AWSSection, AWSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;aws endpointDefinition="c:\config\endpoints.xml" /&gt;
        /// </code>
        /// </summary>
        public static string EndpointDefinition
        {
            get { return _rootConfig.EndpointDefinition; }
            set { _rootConfig.EndpointDefinition = value; }
        }

        #endregion

        #region DynamoDBContext TableNamePrefix

        /// <summary>
        /// Key for the DynamoDBContextTableNamePrefix property.
        /// <seealso cref="Amazon.AWSConfigs.DynamoDBContextTableNamePrefix"/>
        /// </summary>
        public const string DynamoDBContextTableNamePrefixKey = "AWS.DynamoDBContext.TableNamePrefix";
        
        /// <summary>
        /// Configures the default TableNamePrefix that the DynamoDBContext will use if
        /// not manually configured.
        /// Changes to this setting will only take effect in newly-constructed instances of
        /// DynamoDBContextConfig and DynamoDBContext.
        /// 
        /// The setting can be configured through App.config, for example:
        /// <code>
        /// &lt;appSettings&gt;
        ///   &lt;add key="AWS.DynamoDBContext.TableNamePrefix" value="Test-"/&gt;
        /// &lt;/appSettings&gt;
        /// </code>
        /// </summary>
        [Obsolete("This property is obsolete. Use DynamoDBConfig.Context.TableNamePrefix instead.")]
        public static string DynamoDBContextTableNamePrefix
        {
            get { return _rootConfig.DynamoDB.Context.TableNamePrefix; }
            set { _rootConfig.DynamoDB.Context.TableNamePrefix = value; }
        }

        #endregion

        #region S3 SignatureV4

        /// <summary>
        /// Key for the S3UseSignatureVersion4 property.
        /// <seealso cref="Amazon.AWSConfigs.S3UseSignatureVersion4"/>
        /// </summary>
        public const string S3UseSignatureVersion4Key = "AWS.S3.UseSignatureVersion4";

        /// <summary>
        /// Configures if the S3 client should use Signature Version 4 signing with requests.
        /// By default, this setting is false, though Signature Version 4 may be used by default
        /// in some cases or with some regions. When the setting is true, Signature Version 4
        /// will be used for all requests.
        /// 
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// 
        /// The setting can be configured through App.config, for example:
        /// <code>
        /// &lt;appSettings&gt;
        ///   &lt;add key="AWS.S3.UseSignatureVersion4" value="true"/&gt;
        /// &lt;/appSettings&gt;
        /// </code>
        /// </summary>
        [Obsolete("This property is obsolete. Use S3Config.UseSignatureVersion4 instead.")]
        public static bool S3UseSignatureVersion4
        {
            get { return _rootConfig.S3.UseSignatureVersion4; }
            set { _rootConfig.S3.UseSignatureVersion4 = value; }
        }

        #endregion

        #region EC2 SignatureV4

        /// <summary>
        /// Key for the EC2UseSignatureVersion4 property.
        /// <seealso cref="Amazon.AWSConfigs.EC2UseSignatureVersion4"/>
        /// </summary>
        public const string EC2UseSignatureVersion4Key = "AWS.EC2.UseSignatureVersion4";

        /// <summary>
        /// Configures if the EC2 client should use Signature Version 4 signing with requests.
        /// By default, this setting is false, though Signature Version 4 may be used by default
        /// in some cases or with some regions. When the setting is true, Signature Version 4
        /// will be used for all requests.
        /// 
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// 
        /// The setting can be configured through App.config, for example:
        /// <code>
        /// &lt;appSettings&gt;
        ///   &lt;add key="AWS.EC2.UseSignatureVersion4" value="true"/&gt;
        /// &lt;/appSettings&gt;
        /// </code>
        /// </summary>
        [Obsolete("This property is obsolete. Use EC2Config.UseSignatureVersion4 instead.")]
        public static bool EC2UseSignatureVersion4
        {
            get { return _rootConfig.EC2.UseSignatureVersion4; }
            set { _rootConfig.EC2.UseSignatureVersion4 = value; }
        }

        #endregion

        #region AWS Config Sections

        /// <summary>
        /// Configuration for the Logging section of AWS configuration.
        /// Changes to some settings may not take effect until a new client is constructed.
        /// 
        /// Example section:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="aws" type="Amazon.AWSSection, AWSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;aws&gt;
        ///   &lt;logging logTo="Log4Net, SystemDiagnostics" logResponses="Always" logMetrics="true" /&gt;
        /// &lt;/aws&gt;
        /// </code>
        /// </summary>
        public static LoggingConfig LoggingConfig { get { return _rootConfig.Logging; } }

        /// <summary>
        /// Configuration for the DynamoDB section of AWS configuration.
        /// Changes to some settings may not take effect until a new client or context is constructed.
        /// 
        /// Example section:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="aws" type="Amazon.AWSSection, AWSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;aws&gt;
        ///   &lt;dynamoDB&gt;
        ///     &lt;dynamoDBContext tableNamePrefix="Prod-"&gt;
        /// 
        ///       &lt;tableAliases&gt;
        ///         &lt;alias fromTable="FakeTable" toTable="People" /&gt;
        ///         &lt;alias fromTable="Persons" toTable="People" /&gt;
        ///       &lt;/tableAliases&gt;
        /// 
        ///       &lt;mappings&gt;
        ///         &lt;map type="Sample.Tests.Author, SampleDLL" targetTable="People" /&gt;
        ///         &lt;map type="Sample.Tests.Editor, SampleDLL" targetTable="People"&gt;
        ///           &lt;property name="FullName" attribute="Name" /&gt;
        ///           &lt;property name="EmployeeId" attribute="Id" /&gt;
        ///           &lt;property name="ComplexData" converter="Sample.Tests.ComplexDataConverter, SampleDLL" /&gt;
        ///           &lt;property name="Version" version="true" /&gt;
        ///           &lt;property name="Password" ignore="true" /&gt;
        ///         &lt;/map&gt;
        ///       &lt;/mappings&gt;
        /// 
        ///     &lt;/dynamoDBContext&gt;
        ///   &lt;/dynamoDB&gt;
        /// &lt;/aws&gt;
        /// </code>
        /// </summary>
        public static DynamoDBConfig DynamoDBConfig { get { return _rootConfig.DynamoDB; } }

        /// <summary>
        /// Configuration for the S3 section of AWS configuration.
        /// Changes to some settings may not take effect until a new client is constructed.
        /// 
        /// Example section:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="aws" type="Amazon.AWSSection, AWSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;aws&gt;
        ///   &lt;s3 useSignatureVersion4="true" /&gt;
        /// &lt;/aws&gt;
        /// </code>
        /// </summary>
        public static S3Config S3Config { get { return _rootConfig.S3; } }

        /// <summary>
        /// Configuration for the EC2 section of AWS configuration.
        /// Changes to some settings may not take effect until a new client is constructed.
        /// 
        /// Example section:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="aws" type="Amazon.AWSSection, AWSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;aws&gt;
        ///   &lt;ec2 useSignatureVersion4="true" /&gt;
        /// &lt;/aws&gt;
        /// </code>
        /// </summary>
        public static EC2Config EC2Config { get { return _rootConfig.EC2; } }

        /// <summary>
        /// Configuration for the Proxy section of AWS configuration.
        /// Changes to some settings may not take effect until a new client is constructed.
        /// 
        /// Example section:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="aws" type="Amazon.AWSSection, AWSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;aws&gt;
        ///   &lt;proxy host="localhost" port="8888" username="1" password="1" /&gt;
        /// &lt;/aws&gt;
        /// </code>
        /// </summary>
        public static ProxyConfig ProxyConfig { get { return _rootConfig.Proxy; } }

        /// <summary>
        /// Configuration for the region endpoint section of AWS configuration.
        /// Changes may not take effect until a new client is constructed.
        /// 
        /// Example section:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="aws" type="Amazon.AWSSection, AWSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;aws region="us-west-2" /&gt;
        /// </code>
        /// </summary>
        public static RegionEndpoint RegionEndpoint
        {
            get { return _rootConfig.RegionEndpoint; }
            set { _rootConfig.RegionEndpoint = value; }
        }

        #endregion

        #region Internal members

        internal static event PropertyChangedEventHandler PropertyChanged;
        internal const string LoggingDestinationProperty = "LogTo";
        internal static void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(null, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region Private general methods

        private static bool GetConfigBool(string name)
        {
            string value = GetConfig(name);
            bool result;
            if (bool.TryParse(value, out result))
                return result;
            return default(bool);
        }

        private static T GetConfigEnum<T>(string name)
        {
            var type = TypeFactory.GetTypeInfo(typeof(T));
            if (!type.IsEnum) throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Type {0} must be enum", type.FullName));

            string value = GetConfig(name);
            if (string.IsNullOrEmpty(value))
                return default(T);
            T result = ParseEnum<T>(value);
            return result;
        }

        private static T ParseEnum<T>(string value)
        {
            T t;
            if (TryParseEnum<T>(value, out t))
                return t;
            Type type = typeof(T);
            string messageFormat = "Unable to parse value {0} as enum of type {1}. Valid values are: {2}";
            string enumNames = string.Join(", ", Enum.GetNames(type));
            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, messageFormat, value, type.FullName, enumNames));
        }

        private static bool TryParseEnum<T>(string value, out T result)
        {
            result = default(T);

            if (string.IsNullOrEmpty(value))
                return false;

            try
            {
                T t = (T)Enum.Parse(typeof(T), value, true);
                result = t;
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        #endregion
    }

    /// <summary>
    /// Logging options.
    /// Can be combined to enable multiple loggers.
    /// </summary>
    [Flags]
    public enum LoggingOptions
    {
        /// <summary>
        /// No logging
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Log using log4net
        /// </summary>
        Log4Net = 1,
        
        /// <summary>
        /// Log using System.Diagnostics
        /// </summary>
        SystemDiagnostics = 2
    }

    /// <summary>
    /// Response logging option.
    /// </summary>
    public enum ResponseLoggingOption
    {
        /// <summary>
        /// Never log service response
        /// </summary>
        Never = 0,

        /// <summary>
        /// Only log service response when there's an error
        /// </summary>
        OnError = 1,

        /// <summary>
        /// Always log service response
        /// </summary>
        Always = 2
    }
}
