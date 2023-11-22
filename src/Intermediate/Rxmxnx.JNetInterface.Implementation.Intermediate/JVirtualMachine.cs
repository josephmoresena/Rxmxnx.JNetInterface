namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JVirtualMachine : IVirtualMachine
{
	/// <summary>
	/// <see cref="JVirtualMachine"/> cache.
	/// </summary>
	private readonly JVirtualMachineCache _cache;
	/// <summary>
	/// Indicates whether current instance is disposable.
	/// </summary>
	public virtual Boolean IsDisposable => false;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	private JVirtualMachine(JVirtualMachineRef vmRef) => this._cache = new(vmRef);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cache">A <see cref="JVirtualMachineCache"/> reference.</param>
	private JVirtualMachine(JVirtualMachineCache cache) => this._cache = cache;

	/// <inheritdoc/>
	public JVirtualMachineRef Reference => this._cache.Reference;

	IEnvironment? IVirtualMachine.GetEnvironment()
	{
		GetEnvDelegate del =
			this._cache.DelegateCache.GetDelegate<GetEnvDelegate>(this.Reference.Reference.Reference.GetEnvPointer);
		JResult result =  del(this.Reference, out JEnvironmentRef envRef, 0x00010006);
		return result != JResult.Ok ? this._cache.ThreadCache.Get(envRef, false, out _) : default(IEnvironment?);
	}
	IEnvironment IVirtualMachine.GetEnvironment(JEnvironmentRef envRef)
		=> this._cache.ThreadCache.Get(envRef, false, out _);
	IThread IVirtualMachine.CreateThread(ThreadPurpose purpose) => throw new NotImplementedException();
	IThread IVirtualMachine.InitializeThread(CString threadName, JObject? threadGroup, Int32 version)
		=> throw new NotImplementedException();
	IThread IVirtualMachine.InitializeDaemon(CString threadName, JObject? threadGroup,
		Int32 version)
		=> throw new NotImplementedException();

	/// <summary>
	/// Detaches the current thread from a JVM.
	/// </summary>
	internal void DetachCurrentThread()
	{
		DetachCurrentThreadDelegate del =
			this._cache.DelegateCache.GetDelegate<DetachCurrentThreadDelegate>(
				this._cache.Reference.Reference.Reference.DetachCurrentThreadPointer);
		del(this._cache.Reference);
	}

	/// <summary>
	/// Retrieves the <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <returns>The <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.</returns>
	public static IVirtualMachine GetVirtualMachine(JVirtualMachineRef reference)
		=> ReferenceCache.Instance.Get(reference, false, out Boolean _);
	/// <summary>
	/// Removes the <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JVirtualMachineRef"/> reference.</param>
	public static void RemoveVirtualMachine(JVirtualMachineRef reference)
		=> ReferenceCache.Instance.Remove(reference);

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
		JVirtualMachine vm = ReferenceCache.Instance.Get(reference, true, out Boolean _);
		env = vm._cache.ThreadCache.Get(envRef, false, out Boolean _);
		if (vm is IInvokedVirtualMachine invoked)
			return invoked;
		return new Invoked(vm);
	}
	/// <summary>
	/// Removes the <see cref="IEnvironment"/> instance referenced by <paramref name="envRef"/>
	/// into the <see cref="IVirtualMachine"/> referenced by <paramref name="vmRef"/>.
	/// </summary>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	internal static void RemoveEnvironment(JVirtualMachineRef vmRef, JEnvironmentRef envRef)
	{
		JVirtualMachine vm = ReferenceCache.Instance.Get(vmRef, false, out Boolean _);
		vm._cache.ThreadCache.Remove(envRef);
	}
}