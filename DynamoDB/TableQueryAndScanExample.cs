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
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Threading;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime.Internal;
using System.Collections.Generic;
using Amazon.Util;
using Amazon.DynamoDBv2.Model;


namespace AWSSDK.Examples
{
    public class TableQueryAndScanExample : DynamoDbBaseExample 
    {
        public Button back;
        public Button loadTable;
        public Button findRepliesInLast15DaysWithConfig;
        public Button findRepliesWithLimit;
        public Button findProductsWithPriceLessThanZero;
        public Text resultText;
        
        private static Table productCatalogTable;
        private static Table threadTable;
        private static Table replyTable;
        private static Table forumTable;

        private IAmazonDynamoDB _client;
        
        void Start()
        {
            back.onClick.AddListener(BackListener);
            loadTable.onClick.AddListener(LoadTableListener);
            findRepliesInLast15DaysWithConfig.onClick.AddListener(FindRepliesInLast15DaysWithConfigListener);
            findRepliesWithLimit.onClick.AddListener(FindRepliesForAThreadSpecifyOptionsLimitListener);
            findProductsWithPriceLessThanZero.onClick.AddListener(FindProductsForPriceLessThanZeroListener);
            _client = Client;
        }
        
        void LoadTableListener ()
        {
            resultText.text = "\n***LoadTable***";
            Table.LoadTableAsync(_client,"ProductCatalog",(loadTableResult)=>{
                if(loadTableResult.Exception != null)
                {
                    resultText.text += "\n failed to load product catalog table";
                }
                else
                {
                    productCatalogTable = loadTableResult.Result;
                    LoadSampleProducts();
                }
            });
            Table.LoadTableAsync(_client,"Thread",(loadTableResult)=>{
                if(loadTableResult.Exception != null)
                {
                    resultText.text += "\n failed to load thread table";
                }
                else
                {
                    threadTable = loadTableResult.Result;
                    LoadSampleThreads();
                }
            });
            Table.LoadTableAsync(_client,"Reply",(loadTableResult)=>{
                if(loadTableResult.Exception != null)
                {
                    resultText.text += "\n failed to load reply table";
                }
                else
                {
                    replyTable = loadTableResult.Result;
                    LoadSampleReplies();
                }
            });
            Table.LoadTableAsync(_client,"Forum",(loadTableResult)=>{
                if(loadTableResult.Exception != null)
                {
                    resultText.text += "\n failed to load reply table";
                }
                else
                {
                    forumTable = loadTableResult.Result;
                    LoadSampleForums();
                }
            });
        }
        
        private static void LoadSampleProducts()
        {
            // ********** Add Books *********************
            var book1 = new Document();
            book1["Id"] = 101;
            book1["Title"] = "Book 101 Title";
            book1["ISBN"] = "111-1111111111";
            book1["Authors"] = new List<string> { "Author 1" };
            book1["Price"] = -2; // *** Intentional value. Later used to illustrate scan.
            book1["Dimensions"] = "8.5 x 11.0 x 0.5";
            book1["PageCount"] = 500;
            book1["InPublication"] = true;
            book1["ProductCategory"] = "Book";
            productCatalogTable.PutItemAsync(book1,(r)=>{});
            
            var book2 = new Document();
            
            book2["Id"] = 102;
            book2["Title"] = "Book 102 Title";
            book2["ISBN"] = "222-2222222222";
            book2["Authors"] = new List<string> { "Author 1", "Author 2" }; ;
            book2["Price"] = 20;
            book2["Dimensions"] = "8.5 x 11.0 x 0.8";
            book2["PageCount"] = 600;
            book2["InPublication"] = true;
            book2["ProductCategory"] = "Book";
            productCatalogTable.PutItemAsync(book2,(r)=>{});
            
            var book3 = new Document();
            book3["Id"] = 103;
            book3["Title"] = "Book 103 Title";
            book3["ISBN"] = "333-3333333333";
            book3["Authors"] = new List<string> { "Author 1", "Author2", "Author 3" }; ;
            book3["Price"] = 2000;
            book3["Dimensions"] = "8.5 x 11.0 x 1.5";
            book3["PageCount"] = 700;
            book3["InPublication"] = false;
            book3["ProductCategory"] = "Book";
            productCatalogTable.PutItemAsync(book3,(r)=>{});
            
            // ************ Add bikes. *******************
            var bicycle1 = new Document();
            bicycle1["Id"] = 201;
            bicycle1["Title"] = "18-Bike 201"; // size, followed by some title.
            bicycle1["Description"] = "201 description";
            bicycle1["BicycleType"] = "Road";
            bicycle1["Brand"] = "Brand-Company A"; // Trek, Specialized.
            bicycle1["Price"] = 100;
            bicycle1["Gender"] = "M";
            bicycle1["Color"] = new List<string> { "Red", "Black" };
            bicycle1["ProductCategory"] = "Bike";
            productCatalogTable.PutItemAsync(bicycle1,(r)=>{});
            
            var bicycle2 = new Document();
            bicycle2["Id"] = 202;
            bicycle2["Title"] = "21-Bike 202Brand-Company A";
            bicycle2["Description"] = "202 description";
            bicycle2["BicycleType"] = "Road";
            bicycle2["Brand"] = "";
            bicycle2["Price"] = 200;
            bicycle2["Gender"] = "M"; // Mens.
            bicycle2["Color"] = new List<string> { "Green", "Black" };
            bicycle2["ProductCategory"] = "Bicycle";
            productCatalogTable.PutItemAsync(bicycle2,(r)=>{});
            
            var bicycle3 = new Document();
            bicycle3["Id"] = 203;
            bicycle3["Title"] = "19-Bike 203";
            bicycle3["Description"] = "203 description";
            bicycle3["BicycleType"] = "Road";
            bicycle3["Brand"] = "Brand-Company B";
            bicycle3["Price"] = 300;
            bicycle3["Gender"] = "W";
            bicycle3["Color"] = new List<string> { "Red", "Green", "Black" };
            bicycle3["ProductCategory"] = "Bike";
            productCatalogTable.PutItemAsync(bicycle3,(r)=>{});
            
            var bicycle4 = new Document();
            bicycle4["Id"] = 204;
            bicycle4["Title"] = "18-Bike 204";
            bicycle4["Description"] = "204 description";
            bicycle4["BicycleType"] = "Mountain";
            bicycle4["Brand"] = "Brand-Company B";
            bicycle4["Price"] = 400;
            bicycle4["Gender"] = "W"; // Women.
            bicycle4["Color"] = new List<string> { "Red" };
            bicycle4["ProductCategory"] = "Bike";
            productCatalogTable.PutItemAsync(bicycle4,(r)=>{});
            
            var bicycle5 = new Document();
            bicycle5["Id"] = 205;
            bicycle5["Title"] = "20-Title 205";
            bicycle4["Description"] = "205 description";
            bicycle5["BicycleType"] = "Hybrid";
            bicycle5["Brand"] = "Brand-Company C";
            bicycle5["Price"] = 500;
            bicycle5["Gender"] = "B"; // Boys.
            bicycle5["Color"] = new List<string> { "Red", "Black" };
            bicycle5["ProductCategory"] = "Bike";
            productCatalogTable.PutItemAsync(bicycle5,(r)=>{});
        }
        
        private static void LoadSampleForums()
        {
            var forum1 = new Document();
            forum1["Name"] = "Amazon DynamoDB"; // PK
            forum1["Category"] = "Amazon Web Services";
            forum1["Threads"] = 2;
            forum1["Messages"] = 4;
            forum1["Views"] = 1000;
            
            forumTable.PutItemAsync(forum1,(r)=>{});
            
            var forum2 = new Document();
            forum2["Name"] = "Amazon S3"; // PK
            forum2["Category"] = "Amazon Web Services";
            forum2["Threads"] = 1;
            
            forumTable.PutItemAsync(forum2,(r)=>{});
        }
        
        private static void LoadSampleThreads()
        {
            // Thread 1.
            var thread1 = new Document();
            thread1["ForumName"] = "Amazon DynamoDB"; // Hash attribute.
            thread1["Subject"] = "DynamoDB Thread 1"; // Range attribute.
            thread1["Message"] = "DynamoDB thread 1 message text";
            thread1["LastPostedBy"] = "User A";
            thread1["LastPostedDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(14, 0, 0, 0));
            thread1["Views"] = 0;
            thread1["Replies"] = 0;
            thread1["Answered"] = false;
            thread1["Tags"] = new List<string> { "index", "primarykey", "table" };
            
            threadTable.PutItemAsync(thread1,(r)=>{});
            
            // Thread 2.
            var thread2 = new Document();
            thread2["ForumName"] = "Amazon DynamoDB"; // Hash attribute.
            thread2["Subject"] = "DynamoDB Thread 2"; // Range attribute.
            thread2["Message"] = "DynamoDB thread 2 message text";
            thread2["LastPostedBy"] = "User A";
            thread2["LastPostedDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(21, 0, 0, 0));
            thread2["Views"] = 0;
            thread2["Replies"] = 0;
            thread2["Answered"] = false;
            thread2["Tags"] = new List<string> { "index", "primarykey", "rangekey" };
            
            threadTable.PutItemAsync(thread2,(r)=>{});
            
            // Thread 3.
            var thread3 = new Document();
            thread3["ForumName"] = "Amazon S3"; // Hash attribute.
            thread3["Subject"] = "S3 Thread 1"; // Range attribute.
            thread3["Message"] = "S3 thread 3 message text";
            thread3["LastPostedBy"] = "User A";
            thread3["LastPostedDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(7, 0, 0, 0));
            thread3["Views"] = 0;
            thread3["Replies"] = 0;
            thread3["Answered"] = false;
            thread3["Tags"] = new List<string> { "largeobjects", "multipart upload" };
            
            threadTable.PutItemAsync(thread3,(r)=>{});
        }
        
        private static void LoadSampleReplies()
        {
            // Reply 1 - thread 1.
            var thread1Reply1 = new Document();
            thread1Reply1["Id"] = "Amazon DynamoDB#DynamoDB Thread 1"; // Hash attribute.
            thread1Reply1["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(21, 0, 0, 0)); // Range attribute.
            thread1Reply1["Message"] = "DynamoDB Thread 1 Reply 1 text";
            thread1Reply1["PostedBy"] = "User A";
            
            replyTable.PutItemAsync(thread1Reply1,(r)=>{});
            
            // Reply 2 - thread 1.
            var thread1reply2 = new Document();
            thread1reply2["Id"] = "Amazon DynamoDB#DynamoDB Thread 1"; // Hash attribute.
            thread1reply2["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(14, 0, 0, 0)); // Range attribute.
            thread1reply2["Message"] = "DynamoDB Thread 1 Reply 2 text";
            thread1reply2["PostedBy"] = "User B";
            
            replyTable.PutItemAsync(thread1reply2,(r)=>{});
            
            // Reply 3 - thread 1.
            var thread1Reply3 = new Document();
            thread1Reply3["Id"] = "Amazon DynamoDB#DynamoDB Thread 1"; // Hash attribute.
            thread1Reply3["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(7, 0, 0, 0)); // Range attribute.
            thread1Reply3["Message"] = "DynamoDB Thread 1 Reply 3 text";
            thread1Reply3["PostedBy"] = "User B";
            
            replyTable.PutItemAsync(thread1Reply3,(r)=>{});
            
            // Reply 1 - thread 2.
            var thread2Reply1 = new Document();
            thread2Reply1["Id"] = "Amazon DynamoDB#DynamoDB Thread 2"; // Hash attribute.
            thread2Reply1["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(7, 0, 0, 0)); // Range attribute.
            thread2Reply1["Message"] = "DynamoDB Thread 2 Reply 1 text";
            thread2Reply1["PostedBy"] = "User A";
            
            
            replyTable.PutItemAsync(thread2Reply1,(r)=>{});
            
            // Reply 2 - thread 2.
            var thread2Reply2 = new Document();
            thread2Reply2["Id"] = "Amazon DynamoDB#DynamoDB Thread 2"; // Hash attribute.
            thread2Reply2["ReplyDateTime"] = DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0)); // Range attribute.
            thread2Reply2["Message"] = "DynamoDB Thread 2 Reply 2 text";
            thread2Reply2["PostedBy"] = "User A";
            
            replyTable.PutItemAsync(thread2Reply2,(r)=>{});
        }
        
        void FindRepliesInLast15DaysWithConfigListener ()
        {
            FindRepliesHelper(new QueryRequest(),null);
        }
        
        void FindRepliesHelper(QueryRequest request, Dictionary<string,AttributeValue> lastKeyEvaluated)
        {
            string forumName = "Amazon DynamoDB";
            string threadSubject = "DynamoDB Thread 1";
            string replyId = forumName + "#" + threadSubject;
            
            DateTime twoWeeksAgoDate = DateTime.UtcNow - TimeSpan.FromDays(15);
            string twoWeeksAgoString = twoWeeksAgoDate.ToString(AWSSDKUtils.ISO8601DateFormat);
            
            request.TableName = "Reply";
            
            request.KeyConditions = new Dictionary<string, Condition>()
            {
                { 
                    "Id",  new Condition()
                    { 
                        ComparisonOperator = "EQ",
                        AttributeValueList = new List<AttributeValue>()
                        {
                            new AttributeValue { S = replyId }
                        }
                    }
                },
                { 
                    "ReplyDateTime", new Condition()
                    {
                        ComparisonOperator = "GT",
                        AttributeValueList = new List<AttributeValue>()
                        { 
                            new AttributeValue { S = twoWeeksAgoString }
                        }
                    }
                }
            };
            
            // Optional parameter.
            request.ProjectionExpression = "Id, ReplyDateTime, PostedBy";
            
            // Optional parameter.
            request.ConsistentRead = true;
            request.Limit = 2; // The Reply table has only a few sample items. So the page size is smaller.
            request.ExclusiveStartKey = lastKeyEvaluated;
            request.ReturnConsumedCapacity = "TOTAL";
            
            _client.QueryAsync(request,(result)=>{
                resultText.text = string.Format("No. of reads used (by query in FindRepliesForAThreadSpecifyLimit) {0}\n",
                                                 result.Response.ConsumedCapacity.CapacityUnits);
                
                foreach (var item in result.Response.Items)
                {
                    PrintItem(item);
                }
                
                lastKeyEvaluated = result.Response.LastEvaluatedKey;
                if(lastKeyEvaluated !=null && lastKeyEvaluated.Count >0)
                {
                    FindRepliesHelper(request,result.Response.LastEvaluatedKey);
                }
            });
        }
        
        void FindRepliesForAThreadSpecifyOptionsLimitListener()
        {
            FindRepliesForAThreadSpecifyOptionalLimitHelper(null);
        }
        
        private void FindRepliesForAThreadSpecifyOptionalLimitHelper(Dictionary<string, AttributeValue> lastKeyEvaluated)
        {
            string forumName = "Amazon DynamoDB";
            string threadSubject = "DynamoDB Thread 1";
            string replyId = forumName + "#" + threadSubject;
            
            resultText.text = ("*** Executing FindRepliesForAThreadSpecifyOptionalLimit() ***");
            
            var request = new QueryRequest
            {
                TableName = "Reply",
                ReturnConsumedCapacity = "TOTAL",
                KeyConditions = new Dictionary<string, Condition>()
                {
                    {
                        "Id",
                        new Condition
                        {
                            ComparisonOperator = "EQ",
                            AttributeValueList = new List<AttributeValue>()
                            {
                                new AttributeValue { S = replyId }
                            }
                        }
                    }
                },
                Limit = 2, // The Reply table has only a few sample items. So the page size is smaller.
                ExclusiveStartKey = lastKeyEvaluated
            };
            
            _client.QueryAsync(request,(result)=>
            {
                resultText.text += string.Format("No. of reads used (by query in FindRepliesForAThreadSpecifyLimit) {0}\n",
                                    result.Response.ConsumedCapacity.CapacityUnits);
                foreach (Dictionary<string, AttributeValue> item
                         in result.Response.Items)
                {
                        PrintItem(item);
                }
                lastKeyEvaluated = result.Response.LastEvaluatedKey;
                if(lastKeyEvaluated != null && lastKeyEvaluated.Count != 0)
                {
                    FindRepliesForAThreadSpecifyOptionalLimitHelper(lastKeyEvaluated);
                }
            });
        }
        
        private void FindRepliesForAThread(string forumName, string threadSubject)
        {
            resultText.text = ("*** Executing FindRepliesForAThread() ***");
            string replyId = forumName + "#" + threadSubject;
            
            var request = new QueryRequest
            {
                TableName = "Reply",
                ReturnConsumedCapacity = "TOTAL",
                KeyConditions = new Dictionary<string, Condition>()
                {
                    {
                        "Id",
                        new Condition
                        {
                            ComparisonOperator = "EQ",
                            AttributeValueList = new List<AttributeValue>()
                            {
                                new AttributeValue { S = replyId }
                            }
                        }
                    }
                }
            };
            
            _client.QueryAsync(request,(result)=>{
                resultText.text = string.Format("No. of reads used (by query in FindRepliesForAThread) {0}\n",
                                    result.Response.ConsumedCapacity.CapacityUnits);
                foreach (Dictionary<string, AttributeValue> item
                         in result.Response.Items)
                {
                    PrintItem(item);
                }
            });
        }
        
        private void FindProductsForPriceLessThanZeroListener()
        {
            FindProductsForPriceLessThanZeroHelper(null);
        }
        
        private void FindProductsForPriceLessThanZeroHelper(Dictionary<string,AttributeValue> lastKeyEvaluated)
        {
            var request = new ScanRequest
            {
                TableName = "ProductCatalog",
                Limit = 2,
                ExclusiveStartKey = lastKeyEvaluated,
                ExpressionAttributeValues = new Dictionary<string,AttributeValue> {
                        {":val", new AttributeValue { N = "0" }}
                },
                FilterExpression = "Price < :val",
                
                ProjectionExpression = "Id, Title, Price"
            };
            
            _client.ScanAsync(request,(result)=>{
                foreach (Dictionary<string, AttributeValue> item
                         in result.Response.Items)
                {
                        resultText.text =("\nScanThreadTableUsePaging - printing.....");
                        PrintItem(item);
                }
                lastKeyEvaluated = result.Response.LastEvaluatedKey;
                if(lastKeyEvaluated != null && lastKeyEvaluated.Count != 0)
                {
                    FindProductsForPriceLessThanZeroHelper(lastKeyEvaluated);
                }
            });
        }
        
        
        private void PrintItem(Dictionary<string, AttributeValue> attributeList)
        {
            foreach (var kvp in attributeList)
            {
                string attributeName = kvp.Key;
                AttributeValue value = kvp.Value;
                
                resultText.text += 
                (
                    "\n" + attributeName + " " +
                    (value.S == null ? "" : "S=[" + value.S + "]") +
                    (value.N == null ? "" : "N=[" + value.N + "]") +
                    (value.SS == null ? "" : "SS=[" + string.Join(",", value.SS.ToArray()) + "]") +
                    (value.NS == null ? "" : "NS=[" + string.Join(",", value.NS.ToArray()) + "]")
                );
            }
            resultText.text += ("\n************************************************");
        }
    }
}
