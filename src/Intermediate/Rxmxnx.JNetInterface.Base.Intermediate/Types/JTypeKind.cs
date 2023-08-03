namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// Kinds of java datatype.
/// </summary>
public enum JTypeKind : Byte
{
	/// <summary>
	/// Indicates current type is a java primitive value.
	/// </summary>
	Primitive = 0,
	/// <summary>
	/// Indicates current type is a java class.
	/// </summary>
	Class = 1,
	/// <summary>
	/// Indicates current type is a java array.
	/// </summary>
	Array = 2,
	/// <summary>
	/// Indicates current type is a java interface.
	/// </summary>
	Interface = 3,
	/// <summary>
	/// Indicates current type is a java enumeration.
	/// </summary>
	Enum = 4,
	/// <summary>
	/// Indicates current type is a java annotation.
	/// </summary>
	Annotation = 5,
}