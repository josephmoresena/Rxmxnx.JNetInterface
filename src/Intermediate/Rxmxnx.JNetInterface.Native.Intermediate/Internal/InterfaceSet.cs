namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Interface record set.
/// </summary>
internal partial record InterfaceSet : IReadOnlySet<JInterfaceTypeMetadata>
{
	/// <summary>
	/// Internal set.
	/// </summary>
	private readonly IReadOnlySet<JInterfaceTypeMetadata> _internalSet;
	/// <summary>
	/// Internal set.
	/// </summary>
	private String? _stringRepresentation;

	/// <summary>
	/// Internal enumeration.
	/// </summary>
	protected virtual IEnumerable<JInterfaceTypeMetadata> Enumerable => this._internalSet;
	/// <inheritdoc/>
	public virtual Int32 Count => this._internalSet.Count;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="set">A <see cref="IReadOnlySet{T}"/> instance.</param>
	private InterfaceSet(IReadOnlySet<JInterfaceTypeMetadata> set) => this._internalSet = set;

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
	private IReadOnlySet<JInterfaceTypeMetadata> GetFullSet()
	{
		if (this.Enumerable is IReadOnlySet<JInterfaceTypeMetadata> set) return set;
		return new HashSet<JInterfaceTypeMetadata>(this.Enumerable);
	}
}