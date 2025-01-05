namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Constructor implementation.
	/// </summary>
	/// <param name="definition">Internal <see cref="JConstructorDefinition"/> instance.</param>
	private sealed class Constructor(JConstructorDefinition definition)
		: IndeterminateCall(definition, JPrimitiveTypeMetadata.VoidMetadata.Signature);

	/// <summary>
	/// Method implementation.
	/// </summary>
	/// <param name="definition">Internal <see cref="JMethodDefinition"/> instance.</param>
	private sealed class Method(JMethodDefinition definition)
		: IndeterminateCall(definition, JPrimitiveTypeMetadata.VoidMetadata.Signature);

	/// <summary>
	/// Function implementation.
	/// </summary>
	/// <param name="definition">Internal <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="returnType">Return type signature.</param>
	private sealed class Function(JFunctionDefinition definition, CString returnType)
		: IndeterminateCall(definition, returnType);
}