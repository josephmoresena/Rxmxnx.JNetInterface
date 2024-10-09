namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Operation hash set.
	/// </summary>
	[ThreadStatic]
	private static HashSet<String>? operationHashes;

	/// <summary>
	/// Empty interface set.
	/// </summary>
	public static readonly InterfaceSet Empty = new([]);
	/// <summary>
	/// Array interface set.
	/// </summary>
	public static readonly IInterfaceSet ArraySet = ArrayInterfaceSet.Instance;
	/// <summary>
	/// Annotation interface set.
	/// </summary>
	public static readonly IInterfaceSet AnnotationSet = AnnotationInterfaceSet.Instance;
	/// <summary>
	/// Serializable interface set.
	/// </summary>
	public static readonly IInterfaceSet SerializableSet = SerializableInterfaceSet.Instance;
	/// <summary>
	/// Comparable interface set.
	/// </summary>
	public static readonly IInterfaceSet ComparableSet = ComparableInterfaceSet.Instance;
	/// <summary>
	/// AnnotatedElement interface set.
	/// </summary>
	public static readonly IInterfaceSet AnnotatedElementSet = AnnotatedElementInterfaceSet.Instance;
	/// <summary>
	/// Serializable and Comparable interface set.
	/// </summary>
	public static readonly IInterfaceSet SerializableComparableSet = SerializableComparableInterfaceSet.Instance;

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
	/// Defines an explicit conversion of a given <see cref="ImmutableHashSet{JInterfaceTypeMetadata}"/> to
	/// <see cref="InterfaceSet"/>.
	/// </summary>
	/// <param name="interfaces">A <see cref="ImmutableHashSet{JInterfaceTypeMetadata}"/> to implicitly convert.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator InterfaceSet(ImmutableHashSet<JInterfaceTypeMetadata> interfaces)
		=> new(interfaces);

	/// <summary>
	/// Initializes an operation and retrieves operation hash set.
	/// </summary>
	/// <param name="isNew">Output. Indicates whether current operation is new.</param>
	/// <returns>A <see cref="HashSet{String}"/> instance.</returns>
	private static HashSet<String> OpenSetOperation(out Boolean isNew)
	{
		isNew = InterfaceSet.operationHashes is null;
		return InterfaceSet.operationHashes ??= [];
	}
	/// <summary>
	/// Initializes an operation and retrieves operation hash set.
	/// </summary>
	/// <param name="interfaceSet">Current interface set.</param>
	/// <param name="isNew">Output. Indicates whether current operation is new.</param>
	/// <param name="isRecursive">Output. Indicates whether current operation is recursive.</param>
	/// <returns>A <see cref="HashSet{String}"/> instance.</returns>
	private static HashSet<String> OpenSetOperation(InterfaceSet interfaceSet, out Boolean isNew,
		out Boolean isRecursive)
	{
		isNew = InterfaceSet.operationHashes is null;
		isRecursive = interfaceSet is InterfaceInterfaceSet;
		return InterfaceSet.operationHashes ??= [];
	}
	/// <summary>
	/// Closes current operation.
	/// </summary>
	/// <param name="isNew">Indicates if operation is new.</param>
	private static void CloseSetOperation(Boolean isNew)
	{
		if (isNew) InterfaceSet.operationHashes = default;
	}
}