namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JVirtualMachine : IVirtualMachine
{
	/// <inheritdoc/>
	public JVirtualMachineRef Reference => this._cache.Reference;

	IEnvironment? IVirtualMachine.GetEnvironment() => this.GetEnvironment();
	IEnvironment IVirtualMachine.GetEnvironment(JEnvironmentRef envRef) => this._cache.ThreadCache.Get(envRef, out _);
	IThread IVirtualMachine.CreateThread(ThreadPurpose purpose)
	{
		ThreadCreationArgs args = ThreadCreationArgs.Create(purpose);
		return this.AttachThread(args);
	}
	IThread IVirtualMachine.InitializeThread(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this.AttachThread(new() { Name = threadName, ThreadGroup = threadGroup, Version = version, });
	IThread IVirtualMachine.InitializeDaemon(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this.AttachThread(new()
		{
			Name = threadName, ThreadGroup = threadGroup, Version = version, IsDaemon = true,
		});

	/// <summary>
	/// Detaches the current thread from a JVM.
	/// </summary>
	internal void DetachCurrentThread()
	{
		DetachCurrentThreadDelegate del = this._cache.GetDelegate<DetachCurrentThreadDelegate>();
		del(this._cache.Reference);
	}

	/// <summary>
	/// Retrieves the <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <returns>The <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.</returns>
	public static IVirtualMachine GetVirtualMachine(JVirtualMachineRef reference)
		=> ReferenceCache.Instance.Get(reference, out _);
	/// <summary>
	/// Removes the <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JVirtualMachineRef"/> reference.</param>
	public static void RemoveVirtualMachine(JVirtualMachineRef reference) => ReferenceCache.Instance.Remove(reference);

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