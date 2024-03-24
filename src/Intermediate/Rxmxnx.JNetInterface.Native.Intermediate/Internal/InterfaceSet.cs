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

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="set">A <see cref="IReadOnlySet{T}"/> instance.</param>
	private InterfaceSet(ImmutableHashSet<JInterfaceTypeMetadata> set) => this._internalSet = set;

	[ExcludeFromCodeCoverage]
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

	/// <inheritdoc/>
	public IEnumerator<JInterfaceTypeMetadata> GetEnumerator() => this.GetEnumerable().GetEnumerator();
	/// <inheritdoc/>
	public virtual Boolean Contains(JInterfaceTypeMetadata item) => this._internalSet.Contains(item);
	/// <inheritdoc/>
	public virtual void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
	{
		HashSet<String> hashes = InterfaceSet.OpenSetOperation(this, out Boolean isNew, out Boolean isRecursive);
		try
		{
			foreach (JInterfaceTypeMetadata interfaceMetadata in this._internalSet)
			{
				if (!hashes.Add(interfaceMetadata.Hash)) continue;
				action(state, interfaceMetadata);
				if (isRecursive) interfaceMetadata.Interfaces.ForEach(state, action);
			}
		}
		finally
		{
			InterfaceSet.CloseSetOperation(isNew);
		}
	}
	/// <inheritdoc/>
	public override String ToString()
	{
		if (String.IsNullOrEmpty(this._stringRepresentation))
			this._stringRepresentation = $"[{String.Join(", ", this.GetEnumerable().Select(i => i.ClassName))}]";
		return this._stringRepresentation;
	}

	/// <summary>
	/// Internal enumeration.
	/// </summary>
	private protected virtual IEnumerable<JInterfaceTypeMetadata> GetEnumerable() => this._internalSet;
}