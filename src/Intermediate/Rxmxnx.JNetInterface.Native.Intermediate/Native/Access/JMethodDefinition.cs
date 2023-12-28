namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a method definition.
/// </summary>
public record JMethodDefinition : JCallDefinition
{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	/// <remarks>This constructor should be never inherited.</remarks>
	public JMethodDefinition(ReadOnlySpan<Byte> methodName) : base(methodName) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JMethodDefinition(ReadOnlySpan<Byte> methodName, params JArgumentMetadata[] metadata) : base(
		methodName, metadata) { }

	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition passing the
	/// default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	protected void Invoke(JLocalObject jLocal) => this.Invoke(jLocal, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/> passing the default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	protected void Invoke(JLocalObject jLocal, JClassObject jClass)
		=> this.Invoke(jLocal, jClass, this.CreateArgumentsArray());

	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void Invoke(JLocalObject jLocal, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessProvider.CallMethod(jLocal, jLocal.Class, this, false, args);
	}
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void Invoke(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessProvider.CallMethod(jLocal, jClass, this, false, args);
	}
	/// <summary>
	/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessProvider.CallMethod(jLocal, jClass, this, true, args);
	}
	/// <summary>
	/// Invokes a static method on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	protected void StaticInvoke(JClassObject jClass) => this.Invoke(jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a static method on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected void StaticInvoke(JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jClass.Environment;
		env.AccessProvider.CallStaticMethod(jClass, this, args);
	}

	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> definition.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static void Invoke(JMethodDefinition definition, JLocalObject jLocal, JClassObject? jClass = default,
		Boolean nonVirtual = false, IObject?[]? args = default)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessProvider.CallInternalMethod(jLocal, jClass ?? jLocal.Class, definition, nonVirtual,
		                                      args ?? definition.CreateArgumentsArray());
	}
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static void StaticInvoke(JMethodDefinition definition, JClassObject jClass, IObject?[]? args = default)
	{
		IEnvironment env = jClass.Environment;
		env.AccessProvider.CallInternalStaticMethod(jClass, definition, args ?? definition.CreateArgumentsArray());
	}
}