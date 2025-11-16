namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Helper for class name retrieving.
/// </summary>
internal readonly partial struct ClassNameHelper
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
		this._offset = ClassNameHelper.GetOffset(classSignature, arrayDimension, out Boolean isObjectClass);
		this._padding = ClassNameHelper.GetPadding(classSignature, isObjectClass);
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
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static String GetClassName(CString classSignature, Int32 dimension = 0)
	{
		switch (classSignature[dimension])
		{
			case CommonNames.VoidSignatureChar:
				return CommonNames.VoidPrimitive;
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
	/// Retrieves class name from primitive signature.
	/// </summary>
	/// <param name="primitiveSignature">JNI primitive signature.</param>
	/// <returns>Class name.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static String GetPrimitiveClassName(Byte primitiveSignature)
	{
		return primitiveSignature switch
		{
			CommonNames.VoidSignatureChar => CommonNames.VoidPrimitive,
			CommonNames.BooleanSignatureChar => CommonNames.BooleanPrimitive,
			CommonNames.ByteSignatureChar => CommonNames.BytePrimitive,
			CommonNames.CharSignatureChar => CommonNames.CharPrimitive,
			CommonNames.DoubleSignatureChar => CommonNames.DoublePrimitive,
			CommonNames.FloatSignatureChar => CommonNames.FloatPrimitive,
			CommonNames.IntSignatureChar => CommonNames.IntPrimitive,
			CommonNames.LongSignatureChar => CommonNames.LongPrimitive,
			CommonNames.ShortSignatureChar => CommonNames.ShortPrimitive,
			CommonNames.ObjectSignaturePrefixChar => nameof(Object),
			_ => String.Empty,
		};
	}

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="ClassNameHelper"/> to
	/// <see cref="ReadOnlySpan{Byte}"/>.
	/// </summary>
	/// <param name="helper">A <see cref="ClassNameHelper"/> to implicitly convert.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator ReadOnlySpan<Byte>(ClassNameHelper helper)
		=> helper._classSignature.AsSpan()[helper._offset..^helper._padding];

	/// <summary>
	/// Wirtes in <paramref name="buffer"/> array object element name.
	/// </summary>
	/// <param name="buffer">UTF-16 buffer.</param>
	/// <param name="helper">Helper object.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void WriteObjectElementName(Span<Char> buffer, ClassNameHelper helper)
	{
		Encoding.UTF8.GetChars(helper, buffer); // Decodes UTF-8 chars.
#if !NET8_0_OR_GREATER
		ClassNameHelper.Replace(buffer, '/', '.'); // Escapes chars.
#else
		buffer.Replace('/', '.'); // Escapes chars.
#endif
	}
	/// <summary>
	/// Retrieves offset for current class name.
	/// </summary>
	/// <param name="classSignature">Class signature.</param>
	/// <param name="arrayDimension">Array dimension.</param>
	/// <param name="isObjectClass">Output. Indicates whether current class is a object class.</param>
	/// <returns>Offset of current class name.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Int32 GetOffset(CString classSignature, Int32 arrayDimension, out Boolean isObjectClass)
	{
		isObjectClass = classSignature[arrayDimension] == CommonNames.ObjectSignaturePrefixChar;
		return arrayDimension + (isObjectClass ? 1 : 0);
	}
	/// <summary>
	/// Retrieves padding for current class name.
	/// </summary>
	/// <param name="classSignature">Class signature.</param>
	/// <param name="isObjectClass">Indicates whether current class is a object class.</param>
	/// <returns>Padding of current class name.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Int32 GetPadding(CString classSignature, Boolean isObjectClass)
		=> classSignature[^1] == CommonNames.ObjectSignatureSuffixChar && isObjectClass ? 1 : 0;

	/// <summary>
	/// Replaces all occurrences of <paramref name="oldValue"/> with <paramref name="newValue"/>.
	/// </summary>
	/// <param name="span">The span in which the chars should be replaced.</param>
	/// <param name="oldValue">The char to be replaced with newValue.</param>
	/// <param name="newValue">The char to replace all occurrences of oldValue.</param>
	public static void Replace(Span<Char> span, Char oldValue, Char newValue)
#if NET8_0_OR_GREATER
		=> span.Replace(oldValue, newValue);
#else
	{
		ref Char src = ref MemoryMarshal.GetReference(span);
		for (Int32 idx = 0; idx < span.Length; ++idx)
		{
			Char original = Unsafe.Add(ref src, idx);
			Unsafe.Add(ref src, idx) = oldValue.Equals(original) ? newValue : original;
		}
	}
#endif
}