namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Set of base types for given type.
/// </summary>
public sealed record BaseTypeSet<TReference> : IReadOnlySet<Type>
	where TReference : JReferenceObject, IReferenceType<TReference>
{
	/// <summary>
	/// Internal set.
	/// </summary>
	private readonly IReadOnlySet<Type> _instance = BaseTypeSet<TReference>.GetBaseTypes().ToImmutableHashSet();

	IEnumerator<Type> IEnumerable<Type>.GetEnumerator() => this._instance.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => this._instance.GetEnumerator();
	Int32 IReadOnlyCollection<Type>.Count => this._instance.Count;
	Boolean IReadOnlySet<Type>.Contains(Type item) => this._instance.Contains(item);
	Boolean IReadOnlySet<Type>.IsProperSubsetOf(IEnumerable<Type> other) => this._instance.IsProperSubsetOf(other);
	Boolean IReadOnlySet<Type>.IsProperSupersetOf(IEnumerable<Type> other) => this._instance.IsProperSupersetOf(other);
	Boolean IReadOnlySet<Type>.IsSubsetOf(IEnumerable<Type> other) => this._instance.IsSubsetOf(other);
	Boolean IReadOnlySet<Type>.IsSupersetOf(IEnumerable<Type> other) => this._instance.IsSupersetOf(other);
	Boolean IReadOnlySet<Type>.Overlaps(IEnumerable<Type> other) => this._instance.Overlaps(other);
	Boolean IReadOnlySet<Type>.SetEquals(IEnumerable<Type> other) => this._instance.SetEquals(other);

	/// <summary>
	/// Retrieves the base types from current type.
	/// </summary>
	/// <returns>Enumerable of types.</returns>
	internal static IEnumerable<Type> GetBaseTypes()
	{
		Type? currentType = typeof(TReference).BaseType;
		while (currentType is not null && currentType != typeof(JReferenceObject))
		{
			yield return currentType;
			currentType = currentType.BaseType;
		}
	}
}