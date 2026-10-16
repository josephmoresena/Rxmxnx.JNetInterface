namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Creates a new JNI async context and invokes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TAction">Type of <see cref="IAndroidNativeAction"/>.</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="action">A <typeparamref name="TAction"/> instance.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="action"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task Post<TAction>(SynchronizationContext context, TAction action, Boolean useWeakGlobal = true)
		where TAction : IAndroidNativeAction
	{
		AsyncActionState<TAction> taskState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Action = action, };
		TaskCompletionSource source = new();
		PostState postState = new(taskState, source);
		context.Post(PostState.Post, postState);
		return source.Task;
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{TResult}"/>.</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Task<TResult> Post<TResult, TFunction>(SynchronizationContext context, TFunction func,
		Boolean useWeakGlobal = true) where TFunction : struct, IAndroidNativeFunction<TResult>
	{
		AsyncFunctionState<TResult, TFunction> taskState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = func, };
		TaskCompletionSource<TResult> source = new();
		PostState<TResult> postState = new(taskState, source);
		context.Post(PostState<TResult>.Post, postState);
		return source.Task;
	}
}