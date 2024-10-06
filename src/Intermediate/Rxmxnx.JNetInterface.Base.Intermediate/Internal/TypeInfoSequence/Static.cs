namespace Rxmxnx.JNetInterface.Internal;

internal partial class TypeInfoSequence
{
	/// <summary>
	/// Creates hash from <paramref name="className"/>.
	/// </summary>
	/// <param name="className">Reference type class name.</param>
	/// <param name="escape">Indicates whether <paramref name="className"/> sould be escaped.</param>
	/// <param name="isArray">Output. Indicates whether <paramref name="className"/> is for array class.</param>
	/// <returns>Type hash.</returns>
	private static unsafe String CreateHash(ReadOnlySpan<Byte> className, Boolean escape, out Boolean isArray)
	{
		// To create TypeInfoSequence instance we use JNI class name.
		ReadOnlySpan<Byte> jniClassName = escape ?
			className :
			TypeInfoSequence.JniEscapeClassName(stackalloc Byte[className.Length], className);
		// Buffer length should hold at least 3 times the class name, 3 null-characters and 1 array prefix char.
		Int32 bufferLength = 3 * className.Length + 4;
		isArray = className[0] == CommonNames.ArraySignaturePrefixChar;
		// If class is not an array, signature and class name are different, so we need hold signature
		// prefix and suffix 2 times.
		if (!isArray) bufferLength += 4;
		fixed (Byte* char0 = &MemoryMarshal.GetReference(jniClassName))
		{
			SpanState state = new(char0, jniClassName.Length, isArray);
			Int32 stringLength = bufferLength / sizeof(Char) + bufferLength % sizeof(Char);
			return String.Create(stringLength, state, TypeInfoSequence.CreateInfoSequence);
		}
	}
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