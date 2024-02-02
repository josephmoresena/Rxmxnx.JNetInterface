namespace Rxmxnx.JNetInterface.Internal;

internal partial struct JniTransactionHandle
{
	/// <summary>
	/// Represents a JNI native array memory adapter.
	/// </summary>
	private sealed record NativeArrayMemoryAdapter<TPrimitive> : NativeMemoryAdapter
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		/// <inheritdoc cref="UnaryTransaction.LocalRef"/>
		private new JArrayLocalRef LocalRef
			=> NativeUtilities.Transform<JObjectLocalRef, JArrayLocalRef>(in base.LocalRef);

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject{Primitive}"/> instance.</param>
		/// <param name="referenceKind">Reference memory kind.</param>
		/// <param name="critical">Indicates this adapter is for a critical sequence.</param>
		public NativeArrayMemoryAdapter(JArrayObject<TPrimitive> jArray, JMemoryReferenceKind referenceKind,
			Boolean critical) : base(jArray, referenceKind, critical)
			=> this.BinarySize = jArray.Length * IPrimitiveType.GetMetadata<TPrimitive>().SizeOf;

		/// <inheritdoc/>
		public override void Activate(IEnvironment env)
		{
			this.Pointer = !this.Critical ?
				env.ArrayFeature.GetPrimitiveSequence<TPrimitive>(this.LocalRef, out this.IsCopy) :
				env.ArrayFeature.GetPrimitiveCriticalSequence(this.LocalRef);
		}
		/// <inheritdoc/>
		public override void Release(JReleaseMode mode)
		{
			if (this.Disposed) return;
			using IThread thread = this.VirtualMachine.CreateThread(ThreadPurpose.ReleaseSequence);
			if (!this.Critical)
				thread.ArrayFeature.ReleasePrimitiveSequence<TPrimitive>(this.LocalRef, this.Pointer, mode);
			else
				thread.ArrayFeature.ReleasePrimitiveCriticalSequence(this.LocalRef, (ValPtr<Byte>)this.Pointer);
			base.Release(mode);
		}
	}
}