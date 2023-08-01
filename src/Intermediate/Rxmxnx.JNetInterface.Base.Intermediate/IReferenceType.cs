namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
public interface IReferenceType : IObject, IDataType, IDisposable
{
	/// <summary>
	/// Excluding CLR types.
	/// </summary>
	internal static abstract IImmutableSet<Type> ExcludingTypes { get; }

	/// <summary>
	/// Retrieves the metadata for given reference type.
	/// </summary>
	/// <typeparam name="TReference">Type of current java reference datatype.</typeparam>
	/// <returns>The <see cref="JReferenceMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JReferenceMetadata GetMetadata<TReference>() where TReference : IReferenceType
		=> (JReferenceMetadata)IDataType.GetMetadata<TReference>();
}

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
/// <typeparam name="TReference">Type of java reference type.</typeparam>
public interface
	IReferenceType<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] out TReference> : IReferenceType,
		IDataType<TReference> where TReference : JReferenceObject, IReferenceType<TReference>
{
	/// <inheritdoc cref="IDataType{TReference}.Interfaces"/>
	private static readonly IImmutableSet<Type> interfaces = IReferenceType<TReference>
	                                                         .FilterInterfaces(typeof(TReference).GetInterfaces())
	                                                         .ToImmutableHashSet();
	/// <inheritdoc cref="IDataType{TReference}.Bases"/>
	private static readonly IImmutableSet<Type> bases = IReferenceType<TReference>.GetBaseTypes().ToImmutableHashSet();

	/// <summary>
	/// Excluding generic CLR types.
	/// </summary>
	internal static abstract IImmutableSet<Type> ExcludingGenericTypes { get; }

	static IImmutableSet<Type> IDataType<TReference>.Interfaces => IReferenceType<TReference>.interfaces;
	static IImmutableSet<Type> IDataType<TReference>.Bases => IReferenceType<TReference>.bases;

	/// <summary>
	/// Indicates whether <typeparamref name="TReference"/> is derived from <paramref name="type"/>.
	/// </summary>
	/// <param name="type">CLR type.</param>
	/// <returns>
	/// <see langword="true"/> if <typeparamref name="TReference"/> is derived from <paramref name="type"/>;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	protected static Boolean IsDerivedFrom(Type type) => IReferenceType<TReference>.bases.Contains(type);
	/// <summary>
	/// Indicates whether <typeparamref name="TReference"/> is derived from <typeparamref name="TOther"/>.
	/// </summary>
	/// <typeparam name="TOther">Other type of java reference type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if <typeparamref name="TReference"/> is derived from <typeparamref name="TOther"/>;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
	public static Boolean IsDerivedFrom<TOther>() where TOther : JReferenceObject, IReferenceType<TOther>
		=> IReferenceType<TReference>.bases.Contains(typeof(TOther));
	/// <summary>
	/// Indicates whether current type implements <typeparamref name="TInterface"/> type.
	/// </summary>
	/// <typeparam name="TInterface">Type of interface.</typeparam>
	/// <returns>
	/// <see langword="true"/> if <typeparamref name="TReference"/> extends or implements from
	/// <typeparamref name="TInterface"/>;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean HasInterface<TInterface>() where TInterface : JReferenceObject, IInterfaceType<TInterface>
		=> IReferenceType<TReference>.interfaces.Contains(typeof(IDerivedType<TReference, TInterface>));

	/// <summary>
	/// Retrieves the base types from current type.
	/// </summary>
	/// <returns>Enumerable of types.</returns>
	private static IEnumerable<Type> GetBaseTypes()
	{
		Type? currentType = typeof(TReference).BaseType;
		while (currentType is not null && currentType != typeof(JReferenceObject))
		{
			yield return currentType;
			currentType = currentType.BaseType;
		}
	}
	/// <summary>
	/// Retrieves the interfaces types from current type.
	/// </summary>
	/// <param name="typeInterfaces">Current type interfaces.</param>
	/// <returns>Enumerable of types.</returns>
	private static IEnumerable<Type> FilterInterfaces(IEnumerable<Type> typeInterfaces)
	{
		foreach (Type interfaceType in typeInterfaces)
		{
			if (TReference.ExcludingTypes.Contains(interfaceType))
				continue;
			if (TReference.ExcludingGenericTypes.Contains(interfaceType))
				continue;
			yield return interfaceType;
		}
	}
}