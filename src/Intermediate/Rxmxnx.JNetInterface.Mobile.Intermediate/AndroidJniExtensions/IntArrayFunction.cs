namespace Rxmxnx.JNetInterface;

public static partial class AndroidJniExtensions
{
	/// <summary>
	/// Executes a provided frame function within a JNI context.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TOutput}"/> type of the frame action to execute.</typeparam>
	/// <param name="envRef">The <see cref="JEnvironmentRef"/> native reference for the current thread.</param>
	/// <param name="func">The frame function to execute.</param>
	/// <param name="result">Output. The JNI reference of the object that is returned by the frame function.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the function execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WithNativeFrame<TFunction>(this JEnvironmentRef envRef, TFunction? func,
		out JIntArrayLocalRef result,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default) where TFunction : IFrameFunction<JArrayObject<JInt>?>
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens)
		where TFunction : IFrameFunction<JArrayObject<JInt>?>, allows ref struct
#endif
	{
		if (envRef == default || func is null)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		if (typeof(TFunction).IsValueType)
		{
			AndroidJniHost.GetEnvironment(envRef).ObjectInvoke(ref func, out result, tokens);
			return;
		}
		NonGenericFrameFunction<JArrayObject<JInt>?> nonGeneric =
			NonGenericFrameFunction<JArrayObject<JInt>?>.Create(func);
		AndroidJniHost.GetEnvironment(envRef).ObjectInvoke(ref nonGeneric, out result, tokens);
	}
	/// <summary>
	/// Executes the current frame function within a JNI context.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TOutput}"/> type of the frame action to execute.</typeparam>
	/// <param name="func">The frame function to execute.</param>
	/// <param name="envRef">The <see cref="JEnvironmentRef"/> native reference for the current thread.</param>
	/// <param name="result">Output. The JNI reference of the object that is returned by the frame function.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the function execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WithNativeFrame<TFunction>(this ref TFunction func, JEnvironmentRef envRef,
		out JIntArrayLocalRef result,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default)
		where TFunction : struct, IFrameFunction<JArrayObject<JInt>?>
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens)
		where TFunction : struct, IFrameFunction<JArrayObject<JInt>?>, allows ref struct
#endif
	{
		if (envRef == default)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		AndroidJniHost.GetEnvironment(envRef).ObjectInvoke(ref func, out result, tokens);
	}
}