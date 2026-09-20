namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI transaction with more than three references.
	/// </summary>
	private sealed unsafe class MallocTransaction : FixedTransaction
	{
		/// <summary>
		/// Native memory pointer.
		/// </summary>
		private readonly JObjectLocalRef* _ptr;
		/// <summary>
		/// Current count of references.
		/// </summary>
		private Int32 _count;
		/// <summary>
		/// Internal lock instance.
		/// </summary>
		private ReaderWriterLockSlim? _lock;

		/// <inheritdoc/>
		public MallocTransaction(Int32 transactionCapacity) : base(transactionCapacity)
		{
			this._ptr = (JObjectLocalRef*)NativeMemory.Alloc((UIntPtr)(transactionCapacity * IntPtr.Size));
			this._count = 0;
			this._lock = new();
		}

		/// <inheritdoc/>
		protected override void PutValue(JObjectLocalRef localRef)
		{
			// This path is always safe.
			this._lock!.EnterWriteLock();
			try
			{
				this._ptr[this._count++] = localRef;
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}
		/// <inheritdoc/>
		protected override Boolean InTransaction(IntPtr reference)
		{
			ReaderWriterLockSlim? currentLock = this._lock;
			try
			{
				currentLock?.EnterReadLock();
			}
			catch (ObjectDisposedException)
			{
				// The current transaction is disposed of.
				return false;
			}

			if (currentLock is null)
				return false;

			try
			{
				return new ReadOnlySpan<IntPtr>(this._ptr, this._count).Contains(reference);
			}
			finally
			{
				currentLock.ExitReadLock();
			}
		}

		/// <inheritdoc/>
		protected override void Dispose(Boolean disposing)
		{
			base.Dispose(disposing);
			ReaderWriterLockSlim? currentLock = this._lock;
			this._lock = default;
			if (currentLock is null) return;
			currentLock.EnterWriteLock();
			try
			{
				NativeMemory.Free(this._ptr);
			}
			finally
			{
				currentLock.ExitWriteLock();
				currentLock.Dispose();
			}
		}
	}
}