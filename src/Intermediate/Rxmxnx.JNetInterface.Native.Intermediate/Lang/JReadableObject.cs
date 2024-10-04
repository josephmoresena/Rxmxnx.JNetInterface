namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JInterfaceTypeMetadata<JReadableObject>;

/// <summary>
/// This class represents a local <c>java.lang.Readable</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JReadableObject : JInterfaceObject<JReadableObject>, IInterfaceType<JReadableObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ReadableHash, 18);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JReadableObject>(JReadableObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JReadableObject>.Metadata => JReadableObject.typeMetadata;

	/// <inheritdoc/>
	private JReadableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JReadableObject IInterfaceType<JReadableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}