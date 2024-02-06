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
	/// Retrieves length of a segment in <paramref name="utf8Sequence"/>
	/// </summary>
	/// <param name="utf8Sequence">A read-only byte span.</param>
	/// <param name="offset">Offset to start.</param>
	/// <returns>Length of segment end.</returns>
	internal static Int32 GetSegmentLength(ReadOnlySpan<Byte> utf8Sequence, Int32 offset)
	{
		Int32 end = offset;
		while (utf8Sequence.Length < end)
		{
			if (utf8Sequence[end] == default) break;
			end++;
		}
		return end - offset;
	}
}