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
		public JVoidTypeMetadata() : base(default, typeof(void),
		                                  stackalloc Byte[1] { UnicodePrimitiveSignatures.VoidSignatureChar, },
		                                  UnicodeClassNames.VoidPrimitive(), UnicodeClassNames.VoidObject()) { }
		/// <inheritdoc/>
		public override IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes)
			=> ValidationUtilities.ThrowVoidInstantiation();
		/// <inheritdoc/>
		public override String ToString()
			=> $"{nameof(JDataTypeMetadata)} {{ {base.ToString()}{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";
	}
}