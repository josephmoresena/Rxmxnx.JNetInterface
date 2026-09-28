// ReSharper disable MemberCanBeMadeStatic.Global

namespace Rxmxnx.JNetInterface;

internal readonly partial struct EnvironmentValue
{
	/// <summary>
	/// Creates a new local reference frame and invokes <paramref name="action"/> inside of it.
	/// </summary>
	/// <param name="nativeThread">A <see langword="INativeThread"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="action">An action to invoke inside created a new local frame.</param>
	public void WithFrame(INativeThread nativeThread, Int32 capacity, Action action)
	{
		LocalFrameAction val = new(action);
		EnvironmentValue.WithFrame(nativeThread, capacity, ref val);
	}
	/// <summary>
	/// Creates a new local reference frame and invokes <paramref name="action"/> inside of it.
	/// </summary>
	/// <param name="nativeThread">A <see langword="INativeThread"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="state">A state object.</param>
	/// <param name="action">An action to invoke inside created a new local frame.</param>
	public void WithFrame<TState>(INativeThread nativeThread, Int32 capacity, TState state, Action<TState> action)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		LocalFrameAction<TState> val = new(state, action);
		EnvironmentValue.WithFrame(nativeThread, capacity, ref val);
	}

	/// <summary>
	/// Creates a new local reference frame and executes <paramref name="func"/> inside of it.
	/// </summary>
	/// <param name="nativeThread">A <see langword="INativeThread"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="func">A function to execute inside created a new local frame.</param>
	/// <returns>Function result.</returns>
	public static TResult WithFrame<TResult>(INativeThread nativeThread, Int32 capacity, Func<TResult> func)
	{
		LocalFrameFunction<TResult> val = new(func);
		return EnvironmentValue.WithFrame<TResult, LocalFrameFunction<TResult>>(nativeThread, capacity, ref val);
	}
	/// <summary>
	/// Creates a new local reference frame and executes <paramref name="func"/> inside of it.
	/// </summary>
	/// <param name="nativeThread">A <see langword="INativeThread"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="state">A state object.</param>
	/// <param name="func">A function to execute inside created a new local frame.</param>
	/// <returns>Function result.</returns>
	public static TResult WithFrame<TResult, TState>(INativeThread nativeThread, Int32 capacity, TState state,
		Func<TState, TResult> func)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		LocalFrameFunction<TResult, TState> val = new(state, func);
		return EnvironmentValue
			.WithFrame<TResult, LocalFrameFunction<TResult, TState>>(nativeThread, capacity, ref val);
	}
}