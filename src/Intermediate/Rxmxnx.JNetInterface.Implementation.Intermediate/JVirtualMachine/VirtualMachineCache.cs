namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This class stores cache for a <see cref="JVirtualMachine"/> instance.
	/// </summary>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
#endif
	private sealed partial class VirtualMachineCache : GlobalMainClasses
	{
		/// <summary>
		/// <see cref="NativeCache"/> instance.
		/// </summary>
		public readonly NativeCache NativesCache = new();
		/// <inheritdoc cref="JVirtualMachine.Reference"/>
		public readonly JVirtualMachineRef Reference;
		/// <summary>
		/// Thread cache.
		/// </summary>
		public readonly ThreadCache ThreadCache;
		/// <summary>
		/// Weak cache.
		/// </summary>
		public readonly ClassCache WeakClassCache = new(JReferenceType.WeakGlobalRefType);

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
		public VirtualMachineCache(JVirtualMachine vm, JVirtualMachineRef vmRef) : base(vm)
		{
			this._vm = vm;
			this.Reference = vmRef;
			this.ThreadCache = new(vm);
		}

		/// <summary>
		/// Retrieves managed <see cref="InvokeInterface"/> reference from current instance.
		/// </summary>
		/// <returns>A managed <see cref="InvokeInterface"/> reference from current instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref readonly InvokeInterface GetInvokeInterface()
			=> ref *(InvokeInterface*)this.Reference.InterfacePointer;
		/// <summary>
		/// Clears cache.
		/// </summary>
		public void ClearCache()
		{
			JGlobalRef[] globalKeys = this._globalObjects.Keys.ToArray();
			JWeakRef[] weakKeys = this._weakObjects.Keys.ToArray();
			this.GlobalClassCache.Clear();
			this.WeakClassCache.Clear();
			if (globalKeys.Length == 0 && weakKeys.Length == 0) return;
			using IThread thread = this._vm.CreateThread(ThreadPurpose.RemoveGlobalReference);
			if (thread is not JEnvironment env) return;
			foreach (JWeakRef key in weakKeys)
			{
				if (this._weakObjects.Remove(key, out WeakReference<JWeak>? weak) &&
				    weak.TryGetTarget(out JWeak? jWeak) && !jWeak.IsDefault)
					env.DeleteWeakGlobalRef(jWeak.Reference);
			}
			foreach (JGlobalRef key in globalKeys)
			{
				if (this._globalObjects.Remove(key, out WeakReference<JGlobal>? weak) &&
				    weak.TryGetTarget(out JGlobal? jGlobal) && !jGlobal.IsDefault)
					env.DeleteGlobalRef(jGlobal.Reference);
			}
		}
		/// <inheritdoc cref="INativeMemoryManager.CreateTransaction(Int32)"/>
		public INativeTransaction CreateTransaction(Int32 capacity)
			=> JniTransactionHandle.CreateTransaction(capacity, this._transactions);
	}
}