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
using System.Globalization;
using System.IO;
using System.Text;
using ThirdParty.Json.LitJson;
using Amazon.Runtime.Internal.Util;

namespace Amazon.Runtime.Internal.Transform
{
    /// <summary>
    /// Wraps a json string for unmarshalling.
    /// 
    /// Each <c>Read()</c> operation gets the next token.
    /// <c>TestExpression()</c> is used to match the current key-chain
    /// to an xpath expression. The general pattern looks like this:
    /// <code>
    /// JsonUnmarshallerContext context = new JsonUnmarshallerContext(jsonString);
    /// while (context.Read())
    /// {
    ///     if (context.IsKey)
    ///     {
    ///         if (context.TestExpresion("path/to/element"))
    ///         {
    ///             myObject.stringMember = stringUnmarshaller.GetInstance().Unmarshall(context);
    ///             continue;
    ///         }
    ///     }
    /// }
    /// </code>
    /// </summary>
    public class JsonUnmarshallerContext : UnmarshallerContext
    {
        private const string DELIMITER = "/";
        #region Private members

        private StreamReader streamReader = null;
        private JsonReader jsonReader = null;
        private JsonPathStack stack = new JsonPathStack();
        private string currentField;
        private JsonToken? currentToken = null;
        private bool disposed = false;
        private bool wasPeeked = false;
        #endregion

        #region Constructors

        /// <summary>
        /// Wrap the jsonstring for unmarshalling.
        /// </summary>
        /// <param name="responseStream">Stream that contains the JSON for unmarshalling</param>
        /// <param name="maintainResponseBody"> If set to true, maintains a copy of the complete response body as the stream is being read.</param>
        /// <param name="responseData">Response data coming back from the request</param>
        public JsonUnmarshallerContext(Stream responseStream, bool maintainResponseBody, IWebResponseData responseData)
        {
            if (maintainResponseBody)
            {
                this.WrappingStream = new CachingWrapperStream(responseStream);
                responseStream = this.WrappingStream;
            }
            
            this.WebResponseData = responseData;
            this.MaintainResponseBody = maintainResponseBody;

            long contentLength;
            if (responseData != null && long.TryParse(responseData.GetHeaderValue("Content-Length"), out contentLength))
            {
                base.SetupCRCStream(responseData, responseStream, contentLength);
            }

            if (this.CrcStream != null)
                streamReader = new StreamReader(this.CrcStream);
            else
                streamReader = new StreamReader(responseStream);

            jsonReader = new JsonReader(streamReader);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Are we at the start of the json document.
        /// </summary>
        public override bool IsStartOfDocument
        {
            get
            {
                return (CurrentTokenType == JsonToken.None) && (!streamReader.EndOfStream);
            }
        }

        /// <summary>
        /// Is the current token the end of an object
        /// </summary>    
        public override bool IsEndElement
        {
            get { return CurrentTokenType == JsonToken.ObjectEnd; }
        }

        /// <summary>
        /// Is the current token the start of an object
        /// </summary>
        public override bool IsStartElement
        {
            get { return CurrentTokenType == JsonToken.ObjectStart; }
        }

        /// <summary>
        ///     Returns the element depth of the parser's current position in the json
        ///     document being parsed.
        /// </summary>
        public override int CurrentDepth
        {
            get
            {
                return this.stack.CurrentDepth;
            }
        }

        /// <summary>
        /// The current Json path that is being unmarshalled.
        /// </summary>
        public override string CurrentPath
        {
            get 
            {
                return this.stack.CurrentPath; 
            }
        }

        /// <summary>
        ///     Reads to the next token in the json document, and updates the context
        ///     accordingly.
        /// </summary>
        /// <returns>
        ///     True if a token was read, false if there are no more tokens to read.
        /// </returns>
        public override bool Read()
        {
            if (wasPeeked)
            {
                wasPeeked = false;
                return currentToken == null;
            }
            
            bool result = jsonReader.Read();
            if (result)
            {
                currentToken = jsonReader.Token;
                UpdateContext();
            }
            else
            {
                currentToken = null;
            }
            wasPeeked = false;
            return result;
        }

        /// <summary>
        /// Peeks at the next token. This peek implementation
        /// reads the next token and makes the subsequent Read() return the same data.
        /// If Peek is called successively, it will return the same data.
        /// Only the first one calls Read(), subsequent calls 
        /// will return the same data until a Read() call is made.
        /// </summary>
        /// <param name="token">Token to peek.</param>
        /// <returns>Returns true if the peeked token matches given token.</returns>
        public bool Peek(JsonToken token)
        {
            if (wasPeeked)
                return currentToken != null && currentToken == token;

            if (Read())
            {
                wasPeeked = true;
                return currentToken == token;
            }
            return false;
        }

        /// <summary>
        ///     Returns the text contents of the current token being parsed.
        /// </summary>
        /// <returns>
        ///     The text contents of the current token being parsed.
        /// </returns>
        public override string ReadText()
        {
            object data = jsonReader.Value;
            string text;
            switch (currentToken)
            {
                case JsonToken.Null:
                    text = null;
                    break;
                case JsonToken.String:
                case JsonToken.PropertyName:
                    text = data as string;
                    break;
                case JsonToken.Boolean:                
                case JsonToken.Int:
                case JsonToken.Long:
                    IFormattable iformattable = data as IFormattable;
                    if (iformattable != null)
                        text = iformattable.ToString(null, CultureInfo.InvariantCulture);
                    else
                        text = data.ToString();
                    break;
                case JsonToken.Double:
                    var formattable = data as IFormattable;
                    if (formattable != null)
                        text = formattable.ToString("R", CultureInfo.InvariantCulture);
                    else
                        text = data.ToString();
                    break;
                default:
                    throw new AmazonClientException(
                            "We expected a VALUE token but got: " + currentToken);
            }
            return text;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The type of the current token
        /// </summary>
        public JsonToken CurrentTokenType
        {
            get { return currentToken.Value; }
        }

        #endregion

        #region Internal methods/properties

        /// <summary>
        /// Get the base stream of the jsonStream.
        /// </summary>
        public Stream Stream
        {
            get { return streamReader.BaseStream; }
        }

        /// <summary>
        /// Peeks at the next (non-whitespace) character in the jsonStream.
        /// </summary>
        /// <returns>The next (non-whitespace) character in the jsonStream, or -1 if at the end.</returns>
        public int Peek()
        {
            while (Char.IsWhiteSpace((char)StreamPeek()))
            {
                streamReader.Read();
            }
            return StreamPeek();

        }

        #endregion

        #region Private methods

        /// <summary>
        /// Peeks at the next character in the stream.
        /// If the data isn't buffered into the StreamReader (Peek() returns -1),
        /// we flush the buffered data and try one more time.
        /// </summary>
        /// <returns></returns>
        private int StreamPeek()
        {
            int peek = streamReader.Peek();
            if (peek == -1)
            {
                streamReader.DiscardBufferedData();
                peek = streamReader.Peek();
            }
            return peek;
        }

        private void UpdateContext()
        {
            if (!currentToken.HasValue) return;

            if (currentToken.Value == JsonToken.ObjectStart || currentToken.Value == JsonToken.ArrayStart)
            {
                // Push '/' for object start and array start.
                stack.Push(DELIMITER);
            }
            else if (currentToken.Value == JsonToken.ObjectEnd || currentToken.Value == JsonToken.ArrayEnd)
            {
                if (object.ReferenceEquals(stack.Peek(),DELIMITER))
                {
                    // Pop '/' associated with corresponding object start and array start.
                    stack.Pop();
                    if (stack.Count > 0 && ! object.ReferenceEquals(stack.Peek(),DELIMITER))
                    {
                        // Pop the property name associated with the
                        // object or array if present.
                        // e.g. {"a":["1","2","3"]}
                        stack.Pop();
                    }
                }
                currentField = null;
            }
            else if (currentToken.Value == JsonToken.PropertyName)
            {
                string t = ReadText();
                currentField = t;
                // Push property name, it's appended to the stack's CurrentPath,
                // it this does not affect the depth.
                stack.Push(currentField);
            }
            else if (currentToken.Value != JsonToken.None && !stack.CurrentPath.EndsWith(DELIMITER, StringComparison.OrdinalIgnoreCase))
            {
                // Pop if you encounter a simple data type or null
                // This will pop the property name associated with it in cases like  {"a":"b"}.
                // Exclude the case where it's a value in an array so we dont end poping the start of array and
                // property name e.g. {"a":["1","2","3"]}
                stack.Pop();
            }

        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (streamReader != null)
                    {
                        streamReader.Dispose();
                        streamReader = null;
                    }
                }
                disposed = true;
            }

            base.Dispose(disposing);
        }

        class JsonPathStack
        {
            private Stack<string> stack = new Stack<string>();
            int currentDepth = 0;
            private StringBuilder stackStringBuilder = new StringBuilder(128);
            private string stackString;

            public int CurrentDepth
            {
                get { return this.currentDepth; }
            }

            public string CurrentPath
            {
                get
                {
                    if (this.stackString == null)
                        this.stackString = this.stackStringBuilder.ToString();

                    return this.stackString;
                }
            }

            public void Push(string value)
            {
                if (value == "/")
                    currentDepth++;

                stackStringBuilder.Append(value);
                stackString = null;

                stack.Push(value);
            }

            public string Pop()
            {
                var value = this.stack.Pop();
                if (value == "/")
                    currentDepth--;

                stackStringBuilder.Remove(stackStringBuilder.Length - value.Length, value.Length);
                stackString = null;

                return value;
            }

            public string Peek()
            {
                return this.stack.Peek();
            }

            public int Count
            {
                get { return this.stack.Count; }
            }
        }
    }
}
