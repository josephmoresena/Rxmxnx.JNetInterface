namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Enum for <c>java.lang.reflect.Modifier</c> constants.
/// </summary>
[Flags]
public enum JModifier
{
	/// <summary>
	/// <c>public</c> modifier.
	/// </summary>
	Public = 0x1,
	/// <summary>
	/// <c>private</c> modifier.
	/// </summary>
	Private = 0x2,
	/// <summary>
	/// <c>protected</c> modifier.
	/// </summary>
	Protected = 0x4,
	/// <summary>
	/// <c>static</c> modifier.
	/// </summary>
	Static = 0x8,
	/// <summary>
	/// <c>final</c> modifier.
	/// </summary>
	Final = 0x10,
	/// <summary>
	/// <c>synchronized</c> modifier.
	/// </summary>
	Synchronized = 0x20,
	/// <summary>
	/// <c>volatile</c> modifier.
	/// </summary>
	Volatile = 0x40,
	/// <summary>
	/// <c>transient</c> modifier.
	/// </summary>
	Transient = 0x80,
	/// <summary>
	/// <c>native</c> modifier.
	/// </summary>
	Native = 0x100,
	/// <summary>
	/// <c>interface</c> modifier.
	/// </summary>
	Interface = 0x200,
	/// <summary>
	/// <c>abstract</c> modifier.
	/// </summary>
	Abstract = 0x400,
	/// <summary>
	/// <c>strictfp</c> modifier.
	/// </summary>
	Strict = 0x800,
}