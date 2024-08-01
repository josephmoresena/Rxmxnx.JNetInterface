namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial class JDataTypeMetadata
{
	/// <inheritdoc cref="JDataTypeMetadata.ArraySignature"/>
	private readonly CString _arraySignature;
	/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
	private readonly CString _className;
	/// <summary>
	/// Internal sequence information.
	/// </summary>
	private readonly CStringSequence _sequence;
	/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
	private readonly CString _signature;

	Boolean? ITypeInformation.IsFinal => this.Modifier is JTypeModifier.Final;

	/// <summary>
	/// Creates hash from given parameters.
	/// </summary>
	/// <param name="memoryList">JNI parameter list of the current type.</param>
	/// <returns>A <see cref="CStringSequence"/> containing JNI information.</returns>
	private static CStringSequence CreateInformationSequence(ReadOnlyFixedMemoryList memoryList)
	{
		Boolean isArray = memoryList[0].Bytes[0] == CommonNames.ArraySignaturePrefixChar;
		Int32 signatureAdditionalChars = isArray ? 0 : 2;
		Int32 signatureLength = memoryList[1].Bytes.Length > 0 ?
			memoryList[1].Bytes.Length :
			memoryList[0].Bytes.Length + signatureAdditionalChars;
		Int32 arraySignatureLength = signatureLength + 1;
		Int32?[] lengths =
		[
			memoryList[0].Bytes.Length,
			signatureLength,
			arraySignatureLength,
		];
		return CStringSequence.Create(memoryList.ToArray(), JDataTypeMetadata.CreateInformationSequence, lengths);
	}
	/// <summary>
	/// Creates a call sequence.
	/// </summary>
	/// <param name="span">A span of bytes.</param>
	/// <param name="index">Index of the current sequence item.</param>
	/// <param name="arg">Creation instance.</param>
	private static void CreateInformationSequence(Span<Byte> span, Int32 index, IReadOnlyFixedMemory[] arg)
	{
		if (index < arg.Length && arg[index].Bytes.Length != 0)
			arg[index].Bytes.CopyTo(span);
		else
			switch (index)
			{
				case 1:
					JDataTypeMetadata.WriteSignature(span, arg[0].Bytes);
					break;
				case 2:
				{
					span[0] = CommonNames.ArraySignaturePrefixChar;
					if (arg[1].Bytes.Length > 0)
						arg[1].Bytes.CopyTo(span[1..]);
					else
						JDataTypeMetadata.WriteSignature(span[1..], arg[0].Bytes);
					break;
				}
			}
	}
	/// <summary>
	/// Writes type signature from <paramref name="className"/> instance.
	/// </summary>
	/// <param name="span">Destination span.</param>
	/// <param name="className">JNI class name.</param>
	private static void WriteSignature(Span<Byte> span, ReadOnlySpan<Byte> className)
	{
		if (className[0] == CommonNames.ArraySignaturePrefixChar)
		{
			className.CopyTo(span);
		}
		else
		{
			span[0] = CommonNames.ObjectSignaturePrefixChar;
			className.CopyTo(span[1..]);
			span[^1] = CommonNames.ObjectSignatureSuffixChar;
		}
	}
	/// <summary>
	/// Escapes Java class name char to JNI class name.
	/// </summary>
	/// <param name="classNameChar">A Java class name char.</param>
	/// <returns>A JNI class name char.</returns>
	private static Byte EscapeClassNameChar(Byte classNameChar)
	{
		ReadOnlySpan<Byte> escapeSpan = JDataTypeMetadata.GetEscapeSpan();
		return classNameChar == escapeSpan[0] ? escapeSpan[1] : classNameChar;
	}
	/// <summary>
	/// Escape for Java class name -> JNI class name.
	/// </summary>
	/// <returns>A read-only binary span.</returns>
	private static ReadOnlySpan<Byte> GetEscapeSpan() => "./"u8;
}