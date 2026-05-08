namespace Rxmxnx.JNetInterface.Internal;

internal abstract partial class VirtualMachineCore
{
	/// <summary>
	/// JNI transaction dictionary.
	/// </summary>
	private readonly ConcurrentDictionary<Guid, INativeTransaction> _transactions = new();
	/// <summary>
	/// Global object dictionary.
	/// </summary>
	private protected readonly ConcurrentDictionary<JGlobalRef, WeakReference<JGlobal>> GlobalObjects = new();
	/// <summary>
	/// Main <see cref="IVirtualMachine"/> instance.
	/// </summary>
	private protected readonly IVirtualMachine VirtualMachine;
	/// <summary>
	/// Weak global object dictionary.
	/// </summary>
	private protected readonly ConcurrentDictionary<JWeakRef, WeakReference<JWeak>> WeakObjects = new();

	/// <summary>
	/// Removes <see cref="JGlobalRef"/> from current cache.
	/// </summary>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
	private void Remove(JGlobalRef globalRef)
	{
		if (globalRef == default) return;
		this.GlobalObjects.Remove(globalRef, out _);
		this.GlobalClassCache.Unload(new JClassLocalRef(globalRef));
	}
	/// <summary>
	/// Removes <see cref="JWeakRef"/> from current cache.
	/// </summary>
	/// <param name="weakRef">A <see cref="JWeakRef"/> reference.</param>
	private void Remove(JWeakRef weakRef)
	{
		if (weakRef == default) return;
		this.WeakObjects.Remove(weakRef, out _);
		this.WeakClassCache.Unload(new JClassLocalRef(weakRef));
	}
	/// <summary>
	/// Indicates whether given <paramref name="jniRef"/> is begin using by a transaction.
	/// </summary>
	/// <param name="jniRef">A <see cref="IntPtr"/> value.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jniRef"/> is begin using by a transaction;
	/// otherwise, <see langword="false"/>.
	/// </returns>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3267,
	                 Justification = CommonConstants.NonStandardLinqJustification)]
#endif
	private Boolean InTransaction(IntPtr jniRef)
	{
		// ReSharper disable once LoopCanBeConvertedToQuery
		foreach (KeyValuePair<Guid, INativeTransaction> kvp in this._transactions)
		{
			if (kvp.Value.Contains(jniRef))
				return true;
		}
		return false;
	}
	/// <inheritdoc cref="INativeMemoryManager.CreateTransaction(Int32)"/>
	private INativeTransaction CreateTransaction(Int32 capacity)
		=> JniTransactionHandle.CreateTransaction(capacity, this._transactions);
}