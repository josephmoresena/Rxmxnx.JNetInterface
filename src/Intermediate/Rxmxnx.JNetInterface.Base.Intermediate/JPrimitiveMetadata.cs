namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitiveType"/> type.
/// </summary>
public abstract record JPrimitiveMetadata : JDataTypeMetadata
{
	/// <summary>
	/// JNI signature for current type wrapper class.
	/// </summary>
	public abstract CString ClassSignature { get; }
	/// <summary>
	/// Underline primitive CLR type.
	/// </summary>
	public abstract Type UnderlineType { get; }

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Primitive;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="signature">JNI signature for current primitive type.</param>
	/// <param name="className">Wrapper class name of current primitive type.</param>
	/// <param name="arraySignature">JNI signature for an array of current type.</param>
	internal JPrimitiveMetadata(CString signature, CString className, CString? arraySignature) : base(
		className, signature, arraySignature) { }
}