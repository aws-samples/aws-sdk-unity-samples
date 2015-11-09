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
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.DynamoDBv2.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Response Unmarshaller for TableDescription Object
    /// </summary>  
    public class TableDescriptionUnmarshaller : IUnmarshaller<TableDescription, XmlUnmarshallerContext>, IUnmarshaller<TableDescription, JsonUnmarshallerContext>
    {
        /// <summary>
        /// Unmarshaller the response from the service to the response class.
        /// </summary>  
        /// <param name="context"></param>
        /// <returns></returns>
        TableDescription IUnmarshaller<TableDescription, XmlUnmarshallerContext>.Unmarshall(XmlUnmarshallerContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unmarshaller the response from the service to the response class.
        /// </summary>  
        /// <param name="context"></param>
        /// <returns></returns>
        public TableDescription Unmarshall(JsonUnmarshallerContext context)
        {
            context.Read();
            if (context.CurrentTokenType == JsonToken.Null) 
                return null;

            TableDescription unmarshalledObject = new TableDescription();
        
            int targetDepth = context.CurrentDepth;
            while (context.ReadAtDepth(targetDepth))
            {
                if (context.TestExpression("AttributeDefinitions", targetDepth))
                {
                    var unmarshaller = new ListUnmarshaller<AttributeDefinition, AttributeDefinitionUnmarshaller>(AttributeDefinitionUnmarshaller.Instance);
                    unmarshalledObject.AttributeDefinitions = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("CreationDateTime", targetDepth))
                {
                    var unmarshaller = DateTimeUnmarshaller.Instance;
                    unmarshalledObject.CreationDateTime = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("GlobalSecondaryIndexes", targetDepth))
                {
                    var unmarshaller = new ListUnmarshaller<GlobalSecondaryIndexDescription, GlobalSecondaryIndexDescriptionUnmarshaller>(GlobalSecondaryIndexDescriptionUnmarshaller.Instance);
                    unmarshalledObject.GlobalSecondaryIndexes = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("ItemCount", targetDepth))
                {
                    var unmarshaller = LongUnmarshaller.Instance;
                    unmarshalledObject.ItemCount = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("KeySchema", targetDepth))
                {
                    var unmarshaller = new ListUnmarshaller<KeySchemaElement, KeySchemaElementUnmarshaller>(KeySchemaElementUnmarshaller.Instance);
                    unmarshalledObject.KeySchema = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("LatestStreamArn", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    unmarshalledObject.LatestStreamArn = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("LatestStreamLabel", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    unmarshalledObject.LatestStreamLabel = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("LocalSecondaryIndexes", targetDepth))
                {
                    var unmarshaller = new ListUnmarshaller<LocalSecondaryIndexDescription, LocalSecondaryIndexDescriptionUnmarshaller>(LocalSecondaryIndexDescriptionUnmarshaller.Instance);
                    unmarshalledObject.LocalSecondaryIndexes = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("ProvisionedThroughput", targetDepth))
                {
                    var unmarshaller = ProvisionedThroughputDescriptionUnmarshaller.Instance;
                    unmarshalledObject.ProvisionedThroughput = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("StreamSpecification", targetDepth))
                {
                    var unmarshaller = StreamSpecificationUnmarshaller.Instance;
                    unmarshalledObject.StreamSpecification = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("TableArn", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    unmarshalledObject.TableArn = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("TableName", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    unmarshalledObject.TableName = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("TableSizeBytes", targetDepth))
                {
                    var unmarshaller = LongUnmarshaller.Instance;
                    unmarshalledObject.TableSizeBytes = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("TableStatus", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    unmarshalledObject.TableStatus = unmarshaller.Unmarshall(context);
                    continue;
                }
            }
          
            return unmarshalledObject;
        }


        private static TableDescriptionUnmarshaller _instance = new TableDescriptionUnmarshaller();        

        /// <summary>
        /// Gets the singleton.
        /// </summary>  
        public static TableDescriptionUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}