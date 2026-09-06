namespace Rxmxnx.JNetInterface;

internal readonly partial struct EnvironmentValue
{
	/// <summary>
	/// Creates a new local reference frame and invokes <paramref name="action"/> inside of it.
	/// </summary>
	/// <param name="owner">A <see langword="ILocalCacheOwner"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="action">An action to invoke inside created a new local frame.</param>
	public void WithFrame(ILocalCacheOwner owner, Int32 capacity, Action action)
	{
		using LocalFrame _ = new(owner, capacity);
		this.Core.CheckJniError();
		action();
	}
	/// <summary>
	/// Creates a new local reference frame and invokes <paramref name="action"/> inside of it.
	/// </summary>
	/// <param name="owner">A <see langword="ILocalCacheOwner"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="state">A state object.</param>
	/// <param name="action">An action to invoke inside created a new local frame.</param>
	public void WithFrame<TState>(ILocalCacheOwner owner, Int32 capacity, TState state, Action<TState> action)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		using LocalFrame _ = new(owner, capacity);
		this.Core.CheckJniError();
		action(state);
	}

	/// <summary>
	/// Creates a new local reference frame and executes <paramref name="func"/> inside of it.
	/// </summary>
	/// <param name="owner">A <see langword="ILocalCacheOwner"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="func">A function to execute inside created a new local frame.</param>
	/// <returns>Function result.</returns>
	public static TResult WithFrame<TResult>(ILocalCacheOwner owner, Int32 capacity, Func<TResult> func)
	{
		using LocalFrame localFrame = new(owner, capacity);
		TResult result = func();
		localFrame.SetResult(result);
		return result;
	}
	/// <summary>
	/// Creates a new local reference frame and executes <paramref name="func"/> inside of it.
	/// </summary>
	/// <param name="owner">A <see langword="ILocalCacheOwner"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="state">A state object.</param>
	/// <param name="func">A function to execute inside created a new local frame.</param>
	/// <returns>Function result.</returns>
	public static TResult WithFrame<TResult, TState>(ILocalCacheOwner owner, Int32 capacity, TState state,
		Func<TState, TResult> func)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		using LocalFrame localFrame = new(owner, capacity);
		TResult result = func(state);
		localFrame.SetResult(result);
		return result;
	}
}