namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// State for Superinterface validation.
/// </summary>
internal readonly struct SuperInterfaceValidationState
{
	/// <summary>
	/// Current interface types.
	/// </summary>
	public IReadOnlySet<Type> Interfaces { get; init; }
	/// <summary>
	/// Name of excluded interfaces.
	/// </summary>
	public HashSet<CString> NotContained { get; init; }
}