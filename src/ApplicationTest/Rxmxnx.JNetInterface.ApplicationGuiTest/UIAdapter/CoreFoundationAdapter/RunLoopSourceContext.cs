using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class UIAdapter
{
	private sealed unsafe partial class CoreFoundationAdapter
	{
		[StructLayout(LayoutKind.Sequential)]
		private struct RunLoopSourceContext
		{
			public IntPtr Version;
			public IntPtr Info;
			public IntPtr Retain;
			public IntPtr Release;
			public IntPtr CopyDescription;
			public IntPtr Equal;
			public IntPtr Hash;
			public IntPtr Schedule;
			public IntPtr Cancel;
			public delegate* unmanaged<IntPtr, void> Perform;
		}
	}
}