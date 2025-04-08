namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// State for Superinterface implementation.
/// </summary>
internal readonly struct SuperInterfaceImplementationState
{
	/// <summary>
	/// Interface set.
	/// </summary>
	public HashSet<JInterfaceTypeMetadata> Interfaces
	{
#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		get;
		init;
	}
	/// <summary>
	/// Base interface set.
	/// </summary>
	public IInterfaceSet BaseInterfaces
	{
#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		get;
		init;
	}
}