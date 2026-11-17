namespace Rxmxnx.JNetInterface;

public static partial class AndroidJniExtensions
{
	/// <summary>
	/// Executes a provided frame function within a JNI context.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TOutput}"/> type of the frame action to execute.</typeparam>
	/// <typeparam name="TResult">The type of the result returned by the frame function.</typeparam>
	/// <param name="envRef">The <see cref="JEnvironmentRef"/> native reference for the current thread.</param>
	/// <param name="func">The frame function to execute.</param>
	/// <param name="result">Output. The primitive value that is returned by the frame function.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the function execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WithNativeFrame<TFunction, TResult>(this JEnvironmentRef envRef, TFunction? func,
		out TResult result,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default) where TFunction : IFrameFunction<TResult>
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens) where TFunction : IFrameFunction<TResult>, allows ref struct
#endif
		where TResult : unmanaged, IPrimitiveType<TResult>
	{
		if (envRef == default || func is null)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		if (typeof(TFunction).IsValueType)
		{
			AndroidJniHost.GetEnvironment(envRef).PrimitiveInvoke(ref func, out result, tokens);
			return;
		}
		NonGenericFrameFunction<TResult> nonGeneric = NonGenericFrameFunction<TResult>.Create(func);
		AndroidJniHost.GetEnvironment(envRef).PrimitiveInvoke(ref nonGeneric, out result, tokens);
	}
	/// <summary>
	/// Executes the current frame function within a JNI context.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TOutput}"/> type of the frame action to execute.</typeparam>
	/// <typeparam name="TResult">The type of the result returned by the frame function.</typeparam>
	/// <param name="func">The frame function to execute.</param>
	/// <param name="envRef">The <see cref="JEnvironmentRef"/> native reference for the current thread.</param>
	/// <param name="result">Output. The primitive value that is returned by the frame function.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the function execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WithNativeFrame<TFunction, TResult>(this ref TFunction func, JEnvironmentRef envRef,
		out TResult result,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default) where TFunction : struct, IFrameFunction<TResult>
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens)
		where TFunction : struct, IFrameFunction<TResult>, allows ref struct
#endif
		where TResult : unmanaged, IPrimitiveType<TResult>
	{
		if (envRef == default)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		AndroidJniHost.GetEnvironment(envRef).PrimitiveInvoke(ref func, out result, tokens);
	}
}