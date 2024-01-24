namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Base class for fixed capacity transaction.
	/// </summary>
	private abstract record FixedTransaction : INativeTransaction
	{
		/// <summary>
		/// Transaction capacity.
		/// </summary>
		private readonly Int32 _transactionCapacity;
		/// <summary>
		/// Internal counter.
		/// </summary>
		private Int32 _count;
		/// <summary>
		/// Indicates current instance is disposed.
		/// </summary>
		private Boolean _disposed;

		/// <summary>
		/// Current transaction handle.
		/// </summary>
		private JniTransactionHandle _handle;

		/// <summary>
		/// Indicates whether current instance is disposed.
		/// </summary>
		protected Boolean Disposed => this._disposed;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="transactionCapacity">Transaction capacity.</param>
		protected FixedTransaction(Int32 transactionCapacity) => this._transactionCapacity = transactionCapacity;

		/// <inheritdoc cref="IReferenceable{T}.Reference"/>
		public ref JniTransactionHandle Reference => ref this._handle;

		ref readonly JniTransactionHandle IReadOnlyReferenceable<JniTransactionHandle>.Reference => ref this.Reference;

		/// <inheritdoc/>
		public JObjectLocalRef Add(JObjectLocalRef localRef)
		{
			if (localRef == default) return default;
			this._count++;
			if (this._count > this._transactionCapacity)
				throw new InvalidOperationException(
					$"This transaction can hold only {this._transactionCapacity} reference{(this._transactionCapacity != 1 ? "s" : "")}.");
			this.PutValue(localRef);
			return localRef;
		}
		/// <inheritdoc/>
		public virtual void Dispose()
		{
			if (this._disposed) return;
			this._disposed = true;
			this._handle.Dispose();
			GC.SuppressFinalize(this);
		}

		/// <inheritdoc/>
		public Boolean Contains(IntPtr reference) => reference != default && this.InTransaction(reference);

		/// <summary>
		/// Puts current value into current transaction.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		protected abstract void PutValue(JObjectLocalRef localRef);
		/// <summary>
		/// Indicates whether given <paramref name="reference"/> is begin using by a transaction.
		/// </summary>
		/// <param name="reference">A <see cref="IntPtr"/> value.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="reference"/> is begin using by a transaction;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		protected abstract Boolean InTransaction(IntPtr reference);
	}
}