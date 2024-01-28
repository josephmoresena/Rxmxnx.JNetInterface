namespace Rxmxnx.JNetInterface.Native.Access;

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
	internal JFunctionDefinition(ReadOnlySpan<Byte> functionName, ReadOnlySpan<Byte> returnType,
		params JArgumentMetadata[] metadata) : base(functionName, returnType, metadata) { }

	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="definition">A <see cref="JFunctionDefinition{TResult}"/> definition.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static TResult? Invoke<TResult>(JFunctionDefinition<TResult> definition, JLocalObject jLocal,
		JClassObject? jClass = default, Boolean nonVirtual = false, IObject?[]? args = default)
		where TResult : IDataType<TResult>
		=> JFunctionDefinition<TResult>.Invoke(definition, jLocal, jClass, nonVirtual, args);

	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <param name="definition">A <see cref="JFunctionDefinition{TResult}"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static TResult? StaticInvoke<TResult>(JFunctionDefinition<TResult> definition, JClassObject jClass,
		IObject?[]? args = default) where TResult : IDataType<TResult>
		=> JFunctionDefinition<TResult>.StaticInvoke(definition, jClass, args);
}

/// <summary>
/// This class stores a function definition.
/// </summary>
/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
public record JFunctionDefinition<TResult> : JFunctionDefinition where TResult : IDataType<TResult>
{
	/// <inheritdoc/>
	internal override Type Return => JAccessibleObjectDefinition.ReturnType<TResult>();

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <remarks>This constructor should be never inherited.</remarks>
	public JFunctionDefinition(ReadOnlySpan<Byte> functionName) : base(functionName,
	                                                                   IDataType.GetMetadata<TResult>().Signature) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JFunctionDefinition(ReadOnlySpan<Byte> functionName, params JArgumentMetadata[] metadata) : base(
		functionName, IDataType.GetMetadata<TResult>().Signature, metadata) { }

	/// <summary>
	/// Internal Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="returnType">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	internal JFunctionDefinition(ReadOnlySpan<Byte> functionName, ReadOnlySpan<Byte> returnType,
		params JArgumentMetadata[] metadata) : base(functionName, returnType, metadata) { }
	/// <summary>
	/// Internal Constructor.
	/// </summary>
	/// <param name="definition">Function definition name.</param>
	internal JFunctionDefinition(JFunctionDefinition definition) : base(definition) { }

	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition passing the
	/// default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	protected TResult? Invoke(JLocalObject jLocal) => this.Invoke(jLocal, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/> passing the default value for each argument.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	protected TResult? Invoke(JLocalObject jLocal, JClassObject jClass)
		=> this.Invoke(jLocal, jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected TResult? Invoke(JLocalObject jLocal, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallFunction<TResult>(jLocal, jLocal.Class, this, false, args);
	}
	/// <summary>
	/// Invokes a function on <paramref name="jClass"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected TResult? Invoke(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallFunction<TResult>(jLocal, jClass, this, false, args);
	}
	/// <summary>
	/// Invokes a function on <paramref name="jLocal"/> which matches with current definition but using the
	/// implementation declared on <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected TResult? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallFunction<TResult>(jLocal, jClass, this, true, args);
	}
	/// <summary>
	/// Invokes a static function on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	protected TResult? StaticInvoke(JClassObject jClass) => this.Invoke(jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a static function on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	protected TResult? StaticInvoke(JClassObject jClass, IObject?[] args)
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallStaticFunction<TResult>(jClass, this, args);
	}

	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition{TResult}"/> definition.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static TResult? Invoke(JFunctionDefinition<TResult> definition, JLocalObject jLocal,
		JClassObject? jClass = default, Boolean nonVirtual = false, IObject?[]? args = default)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.CallInternalFunction<TResult>(jLocal, jClass ?? jLocal.Class, definition, nonVirtual,
		                                                       args ?? definition.CreateArgumentsArray());
	}
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jClass"/> which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition{TResult}"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static TResult? StaticInvoke(JFunctionDefinition<TResult> definition, JClassObject jClass,
		IObject?[]? args = default)
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallInternalStaticFunction<TResult>(jClass, definition,
		                                                             args ?? definition.CreateArgumentsArray());
	}

	/// <summary>
	/// Create a <see cref="JFunctionDefinition{TResult}"/> instance for <paramref name="metadata"/>.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JFunctionDefinition{TResult}"/> instance.</returns>
	internal static JFunctionDefinition<TResult> Create(ReadOnlySpan<Byte> functionName, JArgumentMetadata[] metadata)
		=> new(functionName, metadata);
}