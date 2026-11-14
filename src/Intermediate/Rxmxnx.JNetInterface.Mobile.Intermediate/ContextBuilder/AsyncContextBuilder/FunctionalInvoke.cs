namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Creates a new JNI async context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TAction">Type of <see cref="IAndroidNativeAction"/>.</typeparam>
	/// <param name="action">A <typeparamref name="TAction"/> instance.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="action"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task InvokeAsync<TAction>(TAction action, Boolean useWeakGlobal = true) where TAction : IAndroidNativeAction
	{
		AsyncActionState<TAction> tState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Action = action, };
		return Task.Factory.StartNew(AsyncState.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning);
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{TResult}"/>.</typeparam>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task<TResult> InvokeAsync<TResult, TFunction>(TFunction func, Boolean useWeakGlobal = true)
		where TFunction : IAndroidNativeFunction<TResult>
	{
		AsyncFunctionState<TResult, TFunction> tState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = func, };
		return Task.Factory.StartNew(AsyncState<TResult>.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning)!;
	}
}