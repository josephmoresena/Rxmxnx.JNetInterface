namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Cloneable</c> instance.
/// </summary>
public sealed class JCloneableObject : JInterfaceObject<JCloneableObject>, IInterfaceType<JCloneableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JCloneableObject> typeMetadata =
		TypeMetadataBuilder<JCloneableObject>.Create(UnicodeClassNames.CloneableInterface()).Build();

	static JInterfaceTypeMetadata<JCloneableObject> IInterfaceType<JCloneableObject>.Metadata
		=> JCloneableObject.typeMetadata;

	/// <inheritdoc/>
	private JCloneableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JCloneableObject IInterfaceType<JCloneableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}