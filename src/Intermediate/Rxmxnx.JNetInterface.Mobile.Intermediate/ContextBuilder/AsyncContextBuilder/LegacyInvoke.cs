namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Creates a new JNI async context and invokes <paramref name="action"/>.
	/// </summary>
	/// <param name="action">A <see cref="AndroidJniAction"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="action"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task InvokeAsync(AndroidJniAction action, Boolean useWeakGlobal = true)
	{
		AsyncActionState<AndroidNativeAction> tState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Action = new(action), };
		return Task.Factory.StartNew(AsyncState.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning);
	}
	/// <summary>
	/// Creates a new JNI async context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="action">A <see cref="AndroidJniAction{TState}"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="action"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task InvokeAsync<TState>(TState state, AndroidJniAction<TState> action, Boolean useWeakGlobal = true)
	{
		AsyncActionState<AndroidNativeAction<TState>> tState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Action = new(state, action), };
		return Task.Factory.StartNew(AsyncState.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning);
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task<TResult> InvokeAsync<TResult>(AndroidJniFunc<TResult> func, Boolean useWeakGlobal = true)
	{
		AsyncFunctionState<TResult, AndroidNativeFunction<TResult>> tState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = new(func), };
		return Task.Factory.StartNew(AsyncState<TResult>.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning)!;
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task<TResult?> InvokeAsync<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
		AndroidJniFunc<JReferenceObject?> func, Boolean useWeakGlobal = true) where TResult : class, IJavaPeerable
	{
		AsyncObjectFunctionState<TResult, AndroidNativeFunction<JReferenceObject?>> tState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = new(func), };
		return Task.Factory.StartNew(AsyncState<TResult>.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning);
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task<TResult> InvokeAsync<TState, TResult>(TState state, AndroidJniFunc<TState, TResult> func,
		Boolean useWeakGlobal = true)
	{
		AsyncFunctionState<TResult, AndroidNativeFunction<TResult, TState>> tState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = new(state, func), };
		return Task.Factory.StartNew(AsyncState<TResult>.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning)!;
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TState">Type of state object</typeparam>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <param name="state">A state instance object.</param>
	/// <param name="func">A <see cref="AndroidJniFunc{TResult}"/> delegate.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task<TResult?> InvokeAsync<TState,
		[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(TState state,
		AndroidJniFunc<TState, JReferenceObject?> func, Boolean useWeakGlobal = true)
		where TResult : class, IJavaPeerable
	{
		AsyncObjectFunctionState<TResult, AndroidNativeFunction<JReferenceObject?, TState>> tState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = new(state, func), };
		return Task.Factory.StartNew(AsyncState<TResult>.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning);
	}
}