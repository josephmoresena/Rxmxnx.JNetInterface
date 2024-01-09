namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI transaction with two references.
	/// </summary>
	private sealed record DuplexTransaction : FixedTransaction
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
		public DuplexTransaction() : base(2) { }

		/// <inheritdoc/>
		public override JObjectLocalRef Add(JObjectLocalRef localRef)
		{
			base.Add(localRef);
			if (localRef == default) return default;
			this._localRef2 = this._localRef1;
			this._localRef1 = localRef;
			return localRef;
		}
		/// <inheritdoc/>
		public override Boolean Contains(IntPtr reference)
			=> reference != default && (this._localRef1.Pointer == reference || this._localRef2.Pointer == reference);
	}
}