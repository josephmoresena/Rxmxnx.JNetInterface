namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI native memory handle.
	/// </summary>
	private abstract record NativeMemoryHandle : UnaryTransaction, INativeMemoryHandle
	{
		/// <summary>
		/// Synchronized instance.
		/// </summary>
		private readonly JReferenceObject _source;
		/// <inheritdoc cref="INativeMemoryHandle.Copy"/>
		protected Boolean IsCopy;

		/// <summary>
		/// Pointer to beginning element in current sequence.
		/// </summary>
		protected IntPtr Pointer;

		/// <summary>
		/// <see cref="IVirtualMachine"/> instance.
		/// </summary>
		protected IVirtualMachine VirtualMachine { get; }
		/// <summary>
		/// Binary sequence length.
		/// </summary>
		protected Int32 BinarySize { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="referenceKind">Reference memory kind.</param>
		/// <param name="critical">Indicates this handle is for a critical sequence.</param>
		protected NativeMemoryHandle(JLocalObject jLocal, JMemoryReferenceKind referenceKind, Boolean critical)
		{
			this.VirtualMachine = jLocal.Environment.VirtualMachine;
			this.Critical = critical;
			this._source = NativeMemoryHandle.GetSource(jLocal, referenceKind);
			(this as INativeTransaction).Add(this._source);
		}

		/// <inheritdoc/>
		public Boolean Critical { get; }
		/// <inheritdoc/>
		public Boolean Copy => this.IsCopy;

		/// <inheritdoc/>
		public IReadOnlyFixedContext<Byte>.IDisposable GetReadOnlyContext(JNativeMemory jMemory)
		{
			ReadOnlyValPtr<Byte> ptr = (ReadOnlyValPtr<Byte>)this.Pointer;
			return ptr.GetUnsafeFixedContext(this.BinarySize, jMemory);
		}
		/// <inheritdoc/>
		public IFixedContext<Byte>.IDisposable GetContext(JNativeMemory jMemory)
		{
			ValPtr<Byte> ptr = (ValPtr<Byte>)this.Pointer;
			return ptr.GetUnsafeFixedContext(this.BinarySize, jMemory);
		}

		/// <inheritdoc/>
		public virtual void Release(JReleaseMode mode) => this.Dispose();

		/// <inheritdoc/>
		public override void Dispose()
		{
			base.Dispose();
			if (!this.Disposed && this._source is JGlobalBase jGlobal)
				jGlobal.Dispose();
		}

		/// <summary>
		/// Retrieves the <see cref="JReferenceObject"/> instance which serves as source of current
		/// native memory.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="referenceKind">Reference memory kind.</param>
		/// <returns>A <see cref="JReferenceObject"/> instance.</returns>
		private static JReferenceObject GetSource(JLocalObject jLocal, JMemoryReferenceKind referenceKind)
		{
			IEnvironment env = jLocal.Environment;
			return referenceKind switch
			{
				JMemoryReferenceKind.ThreadIndependent => env.ReferenceFeature.Create<JWeak>(jLocal),
				JMemoryReferenceKind.ThreadUnrestricted => env.ReferenceFeature.Create<JGlobal>(jLocal),
				_ => jLocal,
			};
		}
	}
}