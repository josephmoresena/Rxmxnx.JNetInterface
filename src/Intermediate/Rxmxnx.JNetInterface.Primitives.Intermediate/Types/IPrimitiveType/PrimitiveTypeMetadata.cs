namespace Rxmxnx.JNetInterface.Types;

internal partial interface IPrimitiveType<TPrimitive, TValue>
{
	public sealed class PrimitiveTypeMetadata : JPrimitiveTypeMetadata<TPrimitive>
	{
		/// <inheritdoc/>
		public override JNativeType NativeType { get; }
		/// <inheritdoc/>
		public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TPrimitive>();
		/// <inheritdoc/>
		public override Type Type { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="info">JNI signature for the current primitive type.</param>
		/// <param name="wrapperInfo">Wrapper class name of the current primitive type.</param>
		internal PrimitiveTypeMetadata(TypeInfoSequence info, TypeInfoSequence wrapperInfo) : base(
			typeof(TValue), info, wrapperInfo)
		{
			this.NativeType = TPrimitive.JniType;
			this.Type = typeof(TPrimitive);
		}

		/// <inheritdoc/>
		public override IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes) => bytes.ToValue<TPrimitive>();
#if PACKAGE
		/// <inheritdoc/>
		public override JArrayTypeMetadata GetArrayMetadata() 
			// Required to initialize JArrayObject<JArrayObject<TPrimitive>> type.
			=> (JArrayTypeMetadata)IArrayType.GetArrayArrayMetadata<TPrimitive>().ElementMetadata;
#endif
	}
}