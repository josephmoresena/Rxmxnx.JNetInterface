namespace Rxmxnx.JNetInterface.Native.Access;

public sealed partial class JNonTypedFunctionDefinition
{
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? Invoke(JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> base.Invoke(jLocal, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, JClassObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? Invoke(JLocalObject jLocal, JClassObject jClass,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> base.Invoke(jLocal, jClass, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeNonVirtual(JLocalObject, JClassObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> base.InvokeNonVirtual(jLocal, jClass, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.StaticInvoke(JClassObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? StaticInvoke(JClassObject jClass,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> base.StaticInvoke(jClass, args);

	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeReflected(JMethodObject, JLocalObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? InvokeReflected(JMethodObject jMethod, JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> base.InvokeReflected(jMethod, jLocal, args);
	/// <inheritdoc
	///     cref="JFunctionDefinition{TResult}.InvokeNonVirtualReflected(JMethodObject, JLocalObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> base.InvokeNonVirtualReflected(jMethod, jLocal, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeStaticReflected(JMethodObject, ReadOnlySpan{IObject})"/>
	public new JLocalObject? InvokeStaticReflected(JMethodObject jMethod,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> base.InvokeStaticReflected(jMethod, args);
}