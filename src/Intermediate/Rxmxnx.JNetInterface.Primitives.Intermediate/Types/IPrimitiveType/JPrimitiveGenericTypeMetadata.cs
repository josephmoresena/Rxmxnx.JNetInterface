using Rxmxnx.JNetInterface.Native.Access;

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
			/// <param name="sizeOf">Size of current primitive type in bytes.</param>
			/// <param name="underlineType">Underline primitive CLR type.</param>
			/// <param name="signature">JNI signature for current primitive type.</param>
			/// <param name="className">Wrapper class name of current primitive type.</param>
			/// <param name="wrapperClassName">Wrapper class JNI name of current primitive type.</param>
			internal JPrimitiveGenericTypeMetadata(Int32 sizeOf, Type underlineType, ReadOnlySpan<Byte> signature,
				ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> wrapperClassName) : base(
				sizeOf, underlineType, signature, className, wrapperClassName)
			{
				this._nativeType = TPrimitive.JniType;
				this._type = typeof(TPrimitive);
			}

			/// <inheritdoc/>
			public override IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes) => bytes.ToValue<TPrimitive>();
			/// <inheritdoc/>
			public override String ToString()
				=> $"{{ {base.ToString()}{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";
		}
	}
}