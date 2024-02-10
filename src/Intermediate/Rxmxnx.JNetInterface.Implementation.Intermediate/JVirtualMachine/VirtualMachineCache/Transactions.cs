namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	private partial record VirtualMachineCache
	{
		/// <inheritdoc cref="JVirtualMachine.CreateTransaction(Int32)"/>
		public INativeTransaction CreateTransaction(Int32 capacity)
			=> JniTransactionHandle.CreateTransaction(capacity, this._transactions);
		/// <inheritdoc cref="JVirtualMachine.CreateSynchronized(IEnvironment, JReferenceObject)"/>
		public IDisposable CreateSynchronized(IEnvironment env, JReferenceObject jObject)
			=> JniTransactionHandle.CreateSynchronizer(env, jObject, this._transactions);
		/// <inheritdoc cref="JVirtualMachine.CreateMemoryAdapter(JStringObject, JMemoryReferenceKind, Nullable{Boolean})"/>
		public INativeMemoryAdapter CreateMemoryAdapter(JStringObject jString, JMemoryReferenceKind referenceKind,
			Boolean? critical)
			=> JniTransactionHandle.CreateMemoryAdapter(jString, referenceKind, critical, this._transactions);
		/// <inheritdoc
		///     cref="JVirtualMachine.CreateMemoryAdapter{TPrimitive}(JArrayObject{TPrimitive}, JMemoryReferenceKind, Boolean)"/>
		public INativeMemoryAdapter CreateMemoryAdapter<TPrimitive>(JArrayObject<TPrimitive> jArray,
			JMemoryReferenceKind referenceKind, Boolean critical)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
			=> JniTransactionHandle.CreateMemoryAdapter(jArray, referenceKind, critical, this._transactions);
		/// <summary>
		/// Indicates whether given <paramref name="jRef"/> is begin using by a transaction.
		/// </summary>
		/// <param name="jRef">A <see cref="IntPtr"/> value.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="jRef"/> is begin using by a transaction;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		public Boolean InTransaction(IntPtr jRef)
		{
			Boolean result = false;
			Parallel.ForEach(this._transactions.Values, (t, s) =>
			{
				if (!t.Contains(jRef)) return;
				result = true;
				s.Stop();
			});
			return result;
		}
	}
}