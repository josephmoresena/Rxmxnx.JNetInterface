namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class UIAdapter
{
	private sealed partial class CoreFoundationAdapter : ConsoleAdapter
	{
		public override unsafe void ExecuteGui<TState>(in TState state, in Action<TState> action)
		{
			CoreFoundationAdapter.loopRef = CoreFoundationAdapter.RunLoopGetCurrent();
			RunLoopSourceContext context = new()
			{
				Version = 0,
				Info = IntPtr.Zero,
				Retain = IntPtr.Zero,
				Release = IntPtr.Zero,
				CopyDescription = IntPtr.Zero,
				Equal = IntPtr.Zero,
				Hash = IntPtr.Zero,
				Schedule = IntPtr.Zero,
				Cancel = IntPtr.Zero,
				Perform = &CoreFoundationAdapter.DummyCallback,
			};
			CoreFoundationAdapter.sourceRef = CoreFoundationAdapter.RunLoopSourceCreate(IntPtr.Zero, 0, ref context);
			CoreFoundationAdapter.RunLoopAddSource(CoreFoundationAdapter.loopRef, CoreFoundationAdapter.sourceRef,
			                                       CoreFoundationAdapter.GetCommonModes());
			MacExecutionState<TState> executionState = new(in state, in action);
			this.ExecuteGui(executionState);
			CoreFoundationAdapter.ReleaseSource(CoreFoundationAdapter.sourceRef);
		}

		private protected override void BlockThread() => CoreFoundationAdapter.RunLoopRun();
	}
}