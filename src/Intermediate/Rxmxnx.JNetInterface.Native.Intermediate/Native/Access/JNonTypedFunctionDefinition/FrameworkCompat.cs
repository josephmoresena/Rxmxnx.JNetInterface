namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JNonTypedFunctionDefinition
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="returnTypeSignature">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JNonTypedFunctionDefinition(ReadOnlySpan<Byte> functionName, ReadOnlySpan<Byte> returnTypeSignature,
#if !NET9_0_OR_GREATER
		params JArgumentMetadata[] metadata
#else
		JArgumentMetadata[] metadata
#endif
	) : this(functionName, returnTypeSignature, metadata.AsReadOnlySpan()) { }

	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, IObject?[])"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new JLocalObject? Invoke(JLocalObject jLocal,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> base.Invoke(jLocal, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, JClassObject, IObject?[])"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new JLocalObject? Invoke(JLocalObject jLocal, JClassObject jClass,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> base.Invoke(jLocal, jClass, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeNonVirtual(JLocalObject, JClassObject, IObject?[])"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new JLocalObject? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> base.InvokeNonVirtual(jLocal, jClass, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.StaticInvoke(JClassObject, IObject?[])"/>
	public new JLocalObject? StaticInvoke(JClassObject jClass,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> base.StaticInvoke(jClass, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeReflected(JMethodObject, JLocalObject, IObject?[])"/>
	public new JLocalObject? InvokeReflected(JMethodObject jMethod, JLocalObject jLocal,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> base.InvokeReflected(jMethod, jLocal, args);
	/// <inheritdoc
	///     cref="JFunctionDefinition{TResult}.InvokeNonVirtualReflected(JMethodObject, JLocalObject, IObject?[])"/>
	public new JLocalObject? InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> base.InvokeNonVirtualReflected(jMethod, jLocal, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeStaticReflected(JMethodObject, IObject?[])"/>
	public new JLocalObject? InvokeStaticReflected(JMethodObject jMethod,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> base.InvokeStaticReflected(jMethod, args);
}