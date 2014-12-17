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

namespace Amazon.Util
{
    /// <summary>
    /// Root AWS config
    /// </summary>
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
//            var root = AWSConfigs.GetSection<AWSSection>(_rootAwsSectionName);
//
//            Logging.Configure(root.Logging);
//            DynamoDB.Configure(root.DynamoDB);
//            S3.Configure(root.S3);
//            EC2.Configure(root.EC2);
//            Proxy.Configure(root.Proxy);
//
//            EndpointDefinition = Choose(EndpointDefinition, root.EndpointDefinition);
//            Region = Choose(Region, root.Region);
//            ProfileName = Choose(ProfileName, root.ProfileName);
//            ProfilesLocation = Choose(ProfilesLocation, root.ProfilesLocation);
#endif
        }

        // If a is not null-or-empty, returns a; otherwise, returns b.
        private static string Choose(string a, string b)
        {
            return (string.IsNullOrEmpty(a) ? b : a);
        }
    }

    #region Basic sections

    /// <summary>
    /// S3 settings.
    /// </summary>
    public partial class S3Config
    {
        /// <summary>
        /// Configures if the S3 client should use Signature Version 4 signing with requests.
        /// By default, this setting is false, though Signature Version 4 may be used by default
        /// in some cases or with some regions. When the setting is true, Signature Version 4
        /// will be used for all requests.
        /// </summary>
        public bool UseSignatureVersion4 { get; set; }

        internal S3Config()
        {
            UseSignatureVersion4 = AWSConfigs._s3UseSignatureVersion4;
        }
    }

    /// <summary>
    /// EC2 settings.
    /// </summary>
    public partial class EC2Config
    {
        /// <summary>
        /// Configures if the EC2 client should use Signature Version 4 signing with requests.
        /// By default, this setting is false, though Signature Version 4 may be used by default
        /// in some cases or with some regions. When the setting is true, Signature Version 4
        /// will be used for all requests.
        /// </summary>
        public bool UseSignatureVersion4 { get; set; }

        internal EC2Config()
        {
            UseSignatureVersion4 = AWSConfigs._ec2UseSignatureVersion4;
        }
    }

    /// <summary>
    /// Settings for configuring a proxy for the SDK to use.
    /// </summary>
    public partial class ProxyConfig
    {
        /// <summary>
        /// The host name or IP address of the proxy server.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The port number of the proxy.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// The username to authenticate with the proxy server.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password to authenticate with the proxy server.
        /// </summary>
        public string Password { get; set; }


        internal ProxyConfig()
        {
        }
    }

    /// <summary>
    /// Settings for logging in the SDK.
    /// </summary>
    public partial class LoggingConfig
    {
        private LoggingOptions _logTo;

        /// <summary>
        /// Logging destination.
        /// </summary>
        public LoggingOptions LogTo
        {
            get { return _logTo; }
            set
            {
                _logTo = value;
                AWSConfigs.OnPropertyChanged(AWSConfigs.LoggingDestinationProperty);
            }
        }
        /// <summary>
        /// When to log responses.
        /// </summary>
        public ResponseLoggingOption LogResponses { get; set; }
        /// <summary>
        /// Whether or not to log SDK metrics.
        /// </summary>
        public bool LogMetrics { get; set; }

        internal LoggingConfig()
        {
            LogTo = AWSConfigs._logging;
            LogResponses = AWSConfigs._responseLogging;
            LogMetrics = AWSConfigs._logMetrics;
        }
    }

    #endregion

    #region DynamoDB sections

    /// <summary>
    /// DynamoDB settings.
    /// </summary>
    public partial class DynamoDBConfig
    {
        /// <summary>
        /// Settings for DynamoDBContext.
        /// </summary>
        public DynamoDBContextConfig Context { get; private set; }

        internal DynamoDBConfig()
        {
            Context = new DynamoDBContextConfig();
        }
    }

    /// <summary>
    /// Settings for DynamoDBContext.
    /// </summary>
    public partial class DynamoDBContextConfig
    {
        /// <summary>
        /// Configures the default TableNamePrefix that the DynamoDBContext will use if
        /// not manually configured.
        /// 
        /// TableNamePrefix is used after TableAliases have been applied.
        /// </summary>
        public string TableNamePrefix { get; set; }

        /// <summary>
        /// A string-to-string dictionary (From-Table to To-Table) used by DynamoDBContext to
        /// use a different table from one that is configured for a type.
        /// 
        /// Remapping is done before applying TableNamePrefix.
        /// </summary>
        public Dictionary<string, string> TableAliases { get; private set; }

        /// <summary>
        /// A Type-to-TypeMapping (type to TypeMapping defining its DynamoDB conversion) used by
        /// DynamoDBContext to modify or configure a particular type.
        /// </summary>
        public Dictionary<Type, TypeMapping> TypeMappings { get; private set; }

        /// <summary>
        /// Adds a TableAlias to the TableAliases property.
        /// An exception is thrown if there is already a TableAlias with the same FromTable configured.
        /// </summary>
        /// <param name="tableAlias"></param>
        public void AddAlias(TableAlias tableAlias)
        {
            TableAliases.Add(tableAlias.FromTable, tableAlias.ToTable);
        }

        /// <summary>
        /// Adds a TypeMapping to the TypeMappings property.
        /// An exception is thrown if there is already a TypeMapping with the same Type configured.
        /// </summary>
        /// <param name="typeMapping"></param>
        public void AddMapping(TypeMapping typeMapping)
        {
            TypeMappings.Add(typeMapping.Type, typeMapping);
        }

        internal DynamoDBContextConfig()
        {
            TableNamePrefix = AWSConfigs._dynamoDBContextTableNamePrefix;
            TableAliases = new Dictionary<string, string>(StringComparer.Ordinal);
            TypeMappings = new Dictionary<Type, TypeMapping>();
        }
    }

    /// <summary>
    /// Single DynamoDB table alias
    /// </summary>
    public partial class TableAlias
    {
        /// <summary>
        /// Source table
        /// </summary>
        public string FromTable { get; set; }

        /// <summary>
        /// Destination table
        /// </summary>
        public string ToTable { get; set; }

        /// <summary>
        /// Initializes an empty TableAlias object
        /// </summary>
        public TableAlias() { }

        /// <summary>
        /// Initializes a TableAlias object with specific source and
        /// destination tables
        /// </summary>
        /// <param name="fromTable">Source table</param>
        /// <param name="toTable">Destination table</param>
        public TableAlias(string fromTable, string toTable)
        {
            FromTable = fromTable;
            ToTable = toTable;
        }
    }

    /// <summary>
    /// Single DynamoDB type mapping config
    /// </summary>
    public partial class TypeMapping
    {
        /// <summary>
        /// Type to which the mapping applies
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Target table for the type
        /// </summary>
        public string TargetTable { get; set; }

        /// <summary>
        /// A string-to-PropertyConfig dictionary (property name to PropertyConfig) describing
        /// how each property on the type should be treated.
        /// </summary>
        public Dictionary<string, PropertyConfig> PropertyConfigs { get; private set; }

        /// <summary>
        /// Adds a PropertyConfig to the PropertyConfigs property.
        /// An exception is thrown if there is already a PropertyConfig with the same Name configured.
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void AddProperty(PropertyConfig propertyConfig)
        {
            PropertyConfigs.Add(propertyConfig.Name, propertyConfig);
        }

        /// <summary>
        /// Initializes a TypeMapping object for a specific type and target table.
        /// </summary>
        /// <param name="type">Target type</param>
        /// <param name="targetTable">Target table</param>
        public TypeMapping(Type type, string targetTable)
        {
            Type = type;
            TargetTable = targetTable;
            PropertyConfigs = new Dictionary<string, PropertyConfig>(StringComparer.Ordinal);
        }
    }

    /// <summary>
    /// Single DynamoDB property mapping config
    /// </summary>
    public partial class PropertyConfig
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Attribute name
        /// </summary>
        public string Attribute { get; set; }

        /// <summary>
        /// Whether this property should be ignored by DynamoDBContext
        /// </summary>
        public bool Ignore { get; set; }

        /// <summary>
        /// Whether this property should be treated as a version property
        /// </summary>
        public bool Version { get; set; }

        /// <summary>
        /// The type of converter that should be used on this property
        /// </summary>
        public Type Converter { get; set; }

        /// <summary>
        /// Initializes a PropertyConfig object for a specific property
        /// </summary>
        /// <param name="name"></param>
        public PropertyConfig(string name)
        {
            Name = name;
        }
    }

    #endregion
}
