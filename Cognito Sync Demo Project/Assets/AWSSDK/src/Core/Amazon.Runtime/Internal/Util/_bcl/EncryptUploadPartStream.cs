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
using Amazon.Runtime;

namespace Amazon.Runtime.Internal.Util
{
    /// <summary>
    /// A wrapper stream that encrypts the base stream as it
    /// is being read.
    /// </summary>
    public abstract class EncryptUploadPartStream : WrapperStream
    {
        #region Properties

        protected IEncryptionWrapper Algorithm { get; set; }
        private byte[] internalBuffer;
        public byte[] InitializationVector { get; protected set; }
        internal const int InternalEncryptionBlockSize = 16;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an EncryptStream for Multipart uploads with an encryption algorithm and a base stream.
        /// </summary>
        /// <param name="baseStream">Stream to perform encryption on..</param>
        protected EncryptUploadPartStream(Stream baseStream)
            : base(baseStream)
        {
            internalBuffer = new byte[InternalEncryptionBlockSize];
            InitializationVector = new byte[InternalEncryptionBlockSize];
            ValidateBaseStream();
        }
        #endregion

        #region Stream overrides

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position
        /// within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes. When this method returns, the buffer contains the specified
        /// byte array with the values between offset and (offset + count - 1) replaced
        /// by the bytes read from the current source.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset in buffer at which to begin storing the data read
        /// from the current stream.
        /// </param>
        /// <param name="count">
        /// The maximum number of bytes to be read from the current stream.
        /// </param>
        /// <returns>
        /// The total number of bytes read into the buffer. This can be less than the
        /// number of bytes requested if that many bytes are not currently available,
        /// or zero (0) if the end of the stream has been reached.
        /// </returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            int readBytes = base.Read(buffer, offset, count);

            if (readBytes == 0)
                return 0;

            int numBytesRead = 0;
            while (numBytesRead < readBytes)
            {
                numBytesRead += Algorithm.AppendBlock(buffer, offset, InternalEncryptionBlockSize, internalBuffer, 0);
                Buffer.BlockCopy(internalBuffer, 0, buffer, offset, InternalEncryptionBlockSize);
                offset = offset + InternalEncryptionBlockSize;
            }

            Buffer.BlockCopy(buffer, numBytesRead - InternalEncryptionBlockSize, InitializationVector, 0, InternalEncryptionBlockSize);
            return numBytesRead;
        }

        public override void Close()
        {
            base.Close();
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports seeking.
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Returns encrypted content length.
        /// </summary>
        public override long Length
        {
            get
            {
                if (base.Length % InternalEncryptionBlockSize == 0)
                {
                    return (base.Length);
                }
                else
                {
                    return (base.Length + InternalEncryptionBlockSize - (base.Length % InternalEncryptionBlockSize));
                }
            }
        }

        /// <summary>
        /// Gets or sets the position within the current stream.
        /// </summary>
        public override long Position
        {
            get
            {
                return BaseStream.Position;
            }
            set
            {
                Seek(offset: value, origin: SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// Sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the origin parameter.</param>
        /// <param name="origin">
        /// A value of type System.IO.SeekOrigin indicating the reference point used
        /// to obtain the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            long position = BaseStream.Seek(offset, origin);
            this.Algorithm.Reset();

            return position;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Validates the underlying stream.
        /// </summary>
        private void ValidateBaseStream()
        {
            if (!BaseStream.CanRead && !BaseStream.CanWrite)
                throw new InvalidDataException("EncryptStreamForUploadPart does not support base streams that are not capable of reading or writing");
        }

        #endregion
    }


    /// <summary>
    /// A wrapper stream that encrypts the base stream as it
    /// is being read.
    /// </summary>   
    public class EncryptUploadPartStream<T> : EncryptUploadPartStream
            where T : class, IEncryptionWrapper, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes an EncryptStream with an encryption algorithm and a base stream.
        /// </summary>
        /// <param name="baseStream">Stream to perform encryption on..</param>
        /// <param name="key">Symmetric key to perform encryption</param>
        /// <param name="IV">Initialization vector to perform encryption</param>
        public EncryptUploadPartStream(Stream baseStream, byte[] key, byte[] IV)
            : base(baseStream)
        {
            Algorithm = new T();
            Algorithm.SetEncryptionData(key, IV);
            Algorithm.CreateEncryptor();
        }

        #endregion
    }


    /// <summary>
    /// A wrapper stream that encrypts the base stream as it
    /// is being read.
    /// </summary>   
    public class AESEncryptionUploadPartStream : EncryptUploadPartStream<EncryptionWrapperAES>
    {
        #region Constructors

        /// <summary>
        /// Initializes an AESEncryptionStream with a base stream.
        /// </summary>
        /// <param name="baseStream">Stream to perform encryption on..</param>
        /// <param name="key">Symmetric key to perform encryption</param>
        /// <param name="IV">Initialization vector to perform encryption</param>
        public AESEncryptionUploadPartStream(Stream baseStream, byte[] key, byte[] IV)
            : base(baseStream, key, IV) { }

        #endregion
    }

}
