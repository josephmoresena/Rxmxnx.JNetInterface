namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// Kind of derivation.
/// </summary>
public enum JDerivationKind
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