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
	/// <param name="reference">A <see cref="JVirtualMachineRef"/> reference.</param>
	private JVirtualMachine(JVirtualMachineRef reference) => this._cache = new(reference);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cache">A <see cref="JVirtualMachineCache"/> reference.</param>
	private JVirtualMachine(JVirtualMachineCache cache) => this._cache = cache;

	/// <inheritdoc/>
	public JVirtualMachineRef Reference => this._cache.Reference;

	IEnvironment? IVirtualMachine.GetEnvironment() => throw new NotImplementedException();
	IEnvironment IVirtualMachine.GetEnvironment(JEnvironmentRef envRef) => throw new NotImplementedException();
	IThread IVirtualMachine.CreateThread(ThreadPurpose purpose) => throw new NotImplementedException();
	IThread IVirtualMachine.InitializeThread(CString threadName, JObject? threadGroup, Int32 version)
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
		env = default!; //TODO: Implement IEnvironment creation.
		if (vm is IInvokedVirtualMachine invoked)
			return invoked;
		return new Invoked(vm);
	}
}