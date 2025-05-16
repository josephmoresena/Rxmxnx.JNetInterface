namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a <c>java.lang.Object</c> instance.
/// </summary>
public interface IObject
{
	/// <summary>
	/// Class name of the current instance.
	/// </summary>
	CString ObjectClassName { get; }
	/// <summary>
	/// Class signature of the current instance.
	/// </summary>
	CString ObjectSignature { get; }

	/// <summary>
	/// Copies the sequence of bytes of the current instance to <paramref name="span"/>.
	/// </summary>
	/// <param name="span">Binary span.</param>
	/// <returns>Number of bytes copied.</returns>
	internal sealed void CopyTo(Span<Byte> span)
	{
		Int32 index = 0;
		this.CopyTo(span, ref index);
	}
	/// <summary>
	/// Copies the sequence of bytes of the current instance to <paramref name="span"/> at specified
	/// <paramref name="offset"/>.
	/// </summary>
	/// <param name="span">Binary span.</param>
	/// <param name="offset">Offset in <paramref name="offset"/> to begin copy.</param>
	/// <returns>Number of bytes copied.</returns>
	internal void CopyTo(Span<Byte> span, ref Int32 offset);
	/// <summary>
	/// Copies the sequence of bytes of the current instance to <paramref name="span"/> at specified
	/// <paramref name="index"/>.
	/// </summary>
	/// <param name="span">Binary span.</param>
	/// <param name="index">Index to copy current value.</param>
	internal void CopyTo(Span<JValue> span, Int32 index);
	/// <summary>
	/// Indicates current instance is default value.
	/// </summary>
	/// <returns><see langword="true"/> if current instance is default; otherwise, <see langword="false"/>.</returns>
	internal sealed Boolean IsDefault()
	{
		Span<Byte> values = stackalloc Byte[JValue.Size];
		this.CopyTo(values);
		return values.SequenceEqual(NativeUtilities.AsBytes(in JValue.Empty));
	}
}