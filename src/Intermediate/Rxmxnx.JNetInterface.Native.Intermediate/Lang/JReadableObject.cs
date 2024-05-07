namespace Rxmxnx.JNetInterface.Lang;

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
	private static readonly JInterfaceTypeMetadata<JReadableObject> typeMetadata =
		TypeMetadataBuilder<JReadableObject>.Create(UnicodeClassNames.ReadableInterface()).Build();

	static JInterfaceTypeMetadata<JReadableObject> IInterfaceType<JReadableObject>.Metadata
		=> JReadableObject.typeMetadata;

	/// <inheritdoc/>
	private JReadableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JReadableObject IInterfaceType<JReadableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}