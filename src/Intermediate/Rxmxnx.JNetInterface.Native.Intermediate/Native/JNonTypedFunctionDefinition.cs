namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a non-typed class function definition.
/// </summary>
public sealed record JNonTypedFunctionDefinition : JFunctionDefinition<JLocalObject>
{
	/// <inheritdoc/>
	internal override Type Return => typeof(JReferenceObject);

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="returnType">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	public JNonTypedFunctionDefinition(CString functionName, CString returnType, params JArgumentMetadata[] metadata) :
		base(functionName, JAccessibleObjectDefinition.ValidateSingnature(returnType), metadata) { }
}