namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JVirtualMachine : IVirtualMachine
{
	/// <inheritdoc/>
	public JVirtualMachineRef Reference => this._cache.Reference;

	IEnvironment? IVirtualMachine.GetEnvironment() => this.GetEnvironment();

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

	/// <inheritdoc/>
	public void FatalError(String? message) => this.FatalError((CString?)message);
	/// <inheritdoc/>
	public void FatalError(CString? message)
	{
		ReadOnlySpan<Byte> utf8Message = message is null ? ReadOnlySpan<Byte>.Empty :
			!message.IsNullTerminated ? message : (CString)message.Clone();
		using IThread thread = this.AttachThread(ThreadCreationArgs.Create(ThreadPurpose.FatalError));
		JEnvironment env = this.GetEnvironment(thread.Reference);
		utf8Message.WithSafeFixed(env, JEnvironment.FatalError);
	}

	/// <summary>
	/// Registers <typeparamref name="TReference"/> as valid datatype for current process.
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
	public static void RemoveVirtualMachine(JVirtualMachineRef reference)
	{
		ReferenceCache.Instance.Get(reference, out _)._cache.ClearCache();
		ReferenceCache.Instance.Remove(reference);
	}
}