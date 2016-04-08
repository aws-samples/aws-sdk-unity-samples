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
using UnityEngine.UI;
using Amazon.Kinesis;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon;
using System.Text;
using Amazon.Kinesis.Model;
using System.IO;

namespace AWSSDK.Examples
{
    public class KinesisExample : MonoBehaviour
    {
        public string IdentityPoolId = "";
 
        public string CognitoIdentityRegion = RegionEndpoint.USEast1.SystemName;

        private RegionEndpoint _CognitoIdentityRegion
        {
            get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
        }

        public string KinesisRegion = RegionEndpoint.USEast1.SystemName;

        private RegionEndpoint _KinesisRegion
        {
            get { return RegionEndpoint.GetBySystemName(KinesisRegion); }
        }

        public InputField StreamNameField = null;
        public InputField RecordField = null;
        public Button PutRecordButton = null;
        public Button ListStreamsButton = null;
        public Button DescribeStreamButton = null;
        public Text ResultText = null;

        void Start()
        {
            UnityInitializer.AttachToGameObject(this.gameObject);
            PutRecordButton.onClick.AddListener(() => { PutRecord(); });
            ListStreamsButton.onClick.AddListener(() => { ListStreams(); });
            DescribeStreamButton.onClick.AddListener(() => { DescribeStream(); });
        }

        #region private members

        private IAmazonKinesis _kinesisClient;
        private AWSCredentials _credentials;

        private AWSCredentials Credentials
        {
            get
            {
                if (_credentials == null)
                    _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
                return _credentials;
            }
        }

        private IAmazonKinesis Client
        {
            get
            {
                if (_kinesisClient == null)
                {
                    _kinesisClient = new AmazonKinesisClient(Credentials, _KinesisRegion);
                }
                return _kinesisClient;
            }
        }

        #endregion

        # region Put Record
        /// <summary>
        /// Example method to demostrate Kinesis PutRecord. Puts a record with the data specified
        /// in the "Record Data" Text Input Field to the stream specified in the "Stream Name"
        /// Text Input Field.
        /// </summary>
        public void PutRecord()
        {
            ResultText.text = string.Format("Putting record with data '{0}' to Kinesis stream '{1}'.", RecordField.text, StreamNameField.text);
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                streamWriter.Write(RecordField.text);
                Client.PutRecordAsync(new PutRecordRequest
                {
                    Data = memoryStream,
                    PartitionKey = "partitionKey",
                    StreamName = StreamNameField.text
                },
                (responseObject) =>
                {
                    ResultText.text += "\n";
                    if (responseObject.Exception == null)
                    {
                        ResultText.text += string.Format("Successfully put record with sequence number '{0}'.", responseObject.Response.SequenceNumber);
                    }
                    else
                    {
                        ResultText.text += responseObject.Exception + "\n";
                    }
                }
                );
            }
        }

        # endregion

        # region List Streams
        /// <summary>
        /// Example method to demostrate Kinesis ListStreams. Prints all of the Kinesis Streams
        /// that your Cognito Identity has access to.
        /// </summary>
        public void ListStreams()
        {
            ResultText.text = "Getting a list of Streams.";
            Client.ListStreamsAsync(new ListStreamsRequest(),
            (responseObject) =>
            {
                ResultText.text += "\n";
                if (responseObject.Exception == null)
                {
                    ResultText.text += "Stream names:\n";
                    foreach (string streamName in responseObject.Response.StreamNames)
                    {
                        ResultText.text += string.Format("  {0}\n", streamName);
                    }
                }
                else
                {
                    ResultText.text += responseObject.Exception + "\n";
                }
            }
            );
        }

        # endregion

        # region Describe Stream
        /// <summary>
        /// Example method to demostrate Kinesis DescribeStream. Prints information about the
        /// stream specified in the "Stream Name" Text Input Field.
        /// </summary>
        public void DescribeStream()
        {
            ResultText.text = string.Format("Describing Stream '{0}'.", StreamNameField.text);
            Client.DescribeStreamAsync(new DescribeStreamRequest()
            {
                StreamName = StreamNameField.text
            },
            (responseObject) =>
            {
                ResultText.text += "\n";
                if (responseObject.Exception == null)
                {
                    ResultText.text += "Stream description:\n";
                    ResultText.text += string.Format("  Stream Name = '{0}'\n", responseObject.Response.StreamDescription.StreamName);
                    ResultText.text += string.Format("  Stream ARN = '{0}'\n", responseObject.Response.StreamDescription.StreamARN);
                    ResultText.text += string.Format("  Stream Status = '{0}'\n", responseObject.Response.StreamDescription.StreamStatus.ToString());
                    string shardIDs = "";
                    foreach (var shard in responseObject.Response.StreamDescription.Shards)
                    {
                        shardIDs += string.Format("'{0}', ", shard.ShardId);
                    }
                    ResultText.text += string.Format("  Shard IDs: [{0}]", shardIDs);
                }
                else
                {
                    ResultText.text += responseObject.Exception + "\n";
                }
            }
            );
        }

        # endregion
    }
}
