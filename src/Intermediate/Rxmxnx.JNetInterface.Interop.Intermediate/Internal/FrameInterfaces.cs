namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
internal readonly struct LocalFrameAction(Action action) : IFrameAction
{
	void IFrameAction.Accept(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => action();
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
#if !NET9_0_OR_GREATER
internal readonly struct LocalFrameAction<TState>(TState state, Action<TState> action) : IFrameAction
{
	void IFrameAction.Accept(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => action(state);
#else
internal readonly ref struct LocalFrameAction<TState>(TState state, Action<TState> action)
	: IFrameAction where TState : allows ref struct
{
	private readonly TState _state = state;

	void IFrameAction.Accept(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => action(this._state);
#endif
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
internal readonly struct LocalFrameFunction<TResult>(Func<TResult> func) : IFrameFunction<TResult>
{
	TResult IFrameFunction<TResult>.Apply(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => func();
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
#if !NET9_0_OR_GREATER
internal readonly struct LocalFrameFunction<TResult, TState>(TState state, Func<TState, TResult> func)
	: IFrameFunction<TResult>
{
	TResult IFrameFunction<TResult>.Apply(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => func(state);
#else
internal readonly ref struct LocalFrameFunction<TResult, TState>(TState state, Func<TState, TResult> func)
	: IFrameFunction<TResult> where TState : allows ref struct
{
	private readonly TState _state = state;

	TResult IFrameFunction<TResult>.Apply(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => func(this._state);
#endif
}