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

namespace Amazon.CognitoSync.SyncManager
{
    /// <summary>
    /// This exception is thrown when an update fails due to conflicting states
    /// </summary>
    public class DataConflictException : DataStorageException
    {
        public DataConflictException()
            : base()
        {
        }

        public DataConflictException(string detailMessage, Exception ex)
            : base(detailMessage, ex)
        {
        }

        public DataConflictException(string detailMessage)
            : base(detailMessage)
        {
        }

        public DataConflictException(Exception ex)
            : base(ex.Message, ex)
        {
        }
    }
}

