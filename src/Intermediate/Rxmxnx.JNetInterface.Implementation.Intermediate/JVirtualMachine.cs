namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements the <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JVirtualMachine : IVirtualMachine
{
	/// <summary>
	/// Indicates whether metadata for built-in throwable objects should be auto-registered.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean BuiltInThrowableAutoRegistered
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
			=> !AppContext.TryGetSwitch("JNetInterface.DisableBuiltInThrowableAutoRegistration", out Boolean disable) ||
				!disable;
	}
	/// <summary>
	/// Indicates whether metadata for reflection objects should be auto-registered.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean ReflectionAutoRegistered
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
			=> !AppContext.TryGetSwitch("JNetInterface.DisableReflectionAutoRegistration", out Boolean disable) ||
				!disable;
	}
	/// <summary>
	/// Indicates whether metadata for NIO objects should be auto-registered.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean NioAutoRegistered
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableNioAutoRegistration", out Boolean disable) || !disable;
	}

	/// <summary>
	/// Indicates whether final user-types should be treated as real classes at runtime.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean FinalUserTypeRuntimeEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => AppContext.TryGetSwitch("JNetInterface.EnableFinalUserTypeRuntime", out Boolean enable) && enable;
	}

	/// <summary>
	/// Indicates whether the current virtual machine remains alive.
	/// </summary>
	public virtual Boolean IsAlive => true;
	/// <inheritdoc/>
	public JVirtualMachineRef Reference => this._cache.Reference;

	Boolean IVirtualMachine.NoProxy => true;
	IEnvironment? IVirtualMachine.GetEnvironment() => this.GetEnvironment();

	IThread IVirtualMachine.CreateThread(ThreadPurpose purpose)
	{
		if (!this.IsAlive) return new DeadThread(this);
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

	/// <inheritdoc/>
	public void FatalError(String? message) => this.FatalError((CString?)message);
	/// <inheritdoc/>
	public void FatalError(CString? message)
	{
		ReadOnlySpan<Byte> utf8Message = JEnvironment.GetSafeSpan(message);
		using IThread thread = this.AttachThread(ThreadCreationArgs.Create(ThreadPurpose.FatalError));
		JEnvironment env = this.GetEnvironment(thread.Reference);
		env.FatalError(utf8Message);
	}

	/// <summary>
	/// Registers <typeparamref name="TReference"/> as valid datatype for the current process.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TDataType}"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current datatype was registered; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean Register<TReference>() where TReference : JReferenceObject, IReferenceType<TReference>
		=> MetadataHelper.Register<TReference>();
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
	/// <returns>
	/// <set langword="true"/> if the <see cref="IVirtualMachine"/> instance referenced by
	/// <paramref name="reference"/> was removed successfully; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean RemoveVirtualMachine(JVirtualMachineRef reference)
	{
		ReferenceCache.Instance.Get(reference)?._cache.ClearCache();
		return ReferenceCache.Instance.Remove(reference);
	}
}