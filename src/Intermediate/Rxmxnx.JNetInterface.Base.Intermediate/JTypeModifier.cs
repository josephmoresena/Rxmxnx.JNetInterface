namespace Rxmxnx.JNetInterface;

/// <summary>
/// Modifiers of java datatype.
/// </summary>
public enum JTypeModifier : Byte
{
	/// <summary>
	/// Indicates current type can be inherit by other types.
	/// </summary>
	Extensible = 0,
	/// <summary>
	/// Indicates current type is not inheritable.
	/// </summary>
	Final = 1,
	/// <summary>
	/// Indicates current type is abstract.
	/// </summary>
	Abstract = 2,
}