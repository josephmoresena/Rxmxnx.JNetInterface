namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
public abstract partial record JDataTypeMetadata : ITypeInformation
{
	/// <summary>
	/// Array signature for current type.
	/// </summary>
	public CString ArraySignature => this._arraySignature;

	/// <summary>
	/// CLR type of <see cref="IDataType"/>.
	/// </summary>
	public abstract Type Type { get; }
	/// <inheritdoc cref="JDataTypeMetadata.Kind"/>
	public abstract JTypeKind Kind { get; }
	/// <summary>
	/// Modifier of current type.
	/// </summary>
	public abstract JTypeModifier Modifier { get; }
	/// <summary>
	/// Size of current type in bytes.
	/// </summary>
	public abstract Int32 SizeOf { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">JNI name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	internal JDataTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature = default)
	{
		this._sequence = JDataTypeMetadata.CreateInformationSequence(className, signature);
		this._className = this._sequence[0];
		this._signature = this._sequence[1];
		this._arraySignature = this._sequence[2];
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="information">Internal sequence information.</param>
	internal JDataTypeMetadata(CStringSequence information)
	{
		this._sequence = information;
		this._className = this._sequence[0];
		this._signature = this._sequence[1];
		this._arraySignature = this._sequence[2];
	}

	/// <inheritdoc/>
	public CString ClassName => this._className;
	/// <inheritdoc/>
	public CString Signature => this._signature;
	/// <inheritdoc/>
	public String Hash => this._sequence.ToString();

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

	/// <summary>
	/// Computes the type signature for given type class name.
	/// </summary>
	/// <param name="className"><see cref="IDataType"/> class name.</param>
	/// <returns>Signature for given <see cref="IDataType"/> type.</returns>
	protected static CString ComputeReferenceTypeSignature(ReadOnlySpan<Byte> className)
		=> CString.Concat(stackalloc Byte[1] { UnicodeObjectSignatures.ObjectSignaturePrefixChar, }, className,
		                  stackalloc Byte[1] { UnicodeObjectSignatures.ObjectSignatureSuffixChar, });
}