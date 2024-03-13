namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Interface record set.
/// </summary>
internal partial class InterfaceSet : IInterfaceSet
{
	/// <summary>
	/// Internal set.
	/// </summary>
	private readonly ImmutableHashSet<JInterfaceTypeMetadata> _internalSet;
	/// <summary>
	/// Internal set.
	/// </summary>
	private String? _stringRepresentation;

	/// <inheritdoc/>
	public virtual IEnumerable<JInterfaceTypeMetadata> Enumerable => this._internalSet;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="set">A <see cref="IReadOnlySet{T}"/> instance.</param>
	private InterfaceSet(ImmutableHashSet<JInterfaceTypeMetadata> set) => this._internalSet = set;

	/// <inheritdoc/>
	public virtual Boolean Contains(JInterfaceTypeMetadata item) => this._internalSet.Contains(item);

	/// <inheritdoc/>
	public override String ToString()
	{
		if (String.IsNullOrEmpty(this._stringRepresentation))
			this._stringRepresentation = $"[{String.Join(", ", this.Enumerable.Select(i => i.ClassName))}]";
		return this._stringRepresentation;
	}
}