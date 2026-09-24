namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
internal readonly struct FrameAction(Int32 capacity, Action action) : IFrameAction
{
	Int32 IFrameAction.RequiredCapacity => capacity;
	void IFrameAction.Accept(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => action();
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
#if !NET9_0_OR_GREATER
internal readonly struct FrameAction<TState>(Int32 capacity, TState state, Action<TState> action) : IFrameAction
{
	void IFrameAction.Accept(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => action(state);
#else
internal readonly ref struct FrameAction<TState>(Int32 capacity, TState state, Action<TState> action) : IFrameAction
	where TState : allows ref struct
{
	private readonly TState _state = state;

	void IFrameAction.Accept(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => action(this._state);
#endif
	Int32 IFrameAction.RequiredCapacity => capacity;
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
internal readonly struct FrameFunction<TResult>(Int32 capacity, Func<TResult> func) : IFrameFunction<TResult>
{
	Int32 IFrameFunction<TResult>.RequiredCapacity => capacity;
	TResult IFrameFunction<TResult>.Apply(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => func();
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
#if !NET9_0_OR_GREATER
internal readonly struct FrameFunction<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
	: IFrameFunction<TResult>
{
	TResult IFrameFunction<TResult>.Apply(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => func(state);
#else
internal readonly ref struct FrameFunction<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
	: IFrameFunction<TResult> where TState : allows ref struct
{
	private readonly TState _state = state;

	TResult IFrameFunction<TResult>.Apply(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => func(this._state);
#endif
	Int32 IFrameFunction<TResult>.RequiredCapacity => capacity;
}