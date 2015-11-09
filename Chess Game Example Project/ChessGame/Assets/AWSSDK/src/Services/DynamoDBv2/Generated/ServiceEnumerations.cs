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
 * Do not modify this file. This file is generated from the dynamodb-2012-08-10.normal.json service model.
 */

using System;

using Amazon.Runtime;

namespace Amazon.DynamoDBv2
{

    /// <summary>
    /// Constants used for properties of type AttributeAction.
    /// </summary>
    public class AttributeAction : ConstantClass
    {

        /// <summary>
        /// Constant ADD for AttributeAction
        /// </summary>
        public static readonly AttributeAction ADD = new AttributeAction("ADD");
        /// <summary>
        /// Constant DELETE for AttributeAction
        /// </summary>
        public static readonly AttributeAction DELETE = new AttributeAction("DELETE");
        /// <summary>
        /// Constant PUT for AttributeAction
        /// </summary>
        public static readonly AttributeAction PUT = new AttributeAction("PUT");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AttributeAction(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static AttributeAction FindValue(string value)
        {
            return FindValue<AttributeAction>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator AttributeAction(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type ComparisonOperator.
    /// </summary>
    public class ComparisonOperator : ConstantClass
    {

        /// <summary>
        /// Constant BEGINS_WITH for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator BEGINS_WITH = new ComparisonOperator("BEGINS_WITH");
        /// <summary>
        /// Constant BETWEEN for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator BETWEEN = new ComparisonOperator("BETWEEN");
        /// <summary>
        /// Constant CONTAINS for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator CONTAINS = new ComparisonOperator("CONTAINS");
        /// <summary>
        /// Constant EQ for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator EQ = new ComparisonOperator("EQ");
        /// <summary>
        /// Constant GE for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator GE = new ComparisonOperator("GE");
        /// <summary>
        /// Constant GT for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator GT = new ComparisonOperator("GT");
        /// <summary>
        /// Constant IN for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator IN = new ComparisonOperator("IN");
        /// <summary>
        /// Constant LE for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator LE = new ComparisonOperator("LE");
        /// <summary>
        /// Constant LT for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator LT = new ComparisonOperator("LT");
        /// <summary>
        /// Constant NE for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator NE = new ComparisonOperator("NE");
        /// <summary>
        /// Constant NOT_CONTAINS for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator NOT_CONTAINS = new ComparisonOperator("NOT_CONTAINS");
        /// <summary>
        /// Constant NOT_NULL for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator NOT_NULL = new ComparisonOperator("NOT_NULL");
        /// <summary>
        /// Constant NULL for ComparisonOperator
        /// </summary>
        public static readonly ComparisonOperator NULL = new ComparisonOperator("NULL");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ComparisonOperator(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static ComparisonOperator FindValue(string value)
        {
            return FindValue<ComparisonOperator>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator ComparisonOperator(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type ConditionalOperator.
    /// </summary>
    public class ConditionalOperator : ConstantClass
    {

        /// <summary>
        /// Constant AND for ConditionalOperator
        /// </summary>
        public static readonly ConditionalOperator AND = new ConditionalOperator("AND");
        /// <summary>
        /// Constant OR for ConditionalOperator
        /// </summary>
        public static readonly ConditionalOperator OR = new ConditionalOperator("OR");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ConditionalOperator(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static ConditionalOperator FindValue(string value)
        {
            return FindValue<ConditionalOperator>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator ConditionalOperator(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type IndexStatus.
    /// </summary>
    public class IndexStatus : ConstantClass
    {

        /// <summary>
        /// Constant ACTIVE for IndexStatus
        /// </summary>
        public static readonly IndexStatus ACTIVE = new IndexStatus("ACTIVE");
        /// <summary>
        /// Constant CREATING for IndexStatus
        /// </summary>
        public static readonly IndexStatus CREATING = new IndexStatus("CREATING");
        /// <summary>
        /// Constant DELETING for IndexStatus
        /// </summary>
        public static readonly IndexStatus DELETING = new IndexStatus("DELETING");
        /// <summary>
        /// Constant UPDATING for IndexStatus
        /// </summary>
        public static readonly IndexStatus UPDATING = new IndexStatus("UPDATING");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public IndexStatus(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static IndexStatus FindValue(string value)
        {
            return FindValue<IndexStatus>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator IndexStatus(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type KeyType.
    /// </summary>
    public class KeyType : ConstantClass
    {

        /// <summary>
        /// Constant HASH for KeyType
        /// </summary>
        public static readonly KeyType HASH = new KeyType("HASH");
        /// <summary>
        /// Constant RANGE for KeyType
        /// </summary>
        public static readonly KeyType RANGE = new KeyType("RANGE");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public KeyType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static KeyType FindValue(string value)
        {
            return FindValue<KeyType>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator KeyType(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type ProjectionType.
    /// </summary>
    public class ProjectionType : ConstantClass
    {

        /// <summary>
        /// Constant ALL for ProjectionType
        /// </summary>
        public static readonly ProjectionType ALL = new ProjectionType("ALL");
        /// <summary>
        /// Constant INCLUDE for ProjectionType
        /// </summary>
        public static readonly ProjectionType INCLUDE = new ProjectionType("INCLUDE");
        /// <summary>
        /// Constant KEYS_ONLY for ProjectionType
        /// </summary>
        public static readonly ProjectionType KEYS_ONLY = new ProjectionType("KEYS_ONLY");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProjectionType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static ProjectionType FindValue(string value)
        {
            return FindValue<ProjectionType>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator ProjectionType(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type ReturnConsumedCapacity.
    /// </summary>
    public class ReturnConsumedCapacity : ConstantClass
    {

        /// <summary>
        /// Constant INDEXES for ReturnConsumedCapacity
        /// </summary>
        public static readonly ReturnConsumedCapacity INDEXES = new ReturnConsumedCapacity("INDEXES");
        /// <summary>
        /// Constant NONE for ReturnConsumedCapacity
        /// </summary>
        public static readonly ReturnConsumedCapacity NONE = new ReturnConsumedCapacity("NONE");
        /// <summary>
        /// Constant TOTAL for ReturnConsumedCapacity
        /// </summary>
        public static readonly ReturnConsumedCapacity TOTAL = new ReturnConsumedCapacity("TOTAL");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ReturnConsumedCapacity(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static ReturnConsumedCapacity FindValue(string value)
        {
            return FindValue<ReturnConsumedCapacity>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator ReturnConsumedCapacity(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type ReturnItemCollectionMetrics.
    /// </summary>
    public class ReturnItemCollectionMetrics : ConstantClass
    {

        /// <summary>
        /// Constant NONE for ReturnItemCollectionMetrics
        /// </summary>
        public static readonly ReturnItemCollectionMetrics NONE = new ReturnItemCollectionMetrics("NONE");
        /// <summary>
        /// Constant SIZE for ReturnItemCollectionMetrics
        /// </summary>
        public static readonly ReturnItemCollectionMetrics SIZE = new ReturnItemCollectionMetrics("SIZE");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ReturnItemCollectionMetrics(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static ReturnItemCollectionMetrics FindValue(string value)
        {
            return FindValue<ReturnItemCollectionMetrics>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator ReturnItemCollectionMetrics(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type ReturnValue.
    /// </summary>
    public class ReturnValue : ConstantClass
    {

        /// <summary>
        /// Constant ALL_NEW for ReturnValue
        /// </summary>
        public static readonly ReturnValue ALL_NEW = new ReturnValue("ALL_NEW");
        /// <summary>
        /// Constant ALL_OLD for ReturnValue
        /// </summary>
        public static readonly ReturnValue ALL_OLD = new ReturnValue("ALL_OLD");
        /// <summary>
        /// Constant NONE for ReturnValue
        /// </summary>
        public static readonly ReturnValue NONE = new ReturnValue("NONE");
        /// <summary>
        /// Constant UPDATED_NEW for ReturnValue
        /// </summary>
        public static readonly ReturnValue UPDATED_NEW = new ReturnValue("UPDATED_NEW");
        /// <summary>
        /// Constant UPDATED_OLD for ReturnValue
        /// </summary>
        public static readonly ReturnValue UPDATED_OLD = new ReturnValue("UPDATED_OLD");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ReturnValue(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static ReturnValue FindValue(string value)
        {
            return FindValue<ReturnValue>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator ReturnValue(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type ScalarAttributeType.
    /// </summary>
    public class ScalarAttributeType : ConstantClass
    {

        /// <summary>
        /// Constant B for ScalarAttributeType
        /// </summary>
        public static readonly ScalarAttributeType B = new ScalarAttributeType("B");
        /// <summary>
        /// Constant N for ScalarAttributeType
        /// </summary>
        public static readonly ScalarAttributeType N = new ScalarAttributeType("N");
        /// <summary>
        /// Constant S for ScalarAttributeType
        /// </summary>
        public static readonly ScalarAttributeType S = new ScalarAttributeType("S");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ScalarAttributeType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static ScalarAttributeType FindValue(string value)
        {
            return FindValue<ScalarAttributeType>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator ScalarAttributeType(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type Select.
    /// </summary>
    public class Select : ConstantClass
    {

        /// <summary>
        /// Constant ALL_ATTRIBUTES for Select
        /// </summary>
        public static readonly Select ALL_ATTRIBUTES = new Select("ALL_ATTRIBUTES");
        /// <summary>
        /// Constant ALL_PROJECTED_ATTRIBUTES for Select
        /// </summary>
        public static readonly Select ALL_PROJECTED_ATTRIBUTES = new Select("ALL_PROJECTED_ATTRIBUTES");
        /// <summary>
        /// Constant COUNT for Select
        /// </summary>
        public static readonly Select COUNT = new Select("COUNT");
        /// <summary>
        /// Constant SPECIFIC_ATTRIBUTES for Select
        /// </summary>
        public static readonly Select SPECIFIC_ATTRIBUTES = new Select("SPECIFIC_ATTRIBUTES");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Select(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static Select FindValue(string value)
        {
            return FindValue<Select>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator Select(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type StreamViewType.
    /// </summary>
    public class StreamViewType : ConstantClass
    {

        /// <summary>
        /// Constant KEYS_ONLY for StreamViewType
        /// </summary>
        public static readonly StreamViewType KEYS_ONLY = new StreamViewType("KEYS_ONLY");
        /// <summary>
        /// Constant NEW_AND_OLD_IMAGES for StreamViewType
        /// </summary>
        public static readonly StreamViewType NEW_AND_OLD_IMAGES = new StreamViewType("NEW_AND_OLD_IMAGES");
        /// <summary>
        /// Constant NEW_IMAGE for StreamViewType
        /// </summary>
        public static readonly StreamViewType NEW_IMAGE = new StreamViewType("NEW_IMAGE");
        /// <summary>
        /// Constant OLD_IMAGE for StreamViewType
        /// </summary>
        public static readonly StreamViewType OLD_IMAGE = new StreamViewType("OLD_IMAGE");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public StreamViewType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static StreamViewType FindValue(string value)
        {
            return FindValue<StreamViewType>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator StreamViewType(string value)
        {
            return FindValue(value);
        }
    }


    /// <summary>
    /// Constants used for properties of type TableStatus.
    /// </summary>
    public class TableStatus : ConstantClass
    {

        /// <summary>
        /// Constant ACTIVE for TableStatus
        /// </summary>
        public static readonly TableStatus ACTIVE = new TableStatus("ACTIVE");
        /// <summary>
        /// Constant CREATING for TableStatus
        /// </summary>
        public static readonly TableStatus CREATING = new TableStatus("CREATING");
        /// <summary>
        /// Constant DELETING for TableStatus
        /// </summary>
        public static readonly TableStatus DELETING = new TableStatus("DELETING");
        /// <summary>
        /// Constant UPDATING for TableStatus
        /// </summary>
        public static readonly TableStatus UPDATING = new TableStatus("UPDATING");

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TableStatus(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Finds the constant for the unique value.
        /// </summary>
        /// <param name="value">The unique value for the constant</param>
        /// <returns>The constant for the unique value</returns>
        public static TableStatus FindValue(string value)
        {
            return FindValue<TableStatus>(value);
        }

        /// <summary>
        /// Utility method to convert strings to the constant class.
        /// </summary>
        /// <param name="value">The string value to convert to the constant class.</param>
        /// <returns></returns>
        public static implicit operator TableStatus(string value)
        {
            return FindValue(value);
        }
    }

}