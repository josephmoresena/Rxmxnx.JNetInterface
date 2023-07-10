namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a function definition.
/// </summary>
public abstract record JFunctionDefinition : JCallDefinition
{
	/// <inheritdoc/>
	internal override Type Return => typeof(JReferenceObject);

	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="functionName">Method defined name.</param>
	/// <param name="returnType">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	internal JFunctionDefinition(CString functionName, CString returnType, params JArgumentMetadata[] metadata) 
		: base(functionName, returnType, metadata) { }
}

/// <summary>
/// This class stores a function definition.
/// </summary>
/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
public record JFunctionDefinition<TResult> : JFunctionDefinition where TResult : IDataType<TResult>
{
	/// <inheritdoc/>
	internal override Type Return => typeof(TResult);

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <remarks>This constructor should be never inherited.</remarks>
	public JFunctionDefinition(CString functionName) : base(functionName, TResult.Signature) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JFunctionDefinition(CString functionName, params JArgumentMetadata[] metadata) : base(
		functionName, TResult.Signature, metadata) { }

	/// <summary>
	/// Internal Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="returnType">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	internal JFunctionDefinition(CString functionName, CString returnType, params JArgumentMetadata[] metadata) : base(
		functionName, returnType, metadata) { }

	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition passing the
	/// default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	public TResult? Invoke(JLocalObject jLocal) => this.Invoke(jLocal, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/> passing the default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	public TResult? Invoke(JLocalObject jLocal, JClassObject jClass) => this.Invoke(jLocal, jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a static function on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	public TResult? StaticInvoke(JClassObject jClass) => this.Invoke(jClass, this.CreateArgumentsArray());

	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected TResult? Invoke(JLocalObject jLocal, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		return env.Accessor.CallFunction<TResult>(jLocal, this, args);
	}
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected TResult? Invoke(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		return env.Accessor.CallNonVirtualFunction<TResult>(jLocal, jClass, this, args);
	}
	/// <summary>
	/// Invokes a static function on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected TResult? StaticInvoke(JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jClass.Environment;
		return env.Accessor.CallStaticFunction<TResult>(jClass, this, args);
	}
}