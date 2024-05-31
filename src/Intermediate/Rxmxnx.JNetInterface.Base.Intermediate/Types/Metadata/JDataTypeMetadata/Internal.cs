namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial record JDataTypeMetadata
{
	/// <summary>
	/// Creates hash from given parameters.
	/// </summary>
	/// <param name="className">JNI name of the current type.</param>
	/// <param name="signature">JNI signature for the current type.</param>
	/// <returns>A <see cref="CStringSequence"/> containing JNI information.</returns>
	internal static CStringSequence CreateInformationSequence(ReadOnlySpan<Byte> className,
		ReadOnlySpan<Byte> signature = default)
		=> NativeUtilities.WithSafeFixed(className, signature, JDataTypeMetadata.CreateInformationSequence);
	/// <summary>
	/// Retrieves escaped JNI class name.
	/// </summary>
	/// <param name="className">Java class name.</param>
	/// <returns>A <see cref="CString"/> containing JNI class name.</returns>
	internal static CString JniEscapeClassName(CString className)
	{
		CString classNameF = !className.Contains(JDataTypeMetadata.GetEscapeSpan()[0]) ?
			className :
			CString.Create(className.Select(JDataTypeMetadata.EscapeClassNameChar).ToArray());
		return classNameF;
	}
	/// <summary>
	/// Retrieves escaped JNI class name.
	/// </summary>
	/// <param name="className">Java class name.</param>
	/// <returns>A <see cref="CString"/> containing JNI class name.</returns>
	internal static ReadOnlySpan<Byte> JniEscapeClassName(ReadOnlySpan<Byte> className)
	{
		if (!className.Contains(JDataTypeMetadata.GetEscapeSpan()[0])) return className;
		Byte[] classNameBytes = new Byte[className.Length];
		for (Int32 i = 0; i < className.Length; i++)
			classNameBytes[i] = JDataTypeMetadata.EscapeClassNameChar(className[i]);
		return classNameBytes;
	}
}