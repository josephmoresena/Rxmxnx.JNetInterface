namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance that <paramref name="envRef"/>
	/// references to.
	/// </summary>
	/// <param name="envRef"><see cref="JEnvironmentRef"/> reference to JNI interface.</param>
	/// <returns>
	/// The <see cref="IEnvironment"/> instance referenced by <paramref name="envRef"/>.
	/// </returns>
	internal JEnvironment GetEnvironment(JEnvironmentRef envRef) => this._cache.ThreadCache.Get(envRef, out _);
	/// <summary>
	/// Detaches the current thread from a JVM.
	/// </summary>
	internal void DetachCurrentThread()
	{
		DetachCurrentThreadDelegate del = this._cache.GetDelegate<DetachCurrentThreadDelegate>();
		del(this._cache.Reference);
	}
	/// <summary>
	/// Registers a <see cref="JGlobal"/> instance in current VM.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
	[return: NotNullIfNotNull(nameof(jGlobal))]
	internal JGlobal? Register(JGlobal? jGlobal) => this._cache.Register(jGlobal);
	/// <summary>
	/// Registers a <see cref="JWeak"/> instance in current VM.
	/// </summary>
	/// <param name="jWeak">A <see cref="JWeak"/> instance.</param>
	[return: NotNullIfNotNull(nameof(jWeak))]
	internal JWeak? Register(JWeak? jWeak) => this._cache.Register(jWeak);
	/// <summary>
	/// Removes <paramref name="jGlobal"/> from current VM cache.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	internal void Remove(JGlobalBase? jGlobal)
	{
		if (jGlobal is null || jGlobal.IsDefault) return;
		switch (jGlobal)
		{
			case JGlobal:
				this._cache.Remove(jGlobal.As<JGlobalRef>());
				break;
			case JWeak:
				this._cache.Remove(jGlobal.As<JWeakRef>());
				break;
		}
	}
	/// <summary>
	/// Loads global instance in given <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JGlobal"/> instance.</returns>
	internal JGlobal LoadGlobal(JClassObject jClass)
	{
		ObjectLifetime lifetime = jClass.Lifetime;
		if (!this._cache.GlobalClassCache.TryGetValue(jClass.Hash, out JGlobal? jGlobal))
			jGlobal = new(this, new(jClass), false, default);
		lifetime.SetGlobal(jGlobal);
		return jGlobal;
	}
	/// <summary>
	/// Retrieves <see cref="AccessCache"/> for <paramref name="classRef"/>.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="AccessCache"/> instance.</returns>
	internal AccessCache? GetAccess(JClassLocalRef classRef)
		=> this._cache.GlobalClassCache[classRef] ?? this._cache.WeakClassCache[classRef];
	/// <inheritdoc cref="JVirtualMachineCache.CreateTransaction()"/>
	internal JniTransaction CreateTransaction() => this._cache.CreateTransaction();
	/// <inheritdoc cref="JVirtualMachineCache.CreateUnaryTransaction()"/>
	internal INativeTransaction CreateUnaryTransaction() => this._cache.CreateUnaryTransaction();
	/// <inheritdoc cref="JVirtualMachineCache.CreateDuplexTransaction()"/>
	internal INativeTransaction CreateDuplexTransaction() => this._cache.CreateDuplexTransaction();
	/// <inheritdoc cref="JVirtualMachineCache.CreateSynchronized(JEnvironment, JReferenceObject)"/>
	public IDisposable CreateSynchronized(JEnvironment env, JReferenceObject jObject)
		=> this._cache.CreateSynchronized(env, jObject);
	/// <summary>
	/// Indicates whether <paramref name="weakRef"/> can be removed safely.
	/// </summary>
	/// <param name="weakRef">A <see cref="JWeakRef"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="weakRef"/> can be removed safely;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	internal Boolean SecureRemove(JWeakRef weakRef) => this._cache.InTransaction(weakRef.Pointer);
	/// <summary>
	/// Indicates whether <paramref name="globalRef"/> can be removed safely.
	/// </summary>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="globalRef"/> can be removed safely;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	internal Boolean SecureRemove(JGlobalRef globalRef) => this._cache.InTransaction(globalRef.Pointer);
	/// <summary>
	/// Indicates whether <paramref name="localRef"/> can be removed safely.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="localRef"/> can be removed safely;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	internal Boolean SecureRemove(JObjectLocalRef localRef) => !this._cache.InTransaction(localRef.Pointer);
	/// <summary>
	/// Registers native methods for given class.
	/// </summary>
	/// <param name="classHash">Class hash.</param>
	/// <param name="calls">A <see cref="JNativeCall"/> array.</param>
	internal void RegisterNatives(String classHash, IReadOnlyList<JNativeCall> calls)
		=> this._cache.NativesCache[classHash] = calls;
	/// <summary>
	/// Unregister any native method for given class.
	/// </summary>
	/// <param name="classHash">Class hash.</param>
	public void UnregisterNatives(String classHash) => this._cache.NativesCache.Clear(classHash);

	/// <summary>
	/// Retrieves the <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="env">Output. <see cref="IEnvironment"/> instance.</param>
	/// <returns>The <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.</returns>
	internal static IInvokedVirtualMachine GetVirtualMachine(JVirtualMachineRef reference, JEnvironmentRef envRef,
		out IEnvironment env)
	{
		JVirtualMachine vm = ReferenceCache.Instance.Get(reference, out _, true);
		env = vm._cache.ThreadCache.Get(envRef, out _);
		if (vm is IInvokedVirtualMachine invoked) return invoked;
		return new Invoked(vm);
	}
	/// <summary>
	/// Detaches current thread from <see cref="IVirtualMachine"/> referenced by <paramref name="vmRef"/>.
	/// </summary>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <param name="thread">A <see cref="Thread"/> instance.</param>
	internal static void DetachCurrentThread(JVirtualMachineRef vmRef, Thread thread)
	{
		ValidationUtilities.ThrowIfDifferentThread(thread);
		JVirtualMachine vm = ReferenceCache.Instance.Get(vmRef, out _);
		DetachCurrentThreadDelegate detachCurrentThread = vm._cache.GetDelegate<DetachCurrentThreadDelegate>();
		JResult result = detachCurrentThread(vmRef);
		if (result != JResult.Ok) throw new JniException(result);
	}
	/// <summary>
	/// Removes the <see cref="IEnvironment"/> instance referenced by <paramref name="envRef"/>
	/// into the <see cref="IVirtualMachine"/> referenced by <paramref name="vmRef"/>.
	/// </summary>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	internal static void RemoveEnvironment(JVirtualMachineRef vmRef, JEnvironmentRef envRef)
	{
		JVirtualMachine vm = ReferenceCache.Instance.Get(vmRef, out _);
		vm._cache.ThreadCache.Remove(envRef);
	}
}