namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JMethodDefinition
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JMethodDefinition(ReadOnlySpan<Byte> methodName,
#if !NET9_0_OR_GREATER
		params JArgumentMetadata[] metadata
#else
		JArgumentMetadata[] metadata
#endif
	) : this(methodName, metadata.AsReadOnlySpan()) { }

	/// <inheritdoc cref="JMethodDefinition.Invoke(JLocalObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void Invoke(JLocalObject jLocal,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.Invoke(jLocal, args.AsReadOnlySpan());
	/// <inheritdoc cref="JMethodDefinition.Invoke(JLocalObject, JClassObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void Invoke(JLocalObject jLocal, JClassObject jClass,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.Invoke(jLocal, jClass, args.AsReadOnlySpan());
	/// <inheritdoc cref="JMethodDefinition.InvokeNonVirtual(JLocalObject, JClassObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.InvokeNonVirtual(jLocal, jClass, args.AsReadOnlySpan());
	/// <inheritdoc cref="JMethodDefinition.StaticInvoke(JClassObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void StaticInvoke(JClassObject jClass,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.StaticInvoke(jClass, args.AsReadOnlySpan());
	/// <inheritdoc cref="JMethodDefinition.InvokeReflected(JMethodObject, JLocalObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void InvokeReflected(JMethodObject jMethod, JLocalObject jLocal,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.InvokeReflected(jMethod, jLocal, args.AsReadOnlySpan());
	/// <inheritdoc cref="JMethodDefinition.InvokeNonVirtualReflected(JMethodObject, JLocalObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.InvokeNonVirtualReflected(jMethod, jLocal, args.AsReadOnlySpan());
	/// <inheritdoc cref="JMethodDefinition.InvokeStaticReflected(JMethodObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void InvokeStaticReflected(JMethodObject jMethod,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.InvokeStaticReflected(jMethod, args.AsReadOnlySpan());
}