namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Comparable</c> instance.
/// </summary>
public sealed class JComparableObject : JInterfaceObject<JComparableObject>, IInterfaceType<JComparableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JComparableObject> typeMetadata =
		TypeMetadataBuilder<JComparableObject>.Create(UnicodeClassNames.ComparableInterface()).Build();

	static JInterfaceTypeMetadata<JComparableObject> IInterfaceType<JComparableObject>.Metadata
		=> JComparableObject.typeMetadata;

	/// <inheritdoc/>
	private JComparableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JComparableObject IInterfaceType<JComparableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}