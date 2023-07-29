namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
public abstract record JReferenceMetadata : JDataTypeMetadata
{
	/// <summary>
	/// Metadata of base type.
	/// </summary>
	private readonly JReferenceMetadata? _baseMetadata;
	
	/// <summary>
	/// Last <see cref="IDataType"/> signature.
	/// </summary>
	private CString? _lastSignature;
	/// <summary>
	/// Delegate to retrieve JNI signature for an array of current type.
	/// </summary>
	private CString _arraySignature;

	/// <summary>
	/// Metadata of base type.
	/// </summary>
	public JReferenceMetadata? BaseMetadata => this._baseMetadata;

	/// <inheritdoc/>
	public override CString Signature
	{
		get
		{
			if (this._lastSignature is not null && this._lastSignature != this.Signature)
				this._arraySignature =	this.ComputeArraySignature();
			return this._arraySignature;
		}
	}
	/// <inheritdoc/>
	public override CString ArraySignature => this._arraySignature;
	/// <inheritdoc/>
	public override Int32 SizeOf => NativeUtilities.PointerSize;
	
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="type">CLR type of <see cref="IDataType"/>.</param>
	/// <param name="arraySignature">Delegate to retrieve JNI signature for an array of current type.</param>
	/// <param name="baseMetadata">Size of current primitive type in bytes.</param>
	internal JReferenceMetadata(Type type, CString arraySignature, JReferenceMetadata? baseMetadata) 
		: base(type)
	{
		this._arraySignature = arraySignature;
		this._baseMetadata = baseMetadata;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="type">CLR type of <see cref="IDataType"/>.</param>
	/// <param name="baseMetadata">Size of current primitive type in bytes.</param>
	internal JReferenceMetadata(Type type, JReferenceMetadata? baseMetadata) 
		: base(type)
	{
		this._baseMetadata = baseMetadata;
		this._arraySignature = this.ComputeArraySignature();
	}

	private static CString GetSignature(JDataTypeMetadata metadata) => metadata.Signature;
	private CString ComputeArraySignature()
	{
		this._lastSignature = this.Signature;
		return CString.Concat(UnicodeObjectSignatures.ArraySignaturePrefix, this._lastSignature);
	}
}