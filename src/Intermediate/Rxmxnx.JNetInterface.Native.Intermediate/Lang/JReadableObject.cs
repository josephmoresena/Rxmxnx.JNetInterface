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
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		TypeMetadataBuilder<JReadableObject>.Create("java/lang/Readable"u8).Build();

	static TypeMetadata IInterfaceType<JReadableObject>.Metadata => JReadableObject.typeMetadata;

	/// <inheritdoc/>
	private JReadableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JReadableObject IInterfaceType<JReadableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}