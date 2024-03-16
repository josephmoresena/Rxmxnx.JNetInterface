namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
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
	/// <returns>A <see cref="IInterfaceSet"/> instance.</returns>
	public static IInterfaceSet GetClassInterfaces(JClassTypeMetadata? baseMetadata,
		IReadOnlySet<JInterfaceTypeMetadata> interfaces)
	{
		if (baseMetadata is null)
			return interfaces.Count == 0 ? InterfaceSet.Empty : new(interfaces.ToImmutableHashSet());
		return interfaces.Count == 0 ?
			baseMetadata.Interfaces :
			new ClassInterfaceSet(baseMetadata, interfaces.ToImmutableHashSet());
	}
	/// <summary>
	/// Retrieves a set with interface super interfaces.
	/// </summary>
	/// <param name="interfaces">A <see cref="IReadOnlySet{JInterfaceTypeMetadata}"/> instance.</param>
	/// <returns>A <see cref="IInterfaceSet"/> instance.</returns>
	public static IInterfaceSet GetInterfaceInterfaces(IReadOnlySet<JInterfaceTypeMetadata> interfaces)
		=> interfaces.Count == 0 ? InterfaceSet.Empty : new InterfaceInterfaceSet(interfaces.ToImmutableHashSet());

	/// <summary>
	/// Internal for each implementation.
	/// </summary>
	/// <typeparam name="T">Type of state object.</typeparam>
	/// <param name="args">Execution args.</param>
	/// <param name="interfaceMetadata">A <see cref="JInterfaceObject{TInterface}"/> instance.</param>
	private static void ForEachImpl<T>(
		(T state, HashSet<String> hashes, Boolean recursive, Action<T, JInterfaceTypeMetadata> action) args,
		JInterfaceTypeMetadata interfaceMetadata)
	{
		if (!args.hashes.Add(interfaceMetadata.Hash)) return;
		args.action(args.state, interfaceMetadata);
		if (args.recursive)
			interfaceMetadata.Interfaces.ForEach(args, InterfaceSet.ForEachImpl);
	}
}