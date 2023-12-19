namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
public abstract record JDataTypeMetadata : ITypeInformation
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
	/// Array signature for current type.
	/// </summary>
	public CString ArraySignature => this._arraySignature;

	/// <summary>
	/// Base types set.
	/// </summary>
	public virtual IReadOnlySet<Type> BaseTypes => ImmutableHashSet<Type>.Empty;

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
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	internal JDataTypeMetadata(CString className, CString signature, CString? arraySignature = default)
	{
		this._sequence = JDataTypeMetadata.CreateInformationSequence(className, signature, arraySignature);
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
		Byte[] classNameBytes = new Byte[className.Length];
		for (Int32 i = 0; i < className.Length; i++)
			classNameBytes[i] = JDataTypeMetadata.EscapeClassNameChar(className[i]);
		return classNameBytes;
	}

	/// <summary>
	/// Computes the type signature for given type class name.
	/// </summary>
	/// <param name="className"><see cref="IDataType"/> class name.</param>
	/// <returns>Signature for given <see cref="IDataType"/> type.</returns>
	protected static CString ComputeReferenceTypeSignature(CString className)
		=> CString.Concat(UnicodeObjectSignatures.ObjectSignaturePrefix, className,
		                  UnicodeObjectSignatures.ObjectSignatureSuffix);

	/// <summary>
	/// Computes the array signature for given type signature.
	/// </summary>
	/// <param name="signature"><see cref="IDataType"/> signature.</param>
	/// <returns>Signature for given <see cref="IDataType"/> type.</returns>
	private static CString ComputeArraySignature(CString signature)
		=> CString.Concat(UnicodeObjectSignatures.ArraySignaturePrefix, signature);
	/// <summary>
	/// Escapes Java class name char to JNI class name.
	/// </summary>
	/// <param name="classNameChar">A Java class name char.</param>
	/// <returns>A JNI class name char.</returns>
	private static Byte EscapeClassNameChar(Byte classNameChar)
		=> classNameChar == JDataTypeMetadata.classNameEscape[0] ? JDataTypeMetadata.classNameEscape[1] : classNameChar;
}