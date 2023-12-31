namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveType<TPrimitive, TValue>
{
	protected ref partial struct JTypeMetadataBuilder
	{
		/// <summary>
		/// This record stores the metadata for a value <see cref="IPrimitiveType"/> type.
		/// </summary>
		private sealed record JPrimitiveGenericTypeMetadata : JPrimitiveTypeMetadata
		{
			/// <summary>
			/// Native primitive type.
			/// </summary>
			private readonly JNativeType _nativeType;
			/// <inheritdoc cref="SizeOf"/>
			private readonly Int32 _sizeOf;
			/// <inheritdoc cref="JDataTypeMetadata.Type"/>
			private readonly Type _type;
			/// <summary>
			/// CLR underline type.
			/// </summary>
			private readonly Type _underlineType;
			/// <summary>
			/// Wrapper class information.
			/// </summary>
			private readonly CStringSequence _wrapperInformation;

			/// <inheritdoc/>
			public override CString WrapperClassSignature => this._wrapperInformation[1];
			/// <inheritdoc/>
			public override CString WrapperClassName => this._wrapperInformation[0];
			/// <inheritdoc/>
			public override Type UnderlineType => this._underlineType;
			/// <inheritdoc/>
			public override JNativeType NativeType => this._nativeType;
			/// <inheritdoc/>
			public override Type Type => this._type;
			/// <inheritdoc/>
			internal override CStringSequence WrapperInformation => this._wrapperInformation;
			/// <summary>
			/// Size of current primitive type in bytes.
			/// </summary>
			public override Int32 SizeOf => this._sizeOf;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="sizeOf">Size of current primitive type in bytes.</param>
			/// <param name="underlineType">Underline primitive CLR type.</param>
			/// <param name="signature">JNI signature for current primitive type.</param>
			/// <param name="className">Wrapper class name of current primitive type.</param>
			/// <param name="wrapperClassName">Wrapper class JNI name of current primitive type.</param>
			internal JPrimitiveGenericTypeMetadata(Int32 sizeOf, Type underlineType, ReadOnlySpan<Byte> signature,
				ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> wrapperClassName) : base(signature, className)
			{
				this._sizeOf = sizeOf;
				this._underlineType = underlineType;
				this._nativeType = TPrimitive.JniType;
				this._type = typeof(TPrimitive);
				this._wrapperInformation = JDataTypeMetadata.CreateInformationSequence(wrapperClassName);
			}

			/// <inheritdoc/>
			public override IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes) => bytes.ToValue<TPrimitive>();
		}
	}
}