namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements the <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JVirtualMachine : IVirtualMachine
{
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
	/// Indicates whether native call adapters should check parameter references type.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean CheckRefTypeNativeCallEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableCheckRefTypeNativeCall", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether native call adapters should check parameter class object class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean CheckClassRefNativeCallEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
			=> !AppContext.TryGetSwitch("JNetInterface.DisableCheckClassRefNativeCall", out Boolean disable) ||
				!disable;
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
		try
		{
			return this.AttachThread(args);
		}
		catch (Exception)
		{
			switch (purpose)
			{
				case ThreadPurpose.ReleaseSequence:
				case ThreadPurpose.RemoveGlobalReference:
				case ThreadPurpose.CheckGlobalReference:
				case ThreadPurpose.CheckAssignability:
				case ThreadPurpose.SynchronizeGlobalReference:
					return new DeadThread(this);
				case ThreadPurpose.ExceptionExecution:
				case ThreadPurpose.CreateGlobalReference:
				case ThreadPurpose.FatalError:
				default:
					throw;
			}
		}
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
	/// <summary>
	/// Sets <typeparamref name="TReference"/> as main class.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	public static void SetMainClass<TReference>() where TReference : JReferenceObject, IReferenceType<TReference>
	{
		String hash = MetadataHelper.GetExactMetadata<TReference>().Hash;
		if (!JVirtualMachine.userMainClasses.ContainsKey(hash))
			JVirtualMachine.userMainClasses.TryAdd(hash, ClassObjectMetadata.Create<TReference>());
	}
}