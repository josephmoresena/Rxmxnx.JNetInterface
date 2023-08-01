namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
public abstract record JDataTypeMetadata
{
	/// <inheritdoc cref="JDataTypeMetadata.ArraySignature"/>
	private readonly CString _arraySignature;
	/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
	private readonly CString _className;
	/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
	private readonly CString _signature;

	/// <summary>
	/// Class name.
	/// </summary>
	public CString ClassName => this._className;
	/// <summary>
	/// JNI signature.
	/// </summary>
	public CString Signature => this._signature;
	/// <summary>
	/// Array JNI signature of current type.
	/// </summary>
	public CString ArraySignature => this._arraySignature;

	/// <summary>
	/// CLR type of <see cref="IDataType"/>.
	/// </summary>
	public abstract Type Type { get; }
	/// <summary>
	/// Kind of current type.
	/// </summary>
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
	/// <param name="className">Class name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	/// <param name="arraySignature">Array JNI signature for current type.</param>
	internal JDataTypeMetadata(CString className, CString signature, CString? arraySignature = default)
	{
		this._className = JDataTypeMetadata.SafeNullTerminated(className);
		this._signature = JDataTypeMetadata.SafeNullTerminated(signature);
		this._arraySignature =
			JDataTypeMetadata.SafeNullTerminated(arraySignature ?? JDataTypeMetadata.ComputeArraySignature(signature));
	}

	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="jObject"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JObject"/> instance.</param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="jObject"/>.</returns>
	internal abstract IDataType? Create(JObject? jObject);

	/// <summary>
	/// Retrieve a safe null-terminated <see cref="CString"/> from <paramref name="cstr"/>.
	/// </summary>
	/// <param name="cstr">A UTF-8 string.</param>
	/// <returns>A null-terminated UTF-8 string.</returns>
	protected static CString SafeNullTerminated(CString cstr)
	{
		if (cstr.IsNullTerminated)
			return cstr;
		return (CString)cstr.Clone();
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
}