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
using System.Linq;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using System.Threading;
using Amazon.CognitoIdentity;
using Amazon.Unity3D;

class AmazonMidlevelItemCrud : MonoBehaviour
{
    private AmazonDynamoDBClient client;
    private string tableName = "ProductCatalog";
    // The sample uses the following id PK value to add book item.
    private int sampleBookId = 555;
    
    private string displayMessage = "";
    private Table productCatalog;
    
    public string cognitoIdentityPoolId = "";
    public AWSRegion cognitoRegion = AWSRegion.USEast1;
    public AWSRegion dynamoDBRegion = AWSRegion.USEast1;
    
    void Start()
    {
        // Set Unity SDK logging level
        AmazonLogging.Level = AmazonLogging.LoggingLevel.DEBUG;
        
        client = new AmazonDynamoDBClient(new CognitoAWSCredentials(cognitoIdentityPoolId, cognitoRegion.GetRegionEndpoint()), dynamoDBRegion.GetRegionEndpoint());
    }
    
    void OnGUI()
    {
        
        GUILayout.BeginArea (new Rect (0, 0, Screen.width * 0.5f, Screen.height));
        GUILayout.Space(20f);
        GUILayout.Label ("MidlevelItemCRUD Operations");
        GUILayout.Label ("Note: Use them in the same order");
        
        if (GUILayout.Button ("Load Table", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            client = new AmazonDynamoDBClient(RegionEndpoint.USEast1);
            this.LoadTable();
        }
        else if (GUILayout.Button ("Create Book", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.CreateBookItem();
        }
        else if (GUILayout.Button ("Retrieve Book", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.RetrieveBook();
        }
        else if (GUILayout.Button ("Update Multiple Attributes", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.UpdateMultipleAttributes();
        }
        else if (GUILayout.Button ("Delete Book", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.DeleteBook();
        }
        else if (GUILayout.Button ("SingleBatchWrite", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.SingleTableBatchWrite();
        }
        else if (GUILayout.Button ("MultiTableBatchWrite", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.MultiTableBatchWrite();
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
    
    private void LoadTable()
    {
        this.displayMessage += "\n***LoadTable***";
        Table.LoadTableAsync(client, tableName, 
                             (AmazonDynamoResult<Table> result) =>
        {
            if (result.Exception != null)
            {
                this.displayMessage += "\nLoadTable ; " +result.Exception.Message;
                Debug.LogException(result.Exception);
                return;
            }
            productCatalog = result.Response;
            this.displayMessage += "\nLoadTable Success ";
        }, null);
    }

    // Creates a sample book item. 
    private void CreateBookItem()
    {
        this.displayMessage += ("\n*** Executing CreateBookItem() ***");
        var book = new Document();
        book["Id"] = sampleBookId;
        book["Title"] = "Book " + sampleBookId;
        book["Price"] = 19.99;
        book["ISBN"] = "111-1111111111";
        book["Authors"] = new List<string> { "Author 1", "Author 2", "Author 3" };
        book["PageCount"] = 500;
        book["Dimensions"] = "8.5x11x.5";
        book["InPublication"] = true;
        book["InStock"] = false;
        book["QuantityOnHand"] = 0;
        
        productCatalog.PutItemAsync(book, (AmazonDynamoResult<Document> result) =>
        {
            if (result.Exception != null)
            {
                this.displayMessage += "\nCreateBookItem ; " +result.Exception.Message;
                Debug.LogException(result.Exception);
                return;
            }
            this.displayMessage += "\nCreateBookItem Saved; ";
        }, null);
    }
    
    private void RetrieveBook()
    {
        this.displayMessage += ("\n*** Executing RetrieveBook() ***");
        // Optional configuration.
        GetItemOperationConfig config = new GetItemOperationConfig
        {
            AttributesToGet = new List<string> { "Id", "ISBN", "Title", "Authors", "Price" },
            ConsistentRead = true
        };
        productCatalog.GetItemAsync(sampleBookId, config, 
                                     (AmazonDynamoResult<Document> result) => 
        {
            if (result.Exception != null)
            {
                this.displayMessage += "\nRetrieveBook ; " +result.Exception.Message;
                Debug.LogException(result.Exception);
                return;
            }
            Document document = result.Response;
            this.displayMessage += ("\nRetrieveBook: Printing book retrieved...");
            PrintDocument(document);
        }, null);
        
    }
    
    private void UpdateMultipleAttributes()
    {
        this.displayMessage += ("\n*** Executing UpdateMultipleAttributes() ***");
        this.displayMessage += ("\nUpdating multiple attributes....");
        int hashKey = sampleBookId;
        
        var book = new Document();
        book["Id"] = hashKey;
        // List of attribute updates.
        // The following replaces the existing authors list.
        book["Authors"] = new List<string> { "Author x", "Author y" };
        book["newAttribute"] = "New Value";
        book["ISBN"] = null; // Remove it.
        
        // Optional parameters.
        UpdateItemOperationConfig config = new UpdateItemOperationConfig
        {
            // Get updated item in response.
            ReturnValues = ReturnValues.AllNewAttributes
        };
        productCatalog.UpdateItemAsync(book, config, 
                                        (AmazonDynamoResult<Document> result) =>
        {
            if (result.Exception != null)
            {
                this.displayMessage += ("\nUpdateMultipleAttributes ; " +result.Exception.Message);
                Debug.LogException(result.Exception);
                return;
            }
            Document updatedBook = result.Response;
            this.displayMessage += ("\nUpdateMultipleAttributes: Printing item after updates ...");
            PrintDocument(updatedBook);
        }, null);
    }
    
    private void DeleteBook()
    {
        this.displayMessage += ("\n*** Executing DeleteBook() ***");
        // Optional configuration.
        DeleteItemOperationConfig config = new DeleteItemOperationConfig
        {
            // Return the deleted item.
            ReturnValues = ReturnValues.AllOldAttributes
        };
        productCatalog.DeleteItemAsync(sampleBookId, config,
                                        (AmazonDynamoResult<Document> result) =>
        {
            if (result.Exception != null)
            {
                this.displayMessage += ("\nDeleteBook ; " +result.Exception.Message);
                Debug.LogException(result.Exception);
                return;
            }
            Document document = result.Response;
            this.displayMessage += ("\nDeleteBook: Printing deleted just deleted...");
            PrintDocument(document);
        }, null);
        
    }
    
    private void PrintDocument(Document updatedDocument)
    {
        foreach (var attribute in updatedDocument.GetAttributeNames())
        {
            string stringValue = null;
            var value = updatedDocument[attribute];
            if (value is Primitive)
                stringValue = value.AsPrimitive().Value.ToString();
            else if (value is PrimitiveList)
            {
                foreach(Primitive primitive in value.AsPrimitiveList().Entries)
                    stringValue = primitive.Value.ToString() + ",";
            }
            this.displayMessage += String.Format("\n{0} - {1}", attribute, stringValue);
        }
    }


    private void SingleTableBatchWrite()
    {
        this.displayMessage = ("\n*** SingleTableBatchWrite() ***");
        ThreadPool.QueueUserWorkItem((s)=> 
        {
            this.displayMessage += ("\n running in background thread");
            try
            {
                Table productCatalog = Table.LoadTable(client, "ProductCatalog");
                var batchWrite = productCatalog.CreateBatchWrite();
                
                var book1 = new Document();
                book1["Id"] = 902;
                book1["Title"] = "My book1 in batch write using .NET helper classes";
                book1["ISBN"] = "902-11-11-1111";
                book1["Price"] = 10;
                book1["ProductCategory"] = "Book";
                book1["Authors"] = new List<string> { "Author 1", "Author 2", "Author 3" };
                book1["Dimensions"] = "8.5x11x.5";
                book1["InStock"] = true;
                book1["QuantityOnHand"] = null; //Quantity is unknown at this time
                
                batchWrite.AddDocumentToPut(book1);
                // Specify delete item using overload that takes PK. 
                batchWrite.AddKeyToDelete(12345);
                this.displayMessage += ("\nPerforming batch write in SingleTableBatchWrite()");
                batchWrite.Execute();
                this.displayMessage += "\nCompleted!";
            }
            catch(Exception ex)
            {
                this.displayMessage += ex.Message;
                Debug.LogException(ex);
            }
        });

    }
    
    private void MultiTableBatchWrite()
    {
        this.displayMessage = ("\n*** MultiTableBatchWrite() ***");
        ThreadPool.QueueUserWorkItem((s)=> 
        {
            this.displayMessage += ("\n running in background thread");
            try
            {
                var batchWrite = productCatalog.CreateBatchWrite();
                
                var book1 = new Document();
                book1["Id"] = 9111;
                book1["Title"] = "My 222book1 in batch write using .NET helper classes";
                book1["ISBN"] = "902-11-11-1111";
                book1["Price"] = 10;
                book1["ProductCategory"] = "Book";
                book1["Authors"] = new List<string> { "Author 1", "Author 2", "Author 3" };
                book1["Dimensions"] = "8.5x11x.5";
                book1["InStock"] = true;
                book1["QuantityOnHand"] = null; //Quantity is unknown at this time
                batchWrite.AddDocumentToPut(book1);
                // 1. Specify item to add in the Forum table.
                Table reply = Table.LoadTable(client, "Reply");

                var replyBatchWrite = reply.CreateBatchWrite();
                
                // Reply 1 - thread 1.
                var thread1Reply1 = new Document();
                thread1Reply1["Id"] = "Amazon DynamoDB#DynamoDB Thread 1"; // Hash attribute.
                thread1Reply1["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(21, 0, 0, 0)); // Range attribute.
                thread1Reply1["Message"] = "DynamoDB Thread 1 Reply 1 text";
                thread1Reply1["PostedBy"] = "User A";
                thread1Reply1["Votes"] = -2;
                replyBatchWrite.AddDocumentToPut(thread1Reply1);
                

                // 3. Create multi-table batch.
                var superBatch = new MultiTableDocumentBatchWrite();
                superBatch.AddBatch(replyBatchWrite);
                superBatch.AddBatch(batchWrite);
                this.displayMessage += ("\nPerforming batch write in MultiTableBatchWrite()");
                superBatch.Execute();
                this.displayMessage += "\nCompleted!";
            }
            catch(Exception ex)
            {
                this.displayMessage += ex.Message;
                Debug.LogException(ex);
            }
        });

    }

}
