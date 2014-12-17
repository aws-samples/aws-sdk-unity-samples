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
//
//using System;
//
//using System.Collections.Generic;
//
//namespace Amazon.Util
//{
//    /// <summary>
//    /// Root AWS config
//    /// </summary>
//    internal partial class RootConfig
//    {
//    }
//
//    #region Basic sections
//
//    /// <summary>
//    /// S3 settings.
//    /// </summary>
//    public partial class S3Config
//    {
//        internal void Configure(V4ClientSection section)
//        {
//            if (section.ElementInformation.IsPresent)
//                UseSignatureVersion4 = section.UseSignatureVersion4.GetValueOrDefault(false);
//        }
//    }
//
//    /// <summary>
//    /// EC2 settings.
//    /// </summary>
//    public partial class EC2Config
//    {
//        internal void Configure(V4ClientSection section)
//        {
//            if (section.ElementInformation.IsPresent)
//                UseSignatureVersion4 = section.UseSignatureVersion4.GetValueOrDefault(false);
//        }
//    }
//
//    /// <summary>
//    /// Settings for configuring a proxy for the SDK to use.
//    /// </summary>
//    public partial class ProxyConfig
//    {
//        internal void Configure(ProxySection section)
//        {
//            if (section.ElementInformation.IsPresent)
//            {
//                Host = section.Host;
//                Port = section.Port;
//                Username = section.Username;
//                Password = section.Password;
//            }
//        }
//    }
//
//    /// <summary>
//    /// Settings for logging in the SDK.
//    /// </summary>
//    public partial class LoggingConfig
//    {
//        internal void Configure(LoggingSection section)
//        {
//            if (section.ElementInformation.IsPresent)
//            {
//                LogTo = section.LogTo;
//                LogResponses = section.LogResponses;
//                LogMetrics = section.LogMetrics.GetValueOrDefault(false);
//            }
//        }
//    }
//
//    #endregion
//
//    #region DynamoDB sections
//
//    /// <summary>
//    /// DynamoDB settings.
//    /// </summary>
//    public partial class DynamoDBConfig
//    {
//        internal void Configure(DynamoDBSection section)
//        {
//            Context.Configure(section.Context);
//        }
//    }
//
//    /// <summary>
//    /// Settings for DynamoDBContext.
//    /// </summary>
//    public partial class DynamoDBContextConfig
//    {
//        internal void Configure(DynamoDBContextSection section)
//        {
//            if (section.ElementInformation.IsPresent)
//            {
//                TableNamePrefix = section.TableNamePrefix;
//
//                AWSSDKUtils.FillDictionary(section.TypeMappings.Items, t => t.Type, t => new TypeMapping(t), TypeMappings);
//                AWSSDKUtils.FillDictionary(section.TableAliases.Items, t => t.FromTable, t => t.ToTable, TableAliases);
//            }
//        }
//    }
//
//    /// <summary>
//    /// Single DynamoDB table alias
//    /// </summary>
//    public partial class TableAlias
//    {
//        internal TableAlias(TableAliasElement alias)
//            : this(alias.FromTable, alias.ToTable) { }
//    }
//
//    /// <summary>
//    /// Single DynamoDB type mapping config
//    /// </summary>
//    public partial class TypeMapping
//    {
//        internal TypeMapping(TypeMappingElement mapping)
//            : this(mapping.Type, mapping.TargetTable)
//        {
//            AWSSDKUtils.FillDictionary(mapping.PropertyConfigs.Items, p => p.Name, p => new PropertyConfig(p), PropertyConfigs);
//        }
//    }
//
//    /// <summary>
//    /// Single DynamoDB property mapping config
//    /// </summary>
//    public partial class PropertyConfig
//    {
//        internal PropertyConfig(PropertyConfigElement prop)
//            : this(prop.Name)
//        {
//            Attribute = prop.Attribute;
//            Ignore = prop.Ignore.GetValueOrDefault(false);
//            Version = prop.Version.GetValueOrDefault(false);
//            Converter = prop.Converter;
//        }
//    }
//
//    #endregion
//}
