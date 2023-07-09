namespace Rxmxnx.JNetInterface.Native;

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
	public JMethodDefinition(CString methodName) : base(methodName) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Function name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JMethodDefinition(CString methodName, params JArgumentMetadata[] metadata) 
		: base(methodName, metadata) { }
}