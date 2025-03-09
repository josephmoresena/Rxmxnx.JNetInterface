namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Interface record set.
/// </summary>
internal partial class InterfaceSet : IAppendableInterfaceSet
{
	/// <summary>
	/// Internal set.
	/// </summary>
	private readonly ImmutableHashSet<JInterfaceTypeMetadata> _internalSet;

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
#if NET9_0_OR_GREATER
		where T : allows ref struct
#endif
	{
		foreach (JInterfaceTypeMetadata interfaceMetadata in this._internalSet)
			action(state, interfaceMetadata);
	}

	/// <summary>
	/// Internal enumeration.
	/// </summary>
	private protected virtual IEnumerable<JInterfaceTypeMetadata> GetEnumerable() => this._internalSet;
}