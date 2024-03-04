namespace Rxmxnx.JNetInterface.Io;

/// <summary>
/// This class represents a local <c>java.io.Serializable</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JSerializableObject : JInterfaceObject<JSerializableObject>, IInterfaceType<JSerializableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JSerializableObject> typeMetadata =
		TypeMetadataBuilder<JSerializableObject>.Create(UnicodeClassNames.SerializableInterface()).Build();

	static JInterfaceTypeMetadata<JSerializableObject> IInterfaceType<JSerializableObject>.Metadata
		=> JSerializableObject.typeMetadata;

	/// <inheritdoc/>
	private JSerializableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JSerializableObject IInterfaceType<JSerializableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}