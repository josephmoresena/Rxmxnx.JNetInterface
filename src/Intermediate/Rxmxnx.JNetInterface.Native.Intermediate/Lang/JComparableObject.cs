namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JInterfaceTypeMetadata<JComparableObject>;

/// <summary>
/// This class represents a local <c>java.lang.Comparable</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JComparableObject : JInterfaceObject<JComparableObject>, IInterfaceType<JComparableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		TypeMetadataBuilder<JComparableObject>.Create("java/lang/Comparable"u8).Build();

	static TypeMetadata IInterfaceType<JComparableObject>.Metadata => JComparableObject.typeMetadata;

	/// <inheritdoc/>
	private JComparableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JComparableObject IInterfaceType<JComparableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}