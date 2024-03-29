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
		JTypeMetadataBuilder<JCloneableObject>.Create(UnicodeClassNames.CloneableInterface()).Build();

	static JInterfaceTypeMetadata<JCloneableObject> IInterfaceType<JCloneableObject>.Metadata
		=> JCloneableObject.typeMetadata;

	/// <inheritdoc/>
	private JCloneableObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JCloneableObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JCloneableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JCloneableObject IReferenceType<JCloneableObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JCloneableObject IReferenceType<JCloneableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JCloneableObject IReferenceType<JCloneableObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}