namespace Rxmxnx.JNetInterface.Internal;

internal abstract partial class VirtualMachineCore : IGlobalObjectManager, INativeMemoryManager
{
	JGlobal? IGlobalObjectManager.Register(JGlobal? jGlobal)
	{
		if (jGlobal is null || jGlobal.IsDefault) return jGlobal;
		using IThread thread = this.VirtualMachine.CreateThread(ThreadPurpose.CheckGlobalReference);
		this.GlobalObjects[jGlobal.Reference] = new(jGlobal.GetCacheable(thread));
		if (CommonNames.ClassObject.SequenceEqual(jGlobal.ObjectMetadata.ObjectClassName))
			this.GlobalClassCache.Load(jGlobal.As<JClassLocalRef>());
		return jGlobal;
	}
	JWeak? IGlobalObjectManager.Register(JWeak? jWeak)
	{
		if (jWeak is null || jWeak.IsDefault) return jWeak;
		this.WeakObjects[jWeak.Reference] = new(jWeak);
		if (CommonNames.ClassObject.SequenceEqual(jWeak.ObjectMetadata.ObjectClassName))
			this.WeakClassCache.Load(jWeak.As<JClassLocalRef>());
		return jWeak;
	}
	void IGlobalObjectManager.Remove(JGlobalBase? jGlobal)
	{
		switch (jGlobal)
		{
			case JGlobal:
				this.Remove(jGlobal.As<JGlobalRef>());
				break;
			case JWeak:
				this.Remove(jGlobal.As<JWeakRef>());
				break;
		}
	}

	INativeTransaction INativeMemoryManager.CreateTransaction(Int32 capacity) => this.CreateTransaction(capacity);
	IDisposable INativeMemoryManager.CreateSynchronized(IEnvironment env, JReferenceObject jObject)
		=> JniTransactionHandle.CreateSynchronizer(env, jObject, this._transactions);
	INativeMemoryAdapter INativeMemoryManager.CreateMemoryAdapter(JStringObject jString,
		JMemoryReferenceKind referenceKind, Boolean? critical)
		=> JniTransactionHandle.CreateMemoryAdapter(jString, referenceKind, critical, this._transactions);
	INativeMemoryAdapter INativeMemoryManager.CreateMemoryAdapter<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind, Boolean critical)
		=> JniTransactionHandle.CreateMemoryAdapter(jArray, referenceKind, critical, this._transactions);
	Boolean INativeMemoryManager.SecureRemove(JWeakRef weakRef) => !this.InTransaction(weakRef.Pointer);
	Boolean INativeMemoryManager.SecureRemove(JGlobalRef globalRef) => !this.InTransaction(globalRef.Pointer);
	Boolean INativeMemoryManager.SecureRemove(JObjectLocalRef localRef) => !this.InTransaction(localRef.Pointer);
}