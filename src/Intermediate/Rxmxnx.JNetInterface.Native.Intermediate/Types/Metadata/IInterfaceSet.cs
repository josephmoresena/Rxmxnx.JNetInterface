namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// Interface metadata set.
/// </summary>
public interface IInterfaceSet
{
	/// <summary>
	/// Internal enumeration.
	/// </summary>
	IEnumerable<JInterfaceTypeMetadata> Enumerable { get; }

	/// <summary>
	/// Determines if the set contains a specific item.
	/// </summary>
	/// <param name="item">The item to check if the set contains.</param>
	/// <returns>
	/// <see langword="true"/> if found; otherwise <see langword="false"/>.
	/// </returns>
	Boolean Contains(JInterfaceTypeMetadata item);
}