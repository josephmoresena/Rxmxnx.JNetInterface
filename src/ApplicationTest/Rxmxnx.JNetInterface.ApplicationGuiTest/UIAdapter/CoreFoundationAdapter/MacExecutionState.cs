namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class UIAdapter
{
	private sealed partial class CoreFoundationAdapter
	{
		private sealed class MacExecutionState<TState>(in TState state, in Action<TState> action)
			: ExecutionState<TState>(in state, in action)
#if NET9_0_OR_GREATER
			where TState : allows ref struct
#endif
		{
			protected override void FinalizeExecution()
			{
				Environment.Exit(0); // Finalize application.
			}
		}
	}
}