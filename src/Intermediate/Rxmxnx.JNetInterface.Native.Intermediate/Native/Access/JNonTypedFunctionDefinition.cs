namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a non-typed class function definition.
/// </summary>
/// <param name="functionName">Function name.</param>
/// <param name="returnTypeSignature">Method return type defined signature.</param>
/// <param name="metadata">Metadata of the types of call arguments.</param>
public sealed class JNonTypedFunctionDefinition(
	ReadOnlySpan<Byte> functionName,
	ReadOnlySpan<Byte> returnTypeSignature,
	ReadOnlySpan<JArgumentMetadata> metadata = default) : JFunctionDefinition<JLocalObject>(functionName,
	JAccessibleObjectDefinition.ValidateSignature(returnTypeSignature), metadata)
{
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? Invoke(JLocalObject jLocal, ReadOnlySpan<IObject?> args = default)
		=> base.Invoke(jLocal, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, JClassObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? Invoke(JLocalObject jLocal, JClassObject jClass, ReadOnlySpan<IObject?> args = default)
		=> base.Invoke(jLocal, jClass, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeNonVirtual(JLocalObject, JClassObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass,
		ReadOnlySpan<IObject?> args = default)
		=> base.InvokeNonVirtual(jLocal, jClass, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.StaticInvoke(JClassObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? StaticInvoke(JClassObject jClass, ReadOnlySpan<IObject?> args = default)
		=> base.StaticInvoke(jClass, args);

	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeReflected(JMethodObject, JLocalObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? InvokeReflected(JMethodObject jMethod, JLocalObject jLocal,
		ReadOnlySpan<IObject?> args = default)
		=> base.InvokeReflected(jMethod, jLocal, args);
	/// <inheritdoc
	///     cref="JFunctionDefinition{TResult}.InvokeNonVirtualReflected(JMethodObject, JLocalObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal,
		ReadOnlySpan<IObject?> args = default)
		=> base.InvokeNonVirtualReflected(jMethod, jLocal, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeStaticReflected(JMethodObject, ReadOnlySpan{IObject?})"/>
	public new JLocalObject? InvokeStaticReflected(JMethodObject jMethod, ReadOnlySpan<IObject?> args = default)
		=> base.InvokeStaticReflected(jMethod, args);
}