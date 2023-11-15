namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitiveType"/> type.
/// </summary>
public abstract record JPrimitiveTypeMetadata : JDataTypeMetadata
{
	/// <summary>
	/// JNI signature for current type wrapper class.
	/// </summary>
	public abstract CString ClassSignature { get; }
	/// <summary>
	/// Underline primitive CLR type.
	/// </summary>
	public abstract Type UnderlineType { get; }
	/// <summary>
	/// Native primitive type.
	/// </summary>
	public abstract JNativeType NativeType { get; }

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
	internal JPrimitiveTypeMetadata(CString signature, CString className, CString? arraySignature) : base(
		className, signature, arraySignature) { }

	/// <summary>
	/// Creates a <see cref="IPrimitiveType"/> value from <paramref name="bytes"/>.
	/// </summary>
	/// <param name="bytes">Binary read-only span.</param>
	/// <returns>A <see cref="IPrimitiveType"/> value from <paramref name="bytes"/>.</returns>
	public abstract IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes);
}