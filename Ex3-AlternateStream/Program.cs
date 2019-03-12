using System;
using System.Text;

namespace Ex3_AlternateStream
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			const string TestString = "Test data";
			const string TestFile = "test_file.txt";
			const string AlternateStreamName = "alt";

			var writeData = Encoding.UTF8.GetBytes(TestString);
			WriteStream(TestFile, AlternateStreamName, writeData);

			var readData = ReadStream(TestFile, AlternateStreamName);
			string readString = Encoding.UTF8.GetString(readData);

			string testResult = TestString == readString ? "successful" : "failed";
			Console.WriteLine($"R/W test {testResult}");

			Console.ReadLine();
		}

		private static byte[] ReadStream(string filePath, string alternateStreamName)
		{
			var fileHandle = Native.CreateFileW(
				$"{filePath}:{alternateStreamName}",
				Native.EFileAccess.GenericRead,
				Native.EFileShare.None,
				IntPtr.Zero,
				Native.ECreationDisposition.OpenExisting,
				Native.EFileAttributes.Readonly,
				IntPtr.Zero
			);

			var buffer = new byte[4096]; // 4096b is enough for a test :)

			if (Native.ReadFile(fileHandle, buffer, (uint) buffer.Length, out uint readLength, IntPtr.Zero))
			{
				var readBuffer = new byte[readLength];

				Buffer.BlockCopy(buffer, 0, readBuffer, 0, readBuffer.Length);

				return readBuffer;
			}

			return null;
		}

		private static void WriteStream(string filePath, string alternateStreamName, byte[] data)
		{
			var fileHandle = Native.CreateFileW(
				$"{filePath}:{alternateStreamName}",
				Native.EFileAccess.GenericWrite,
				Native.EFileShare.None,
				IntPtr.Zero,
				Native.ECreationDisposition.CreateAlways,
				Native.EFileAttributes.Normal,
				IntPtr.Zero);

			Native.WriteFile(fileHandle, data, (uint) data.Length, out uint writtenLength, IntPtr.Zero);
		}
	}
}