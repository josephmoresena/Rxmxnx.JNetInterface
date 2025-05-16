using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.References;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class UIAdapter
{
	private class ConsoleAdapter : UIAdapter
	{
		public override void ShowError(String errorMessage) => Console.WriteLine(errorMessage);
		public override void ShowError(Exception ex) => Console.WriteLine(ex);
		public override void PrintThreadInfo(JEnvironmentRef environmentRef)
			=> Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId}, {environmentRef}.");
		public override void PrintArgs(JVirtualMachineInitArg jvmLib) => Console.WriteLine(jvmLib);
	}
}