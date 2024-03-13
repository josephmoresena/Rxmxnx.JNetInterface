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

	/// <inheritdoc/>
	public virtual Boolean Contains(JInterfaceTypeMetadata item) => this._internalSet.Contains(item);
	/// <inheritdoc/>
	public virtual void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
	{
		foreach (JInterfaceTypeMetadata interfaceMetadata in this._internalSet)
			action(state, interfaceMetadata);
	}
	/// <inheritdoc/>
	public virtual IEnumerable<JInterfaceTypeMetadata> GetEnumerable() => this._internalSet;

	/// <inheritdoc/>
	public override String ToString()
	{
		if (String.IsNullOrEmpty(this._stringRepresentation))
			this._stringRepresentation = $"[{String.Join(", ", this.GetEnumerable().Select(i => i.ClassName))}]";
		return this._stringRepresentation;
	}

	private static void ForEach<T>(
		(T state, HashSet<String> hashes, Boolean recursive, Action<T, JInterfaceTypeMetadata> action) args,
		JInterfaceTypeMetadata interfaceMetadata)
	{
		if (!args.hashes.Add(interfaceMetadata.Hash)) return;
		args.action(args.state, interfaceMetadata);
		if (args.recursive)
			interfaceMetadata.Interfaces.ForEach(args, InterfaceSet.ForEach);
	}
}