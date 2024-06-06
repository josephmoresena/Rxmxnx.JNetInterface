namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Helper for object element array retrieving.
/// </summary>
internal readonly struct ObjectArrayElementHelper(CString arraySignature, Int32 arrayDimension)
{
	/// <summary>
	/// Array class name.
	/// </summary>
	private readonly CString _arraySignature = arraySignature;
	/// <summary>
	/// Array dimension.
	/// </summary>
	private readonly Int32 _arrayDimension = arrayDimension;

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="ObjectArrayElementHelper"/> to
	/// <see cref="ReadOnlySpan{Byte}"/>.
	/// </summary>
	/// <param name="state">A <see cref="ObjectArrayElementHelper"/> to implicitly convert.</param>
	public static implicit operator ReadOnlySpan<Byte>(ObjectArrayElementHelper state)
		=> state._arraySignature.AsSpan()[(state._arrayDimension + 1)..^1];

	/// <inheritdoc/>
	public override String ToString()
	{
		Int32 elementNameLength = Encoding.UTF8.GetCharCount(this);
		return String.Create(elementNameLength, this, ObjectArrayElementHelper.WriteObjectElementName);
	}

	/// <summary>
	/// Wirtes in <paramref name="buffer"/> array object element name.
	/// </summary>
	/// <param name="buffer">UTF-16 buffer.</param>
	/// <param name="state">State object.</param>
	private static void WriteObjectElementName(Span<Char> buffer, ObjectArrayElementHelper state)
	{
		Encoding.UTF8.GetChars(state, buffer); // Decodes UTF-8 chars.
		buffer.Replace('/', '.'); // Escapes chars.
	}
}