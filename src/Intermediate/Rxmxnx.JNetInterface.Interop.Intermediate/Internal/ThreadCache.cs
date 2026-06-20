namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// <see cref="INativeThread"/> cache implementation.
/// </summary>
/// <typeparam name="TThread">A CLR <see cref="INativeThread{TThread}"/> type.</typeparam>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal sealed class ThreadCache<TThread> : ReferenceHelperCache<TThread, JEnvironmentRef, ThreadCreationArgs?>
	where TThread : class, INativeThread<TThread>
{
	/// <summary>
	/// A <see cref="IVirtualMachineHost"/> instance.
	/// </summary>
	private readonly IVirtualMachineHost _host;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	public ThreadCache(IVirtualMachineHost host) => this._host = host;

	/// <inheritdoc/>
	public override TThread Get(JEnvironmentRef reference, out Boolean isNew, ThreadCreationArgs? arg = default)
	{
		TThread result = this.GetInstance(reference, true, out isNew, arg);
		JTrace.EnvironmentLoad(reference, isNew);
		return result;
	}
	/// <summary>
	/// Retrieves an unsafe <typeparamref name="TThread"/> instance from <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A reference pointer to <typeparamref name="TThread"/> instance.</param>
	/// <returns>A <typeparamref name="TThread"/> instance.</returns>
	public TThread GetUnsafe(JEnvironmentRef reference)
	{
		TThread result = this.GetInstance(reference, false, out Boolean isNew, default);
		JTrace.EnvironmentLoad(reference, isNew);
		return result;
	}
	/// <summary>
	/// Retrieves the <typeparamref name="TThread"/> instance associated with current thread.
	/// </summary>
	/// <returns>The <typeparamref name="TThread"/> instance associated with current thread.</returns>
	public TThread? GetAttachedThread()
	{
		JResult result = this._host.GetEnv(out JEnvironmentRef envRef, (Int32)JRuntimeVersion.SEd2);
		return result is JResult.Ok ? this.Get(envRef, out _) : default;
	}
	/// <summary>
	/// Attaches current thread to VM.
	/// </summary>
	/// <param name="args">A <see cref="ThreadCreationArgs"/> instance.</param>
	/// <returns>A <see cref="IThread"/> instance.</returns>
	public IThread AttachThread(ThreadCreationArgs args)
	{
		ImplementationValidationUtilities.ThrowIfProxy(args.ThreadGroup);
		IThread thread;
		Boolean isNew;
		if (this.GetAttachedThread() is { } env)
		{
			thread = TThread.Create(env);
			isNew = false;
		}
		else
		{
			thread = this.InitializeThread(args);
			isNew = true;
		}
		JTrace.InvokeAt(thread, isNew);
		return thread;
	}
	/// <summary>
	/// Attaches current thread to VM.
	/// </summary>
	/// <param name="purpose">A <see cref="ThreadCreationArgs"/> instance.</param>
	/// <returns>A <see cref="IThread"/> instance.</returns>
	public IThread AttachThread(ThreadPurpose purpose) => this.AttachThread(ThreadCreationArgs.Create(purpose));

	/// <inheritdoc/>
	protected override TThread Create(JEnvironmentRef reference, ThreadCreationArgs? args)
		=> !args.HasValue ? TThread.Create(this._host, reference) : TThread.Create(this._host, reference, args.Value);
	/// <inheritdoc/>
	protected override void Destroy(TThread instance)
	{
		if (instance is IDisposable disposable)
			disposable.Dispose();
	}

	/// <summary>
	/// Retrieves a <typeparamref name="TThread"/> instance from <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A reference pointer to <typeparamref name="TThread"/> instance.</param>
	/// <param name="safeRequest">Indicates whether the current request is from a safe JNI call.</param>
	/// <param name="arg">Creation argument.</param>
	/// <param name="isNew">Indicates whether current object is new in cache.</param>
	/// <returns>A <typeparamref name="TThread"/> instance.</returns>
	private TThread GetInstance(JEnvironmentRef reference, Boolean safeRequest, out Boolean isNew,
		ThreadCreationArgs? arg)
	{
		TThread result = base.Get(reference, out isNew, arg);
		if (safeRequest && !isNew && (!result.IsAttached || !result.Thread.IsAlive))
		{
			// The JVM has been recycled the JNIEnv* pointer.
			JTrace.EnvironmentInvalidLoad(reference, result.Thread.ManagedThreadId);
			this.Remove(result.Reference);
			result = base.Get(reference, out isNew, arg);
		}
		JTrace.EnvironmentLoad(reference, isNew);
		return result;
	}
	/// <summary>
	/// Initializes a new <see cref="IThread"/> instance for current thread.
	/// </summary>
	/// <param name="args">A <see cref="ThreadCreationArgs"/> instance.</param>
	/// <returns>A new <see cref="IThread"/> instance.</returns>
	private unsafe IThread InitializeThread(ThreadCreationArgs args)
	{
		TThread env;
		using INativeTransaction jniTransaction = this._host.MemoryManager.CreateTransaction(1);
		fixed (Byte* ptr = &MemoryMarshal.GetReference((ReadOnlySpan<Byte>)args.Name))
		{
			VirtualMachineArgumentValue arg = ThreadCache<TThread>.CreateAttachArgument(jniTransaction, ptr, args);
			JResult result = this._host.AttachThread(args.IsDaemon, arg, out JEnvironmentRef envRef);
			ImplementationValidationUtilities.ThrowIfInvalidResult(result);
			env = this.Get(envRef, out _, args);
		}
		return env as IThread ?? TThread.Create(env, true);
	}

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