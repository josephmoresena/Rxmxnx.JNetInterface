namespace Rxmxnx.JNetInterface;

public static partial class AndroidJniExtensions
{
	/// <summary>
	/// Executes a provided frame action within the context of the current environment.
	/// </summary>
	/// <typeparam name="TAction">The <see cref="IFrameAction"/> type of the frame action to execute.</typeparam>
	/// <param name="env">The current <see cref="AndroidEnvironment"/> instance.</param>
	/// <param name="action">The frame action to execute.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the action execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void Invoke<TAction>(this AndroidEnvironment env, ref TAction action,
		scoped ReadOnlySpan<JReferenceTypeToken> tokens)
#if !NET9_0_OR_GREATER
		where TAction : IFrameAction
#else
		where TAction : IFrameAction, allows ref struct
#endif
	{
		Debug.Assert(typeof(TAction).IsValueType);
		using JavaInteropCache cache = new(AndroidJniHost.GetAndroidHost(), env);
		NativeCallUtilities.Invoke(env, cache, ref action, tokens);
	}
	/// <summary>
	/// Executes a provided frame function within a JNI context of the current environment.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TOutput}"/> type of the frame action to execute.</typeparam>
	/// <typeparam name="TPrimitive">The type of the result returned by the frame function.</typeparam>
	/// <param name="env">The current <see cref="AndroidEnvironment"/> instance.</param>
	/// <param name="func">The frame func to execute.</param>
	/// <param name="result">Output. The primitive value that is returned by the frame func.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the func execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void PrimitiveInvoke<TFunction, TPrimitive>(this AndroidEnvironment env, ref TFunction func,
		out TPrimitive result, scoped ReadOnlySpan<JReferenceTypeToken> tokens)
#if !NET9_0_OR_GREATER
		where TFunction : IFrameFunction<TPrimitive>
#else
		where TFunction : IFrameFunction<TPrimitive>, allows ref struct
#endif
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		Debug.Assert(typeof(TFunction).IsValueType);
		using JavaInteropCache cache = new(AndroidJniHost.GetAndroidHost(), env);
		NativeCallUtilities.PrimitiveInvoke(env, cache, ref func, tokens, out result);
	}
	/// <summary>
	/// Executes a provided frame function within a JNI context of the current environment.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TOutput}"/> type of the frame action to execute.</typeparam>
	/// <typeparam name="TObjectReference">The type of the result returned by the frame function.</typeparam>
	/// <param name="env">The current <see cref="AndroidEnvironment"/> instance.</param>
	/// <param name="func">The frame func to execute.</param>
	/// <param name="result">Output. The JNI reference of the object that is returned by the frame function.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the func execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void ObjectInvoke<TFunction, TObjectReference>(this AndroidEnvironment env, ref TFunction func,
		out TObjectReference result, scoped ReadOnlySpan<JReferenceTypeToken> tokens)
#if !NET9_0_OR_GREATER
		where TFunction : IFrameFunction<JReferenceObject?>
#else
		where TFunction : IFrameFunction<JReferenceObject?>, allows ref struct
#endif
		where TObjectReference : unmanaged, INativePointerType<TObjectReference>
	{
		Debug.Assert(typeof(TFunction).IsValueType);
		using JavaInteropCache cache = new(AndroidJniHost.GetAndroidHost(), env);
		NativeCallUtilities.ObjectInvoke(env, cache, ref func, tokens, out result);
	}
	/// <summary>
	/// Executes a provided frame function within a JNI context of the current environment.
	/// </summary>
	/// <typeparam name="TFunction">The <see cref="IFrameFunction{TOutput}"/> type of the frame action to execute.</typeparam>
	/// <param name="env">The current <see cref="AndroidEnvironment"/> instance.</param>
	/// <param name="func">The frame func to execute.</param>
	/// <param name="result">Output. The JNI reference of the object that is returned by the frame function.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the func execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void ArrayObjectInvoke<TFunction>(this AndroidEnvironment env, ref TFunction func,
		out JObjectArrayLocalRef result, scoped ReadOnlySpan<JReferenceTypeToken> tokens)
#if !NET9_0_OR_GREATER
		where TFunction : IFrameFunction<IArrayObject<ILocalObject>?>
#else
		where TFunction : IFrameFunction<IArrayObject<ILocalObject>?>, allows ref struct
#endif
	{
		Debug.Assert(typeof(TFunction).IsValueType);
		using JavaInteropCache cache = new(AndroidJniHost.GetAndroidHost(), env);
		NativeCallUtilities.ArrayObjectInvoke(env, cache, ref func, tokens, out result);
	}
}