namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveType<TPrimitive, TValue>
{
	protected ref partial struct TypeMetadataBuilder
	{
		/// <summary>
		/// This record stores the metadata for a value <see cref="IPrimitiveType"/> type.
		/// </summary>
		private sealed class PrimitiveTypeMetadata : JPrimitiveTypeMetadata<TPrimitive>
		{
			/// <summary>
			/// Native primitive type.
			/// </summary>
			private readonly JNativeType _nativeType;
			/// <inheritdoc cref="JDataTypeMetadata.Type"/>
			private readonly Type _type;

			/// <inheritdoc/>
			public override JNativeType NativeType => this._nativeType;
			/// <inheritdoc/>
			public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TPrimitive>();
			/// <inheritdoc/>
			public override Type Type => this._type;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="underlineType">Underline primitive CLR type.</param>
			/// <param name="signature">JNI signature for the current primitive type.</param>
			/// <param name="className">Wrapper class name of the current primitive type.</param>
			/// <param name="wrapperClassName">Wrapper class JNI name of the current primitive type.</param>
			internal PrimitiveTypeMetadata(Type underlineType, ReadOnlySpan<Byte> signature,
				ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> wrapperClassName) : base(
				underlineType, signature, className, wrapperClassName)
			{
				this._nativeType = TPrimitive.JniType;
				this._type = typeof(TPrimitive);
			}

			/// <inheritdoc/>
			public override IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes) => bytes.ToValue<TPrimitive>();
#if PACKAGE
			/// <inheritdoc/>
			public override JArrayTypeMetadata GetArrayMetadata() 
				=> IArrayType.GetMetadata<JArrayObject<TPrimitive>>();
#endif
		}
	}
}