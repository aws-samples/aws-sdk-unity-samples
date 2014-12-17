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
using UnityEngine;

using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon;

namespace com.amazonaws.codesamples
{
    class AmazonLowLevelTableExample : MonoBehaviour
    {
        private static AmazonDynamoDBClient client;

        private static string tableName = "ExampleTable";

        private string displayMessage = "";

        void Start()
        {
            try
            {
                client = new AmazonDynamoDBClient(RegionEndpoint.USEast1);
            }
            catch(Exception e)
            {
                Debug.LogException(e);
            }
        }

        void OnGUI()
        {
            GUILayout.BeginArea (new Rect (0, 0, Screen.width * 0.5f, Screen.height));
            GUILayout.Space(20f);
            GUILayout.Label ("LowLevelTable Operations");
            GUILayout.Label ("Note: Use them in the same order");

            if (GUILayout.Button ("Create Example Table", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
            {
                CreateExampleTable();
            }
            else if (GUILayout.Button ("List My Tables", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
            {
                this.ListMyTables();
            }
            else if (GUILayout.Button ("Update Example Table", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
            {
                UpdateExampleTable();
            }
            else if (GUILayout.Button ("Describe Example Table", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
            {
                this.GetTableInformation();
            }
            else if (GUILayout.Button ("Delete Example Table", GUILayout.MinHeight (Screen.height * 0.2f), GUILayout.Width (Screen.width * 0.4f)))
            {
                this.DeleteExampleTable();
            }
            GUILayout.EndArea ();
            
            
            GUILayout.BeginArea (new Rect (Screen.width * 0.55f, 0, Screen.width * 0.45f, Screen.height));
            GUILayout.Label ("Response");
            
            // Display Running Result
            if (displayMessage != null)
            {             
                GUILayout.TextField (displayMessage, GUILayout.MinHeight (Screen.height * 0.3f), GUILayout.Width (Screen.width * 0.4f));
            }
            GUILayout.EndArea ();  
        }
        
        private void CreateExampleTable()
        {
            this.displayMessage += ("\n*** Creating table ***\n");
            var request = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "N"
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "ReplyDateTime",
                        AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = "HASH"
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "ReplyDateTime",
                        KeyType = "RANGE"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 6
                },
                TableName = tableName
            };

            client.CreateTableAsync(request, (AmazonServiceResult result) =>
            {
                try
                {
                    
                    if (result.Exception != null)
                    {
                        this.displayMessage = result.Exception.Message;
                        Debug.Log(result.Exception);
                        return;
                    }

                    var response = result.Response as CreateTableResponse;
                    var tableDescription = response.TableDescription;
                    this.displayMessage += String.Format("Created {1}: {0}\nReadsPerSec: {2} \nWritesPerSec: {3}\n",
                                                         tableDescription.TableStatus,
                                                         tableDescription.TableName,
                                                         tableDescription.ProvisionedThroughput.ReadCapacityUnits,
                                                         tableDescription.ProvisionedThroughput.WriteCapacityUnits);
                    this.displayMessage += (tableName + "-" + tableDescription.TableStatus + "\n");
                    this.displayMessage += ("Allow a few seconds for changes to reflect...");
                }
                catch(Exception e)
                {
                    Debug.LogException(e);
                }
            }, null);
        }
        
        private void ListMyTables()
        {
            this.displayMessage = ("\n*** listing tables ***");
            string lastTableNameEvaluated = null;
            
            var request = new ListTablesRequest
            {
                Limit = 2,
                ExclusiveStartTableName = lastTableNameEvaluated
            };
            
            client.ListTablesAsync(request, 
            (AmazonServiceResult result) =>
            {
                if (result.Exception != null)
                {
                    this.displayMessage = result.Exception.Message;
                    Debug.Log(result.Exception);
                    return;
                }
                
                this.displayMessage = "ListTable response : \n";
                var response = result.Response as ListTablesResponse; 
                foreach (string name in response.TableNames)
                    displayMessage += name + "\n";
                
                // repeat request to fetch more results
                lastTableNameEvaluated = response.LastEvaluatedTableName;
                
            }, null);
        }
        
        private void GetTableInformation()
        {
            this.displayMessage = ("\n*** Retrieving table information ***\n");
            var request = new DescribeTableRequest
            {
                TableName = tableName
            };
            client.DescribeTableAsync(request, (AmazonServiceResult result) =>
                                 {
                if (result.Exception != null)
                {
                    this.displayMessage += result.Exception.Message;
                    Debug.Log(result.Exception);
                    return;
                }
                var response = result.Response as DescribeTableResponse;
                TableDescription description = response.Table;
                this.displayMessage += ("Name: " + description.TableName + "\n");
                this.displayMessage += ("# of items: " + description.ItemCount + "\n");
                this.displayMessage += ("Provision Throughput (reads/sec): " + description.ProvisionedThroughput.ReadCapacityUnits + "\n");
                this.displayMessage += ("Provision Throughput (reads/sec): " + description.ProvisionedThroughput.WriteCapacityUnits + "\n");

            }, null);
        }
        
        private void UpdateExampleTable()
        {
            this.displayMessage = ("\n*** Updating table ***\n");
            var request = new UpdateTableRequest()
            {
                TableName = tableName,
                ProvisionedThroughput = new ProvisionedThroughput()
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                }
            };
            client.UpdateTableAsync(request, (AmazonServiceResult result) =>
            {
                if (result.Exception != null)
                {
                    this.displayMessage += result.Exception.Message;
                    Debug.Log(result.Exception);
                    return;
                }
                var response = result.Response as UpdateTableResponse;
                var table = response.TableDescription;
                this.displayMessage += ("Table " + table.TableName+ " Updated ! \n Allow a few seconds to reflect !");

            }, null);
        }
        
        private void DeleteExampleTable()
        {
            this.displayMessage +=("\n*** Deleting table ***\n");
            var request = new DeleteTableRequest
            {
                TableName = tableName
            };
            client.DeleteTableAsync(request, (AmazonServiceResult result) =>
            {
                if (result.Exception != null)
                {
                    this.displayMessage += result.Exception.Message;
                    Debug.Log(result.Exception);
                    return;
                }
                var response = result.Response as DeleteTableResponse;

                this.displayMessage += ("Table " + response.TableDescription.TableName +" is being deleted... \nContinue !");
            }, null);
        }

    }
}
