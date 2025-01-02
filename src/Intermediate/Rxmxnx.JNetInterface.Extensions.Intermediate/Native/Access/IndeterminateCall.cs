namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores the definition of an indeterminate java call.
/// </summary>
public abstract partial class IndeterminateCall : IWrapper<JCallDefinition>
{
	/// <summary>
	/// Return type signature.
	/// </summary>
	public CString ReturnType { get; }
	/// <summary>
	/// Internal call definition.
	/// </summary>
	public JCallDefinition Definition { get; }

	JCallDefinition IWrapper<JCallDefinition>.Value => this.Definition;

	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JMethodDefinition"/> to
	/// <see cref="IndeterminateCall"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> to implicitly convert.</param>
	[return: NotNullIfNotNull(nameof(definition))]
	public static implicit operator IndeterminateCall?(JMethodDefinition? definition)
		=> definition is not null ? new Method(definition) : default;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JConstructorDefinition"/> to
	/// <see cref="IndeterminateCall"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> to implicitly convert.</param>
	[return: NotNullIfNotNull(nameof(definition))]
	public static implicit operator IndeterminateCall?(JConstructorDefinition? definition)
		=> definition is not null ? new Constructor(definition) : default;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JFunctionDefinition"/> to
	/// <see cref="IndeterminateCall"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> to implicitly convert.</param>
	[return: NotNullIfNotNull(nameof(definition))]
	public static implicit operator IndeterminateCall?(JFunctionDefinition? definition)
	{
		if (definition is null) return default;
		CString returnType = IndeterminateCall.GetReturnType(definition);
		return new Function(definition, returnType);
	}

	/// <summary>
	/// Creates a <see cref="IndeterminateCall"/> instance for a java constructor.
	/// </summary>
	/// <param name="args">Metadata of the types of call arguments.</param>
	/// <returns>A new <see cref="IndeterminateCall"/> instance.</returns>
	public static IndeterminateCall CreateConstructorDefinition(ReadOnlySpan<JArgumentMetadata> args)
	{
		JConstructorDefinition definition = JConstructorDefinition.Create(args);
		return new Constructor(definition);
	}
	/// <summary>
	/// Creates a <see cref="IndeterminateCall"/> instance for a java method.
	/// </summary>
	/// <param name="methodName">UTF-8 method name.</param>
	/// <param name="args">Metadata of the types of call arguments.</param>
	/// <returns>A new <see cref="IndeterminateCall"/> instance.</returns>
	public static IndeterminateCall CreateMethodDefinition(ReadOnlySpan<Byte> methodName,
		ReadOnlySpan<JArgumentMetadata> args)
	{
		if (CommonNames.Constructor.SequenceEqual(methodName))
			return IndeterminateCall.CreateConstructorDefinition(args);

		JMethodDefinition definition = JMethodDefinition.Create(methodName, args);
		return new Method(definition);
	}
	/// <summary>
	/// Creates a <see cref="IndeterminateCall"/> instance for a java function.
	/// </summary>
	/// <param name="returnType">Return type metadata.</param>
	/// <param name="functionName">UTF-8 function name.</param>
	/// <param name="args">Metadata of the types of call arguments.</param>
	/// <returns>A new <see cref="IndeterminateCall"/> instance.</returns>
	public static IndeterminateCall CreateFunctionDefinition(JArgumentMetadata returnType,
		ReadOnlySpan<Byte> functionName, ReadOnlySpan<JArgumentMetadata> args)
	{
		if (CommonNames.Constructor.SequenceEqual(functionName))
			return IndeterminateCall.CreateConstructorDefinition(args);

		JFunctionDefinition definition = returnType.Signature.Length == 1 ?
			IndeterminateCall.CreatePrimitiveFunction(functionName, returnType.Signature, args) :
			new JNonTypedFunctionDefinition(functionName, returnType.Signature, args);
		return new Function(definition, returnType.Signature);
	}
	/// <summary>
	/// Creates a <see cref="IndeterminateCall"/> instance for a java function.
	/// </summary>
	/// <typeparam name="TResult">Return <see cref="IDataType{TResult}"/> type.</typeparam>
	/// <param name="functionName">UTF-8 function name.</param>
	/// <param name="args">Metadata of the types of call arguments.</param>
	/// <returns>A new <see cref="IndeterminateCall"/> instance.</returns>
	public static IndeterminateCall CreateFunctionDefinition<TResult>(ReadOnlySpan<Byte> functionName,
		ReadOnlySpan<JArgumentMetadata> args) where TResult : IDataType<TResult>
	{
		if (CommonNames.Constructor.SequenceEqual(functionName))
			return IndeterminateCall.CreateConstructorDefinition(args);

		JDataTypeMetadata typeMetadata = IDataType.GetMetadata<TResult>();
		JFunctionDefinition definition = typeMetadata is not JReferenceTypeMetadata referenceTypeMetadata ?
			IndeterminateCall.CreatePrimitiveFunction(functionName, typeMetadata.Signature, args) :
			referenceTypeMetadata.CreateFunctionDefinition(functionName, args);
		return new Function(definition, typeMetadata.Signature);
	}
}