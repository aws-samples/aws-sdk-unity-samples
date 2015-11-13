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
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using UnityEngine.UI;


namespace AWSSDK.Examples
{
    public class HighLevelTableExample : DynamoDbBaseExample {
        
        private IAmazonDynamoDB _client;
        private DynamoDBContext _context;
        
        public Text resultText;
        public Button back;
        public Button createOperation;
        public Button updateOperation;
        public Button deleteOperation;
        
        int bookID = 1001;
        
        private DynamoDBContext Context
        {
            get
            {
                if(_context == null)
                    _context = new DynamoDBContext(_client);
                    
                return _context;
            }
        }
        
        void Awake()
        {
            back.onClick.AddListener(BackListener);
            createOperation.onClick.AddListener(PerformCreateOperation);
            updateOperation.onClick.AddListener(PerformUpdateOperation);
            deleteOperation.onClick.AddListener(PerformDeleteOperation);
            _client = Client;
        }
        
        private void PerformCreateOperation()
        {
            Book myBook = new Book
            {
                Id = bookID,
                Title = "object persistence-AWS SDK for.NET SDK-Book 1001",
                ISBN = "111-1111111001",
                BookAuthors = new List<string> { "Author 1", "Author 2" },
            };
            
            // Save the book.
            Context.SaveAsync(myBook,(result)=>{
                if(result.Exception == null)
                    resultText.text += @"book saved";
            });
        }
        
        private void PerformUpdateOperation()
        {
            // Retrieve the book. 
            Book bookRetrieved = null;
            Context.LoadAsync<Book>(bookID,(result)=>
            {
                if(result.Exception == null )
                {
                    bookRetrieved = result.Result as Book;
                    // Update few properties.
                    bookRetrieved.ISBN = "222-2222221001";
                    bookRetrieved.BookAuthors = new List<string> { " Author 1", "Author x" }; // Replace existing authors list with this.
                    Context.SaveAsync<Book>(bookRetrieved,(res)=>
                    {
                        if(res.Exception == null)
                            resultText.text += ("\nBook updated");
                    });
                }
            });
        }
        
        private void PerformDeleteOperation()
        {
            // Delete the book.
            Context.DeleteAsync<Book>(bookID,(res)=>{
                if(res.Exception ==null)
                {
                    Context.LoadAsync<Book>(bookID,(result)=>
                    {
                        Book deletedBook = result.Result;
                        if(deletedBook==null)
                            resultText.text += ("\nBook is deleted");
                    });
                }
            });
        }
    }
    
    [DynamoDBTable("ProductCatalog")]
    public class Book
    {
        [DynamoDBHashKey]   // Hash key.
        public int Id { get; set; }
        [DynamoDBProperty]
        public string Title { get; set; }
        [DynamoDBProperty]
        public string ISBN { get; set; }
        [DynamoDBProperty("Authors")]    // Multi-valued (set type) attribute. 
        public List<string> BookAuthors { get; set; }
    }
}
