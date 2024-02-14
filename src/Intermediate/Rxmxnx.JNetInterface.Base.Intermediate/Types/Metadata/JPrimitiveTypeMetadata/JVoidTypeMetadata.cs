namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial record JPrimitiveTypeMetadata
{
	/// <summary>
	/// Stores the metadata for Java <c>void</c> type.
	/// </summary>
	private sealed record JVoidTypeMetadata : JPrimitiveTypeMetadata
	{
		/// <summary>
		/// <c>void</c> class fake hash.
		/// </summary>
		public static readonly String FakeHash =
			JDataTypeMetadata.CreateInformationSequence(UnicodeClassNames.VoidPrimitive()).ToString();

		/// <summary>
		/// CLR type for <see langword="void"/>.
		/// </summary>
		public override Type Type => typeof(void);
		/// <inheritdoc/>
		public override JNativeType NativeType => default;
		/// <inheritdoc/>
		public override JArgumentMetadata ArgumentMetadata => ValidationUtilities.ThrowVoidArgument();

		/// <summary>
		/// Constructor.
		/// </summary>
		public JVoidTypeMetadata() { }

		/// <inheritdoc/>
		public override IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes)
			=> ValidationUtilities.ThrowVoidInstantiation();
		/// <inheritdoc/>
		public override String ToString()
			=> $"{nameof(JDataTypeMetadata)} {{ {nameof(JDataTypeMetadata.Type)} = {this.Type}, " +
				$"{nameof(JDataTypeMetadata.Kind)} = {this.Kind}, " +
				$"{nameof(JPrimitiveTypeMetadata.UnderlineType)} = {this.UnderlineType}, " +
				$"{nameof(JPrimitiveTypeMetadata.NativeType)} = {this.NativeType}, " +
				$"{nameof(JPrimitiveTypeMetadata.WrapperClassName)} = {this.WrapperClassName}, " +
				$"{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";
	}
}