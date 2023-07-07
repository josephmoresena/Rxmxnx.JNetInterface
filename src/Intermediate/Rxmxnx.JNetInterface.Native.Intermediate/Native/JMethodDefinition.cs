namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a method definition.
/// </summary>
public record JMethodDefinition : JMethodDefinitionBase
{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	public JMethodDefinition(CString methodName) : base(methodName) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JMethodDefinition(CString functionName, params JArgumentMetadata[] metadata) 
		: base(functionName, metadata) { }
}