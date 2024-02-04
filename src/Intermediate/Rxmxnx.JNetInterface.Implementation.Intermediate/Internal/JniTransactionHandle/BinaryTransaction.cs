namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI transaction with two references.
	/// </summary>
	private sealed record BinaryTransaction : FixedTransaction
	{
		/// <summary>
		/// Internal transaction reference 1.
		/// </summary>
		private JObjectLocalRef _localRef1;
		/// <summary>
		/// Internal transaction reference 2.
		/// </summary>
		private JObjectLocalRef _localRef2;

		/// <summary>
		/// Constructor.
		/// </summary>
		public BinaryTransaction() : base(2) { }

		/// <inheritdoc/>
		protected override void PutValue(JObjectLocalRef localRef)
		{
			this._localRef2 = this._localRef1;
			this._localRef1 = localRef;
		}
		/// <inheritdoc/>
		protected override Boolean InTransaction(IntPtr reference)
			=> this._localRef1.Pointer == reference || this._localRef2.Pointer == reference;
	}
}