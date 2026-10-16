namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Creates a new JNI async context and invokes <paramref name="action"/>.
	/// </summary>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="action"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task Post(SynchronizationContext context, AndroidJniAction action, Boolean useWeakGlobal = true)
	{
		AsyncActionState<AndroidNativeAction> taskState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Action = new(action), };
		TaskCompletionSource source = new();
		PostState postState = new(taskState, source);
		context.Post(PostState.Post, postState);
		return source.Task;
	}
	/// <summary>
	/// Creates a new JNI async context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="action"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task Post<TState>(SynchronizationContext context, TState state, AndroidJniAction<TState> action,
		Boolean useWeakGlobal = true)
	{
		AsyncActionState<AndroidNativeAction<TState>> taskState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Action = new(state, action), };
		TaskCompletionSource source = new();
		PostState postState = new(taskState, source);
		context.Post(PostState.Post, postState);
		return source.Task;
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task<TResult> Post<TResult>(SynchronizationContext context, AndroidJniFunc<TResult> func,
		Boolean useWeakGlobal = true)
	{
		AsyncFunctionState<TResult, AndroidNativeFunction<TResult>> taskState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = new(func), };
		TaskCompletionSource<TResult> source = new();
		PostState<TResult> postState = new(taskState, source);
		context.Post(PostState<TResult>.Post, postState);
		return source.Task;
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task<TResult?> Post<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		SynchronizationContext context, AndroidJniFunc<JReferenceObject?> func, Boolean useWeakGlobal = true)
		where TResult : class, IJavaPeerable
	{
		AsyncObjectFunctionState<TResult, AndroidNativeFunction<JReferenceObject?>> taskState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = new(func), };
		TaskCompletionSource<TResult> source = new();
		PostState<TResult> postState = new(taskState, source);
		context.Post(PostState<TResult>.Post, postState);
		return source.Task!;
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task<TResult> Post<TState, TResult>(SynchronizationContext context, TState state,
		AndroidJniFunc<TState, TResult> func, Boolean useWeakGlobal = true)
	{
		AsyncFunctionState<TResult, AndroidNativeFunction<TResult, TState>> taskState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = new(state, func), };
		TaskCompletionSource<TResult> source = new();
		PostState<TResult> postState = new(taskState, source);
		context.Post(PostState<TResult>.Post, postState);
		return source.Task;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task<TResult?> Post<TState, [DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		TState state, SynchronizationContext context, AndroidJniFunc<TState, JReferenceObject?> func,
		Boolean useWeakGlobal = true) where TResult : class, IJavaPeerable
	{
		AsyncObjectFunctionState<TResult, AndroidNativeFunction<JReferenceObject?, TState>> taskState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = new(state, func), };
		TaskCompletionSource<TResult> source = new();
		PostState<TResult> postState = new(taskState, source);
		context.Post(PostState<TResult>.Post, postState);
		return source.Task!;
	}
}