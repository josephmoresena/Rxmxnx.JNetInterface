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
		protected override void PutValue(JObjectLocalRef localRef) => this.LocalRef = localRef;
		/// <inheritdoc/>
		protected override Boolean InTransaction(IntPtr reference) => this.LocalRef.Pointer == reference;
	}
}