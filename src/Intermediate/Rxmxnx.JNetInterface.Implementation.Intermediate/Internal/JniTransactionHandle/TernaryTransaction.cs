namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI transaction with three references.
	/// </summary>
	private sealed record TernaryTransaction : FixedTransaction
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
		/// Internal transaction reference 3.
		/// </summary>
		private JObjectLocalRef _localRef3;

		/// <summary>
		/// Constructor.
		/// </summary>
		public TernaryTransaction() : base(3) { }

		/// <inheritdoc/>
		protected override void PutValue(JObjectLocalRef localRef)
		{
			this._localRef3 = this._localRef2;
			this._localRef2 = this._localRef1;
			this._localRef1 = localRef;
		}
		/// <inheritdoc/>
		protected override Boolean InTransaction(IntPtr reference)
			=> this._localRef1.Pointer == reference || this._localRef2.Pointer == reference ||
				this._localRef3.Pointer == reference;
	}
}