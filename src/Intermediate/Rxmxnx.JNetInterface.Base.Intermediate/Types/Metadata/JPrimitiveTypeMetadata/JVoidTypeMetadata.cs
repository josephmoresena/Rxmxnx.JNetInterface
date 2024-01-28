namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial record JPrimitiveTypeMetadata
{
	/// <summary>
	/// Stores the metadata for Java <c>void</c> type.
	/// </summary>
	private sealed record JVoidTypeMetadata : JPrimitiveTypeMetadata
	{
		public override Type Type => typeof(void);

		/// <inheritdoc/>
		public override JNativeType NativeType => default;

		/// <summary>
		/// Constructor.
		/// </summary>
		public JVoidTypeMetadata() : base(default, typeof(void),
		                                  stackalloc Byte[1] { UnicodePrimitiveSignatures.VoidSignatureChar, },
		                                  UnicodeClassNames.VoidPrimitive(), UnicodeClassNames.VoidObject()) { }
		/// <inheritdoc/>
		public override IPrimitiveType CreateInstance(ReadOnlySpan<Byte> bytes) => throw new InvalidOperationException("A void value can't be created.");
	}
}