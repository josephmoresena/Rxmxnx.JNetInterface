namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Interface record set.
/// </summary>
internal partial class InterfaceSet : IReadOnlySet<JInterfaceTypeMetadata>
{
	/// <summary>
	/// Internal set.
	/// </summary>
	private readonly ImmutableHashSet<JInterfaceTypeMetadata> _internalSet;
	/// <summary>
	/// Cached set.
	/// </summary>
	private ImmutableHashSet<JInterfaceTypeMetadata>? _cachedSet;
	/// <summary>
	/// Internal set.
	/// </summary>
	private String? _stringRepresentation;

	/// <summary>
	/// Internal enumeration.
	/// </summary>
	protected virtual IEnumerable<JInterfaceTypeMetadata> Enumerable => this._internalSet;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="set">A <see cref="IReadOnlySet{T}"/> instance.</param>
	private InterfaceSet(ImmutableHashSet<JInterfaceTypeMetadata> set) => this._internalSet = set;
	/// <inheritdoc/>
	public virtual Int32 Count => this._internalSet.Count;

	[ExcludeFromCodeCoverage]
	IEnumerator<JInterfaceTypeMetadata> IEnumerable<JInterfaceTypeMetadata>.GetEnumerator()
		=> this.Enumerable.GetEnumerator();
	[ExcludeFromCodeCoverage]
	IEnumerator IEnumerable.GetEnumerator() => this.Enumerable.GetEnumerator();
	[ExcludeFromCodeCoverage]
	Boolean IReadOnlySet<JInterfaceTypeMetadata>.IsProperSubsetOf(IEnumerable<JInterfaceTypeMetadata> other)
		=> this.GetFullSet().IsProperSubsetOf(other);
	[ExcludeFromCodeCoverage]
	Boolean IReadOnlySet<JInterfaceTypeMetadata>.IsProperSupersetOf(IEnumerable<JInterfaceTypeMetadata> other)
		=> this.GetFullSet().IsProperSupersetOf(other);
	[ExcludeFromCodeCoverage]
	Boolean IReadOnlySet<JInterfaceTypeMetadata>.IsSubsetOf(IEnumerable<JInterfaceTypeMetadata> other)
		=> this.GetFullSet().IsSubsetOf(other);
	[ExcludeFromCodeCoverage]
	Boolean IReadOnlySet<JInterfaceTypeMetadata>.IsSupersetOf(IEnumerable<JInterfaceTypeMetadata> other)
		=> this.GetFullSet().IsSubsetOf(other);
	[ExcludeFromCodeCoverage]
	Boolean IReadOnlySet<JInterfaceTypeMetadata>.Overlaps(IEnumerable<JInterfaceTypeMetadata> other)
		=> this.GetFullSet().Overlaps(other);
	[ExcludeFromCodeCoverage]
	Boolean IReadOnlySet<JInterfaceTypeMetadata>.SetEquals(IEnumerable<JInterfaceTypeMetadata> other)
		=> this.GetFullSet().SetEquals(other);

	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public virtual Boolean Contains(JInterfaceTypeMetadata item) => this._internalSet.Contains(item);

	/// <inheritdoc/>
	public override String ToString()
	{
		if (String.IsNullOrEmpty(this._stringRepresentation))
			this._stringRepresentation = $"[{String.Join(", ", this.Enumerable.Select(i => i.ClassName))}]";
		return this._stringRepresentation;
	}

	/// <summary>
	/// Creates a hash set from current instance.
	/// </summary>
	/// <returns>A <see cref="HashSet{JInterfaceTypeMetadata}"/> instance,</returns>
	[ExcludeFromCodeCoverage]
	private ImmutableHashSet<JInterfaceTypeMetadata> GetFullSet() => this._cachedSet ??= this.GetFullSet();
}