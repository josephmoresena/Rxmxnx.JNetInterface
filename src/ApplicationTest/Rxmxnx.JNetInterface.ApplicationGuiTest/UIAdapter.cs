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
		=> this.ExecuteGui(new ExecutionState<TState>(in state, in action));
	public abstract void ShowError(String errorMessage);
	public abstract void ShowError(Exception ex);
	public abstract void PrintThreadInfo(JEnvironmentRef environmentRef);
	public abstract void PrintArgs(JVirtualMachineInitArg jvmLib);

	private protected virtual void BlockThread() { }

	private protected void ExecuteGui<TState>(ExecutionState<TState> executionState)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		Thread thread = new(o =>
		{
			if (OperatingSystem.IsWindows())
			{
				Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
				Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
			}
			(o as ExecutionState<TState>)?.Execute();
		});
		thread.Start(executionState);
		this.BlockThread();
	}
}