namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitive"/> type.
/// </summary>
public sealed partial record JPrimitiveMetadata : JDataTypeMetadata
{
	/// <inheritdoc cref="JPrimitiveMetadata.ClassSignature"/>
	private readonly CString _classSignature;
	/// <inheritdoc cref="SizeOf"/>
	private readonly Int32 _sizeOf;

	/// <summary>
	/// JNI signature for current type wrapper class.
	/// </summary>
	public CString ClassSignature => this._classSignature;
	/// <summary>
	/// Size of current primitive type in bytes.
	/// </summary>
	public override Int32 SizeOf => this._sizeOf;

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Primitive;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="type">CLR type current primitive type.</param>
	/// <param name="sizeOf">Size of current primitive type in bytes.</param>
	/// <param name="signature">JNI signature for current primitive type.</param>
	/// <param name="className">Wrapper class name of current primitive type.</param>
	/// <param name="arraySignature">JNI signature for an array of current type.</param>
	/// <param name="classSignature">Wrapper class JNI signature of current primitive type.</param>
	internal JPrimitiveMetadata(Type type, Int32 sizeOf, CString signature, CString className, CString? arraySignature,
		CString? classSignature) : base(type, className, signature, arraySignature)
	{
		this._sizeOf = sizeOf;
		this._classSignature = JDataTypeMetadata.SafeNullTerminated(
			classSignature ?? JDataTypeMetadata.ComputeReferenceTypeSignature(className));
	}
}