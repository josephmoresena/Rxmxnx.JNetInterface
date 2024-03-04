namespace Rxmxnx.JNetInterface.Nio.Ch;

/// <summary>
/// This class represents a local <c>sun.nio.ch.DirectBuffer</c> instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JDirectBufferObject : JInterfaceObject<JDirectBufferObject>, IInterfaceType<JDirectBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JDirectBufferObject> metadata =
		TypeMetadataBuilder<JDirectBufferObject>.Create(UnicodeClassNames.DirectBufferObject()).Build();

	static JInterfaceTypeMetadata<JDirectBufferObject> IInterfaceType<JDirectBufferObject>.Metadata
		=> JDirectBufferObject.metadata;

	/// <inheritdoc/>
	private JDirectBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JDirectBufferObject IInterfaceType<JDirectBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}