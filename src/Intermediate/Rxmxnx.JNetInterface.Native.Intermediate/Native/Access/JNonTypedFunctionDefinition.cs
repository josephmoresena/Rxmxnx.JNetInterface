namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a non-typed class function definition.
/// </summary>
internal sealed record JNonTypedFunctionDefinition : JFunctionDefinition<JLocalObject>
{
	/// <inheritdoc/>
	internal override Type Return => typeof(JReferenceObject);

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="returnType">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	public JNonTypedFunctionDefinition(ReadOnlySpan<Byte> functionName, ReadOnlySpan<Byte> returnType,
		params JArgumentMetadata[] metadata) : base(functionName,
		                                            JAccessibleObjectDefinition.ValidateSignature(returnType),
		                                            metadata) { }

	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, IObject?[])"/>
	public new JLocalObject? Invoke(JLocalObject jLocal, params IObject?[] args) => base.Invoke(jLocal, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, JClassObject, IObject?[])"/>
	public new JLocalObject? Invoke(JLocalObject jLocal, JClassObject jClass, params IObject?[] args)
		=> base.Invoke(jLocal, jClass, args);
	/// <inheritdoc cref="JFunctionDefinition{TResult}.StaticInvoke(JClassObject, IObject?[])"/>
	public new JLocalObject? StaticInvoke(JClassObject jClass, params IObject?[] args)
		=> base.StaticInvoke(jClass, args);
}