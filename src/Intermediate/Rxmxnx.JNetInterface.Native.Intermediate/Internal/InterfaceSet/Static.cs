namespace Rxmxnx.JNetInterface.Internal;

internal partial record InterfaceSet
{
	/// <summary>
	/// Empty interface set.
	/// </summary>
	public static readonly InterfaceSet Empty = new(ImmutableHashSet<JInterfaceTypeMetadata>.Empty);
	/// <summary>
	/// Array interface set.
	/// </summary>
	public static readonly InterfaceSet ArraySet =
		new(ImmutableHashSet.Create(IInterfaceType.GetMetadata<JSerializableObject>(),
		                            IInterfaceType.GetMetadata<JCloneableObject>()));
	/// <summary>
	/// Annotation interface set.
	/// </summary>
	public static readonly InterfaceSet AnnotationSet =
		new(ImmutableHashSet.Create(IInterfaceType.GetMetadata<JAnnotationObject>()));

	/// <summary>
	/// Primitive wrapper interface set.
	/// </summary>
	public static readonly InterfaceSet PrimitiveWrapperSet =
		new(ImmutableHashSet.Create(IInterfaceType.GetMetadata<JSerializableObject>(),
		                            IInterfaceType.GetMetadata<JComparableObject>()));

	/// <summary>
	/// Retrieves a set with class interfaces.
	/// </summary>
	/// <param name="baseMetadata">A <see cref="JClassTypeMetadata"/> instance.</param>
	/// <param name="interfaces">A <see cref="IReadOnlySet{JInterfaceTypeMetadata}"/> instance.</param>
	/// <returns>A <see cref="IReadOnlySet{JInterfaceTypeMetadata}"/> instance.</returns>
	public static IReadOnlySet<JInterfaceTypeMetadata> GetClassInterfaces(JClassTypeMetadata? baseMetadata,
		IReadOnlySet<JInterfaceTypeMetadata> interfaces)
	{
		if (baseMetadata is null)
			return interfaces.Count == 0 ? InterfaceSet.Empty : new(interfaces);
		return interfaces.Count == 0 ? baseMetadata.Interfaces : new ClassInterfaceSet(baseMetadata, interfaces);
	}
	/// <summary>
	/// Retrieves a set with interface super interfaces.
	/// </summary>
	/// <param name="interfaces">A <see cref="IReadOnlySet{JInterfaceTypeMetadata}"/> instance.</param>
	/// <returns>A <see cref="IReadOnlySet{JInterfaceTypeMetadata}"/> instance.</returns>
	public static IReadOnlySet<JInterfaceTypeMetadata>
		GetInterfaceInterfaces(IReadOnlySet<JInterfaceTypeMetadata> interfaces)
		=> interfaces.Count == 0 ? InterfaceSet.Empty : new InterfaceInterfaceSet(interfaces);
}