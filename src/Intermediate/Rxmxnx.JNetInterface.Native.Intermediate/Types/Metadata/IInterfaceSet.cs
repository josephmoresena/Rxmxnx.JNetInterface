namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// Interface metadata set.
/// </summary>
public interface IInterfaceSet : IEnumerable<JInterfaceTypeMetadata>
{
	/// <summary>
	/// Determines if the set contains a specific item.
	/// </summary>
	/// <param name="item">The item to check if the set contains.</param>
	/// <returns>
	/// <see langword="true"/> if found; otherwise <see langword="false"/>.
	/// </returns>
	Boolean Contains(JInterfaceTypeMetadata item);
	/// <summary>
	/// Performs <paramref name="action"/> for each unique item in current set.
	/// </summary>
	/// <typeparam name="T">Type of state object</typeparam>
	/// <param name="state">Object state.</param>
	/// <param name="action">A <see cref="Action{T, JInterfaceTypeMetadata}"/> delegate.</param>
	void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action);
}