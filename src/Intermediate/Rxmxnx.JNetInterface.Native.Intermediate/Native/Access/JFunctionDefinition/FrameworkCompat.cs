namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JFunctionDefinition<TResult>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JFunctionDefinition(ReadOnlySpan<Byte> functionName,
#if !NET9_0_OR_GREATER
		params JArgumentMetadata[] metadata
#else
		JArgumentMetadata[] metadata
#endif
	) : this(functionName, metadata.AsReadOnlySpan()) { }

	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? Invoke(JLocalObject jLocal,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.Invoke(jLocal, args.AsReadOnlySpan());
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, JClassObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? Invoke(JLocalObject jLocal, JClassObject jClass,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.Invoke(jLocal, jClass, args.AsReadOnlySpan());
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeNonVirtual(JLocalObject, JClassObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.InvokeNonVirtual(jLocal, jClass, args.AsReadOnlySpan());
	/// <inheritdoc cref="JFunctionDefinition{TResult}.StaticInvoke(JClassObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? StaticInvoke(JClassObject jClass,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.StaticInvoke(jClass, args.AsReadOnlySpan());
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeReflected(JMethodObject, JLocalObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? InvokeReflected(JMethodObject jMethod, JLocalObject jLocal,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.InvokeReflected(jMethod, jLocal, args.AsReadOnlySpan());
	/// <inheritdoc
	///     cref="JFunctionDefinition{TResult}.InvokeNonVirtualReflected(JMethodObject, JLocalObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.InvokeNonVirtualReflected(jMethod, jLocal, args.AsReadOnlySpan());
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeStaticReflected(JMethodObject, ReadOnlySpan{IObject?})"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TResult? InvokeStaticReflected(JMethodObject jMethod,
#if !NET9_0_OR_GREATER
		params IObject?[] args
#else
		IObject?[] args
#endif
	)
		=> this.InvokeStaticReflected(jMethod, args.AsReadOnlySpan());
}