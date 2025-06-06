namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// State for Superinterface validation.
/// </summary>
internal readonly struct SuperInterfaceValidationState
{
	/// <summary>
	/// Current interface types.
	/// </summary>
	public IReadOnlySet<Type> Interfaces
	{
#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		get;
		init;
	}
	/// <summary>
	/// Name of excluded interfaces.
	/// </summary>
	public HashSet<String> NotContained { get; init; }
}