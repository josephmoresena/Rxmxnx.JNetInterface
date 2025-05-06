using System.Diagnostics;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class UIAdapter
{
	private sealed class MessageBoxAdapter : UIAdapter
	{
		[DllImport("user32.dll", EntryPoint = "MessageBoxW")]
		private static extern Int32 MessageBox(IntPtr hwnd, ReadOnlyValPtr<Char> text, ReadOnlyValPtr<Char> caption,
			UInt32 type);

		public override void ShowError(String errorMessage)
		{
			using IReadOnlyFixedMemory<Char>.IDisposable fMem = errorMessage.AsMemory().GetFixedContext();
			_ = MessageBoxAdapter.MessageBox(IntPtr.Zero, fMem.ValuePointer, "Error".AsSpan().GetUnsafeValPtr(), 0);
		}
		public override void ShowError(Exception ex) => this.ShowError(ex.ToString());
		public override void PrintThreadInfo(JEnvironmentRef environmentRef)
			=> Trace.WriteLine($"Thread: {Environment.CurrentManagedThreadId}, {environmentRef}.");
		public override void PrintArgs(JVirtualMachineInitArg jvmLib) => Trace.WriteLine(jvmLib);
	}
}