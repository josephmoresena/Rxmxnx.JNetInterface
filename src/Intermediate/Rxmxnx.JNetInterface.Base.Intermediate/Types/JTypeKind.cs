namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// Kinds of java datatype.
/// </summary>
public enum JTypeKind : Byte
{
	/// <summary>
	/// Indicates current type is a undefined.
	/// </summary>
	Undefined = 0,
	/// <summary>
	/// Indicates current type is a java primitive value.
	/// </summary>
	Primitive = 1,
	/// <summary>
	/// Indicates current type is a java class.
	/// </summary>
	Class = 2,
	/// <summary>
	/// Indicates current type is a java array.
	/// </summary>
	Array = 3,
	/// <summary>
	/// Indicates current type is a java interface.
	/// </summary>
	Interface = 4,
	/// <summary>
	/// Indicates current type is a java enumeration.
	/// </summary>
	Enum = 5,
	/// <summary>
	/// Indicates current type is a java annotation.
	/// </summary>
	Annotation = 6,
}