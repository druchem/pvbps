using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ex3_AlternateStream
{
	internal class Native
	{
		[Flags]
		public enum EFileAccess : uint
		{
			AccessSystemSecurity = 0x1000000, // AccessSystemAcl access type
			MaximumAllowed = 0x2000000, // MaximumAllowed access type

			Delete = 0x10000,
			ReadControl = 0x20000,
			WriteDac = 0x40000,
			WriteOwner = 0x80000,
			Synchronize = 0x100000,

			StandardRightsRequired = 0xF0000,
			StandardRightsRead = ReadControl,
			StandardRightsWrite = ReadControl,
			StandardRightsExecute = ReadControl,
			StandardRightsAll = 0x1F0000,
			SpecificRightsAll = 0xFFFF,

			FileReadData = 0x0001, // file & pipe
			FileListDirectory = 0x0001, // directory
			FileWriteData = 0x0002, // file & pipe
			FileAddFile = 0x0002, // directory
			FileAppendData = 0x0004, // file
			FileAddSubdirectory = 0x0004, // directory
			FileCreatePipeInstance = 0x0004, // named pipe
			FileReadEa = 0x0008, // file & directory
			FileWriteEa = 0x0010, // file & directory
			FileExecute = 0x0020, // file
			FileTraverse = 0x0020, // directory
			FileDeleteChild = 0x0040, // directory
			FileReadAttributes = 0x0080, // all
			FileWriteAttributes = 0x0100, // all

			GenericRead = 0x80000000,
			GenericWrite = 0x40000000,
			GenericExecute = 0x20000000,
			GenericAll = 0x10000000,

			FileAllAccess =
				StandardRightsRequired |
				Synchronize |
				0x1FF,

			FileGenericRead =
				StandardRightsRead |
				FileReadData |
				FileReadAttributes |
				FileReadEa |
				Synchronize,

			FileGenericWrite =
				StandardRightsWrite |
				FileWriteData |
				FileWriteAttributes |
				FileWriteEa |
				FileAppendData |
				Synchronize,

			FileGenericExecute =
				StandardRightsExecute |
				FileReadAttributes |
				FileExecute |
				Synchronize
		}

		[Flags]
		public enum EFileShare : uint
		{
			None = 0x00000000,
			Read = 0x00000001,
			Write = 0x00000002,
			Delete = 0x00000004
		}

		public enum ECreationDisposition : uint
		{
			New = 1,
			CreateAlways = 2,
			OpenExisting = 3,
			OpenAlways = 4,
			TruncateExisting = 5
		}

		[Flags]
		public enum EFileAttributes : uint
		{
			Readonly = 0x00000001,
			Hidden = 0x00000002,
			System = 0x00000004,
			Directory = 0x00000010,
			Archive = 0x00000020,
			Device = 0x00000040,
			Normal = 0x00000080,
			Temporary = 0x00000100,
			SparseFile = 0x00000200,
			ReparsePoint = 0x00000400,
			Compressed = 0x00000800,
			Offline = 0x00001000,
			NotContentIndexed = 0x00002000,
			Encrypted = 0x00004000,
			WriteThrough = 0x80000000,
			Overlapped = 0x40000000,
			NoBuffering = 0x20000000,
			RandomAccess = 0x10000000,
			SequentialScan = 0x08000000,
			DeleteOnClose = 0x04000000,
			BackupSemantics = 0x02000000,
			PosixSemantics = 0x01000000,
			OpenReparsePoint = 0x00200000,
			OpenNoRecall = 0x00100000,
			FirstPipeInstance = 0x00080000
		}


		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr CreateFileW(
			[MarshalAs(UnmanagedType.LPWStr)] string filename,
			[MarshalAs(UnmanagedType.U4)] EFileAccess access,
			[MarshalAs(UnmanagedType.U4)] EFileShare share,
			IntPtr securityAttributes,
			[MarshalAs(UnmanagedType.U4)] ECreationDisposition creationDisposition,
			[MarshalAs(UnmanagedType.U4)] EFileAttributes flagsAndAttributes,
			IntPtr templateFile);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadFile(IntPtr hFile, [Out] byte[] lpBuffer,
			uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);

		[DllImport("kernel32.dll")]
		public static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer,
			uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, IntPtr lpOverlapped);
	}
}