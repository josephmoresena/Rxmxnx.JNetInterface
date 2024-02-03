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
	public new JLocalObject? Invoke(JLocalObject jLocal, params IObject?[] args)
	{
		IObject?[] realArgs = this.CreateArgumentsArray();
		args.CopyTo(realArgs, 0);
		return base.Invoke(jLocal, realArgs);
	}
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke(JLocalObject, JClassObject, IObject?[])"/>
	public new JLocalObject? Invoke(JLocalObject jLocal, JClassObject jClass, params IObject?[] args)
	{
		IObject?[] realArgs = this.CreateArgumentsArray();
		args.CopyTo(realArgs, 0);
		return base.Invoke(jLocal, jClass, realArgs);
	}
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeNonVirtual(JLocalObject, JClassObject, IObject?[])"/>
	public new JLocalObject? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass, params IObject?[] args)
	{
		IObject?[] realArgs = this.CreateArgumentsArray();
		args.CopyTo(realArgs, 0);
		return base.InvokeNonVirtual(jLocal, jClass, realArgs);
	}
	/// <inheritdoc cref="JFunctionDefinition{TResult}.StaticInvoke(JClassObject, IObject?[])"/>
	public new JLocalObject? StaticInvoke(JClassObject jClass, params IObject?[] args)
	{
		IObject?[] realArgs = this.CreateArgumentsArray();
		args.CopyTo(realArgs, 0);
		return base.StaticInvoke(jClass, realArgs);
	}

	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeReflected(JMethodObject, JLocalObject, IObject?[])"/>
	public new JLocalObject? InvokeReflected(JMethodObject jMethod, JLocalObject jLocal, params IObject?[] args)
	{
		IObject?[] realArgs = this.CreateArgumentsArray();
		args.CopyTo(realArgs, 0);
		return base.InvokeReflected(jMethod, jLocal, args);
	}
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeNonVirtualReflected(JMethodObject, JLocalObject, IObject?[])"/>
	public new JLocalObject? InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal,
		params IObject?[] args)
	{
		IObject?[] realArgs = this.CreateArgumentsArray();
		args.CopyTo(realArgs, 0);
		return base.InvokeNonVirtualReflected(jMethod, jLocal, args);
	}
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeStaticReflected(JMethodObject, IObject?[])"/>
	public new JLocalObject? InvokeStaticReflected(JMethodObject jMethod, params IObject?[] args)
	{
		IObject?[] realArgs = this.CreateArgumentsArray();
		args.CopyTo(realArgs, 0);
		return base.InvokeStaticReflected(jMethod, args);
	}

	/// <inheritdoc/>
	public override String ToString() => base.ToString();
	/// <inheritdoc/>
	public override Int32 GetHashCode() => base.GetHashCode();
}