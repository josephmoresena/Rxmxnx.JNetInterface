namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Set of base types for given type.
/// </summary>
public sealed record BaseTypeHelper<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TReference>
	where TReference : JReferenceObject, IReferenceType<TReference>
{
	/// <summary>
	/// Internal set.
	/// </summary>
	public IReadOnlySet<Type> Set { get; } = BaseTypeHelper<TReference>.GetBaseTypes().ToImmutableHashSet();

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
}