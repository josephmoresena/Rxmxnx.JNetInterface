namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{JReferenceObject}"/>.</typeparam>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal Task<TResult?>
		ObjectFunctionInvokeAsync<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult,
			TFunction>(
			TFunction func, Boolean useWeakGlobal = true) where TResult : class, IJavaPeerable
		where TFunction : struct, IAndroidNativeFunction<JReferenceObject?>
	{
		AsyncObjectFunctionState<TResult, TFunction> tState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = func, };
		return Task.Factory.StartNew(AsyncState<TResult>.ExecuteAndDispose, tState, TaskCreationOptions.LongRunning);
	}
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{JReferenceObject}"/>.</typeparam>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal Task<TResult?>
		ObjectFunctionPost<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult, TFunction>(
			SynchronizationContext context, TFunction func, Boolean useWeakGlobal = true)
		where TResult : class, IJavaPeerable where TFunction : struct, IAndroidNativeFunction<JReferenceObject?>
	{
		AsyncObjectFunctionState<TResult, TFunction> taskState =
			new(AndroidJniHost.GetAndroidHost(), this, useWeakGlobal) { Function = func, };
		TaskCompletionSource<TResult> source = new();
		PostState<TResult> postState = new(taskState, source);
		context.Post(PostState<TResult>.Post, postState);
		return source.Task!;
	}
}