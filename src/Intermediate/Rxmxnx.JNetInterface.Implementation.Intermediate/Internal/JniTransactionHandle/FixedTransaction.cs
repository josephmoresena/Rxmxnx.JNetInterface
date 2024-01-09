namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
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

		ref readonly JniTransactionHandle IReadOnlyReferenceable<JniTransactionHandle>.Reference => ref this.Reference;

		/// <inheritdoc/>
		public virtual JObjectLocalRef Add(JObjectLocalRef localRef)
		{
			if (localRef == default) return default;
			this._count++;
			if (this._count > this._transactionCapacity)
				throw new InvalidOperationException(
					$"This transaction can hold only {this._transactionCapacity} reference{(this._transactionCapacity != 1 ? "s" : "")}.");
			return localRef;
		}
		/// <inheritdoc/>
		public virtual TReference Add<TReference>(TReference nativeRef)
			where TReference : unmanaged, IObjectReferenceType<TReference>
		{
			this.Add(nativeRef.Value);
			return nativeRef;
		}

		/// <inheritdoc cref="IReferenceable{T}.Reference"/>
		public ref JniTransactionHandle Reference => ref this._handle;

		public virtual void Dispose()
		{
			if (this._disposed) return;
			this._disposed = true;
			this._handle.Dispose();
			GC.SuppressFinalize(this);
		}

		/// <inheritdoc/>
		public abstract Boolean Contains(IntPtr reference);
	}
}