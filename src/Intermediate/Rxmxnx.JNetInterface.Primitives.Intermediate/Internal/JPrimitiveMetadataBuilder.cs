namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// <see cref="JPrimitiveMetadata"/> class builder.
/// </summary>
internal sealed class JPrimitiveMetadataBuilder
{
	/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
	private readonly CString _signature;
	/// <inheritdoc cref="JPrimitiveMetadata.SizeOfOf"/>
	private readonly Int32 _sizeOf;
	/// <inheritdoc cref="JDataTypeMetadata.Type"/>
	private readonly Type _type;
	/// <inheritdoc cref="JDataTypeMetadata.ArraySignature"/>
	private CString? _arraySignature;

	/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
	private CString? _className;
	/// <inheritdoc cref="JPrimitiveMetadata.ClassSignature"/>
	private CString? _classSignature;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="type">CLR type current primitive type.</param>
	/// <param name="sizeOf">Size of current primitive type in bytes.</param>
	/// <param name="signature">JNI signature for current primitive type.</param>
	private JPrimitiveMetadataBuilder(Type type, Int32 sizeOf, CString signature)
	{
		this._type = type;
		this._sizeOf = sizeOf;
		this._signature = signature;
	}

	/// <summary>
	/// Sets the wrapper class name.
	/// </summary>
	/// <param name="className">Wrapper class name to set.</param>
	/// <returns>Current instance.</returns>
	public JPrimitiveMetadataBuilder WithWrapperClassName(CString className)
	{
		this._className = ValidationUtilities.ValidateNotEmpty(className);
		return this;
	}
	/// <summary>
	/// Sets the array signature.
	/// </summary>
	/// <param name="arraySignature">Array signature.</param>
	/// <returns>Current instance.</returns>
	public JPrimitiveMetadataBuilder WithArraySignature(CString arraySignature)
	{
		ValidationUtilities.ThrowIfInvalidSignature(arraySignature, false);
		this._arraySignature = arraySignature;
		return this;
	}
	/// <summary>
	/// Sets the wrapper class signature.
	/// </summary>
	/// <param name="classSignature">Wrapper class signature.</param>
	/// <returns>Current instance.</returns>
	public JPrimitiveMetadataBuilder WithWrapperClassSignature(CString classSignature)
	{
		ValidationUtilities.ThrowIfInvalidSignature(classSignature, false);
		this._classSignature = classSignature;
		return this;
	}
	/// <summary>
	/// Creates the <see cref="JPrimitiveMetadata"/> instance.
	/// </summary>
	/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
	public JPrimitiveMetadata Build()
		=> new(this._type, this._sizeOf, this._signature, ValidationUtilities.ValidateNotEmpty(this._className),
		       this._arraySignature, this._classSignature);

	/// <summary>
	/// Creates a new <see cref="JPrimitiveMetadataBuilder"/> instance.
	/// </summary>
	/// <typeparam name="TPrimitive"><see cref="IPrimitive"/> type.</typeparam>
	/// <param name="signature">Primitive type signature.</param>
	/// <returns>A new <see cref="JPrimitiveMetadataBuilder"/> instance.</returns>
	public static JPrimitiveMetadataBuilder Create<TPrimitive>(CString signature)
		where TPrimitive : unmanaged, IPrimitive<TPrimitive>
	{
		ValidationUtilities.ThrowIfInvalidSignature(signature, true);
		Type type = typeof(TPrimitive);
		Int32 sizeOf = NativeUtilities.SizeOf<TPrimitive>();
		return new(type, sizeOf, signature);
	}
}