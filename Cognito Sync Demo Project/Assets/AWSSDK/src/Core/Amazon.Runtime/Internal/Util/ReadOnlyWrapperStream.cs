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
using System.IO;

namespace Amazon.Runtime.Internal.Util
{
    /// <summary>
    /// Wrapper stream that only supports reading
    /// </summary>
    public class ReadOnlyWrapperStream : WrapperStream
    {
        public ReadOnlyWrapperStream(Stream baseStream)
            : base(baseStream)
        {
            if (!baseStream.CanRead)
                throw new InvalidOperationException("Base stream must be readable");
        }

        #region Overrides

#if !WIN_RT
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotSupportedException();
        }
        public override void EndWrite(IAsyncResult asyncResult)
        {
            throw new NotSupportedException();
        }
#endif

        public override bool CanRead
        {
            get { return true; }
        }
        public override bool CanSeek
        {
            get { return false; }
        }
        public override bool CanWrite
        {
            get { return false; }
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
        public override long Length
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public override long Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                
                throw new NotSupportedException();
            }
        }
        public override void Flush()
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    /// <summary>
    /// Partial wrapper stream that only supports reading
    /// </summary>
    public class PartialReadOnlyWrapperStream : ReadOnlyWrapperStream
    {
        private long _currentPosition;
        private long _size;

        public PartialReadOnlyWrapperStream(Stream baseStream, long size)
            : base(baseStream)
        {
            _currentPosition = 0;
            _size = size;
        }

        private long RemainingSize
        {
            get
            {
                long remaining = this._size - this._currentPosition;
                return remaining;
            }
        }

        #region Overrides

        public override int Read(byte[] buffer, int offset, int count)
        {
            long bytesToRead = Math.Min(count, RemainingSize);
            if (bytesToRead <= 0)
                return 0;
            bytesToRead = Math.Min(bytesToRead, int.MaxValue);

            int result = base.Read(buffer, offset, (int)bytesToRead);
            _currentPosition += result;
            return result;
        }

        public override long Length
        {
            get
            {
                return _size;
            }
        }

        public override long Position
        {
            get
            {
                return _currentPosition;
            }
        }

        #endregion
    }
}
