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
using System.Collections;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using System.Collections.Generic;
using System.IO;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.S3.Util;
using Amazon.Unity3D;
using System.Net;
using Amazon.Common;
using System;

public class AmazonS3Sample : MonoBehaviour
{
    // Put your own Cognito Config in AWSPrefab editor variable
    // Make sure give your role permission to S3
    public string uploadKey = "";
    public string uploadSrcFilePath = "";
    public string downloadKey = "";
    public string bucketName = "";
    public string cognitoIdentityPool = "";
    public Amazon.RegionEndpoint cognitoRegion;
    public Amazon.RegionEndpoint s3Region;
    
    
    private AmazonS3Client _S3Client = null;
    private string runningResult = null;


    void Start()
    {
        cognitoRegion = RegionEndpoint.USEast1;
        s3Region = RegionEndpoint.USEast1;
    }


    private AmazonS3Client S3Client
    {
        get 
        {
            if(_S3Client == null)
            {
                _S3Client = new AmazonS3Client (new CognitoAWSCredentials (cognitoIdentityPool,cognitoRegion),s3Region);
            }
            return _S3Client;
        }
    }
    

    void ListBucketCallback (AmazonServiceResult result)
    {
        runningResult = null;
        if (result.Exception == null)
        {
            ListBucketsResponse response = result.Response as ListBucketsResponse;
            foreach (S3Bucket bucket in response.Buckets)
            {
                runningResult +=  "Find bucket: " + bucket.BucketName + "\n";
            }
        }
        else
        {
            Debug.LogException (result.Exception);
            Debug.LogError ("ListBucket fail");
            runningResult = "ListBucket fail : " + result.Exception.Message;;
        }
    }

    void ListObjectsCallback (AmazonServiceResult result)
    {
        runningResult = null;
        if (result.Exception == null)
        {
            ListObjectsResponse response = result.Response as ListObjectsResponse;
            foreach (S3Object ob in response.S3Objects)
            {
                runningResult += "Find object: " + ob.Key + "\n";
            }
        }
        else
        {
            Debug.LogException (result.Exception);
            Debug.LogError ("ListObject fail");
            runningResult = "ListObject fail: " + result.Exception.Message;
        }
    }

    void GetObjectCallback (AmazonServiceResult result)
    {
        runningResult = null;
        if (result.Exception == null)
        {
            GetObjectResponse response = result.Response as GetObjectResponse;
            using (response)
            {
                using (StreamReader reader = new StreamReader(response.ResponseStream))
                {
                    runningResult = reader.ReadToEnd ();
                }
            }
        }
        else
        {
            Debug.LogException (result.Exception);
            Debug.LogError ("S3 Download Object fail");
            runningResult = "S3 Download Object fail: " + result.Exception.Message;;
        }
    }

    void PostObjectCallback (AmazonServiceResult result)
    {
        runningResult = null;
        if (result.Exception == null)
        {
            runningResult = "S3 Upload Object succeed";
        }
        else
        {
            Debug.LogException (result.Exception);
            Debug.LogError ("S3 Upload Object fail");
            runningResult = "S3 Upload Object fail: " + result.Exception.Message;;
        }
    }

    void OnGUI ()
    {

        if(string.IsNullOrEmpty(bucketName))
        {
            GUILayout.Space(20);
            GUILayout.Label("You must provide bucketName to run the sample code");
            return;
        } 
        
        if(string.IsNullOrEmpty(uploadKey))
        {
            GUILayout.Space(20);
            GUILayout.Label("You must provide uploadKey to run the sample code");
            return;
        } 
        
        if(string.IsNullOrEmpty(downloadKey))
        {
            Debug.LogError("You must provide downloadKey to run the sample code");
            return;
        }        
        
        if(string.IsNullOrEmpty(uploadSrcFilePath))
        {
            GUILayout.Space(20);
            GUILayout.Label("You must provide uploadSrcFilePath to run the sample code");
            return;
        } 

        
        
        GUILayout.BeginArea (new Rect (0, 0, Screen.width * 0.5f, Screen.height));
        GUILayout.Label ("S3 Operations");
        
        // List all buckets
        if (GUILayout.Button ("list bucket", GUILayout.MinHeight (Screen.height * 0.2f), GUILayout.Width (Screen.width * 0.4f)))
        {
            ListBucketsRequest request = new ListBucketsRequest ();
            S3Client.ListBucketsAsync (request, ListBucketCallback,null);
        }

        // List all objects in the bucket
        if (GUILayout.Button ("list object", GUILayout.MinHeight (Screen.height * 0.2f), GUILayout.Width (Screen.width * 0.4f)))
        {
            var request = new ListObjectsRequest ()
            {
                BucketName = bucketName,
            };
            S3Client.ListObjectsAsync (request, ListObjectsCallback,null);
        }
            
        // Download object
        if (GUILayout.Button ("download object", GUILayout.MinHeight (Screen.height * 0.2f), GUILayout.Width (Screen.width * 0.4f)))
        {
            var request = new GetObjectRequest ()
            {
                BucketName = bucketName,
                Key = downloadKey,
            };
            S3Client.GetObjectAsync (request, GetObjectCallback,null);
        }

        // Upload Object
        if (GUILayout.Button ("upload object", GUILayout.MinHeight (Screen.height * 0.2f), GUILayout.Width (Screen.width * 0.4f)))
        {
            Stream stream = null;
            try
            {
                stream = new FileStream(uploadSrcFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (IOException e)
            {
                Debug.LogException(e);
                runningResult = e.Message;
                return;
            }
            
            var postRequest = new PostObjectRequest 
            {
                Key = uploadKey,
                Bucket = bucketName,
                InputStream = stream,
            };
            S3Client.PostObjectAsync (postRequest, PostObjectCallback,null);
        }
        GUILayout.EndArea ();
        
        
        GUILayout.BeginArea (new Rect (Screen.width * 0.55f, 0, Screen.width * 0.45f, Screen.height));
        GUILayout.Label ("Result");
        
        // Display Running Result
        if (runningResult != null)
        {             
            GUILayout.TextField (runningResult, GUILayout.MinHeight (Screen.height * 0.3f), GUILayout.Width (Screen.width * 0.4f));
        }
        GUILayout.EndArea ();  
    }
}