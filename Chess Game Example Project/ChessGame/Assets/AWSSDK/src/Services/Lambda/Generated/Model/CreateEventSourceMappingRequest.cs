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
 * Do not modify this file. This file is generated from the lambda-2015-03-31.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.Lambda.Model
{
    /// <summary>
    /// Container for the parameters to the CreateEventSourceMapping operation.
    /// Identifies a stream as an event source for a Lambda function. It can be either an
    /// Amazon Kinesis stream or an Amazon DynamoDB stream. AWS Lambda invokes the specified
    /// function when records are posted to the stream.
    /// 
    ///  
    /// <para>
    /// This is the pull model, where AWS Lambda invokes the function. For more information,
    /// go to <a href="http://docs.aws.amazon.com/lambda/latest/dg/lambda-introduction.html">AWS
    /// Lambda: How it Works</a> in the <i>AWS Lambda Developer Guide</i>.
    /// </para>
    ///  
    /// <para>
    /// This association between an Amazon Kinesis stream and a Lambda function is called
    /// the event source mapping. You provide the configuration information (for example,
    /// which stream to read from and which Lambda function to invoke) for the event source
    /// mapping in the request body.
    /// </para>
    ///  
    /// <para>
    ///  Each event source, such as an Amazon Kinesis or a DynamoDB stream, can be associated
    /// with multiple AWS Lambda function. A given Lambda function can be associated with
    /// multiple AWS event sources. 
    /// </para>
    ///  
    /// <para>
    /// This operation requires permission for the <code>lambda:CreateEventSourceMapping</code>
    /// action.
    /// </para>
    /// </summary>
    public partial class CreateEventSourceMappingRequest : AmazonLambdaRequest
    {
        private int? _batchSize;
        private bool? _enabled;
        private string _eventSourceArn;
        private string _functionName;
        private EventSourcePosition _startingPosition;

        /// <summary>
        /// Gets and sets the property BatchSize. 
        /// <para>
        /// The largest number of records that AWS Lambda will retrieve from your event source
        /// at the time of invoking your function. Your function receives an event with all the
        /// retrieved records. The default is 100 records.
        /// </para>
        /// </summary>
        public int BatchSize
        {
            get { return this._batchSize.GetValueOrDefault(); }
            set { this._batchSize = value; }
        }

        // Check to see if BatchSize property is set
        internal bool IsSetBatchSize()
        {
            return this._batchSize.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property Enabled. 
        /// <para>
        /// Indicates whether AWS Lambda should begin polling the event source, the default is
        /// not enabled. 
        /// </para>
        /// </summary>
        public bool Enabled
        {
            get { return this._enabled.GetValueOrDefault(); }
            set { this._enabled = value; }
        }

        // Check to see if Enabled property is set
        internal bool IsSetEnabled()
        {
            return this._enabled.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property EventSourceArn. 
        /// <para>
        /// The Amazon Resource Name (ARN) of the Amazon Kinesis or the Amazon DynamoDB stream
        /// that is the event source. Any record added to this stream could cause AWS Lambda to
        /// invoke your Lambda function, it depends on the <code>BatchSize</code>. AWS Lambda
        /// POSTs the Amazon Kinesis event, containing records, to your Lambda function as JSON.
        /// </para>
        /// </summary>
        public string EventSourceArn
        {
            get { return this._eventSourceArn; }
            set { this._eventSourceArn = value; }
        }

        // Check to see if EventSourceArn property is set
        internal bool IsSetEventSourceArn()
        {
            return this._eventSourceArn != null;
        }

        /// <summary>
        /// Gets and sets the property FunctionName. 
        /// <para>
        /// The Lambda function to invoke when AWS Lambda detects an event on the stream.
        /// </para>
        ///  
        /// <para>
        ///  You can specify an unqualified function name (for example, "Thumbnail") or you can
        /// specify Amazon Resource Name (ARN) of the function (for example, "arn:aws:lambda:us-west-2:account-id:function:ThumbNail").
        /// AWS Lambda also allows you to specify only the account ID qualifier (for example,
        /// "account-id:Thumbnail"). Note that the length constraint applies only to the ARN.
        /// If you specify only the function name, it is limited to 64 character in length. 
        /// </para>
        /// </summary>
        public string FunctionName
        {
            get { return this._functionName; }
            set { this._functionName = value; }
        }

        // Check to see if FunctionName property is set
        internal bool IsSetFunctionName()
        {
            return this._functionName != null;
        }

        /// <summary>
        /// Gets and sets the property StartingPosition. 
        /// <para>
        /// The position in the stream where AWS Lambda should start reading. For more information,
        /// go to <a href="http://docs.aws.amazon.com/kinesis/latest/APIReference/API_GetShardIterator.html#Kinesis-GetShardIterator-request-ShardIteratorType">ShardIteratorType</a>
        /// in the <i>Amazon Kinesis API Reference</i>. 
        /// </para>
        /// </summary>
        public EventSourcePosition StartingPosition
        {
            get { return this._startingPosition; }
            set { this._startingPosition = value; }
        }

        // Check to see if StartingPosition property is set
        internal bool IsSetStartingPosition()
        {
            return this._startingPosition != null;
        }

    }
}