namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveType<TPrimitive, TValue>
{
	/// <summary>
	/// <see cref="JPrimitiveTypeMetadata"/> class builder.
	/// </summary>
	protected ref partial struct JTypeMetadataBuilder
	{
		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		private readonly ReadOnlySpan<Byte> _signature;

		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		private ReadOnlySpan<Byte> _className;
		/// <inheritdoc cref="JPrimitiveTypeMetadata.ClassSignature"/>
		private CString? _classSignature;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="signature">JNI signature for current primitive type.</param>
		private JTypeMetadataBuilder(ReadOnlySpan<Byte> signature) => this._signature = signature;

		/// <summary>
		/// Sets the wrapper class name.
		/// </summary>
		/// <param name="className">Wrapper class name to set.</param>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder WithWrapperClassName(ReadOnlySpan<Byte> className)
		{
			this._className = ValidationUtilities.ValidateNotEmpty(className);
			return this;
		}
		/// <summary>
		/// Sets the wrapper class signature.
		/// </summary>
		/// <param name="classSignature">Wrapper class signature.</param>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder WithWrapperClassSignature(CString classSignature)
		{
			ValidationUtilities.ThrowIfInvalidSignature(classSignature, false);
			this._classSignature = classSignature;
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JPrimitiveTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JPrimitiveTypeMetadata Build()
			=> new JPrimitiveGenericTypeMetadata(NativeUtilities.SizeOf<TPrimitive>(), typeof(TValue), this._signature,
			                                     ValidationUtilities.ValidateNotEmpty(this._className),
			                                     this._classSignature);

		/// <summary>
		/// Creates a new <see cref="JTypeMetadataBuilder"/> instance.
		/// </summary>
		/// <param name="signature">Primitive type signature.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder"/> instance.</returns>
		public static JTypeMetadataBuilder Create(ReadOnlySpan<Byte> signature)
		{
			ValidationUtilities.ThrowIfInvalidSignature(signature, true);
			return new(signature);
		}
	}
}