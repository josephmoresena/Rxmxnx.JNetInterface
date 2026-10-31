namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
#pragma warning disable CS8500
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Send(SynchronizationContext context, AndroidJniAction action)
	{
		AndroidNativeAction nativeAction = new(action);
		this.Send(context, ref nativeAction);
	}
	/// <summary>
	/// Creates a new JNI context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Send<TState>(SynchronizationContext context, TState state, AndroidJniAction<TState> action)
	{
		AndroidNativeAction<TState> nativeAction = new(state, action);
		this.Send(context, ref nativeAction);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Send<TResult>(SynchronizationContext context, AndroidJniFunc<TResult> func)
	{
		AndroidNativeFunction<TResult> nativeFunction = new(func);
		this.Send(context, ref nativeFunction, out TResult? result);
		return result!;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Send<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		SynchronizationContext context, AndroidJniFunc<JReferenceObject?> func) where TResult : class, IJavaPeerable
	{
		AndroidNativeFunction<JReferenceObject?> nativeFunction = new(func);
		this.SendObjectFunction(context, ref nativeFunction, out TResult? result);
		return result!;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Send<TState, TResult>(SynchronizationContext context, TState state,
		AndroidJniFunc<TState, TResult> func)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		AndroidNativeFunction<TResult, TState> nativeFunction = new(state, func);
		this.Send(context, ref nativeFunction, out TResult? result);
		return result!;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <returns>A <typeparamref name="TResult"/> function result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Send<TState, [DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		SynchronizationContext context, TState state, AndroidJniFunc<TState, JReferenceObject?> func)
		where TResult : class, IJavaPeerable
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		AndroidNativeFunction<JReferenceObject?, TState> nativeFunction = new(state, func);
		this.SendObjectFunction(context, ref nativeFunction, out TResult? result);
		return result!;
	}
#pragma warning restore CS8500
}