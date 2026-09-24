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
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? Invoke<TArgs>(JLocalObject jLocal, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
		=> base.Invoke(jLocal, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, JClassObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? Invoke<TArgs>(JLocalObject jLocal, JClassObject jClass, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
		=> base.Invoke(jLocal, jClass, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeNonVirtual(JLocalObject, JClassObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? InvokeNonVirtual<TArgs>(JLocalObject jLocal, JClassObject jClass, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
		=> base.InvokeNonVirtual(jLocal, jClass, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.StaticInvoke(JClassObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? StaticInvoke<TArgs>(JClassObject jClass, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
		=> base.StaticInvoke(jClass, args);

	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeReflected(JMethodObject, JLocalObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? InvokeReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
		=> base.InvokeReflected(jMethod, jLocal, args);
	/// <inheritdoc
	///     cref="JFunctionDefinition{TResult}.InvokeNonVirtualReflected(JMethodObject, JLocalObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? InvokeNonVirtualReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal,
		in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
		=> base.InvokeNonVirtualReflected(jMethod, jLocal, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeStaticReflected(JMethodObject, ReadOnlySpan{IObject})"/>
	public new JLocalObject? InvokeStaticReflected<TArgs>(JMethodObject jMethod, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
		=> base.InvokeStaticReflected(jMethod, args);
}