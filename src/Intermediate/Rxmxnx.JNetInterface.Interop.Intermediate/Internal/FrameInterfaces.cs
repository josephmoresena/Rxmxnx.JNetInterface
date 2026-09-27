namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
internal readonly struct LocalFrameAction(Int32 capacity, Action action) : ILocalFrameAction
{
	Int32 ILocalFrameAction.RequiredCapacity => capacity;
	void IFrameAction.Accept(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => action();
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
#if !NET9_0_OR_GREATER
internal readonly struct LocalFrameAction<TState>(Int32 capacity, TState state, Action<TState> action)
	: ILocalFrameAction
{
	void IFrameAction.Accept(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => action(state);
#else
internal readonly ref struct LocalFrameAction<TState>(Int32 capacity, TState state, Action<TState> action) : ILocalFrameAction
	where TState : allows ref struct
{
	private readonly TState _state = state;

	void IFrameAction.Accept(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => action(this._state);
#endif
	Int32 ILocalFrameAction.RequiredCapacity => capacity;
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
internal readonly struct LocalFrameFunction<TResult>(Int32 capacity, Func<TResult> func) : ILocalFrameFunction<TResult>
{
	Int32 ILocalFrameFunction<TResult>.RequiredCapacity => capacity;
	TResult IFrameFunction<TResult>.Apply(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => func();
}

/// <summary>
/// Defines a specific operation or behavior to be executed within the context of a JNI local frame.
/// </summary>
#if !NET9_0_OR_GREATER
internal readonly struct LocalFrameFunction<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
	: ILocalFrameFunction<TResult>
{
	TResult IFrameFunction<TResult>.Apply(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => func(state);
#else
internal readonly ref struct LocalFrameFunction<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
	: ILocalFrameFunction<TResult> where TState : allows ref struct
{
	private readonly TState _state = state;

	TResult IFrameFunction<TResult>.Apply(IEnvironment _, ReadOnlySpan<JLocalObject?> __) => func(this._state);
#endif
	Int32 ILocalFrameFunction<TResult>.RequiredCapacity => capacity;
}