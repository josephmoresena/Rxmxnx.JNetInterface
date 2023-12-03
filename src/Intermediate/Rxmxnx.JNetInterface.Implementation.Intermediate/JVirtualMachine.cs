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
		JResult result = del(this.Reference, out JEnvironmentRef envRef, 0x00010006);
		return result != JResult.Ok ? this._cache.ThreadCache.Get(envRef, out _) : default(IEnvironment?);
	}
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
		DetachCurrentThreadDelegate del =
			this._cache.DelegateCache.GetDelegate<DetachCurrentThreadDelegate>(
				this._cache.Reference.Reference.Reference.DetachCurrentThreadPointer);
		del(this._cache.Reference);
	}

	/// <summary>
	/// Attaches current thread to VM.
	/// </summary>
	/// <param name="args">A <see cref="ThreadCreationArgs"/> instance.</param>
	/// <returns></returns>
	private IThread AttachThread(ThreadCreationArgs args)
	{
		CString threadName = args.Name ?? CString.Zero;
		ValidationUtilities.ThrowIfDummy(args.ThreadGroup);
		return threadName.WithSafeFixed((this, args), JVirtualMachine.AttachThread);
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
		if (vm is IInvokedVirtualMachine invoked)
			return invoked;
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
		DetachCurrentThreadDelegate detachCurrentThread =
			vm._cache.DelegateCache.GetDelegate<DetachCurrentThreadDelegate>(
				vm._cache.Reference.Reference.Reference.DetachCurrentThreadPointer);
		detachCurrentThread(vmRef);
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

	/// <summary>
	/// Attach current thread to VM.
	/// </summary>
	/// <param name="name">Thread name.</param>
	/// <param name="args">Argument.</param>
	/// <returns>A <see cref="IThread"/> instance.</returns>
	/// <exception cref="JniException"/>
	private static IThread AttachThread(in IReadOnlyFixedMemory name,
		(JVirtualMachine vm, ThreadCreationArgs args) args)
	{
		AttachCurrentThreadDelegate attachCurrentThread =
			args.vm._cache.DelegateCache.GetDelegate<AttachCurrentThreadDelegate>(
				args.vm._cache.Reference.Reference.Reference.AttachCurrentThreadPointer);
		JGlobalRef threadGroupRef = args.args.ThreadGroup?.As<JGlobalRef>() ?? default;
		Int32 version = args.args.Version < IVirtualMachine.MinimalVersion ?
			IVirtualMachine.MinimalVersion :
			args.args.Version; 
		JVirtualMachineArgumentValue arg = new() { Name = (ReadOnlyValPtr<Byte>)name.Pointer, Group = threadGroupRef, Version = version, };
		JResult result = attachCurrentThread(args.vm._cache.Reference, out JEnvironmentRef envRef, in arg);

		if (result == JResult.Ok)
		{
			IEnvironment env = args.vm._cache.ThreadCache.Get(envRef, out _, args.args);
			if (env is IThread thread)
				return thread;
		}
		throw new JniException(result);
	}
}