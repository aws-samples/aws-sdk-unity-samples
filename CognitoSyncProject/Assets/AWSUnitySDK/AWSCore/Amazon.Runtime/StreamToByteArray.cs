using System;
using System.IO;

namespace Amazon.Runtime
{

	public static class StreamToByteArray
	{

		public static byte[] Convert(System.IO.Stream stream, int bufferSize)
		{
			byte[] buffer = new byte[bufferSize];
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int readBytes;
				while ((readBytes = stream.Read(buffer, 0, buffer.Length)) > 0)
				{
					memoryStream.Write(buffer, 0, readBytes);
				}
				return memoryStream.ToArray();
			}
		}
	}
}
