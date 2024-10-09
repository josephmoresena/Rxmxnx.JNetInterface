namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial class JPrimitiveTypeMetadata
{
	/// <summary>
	/// Stores the metadata for Java <c>void</c> type.
	/// </summary>
	private sealed class JVoidTypeMetadata()
		: JPrimitiveTypeMetadata(0, typeof(void), JVoidTypeMetadata.voidPrimitiveInfo,
		                         JVoidTypeMetadata.voidWrapperInfo)
	{
		/// <summary>
		/// Primitive type info.
		/// </summary>
		private static readonly TypeInfoSequence voidPrimitiveInfo = new(ClassNameHelper.VoidPrimitiveHash, 4, 1);
		/// <summary>
		/// Wrapper type info.
		/// </summary>
		private static readonly TypeInfoSequence voidWrapperInfo = new(ClassNameHelper.VoidObjectHash, 14, 16);

		/// <summary>
		/// CLR type for <see langword="void"/>.
		/// </summary>
		public override Type Type => typeof(void);
		/// <inheritdoc/>
		public override JNativeType NativeType => JNativeType.Void;
		/// <inheritdoc/>
		public override JArgumentMetadata ArgumentMetadata => CommonValidationUtilities.ThrowVoidArgument();

#if PACKAGE
		/// <inheritdoc/>
		public override JArrayTypeMetadata GetArrayMetadata() 
			=> CommonValidationUtilities.ThrowVoidArray();
#endif
		/// <inheritdoc/>
		public override IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes)
			=> CommonValidationUtilities.ThrowVoidInstantiation();
	}
}