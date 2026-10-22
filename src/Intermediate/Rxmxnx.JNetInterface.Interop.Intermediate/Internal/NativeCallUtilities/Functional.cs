namespace Rxmxnx.JNetInterface.Internal;

internal static unsafe partial class NativeCallUtilities
{
	/// <summary>
	/// Executes the provided action within the context of a native thread, using the specified cache and reference
	/// type tokens.
	/// </summary>
	/// <typeparam name="TAction">
	/// The type of the action to execute. Must implement the <see cref="IFrameAction"/> interface.
	/// </typeparam>
	/// <param name="env">The native thread environment in which the action will execute.</param>
	/// <param name="cache">An object that represents the local cache for the current context.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the action execution.</param>
	/// <param name="action">A reference to the action that will be executed.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Invoke<TAction>(INativeThread env, AlienLocalCache cache,
		ReadOnlySpan<JReferenceTypeToken> tokens, ref TAction action)
#if !NET9_0_OR_GREATER
		where TAction : IFrameAction
#else
		where TAction : IFrameAction, allows ref struct
#endif
	{
		Debug.Assert(typeof(TAction).IsValueType);
		Debug.Assert(tokens.Length <= Byte.MaxValue);

#pragma warning disable CS8500
		fixed (TAction* actionPtr = &action)
#pragma warning restore CS8500
#if !NET9_0_OR_GREATER
		fixed (JReferenceTypeToken* _ = &MemoryMarshal.GetReference(tokens))
#endif
		{
			BufferAction<TAction> bufferAction = new(env, cache, tokens, actionPtr);
			BufferManager<JLocalObject?>.AllocWithReference(ref bufferAction);
		}
	}
	/// <summary>
	/// Executes the provided function within the context of a native thread using the specified cache, reference type
	/// tokens, and produces a primitive result.
	/// </summary>
	/// <typeparam name="TPrimitive">The type of the primitive result.</typeparam>
	/// <typeparam name="TFunction">
	/// The type of the function to execute. Must implement the <see cref="IFrameFunction{TOutput}"/> interface.
	/// </typeparam>
	/// <param name="env">The native thread environment in which the function will be executed.</param>
	/// <param name="cache">An object that represents the local cache for the current context.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the function execution.</param>
	/// <param name="func">A reference to the function that will be executed.</param>
	/// <param name="result">The output primitive result produced by the executed function.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void PrimitiveInvoke<TPrimitive, TFunction>(INativeThread env, AlienLocalCache cache,
		ReadOnlySpan<JReferenceTypeToken> tokens, ref TFunction func, out TPrimitive result)
#if !NET9_0_OR_GREATER
		where TFunction : IFrameFunction<TPrimitive>
#else
		where TFunction : IFrameFunction<TPrimitive>, allows ref struct
#endif
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		Debug.Assert(typeof(TFunction).IsValueType);
		Debug.Assert(tokens.Length <= Byte.MaxValue);

#pragma warning disable CS8500
		fixed (TFunction* funcPtr = &func)
#pragma warning restore CS8500
#if !NET9_0_OR_GREATER
		fixed (JReferenceTypeToken* _ = &MemoryMarshal.GetReference(tokens))
#endif
		{
			BufferPrimitiveFunction<TPrimitive, TFunction> bufferFunc = new(env, cache, tokens, funcPtr);
			BufferManager<JLocalObject?>.AllocWithReference(ref bufferFunc, out result);
		}
	}
	/// <summary>
	/// Invokes the specified function within the context of a native thread, using the provided cache, reference
	/// type tokens, and outputs the resulting object reference.
	/// </summary>
	/// <typeparam name="TObjectReference">The type of the object reference to output.</typeparam>
	/// <typeparam name="TFunction">
	/// The type of the function to be executed. Must implement the <see cref="IFrameFunction{TOutput}"/> interface
	/// where the output is of type <see cref="JReferenceObject"/>.
	/// </typeparam>
	/// <param name="env">The native thread environment in which the function will execute.</param>
	/// <param name="cache">An object that represents the local cache for the current context.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the function execution.</param>
	/// <param name="func">A reference to the function that will be executed.</param>
	/// <param name="result">Outputs the resulting object reference produced by the function execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ObjectInvoke<TObjectReference, TFunction>(INativeThread env, AlienLocalCache cache,
		ReadOnlySpan<JReferenceTypeToken> tokens, ref TFunction func, out TObjectReference result)
#if !NET9_0_OR_GREATER
		where TFunction : IFrameFunction<JReferenceObject?>
#else
		where TFunction : IFrameFunction<JReferenceObject?>, allows ref struct
#endif
		where TObjectReference : unmanaged, INativePointerType<TObjectReference>
	{
		Debug.Assert(typeof(TFunction).IsValueType);
		Debug.Assert(tokens.Length <= Byte.MaxValue);

#pragma warning disable CS8500
		fixed (TFunction* funcPtr = &func)
#pragma warning restore CS8500
#if !NET9_0_OR_GREATER
		fixed (JReferenceTypeToken* _ = &MemoryMarshal.GetReference(tokens))
#endif
		{
			BufferObjectFunction<TObjectReference, TFunction> bufferFunc = new(env, cache, tokens, funcPtr);
			BufferManager<JLocalObject?>.AllocWithReference(ref bufferFunc, out result);
		}
	}
}