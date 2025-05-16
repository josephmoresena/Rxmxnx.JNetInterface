using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class UIAdapter
{
	private protected class ExecutionState<TState>(in TState state, in Action<TState> action)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		private readonly ReadOnlyValPtr<Action<TState>> _actionPtr = NativeUtilities.GetUnsafeValPtr(in action);
		private readonly ReadOnlyValPtr<TState> _statePtr = NativeUtilities.GetUnsafeValPtr(in state);

		public void Execute()
		{
			this._actionPtr.Reference(this._statePtr.Reference);
			this.FinalizeExecution();
		}
		protected virtual void FinalizeExecution() { }
	}
}