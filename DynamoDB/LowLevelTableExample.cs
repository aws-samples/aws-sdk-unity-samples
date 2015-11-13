//
// Copyright 2014-2015 Amazon.com, 
// Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the AWS Mobile SDK For Unity 
// Sample Application License Agreement (the "License"). 
// You may not use this file except in compliance with the 
// License. A copy of the License is located 
// in the "license" file accompanying this file. This file is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, express or implied. See the License 
// for the specific language governing permissions and 
// limitations under the License.
//

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System;

namespace AWSSDK.Examples
{
    public class LowLevelTableExample : DynamoDbBaseExample
    {

        public Button backButton;
        public Button createTableButton;
        public Button listTableButton;
        public Button updateTableButton;
        public Button describeTableButton;
        public Button deleteTableButton;
        public Text resultText;

        // Use this for initialization
        void Start()
        {
            backButton.onClick.AddListener(BackListener);
            createTableButton.onClick.AddListener(CreateTableListener);
            listTableButton.onClick.AddListener(ListTableListener);
            updateTableButton.onClick.AddListener(UpdateTableListener);
            describeTableButton.onClick.AddListener(DescribeTableListener);
            deleteTableButton.onClick.AddListener(DeleteTableListener);
        }

        void CreateTableListener()
        {
            resultText.text = @"\n Creating table";

            var productCatalogTableRequest = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                            AttributeName = "Id",
                            AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                            AttributeName = "Id",
                            KeyType = "HASH"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                },
                TableName = "ProductCatalog"
            };

            Client.CreateTableAsync(productCatalogTableRequest, (result) =>
            {
                if (result.Exception != null)
                {
                    resultText.text += result.Exception.Message;
                    return;
                }
                var tableDescription = result.Response.TableDescription;
                resultText.text += String.Format("Created {1}: {0}\nReadsPerSec: {2} \nWritesPerSec: {3}\n",
                              tableDescription.TableStatus,
                              tableDescription.TableName,
                              tableDescription.ProvisionedThroughput.ReadCapacityUnits,
                              tableDescription.ProvisionedThroughput.WriteCapacityUnits);
                resultText.text += (result.Request.TableName + "-" + tableDescription.TableStatus + "\n");
                resultText.text += ("Allow a few seconds for changes to reflect...");
            });


            var forumTableRequest = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                        new AttributeDefinition
                        {
                                AttributeName = "Name",
                                AttributeType = "S"
                        }
                },
                KeySchema = new List<KeySchemaElement>
                {
                        new KeySchemaElement
                        {
                                AttributeName = "Name",
                                KeyType = "HASH"
                        }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                },
                TableName = "Forum"
            };

            Client.CreateTableAsync(forumTableRequest, (result) =>
            {
                if (result.Exception != null)
                {
                    resultText.text += result.Exception.Message;
                    return;
                }
                var tableDescription = result.Response.TableDescription;
                resultText.text += String.Format("Created {1}: {0}\nReadsPerSec: {2} \nWritesPerSec: {3}\n",
                                                tableDescription.TableStatus,
                                                tableDescription.TableName,
                                                tableDescription.ProvisionedThroughput.ReadCapacityUnits,
                                                tableDescription.ProvisionedThroughput.WriteCapacityUnits);
                resultText.text += (result.Request.TableName + "-" + tableDescription.TableStatus + "\n");
                resultText.text += ("Allow a few seconds for changes to reflect...");
            });

            var threadTableRequest = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                            AttributeName = "ForumName",
                            AttributeType = "S"
                    },
                    new AttributeDefinition
                    {
                            AttributeName = "Subject",
                            AttributeType = "S"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                        new KeySchemaElement
                        {
                            AttributeName = "ForumName",
                            KeyType = "HASH"
                        },
                        new KeySchemaElement
                        {
                            AttributeName = "Subject",
                            KeyType = "RANGE"
                        }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                },
                TableName = "Thread"
            };

            Client.CreateTableAsync(threadTableRequest, (result) =>
            {
                if (result.Exception != null)
                {
                    resultText.text += result.Exception.Message;
                    return;
                }
                var tableDescription = result.Response.TableDescription;
                resultText.text += String.Format("Created {1}: {0}\nReadsPerSec: {2} \nWritesPerSec: {3}\n",
                                                tableDescription.TableStatus,
                                                tableDescription.TableName,
                                                tableDescription.ProvisionedThroughput.ReadCapacityUnits,
                                                tableDescription.ProvisionedThroughput.WriteCapacityUnits);
                resultText.text += (result.Request.TableName + "-" + tableDescription.TableStatus + "\n");
                resultText.text += ("Allow a few seconds for changes to reflect...");
            });

            var replyTableRequest = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "S"
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "ReplyDateTime",
                        AttributeType = "S"
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "PostedBy",
                        AttributeType = "S"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = KeyType.HASH
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "ReplyDateTime",
                        KeyType = KeyType.RANGE
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                },
                LocalSecondaryIndexes = new List<LocalSecondaryIndex>()
                {
                    new LocalSecondaryIndex()
                    {
                        IndexName="PostedBy-index",
                        KeySchema = new List<KeySchemaElement>()
                        {
                            new KeySchemaElement()
                            {
                                    AttributeName = "Id",
                                    KeyType = KeyType.HASH
                            },
                            new KeySchemaElement
                            {
                                AttributeName = "PostedBy",
                                KeyType = KeyType.RANGE
                            }
                        },
                        Projection = new Projection()
                        {
                            ProjectionType = ProjectionType.KEYS_ONLY
                        }
                    }
                },
                TableName = "Reply"
            };

            Client.CreateTableAsync(replyTableRequest, (result) =>
            {
                if (result.Exception != null)
                {
                    resultText.text += result.Exception.Message;
                    return;
                }
                var tableDescription = result.Response.TableDescription;
                resultText.text += String.Format("Created {1}: {0}\nReadsPerSec: {2} \nWritesPerSec: {3}\n",
                                                tableDescription.TableStatus,
                                                tableDescription.TableName,
                                                tableDescription.ProvisionedThroughput.ReadCapacityUnits,
                                                tableDescription.ProvisionedThroughput.WriteCapacityUnits);
                resultText.text += (result.Request.TableName + "-" + tableDescription.TableStatus + "\n");
                resultText.text += ("Allow a few seconds for changes to reflect...");
            });
        }

        void ListTableListener()
        {
            resultText.text = "\n*** listing tables ***";
            string lastTableNameEvaluated = null;

            var request = new ListTablesRequest
            {
                Limit = 2,
                ExclusiveStartTableName = lastTableNameEvaluated
            };

            Client.ListTablesAsync(request, (result) =>
            {
                if (result.Exception != null)
                {
                    resultText.text += result.Exception.Message;
                    return;
                }

                resultText.text += "ListTable response : \n";
                var response = result.Response;
                foreach (string name in response.TableNames)
                    resultText.text += name + "\n";

                // repeat request to fetch more results
                lastTableNameEvaluated = response.LastEvaluatedTableName;
            });
        }

        void UpdateTableListener()
        {
            resultText.text = ("\n*** Updating table ***\n");
            var request = new UpdateTableRequest()
            {
                TableName = @"ProductCatalog",
                ProvisionedThroughput = new ProvisionedThroughput()
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 10
                }
            };
            Client.UpdateTableAsync(request, (result) =>
            {
                if (result.Exception != null)
                {
                    resultText.text += result.Exception.Message;
                    return;
                }
                var response = result.Response;
                var table = response.TableDescription;
                resultText.text += ("Table " + table.TableName + " Updated ! \n Allow a few seconds to reflect !");
            });
        }

        void DescribeTableListener()
        {
            resultText.text = ("\n*** Retrieving table information ***\n");
            var request = new DescribeTableRequest
            {
                TableName = @"ProductCatalog"
            };
            Client.DescribeTableAsync(request, (result) =>
            {
                if (result.Exception != null)
                {
                    resultText.text += result.Exception.Message;
                    Debug.Log(result.Exception);
                    return;
                }
                var response = result.Response;
                TableDescription description = response.Table;
                resultText.text += ("Name: " + description.TableName + "\n");
                resultText.text += ("# of items: " + description.ItemCount + "\n");
                resultText.text += ("Provision Throughput (reads/sec): " + description.ProvisionedThroughput.ReadCapacityUnits + "\n");
                resultText.text += ("Provision Throughput (reads/sec): " + description.ProvisionedThroughput.WriteCapacityUnits + "\n");

            }, null);
        }

        void DeleteTableListener()
        {
            resultText.text = ("\n*** Deleting table ***\n");
            var request = new DeleteTableRequest
            {
                TableName = @"ProductCatalog"
            };
            Client.DeleteTableAsync(request, (result) =>
            {
                if (result.Exception != null)
                {
                    resultText.text += result.Exception.Message;
                    return;
                }
                var response = result.Response;

                resultText.text += ("Table " + response.TableDescription.TableName + " is being deleted...!");
            });
        }
    }
}
