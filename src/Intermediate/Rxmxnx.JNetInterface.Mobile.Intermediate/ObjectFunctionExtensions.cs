using JObject = Rxmxnx.JNetInterface.Native.JReferenceObject;

// ReSharper disable ConvertToExtensionBlock

namespace Rxmxnx.JNetInterface;

/// <summary>
/// Provides extension methods for invoking and managing object functions in the context of native Android JNI operations.
/// </summary>
public static class ObjectFunctionExtensions
{
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{JObject}"/>.</typeparam>
	/// <param name="builder">The current <see cref="SyncContextBuilder"/> instance.</param>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="result">Output. The result of the invoked Java object function.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Invoke<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult, TFunction>(
		this SyncContextBuilder builder, TFunction func, out TResult? result) where TResult : class, IJavaPeerable
#if !NET9_0_OR_GREATER
		where TFunction : IAndroidNativeFunction<JObject?>
#else
		where TFunction : IAndroidNativeFunction<JObject?>, allows ref struct
#endif
	{
		if (typeof(TFunction).IsValueType)
		{
			result = builder.ObjectFunctionInvoke<TResult, TFunction>(ref func);
			return;
		}
		NonGenericNativeFunction<JObject?> nonGeneric = NonGenericNativeFunction<JObject?>.Create(func);
		builder.Invoke(ref nonGeneric, out result);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{JObject}"/>.</typeparam>
	/// <param name="builder">The current <see cref="SyncContextBuilder"/> instance.</param>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="result">Output. The result of the invoked Java object function.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Invoke<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult, TFunction>(
		this SyncContextBuilder builder, ref TFunction func, out TResult? result) where TResult : class, IJavaPeerable
#if !NET9_0_OR_GREATER
		where TFunction : struct, IAndroidNativeFunction<JObject?>
#else
		where TFunction : struct, IAndroidNativeFunction<JObject?>, allows ref struct
#endif
		=> result = builder.ObjectFunctionInvoke<TResult, TFunction>(ref func);
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{TResult}"/>.</typeparam>
	/// <param name="builder">The current <see cref="SyncContextBuilder"/> instance.</param>
	/// <param name="context">The synchronization context on which the action will be executed.</param>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="result">Output. The result of the invoked Java object function.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SendObjectFunction<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult,
		TFunction>(this SyncContextBuilder builder, SynchronizationContext context, TFunction? func,
		out TResult? result) where TResult : class, IJavaPeerable
#if !NET9_0_OR_GREATER
		where TFunction : IAndroidNativeFunction<JObject?>
#else
		where TFunction : IAndroidNativeFunction<JObject?>, allows ref struct
#endif
	{
		if (func is null)
		{
			result = default;
			return;
		}
		if (typeof(TFunction).IsValueType)
		{
			SyncContextBuilder.ObjectFunctionSend(in builder, context, ref func, out result);
			return;
		}
		NonGenericNativeFunction<JObject?> nonGeneric = NonGenericNativeFunction<JObject?>.Create(func);
		builder.SendObjectFunction(context, ref nonGeneric, out result);
	}
	/// <summary>
	/// Creates a new JNI context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{TResult}"/>.</typeparam>
	/// <param name="builder">The current <see cref="SyncContextBuilder"/> instance.</param>
	/// <param name="context">The synchronization context on which the action will be executed.</param>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="result">Output. The result of the invoked Java object function.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SendObjectFunction<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult,
		TFunction>(this SyncContextBuilder builder, SynchronizationContext context, ref TFunction func,
		out TResult? result) where TResult : class, IJavaPeerable
#if !NET9_0_OR_GREATER
		where TFunction : struct, IAndroidNativeFunction<JObject?>
#else
		where TFunction : struct, IAndroidNativeFunction<JObject?>, allows ref struct
#endif
		=> SyncContextBuilder.ObjectFunctionSend(in builder, context, ref func, out result);
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{JReferenceObject}"/>.</typeparam>
	/// <param name="builder">The current <see cref="AsyncContextBuilder"/> instance.</param>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult?>
		InvokeAsync<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult, TFunction>(
			this AsyncContextBuilder builder, TFunction func, Boolean useWeakGlobal = true)
		where TResult : class, IJavaPeerable where TFunction : struct, IAndroidNativeFunction<JObject?>
		=> builder.ObjectFunctionInvokeAsync<TResult, TFunction>(func, useWeakGlobal);
	/// <summary>
	/// Creates a new JNI async context, invokes <paramref name="func"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult">Type of result object</typeparam>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{JReferenceObject}"/>.</typeparam>
	/// <param name="builder">The current <see cref="AsyncContextBuilder"/> instance.</param>
	/// <param name="context">A <see cref="SynchronizationContext"/> instance.</param>
	/// <param name="func">A <typeparamref name="TFunction"/> instance.</param>
	/// <param name="useWeakGlobal">Indicates whether the async call should use weak-global JNI references.</param>
	/// <returns>Asynchronous <paramref name="func"/> invocation operation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult?>
		Post<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult, TFunction>(
			this AsyncContextBuilder builder, SynchronizationContext context, TFunction func,
			Boolean useWeakGlobal = true)
		where TResult : class, IJavaPeerable where TFunction : struct, IAndroidNativeFunction<JObject?>
		=> builder.ObjectFunctionPost<TResult, TFunction>(context, func, useWeakGlobal);
}