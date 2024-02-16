namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI native string memory adapter.
	/// </summary>
	private sealed record NativeStringMemoryAdapter : NativeMemoryAdapter
	{
		/// <summary>
		/// Indicates whether current adapter is for UTF-8 chars.
		/// </summary>
		private readonly Boolean _utf8Chars;

		/// <inheritdoc cref="UnaryTransaction.LocalRef"/>
		private new JStringLocalRef LocalRef => JStringLocalRef.FromReference(in base.LocalRef);

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
		/// <param name="referenceKind">Reference memory kind.</param>
		/// <param name="critical">Indicates this adapter is for a critical sequence.</param>
		public NativeStringMemoryAdapter(JStringObject jString, JMemoryReferenceKind referenceKind, Boolean? critical) :
			base(jString, referenceKind, critical.GetValueOrDefault())
		{
			this._utf8Chars = !critical.HasValue;
			this.BinarySize = this._utf8Chars ? jString.Utf8Length : jString.Length * sizeof(Char);
		}

		/// <inheritdoc/>
		public override void Activate(IEnvironment env)
		{
			this.Pointer = this._utf8Chars ?
				env.StringFeature.GetUtf8Sequence(this.LocalRef, out this.IsCopy) :
				this.GetUtf16Pointer(env);
		}
		/// <inheritdoc/>
		public override void Release(JReleaseMode mode = JReleaseMode.Free)
		{
			if (this.Disposed) return;
			using IThread thread = this.VirtualMachine.CreateThread(ThreadPurpose.ReleaseSequence);
			if (this._utf8Chars)
				thread.StringFeature.ReleaseUtf8Sequence(this.LocalRef, (ReadOnlyValPtr<Byte>)this.Pointer);
			else if (!this.Critical)
				thread.StringFeature.ReleaseSequence(this.LocalRef, (ReadOnlyValPtr<Char>)this.Pointer);
			else
				thread.StringFeature.ReleaseCriticalSequence(this.LocalRef, (ReadOnlyValPtr<Char>)this.Pointer);
			base.Release(mode);
		}

		/// <summary>
		/// Retrieves UTF-16 char pointer.
		/// </summary>
		/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
		/// <returns>A <see cref="ReadOnlyValPtr{Char}"/> pointer.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlyValPtr<Char> GetUtf16Pointer(IEnvironment env)
			=> !this.Critical ?
				env.StringFeature.GetSequence(this.LocalRef, out this.IsCopy) :
				env.StringFeature.GetCriticalSequence(this.LocalRef);
	}
}