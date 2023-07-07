namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a function definition.
/// </summary>
public abstract record JFunctionDefinition : JMethodDefinitionBase
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
	internal override Type Return => JAccessibleObjectDefinition.GetReturnType<TResult>();

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
}