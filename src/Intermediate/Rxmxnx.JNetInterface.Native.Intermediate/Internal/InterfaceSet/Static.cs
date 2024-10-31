namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Empty interface set.
	/// </summary>
	public static readonly InterfaceSet Empty = new([]);
	/// <summary>
	/// Annotation interface set.
	/// </summary>
	public static readonly IInterfaceSet AnnotationSet = GenericInterfaceSet<JAnnotationObject>.Instance;
	/// <summary>
	/// Serializable interface set.
	/// </summary>
	public static readonly IInterfaceSet SerializableSet = GenericInterfaceSet<JSerializableObject>.Instance;
	/// <summary>
	/// Comparable interface set.
	/// </summary>
	public static readonly IInterfaceSet ComparableSet = GenericInterfaceSet<JComparableObject>.Instance;
	/// <summary>
	/// AnnotatedElement interface set.
	/// </summary>
	public static readonly IInterfaceSet AnnotatedElementSet = GenericInterfaceSet<JAnnotatedElementObject>.Instance;
	/// <summary>
	/// Serializable and Comparable interface set.
	/// </summary>
	public static readonly IInterfaceSet SerializableComparableSet =
		GenericInterfaceSet<JSerializableObject, JComparableObject>.Instance;
	/// <summary>
	/// Array interface set.
	/// </summary>
	public static readonly IInterfaceSet SerializableCloneableSet = GenericInterfaceSet<JSerializableObject, JCloneableObject>.Instance;

	/// <summary>
	/// Retrieves a set with class interfaces.
	/// </summary>
	/// <param name="baseMetadata">A <see cref="JClassTypeMetadata"/> instance.</param>
	/// <param name="interfaces">A <see cref="IReadOnlySet{JInterfaceTypeMetadata}"/> instance.</param>
	/// <returns>A <see cref="IInterfaceSet"/> instance.</returns>
	public static IInterfaceSet GetClassInterfaces(JClassTypeMetadata? baseMetadata,
		IReadOnlySet<JInterfaceTypeMetadata> interfaces)
	{
		if (baseMetadata is null)
			return interfaces.Count == 0 ? InterfaceSet.Empty : new([.. interfaces,]);
		return interfaces.Count == 0 ? baseMetadata.Interfaces : new ClassInterfaceSet(baseMetadata, [.. interfaces,]);
	}
	/// <summary>
	/// Retrieves a set with class interfaces.
	/// </summary>
	/// <param name="baseMetadata">A <see cref="JClassTypeMetadata"/> instance.</param>
	/// <param name="interfaces">A <see cref="ImmutableHashSet{JInterfaceTypeMetadata}"/> instance.</param>
	/// <returns>A <see cref="IInterfaceSet"/> instance.</returns>
	public static IInterfaceSet GetClassInterfaces(JClassTypeMetadata baseMetadata,
		ImmutableHashSet<JInterfaceTypeMetadata> interfaces)
		=> interfaces.Count == 0 ? baseMetadata.Interfaces : new ClassInterfaceSet(baseMetadata, interfaces);
	/// <summary>
	/// Retrieves a set with interface super interfaces.
	/// </summary>
	/// <param name="interfaces">A <see cref="IReadOnlySet{JInterfaceTypeMetadata}"/> instance.</param>
	/// <returns>A <see cref="IInterfaceSet"/> instance.</returns>
	public static IInterfaceSet GetInterfaceInterfaces(IReadOnlySet<JInterfaceTypeMetadata> interfaces)
		=> interfaces.Count == 0 ? InterfaceSet.Empty : new InterfaceInterfaceSet([.. interfaces,]);
	
	/// <summary>
	/// Indicates whether if <paramref name="item"/> and <paramref name="local"/> are same.
	/// </summary>
	/// <param name="item">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
	/// <param name="local">Other <see cref="JInterfaceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="item"/> and <paramref name="local"/> are same;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean SameInterface(JInterfaceTypeMetadata item, JInterfaceTypeMetadata local)
	{
		ReadOnlySpan<Char> itemHash = item.Hash;
		ReadOnlySpan<Char> localHash = local.Hash;
		return itemHash.SequenceEqual(localHash);
	}

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="ImmutableHashSet{JInterfaceTypeMetadata}"/> to
	/// <see cref="InterfaceSet"/>.
	/// </summary>
	/// <param name="interfaces">A <see cref="ImmutableHashSet{JInterfaceTypeMetadata}"/> to implicitly convert.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator InterfaceSet(ImmutableHashSet<JInterfaceTypeMetadata> interfaces)
		=> new(interfaces);
}