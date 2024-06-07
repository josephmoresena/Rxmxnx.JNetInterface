namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Helper for class name retrieving.
/// </summary>
internal readonly struct ClassNameHelper
{
	/// <summary>
	/// Array class name.
	/// </summary>
	private readonly CString _classSignature;
	/// <summary>
	/// Offset
	/// </summary>
	private readonly Int32 _offset;
	/// <summary>
	/// Class name length.
	/// </summary>
	private readonly Int32 _padding;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="classSignature">Object class signature.</param>
	/// <param name="arrayDimension">Array dimension.</param>
	private ClassNameHelper(CString classSignature, Int32 arrayDimension)
	{
		this._classSignature = classSignature;
		this._offset = arrayDimension +
			(classSignature[arrayDimension] == CommonNames.ObjectSignaturePrefixChar ? 1 : 0);
		this._padding = classSignature[^1] == CommonNames.ObjectSignatureSuffixChar ? 1 : 0;
	}

	/// <inheritdoc/>
	public override String ToString()
	{
		Int32 elementNameLength = Encoding.UTF8.GetCharCount(this);
		return String.Create(elementNameLength, this, ClassNameHelper.WriteObjectElementName);
	}

	/// <summary>
	/// Retrieves class name from signature.
	/// </summary>
	/// <param name="classSignature">Class signature.</param>
	/// <param name="dimension">Array dimension.</param>
	/// <returns>Class name.</returns>
	public static String GetClassName(CString classSignature, Int32 dimension = 0)
	{
		switch (classSignature[dimension])
		{
			case CommonNames.BooleanSignatureChar:
				return CommonNames.BooleanPrimitive;
			case CommonNames.ByteSignatureChar:
				return CommonNames.BytePrimitive;
			case CommonNames.CharSignatureChar:
				return CommonNames.CharPrimitive;
			case CommonNames.DoubleSignatureChar:
				return CommonNames.DoublePrimitive;
			case CommonNames.FloatSignatureChar:
				return CommonNames.FloatPrimitive;
			case CommonNames.IntSignatureChar:
				return CommonNames.IntPrimitive;
			case CommonNames.LongSignatureChar:
				return CommonNames.LongPrimitive;
			case CommonNames.ShortSignatureChar:
				return CommonNames.ShortPrimitive;
			default:
				ClassNameHelper helper = new(classSignature, dimension);
				return helper.ToString();
		}
	}

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="ClassNameHelper"/> to
	/// <see cref="ReadOnlySpan{Byte}"/>.
	/// </summary>
	/// <param name="helper">A <see cref="ClassNameHelper"/> to implicitly convert.</param>
	public static implicit operator ReadOnlySpan<Byte>(ClassNameHelper helper)
		=> helper._classSignature.AsSpan()[helper._offset..^helper._padding];

	/// <summary>
	/// Wirtes in <paramref name="buffer"/> array object element name.
	/// </summary>
	/// <param name="buffer">UTF-16 buffer.</param>
	/// <param name="helper">Helper object.</param>
	private static void WriteObjectElementName(Span<Char> buffer, ClassNameHelper helper)
	{
		Encoding.UTF8.GetChars(helper, buffer); // Decodes UTF-8 chars.
		buffer.Replace('/', '.'); // Escapes chars.
	}
}