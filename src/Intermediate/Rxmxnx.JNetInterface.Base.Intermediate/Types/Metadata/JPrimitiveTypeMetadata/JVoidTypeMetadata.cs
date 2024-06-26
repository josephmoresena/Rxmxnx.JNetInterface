namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial class JPrimitiveTypeMetadata
{
	/// <summary>
	/// Stores the metadata for Java <c>void</c> type.
	/// </summary>
	private sealed class JVoidTypeMetadata()
		: JPrimitiveTypeMetadata(0, typeof(void), JPrimitiveTypeMetadata.voidInformation, "java/lang/Void"u8)
	{
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