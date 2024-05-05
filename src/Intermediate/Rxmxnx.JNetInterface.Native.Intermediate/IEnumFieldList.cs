namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an object that represents a list java enum type fields instance.
/// </summary>
public interface IEnumFieldList
{
	/// <summary>
	/// Gets the number of elements in the collection.
	/// </summary>
	Int32 Count { get; }
	/// <summary>
	/// Retrieves the enum field name for given ordinal.
	/// </summary>
	/// <param name="ordinal">Enum ordinal.</param>
	CString this[Int32 ordinal] { get; }
	/// <summary>
	/// Retrieves the enum field ordinal for given name.
	/// </summary>
	/// <param name="name">Enum name.</param>
	Int32 this[ReadOnlySpan<Byte> name] { get; }

	/// <summary>
	/// Retrieves the enum field ordinal for given hash name.
	/// </summary>
	/// <param name="hash">Enum name hash.</param>
	internal Int32 this[String hash] { get; }

	/// <summary>
	/// Indicates whether <paramref name="ordinal"/> is defined in current list.
	/// </summary>
	/// <param name="ordinal">Enum ordinal.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="ordinal"/> is defined; otherwise, <see langword="false"/>
	/// </returns>
	internal Boolean HasOrdinal(Int32 ordinal);
	/// <summary>
	/// Indicates whether <paramref name="hash"/> is defined in current list.
	/// </summary>
	/// <param name="hash">Enum ordinal.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="hash"/> is defined; otherwise, <see langword="false"/>
	/// </returns>
	internal Boolean HasHash(String hash);
	/// <summary>
	/// Retrieves the missing ordinal set.
	/// </summary>
	/// <param name="count">Output. Number of defined elements.</param>
	/// <param name="maxOrdinal">Output. Maximum enum ordinal.</param>
	/// <returns>The missing ordinal set.</returns>
	internal IReadOnlySet<Int32> GetMissingFields(out Int32 count, out Int32 maxOrdinal);
}