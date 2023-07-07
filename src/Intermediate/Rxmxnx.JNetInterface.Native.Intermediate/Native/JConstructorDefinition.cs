namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a constructor definition.
/// </summary>
public record JConstructorDefinition : JMethodDefinitionBase
{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <remarks>This constructor should be never inherited.</remarks>
	public JConstructorDefinition() : this(Array.Empty<JArgumentMetadata>()) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	protected JConstructorDefinition(params JArgumentMetadata[] metadata) 
		: base(JMethodDefinitionBase.ConstructorName, metadata) { }
}