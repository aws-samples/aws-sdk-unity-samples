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
using UnityEngine;

using System.Collections.Generic;

using Amazon;
using Amazon.Unity3D;
using Amazon.Runtime;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;             
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.DataModel;
using System.Threading;
using Amazon.CognitoIdentity;


public class AmazonHighLevelMappingArbitraryData : MonoBehaviour
{
    public AmazonHighLevelMappingArbitraryData()
    {
    }
    
    private AmazonDynamoDBClient client;
    private DynamoDBContext context;
    private string displayMessage = "";
    private Book retrievedBook = null;
    
    public string cognitoIdentityPoolId = "";
    public AWSRegion cognitoRegion = AWSRegion.USEast1;
    public AWSRegion dynamoDBRegion = AWSRegion.USEast1;
    
    void Start()
    {
        // Set Unity SDK logging level
        AmazonLogging.Level = AmazonLogging.LoggingLevel.DEBUG;
    }
    
    
    void OnGUI()
    {
        GUILayout.BeginArea (new Rect (0, 0, Screen.width * 0.5f, Screen.height));
        GUILayout.Space(20f);
        GUILayout.Label ("HighLevelMappingArbitraryData Operations");
        GUILayout.Label ("Note: Use them in the same order");
        
        if (GUILayout.Button ("Create DynamoDBContext", GUILayout.MinHeight (Screen.height * 0.15f), GUILayout.Width (Screen.width * 0.4f)))
        {
            client = new AmazonDynamoDBClient(new CognitoAWSCredentials(cognitoIdentityPoolId, cognitoRegion.GetRegionEndpoint()), dynamoDBRegion.GetRegionEndpoint());
            context = new DynamoDBContext(client);
            this.displayMessage = "DynamoDBContext created"; 
        }
        else if (GUILayout.Button ("Create Book", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.CreateBook();
        }
        else if (GUILayout.Button ("Retrieve Book", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.RetrieveBook();
        }
        else if (GUILayout.Button ("Update Retrieved Book", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.UpdateBook();
        }
        else if (GUILayout.Button ("Delete Retrieved Book", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.DeleteBook();
        }
        else if (GUILayout.Button ("SingleTableBatchWrite", GUILayout.MinHeight (Screen.height * 0.1f), GUILayout.Width (Screen.width * 0.4f)))
        {
            this.SingleTableBatchWrite();
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
    private void CreateBook()
    {
        this.displayMessage += "\n*** Creating book**\n";
        // 1. Create a book.
        DimensionType myBookDimensions = new DimensionType()
        {
            Length = 8M,
            Height = 11M,
            Thickness = 0.5M
        };
        
        Book myBook = new Book
        {
            Id = 508,
            Title = "AWS SDK for .NET Object Persistence Model Handling Arbitrary Data",
            ISBN = "999-9999999999",
            BookAuthors = new List<string> { "Author 1", "Author 2" },
            Dimensions = myBookDimensions
        };
        
        
        context.SaveAsync<Book>(myBook, 
                                (AmazonDynamoResult<VoidResponse> result) =>
        {
            if (result.Exception != null)
            {
                this.displayMessage += "Save failed ; " +result.Exception.Message;
                Debug.LogError("Save error");
                Debug.LogException(result.Exception);
                return;
            }
            this.displayMessage += ("Save Book{" + myBook.Id + "} to DynamoDB Complete ! \n");
        }, null);
    }

    private void RetrieveBook()
    {
        this.displayMessage += "\n*** Load book**\n";
        context.LoadAsync<Book>(508, 
                                 (AmazonDynamoResult<Book> result) =>
        {
            if (result.Exception != null)
            {
                
                this.displayMessage += ("LoadAsync error" +result.Exception.Message);
                Debug.LogException(result.Exception);
                return;
            }
            retrievedBook = result.Response;
            this.displayMessage += ("Retrieved Book: " +
                                    "\nId=" + retrievedBook.Id + 
                                    "\nTitle=" + retrievedBook.Title +
                                    "\nISBN=" + retrievedBook.ISBN);
            string authors = "";
            foreach(string author in retrievedBook.BookAuthors)
                authors += author + ",";
            this.displayMessage += "\nBookAuthor= "+ authors;
            this.displayMessage += ("\nDimensions= "+ retrievedBook.Dimensions.Length + " X " + 
                                    retrievedBook.Dimensions.Height + " X " +
                                    retrievedBook.Dimensions.Thickness);

        }, null);
    }

    private void UpdateBook()
    {
        this.displayMessage += "\n*** Update book**\n";
        if (retrievedBook == null)
        {
            this.displayMessage += "\nCan't UpdateBook() before RetrieveBook()";
            return;
        }
        // 3. Update property (book dimensions).
        retrievedBook.Dimensions.Height += 1;
        retrievedBook.Dimensions.Length += 1;
        retrievedBook.Dimensions.Thickness += 0.2M;
        
        // Update the book.
        context.SaveAsync<Book>(retrievedBook, (AmazonDynamoResult<VoidResponse> result) => 
        {
            if (result.Exception != null)
            {
                this.displayMessage += ("Save error" +result.Exception.Message);
                Debug.LogException(result.Exception);
                return;
            }
            this.displayMessage += ("Update Book{" + retrievedBook.Id + "} to DynamoDB Complete ! \n");
        }, null);
    }

    private void DeleteBook()
    {
        this.displayMessage += "\n*** Delete book**\n";
        if (retrievedBook == null)
        {
            this.displayMessage += "\nCan't perform DeleteBook() before RetrieveBook()";
            return;
        }
        // Update the book.
        context.DeleteAsync<Book>(retrievedBook, (AmazonDynamoResult<VoidResponse> result) => 
        {
            if (result.Exception != null)
            {
                this.displayMessage += ("Delete error" +result.Exception.Message);
                Debug.LogException(result.Exception);
                return;
            }
            this.displayMessage += ("Delete Book{" + retrievedBook.Id + "} from DynamoDB Complete ! \n");
        }, null);
    }

    private void SingleTableBatchWrite()
    {
        this.displayMessage = ("\n*** SingleTableBatchWrite() ***");
        ThreadPool.QueueUserWorkItem((s)=> 
        {
            this.displayMessage += ("\n running in background thread");
            try
            {
                
                Book book1 = new Book
                {
                    Id = 902,
                    ISBN = "902-11-11-1111",
                    Title = "My book3 in batch write"
                };
                Book book2 = new Book
                {
                    Id = 903,
                    ISBN = "903-11-11-1111",
                    Title = "My book4 in batch write"
                };
                
                var bookBatch = context.CreateBatchWrite<Book>();
                bookBatch.AddPutItems(new List<Book> { book1, book2 });
                
                this.displayMessage += ("Performing batch write in SingleTableBatchWrite().");
                bookBatch.Execute();
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
[DynamoDBTable("ProductCatalog")]
class Book
{
    [DynamoDBHashKey]   // hash key
    public int Id { get; set; }
    [DynamoDBProperty]
    public string Title { get; set; }
    [DynamoDBProperty]
    public string ISBN { get; set; }
    // Multi-valued (set type) attribute. 
    [DynamoDBProperty("Authors")]
    public List<string> BookAuthors { get; set; }
    // Arbitrary type, with a converter to map it to DynamoDB type.
    [DynamoDBProperty(typeof(DimensionTypeConverter))]
    public DimensionType Dimensions { get; set; }
    
    public Book()
    {
    }
}

class DimensionType
{
    public decimal Length { get; set; }
    public decimal Height { get; set; }
    public decimal Thickness { get; set; }
}

// Converts the complex type DimensionType to string and vice-versa.
class DimensionTypeConverter : IPropertyConverter
{
    public DynamoDBEntry ToEntry(object value)
    {
        DimensionType bookDimensions = value as DimensionType;
        if (bookDimensions == null) throw new ArgumentOutOfRangeException();
        
        string data = string.Format("{1}{0}{2}{0}{3}", " x ",
                                    bookDimensions.Length, bookDimensions.Height, bookDimensions.Thickness);
        
        DynamoDBEntry entry = new Primitive { Value = data };
        return entry;
    }
    
    public object FromEntry(DynamoDBEntry entry)
    {
        Primitive primitive = entry as Primitive;
        if (primitive == null || !(primitive.Value is String) || string.IsNullOrEmpty((string)primitive.Value))
            throw new ArgumentOutOfRangeException();
        
        string[] data = ((string)(primitive.Value)).Split(new string[] { " x " }, StringSplitOptions.None);
        if (data.Length != 3) throw new ArgumentOutOfRangeException();
        
        DimensionType complexData = new DimensionType
        {
            Length = Convert.ToDecimal(data[0]),
            Height = Convert.ToDecimal(data[1]),
            Thickness = Convert.ToDecimal(data[2])
        };
        return complexData;
    }
}

