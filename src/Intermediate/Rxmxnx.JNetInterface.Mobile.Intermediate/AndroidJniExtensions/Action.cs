namespace Rxmxnx.JNetInterface;

public static partial class AndroidJniExtensions
{
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
			AndroidJniHost.GetEnvironment(envRef).Invoke(ref action, tokens);
			return;
		}
		NonGenericFrameAction nonGeneric = NonGenericFrameAction.Create(action);
		AndroidJniHost.GetEnvironment(envRef).Invoke(ref nonGeneric, tokens);
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
		AndroidJniHost.GetEnvironment(envRef).Invoke(ref action, tokens);
	}
}