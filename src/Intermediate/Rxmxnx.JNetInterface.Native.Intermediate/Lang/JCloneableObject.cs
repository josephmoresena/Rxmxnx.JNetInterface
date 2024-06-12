namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JInterfaceTypeMetadata<JCloneableObject>;

/// <summary>
/// This class represents a local <c>java.lang.Cloneable</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JCloneableObject : JInterfaceObject<JCloneableObject>, IInterfaceType<JCloneableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		TypeMetadataBuilder<JCloneableObject>.Create("java/lang/Cloneable"u8).Build();

	static TypeMetadata IInterfaceType<JCloneableObject>.Metadata => JCloneableObject.typeMetadata;

	/// <inheritdoc/>
	private JCloneableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JCloneableObject IInterfaceType<JCloneableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}