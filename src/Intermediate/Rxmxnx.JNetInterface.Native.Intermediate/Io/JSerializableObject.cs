namespace Rxmxnx.JNetInterface.Io;

/// <summary>
/// This class represents a local <c>java.io.Serializable</c> instance.
/// </summary>
public sealed class JSerializableObject : JInterfaceObject<JSerializableObject>, IInterfaceType<JSerializableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JSerializableObject> typeMetadata =
		JTypeMetadataBuilder<JSerializableObject>.Create(UnicodeClassNames.SerializableInterface()).Build();

	static JInterfaceTypeMetadata<JSerializableObject> IInterfaceType<JSerializableObject>.Metadata
		=> JSerializableObject.typeMetadata;

	/// <inheritdoc/>
	private JSerializableObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JSerializableObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JSerializableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JSerializableObject IReferenceType<JSerializableObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JSerializableObject IReferenceType<JSerializableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JSerializableObject IReferenceType<JSerializableObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}