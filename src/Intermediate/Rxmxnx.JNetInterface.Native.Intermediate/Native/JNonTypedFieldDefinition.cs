namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a non-typed class field definition.
/// </summary>
public sealed record JNonTypedFieldDefinition : JFieldDefinition
{
	/// <inheritdoc/>
	internal override Type Return => typeof(JReferenceObject);
	
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="name">Field name.</param>
	/// <param name="signature">Signature field.</param>
	public JNonTypedFieldDefinition(CString name, CString signature) 
		: base(name, JAccessibleObjectDefinition.ValidateSingnature(signature)) { }
}