namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// Modifiers of java datatype.
/// </summary>
public enum JTypeModifier : Byte
{
	/// <summary>
	/// Indicates the current type can be inherited by other types.
	/// </summary>
	Extensible = 0,
	/// <summary>
	/// Indicates the current type is not inheritable.
	/// </summary>
	Final = 1,
	/// <summary>
	/// Indicates the current type is abstract.
	/// </summary>
	Abstract = 2,
}