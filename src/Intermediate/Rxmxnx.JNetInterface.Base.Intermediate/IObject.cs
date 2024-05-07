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
	/// Copy the sequence of bytes of the current instance to <paramref name="span"/>.
	/// </summary>
	/// <param name="span">Binary span.</param>
	/// <returns>Number of bytes copied.</returns>
	internal void CopyTo(Span<Byte> span)
	{
		Int32 index = 0;
		this.CopyTo(span, ref index);
	}
	/// <summary>
	/// Copy the sequence of bytes of the current instance to <paramref name="span"/> at specified
	/// <paramref name="offset"/>.
	/// </summary>
	/// <param name="span">Binary span.</param>
	/// <param name="offset">Offset in <paramref name="offset"/> to begin copy.</param>
	/// <returns>Number of bytes copied.</returns>
	internal void CopyTo(Span<Byte> span, ref Int32 offset);
	/// <summary>
	/// Copy the sequence of bytes of the current instance to <paramref name="span"/> at specified
	/// <paramref name="index"/>.
	/// </summary>
	/// <param name="span">Binary span.</param>
	/// <param name="index">Index to copy current value.</param>
	internal void CopyTo(Span<JValue> span, Int32 index);
}