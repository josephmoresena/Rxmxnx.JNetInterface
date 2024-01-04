namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial record JDataTypeMetadata
{
	/// <summary>
	/// Escape for Java class name -> JNI class name.
	/// </summary>
	private static readonly CString classNameEscape = new(() => "./"u8);

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

	/// <summary>
	/// Creates hash from given parameters.
	/// </summary>
	/// <param name="memoryList">JNI parameter list of current type.</param>
	/// <returns>A <see cref="CStringSequence"/> containing JNI information.</returns>
	private static CStringSequence CreateInformationSequence(ReadOnlyFixedMemoryList memoryList)
	{
		Int32?[] lengths =
		[
			memoryList[0].Bytes.Length,
			memoryList[1].Bytes.Length > 0 ? memoryList[1].Bytes.Length : memoryList[0].Bytes.Length + 2,
			memoryList[1].Bytes.Length > 0 ? memoryList[1].Bytes.Length + 1 : memoryList[0].Bytes.Length + 3,
		];
		return CStringSequence.Create(memoryList.ToArray(), JDataTypeMetadata.CreateInformationSequence, lengths);
	}
	/// <summary>
	/// Creates a call sequence.
	/// </summary>
	/// <param name="span">A span of bytes.</param>
	/// <param name="index">Index of current sequence item.</param>
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
					span[0] = UnicodeObjectSignatures.ArraySignaturePrefixChar;
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
		span[0] = UnicodeObjectSignatures.ObjectSignaturePrefixChar;
		className.CopyTo(span[1..]);
		span[^1] = UnicodeObjectSignatures.ObjectSignatureSuffixChar;
	}
	/// <summary>
	/// Computes the array signature for given type signature.
	/// </summary>
	/// <param name="signature"><see cref="IDataType"/> signature.</param>
	/// <returns>Signature for given <see cref="IDataType"/> type.</returns>
	private static CString ComputeArraySignature(CString signature)
		=> CString.Concat(stackalloc Byte[1] { UnicodeObjectSignatures.ArraySignaturePrefixChar, }, signature);
	/// <summary>
	/// Escapes Java class name char to JNI class name.
	/// </summary>
	/// <param name="classNameChar">A Java class name char.</param>
	/// <returns>A JNI class name char.</returns>
	private static Byte EscapeClassNameChar(Byte classNameChar)
		=> classNameChar == JDataTypeMetadata.classNameEscape[0] ? JDataTypeMetadata.classNameEscape[1] : classNameChar;
}