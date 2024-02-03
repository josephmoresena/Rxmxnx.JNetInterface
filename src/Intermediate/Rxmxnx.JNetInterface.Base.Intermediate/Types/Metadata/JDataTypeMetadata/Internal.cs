namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial record JDataTypeMetadata
{
	/// <summary>
	/// Creates hash from given parameters.
	/// </summary>
	/// <param name="className">JNI name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	/// <returns>A <see cref="CStringSequence"/> containing JNI information.</returns>
	internal static CStringSequence CreateInformationSequence(CString className, CString? signature = default,
		CString? arraySignature = default)
	{
		CString[] arr = new CString[3];
		arr[0] = className;
		arr[1] = signature ?? JDataTypeMetadata.ComputeReferenceTypeSignature(className);
		arr[2] = arraySignature ?? JDataTypeMetadata.ComputeArraySignature(arr[1]);
		return new(arr);
	}
	/// <summary>
	/// Creates hash from given parameters.
	/// </summary>
	/// <param name="className">JNI name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <returns>A <see cref="CStringSequence"/> containing JNI information.</returns>
	internal static CStringSequence CreateInformationSequence(ReadOnlySpan<Byte> className,
		ReadOnlySpan<Byte> signature = default)
		=> NativeUtilities.WithSafeFixed(className, signature, JDataTypeMetadata.CreateInformationSequence);
	/// <summary>
	/// Retrieves escaped JNI class name.
	/// </summary>
	/// <param name="className">Java class name.</param>
	/// <returns>A <see cref="CString"/> containing JNI class name.</returns>
	internal static CString JniParseClassName(CString className)
	{
		CString classNameF = !className.Contains(JDataTypeMetadata.classNameEscape[0]) ?
			className :
			CString.Create(className.Select(JDataTypeMetadata.EscapeClassNameChar).ToArray());
		return classNameF;
	}
	/// <summary>
	/// Retrieves escaped JNI class name.
	/// </summary>
	/// <param name="className">Java class name.</param>
	/// <returns>A <see cref="CString"/> containing JNI class name.</returns>
	internal static CString JniParseClassName(ReadOnlySpan<Byte> className)
	{
		if (!className.Contains(JDataTypeMetadata.classNameEscape[0])) return CString.Create(className);
		Byte[] classNameBytes = new Byte[className.Length + 1];
		for (Int32 i = 0; i < className.Length; i++)
			classNameBytes[i] = JDataTypeMetadata.EscapeClassNameChar(className[i]);
		return classNameBytes;
	}
}