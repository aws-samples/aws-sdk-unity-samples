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
using System;
using System.Collections.Generic;

using System.IO;
using Amazon.DynamoDBv2.Model;
using Amazon.Util;

namespace Amazon.DynamoDBv2.DocumentModel
{
    /// <summary>
    /// Expressions are used for conditional deletes and filtering for query and scan operations.
    /// </summary>
    public class Expression
    {
        private Dictionary<string, string> _expressionAttributeNames = new Dictionary<string, string>(StringComparer.Ordinal);
        private Dictionary<string, DynamoDBEntry> _expressionAttributeValues = new Dictionary<string, DynamoDBEntry>(StringComparer.Ordinal);

        internal bool IsSet
        {
            get { return this.ExpressionStatement != null; }
        }

        /// <summary>
        /// Gets and sets the property ExpressionStatement. "Price > :price" is an example expression statement. 
        /// :price is a variable which gets its value from the ExpressionAttributeValues collection. If this is used
        /// for deletes then it prevents the delete from happening if the Price attribute on the item is less then the passed
        /// in price. For query and scan it will only return back items where the Price attribute is greater then passed
        /// in price.
        /// </summary>
        public string ExpressionStatement
        {
            get;
            set;
        }



        /// <summary>
        /// Gets and sets the property ExpressionAttributeNames. This collection contains attribute names from the item
        /// that should be substituted in the expression when it is evaluated. For example the expression "#C &lt; #U" will 
        /// expect the attribute names to be added to this collection.
        /// <code>
        /// expression.ExpressionAttributeNames["#C"] = "CriticRating"
        /// expression.ExpressionAttributeNames["#U"] = "UserRating"
        /// </code>
        /// </summary>
        public Dictionary<string, string> ExpressionAttributeNames
        {
            get { return this._expressionAttributeNames; }
            set { this._expressionAttributeNames = value; }
        }

        /// <summary>
        /// Gets and sets the property ExpressionAttributeValues. This collection contains the values to be substituted in the expression. 
        /// For example the expression "Price > :price" will contain one entry in this collection a key of ":price". 
        /// <para>
        /// DynamoDBEntry contains many common implicit cast operations so assignment can be done with the basic .NET types.
        /// In the price example shown above the value to be used for the expression can be provided using the following code snippet:
        /// <code>
        /// expression.ExpressionAttributeNames[":price"] = 3.99;
        /// </code>
        /// </para>
        /// </summary>
        public Dictionary<string, DynamoDBEntry> ExpressionAttributeValues
        {
            get { return this._expressionAttributeValues; }
            set { this._expressionAttributeValues = value; }
        }

        internal  Dictionary<string, AttributeValue> ConvertToAttributeValues(DynamoDBEntryConversion conversion)
        {
            var convertedValues = new Dictionary<string, AttributeValue>();
            if (this._expressionAttributeValues != null)
            {
                foreach (var kvp in this.ExpressionAttributeValues)
                {
                    if (kvp.Value == null)
                        convertedValues[kvp.Key] = new AttributeValue { NULL = true };
                    else
                        convertedValues[kvp.Key] = kvp.Value.ConvertToAttributeValue(new DynamoDBEntry.AttributeConversionConfig(conversion));
                }
            }

            return convertedValues;
        }

        internal void ApplyExpression(ScanRequest request, DynamoDBEntryConversion conversion)
        {
            request.FilterExpression = this.ExpressionStatement;
            request.ExpressionAttributeNames = new Dictionary<string, string>(this.ExpressionAttributeNames);
            request.ExpressionAttributeValues = this.ConvertToAttributeValues(conversion);
        }

        internal void ApplyExpression(QueryRequest request, DynamoDBEntryConversion conversion)
        {
            request.FilterExpression = this.ExpressionStatement;
            request.ExpressionAttributeNames = new Dictionary<string, string>(this.ExpressionAttributeNames);
            request.ExpressionAttributeValues = this.ConvertToAttributeValues(conversion);
        }

        internal void ApplyExpression(DeleteItemRequest request, DynamoDBEntryConversion conversion)
        {
            request.ConditionExpression = this.ExpressionStatement;
            request.ExpressionAttributeNames = new Dictionary<string, string>(this.ExpressionAttributeNames);
            request.ExpressionAttributeValues = this.ConvertToAttributeValues(conversion);
        }

        internal void ApplyExpression(PutItemRequest request, DynamoDBEntryConversion conversion)
        {
            request.ConditionExpression = this.ExpressionStatement;
            request.ExpressionAttributeNames = new Dictionary<string, string>(this.ExpressionAttributeNames);
            request.ExpressionAttributeValues = this.ConvertToAttributeValues(conversion);
        }

        internal void ApplyExpression(UpdateItemRequest request, DynamoDBEntryConversion conversion)
        {
            request.ConditionExpression = this.ExpressionStatement;
            request.ExpressionAttributeNames = new Dictionary<string,string>(this.ExpressionAttributeNames);
            request.ExpressionAttributeValues = this.ConvertToAttributeValues(conversion);
        }

        internal static void ApplyExpression(QueryRequest request, DynamoDBEntryConversion conversion,
            Expression keyExpression, Expression filterExpression)
        {
            if (keyExpression == null)
                keyExpression = new Expression();
            if (filterExpression == null)
                filterExpression = new Expression();

            if (!keyExpression.IsSet && !filterExpression.IsSet)
                return;

            if (keyExpression.IsSet)
                request.KeyConditionExpression = keyExpression.ExpressionStatement;
            if (filterExpression.IsSet)
                request.FilterExpression = filterExpression.ExpressionStatement;

            var kean = keyExpression.ExpressionAttributeNames;
            var fean = filterExpression.ExpressionAttributeNames;
            var combinedEan = Common.Combine(kean, fean, StringComparer.Ordinal);
            request.ExpressionAttributeNames = combinedEan;

            var keav = new Document(keyExpression.ExpressionAttributeValues).ForceConversion(conversion);
            var feav = new Document(filterExpression.ExpressionAttributeValues).ForceConversion(conversion);
            var combinedEav = Common.Combine(keav, feav, null);
            request.ExpressionAttributeValues = ConvertToAttributeValues(combinedEav, conversion);
        }

        internal static Dictionary<string, AttributeValue> ConvertToAttributeValues(
            Dictionary<string, DynamoDBEntry> valueMap, DynamoDBEntryConversion conversion)
        {
            var convertedValues = new Dictionary<string, AttributeValue>();
            if (valueMap != null)
            {
                foreach (var kvp in valueMap)
                {
                    if (kvp.Value == null)
                        convertedValues[kvp.Key] = new AttributeValue { NULL = true };
                    else
                        convertedValues[kvp.Key] = kvp.Value.ConvertToAttributeValue(new DynamoDBEntry.AttributeConversionConfig(conversion));
                }
            }

            return convertedValues;
        }

    }
}
