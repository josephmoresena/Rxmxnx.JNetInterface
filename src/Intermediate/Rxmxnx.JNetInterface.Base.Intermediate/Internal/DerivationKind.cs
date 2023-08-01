namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Kind of derivation.
/// </summary>
public enum DerivationKind
{
	/// <summary>
	/// Indicates there is no derivation.
	/// </summary>
	None = 0,
	/// <summary>
	/// Indicates the derivation is due to interface implementing.
	/// </summary>
	Implementation = 1,
	/// <summary>
	/// Indicates the derivation is due to type extending.
	/// </summary>
	Extension = 2,
}