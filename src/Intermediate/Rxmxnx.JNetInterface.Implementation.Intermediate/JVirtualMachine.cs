namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements the <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JVirtualMachine : IVirtualMachine
{
	/// <summary>
	/// Android API level.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Int32? AndroidApiLevel => AndroidHelper.IsZygote ? AndroidHelper.ApiLevel : default;

	/// <summary>
	/// Indicates whether the current virtual machine remains alive.
	/// </summary>
	public virtual Boolean IsAlive => true;
	/// <inheritdoc cref="IVirtualMachine.Version"/>
	public JRuntimeVersion Version => this._core.GetVersion(this);
	/// <summary>
	/// Indicates whether current instance is disposable.
	/// </summary>
	public virtual Boolean IsDisposable => false;
	/// <inheritdoc/>
	public JVirtualMachineRef Reference => this._core.Reference;

	Boolean IVirtualMachine.NoProxy => true;
	IEnvironment? IVirtualMachine.GetEnvironment() => this._core.ThreadCache.GetAttachedThread();
	JRuntimeVersion IVirtualMachine.Version
	{
		get
		{
			if (JavaStandardFeature.GetRuntimeVersion() is { } jre) return jre;
			return AndroidFeature.IsFixedAndroid ? JRuntimeVersion.J6 : this.Version;
		}
	}
	Int32 IVirtualMachine.AndroidApiLevel
	{
		get
		{
			if (JavaStandardFeature.IsFixedRuntimeVersion) return -1;
			if (AndroidFeature.ApiLevel is { } apiLevel) return apiLevel;
			return JVirtualMachine.AndroidApiLevel ?? -1;
		}
	}

	IThread IVirtualMachine.CreateThread(ThreadPurpose purpose) => this._core.CreateThread(this.IsAlive, purpose);
	IThread IVirtualMachine.InitializeThread(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this._core.AttachThread(new() { Name = threadName, ThreadGroup = threadGroup, Version = version, });
	IThread IVirtualMachine.InitializeDaemon(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this._core.AttachThread(new()
		{
			Name = threadName,
			ThreadGroup = threadGroup,
			Version = version,
			IsDaemon = true,
		});

	/// <inheritdoc/>
	public void FatalError(String? message) => this.FatalError((CString?)message);
	/// <inheritdoc/>
	public void FatalError(CString? message)
	{
		ReadOnlySpan<Byte> utf8Message = EnvironmentCore.GetSafeSpan(message);
		using IThread thread = this._core.AttachThread(ThreadCreationArgs.Create(ThreadPurpose.FatalError));
		JEnvironment env = this.GetEnvironment(thread.Reference);
		env.FatalError(utf8Message);
	}
}