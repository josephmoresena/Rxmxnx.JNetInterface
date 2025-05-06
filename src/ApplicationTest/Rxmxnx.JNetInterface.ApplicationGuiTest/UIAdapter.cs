using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.References;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal abstract partial class UIAdapter
{
	public static readonly UIAdapter Instance = OperatingSystem.IsWindows() ? new MessageBoxAdapter() :
		OperatingSystem.IsMacOS() || OperatingSystem.IsIOS() || OperatingSystem.IsTvOS() ? new CoreFoundationAdapter() :
		new ConsoleAdapter();

	public virtual void ExecuteGui<TState>(in TState state, in Action<TState> action)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
		=> action(state);
	public abstract void ShowError(String errorMessage);
	public abstract void ShowError(Exception ex);
	public abstract void PrintThreadInfo(JEnvironmentRef environmentRef);
	public abstract void PrintArgs(JVirtualMachineInitArg jvmLib);
}