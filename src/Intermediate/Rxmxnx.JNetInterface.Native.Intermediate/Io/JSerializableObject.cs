namespace Rxmxnx.JNetInterface.Io;

using TypeMetadata = JInterfaceTypeMetadata<JSerializableObject>;

/// <summary>
/// This class represents a local <c>java.io.Serializable</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JSerializableObject : JInterfaceObject<JSerializableObject>, IInterfaceType<JSerializableObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.SerializableHash, 20);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JSerializableObject>(
			JSerializableObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JSerializableObject>.Metadata => JSerializableObject.typeMetadata;

	/// <inheritdoc/>
	private JSerializableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JSerializableObject IInterfaceType<JSerializableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}