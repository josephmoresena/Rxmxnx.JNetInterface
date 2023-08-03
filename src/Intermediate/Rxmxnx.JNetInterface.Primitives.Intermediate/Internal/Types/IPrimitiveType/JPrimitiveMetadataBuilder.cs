namespace Rxmxnx.JNetInterface.Internal.Types;

internal partial interface IPrimitiveType<TPrimitive, TValue>
{
	/// <summary>
	/// <see cref="JPrimitiveMetadata"/> class builder.
	/// </summary>
	protected sealed partial class JPrimitiveMetadataBuilder
	{
		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		private readonly CString _signature;
		/// <inheritdoc cref="JDataTypeMetadata.ArraySignature"/>
		private CString? _arraySignature;

		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		private CString? _className;
		/// <inheritdoc cref="JPrimitiveMetadata.ClassSignature"/>
		private CString? _classSignature;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="signature">JNI signature for current primitive type.</param>
		private JPrimitiveMetadataBuilder(CString signature) => this._signature = signature;

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
			=> new JPrimitiveGenericMetadata(NativeUtilities.SizeOf<TPrimitive>(), typeof(TValue), this._signature,
			                                 ValidationUtilities.ValidateNotEmpty(this._className),
			                                 this._arraySignature, this._classSignature);

		/// <summary>
		/// Creates a new <see cref="JPrimitiveMetadataBuilder"/> instance.
		/// </summary>
		/// <param name="signature">Primitive type signature.</param>
		/// <returns>A new <see cref="JPrimitiveMetadataBuilder"/> instance.</returns>
		public static JPrimitiveMetadataBuilder Create(CString signature)
		{
			ValidationUtilities.ThrowIfInvalidSignature(signature, true);
			return new(signature);
		}
	}
}