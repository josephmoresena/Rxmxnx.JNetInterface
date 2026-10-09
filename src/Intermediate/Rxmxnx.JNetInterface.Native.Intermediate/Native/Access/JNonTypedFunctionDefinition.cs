namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a non-typed class function definition.
/// </summary>
/// <param name="functionName">Function name.</param>
/// <param name="returnTypeSignature">Method return type-defined signature.</param>
/// <param name="metadata">Metadata of the types of call arguments.</param>
public sealed partial class JNonTypedFunctionDefinition(
	ReadOnlySpan<Byte> functionName,
	ReadOnlySpan<Byte> returnTypeSignature,
#if NET9_0_OR_GREATER
	params ReadOnlySpan<JArgumentMetadata> metadata
#else
	ReadOnlySpan<JArgumentMetadata> metadata = default
#endif
) : JFunctionDefinition<JLocalObject>(functionName, JAccessibleObjectDefinition.ValidateSignature(returnTypeSignature),
                                      metadata)
{
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke{TArgs}(JLocalObject, ref TArgs)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new JLocalObject? Invoke<TArgs>(JLocalObject jLocal, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> base.Invoke(jLocal, ref args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke{TArgs}(JLocalObject, JClassObject, ref TArgs)"/>
	public new JLocalObject? Invoke<TArgs>(JLocalObject jLocal, JClassObject jClass, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> base.Invoke(jLocal, jClass, ref args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeNonVirtual{TArgs}(JLocalObject, JClassObject, ref TArgs)"/>
	public new JLocalObject? InvokeNonVirtual<TArgs>(JLocalObject jLocal, JClassObject jClass, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> base.InvokeNonVirtual(jLocal, jClass, ref args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.StaticInvoke{TArgs}(JClassObject, ref TArgs)"/>
	public new JLocalObject? StaticInvoke<TArgs>(JClassObject jClass, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> base.StaticInvoke(jClass, ref args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeReflected{TArgs}(JMethodObject, JLocalObject, ref TArgs)"/>
	public new JLocalObject? InvokeReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> base.InvokeReflected(jMethod, jLocal, args);
	/// <inheritdoc
	///     cref="JFunctionDefinition{TResult}.InvokeNonVirtualReflected{TArgs}(JMethodObject, JLocalObject, ref TArgs)"/>
	public new JLocalObject? InvokeNonVirtualReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal,
		ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> base.InvokeNonVirtualReflected(jMethod, jLocal, ref args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeStaticReflected{TArgs}(JMethodObject, ref TArgs)"/>
	public new JLocalObject? InvokeStaticReflected<TArgs>(JMethodObject jMethod, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> base.InvokeStaticReflected(jMethod, ref args);
}