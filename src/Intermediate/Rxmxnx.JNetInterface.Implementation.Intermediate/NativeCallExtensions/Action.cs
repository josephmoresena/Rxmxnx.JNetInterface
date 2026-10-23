namespace Rxmxnx.JNetInterface;

public static partial class NativeCallExtensions
{
	/// <summary>
	/// Executes a provided frame action within a JNI context of the current <see cref="IVirtualMachine"/>.
	/// </summary>
	/// <typeparam name="TAction">The <see cref="IFrameAction"/> type of the frame action to execute.</typeparam>
	/// <param name="vm">The current <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="envRef">The <see cref="JEnvironmentRef"/> native reference for the current thread.</param>
	/// <param name="action">The frame action to execute.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the action execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WithNativeFrame<TAction>(this IVirtualMachine? vm, JEnvironmentRef envRef, TAction? action,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default) where TAction : IFrameAction
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens) where TAction : IFrameAction, allows ref struct
#endif
	{
		if (vm is not JVirtualMachine { IsAlive: true, } jvm || envRef == default || action is null)
			return;
		if (typeof(TAction).IsValueType)
		{
			jvm.GetEnvironment(envRef).Invoke(ref action, tokens);
			return;
		}
		NonGenericFrameAction nonGeneric = NonGenericFrameAction.Create(action);
		jvm.GetEnvironment(envRef).Invoke(ref nonGeneric, tokens);
	}
	/// <summary>
	/// Executes a provided frame action within a JNI context.
	/// </summary>
	/// <typeparam name="TAction">The <see cref="IFrameAction"/> type of the frame action to execute.</typeparam>
	/// <param name="envRef">The <see cref="JEnvironmentRef"/> native reference for the current thread.</param>
	/// <param name="action">The frame action to execute.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the action execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WithNativeFrame<TAction>(this JEnvironmentRef envRef, TAction? action,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default) where TAction : IFrameAction
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens) where TAction : IFrameAction, allows ref struct
#endif
	{
		if (envRef == default || action is null)
			return;
		if (typeof(TAction).IsValueType)
		{
			JEnvironment.GetEnvironment(envRef).Invoke(ref action, tokens);
			return;
		}
		NonGenericFrameAction nonGeneric = NonGenericFrameAction.Create(action);
		JEnvironment.GetEnvironment(envRef).Invoke(ref nonGeneric, tokens);
	}
	/// <summary>
	/// Executes the current frame action within a JNI context of the specified <see cref="IVirtualMachine"/> instance.
	/// </summary>
	/// <typeparam name="TAction">The <see cref="IFrameAction"/> type of the frame action to execute.</typeparam>
	/// <param name="action">The frame action to execute.</param>
	/// <param name="vm">The current <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="envRef">The <see cref="JEnvironmentRef"/> native reference for the current thread.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the action execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WithNativeFrame<TAction>(this ref TAction action, IVirtualMachine? vm, JEnvironmentRef envRef,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default) where TAction : struct, IFrameAction
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens) where TAction : struct, IFrameAction, allows ref struct
#endif
	{
		if (vm is not JVirtualMachine { IsAlive: true, } jvm || envRef == default)
			return;
		jvm.GetEnvironment(envRef).Invoke(ref action, tokens);
	}
	/// <summary>
	/// Executes the current frame action within a JNI context.
	/// </summary>
	/// <typeparam name="TAction">The <see cref="IFrameAction"/> type of the frame action to execute.</typeparam>
	/// <param name="action">The frame action to execute.</param>
	/// <param name="envRef">The <see cref="JEnvironmentRef"/> native reference for the current thread.</param>
	/// <param name="tokens">A span of reference type tokens to be used during the action execution.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WithNativeFrame<TAction>(this ref TAction action, JEnvironmentRef envRef,
#if !NET9_0_OR_GREATER
		scoped ReadOnlySpan<JReferenceTypeToken> tokens = default) where TAction :struct, IFrameAction
#else
		params ReadOnlySpan<JReferenceTypeToken> tokens) where TAction : struct, IFrameAction, allows ref struct
#endif
	{
		if (envRef == default)
			return;
		JEnvironment.GetEnvironment(envRef).Invoke(ref action, tokens);
	}
}