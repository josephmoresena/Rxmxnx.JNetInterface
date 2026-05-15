namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class IndeterminateCall
{
	/// <inheritdoc cref="IndeterminateHelper.ReflectedFunctionCall(JMethodObject, JLocalObject, ReadOnlySpan{IObject})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IndeterminateResult ReflectedFunctionCall(JMethodObject jMethod, JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> IndeterminateHelper.ReflectedFunctionCall(jMethod, jLocal, false, args);
	/// <inheritdoc
	///     cref="IndeterminateHelper.ReflectedFunctionCall(JMethodObject, JLocalObject, Boolean, ReadOnlySpan{IObject})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IndeterminateResult ReflectedFunctionCall(JMethodObject jMethod, JLocalObject jLocal,
		Boolean nonVirtual,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> IndeterminateHelper.ReflectedFunctionCall(jMethod, jLocal, nonVirtual, args);
	/// <inheritdoc cref="IndeterminateHelper.ReflectedStaticFunctionCall(JExecutableObject, ReadOnlySpan{IObject})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IndeterminateResult ReflectedStaticFunctionCall(JExecutableObject jExecutable,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> IndeterminateHelper.ReflectedStaticFunctionCall(jExecutable, args);

	/// <inheritdoc cref="IndeterminateHelper.ReflectedMethodCall(JMethodObject, JLocalObject, ReadOnlySpan{IObject})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReflectedMethodCall(JMethodObject jMethod, JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> IndeterminateHelper.ReflectedMethodCall(jMethod, jLocal, false, args);
	/// <inheritdoc cref="IndeterminateHelper.ReflectedMethodCall(JMethodObject, JLocalObject, Boolean, ReadOnlySpan{IObject})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReflectedMethodCall(JMethodObject jMethod, JLocalObject jLocal, Boolean nonVirtual,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> IndeterminateHelper.ReflectedMethodCall(jMethod, jLocal, nonVirtual, args);
	/// <inheritdoc cref="IndeterminateHelper.ReflectedStaticMethodCall(JExecutableObject, ReadOnlySpan{IObject})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReflectedStaticMethodCall(JExecutableObject jExecutable,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> IndeterminateHelper.ReflectedStaticMethodCall(jExecutable, args);

	/// <inheritdoc cref="IndeterminateHelper.ReflectedNewCall(JConstructorObject, ReadOnlySpan{IObject})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JLocalObject ReflectedNewCall(JConstructorObject jConstructor,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> IndeterminateHelper.ReflectedNewCall(jConstructor, args);
	/// <inheritdoc cref="IndeterminateHelper.ReflectedNewCall{TObject}(JConstructorObject, ReadOnlySpan{IObject})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	public static TObject ReflectedNewCall<TObject>(JConstructorObject jConstructor,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	) where TObject : JLocalObject, IClassType<TObject>
		=> IndeterminateHelper.ReflectedNewCall<TObject>(jConstructor, args);
}