namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a constructor definition.
/// </summary>
public abstract record JConstructorDefinition : JMethodDefinitionBase
{
	/// <inheritdoc/>
	internal override Type? Return => default;
	
	/// <summary>
	/// Internal constructor.
	/// </summary>
	internal JConstructorDefinition(params JArgumentMetadata[] metadata) 
		: base(JMethodDefinitionBase.ConstructorName, metadata) { }
}

/// <summary>
/// This class stores a constructor definition.
/// </summary>
/// <typeparam name="TObject"><see cref="IDataType"/> type of function result.</typeparam>
public record JConstructorDefinition<TObject> : JConstructorDefinition
	where TObject : JReferenceObject, IDataType<TObject>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public JConstructorDefinition() { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JConstructorDefinition(params JArgumentMetadata[] metadata) : base(metadata) { }
}