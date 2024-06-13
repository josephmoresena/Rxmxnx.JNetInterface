namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a <c>java.lang.Class&lt;?&gt;</c> JNI information container.
/// </summary>
public interface ITypeInformation
{
	/// <summary>
	/// JNI class name.
	/// </summary>
	CString ClassName { get; }
	/// <summary>
	/// JNI signature for object instances of this type.
	/// </summary>
	CString Signature { get; }
	/// <summary>
	/// Current datatype hash.
	/// </summary>
	String Hash { get; }
	/// <summary>
	/// Kind of the current type.
	/// </summary>
	JTypeKind Kind { get; }
	/// <summary>
	/// Modifier of the current type.
	/// </summary>
	JTypeModifier? Modifier { get; }

	/// <summary>
	/// Retrieves UTF-8 segment.
	/// </summary>
	/// <param name="source">Reference. UTF-8 sequence.</param>
	/// <returns>A UTF-8 segment.</returns>
	internal static ReadOnlySpan<Byte> GetSegment(ref ReadOnlySpan<Byte> source)
	{
		Int32 end = 0;
		while (end < source.Length)
		{
			if (source[end] == default) break; // Null UTF-8 char is segment end.
			end++;
		}
		// Creates segment slice.
		ReadOnlySpan<Byte> segment = source[..end];
		// Removes segment from source.
		source = source.Length > end + 1 ? source[(end + 1)..] : ReadOnlySpan<Byte>.Empty;
		return segment;
	}
	/// <summary>
	/// Retrieves printable text hash.
	/// </summary>
	/// <param name="hash">Class hash.</param>
	/// <param name="lastChar">Last char hash.</param>
	/// <returns>A read-only UTF-16 char span.</returns>
	[ExcludeFromCodeCoverage]
	internal static ReadOnlySpan<Char> GetPrintableHash(String hash, out String lastChar)
	{
		ReadOnlySpan<Char> hashSpan = hash;
		lastChar = hashSpan[^1] == default ? @"\0" : $"{hashSpan[^1]}";
		return hashSpan[..^1];
	}
}