namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI transaction with more than three references.
	/// </summary>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3459,
	                 Justification = CommonConstants.ReferenceableFieldJustification)]
#endif
	private sealed class SetTransaction : INativeTransaction
	{
		/// <summary>
		/// Lock object.
		/// </summary>
		private readonly Object _lock = new();
		/// <summary>
		/// Set of references.
		/// </summary>
		private readonly HashSet<IntPtr> _references = [];

		/// <summary>
		/// Indicates current instance is disposed.
		/// </summary>
		private Boolean _disposed;
		/// <summary>
		/// Current transaction handle.
		/// </summary>
		private JniTransactionHandle _handle;

		/// <inheritdoc/>
		public ref JniTransactionHandle Reference => ref this._handle;

		ref readonly JniTransactionHandle IReadOnlyReferenceable<JniTransactionHandle>.Reference => ref this.Reference;

		/// <inheritdoc/>
		public JObjectLocalRef Add(JObjectLocalRef localRef)
		{
			if (localRef == default) return default;
			lock (this._lock)
				this._references.Add(localRef.Pointer);
			return localRef;
		}
		/// <summary>
		/// Indicates whether reference is used by current transaction.
		/// </summary>
		/// <param name="reference">A <see cref="IntPtr"/> reference.</param>
		/// <returns>
		/// <see langword="true"/> if reference is used by current transaction;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		public Boolean Contains(IntPtr reference)
		{
			if (reference == IntPtr.Zero) return false;
			lock (this._lock)
				return this._references.Contains(reference);
		}
		/// <inheritdoc/>
		public void Dispose()
		{
			if (this._disposed) return;
			this._disposed = true;
			this._handle.Dispose();
		}
	}
}