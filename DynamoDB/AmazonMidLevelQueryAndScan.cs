using System;
using System.Collections.Generic;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon;

using UnityEngine;
using System.Threading;
using Amazon.CognitoIdentity;
using Amazon.Unity3D;

class AmazonMidLevelQueryAndScan : MonoBehaviour
{
    private AmazonDynamoDBClient client;
    private Table replyTable;
    private string displayMessage = "";
    private string forumName = "Amazon DynamoDB";
    private string threadSubject = "DynamoDB Thread 2";
    
    public string cognitoIdentityPoolId = "YourIdentityPoolId";
    public AWSRegion cognitoRegion = AWSRegion.USEast1;
    public AWSRegion dynamoDBRegion = AWSRegion.USEast1;
    
    void Start()
    {
        // Set Unity SDK logging level
        AmazonLogging.Level = AmazonLogging.LoggingLevel.DEBUG;
        
        
        client = new AmazonDynamoDBClient(new CognitoAWSCredentials(cognitoIdentityPoolId,cognitoRegion.GetRegionEndpoint()), dynamoDBRegion.GetRegionEndpoint());
    }
    
    void OnGUI()
    {
        /*
         * Table Schema used for this example
         * Reply ( Id(HK), ReplyDateTime(RK), Message, PostedBy, Votes, ...)
         */ 

        GUILayout.BeginArea (new Rect (0, 0, Screen.width * 0.5f, Screen.height));
        GUILayout.Space(20f);
        GUILayout.Label ("MidLevelQueryAndScan Operations");
        GUILayout.Label ("Note: Use them in the same order");
        
        if (GUILayout.Button ("Load Table", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.LoadTable();
        }
        else if (GUILayout.Button ("UploadSampleReplies", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.UploadSampleReplies();
        }
        else if (GUILayout.Button ("FindRepliesInLast15Days", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.FindRepliesInLast15Days(replyTable, forumName, threadSubject);
        }
        else if (GUILayout.Button ("FindRepliesInLast15DaysWithConfig", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.FindRepliesInLast15DaysWithConfig(replyTable, forumName, threadSubject);
        }
        else if (GUILayout.Button ("FindRepliesWithNegativeVotes", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.FindRepliesWithNegativeVotes(replyTable);
        }
        else if (GUILayout.Button ("FindRepliesWithNegativeVotesWithConfig", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.FindRepliesWithNegativeVotesWithConfig(replyTable);
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
        Table.LoadTableAsync(client, "Reply", 
        (AmazonDynamoResult<Table> result) =>
        {
            if (result.Exception != null)
            {
                this.displayMessage += "\nLoadTable ; " +result.Exception.Message;
                Debug.LogException(result.Exception);
                return;
            }
            replyTable = result.Response;
            this.displayMessage += "\nLoadTable Success ";
        }, null);
    }

    
    private void UploadSampleReplies()
    {
        this.displayMessage += "\n***FindRepliesWithNegativeVotes***\n";
        if (replyTable == null)
        {
            this.displayMessage += "\nLoad table before running scans";
            return;
        }
        ThreadPool.QueueUserWorkItem((s) =>
        {
            try
            {
                this.displayMessage += "\nRunning on background thread";

                // Reply 1 - thread 1.
                var thread1Reply1 = new Document();
                thread1Reply1["Id"] = "Amazon DynamoDB#DynamoDB Thread 1"; // Hash attribute.
                thread1Reply1["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(21, 0, 0, 0)); // Range attribute.
                thread1Reply1["Message"] = "DynamoDB Thread 1 Reply 1 text";
                thread1Reply1["PostedBy"] = "User A";
                thread1Reply1["Votes"] = -2;
                replyTable.PutItem(thread1Reply1);
                
                // Reply 2 - thread 1.
                var thread1reply2 = new Document();
                thread1reply2["Id"] = "Amazon DynamoDB#DynamoDB Thread 1"; // Hash attribute.
                thread1reply2["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(14, 0, 0, 0)); // Range attribute.
                thread1reply2["Message"] = "DynamoDB Thread 1 Reply 2 text";
                thread1reply2["PostedBy"] = "User B";
                thread1reply2["Votes"] = 5;
                replyTable.PutItem(thread1reply2);
                
                // Reply 3 - thread 1.
                var thread1Reply3 = new Document();
                thread1Reply3["Id"] = "Amazon DynamoDB#DynamoDB Thread 1"; // Hash attribute.
                thread1Reply3["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(7, 0, 0, 0)); // Range attribute.
                thread1Reply3["Message"] = "DynamoDB Thread 1 Reply 3 text";
                thread1Reply3["PostedBy"] = "User B";
                thread1Reply3["Votes"] = 2;
                replyTable.PutItem(thread1Reply3);
                
                // Reply 1 - thread 2.
                var thread2Reply1 = new Document();
                thread2Reply1["Id"] = "Amazon DynamoDB#DynamoDB Thread 2"; // Hash attribute.
                thread2Reply1["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(7, 0, 0, 0)); // Range attribute.
                thread2Reply1["Message"] = "DynamoDB Thread 2 Reply 1 text";
                thread2Reply1["PostedBy"] = "User A";
                thread2Reply1["Votes"] = -3;
                
                replyTable.PutItem(thread2Reply1);
                
                // Reply 2 - thread 2.
                var thread2Reply2 = new Document();
                thread2Reply2["Id"] = "Amazon DynamoDB#DynamoDB Thread 2"; // Hash attribute.
                thread2Reply2["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0)); // Range attribute.
                thread2Reply2["Message"] = "DynamoDB Thread 2 Reply 2 text";
                thread2Reply2["PostedBy"] = "User A";
                thread2Reply2["Votes"] = 0;
                replyTable.PutItem(thread2Reply2);
                this.displayMessage += "\nCompleted!";
            }
            catch(Exception ex)
            {
                this.displayMessage += "\nFindRepliesWithNegativeVotes:" + ex.Message;
                Debug.LogException(ex);
            }
        });
    }
    
    private void FindRepliesWithNegativeVotes(Table productCatalogTable)
    {
        
        this.displayMessage += "\n***FindRepliesWithNegativeVotes***\n";
        if (replyTable == null)
        {
            this.displayMessage += "\nLoad table before running scans";
            return;
        }
        ThreadPool.QueueUserWorkItem((s) =>
                                     {
            try
            {
                this.displayMessage += "\nRunning on background thread";

                ScanFilter scanFilter = new ScanFilter();
                scanFilter.AddCondition("Votes", ScanOperator.LessThan, 0);
                
                Search search = productCatalogTable.Scan(scanFilter);
                
                List<Document> documentList = new List<Document>();
                do
                {
                    documentList = search.GetNextSet();
                    Console.WriteLine("\nFindRepliesWithNegativeVotes: printing ............");
                    foreach (var document in documentList)
                        PrintDocument(document);
                } while (!search.IsDone);
                this.displayMessage += "\nCompleted !\n";
            }
            catch(Exception ex)
            {
                this.displayMessage += "\nFindRepliesWithNegativeVotes:" + ex.Message;
                Debug.LogException(ex);
            }
        });
    }
    
    private void FindRepliesWithNegativeVotesWithConfig(Table productCatalogTable)
    {
        this.displayMessage = "***FindRepliesWithNegativeVotesWithConfig**\n";
        if (replyTable == null)
        {
            this.displayMessage += "\nLoad table before running scans";
            return;
        }
        ThreadPool.QueueUserWorkItem((s) =>
        {
            try
            {
                this.displayMessage += "\nRunning on background thread";

                ScanFilter scanFilter = new ScanFilter();
                scanFilter.AddCondition("Votes", ScanOperator.LessThan, 0);
                
                ScanOperationConfig config = new ScanOperationConfig()
                {
                    Filter = scanFilter,
                    Select = SelectValues.SpecificAttributes,
                    AttributesToGet = new List<string> { "Id", "ReplyDateTime", "Message" }
                };
                
                Search search = productCatalogTable.Scan(config);
                
                List<Document> documentList = new List<Document>();
                do
                {
                    documentList = search.GetNextSet();
                    this.displayMessage += ("\nFindRepliesWithNegativeVotesWithConfig: printing ............");
                    foreach (var document in documentList)
                        PrintDocument(document);
                } while (!search.IsDone);
                this.displayMessage += "\nCompleted !\n";
            }
            catch(Exception ex)
            {
                this.displayMessage += "\nFindRepliesWithNegativeVotesWithConfig:" + ex.Message;
                Debug.LogException(ex);
            }
        });
    }
    
    private void FindRepliesInLast15Days(Table table, string forumName, string threadSubject)
    {
        this.displayMessage += "\n***FindRepliesInLast15Days***\n";
        if (replyTable == null)
        {
            this.displayMessage += "\nLoad table before running query";
            return;
        }
        ThreadPool.QueueUserWorkItem((s) =>
        {
            try
            {
                this.displayMessage += "\nRunning on background thread";
                
                string hashAttribute = forumName + "#" + threadSubject;
                
                DateTime twoWeeksAgoDate = DateTime.UtcNow - TimeSpan.FromDays(15);
                QueryFilter filter = new QueryFilter("Id", QueryOperator.Equal, hashAttribute);
                filter.AddCondition("ReplyDateTime", QueryOperator.GreaterThan, twoWeeksAgoDate);
                
                // Use Query overloads that takes the minimum required query parameters.
                Search search = table.Query(filter);
                
                List<Document> documentSet = new List<Document>();
                do
                {
                    documentSet = search.GetNextSet();
                    this.displayMessage += ("\nFindRepliesInLast15Days: printing ............");
                    foreach (var document in documentSet)
                        PrintDocument(document);
                } while (!search.IsDone);

                this.displayMessage += "\nCompleted !\n";
            }
            catch(Exception ex)
            {
                this.displayMessage += "\nFindRepliesInLast15Days:" + ex.Message;
                Debug.LogException(ex);
            }
        });
    }
    
    private void FindRepliesPostedWithinTimePeriod(Table table, string forumName, string threadSubject)
    {
        this.displayMessage += "\n***FindRepliesPostedWithinTimePeriod***\n";
        if (replyTable == null)
        {
            this.displayMessage += "\nLoad table before running query";
            return;
        }
        ThreadPool.QueueUserWorkItem((s) =>
        {
            try
            {
                this.displayMessage = "\nRunning on background thread";
                
                
                DateTime startDate = DateTime.UtcNow.Subtract(new TimeSpan(21, 0, 0, 0));
                DateTime endDate = DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0));
                
                QueryFilter filter = new QueryFilter("Id", QueryOperator.Equal, forumName + "#" + threadSubject);
                filter.AddCondition("ReplyDateTime", QueryOperator.Between, startDate, endDate);
                
                QueryOperationConfig config = new QueryOperationConfig()
                {
                    Limit = 2, // 2 items/page.
                    Select = SelectValues.SpecificAttributes,
                    AttributesToGet = new List<string> { "Message", 
                        "ReplyDateTime", 
                        "PostedBy" },
                    ConsistentRead = true,
                    Filter = filter
                };
                
                Search search = table.Query(config);
                
                List<Document> documentList = new List<Document>();
                
                do
                {
                    documentList = search.GetNextSet();
                    this.displayMessage += String.Format("\nFindRepliesPostedWithinTimePeriod: printing replies posted within dates: {0} and {1} ............", startDate, endDate);
                    foreach (var document in documentList)
                    {
                        PrintDocument(document);
                    }
                } while (!search.IsDone);
                this.displayMessage += "\nCompleted !\n";
            }
            catch(Exception ex)
            {
                this.displayMessage += "\nFindRepliesPostedWithinTimePeriod:" + ex.Message;
                Debug.LogException(ex);
            }
        });

    }
    
    private void FindRepliesInLast15DaysWithConfig(Table table, string forumName, string threadName)
    {
        this.displayMessage = "\n***FindRepliesInLast15DaysWithConfig***\n";
        if (replyTable == null)
        {
            this.displayMessage += "\nLoad table before running query";
            return;
        }
        ThreadPool.QueueUserWorkItem((s) =>
        {
            try
            {
                this.displayMessage += "\nRunning on background thread";

                DateTime twoWeeksAgoDate = DateTime.UtcNow - TimeSpan.FromDays(15);
                QueryFilter filter = new QueryFilter("Id", QueryOperator.Equal, forumName + "#" + threadName);
                filter.AddCondition("ReplyDateTime", QueryOperator.GreaterThan, twoWeeksAgoDate);
                // You are specifying optional parameters so use QueryOperationConfig.
                QueryOperationConfig config = new QueryOperationConfig()
                {
                    Filter = filter,
                    // Optional parameters.
                    Select = SelectValues.SpecificAttributes,
                    AttributesToGet = new List<string> { "Message", "ReplyDateTime", 
                        "PostedBy" },
                    ConsistentRead = true
                };
                
                Search search = table.Query(config);
                
                List<Document> documentSet = new List<Document>();
                do
                {
                    documentSet = search.GetNextSet();
                    this.displayMessage += ("\nFindRepliesInLast15DaysWithConfig: printing ............");
                    foreach (var document in documentSet)
                        PrintDocument(document);
                } while (!search.IsDone);

                this.displayMessage += "\nCompleted !\n";
            }
            catch(Exception ex)
            {
                this.displayMessage += "\nFindRepliesInLast15DaysWithConfig:" + ex.Message;
                Debug.LogException(ex);
            }
        });
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
}