namespace Rxmxnx.JNetInterface;

[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
public partial class JVirtualMachine
{
	/// <summary>
	/// <see cref="JVirtualMachine"/> cache.
	/// </summary>
	private readonly VirtualMachineCache _cache;
	/// <summary>
	/// Indicates whether current instance is disposable.
	/// </summary>
	public virtual Boolean IsDisposable => false;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	private JVirtualMachine(JVirtualMachineRef vmRef) => this._cache = new(this, vmRef);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cache">A <see cref="VirtualMachineCache"/> reference.</param>
	private JVirtualMachine(VirtualMachineCache cache) => this._cache = cache;

	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance associated with current thread.
	/// </summary>
	/// <returns>The <see cref="IEnvironment"/> instance associated with current thread.</returns>
	private unsafe JEnvironment? GetEnvironment()
	{
		JResult result = this._cache.GetInvokeInterface()
		                     .GetEnv(this.Reference, out JEnvironmentRef envRef, 0x00010006);
		return result is JResult.Ok ? this._cache.ThreadCache.Get(envRef, out _) : default;
	}
	/// <summary>
	/// Attaches current thread to VM.
	/// </summary>
	/// <param name="args">A <see cref="ThreadCreationArgs"/> instance.</param>
	/// <returns></returns>
	private unsafe IThread AttachThread(ThreadCreationArgs args)
	{
		ImplementationValidationUtilities.ThrowIfProxy(args.ThreadGroup);
		if (this.GetEnvironment() is { } env) return new JEnvironment.JThread(env);
		using INativeTransaction jniTransaction = this.CreateTransaction(1);
		fixed (Byte* ptr = &MemoryMarshal.GetReference((ReadOnlySpan<Byte>)args.Name))
		{
			VirtualMachineArgumentValue arg = JVirtualMachine.CreateAttachArgument(jniTransaction, ptr, args);
			JResult result = JVirtualMachine.AttachThread(this, args.IsDaemon, arg, out JEnvironmentRef envRef);
			ImplementationValidationUtilities.ThrowIfInvalidResult(result);
			env = this._cache.ThreadCache.Get(envRef, out _, args);
		}
		return (IThread)env;
	}
	/// <summary>
	/// Initialize main classes.
	/// </summary>
	private void InitializeClasses()
	{
		using IThread thread = this.AttachThread(ThreadCreationArgs.Create(ThreadPurpose.CreateGlobalReference));
		JEnvironment env = this.GetEnvironment(thread.Reference);
		this._cache.LoadMainClasses(env);
	}

	/// <summary>
	/// Attach current thread to VM.
	/// </summary>
	/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
	/// <param name="isDaemon">Indicates current thread will be attached as daemon.</param>
	/// <param name="arg">Attach argument.</param>
	/// <param name="envRef">Output. Attached thread <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>JNI code result.</returns>
	private static unsafe JResult AttachThread(JVirtualMachine vm, Boolean isDaemon, VirtualMachineArgumentValue arg,
		out JEnvironmentRef envRef)
		=> !isDaemon ?
			vm._cache.GetInvokeInterface().AttachCurrentThread(vm._cache.Reference, out envRef, in arg) :
			vm._cache.GetInvokeInterface().AttachCurrentThreadAsDaemon(vm._cache.Reference, out envRef, in arg);
	/// <summary>
	/// Creates a <see cref="VirtualMachineArgumentValue"/> value from <paramref name="namePtr"/> and
	/// <paramref name="args"/>.
	/// </summary>
	/// <param name="jniTransaction">A <see cref="INativeTransaction"/> instance.</param>
	/// <param name="namePtr">Pointer to thread name.</param>
	/// <param name="args">A <see cref="ThreadCreationArgs"/> instance.</param>
	/// <returns>A <see cref="VirtualMachineArgumentValue"/> value.</returns>
	private static unsafe VirtualMachineArgumentValue CreateAttachArgument(INativeTransaction jniTransaction,
		Byte* namePtr, ThreadCreationArgs args)
	{
		JGlobalRef threadGroupRef = jniTransaction.Add<JGlobalRef>(args.ThreadGroup);
		Int32 version = args.Version < IVirtualMachine.MinimalVersion ? IVirtualMachine.MinimalVersion : args.Version;
		VirtualMachineArgumentValue arg = new(version, namePtr, threadGroupRef);
		return arg;
	}
}