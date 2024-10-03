namespace Rxmxnx.JNetInterface.Internal;

internal partial class TypeInfoSequence
{
	/// <summary>
	/// Retrieves escaped JNI class name.
	/// </summary>
	/// <param name="jniClassName">Span to hold JNI class name.</param>
	/// <param name="className">Java class name.</param>
	/// <returns>
	///     <paramref name="jniClassName"/>
	/// </returns>
	private static ReadOnlySpan<Byte> JniEscapeClassName(Span<Byte> jniClassName, ReadOnlySpan<Byte> className)
	{
		for (Int32 i = 0; i < className.Length; i++)
			jniClassName[i] = TypeInfoSequence.EscapeClassNameChar(className[i]);
		return jniClassName;
	}
	/// <summary>
	/// Escapes Java class name char to JNI class name.
	/// </summary>
	/// <param name="classNameChar">A Java class name char.</param>
	/// <returns>A JNI class name char.</returns>
	private static Byte EscapeClassNameChar(Byte classNameChar)
	{
		ReadOnlySpan<Byte> escapeSpan = TypeInfoSequence.GetEscapeSpan();
		return classNameChar == escapeSpan[0] ? escapeSpan[1] : classNameChar;
	}
	/// <summary>
	/// Escape for Java class name -> JNI class name.
	/// </summary>
	/// <returns>A read-only binary span.</returns>
	private static ReadOnlySpan<Byte> GetEscapeSpan() => "./"u8;
	/// <summary>
	/// Creates type information sequence in <paramref name="span"/> from <paramref name="arg"/>
	/// </summary>
	/// <param name="span">UTF-16 buffer.</param>
	/// <param name="arg">JNI class name state.</param>
	private static void CreateInfoSequence(Span<Char> span, SpanState arg)
	{
		// Initial buffer.
		Span<Byte> buffer = span.AsBytes();
		// JNI class name.
		ReadOnlySpan<Byte> jniSegment = arg.GetSpan();
		jniSegment.CopyTo(buffer);
		// Buffer after JNI class name + null-character
		buffer = buffer[(jniSegment.Length + 1)..];
		if (!arg.IsArray)
		{
			// Open signature with prefix,
			buffer[0] = CommonNames.ObjectSignaturePrefixChar;
			// Write JNI class name.
			jniSegment.CopyTo(buffer[1..]);
			// Close signature with suffix.
			buffer[jniSegment.Length + 1] = CommonNames.ObjectSignatureSuffixChar;
		}
		else
		{
			jniSegment.CopyTo(buffer);
		}
		// JNI signature
		jniSegment = buffer[..(jniSegment.Length + (arg.IsArray ? 0 : 2))];
		// Buffer after JNI signature + null-character
		buffer = buffer[(jniSegment.Length + 1)..];
		// Open array signature with array signature prefix.
		buffer[0] = CommonNames.ArraySignaturePrefixChar;
		// Writes JNI signature.
		jniSegment.CopyTo(buffer[1..]);
	}
}