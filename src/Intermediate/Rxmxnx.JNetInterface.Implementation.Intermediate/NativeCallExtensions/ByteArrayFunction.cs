namespace Rxmxnx.JNetInterface;

public static partial class NativeCallExtensions
{
	/// <summary>
	/// Executes a provided frame function within a JNI context of the current <see cref="IVirtualMachine"/>.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TOutput}"/> type of the frame action to execute.</typeparam>
	/// <param name="vm">The current <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="envRef">The <see cref="JEnvironmentRef"/> native reference for the current thread.</param>
	/// <param name="func">The frame function to execute.</param>
	/// <param name="result">Output. The JNI reference of the object that is returned by the frame function.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the function execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WithNativeFrame<TFunction>(this IVirtualMachine? vm, JEnvironmentRef envRef, TFunction? func,
		out JByteArrayLocalRef result,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default) where TFunction : IFrameFunction<JArrayObject<JByte>>
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens)
		where TFunction : IFrameFunction<JArrayObject<JByte>>, allows ref struct
#endif
	{
		if (vm is not JVirtualMachine { IsAlive: true, } jvm || envRef == default || func is null)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		if (typeof(TFunction).IsValueType)
		{
			jvm.GetEnvironment(envRef).ObjectInvoke(ref func, out result, tokens);
			return;
		}
		NonGenericFrameFunction<JArrayObject<JByte>> nonGeneric =
			NonGenericFrameFunction<JArrayObject<JByte>>.Create(func);
		jvm.GetEnvironment(envRef).ObjectInvoke(ref nonGeneric, out result, tokens);
	}
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
		out JByteArrayLocalRef result,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default) where TFunction : IFrameFunction<JArrayObject<JByte>>
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens)
		where TFunction : IFrameFunction<JArrayObject<JByte>>, allows ref struct
#endif
	{
		if (envRef == default || func is null)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		if (typeof(TFunction).IsValueType)
		{
			JEnvironment.GetEnvironment(envRef).ObjectInvoke(ref func, out result, tokens);
			return;
		}
		NonGenericFrameFunction<JArrayObject<JByte>> nonGeneric =
			NonGenericFrameFunction<JArrayObject<JByte>>.Create(func);
		JEnvironment.GetEnvironment(envRef).ObjectInvoke(ref nonGeneric, out result, tokens);
	}
	/// <summary>
	/// Executes the current frame function within a JNI context of the specified <see cref="IVirtualMachine"/> instance.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TOutput}"/> type of the frame action to execute.</typeparam>
	/// <param name="func">The frame function to execute.</param>
	/// <param name="vm">The current <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="envRef">The <see cref="JEnvironmentRef"/> native reference for the current thread.</param>
	/// <param name="result">Output. The JNI reference of the object that is returned by the frame function.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the function execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WithNativeFrame<TFunction>(this ref TFunction func, IVirtualMachine? vm, JEnvironmentRef envRef,
		out JByteArrayLocalRef result,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default)
		where TFunction : struct, IFrameFunction<JArrayObject<JByte>>
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens)
		where TFunction : struct, IFrameFunction<JArrayObject<JByte>>, allows ref struct
#endif
	{
		if (vm is not JVirtualMachine { IsAlive: true, } jvm || envRef == default)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		jvm.GetEnvironment(envRef).ObjectInvoke(ref func, out result, tokens);
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
		out JByteArrayLocalRef result,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default)
		where TFunction : struct, IFrameFunction<JArrayObject<JByte>>
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens)
		where TFunction : struct, IFrameFunction<JArrayObject<JByte>>, allows ref struct
#endif
	{
		if (envRef == default)
		{
			Unsafe.SkipInit(out result);
			return;
		}
		JEnvironment.GetEnvironment(envRef).ObjectInvoke(ref func, out result, tokens);
	}
}