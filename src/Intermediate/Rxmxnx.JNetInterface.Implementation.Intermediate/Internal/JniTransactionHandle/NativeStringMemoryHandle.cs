namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI native string memory handle.
	/// </summary>
	private sealed record NativeStringMemoryHandle : NativeMemoryHandle
	{
		/// <summary>
		/// Indicates whether current handle is for UTF-8 chars.
		/// </summary>
		private readonly Boolean _utf8Chars;

		/// <inheritdoc cref="UnaryTransaction.LocalRef"/>
		private new JStringLocalRef LocalRef
			=> NativeUtilities.Transform<JObjectLocalRef, JStringLocalRef>(in base.LocalRef);

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
		/// <param name="referenceKind">Reference memory kind.</param>
		/// <param name="critical">Indicates this handle is for a critical sequence.</param>
		public NativeStringMemoryHandle(JStringObject jString, JMemoryReferenceKind referenceKind, Boolean? critical) :
			base(jString, referenceKind, critical.GetValueOrDefault())
		{
			this._utf8Chars = !critical.HasValue;
			this.BinarySize = this._utf8Chars ? jString.Utf8Length : jString.Length * sizeof(Char);
			IEnvironment env = jString.Environment;
		}

		/// <summary>
		/// Activates current memory handle.
		/// </summary>
		/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
		public void Activate(IEnvironment env)
		{
			this.Pointer = this._utf8Chars ? env.StringFeature.GetUtf8Sequence(this.LocalRef, out this.IsCopy) :
				!this.Critical ? env.StringFeature.GetSequence(this.LocalRef, out this.IsCopy) :
				env.StringFeature.GetCriticalSequence(this.LocalRef);
		}

		/// <inheritdoc/>
		public override void Release(JReleaseMode mode)
		{
			using IThread thread = this.VirtualMachine.CreateThread(ThreadPurpose.ReleaseSequence);
			if (this._utf8Chars)
				thread.StringFeature.ReleaseUtf8Sequence(this.LocalRef, (ReadOnlyValPtr<Byte>)this.Pointer);
			else if (!this.Critical)
				thread.StringFeature.ReleaseSequence(this.LocalRef, (ReadOnlyValPtr<Char>)this.Pointer);
			else
				thread.StringFeature.ReleaseCriticalSequence(this.LocalRef, (ReadOnlyValPtr<Char>)this.Pointer);
			base.Release(mode);
		}
	}
}