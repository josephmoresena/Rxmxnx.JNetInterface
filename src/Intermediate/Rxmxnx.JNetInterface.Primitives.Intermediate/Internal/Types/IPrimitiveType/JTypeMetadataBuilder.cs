namespace Rxmxnx.JNetInterface.Internal.Types;

internal partial interface IPrimitiveType<TPrimitive, TValue>
{
	/// <summary>
	/// <see cref="JPrimitiveTypeMetadata"/> class builder.
	/// </summary>
	protected sealed partial class JTypeMetadataBuilder
	{
		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		private readonly CString _signature;
		/// <inheritdoc cref="JDataTypeMetadata.ArraySignature"/>
		private CString? _arraySignature;

		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		private CString? _className;
		/// <inheritdoc cref="JPrimitiveTypeMetadata.ClassSignature"/>
		private CString? _classSignature;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="signature">JNI signature for current primitive type.</param>
		private JTypeMetadataBuilder(CString signature) => this._signature = signature;

		/// <summary>
		/// Sets the wrapper class name.
		/// </summary>
		/// <param name="className">Wrapper class name to set.</param>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder WithWrapperClassName(CString className)
		{
			this._className = ValidationUtilities.ValidateNotEmpty(className);
			return this;
		}
		/// <summary>
		/// Sets the array signature.
		/// </summary>
		/// <param name="arraySignature">Array signature.</param>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder WithArraySignature(CString arraySignature)
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
			                                 this._arraySignature, this._classSignature);

		/// <summary>
		/// Creates a new <see cref="JTypeMetadataBuilder"/> instance.
		/// </summary>
		/// <param name="signature">Primitive type signature.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder"/> instance.</returns>
		public static JTypeMetadataBuilder Create(CString signature)
		{
			ValidationUtilities.ThrowIfInvalidSignature(signature, true);
			return new(signature);
		}
	}
}