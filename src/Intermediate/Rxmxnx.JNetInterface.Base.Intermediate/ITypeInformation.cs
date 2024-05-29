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
	/// Retrieve fixed pointer of class name.
	/// </summary>
	IFixedPointer.IDisposable GetClassNameFixedPointer();

	/// <summary>
	/// Retrieves length of a segment in <paramref name="utf8Sequence"/>
	/// </summary>
	/// <param name="utf8Sequence">A read-only byte span.</param>
	/// <param name="offset">Offset to start.</param>
	/// <returns>Length of segment end.</returns>
	internal static Int32 GetSegmentLength(ReadOnlySpan<Byte> utf8Sequence, Int32 offset)
	{
		Int32 end = offset;
		while (utf8Sequence.Length > end)
		{
			if (utf8Sequence[end] == default) break;
			end++;
		}
		return end - offset;
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