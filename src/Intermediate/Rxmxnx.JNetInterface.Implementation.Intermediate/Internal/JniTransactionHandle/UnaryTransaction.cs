namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI transaction with single reference.
	/// </summary>
	private record UnaryTransaction : FixedTransaction
	{
		/// <summary>
		/// Internal transaction reference.
		/// </summary>
		protected JObjectLocalRef LocalRef;

		/// <summary>
		/// Constructor.
		/// </summary>
		public UnaryTransaction() : base(1) { }

		/// <inheritdoc/>
		public override JObjectLocalRef Add(JObjectLocalRef localRef)
		{
			if (localRef == default) return default;
			this.LocalRef = localRef;
			return localRef;
		}
		/// <inheritdoc/>
		public override Boolean Contains(IntPtr reference)
			=> reference != default && this.LocalRef.Pointer == reference;
	}
}