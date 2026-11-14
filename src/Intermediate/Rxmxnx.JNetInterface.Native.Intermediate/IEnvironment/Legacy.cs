namespace Rxmxnx.JNetInterface;

public partial interface IEnvironment
{
	/// <summary>
	/// Creates a new local reference frame and invokes <paramref name="action"/> inside of it.
	/// </summary>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="action">An action to invoke inside created a new local frame.</param>
	void WithFrame(Int32 capacity, Action action);
	/// <summary>
	/// Creates a new local reference frame and invokes <paramref name="action"/> inside of it.
	/// </summary>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="state">A state object.</param>
	/// <param name="action">An action to invoke inside created a new local frame.</param>
	void WithFrame<TState>(Int32 capacity, TState state, Action<TState> action)
#if NET9_0_OR_GREATER
		where TState : allows ref struct;
#else
		;
#endif
	/// <summary>
	/// Creates a new local reference frame and executes <paramref name="func"/> inside of it.
	/// </summary>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="func">A function to execute inside created a new local frame.</param>
	/// <returns>Function result.</returns>
	TResult WithFrame<TResult>(Int32 capacity, Func<TResult> func);
	/// <summary>
	/// Creates a new local reference frame and executes <paramref name="func"/> inside of it.
	/// </summary>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="state">A state object.</param>
	/// <param name="func">A function to execute inside created a new local frame.</param>
	/// <returns>Function result.</returns>
	TResult WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
#if NET9_0_OR_GREATER
		where TState : allows ref struct;
#else
		;
#endif
}