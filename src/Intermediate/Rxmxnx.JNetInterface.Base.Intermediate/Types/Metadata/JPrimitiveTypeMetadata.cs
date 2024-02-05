namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitiveType"/> type.
/// </summary>
public abstract partial record JPrimitiveTypeMetadata : JDataTypeMetadata
{
	/// <summary>
	/// <see cref="JPrimitiveTypeMetadata"/> instance for Java <c>void</c> type.
	/// </summary>
	public static readonly JPrimitiveTypeMetadata VoidMetadata = new JVoidTypeMetadata();

	/// <summary>
	/// Fake class hash for primitive void.
	/// </summary>
	internal static String FakeVoidHash => JVoidTypeMetadata.FakeHash;

	/// <inheritdoc cref="SizeOf"/>
	private readonly Int32 _sizeOf;
	/// <summary>
	/// CLR underline type.
	/// </summary>
	private readonly Type _underlineType;
	/// <summary>
	/// Wrapper class information.
	/// </summary>
	private readonly CStringSequence _wrapperInformation;

	/// <summary>
	/// JNI name for current type wrapper class.
	/// </summary>
	public CString WrapperClassSignature => this._wrapperInformation[1];
	/// <summary>
	/// JNI signature for current type wrapper class.
	/// </summary>
	public CString WrapperClassName => this._wrapperInformation[0];
	/// <summary>
	/// Underline primitive CLR type.
	/// </summary>
	public Type UnderlineType => this._underlineType;
	/// <summary>
	/// Native primitive type.
	/// </summary>
	public abstract JNativeType NativeType { get; }
	/// <summary>
	/// Size of current primitive type in bytes.
	/// </summary>
	public override Int32 SizeOf => this._sizeOf;

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Primitive;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;

	/// <summary>
	/// Information of wrapper class.
	/// </summary>
	internal CStringSequence WrapperInformation => this._wrapperInformation;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="sizeOf">Size of current primitive type in bytes.</param>
	/// <param name="underlineType">Underline primitive CLR type.</param>
	/// <param name="signature">JNI signature for current primitive type.</param>
	/// <param name="className">Wrapper class name of current primitive type.</param>
	/// <param name="wrapperClassName">Wrapper class JNI name of current primitive type.</param>
	private protected JPrimitiveTypeMetadata(Int32 sizeOf, Type underlineType, ReadOnlySpan<Byte> signature,
		ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> wrapperClassName) : base(className, signature)
	{
		this._sizeOf = sizeOf;
		this._underlineType = underlineType;
		this._wrapperInformation = JDataTypeMetadata.CreateInformationSequence(wrapperClassName);
	}

	/// <summary>
	/// Creates a <see cref="IPrimitiveType"/> value from <paramref name="bytes"/>.
	/// </summary>
	/// <param name="bytes">Binary read-only span.</param>
	/// <returns>A <see cref="IPrimitiveType"/> value from <paramref name="bytes"/>.</returns>
	public abstract IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes);

	/// <inheritdoc/>
	public override String ToString()
		=> base.ToString() + $"{nameof(JPrimitiveTypeMetadata.UnderlineType)} = {this.UnderlineType}, " +
			$"{nameof(JPrimitiveTypeMetadata.NativeType)} = {this.NativeType}, " +
			$"{nameof(JPrimitiveTypeMetadata.WrapperClassName)} = {this.WrapperClassName}, ";
}