#if NET9_0_OR_GREATER
namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
	/// <inheritdoc cref="Rxmxnx.JNetInterface.Internal.AndroidNativeAction{TState}"/>
	private readonly ref struct AndroidNativeAction<TState>(TState state, AndroidJniAction<TState> action)
		: IAndroidNativeAction where TState : allows ref struct
	{
		private readonly TState _state = state;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void IAndroidNativeAction.Accept(AndroidJniContext context) => action(context, this._state);
	}

	/// <inheritdoc cref="Rxmxnx.JNetInterface.Internal.AndroidNativeFunction{TResult, TState}"/>
	private readonly ref struct AndroidNativeFunction<TResult, TState>(
		TState state,
		AndroidJniFunc<TState, TResult> func) : IAndroidNativeFunction<TResult> where TState : allows ref struct
	{
		private readonly TState _state = state;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		TResult IAndroidNativeFunction<TResult>.Apply(AndroidJniContext context) => func(context, this._state);
	}
}
#endif