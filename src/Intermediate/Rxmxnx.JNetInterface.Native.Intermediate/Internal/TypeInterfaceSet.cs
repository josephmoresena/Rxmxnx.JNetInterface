namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Set of interfaces for given type.
/// </summary>
internal sealed record TypeInterfaceSet<TReference> : IReadOnlySet<Type>
	where TReference : JReferenceObject, IReferenceType<TReference>
{
	/// <summary>
	/// Internal set.
	/// </summary>
	private readonly IReadOnlySet<Type> _instance = typeof(TReference).GetInterfaces().ToImmutableHashSet();

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
}