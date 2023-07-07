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
	public JConstructorDefinition() : this(Array.Empty<JArgumentMetadata>()) { }

	/// <summary>
	/// Internal constructor.
	/// </summary>
	protected JConstructorDefinition(params JArgumentMetadata[] metadata) 
		: base(JMethodDefinitionBase.ConstructorName, metadata) { }
}