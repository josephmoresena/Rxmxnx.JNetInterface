namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveType<TPrimitive, TValue>
{
	/// <summary>
	/// <see cref="JPrimitiveTypeMetadata"/> class builder.
	/// </summary>
	protected ref partial struct TypeMetadataBuilder
	{
		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		private readonly Byte _signature;

		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		private readonly ReadOnlySpan<Byte> _className;
		/// <inheritdoc cref="JPrimitiveTypeMetadata.WrapperClassName"/>
		private ReadOnlySpan<Byte> _wrapperClassName;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="className">Primitive class name.</param>
		/// <param name="signature">JNI signature for current primitive type.</param>
		private TypeMetadataBuilder(ReadOnlySpan<Byte> className, Byte signature)
		{
			this._className = className;
			this._signature = signature;
		}

		/// <summary>
		/// Sets the wrapper class name.
		/// </summary>
		/// <param name="className">Wrapper class name to set.</param>
		/// <returns>Current instance.</returns>
		public TypeMetadataBuilder WithWrapperClassName(ReadOnlySpan<Byte> className)
		{
			this._wrapperClassName = ValidationUtilities.ValidateNotEmpty(className);
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JPrimitiveTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JPrimitiveTypeMetadata<TPrimitive> Build()
			=> new PrimitiveTypeMetadata(typeof(TValue), stackalloc Byte[1] { this._signature, }, this._className,
			                             ValidationUtilities.ValidateNotEmpty(this._wrapperClassName));

		/// <summary>
		/// Creates a new <see cref="TypeMetadataBuilder"/> instance.
		/// </summary>
		/// <param name="className">Primitive class name.</param>
		/// <param name="signature">Primitive type signature.</param>
		/// <returns>A new <see cref="TypeMetadataBuilder"/> instance.</returns>
		public static TypeMetadataBuilder Create(ReadOnlySpan<Byte> className, Byte signature)
			=> new(className, signature);
	}
}