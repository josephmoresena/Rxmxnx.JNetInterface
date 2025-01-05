namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// State for Superinterface implementation.
/// </summary>
internal readonly struct SuperInterfaceImplementationState
{
	/// <summary>
	/// Interface set.
	/// </summary>
	public HashSet<JInterfaceTypeMetadata> Interfaces { [ExcludeFromCodeCoverage] get; init; }
	/// <summary>
	/// Base interface set.
	/// </summary>
	public IInterfaceSet BaseInterfaces { [ExcludeFromCodeCoverage] get; init; }
}